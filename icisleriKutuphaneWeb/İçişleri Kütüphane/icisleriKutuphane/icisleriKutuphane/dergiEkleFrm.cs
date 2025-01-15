using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace icisleriKutuphane
{
    public partial class dergiEkleFrm : Form
    {
        public dergiEkleFrm()
        {
            InitializeComponent();
        }

        private string connectionString = "Data Source=EXCALIBUR\\SQLEXPRESS;Initial Catalog=icisleriKutuphanesi;Integrated Security=True";

        private void dergiEkleFrm_Load(object sender, EventArgs e)
        {

        }


        private void btn_sureliYayinEkle_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Demirbaş numarasının var olup olmadığını kontrol et
                    SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM sureliYayinlar WHERE Demirbas = @Demirbas", connection);
                    checkCommand.Parameters.AddWithValue("@Demirbas", txtDemirbas.Text);
                    int count = (int)checkCommand.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("Bu demirbaş numarası zaten mevcut. Lütfen farklı bir demirbaş numarası giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Kullanıcıya yayını eklemek isteyip istemediğini sor
                    DialogResult result = MessageBox.Show($"Adı '{txtEser.Text}' ve sorumlu '{txtSorumlular.Text}' olan yayını eklemek istiyor musunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.No)
                    {
                        return; // Kullanıcı "Hayır" seçeneğine tıklarsa işlem iptal edilir
                    }

                    // Yayın ekleme işlemi
                    SqlCommand insertCommand = new SqlCommand(
                        "INSERT INTO sureliYayinlar (KayitTarih, YayinTarih, BasimTarih, AltTur, Sure, DilBir, Diliki, TemelGiris, Demirbas, Durumu, " +
                        "Oda, Kopya, Sekil, AramaGrubu, OzelNot, Sorumlular, Eser, Baslik, Notlar, YayinYeri, Yayinlayan, Matbaa, issn, FizikselNiteleme, " +
                        "SureliAyrinti, URLadres, MarcEkGiris, OduncVerilmeyecekYayin) " +
                        "VALUES (@KayitTarih, @YayinTarih, @BasimTarih, @AltTur, @Sure, @DilBir, @Diliki, @TemelGiris, @Demirbas, @Durumu, " +
                        "@Oda, @Kopya, @Sekil, @AramaGrubu, @OzelNot, @Sorumlular, @Eser, @Baslik, @Notlar, @YayinYeri, @Yayinlayan, @Matbaa, @issn, @FizikselNiteleme, " +
                        "@SureliAyrinti, @URLadres, @MarcEkGiris, @OduncVerilmeyecekYayin)", connection);

                    insertCommand.Parameters.AddWithValue("@KayitTarih", DateTime.Now);
                    insertCommand.Parameters.AddWithValue("@YayinTarih", dtpYayinTarih.Value);
                    insertCommand.Parameters.AddWithValue("@BasimTarih", dtpBasimTarih.Value);
                    insertCommand.Parameters.AddWithValue("@AltTur", cbAltTur.Text);
                    insertCommand.Parameters.AddWithValue("@Sure", cbSure.Text);
                    insertCommand.Parameters.AddWithValue("@DilBir", cbDil1.Text);
                    insertCommand.Parameters.AddWithValue("@Diliki", cbDil2.Text);
                    insertCommand.Parameters.AddWithValue("@TemelGiris", cbTemelGiris.Text);
                    insertCommand.Parameters.AddWithValue("@Demirbas", txtDemirbas.Text);
                    insertCommand.Parameters.AddWithValue("@Durumu", cbDurumu.Text);
                    insertCommand.Parameters.AddWithValue("@Oda", cbOda.Text);
                    insertCommand.Parameters.AddWithValue("@Kopya", int.Parse(txtKopya.Text));
                    insertCommand.Parameters.AddWithValue("@Sekil", cbSekil.Text);
                    insertCommand.Parameters.AddWithValue("@AramaGrubu", cbAramaGrubu.Text);
                    insertCommand.Parameters.AddWithValue("@OzelNot", richTextBox_ozelNot.Text);
                    insertCommand.Parameters.AddWithValue("@Sorumlular", txtSorumlular.Text);
                    insertCommand.Parameters.AddWithValue("@Eser", txtEser.Text);
                    insertCommand.Parameters.AddWithValue("@Baslik", txtAnaKisaltilmisBaslik.Text);
                    insertCommand.Parameters.AddWithValue("@Notlar", richTextBox_Not.Text);
                    insertCommand.Parameters.AddWithValue("@YayinYeri", txtYayinYeri.Text);
                    insertCommand.Parameters.AddWithValue("@Yayinlayan", txtYayinlayan.Text);
                    insertCommand.Parameters.AddWithValue("@Matbaa", txtMatbaa.Text);
                    insertCommand.Parameters.AddWithValue("@issn", txtissn.Text);
                    insertCommand.Parameters.AddWithValue("@FizikselNiteleme", txtFizikselNiteleme.Text);
                    insertCommand.Parameters.AddWithValue("@SureliAyrinti", txtSureliAyrinti.Text);
                    insertCommand.Parameters.AddWithValue("@URLadres", txtUrl.Text);
                    insertCommand.Parameters.AddWithValue("@MarcEkGiris", MarcEkGiris.Text);
                    insertCommand.Parameters.AddWithValue("@OduncVerilmeyecekYayin", checkBoxOduncVerilmeyecekYayin.Checked);

                    insertCommand.ExecuteNonQuery();
                    MessageBox.Show("Süreli Yayın Kaydı Yapıldı");

                    ClearTextBoxes();
                    ClearRichTextBoxes();
                    ClearComboBoxes();
                }
            }
        }

        private bool ValidateFields()
        {
            bool isValid = true;

            // Uyarı rengini belirle
            Color warningColor = Color.FromArgb(205, 92, 92);

            // Alanları kontrol et ve ilgili label'ı göster
            if (string.IsNullOrWhiteSpace(dtpYayinTarih.Text))
            {
                lblYayinTarihi.Visible = true;
                lblYayinTarihi.ForeColor = Color.Red;
                lblYayinTarihi.Text = "*";
                isValid = false;
            }
            else
            {
                lblYayinTarihi.Visible = false;
            }

            if (string.IsNullOrWhiteSpace(dtpBasimTarih.Text))
            {
                lblBasimTarihi.Visible = true;
                lblBasimTarihi.ForeColor = Color.Red;
                lblBasimTarihi.Text = "*";
                isValid = false;
            }
            else
            {
                lblBasimTarihi.Visible = false;
            }

            if (string.IsNullOrWhiteSpace(cbAltTur.Text))
            {
                lblAltTur.Visible = true;
                lblAltTur.ForeColor = Color.Red;
                lblAltTur.Text = "*";
                isValid = false;
            }
            else
            {
                lblAltTur.Visible = false;
            }

            if (string.IsNullOrWhiteSpace(cbSure.Text))
            {
                lblSure.Visible = true;
                lblSure.ForeColor = Color.Red;
                lblSure.Text = "*";
                isValid = false;
            }
            else
            {
                lblSure.Visible = false;
            }

            if (string.IsNullOrWhiteSpace(cbDil1.Text))
            {
                lblDil.Visible = true;
                lblDil.ForeColor = Color.Red;
                lblDil.Text = "*";
                isValid = false;
            }
            else
            {
                lblDil.Visible = false;
            }

            if (string.IsNullOrWhiteSpace(cbTemelGiris.Text))
            {
                lblTemelGiris.Visible = true;
                lblTemelGiris.ForeColor = Color.Red;
                lblTemelGiris.Text = "*";
                isValid = false;
            }
            else
            {
                lblTemelGiris.Visible = false;
            }

            if (string.IsNullOrWhiteSpace(txtDemirbas.Text))
            {
                lblDemirbas.Visible = true;
                lblDemirbas.ForeColor = Color.Red;
                lblDemirbas.Text = "*";
                isValid = false;
            }
            else
            {
                lblDemirbas.Visible = false;
            }

            if (string.IsNullOrWhiteSpace(cbOda.Text))
            {
                lblOda.Visible = true;
                lblOda.ForeColor = Color.Red;
                lblOda.Text = "*";
                isValid = false;
            }
            else
            {
                lblOda.Visible = false;
            }

            if (string.IsNullOrWhiteSpace(txtKopya.Text))
            {
                lblKopya.Visible = true;
                lblKopya.ForeColor = Color.Red;
                lblKopya.Text = "*";
                isValid = false;
            }
            else
            {
                lblKopya.Visible = false;
            }

            if (string.IsNullOrWhiteSpace(cbAramaGrubu.Text))
            {
                lblAramaGrubu.Visible = true;
                lblAramaGrubu.ForeColor = Color.Red;
                lblAramaGrubu.Text = "*";
                isValid = false;
            }
            else
            {
                lblAramaGrubu.Visible = false;
            }

            if (string.IsNullOrWhiteSpace(txtEser.Text))
            {
                lblEser.Visible = true;
                lblEser.ForeColor = Color.Red;
                lblEser.Text = "*";
                isValid = false;
            }
            else
            {
                lblEser.Visible = false;
            }

            if (string.IsNullOrWhiteSpace(txtFizikselNiteleme.Text))
            {
                lblFizikselNiteleme.Visible = true;
                lblFizikselNiteleme.ForeColor = Color.Red;
                lblFizikselNiteleme.Text = "*";
                isValid = false;
            }
            else
            {
                lblFizikselNiteleme.Visible = false;
            }

            if (!isValid)
            {
                MessageBox.Show("Lütfen tüm zorunlu alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return isValid;
        }

        private void ClearTextBoxes()
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Clear();
                }
            }
        }

        private void ClearRichTextBoxes()
        {
            foreach (Control control in this.Controls)
            {
                if (control is RichTextBox)
                {
                    ((RichTextBox)control).Clear();
                }
            }
        }

        private void ClearComboBoxes()
        {
            foreach (Control control in this.Controls)
            {
                if (control is ComboBox)
                {
                    ((ComboBox)control).SelectedIndex = -1;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 anasayfaFormu = new Form1();
            anasayfaFormu.Show();
            this.Hide();
            anasayfaFormu.FormClosed += (s, args) => this.Close();
        }

        private void richTextBox_uyeNot_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

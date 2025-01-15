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
    public partial class dergiListeleFrm : Form
    {
        public dergiListeleFrm()
        {
            InitializeComponent();
        }

        private SqlConnection baglanti = new SqlConnection("Data Source=EXCALIBUR\\SQLEXPRESS;Initial Catalog=icisleriKutuphanesi;Integrated Security=True");
        private DataSet daset = new DataSet();
        private string connectionString = "Data Source=EXCALIBUR\\SQLEXPRESS;Initial Catalog=icisleriKutuphanesi;Integrated Security=True";

        private void sureliYayinListele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("SELECT * FROM sureliYayinlar", baglanti);
            daset.Tables.Clear(); // Önceki verileri temizle
            adtr.Fill(daset, "sureliYayinlar");
            dataGriddergi.DataSource = daset.Tables["sureliYayinlar"];
            baglanti.Close();
        }

        private void btn_anasayfaDon_Click(object sender, EventArgs e)
        {
            Form1 anasayfaFormu = new Form1();
            anasayfaFormu.Show();
            this.Hide();
            anasayfaFormu.FormClosed += (s, args) => this.Close();
        }

        private void dergiListeleFrm_Load(object sender, EventArgs e)
        {
            sureliYayinListele();
        }

        private void btn_sureliYayinGuncelle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtdemirbas.Text))
            {
                MessageBox.Show("Lütfen demirbaş numarasını giriniz.", "Eksik Alan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Buton tıklama işlemini iptal et
            }

            string demirbas = txtdemirbas.Text;

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();

                // Kitabı seçme sorgusu
                SqlCommand komut = new SqlCommand("SELECT eser FROM sureliYayinlar WHERE Demirbas = @Demirbas", baglanti);
                komut.Parameters.AddWithValue("@Demirbas", demirbas);

                SqlDataReader read = komut.ExecuteReader();
                if (read.Read())
                {
                    read.Close(); // Okuma işlemi tamamlandıktan sonra kapat

                    // Güncelleme işlemi
                    SqlCommand updateCommand = new SqlCommand("UPDATE sureliYayinlar SET Eser = @Eser, Sorumlular = @Sorumlular, YayinYeri = @YayinYeri, " +
                        "Yayinlayan = @Yayinlayan, Matbaa = @Matbaa, Kopya = @Kopya, Baslik = @Baslik, FizikselNiteleme = @FizikselNiteleme WHERE demirbas = @demirbas", baglanti);

                    updateCommand.Parameters.AddWithValue("@Eser", txteser.Text);
                    updateCommand.Parameters.AddWithValue("@Sorumlular", txtsorumlular.Text);
                    updateCommand.Parameters.AddWithValue("@Yayinlayan", txtyayinlayan.Text);
                    updateCommand.Parameters.AddWithValue("@YayinYeri", txtyayinyeri.Text);
                    updateCommand.Parameters.AddWithValue("@Matbaa", txtmatbaa.Text);
                    updateCommand.Parameters.AddWithValue("@Kopya", txtKopya.Text);
                    updateCommand.Parameters.AddWithValue("@Baslik", txtAnaKisaltilmisBaslik.Text);
                    updateCommand.Parameters.AddWithValue("@FizikselNiteleme", txtfizikselNiteleme.Text);
                    updateCommand.Parameters.AddWithValue("@Demirbas", demirbas);

                    updateCommand.ExecuteNonQuery();

                    daset.Tables["kitaplar"].Clear();
                    sureliYayinListele();

                    MessageBox.Show("Güncelleme İşlemi Başarılı");
                    ClearTextBoxes();
                    ClearRichTextBoxes();
                    ClearComboBoxes();
                }
                else
                {
                    MessageBox.Show("Belirtilen demirbaş numarasıyla kitap bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearTextBoxes();
                }
            }
        }

        private void txt_demirbasAra_TextChanged(object sender, EventArgs e)
        {
            DataView dv = daset.Tables["sureliYayinlar"].DefaultView;
            dv.RowFilter = string.Format("Demirbas LIKE '%{0}%'", txtDemirbasAra.Text);
            dataGriddergi.DataSource = dv;
        }


        private void ClearTextBoxes()
        {
            foreach (Control control in Controls)
            {
                if (control is TextBox)
                {
                    control.Text = "";
                }
            }
        }

        private void ClearRichTextBoxes()
        {
            foreach (Control control in Controls)
            {
                if (control is RichTextBox)
                {
                    ((RichTextBox)control).Clear();
                }
            }
        }

        private void ClearComboBoxes()
        {
            foreach (Control control in Controls)
            {
                if (control is ComboBox)
                {
                    ((ComboBox)control).SelectedIndex = -1;
                }
            }
        }

        private void txtdemirbas_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtdemirbas.Text))
            {
                foreach (Control item in Controls)
                {
                    if (item is TextBox && item != txtdemirbas)
                    {
                        item.Text = "";
                    }
                }
                return;
            }

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("SELECT * FROM sureliYayinlar WHERE Demirbas = @Demirbas", baglanti);
                komut.Parameters.AddWithValue("@Demirbas", txtdemirbas.Text);
                SqlDataReader read = komut.ExecuteReader();

                if (read.Read())
                {
                    txteser.Text = read["Eser"].ToString();
                    txtsorumlular.Text = read["Sorumlular"].ToString();
                    txtyayinlayan.Text = read["Yayinlayan"].ToString();
                    txtyayinyeri.Text = read["YayinYeri"].ToString();
                    txtmatbaa.Text = read["Matbaa"].ToString();
                    txtKopya.Text = read["Kopya"].ToString();
                    txtAnaKisaltilmisBaslik.Text = read["Baslik"].ToString();
                    txtfizikselNiteleme.Text = read["FizikselNiteleme"].ToString();
                }
                else
                {
                    foreach (Control item in Controls)
                    {
                        if (item is TextBox && item != txtdemirbas)
                        {
                            item.Text = "";
                        }
                    }
                }
            }
        }

        private void btn_sureliYayinSil_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtdemirbas.Text))
            {
                MessageBox.Show("Lütfen demirbaş numarasını giriniz.", "Eksik Alan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Buton tıklama işlemini iptal et
            }

            string demirbas = txtdemirbas.Text;

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();

                SqlCommand komut = new SqlCommand("SELECT Eser FROM sureliYayinlar WHERE Demirbas = @Demirbas", baglanti);
                komut.Parameters.AddWithValue("@Demirbas", demirbas);

                SqlDataReader read = komut.ExecuteReader();
                if (read.Read())
                {
                    string kitapAdi = read["Eser"].ToString();
                    read.Close();

                    DialogResult dialogResult = MessageBox.Show($"{kitapAdi} isimli süreli yayını silmek istediğinizden emin misiniz?", "Süreli Yayın Silme", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {
                        string password = Microsoft.VisualBasic.Interaction.InputBox("Şifrenizi girin:", "Şifre Doğrulama", "");

                        if (password == "1234")
                        {
                            SqlCommand deleteCommand = new SqlCommand("DELETE FROM sureliYayinlar WHERE Demirbas = @Demirbas", baglanti);
                            deleteCommand.Parameters.AddWithValue("@Demirbas", demirbas);
                            deleteCommand.ExecuteNonQuery();

                            MessageBox.Show("Silme İşlemi Başarılı");

                            daset.Tables["sureliYayinlar"].Clear();
                            sureliYayinListele();
                            ClearTextBoxes();
                            ClearRichTextBoxes();
                            ClearComboBoxes();
                        }
                        else
                        {
                            MessageBox.Show("Yanlış şifre. Silme işlemi iptal edildi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            ClearTextBoxes();
                            ClearRichTextBoxes();
                            ClearComboBoxes();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Belirtilen demirbaş numarasıyla süreli yayın bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearTextBoxes();
                }
            }
        }

        private void txtDemirbasAra_TextChanged(object sender, EventArgs e)
        {
            DataView dv = daset.Tables["sureliYayinlar"].DefaultView;
            dv.RowFilter = string.Format("Demirbas LIKE '%{0}%'", txtDemirbasAra.Text);
            dataGriddergi.DataSource = dv;
        }
    }
}

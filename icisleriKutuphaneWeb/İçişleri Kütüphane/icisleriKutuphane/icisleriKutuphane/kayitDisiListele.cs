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
    public partial class kayitDisiListele : Form
    {
        public kayitDisiListele()
        {
            InitializeComponent();
        }

        private SqlConnection baglanti = new SqlConnection("Data Source=EXCALIBUR\\SQLEXPRESS;Initial Catalog=icisleriKutuphanesi;Integrated Security=True");
        private DataSet daset = new DataSet();
        private string connectionString = "Data Source=EXCALIBUR\\SQLEXPRESS;Initial Catalog=icisleriKutuphanesi;Integrated Security=True";

        private void kayitDisiListele_Load(object sender, EventArgs e)
        {
            kayitDisiKitapListele();
        }

        private void kayitDisiKitapListele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("SELECT * FROM kayitDisiKitaplar", baglanti);
            daset.Tables.Clear(); // Önceki verileri temizle
            adtr.Fill(daset, "kayitDisiKitaplar");
            dataGridView1.DataSource = daset.Tables["kayitDisiKitaplar"];
            baglanti.Close();
        }

        private void btn_kitapGuncelle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_Demirbas.Text))
            {
                MessageBox.Show("Lütfen demirbaş numarasını giriniz.", "Eksik Alan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Buton tıklama işlemini iptal et
            }

            string demirbas = txt_Demirbas.Text;

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();

                // Kitabı seçme sorgusu
                SqlCommand komut = new SqlCommand("SELECT eser FROM kayitDisiKitaplar WHERE demirbas = @demirbas", baglanti);
                komut.Parameters.AddWithValue("@demirbas", demirbas);

                SqlDataReader read = komut.ExecuteReader();
                if (read.Read())
                {
                    read.Close(); // Okuma işlemi tamamlandıktan sonra kapat

                    // Güncelleme işlemi
                    SqlCommand updateCommand = new SqlCommand("UPDATE kayitDisiKitaplar SET eser = @eser, yazar = @yazar, yayinlayan = @yayinlayan, " +
                        "yayinYeri = @yayinYeri, isbn = @isbn, yerNumarasi = @yerNumarasi WHERE demirbas = @demirbas", baglanti);

                    updateCommand.Parameters.AddWithValue("@eser", txt_Eser.Text);
                    updateCommand.Parameters.AddWithValue("@yazar", txt_Yazar.Text);
                    updateCommand.Parameters.AddWithValue("@yayinlayan", txt_Yayinlayan.Text);
                    updateCommand.Parameters.AddWithValue("@yayinYeri", txt_YayinYeri.Text);
                    updateCommand.Parameters.AddWithValue("@isbn", txt_isbnNo.Text);
                    updateCommand.Parameters.AddWithValue("@yerNumarasi", txt_yerNumarasi.Text);
                    updateCommand.Parameters.AddWithValue("@demirbas", demirbas);

                    updateCommand.ExecuteNonQuery();

                    daset.Tables["kitaplar"].Clear();
                    kayitDisiKitapListele();

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

        private void btn_kitapSil_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_Demirbas.Text))
            {
                MessageBox.Show("Lütfen demirbaş numarasını giriniz.", "Eksik Alan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Buton tıklama işlemini iptal et
            }

            string demirbas = txt_Demirbas.Text;

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();

                SqlCommand komut = new SqlCommand("SELECT eser FROM kayitDisiKitaplar WHERE demirbas = @demirbas", baglanti);
                komut.Parameters.AddWithValue("@demirbas", demirbas);

                SqlDataReader read = komut.ExecuteReader();
                if (read.Read())
                {
                    string kitapAdi = read["eser"].ToString();
                    read.Close();

                    DialogResult dialogResult = MessageBox.Show($"{kitapAdi} isimli kitabı silmek istediğinizden emin misiniz?", "Kitap Silme", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {
                        string password = Microsoft.VisualBasic.Interaction.InputBox("Şifrenizi girin:", "Şifre Doğrulama", "");

                        if (password == "1234")
                        {
                            SqlCommand deleteCommand = new SqlCommand("DELETE FROM kayitDisiKitaplar WHERE demirbas = @demirbas", baglanti);
                            deleteCommand.Parameters.AddWithValue("@demirbas", demirbas);
                            deleteCommand.ExecuteNonQuery();

                            MessageBox.Show("Silme İşlemi Başarılı");

                            daset.Tables["kitaplar"].Clear();
                            kayitDisiKitapListele();
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
                    MessageBox.Show("Belirtilen demirbaş numarasıyla kitap bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearTextBoxes();
                }
            }
        }

        private void txt_demirbasAra_TextChanged(object sender, EventArgs e)
        {
            DataView dv = daset.Tables["kayitDisiKitaplar"].DefaultView;
            dv.RowFilter = string.Format("demirbas LIKE '%{0}%'", txt_demirbasAra.Text);
            dataGridView1.DataSource = dv;
        }

        private void txt_Demirbas_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_Demirbas.Text))
            {
                foreach (Control item in Controls)
                {
                    if (item is TextBox && item != txt_Demirbas)
                    {
                        item.Text = "";
                    }
                }
                return;
            }

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("SELECT * FROM kayitDisiKitaplar WHERE demirbas = @demirbas", baglanti);
                komut.Parameters.AddWithValue("@demirbas", txt_Demirbas.Text);
                SqlDataReader read = komut.ExecuteReader();

                if (read.Read())
                {
                    txt_Eser.Text = read["eser"].ToString();
                    txt_Yazar.Text = read["yazar"].ToString();
                    txt_Yayinlayan.Text = read["yayinlayan"].ToString();
                    txt_YayinYeri.Text = read["yayinYeri"].ToString();
                    txt_isbnNo.Text = read["isbn"].ToString();
                    txt_yerNumarasi.Text = read["yerNumarasi"].ToString();
                }
                else
                {
                    foreach (Control item in Controls)
                    {
                        if (item is TextBox && item != txt_Demirbas)
                        {
                            item.Text = "";
                        }
                    }
                }
            }
        }

        private void btn_anasayfaDon_Click(object sender, EventArgs e)
        {
            Form1 anasayfaFormu = new Form1();
            anasayfaFormu.Show();
            this.Hide();
            anasayfaFormu.FormClosed += (s, args) => this.Close();
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
    }
}

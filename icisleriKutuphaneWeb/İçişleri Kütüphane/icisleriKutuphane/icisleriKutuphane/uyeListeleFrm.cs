using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace icisleriKutuphane
{
    public partial class uyeListeleFrm : Form
    {
        public uyeListeleFrm()
        {
            InitializeComponent();
        }

        private string connectionString = "Data Source=EXCALIBUR\\SQLEXPRESS;Initial Catalog=icisleriKutuphanesi;Integrated Security=True";
        private DataSet daset = new DataSet();

        private void uyeListeleFrm_Load(object sender, EventArgs e)
        {
            uyeListele();
        }


        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                txt_kullanıcıAra.Text = dataGridView1.CurrentRow.Cells["tc"].Value.ToString();
            }
        }

        private void txt_kullanıcıAra_TextChanged(object sender, EventArgs e)
        {
            daset.Tables["uyeler"]?.Clear();
            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                // SQL sorgusu
                SqlDataAdapter adtr = new SqlDataAdapter("SELECT * FROM uyeler WHERE tc LIKE @tc", baglanti);

                // Parametreyi tanımla
                adtr.SelectCommand.Parameters.Add("@tc", SqlDbType.NVarChar).Value = txt_kullanıcıAra.Text + "%";

                // Veriyi al
                adtr.Fill(daset, "uyeler");
                dataGridView1.DataSource = daset.Tables["uyeler"];
            }
        }

        private void btn_uyeSil_Click(object sender, EventArgs e)
        {
            // Eğer txt_Tc boşsa, silme yapma
            if (string.IsNullOrWhiteSpace(txt_Tc.Text))
            {
                MessageBox.Show("TC kimlik numarasını giriniz.", "Eksik Alan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Buton tıklama işlemini iptal et
            }

            string tc = txt_Tc.Text;

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();

                // Üyeyi seçme sorgusu
                SqlCommand komut = new SqlCommand("SELECT adSoyad FROM uyeler WHERE tc = @tc", baglanti);
                komut.Parameters.AddWithValue("@tc", tc);

                SqlDataReader read = komut.ExecuteReader();
                if (read.Read())
                {
                    string uyeAdi = read["adSoyad"].ToString();
                    read.Close(); // Okuma işlemi tamamlandıktan sonra kapat

                    // Silme onay penceresi
                    DialogResult dialogResult = MessageBox.Show($"{uyeAdi} isimli üyeyi silmek istediğinizden emin misiniz?", "Üye Silme", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {
                        // Şifre doğrulama penceresi
                        string password = Microsoft.VisualBasic.Interaction.InputBox("Şifrenizi girin:", "Şifre Doğrulama", "");

                        // Şifre kontrolü
                        if (password == "1234")
                        {
                            // Silme işlemi
                            SqlCommand deleteCommand = new SqlCommand("DELETE FROM uyeler WHERE tc = @tc", baglanti);
                            deleteCommand.Parameters.AddWithValue("@tc", tc);
                            deleteCommand.ExecuteNonQuery();

                            MessageBox.Show("Silme İşlemi Başarılı");

                            // Verileri güncelle
                            daset.Tables["uyeler"].Clear();
                            uyeListele();
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
                    MessageBox.Show("Belirtilen TC kimlik numarasıyla üye bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearTextBoxes();
                }
            }
        }

        private void uyeListele()
        {
            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                SqlDataAdapter adtr = new SqlDataAdapter("SELECT * FROM uyeler", baglanti);
                adtr.Fill(daset, "uyeler");
                dataGridView1.DataSource = daset.Tables["uyeler"];
            }
        }

        private void btn_uyeGuncelle_Click(object sender, EventArgs e)
        {

            // Eğer txt_Tc boşsa, güncelleme yapma
            if (string.IsNullOrWhiteSpace(txt_Tc.Text))
            {
                MessageBox.Show("TC kimlik numarasını giriniz.", "Eksik Alan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Buton tıklama işlemini iptal et
            }

            string tc = txt_Tc.Text;

            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();

                // Üyeyi seçme sorgusu
                SqlCommand selectCommand = new SqlCommand("SELECT adSoyad FROM uyeler WHERE tc = @tc", baglanti);
                selectCommand.Parameters.AddWithValue("@tc", tc);

                SqlDataReader read = selectCommand.ExecuteReader();
                if (read.Read())
                {
                    read.Close(); // Okuma işlemi tamamlandıktan sonra kapat

                    // Güncelleme sorgusu
                    SqlCommand updateCommand = new SqlCommand(
                        "UPDATE uyeler SET adSoyad = @adSoyad, telefon = @telefon, email = @email, " +
                        "adres = @adres, unvan = @unvan, isTelefonu = @isTelefonu, durum = @durum, oduncNot = @oduncNot, uyeNot = @uyeNot " +
                        "WHERE tc = @tc",
                        baglanti);

                    // Parametreleri ekle
                    updateCommand.Parameters.AddWithValue("@adSoyad", txt_uyeAdSoyad.Text);
                    updateCommand.Parameters.AddWithValue("@telefon", txt_uyeTelefon.Text);
                    updateCommand.Parameters.AddWithValue("@email", txt_uyeEmail.Text);
                    updateCommand.Parameters.AddWithValue("@adres", txt_uyeAdres.Text);
                    updateCommand.Parameters.AddWithValue("@unvan", txt_Unvan.Text);
                    updateCommand.Parameters.AddWithValue("@isTelefonu", txt_istelefon.Text);
                    updateCommand.Parameters.AddWithValue("@durum", cb_Durum.Text);
                    updateCommand.Parameters.AddWithValue("@oduncNot", string.IsNullOrWhiteSpace(txt_oduncNot.Text) ? (object)DBNull.Value : txt_oduncNot.Text);
                    updateCommand.Parameters.AddWithValue("@uyeNot", string.IsNullOrWhiteSpace(txt_uyeNot.Text) ? (object)DBNull.Value : txt_uyeNot.Text);
                    updateCommand.Parameters.AddWithValue("@tc", tc);

                    // Sorguyu çalıştır
                    updateCommand.ExecuteNonQuery();

                    // Kullanıcıya bildirim
                    MessageBox.Show("Güncelleme İşlemi Başarılı");
                    daset.Tables["uyeler"].Clear();
                    uyeListele();
                    ClearTextBoxes();
                    ClearRichTextBoxes();
                    ClearComboBoxes();
                }
                else
                {
                    MessageBox.Show("Belirtilen TC kimlik numarasıyla üye bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearTextBoxes();
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

        private void txt_Tc_TextChanged(object sender, EventArgs e)
        {
            if (txt_Tc.Text.Length == 11)
            {
                using (SqlConnection baglanti = new SqlConnection(connectionString))
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("SELECT * FROM uyeler WHERE tc = @tc", baglanti);
                    komut.Parameters.Add("@tc", SqlDbType.NVarChar).Value = txt_Tc.Text;

                    SqlDataReader read = komut.ExecuteReader();
                    if (read.Read())
                    {
                        txt_uyeAdSoyad.Text = read["adSoyad"].ToString();
                        txt_uyeTelefon.Text = read["telefon"].ToString();
                        txt_uyeEmail.Text = read["email"].ToString();
                        txt_uyeAdres.Text = read["adres"].ToString();
                        txt_Unvan.Text = read["unvan"].ToString();
                        txt_istelefon.Text = read["isTelefonu"].ToString();
                        cb_Durum.Text = read["durum"].ToString();
                        txt_oduncNot.Text = read["oduncNot"].ToString();
                        txt_uyeNot.Text = read["uyeNot"].ToString();
                    }
                    else
                    {
                        ClearTextBoxes();
                        ClearRichTextBoxes();
                        ClearComboBoxes();
                    }
                }
            }
            else if (txt_Tc.Text.Length == 0)
            {
                ClearTextBoxes();
                ClearRichTextBoxes();
                ClearComboBoxes();
            }
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

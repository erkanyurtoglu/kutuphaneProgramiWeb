using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace icisleriKutuphane
{
    public partial class uyeEkleFrm : Form
    {
        public uyeEkleFrm()
        {
            InitializeComponent();
        }

        private string connectionString = "Data Source=EXCALIBUR\\SQLEXPRESS;Initial Catalog=icisleriKutuphanesi;Integrated Security=True";

        private void btn_UyeEkle_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM uyeler WHERE tc = @tc", connection);
                    checkCommand.Parameters.AddWithValue("@tc", txt_uyeTcKimlik.Text);
                    int count = (int)checkCommand.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("Bu TC kimlik numarasına sahip bir üye zaten mevcut.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        SqlCommand insertCommand = new SqlCommand(
                            "INSERT INTO uyeler (adSoyad, tc, telefon, email, adres, calistigiBirim, " +
                            "unvan, isTelefonu, durum, okunanKitap, oduncNot, uyeNot) " +
                            "VALUES (@adSoyad, @tc, @telefon, @email, @adres, @calistigiBirim," +
                            "@unvan, @isTelefonu, @durum, @okunanKitap, @oduncNot, @uyeNot)",
                            connection);

                        insertCommand.Parameters.AddWithValue("@adSoyad", txt_uyeAdSoyad.Text);
                        insertCommand.Parameters.AddWithValue("@tc", txt_uyeTcKimlik.Text);
                        insertCommand.Parameters.AddWithValue("@telefon", txt_uyeTelefon.Text);
                        insertCommand.Parameters.AddWithValue("@email", txt_uyeEmail.Text);
                        insertCommand.Parameters.AddWithValue("@adres", txt_uyeAdres.Text);
                        insertCommand.Parameters.AddWithValue("@calistigiBirim", txt_calistigiBirim.Text);
                        insertCommand.Parameters.AddWithValue("@unvan", txt_unvan.Text);
                        insertCommand.Parameters.AddWithValue("@isTelefonu", txt_isTelefon.Text);
                        insertCommand.Parameters.AddWithValue("@durum", cb_durum.Text);
                        insertCommand.Parameters.AddWithValue("@okunanKitap", 0);
                        insertCommand.Parameters.AddWithValue("@oduncNot", richTextBox_oduncNot.Text);
                        insertCommand.Parameters.AddWithValue("@uyeNot", richTextBox_uyeNot.Text);

                        insertCommand.ExecuteNonQuery();
                        MessageBox.Show("Üye Kaydı Yapıldı");

                        ClearTextBoxes();
                        ClearRichTextBoxes();
                        ClearComboBoxes();
                    }
                }
            }
        }

        private bool ValidateFields()
        {
            bool isValid = true;

            // Uyarı rengini belirle
            Color warningColor = Color.FromArgb(205, 92, 92);

            // Alanları kontrol et ve ilgili label'ı göster
            if (string.IsNullOrWhiteSpace(txt_uyeAdSoyad.Text))
            {
                lbl_uyeAdSoyad.Visible = true;
                lbl_uyeAdSoyad.ForeColor = Color.Red;
                lbl_uyeAdSoyad.Text = "*";
                isValid = false;
            }
            else
            {
                lbl_uyeAdSoyad.Visible = false;
            }

            if (string.IsNullOrWhiteSpace(txt_uyeTcKimlik.Text))
            {
                lbl_uyeTcKimlik.Visible = true;
                lbl_uyeTcKimlik.ForeColor = Color.Red;
                lbl_uyeTcKimlik.Text = "*";
                isValid = false;
            }
            else
            {
                lbl_uyeTcKimlik.Visible = false;
            }

            if (string.IsNullOrWhiteSpace(txt_uyeTelefon.Text))
            {
                lbl_uyeTelefon.Visible = true;
                lbl_uyeTelefon.ForeColor = Color.Red;
                lbl_uyeTelefon.Text = "*";
                isValid = false;
            }
            else
            {
                lbl_uyeTelefon.Visible = false;
            }

            if (string.IsNullOrWhiteSpace(txt_uyeEmail.Text))
            {
                lbl_uyeEmail.Visible = true;
                lbl_uyeEmail.ForeColor = Color.Red;
                lbl_uyeEmail.Text = "*";
                isValid = false;
            }
            else
            {
                lbl_uyeEmail.Visible = false;
            }

            if (string.IsNullOrWhiteSpace(txt_uyeAdres.Text))
            {
                lbl_uyeAdres.Visible = true;
                lbl_uyeAdres.ForeColor = Color.Red;
                lbl_uyeAdres.Text = "*";
                isValid = false;
            }
            else
            {
                lbl_uyeAdres.Visible = false;
            }


            if (string.IsNullOrWhiteSpace(txt_calistigiBirim.Text))
            {
                lbl_calistigiBirim.Visible = true;
                lbl_calistigiBirim.ForeColor = Color.Red;
                lbl_calistigiBirim.Text = "*";
                isValid = false;
            }
            else
            {
                lbl_calistigiBirim.Visible = false;
            }

            if (string.IsNullOrWhiteSpace(txt_unvan.Text))
            {
                lbl_unvan.Visible = true;
                lbl_unvan.ForeColor = Color.Red;
                lbl_unvan.Text = "*";
                isValid = false;
            }
            else
            {
                lbl_unvan.Visible = false;
            }

            if (string.IsNullOrWhiteSpace(txt_isTelefon.Text))
            {
                lbl_isTelefon.Visible = true;
                lbl_isTelefon.ForeColor = Color.Red;
                lbl_isTelefon.Text = "*";
                isValid = false;
            }
            else
            {
                lbl_isTelefon.Visible = false;
            }

            if (string.IsNullOrWhiteSpace(cb_durum.Text))
            {
                lbl_durum.Visible = true;
                lbl_durum.ForeColor = Color.Red;
                lbl_durum.Text = "*";
                isValid = false;
            }
            else
            {
                lbl_durum.Visible = false;
            }

            if (!isValid)
            {
                MessageBox.Show("Lütfen tüm zorunlu alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return isValid;
        }

        private void ClearTextBoxes()
        {
            foreach (Control control in Controls)
            {
                if (control is TextBox && control != txt_okunanKitap)
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

        private void btn_anasayfaDon_Click(object sender, EventArgs e)
        {
            Form1 anasayfaFormu = new Form1();
            anasayfaFormu.Show();
            this.Hide();
            anasayfaFormu.FormClosed += (s, args) => this.Close();
        }

        private void uyeEkleFrm_Load(object sender, EventArgs e)
        {

        }
    }
}

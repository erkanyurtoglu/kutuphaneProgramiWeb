using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace icisleriKutuphane
{
    public partial class kullaniciGirisFrm : Form
    {
        public kullaniciGirisFrm()
        {
            InitializeComponent();
        }

        private void btn_personelGiris_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = txt_pKullaniciAd.Text;
            string sifre = txt_pSifre.Text;

            SqlConnection baglanti = new SqlConnection("Data Source=EXCALIBUR\\SQLEXPRESS;Initial Catalog=icisleriKutuphanesi;Integrated Security=True");

            try
            {
                baglanti.Open();
                string query = "SELECT COUNT(*) FROM personeller WHERE kullaniciAdi = @kullaniciAdi AND sifre = @sifre";
                SqlCommand komut = new SqlCommand(query, baglanti);
                komut.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
                komut.Parameters.AddWithValue("@sifre", sifre);

                int userCount = (int)komut.ExecuteScalar();

                if (userCount > 0)
                {
                    Form1 anaSayfa = new Form1();
                    anaSayfa.Show();
                    this.Hide();
                    anaSayfa.FormClosed += (s, args) => this.Close();
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifre yanlış!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void txt_pKullaniciAd_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_pSifre_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_kullaniciGiris_Click(object sender, EventArgs e)
        {
            string email = txt_kKullaniciAd.Text;
            string tc = txt_kSifre.Text;

            SqlConnection baglanti = new SqlConnection("Data Source=EXCALIBUR\\SQLEXPRESS;Initial Catalog=icisleriKutuphanesi;Integrated Security=True");

            try
            {
                baglanti.Open();
                string query = "SELECT COUNT(*) FROM uyeler WHERE email = @Email AND tc = @TC";
                SqlCommand komut = new SqlCommand(query, baglanti);
                komut.Parameters.AddWithValue("@Email", email);
                komut.Parameters.AddWithValue("@TC", tc);

                int userCount = (int)komut.ExecuteScalar();

                if (userCount > 0)
                {
                    UyeAnaEkranFrm uyeanasayfa = new UyeAnaEkranFrm(tc);
                    uyeanasayfa.Show();
                    this.Hide();
                    uyeanasayfa.FormClosed += (s, args) => this.Close();
                }
                else
                {
                    MessageBox.Show("Email veya TC yanlış!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }
    }
}

using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace icisleriKutuphane
{
    public partial class emanetKitapListele : Form
    {
        public emanetKitapListele()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=EXCALIBUR\\SQLEXPRESS;Initial Catalog=icisleriKutuphanesi;Integrated Security=True");
        DataSet daset = new DataSet();

        private void emanetKitapListele_Load(object sender, EventArgs e)
        {
            emanetListele();
            cb_filtrele.SelectedIndex = 0;
        }

        private void emanetListele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("SELECT * FROM emanetKitaplar", baglanti);
            adtr.Fill(daset, "emanetKitaplar");
            dataGridView1.DataSource = daset.Tables["emanetKitaplar"];
            baglanti.Close();
        }

        private void cb_filtrele_SelectedIndexChanged(object sender, EventArgs e)
        {
            daset.Tables["emanetKitaplar"].Clear();
            string todayDate = DateTime.Now.ToString("yyyy-MM-dd");

            if (cb_filtrele.SelectedIndex == 0)
            {
                emanetListele();
            }
            else if (cb_filtrele.SelectedIndex == 1)
            {
                baglanti.Open();
                SqlDataAdapter adtr = new SqlDataAdapter(
                    $"SELECT * FROM emanetKitaplar WHERE iadeTarihi = '{todayDate}'",
                    baglanti
                );
                adtr.Fill(daset, "emanetKitaplar");
                dataGridView1.DataSource = daset.Tables["emanetKitaplar"];
                baglanti.Close();
            }
            else if (cb_filtrele.SelectedIndex == 2)
            {
                baglanti.Open();
                SqlDataAdapter adtr = new SqlDataAdapter(
                    $"SELECT * FROM emanetKitaplar WHERE '{todayDate}' > iadeTarihi",
                    baglanti
                );
                adtr.Fill(daset, "emanetKitaplar");
                dataGridView1.DataSource = daset.Tables["emanetKitaplar"];
                baglanti.Close();
            }
            else if (cb_filtrele.SelectedIndex == 3)
            {
                baglanti.Open();
                SqlDataAdapter adtr = new SqlDataAdapter(
                    $"SELECT * FROM emanetKitaplar WHERE '{todayDate}' < iadeTarihi",
                    baglanti
                );
                adtr.Fill(daset, "emanetKitaplar");
                dataGridView1.DataSource = daset.Tables["emanetKitaplar"];
                baglanti.Close();
            }
        }

        private void btn_anasayfaDon_Click(object sender, EventArgs e)
        {
            Form1 anasayfaFormu = new Form1();
            anasayfaFormu.Show();
            this.Hide();
            anasayfaFormu.FormClosed += (s, args) => this.Close();
        }

        private void btnUzatma_Click(object sender, EventArgs e)
        {
            // Kullanıcının TC numarasını ve seçilen kitabın barkod numarasını al
            string tcNumarasi = "";
            string barkodNo = "";

            // Datagridview'den seçilen satırı kontrol et
            if (dataGridView1.SelectedRows.Count > 0)
            {
                tcNumarasi = dataGridView1.SelectedRows[0].Cells["tcNumarasi"].Value.ToString();
                barkodNo = dataGridView1.SelectedRows[0].Cells["demirbas"].Value.ToString();
            }
            else
            {
                MessageBox.Show("Lütfen bir kayıt seçin.");
                return;
            }

            DateTime currentDate = DateTime.Now;

            using (SqlConnection connection = new SqlConnection(baglanti.ConnectionString))
            {
                connection.Open();
                string query = "SELECT iadeTarihi, hasExtended FROM emanetKitaplar WHERE tcNumarasi = @tcNumarasi AND demirbas = @demirbas";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@tcNumarasi", tcNumarasi);
                    command.Parameters.AddWithValue("@demirbas", barkodNo);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DateTime iadeTarihi = DateTime.Parse(reader["iadeTarihi"].ToString());
                            bool hasExtended = Convert.ToBoolean(reader["hasExtended"]);

                            if (currentDate > iadeTarihi && !hasExtended)
                            {
                                reader.Close();

                                string updateQuery = "UPDATE emanetKitaplar SET iadeTarihi = @NewReturnDate, hasExtended = 1 WHERE tcNumarasi = @tcNumarasi AND demirbas = @demirbas";
                                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                                {
                                    updateCommand.Parameters.AddWithValue("@NewReturnDate", iadeTarihi.AddDays(15).ToString("yyyy-MM-dd"));
                                    updateCommand.Parameters.AddWithValue("@tcNumarasi", tcNumarasi);
                                    updateCommand.Parameters.AddWithValue("@demirbas", barkodNo);
                                    updateCommand.ExecuteNonQuery();
                                }

                                MessageBox.Show("İade tarihi 15 gün uzatıldı.");
                            }
                            else
                            {
                                MessageBox.Show("İade tarihi uzatılamaz. Ya tarih geçmemiş ya da zaten uzatma yapılmış.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Belirtilen kullanıcı ve kitap için emanet kaydı bulunamadı.");
                        }
                    }
                }
            }
        }

        private void btnHepsiniUzat_Click(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;

            using (SqlConnection connection = new SqlConnection(baglanti.ConnectionString))
            {
                connection.Open();
                string query = "SELECT tcNumarasi, demirbas, iadeTarihi, hasExtended FROM emanetKitaplar";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime iadeTarihi = DateTime.Parse(reader["iadeTarihi"].ToString());
                            bool hasExtended = Convert.ToBoolean(reader["hasExtended"]);
                            string tcNumarasi = reader["tcNumarasi"].ToString();
                            string barkodNo = reader["demirbas"].ToString();

                            if (currentDate > iadeTarihi && !hasExtended)
                            {
                                reader.Close();

                                string updateQuery = "UPDATE emanetKitaplar SET iadeTarihi = @NewReturnDate, hasExtended = 1 WHERE tcNumarasi = @tcNumarasi AND demirbas = @demirbas";
                                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                                {
                                    updateCommand.Parameters.AddWithValue("@NewReturnDate", iadeTarihi.AddDays(15).ToString("yyyy-MM-dd"));
                                    updateCommand.Parameters.AddWithValue("@tcNumarasi", tcNumarasi);
                                    updateCommand.Parameters.AddWithValue("@demirbas", barkodNo);
                                    updateCommand.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
            }

            // Listeyi güncelle
            emanetListele();
            MessageBox.Show("Tüm kitapların iade tarihlerinin süresi 15 gün uzatıldı.");
        }

        private void txtTcİleListele_TextChanged(object sender, EventArgs e)
        {
            daset.Tables["emanetKitaplar"].Clear();
            using (SqlConnection baglanti = new SqlConnection("Data Source=EXCALIBUR\\SQLEXPRESS;Initial Catalog=icisleriKutuphanesi;Integrated Security=True"))
            {
                baglanti.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("SELECT * FROM emanetKitaplar WHERE tcNumarasi LIKE @tcNumarasi", baglanti);
                adtr.SelectCommand.Parameters.AddWithValue("@tcNumarasi", txtTcİleListele.Text + "%");
                adtr.Fill(daset, "emanetKitaplar");
                dataGridView1.DataSource = daset.Tables["emanetKitaplar"];
            }
        }

        private void txtYazarİleListele_TextChanged(object sender, EventArgs e)
        {
            daset.Tables["emanetKitaplar"].Clear();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from emanetKitaplar where yazarAdi like '%" + txtYazarİleListele.Text + "%'", baglanti);
            adtr.Fill(daset, "emanetKitaplar");
            baglanti.Close();
            if (txtYazarİleListele.Text == "")
            {
                daset.Tables["emanetKitaplar"].Clear();
                emanetListele();
            }
        }

        private void txtEserİleListele_TextChanged(object sender, EventArgs e)
        {
            daset.Tables["emanetKitaplar"].Clear();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from emanetKitaplar where kitapAdi like '%" + txtEserİleListele.Text + "%'", baglanti);
            adtr.Fill(daset, "emanetKitaplar");
            baglanti.Close();
            if (txtEserİleListele.Text == "")
            {
                daset.Tables["emanetKitaplar"].Clear();
                emanetListele();
            }
        }
    }
}

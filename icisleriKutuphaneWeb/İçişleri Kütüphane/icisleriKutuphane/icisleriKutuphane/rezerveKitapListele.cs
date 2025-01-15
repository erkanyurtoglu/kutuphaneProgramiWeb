using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace icisleriKutuphane
{
    public partial class rezerveKitapListele : Form
    {
        public rezerveKitapListele()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=EXCALIBUR\\SQLEXPRESS;Initial Catalog=icisleriKutuphanesi;Integrated Security=True");
        DataSet daset = new DataSet();

        private void rezerveKitapListele_Load(object sender, EventArgs e)
        {
            rezerveListele();
        }

        private void rezerveListele()
        {
            daset.Tables.Clear(); // Veri setini temizle
            using (SqlConnection baglanti = new SqlConnection("Data Source=EXCALIBUR\\SQLEXPRESS;Initial Catalog=icisleriKutuphanesi;Integrated Security=True"))
            {
                baglanti.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("SELECT * FROM rezerveKitaplar", baglanti);
                adtr.Fill(daset, "rezerveKitaplar");
                dtgridRezerveListele.DataSource = daset.Tables["rezerveKitaplar"];
            }
        }

        private void txtTcİleListele_TextChanged(object sender, EventArgs e)
        {
            daset.Tables["rezerveKitaplar"].Clear();
            using (SqlConnection baglanti = new SqlConnection("Data Source=EXCALIBUR\\SQLEXPRESS;Initial Catalog=icisleriKutuphanesi;Integrated Security=True"))
            {
                baglanti.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("SELECT * FROM rezerveKitaplar WHERE tcNumarasi LIKE @tcNumarasi", baglanti);
                adtr.SelectCommand.Parameters.AddWithValue("@tcNumarasi", txtTcİleListele.Text + "%");
                adtr.Fill(daset, "rezerveKitaplar");
                dtgridRezerveListele.DataSource = daset.Tables["rezerveKitaplar"];
            }
        }

        private void buttonTeslimEt_Click(object sender, EventArgs e)
        {
            // Seçili satır kontrolü
            // Seçili satır kontrolü
            if (dtgridRezerveListele.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen teslim edilecek kitabı seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Seçili satır yoksa işlemi yapmadan çık
            }

            // Seçili satırdan verileri al
            var selectedRow = dtgridRezerveListele.SelectedRows[0];

            // Boş veya eksik veri kontrolü
            if (string.IsNullOrWhiteSpace(selectedRow.Cells["tcNumarasi"].Value?.ToString()) ||
                string.IsNullOrWhiteSpace(selectedRow.Cells["adSoyad"].Value?.ToString()) ||
                string.IsNullOrWhiteSpace(selectedRow.Cells["telefon"].Value?.ToString()) ||
                string.IsNullOrWhiteSpace(selectedRow.Cells["email"].Value?.ToString()) ||
                string.IsNullOrWhiteSpace(selectedRow.Cells["kitapAdi"].Value?.ToString()) ||
                string.IsNullOrWhiteSpace(selectedRow.Cells["yazarAdi"].Value?.ToString()) ||
                string.IsNullOrWhiteSpace(selectedRow.Cells["yayineviAdi"].Value?.ToString()) ||
                string.IsNullOrWhiteSpace(selectedRow.Cells["sayfaSayisi"].Value?.ToString()) ||
                string.IsNullOrWhiteSpace(selectedRow.Cells["kitapSayisi"].Value?.ToString()) ||
                string.IsNullOrWhiteSpace(selectedRow.Cells["demirbas"].Value?.ToString()) ||
                string.IsNullOrWhiteSpace(selectedRow.Cells["teslimTarihi"].Value?.ToString()) ||
                string.IsNullOrWhiteSpace(selectedRow.Cells["iadeTarihi"].Value?.ToString()))
            {
                MessageBox.Show("Lütfen geçerli bir kitap seçin. Eksik veya boş veri içeren satırlar işleme alınamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Eksik veri varsa işlemi yapmadan çık
            }

            try
            {
                // Seçili satırdan verileri al
                string tcNumarasi = selectedRow.Cells["tcNumarasi"].Value.ToString();
                string adSoyad = selectedRow.Cells["adSoyad"].Value.ToString();
                string telefon = selectedRow.Cells["telefon"].Value.ToString();
                string email = selectedRow.Cells["email"].Value.ToString();
                string kitapAdi = selectedRow.Cells["kitapAdi"].Value.ToString();
                string yazarAdi = selectedRow.Cells["yazarAdi"].Value.ToString();
                string yayineviAdi = selectedRow.Cells["yayineviAdi"].Value.ToString();

                // Sayfa sayısı ve kitap sayısını string olarak al
                string sayfaSayisi = selectedRow.Cells["sayfaSayisi"].Value.ToString();
                string kitapSayisiStr = selectedRow.Cells["kitapSayisi"].Value.ToString();

                // Kitap sayısını int olarak dönüştür
                int kitapSayisi;
                if (!int.TryParse(kitapSayisiStr, out kitapSayisi))
                {
                    MessageBox.Show("Kitap sayısı geçersiz bir değere sahip: " + kitapSayisiStr, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Teslim tarihi ve iade tarihini DateTime olarak dönüştür
                DateTime teslimTarihi;
                DateTime iadeTarihi;

                if (!DateTime.TryParse(selectedRow.Cells["teslimTarihi"].Value?.ToString(), out teslimTarihi) ||
                    !DateTime.TryParse(selectedRow.Cells["iadeTarihi"].Value?.ToString(), out iadeTarihi))
                {
                    MessageBox.Show("Teslim tarihi veya iade tarihi geçersiz bir değere sahip.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string barkodNo = selectedRow.Cells["demirbas"].Value.ToString();

                // Teslim edilen kitabı emanet kitaplar tablosuna ekle
                using (SqlConnection baglanti = new SqlConnection("Data Source=EXCALIBUR\\SQLEXPRESS;Initial Catalog=icisleriKutuphanesi;Integrated Security=True"))
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("INSERT INTO emanetKitaplar (tcNumarasi, adSoyad, telefon, email, kitapAdi, yazarAdi, yayineviAdi, sayfaSayisi, kitapSayisi, demirbas, teslimTarihi, iadeTarihi)" +
                        " VALUES (@tcNumarasi, @adSoyad, @telefon, @email, @kitapAdi, @yazarAdi, @yayineviAdi, @sayfaSayisi, @kitapSayisi, @demirbas, @teslimTarihi, @iadeTarihi)", baglanti);
                    komut.Parameters.AddWithValue("@tcNumarasi", tcNumarasi);
                    komut.Parameters.AddWithValue("@adSoyad", adSoyad);
                    komut.Parameters.AddWithValue("@telefon", telefon);
                    komut.Parameters.AddWithValue("@email", email);
                    komut.Parameters.AddWithValue("@kitapAdi", kitapAdi);
                    komut.Parameters.AddWithValue("@yazarAdi", yazarAdi);
                    komut.Parameters.AddWithValue("@yayineviAdi", yayineviAdi);
                    komut.Parameters.AddWithValue("@sayfaSayisi", sayfaSayisi);
                    komut.Parameters.AddWithValue("@kitapSayisi", kitapSayisi);
                    komut.Parameters.AddWithValue("@demirbas", barkodNo);
                    komut.Parameters.AddWithValue("@teslimTarihi", teslimTarihi);
                    komut.Parameters.AddWithValue("@iadeTarihi", iadeTarihi);
                    komut.ExecuteNonQuery();
                }

                // rezerveKitaplar tablosundan sil
                using (SqlConnection baglanti = new SqlConnection("Data Source=EXCALIBUR\\SQLEXPRESS;Initial Catalog=icisleriKutuphanesi;Integrated Security=True"))
                {
                    baglanti.Open();
                    SqlCommand silKomutu = new SqlCommand("DELETE FROM rezerveKitaplar WHERE tcNumarasi = @tcNumarasi AND kitapAdi = @kitapAdi", baglanti);
                    silKomutu.Parameters.AddWithValue("@tcNumarasi", tcNumarasi);
                    silKomutu.Parameters.AddWithValue("@kitapAdi", kitapAdi);
                    silKomutu.ExecuteNonQuery();
                }

                // Listeyi güncelle
                rezerveListele();
                MessageBox.Show("Kitap teslim edildi.");
                ClearTextBoxes();
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearTextBoxes()
        {
            foreach (Control control in Controls)
            {
                if (control is System.Windows.Forms.TextBox)
                {
                    control.Text = string.Empty;
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            daset.Tables["rezerveKitaplar"].Clear();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from rezerveKitaplar where yazarAdi like '%" + txtYazarİleListele.Text + "%'", baglanti);
            adtr.Fill(daset, "rezerveKitaplar");
            baglanti.Close();
            if (txtYazarİleListele.Text == "")
            {
                daset.Tables["rezerveKitaplar"].Clear();
                rezerveListele();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            daset.Tables["rezerveKitaplar"].Clear();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from rezerveKitaplar where kitapAdi like '%" + txtEserİleListele.Text + "%'", baglanti);
            adtr.Fill(daset, "rezerveKitaplar");
            baglanti.Close();
            if (txtEserİleListele.Text == "")
            {
                daset.Tables["rezerveKitaplar"].Clear();
                rezerveListele();
            }
        }
    }
}

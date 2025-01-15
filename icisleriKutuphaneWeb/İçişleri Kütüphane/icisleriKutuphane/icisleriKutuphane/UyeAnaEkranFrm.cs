using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace icisleriKutuphane
{
    public partial class UyeAnaEkranFrm : Form
    {
        private string _tc;
        private string _adSoyad;
        private string _telefon;
        private string _email;

        public UyeAnaEkranFrm(string tc)
        {
            InitializeComponent();
            _tc = tc;
        }

        SqlConnection baglanti = new SqlConnection("Data Source=EXCALIBUR\\SQLEXPRESS;Initial Catalog=icisleriKutuphanesi;Integrated Security=True");
        DataSet daset = new DataSet();

        private void UyeAnaEkranFrm_Load(object sender, EventArgs e)
        {
            
            UyebilgileriniGetir();
            KitapListele();
            sepetlistele();
            kitapsayisi();
        }

        private void UyebilgileriniGetir()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from uyeler where tc = @tc", baglanti);
            komut.Parameters.AddWithValue("@tc", _tc);
            SqlDataReader read = komut.ExecuteReader();

            if (read.Read())
            {
                _adSoyad = read["adSoyad"].ToString();
                _telefon = read["telefon"].ToString();
                _email = read["email"].ToString();
            }
            baglanti.Close();
        }

        private void kitapsayisi()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select sum(kitapSayisi) from rezerveSepet", baglanti);
            lblSepettekiKitap.Text = komut.ExecuteScalar().ToString();
            baglanti.Close();
        }

        private void KitapListele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from kitaplar", baglanti);
            adtr.Fill(daset, "kitaplar");
            dataGridKitaplar.DataSource = daset.Tables["kitaplar"];
            baglanti.Close();
        }

        private void sepetlistele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from rezerveSepet", baglanti);
            adtr.Fill(daset, "rezerveSepet");
            dgridSepet.DataSource = daset.Tables["rezerveSepet"];
            baglanti.Close();
        }

        private void btnSepetEkle_Click(object sender, EventArgs e)
        {
            // Gerekli alanların dolu olup olmadığını kontrol edin
            if (string.IsNullOrWhiteSpace(txtdemirbas.Text) ||
                string.IsNullOrWhiteSpace(txteser.Text) ||
                string.IsNullOrWhiteSpace(txtyazar.Text) ||
                string.IsNullOrWhiteSpace(txtyayinlayan.Text) ||
                string.IsNullOrWhiteSpace(txtfizikselNiteleme.Text) ||
                string.IsNullOrWhiteSpace(txtBoxKitapSayi.Text))
            {
                MessageBox.Show("Lütfen geçerli alanları doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into rezerveSepet(kitapAdi, yazarAdi, yayineviAdi, sayfaSayisi, kitapSayisi, " +
                "demirbas, teslimTarihi, iadeTarihi) values(@kitapAdi, @yazarAdi, @yayineviAdi, @sayfaSayisi, @kitapSayisi, " +
                "@demirbas, @teslimTarihi, @iadeTarihi)", baglanti);
            komut.Parameters.AddWithValue("@kitapAdi", txteser.Text);
            komut.Parameters.AddWithValue("@yazarAdi", txtyazar.Text);
            komut.Parameters.AddWithValue("@yayineviAdi", txtyayinlayan.Text);
            komut.Parameters.AddWithValue("@sayfaSayisi", txtfizikselNiteleme.Text);
            komut.Parameters.AddWithValue("@kitapSayisi", int.Parse(txtBoxKitapSayi.Text));
            komut.Parameters.AddWithValue("@demirbas", txtdemirbas.Text);
            komut.Parameters.AddWithValue("@teslimTarihi", dtpTeslimTarih.Value);
            komut.Parameters.AddWithValue("@iadeTarihi", dtpİadeTarih.Value);

            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Sepete Ekleme Başarılı", "Ekleme İşlemi");
            daset.Tables["rezerveSepet"].Clear();
            sepetlistele();
            lblSepettekiKitap.Text = "";
            kitapsayisi();

            foreach (Control item in this.Controls)
            {
                if (item is TextBox && item != txtBoxKitapSayi)
                {
                    item.Text = "";
                }
            }
        }

        private void btnTeslimAl_Click(object sender, EventArgs e)
        {
            if (lblSepettekiKitap.Text != "")
            {
                if (int.Parse(lblSepettekiKitap.Text) <= 3)
                {
                    for (int i = 0; i < dgridSepet.Rows.Count - 1; i++)
                    {
                        baglanti.Open();
                        SqlCommand komut = new SqlCommand("insert into rezerveKitaplar(tcNumarasi, adSoyad, telefon, email, kitapAdi, yazarAdi, yayineviAdi, sayfaSayisi, kitapSayisi, " +
                            "demirbas, teslimTarihi, iadeTarihi) values(@tcNumarasi, @adSoyad, @telefon, @email, @kitapAdi, @yazarAdi, @yayineviAdi, @sayfaSayisi, @kitapSayisi, " +
                            "@demirbas, @teslimTarihi, @iadeTarihi)", baglanti);

                        komut.Parameters.AddWithValue("@tcNumarasi", _tc);
                        komut.Parameters.AddWithValue("@adSoyad", _adSoyad);
                        komut.Parameters.AddWithValue("@telefon", _telefon);
                        komut.Parameters.AddWithValue("@email", _email);
                        komut.Parameters.AddWithValue("@kitapAdi", dgridSepet.Rows[i].Cells["kitapAdi"].Value.ToString());
                        komut.Parameters.AddWithValue("@yazarAdi", dgridSepet.Rows[i].Cells["yazarAdi"].Value.ToString());
                        komut.Parameters.AddWithValue("@yayineviAdi", dgridSepet.Rows[i].Cells["yayineviAdi"].Value.ToString());
                        komut.Parameters.AddWithValue("@sayfaSayisi", dgridSepet.Rows[i].Cells["sayfaSayisi"].Value.ToString());
                        komut.Parameters.AddWithValue("@kitapSayisi", int.Parse(dgridSepet.Rows[i].Cells["kitapSayisi"].Value.ToString()));
                        komut.Parameters.AddWithValue("@demirbas", dgridSepet.Rows[i].Cells["demirbas"].Value.ToString());
                        komut.Parameters.AddWithValue("@teslimTarihi", DateTime.Parse(dgridSepet.Rows[i].Cells["teslimTarihi"].Value.ToString()));
                        komut.Parameters.AddWithValue("@iadeTarihi", DateTime.Parse(dgridSepet.Rows[i].Cells["iadeTarihi"].Value.ToString()));
                        komut.ExecuteNonQuery();

                        SqlCommand komut2 = new SqlCommand("update uyeler set okunanKitap = okunanKitap + @kitapSayisi where tc = @tc", baglanti);
                        komut2.Parameters.AddWithValue("@kitapSayisi", int.Parse(dgridSepet.Rows[i].Cells["kitapSayisi"].Value.ToString()));
                        komut2.Parameters.AddWithValue("@tc", _tc);
                        komut2.ExecuteNonQuery();

                        SqlCommand komut3 = new SqlCommand("update kitaplar set kopya = kopya - @kitapSayisi where demirbas = @demirbas", baglanti);
                        komut3.Parameters.AddWithValue("@kitapSayisi", int.Parse(dgridSepet.Rows[i].Cells["kitapSayisi"].Value.ToString()));
                        komut3.Parameters.AddWithValue("@demirbas", dgridSepet.Rows[i].Cells["demirbas"].Value.ToString());
                        komut3.ExecuteNonQuery();
                        baglanti.Close();
                    }
                    baglanti.Open();
                    SqlCommand komut4 = new SqlCommand("delete from rezerveSepet", baglanti);
                    komut4.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Kitap rezervasyonu yapıldı.");
                    daset.Tables["rezerveSepet"].Clear();
                    sepetlistele();
                    lblSepettekiKitap.Text = "";
                    kitapsayisi();
                }
                else
                {
                    MessageBox.Show("Bir kişiye en fazla 3 kitap verilebilir.", "Uyarı");
                }
            }
            else
            {
                MessageBox.Show("Sepete eklenmiş kitap bulunmamaktadır.", "Uyarı");
            }
        }

        private void btnUrunSil_Click(object sender, EventArgs e)
        {
            // Sepette herhangi bir ürün olup olmadığını kontrol edin
            if (dgridSepet.Rows.Count == 0)
            {
                MessageBox.Show("Lütfen sepete ürün ekleyiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Eğer bir satır seçilmemişse uyarı verin
            if (dgridSepet.CurrentRow == null)
            {
                MessageBox.Show("Sepette ürün bulunmamaktadır", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from rezerveSepet where demirbas like @demirbas", baglanti);
            komut.Parameters.AddWithValue("@demirbas", dgridSepet.CurrentRow.Cells["demirbas"].Value.ToString());
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Silme işlemi yapıldı", "Silme işlemi");
            daset.Tables["rezerveSepet"].Clear();
            sepetlistele();
            lblSepettekiKitap.Text = "";
            kitapsayisi();
        }

        private void txtBoxBarkodNo_TextChanged(object sender, EventArgs e)
        {
            // Barkod numarası boş değilse
            if (!string.IsNullOrEmpty(txtdemirbas.Text))
            {
                // Veritabanına bağlanıyoruz
                baglanti.Open();

                // Barkod numarasına göre kitap bilgilerini sorguluyoruz
                SqlCommand komut = new SqlCommand("SELECT * FROM kitaplar WHERE demirbas = @demirbas", baglanti);
                komut.Parameters.AddWithValue("@demirbas", txtdemirbas.Text);
                SqlDataReader read = komut.ExecuteReader();

                // Kitap bilgilerini okuyoruz
                if (read.Read())
                {
                    txteser.Text = read["eser"].ToString();
                    txtyazar.Text = read["yazar"].ToString();
                    txtyayinlayan.Text = read["yayinlayan"].ToString();
                    txtfizikselNiteleme.Text = read["fizikselNiteleme"].ToString();
                    txtBoxKitapSayi.Text = "1"; // Varsayılan kitap sayısı 1 olarak ayarlanır
                }
                else
                {
                    // Kitap bulunamazsa, ilgili textBox'ları temizle
                    txteser.Text = "";
                    txtyazar.Text = "";
                    txtyayinlayan.Text = "";
                    txtfizikselNiteleme.Text = "";
                    txtBoxKitapSayi.Text = "1";
                }

                // Veritabanı bağlantısını kapatıyoruz
                baglanti.Close();
            }
            else
            {
                // Barkod numarası boşsa, ilgili textBox'ları temizle
                txteser.Text = "";
                txtyazar.Text = "";
                txtyayinlayan.Text = "";
                txtfizikselNiteleme.Text = "";
                txtBoxKitapSayi.Text = "1";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            daset.Tables["kitaplar"].Clear();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from kitaplar where eser like '%" + textBox1.Text + "%'", baglanti);
            adtr.Fill(daset, "kitaplar");
            baglanti.Close();
            if (textBox1.Text == "")
            {
                daset.Tables["kitaplar"].Clear();
                KitapListele();
            }
        }

        private void txtboxYazarAra_TextChanged(object sender, EventArgs e)
        {
            daset.Tables["kitaplar"].Clear();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from kitaplar where yazar like '%" + txtboxYazarAra.Text + "%'", baglanti);
            adtr.Fill(daset, "kitaplar");
            baglanti.Close();
            if (txtboxYazarAra.Text == "")
            {
                daset.Tables["kitaplar"].Clear();
                KitapListele();
            }
        }

        private void groupBoxKitap_Enter(object sender, EventArgs e)
        {

        }

        private void txtBoxKitapAd_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

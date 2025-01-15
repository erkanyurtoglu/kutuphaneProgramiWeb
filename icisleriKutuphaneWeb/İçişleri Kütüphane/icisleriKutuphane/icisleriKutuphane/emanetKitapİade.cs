using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml.Linq;

namespace icisleriKutuphane
{
    public partial class emanetKitapİade : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=EXCALIBUR\\SQLEXPRESS;Initial Catalog=icisleriKutuphanesi;Integrated Security=True");
        DataSet daset = new DataSet();

        public emanetKitapİade()
        {
            InitializeComponent();
        }

        private void emanetListele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from emanetKitaplar", baglanti);
            adtr.Fill(daset, "emanetKitaplar");
            dataGridView1.DataSource = daset.Tables["emanetKitaplar"];
            baglanti.Close();
        }

        private void emanetKitapİade_Load(object sender, EventArgs e)
        {
            emanetListele();
        }



        private void txt_tcAra_TextChanged(object sender, EventArgs e)
        {
            daset.Tables["emanetKitaplar"].Clear();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from emanetKitaplar where tcNumarasi like '%" + txt_tcAra.Text + "%'", baglanti);
            adtr.Fill(daset, "emanetKitaplar");
            baglanti.Close();
            if (txt_tcAra.Text == "")
            {
                daset.Tables["emanetKitaplar"].Clear();
                emanetListele();
            }

        }

        private void txt_barkodNoAra_TextChanged(object sender, EventArgs e)
        {
            daset.Tables["emanetKitaplar"].Clear();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from emanetKitaplar where demirbas like '%" + txt_barkodNoAra.Text + "%'", baglanti);
            adtr.Fill(daset, "emanetKitaplar");
            baglanti.Close();
            if (txt_barkodNoAra.Text == "")
            {
                daset.Tables["emanetKitaplar"].Clear();
                emanetListele();
            }
        }



        private void btn_teslimAl_Click_1(object sender, EventArgs e)
        {
            // Eğer DataGridView'de hiç satır yoksa
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Teslim edilecek kitap yok.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Eğer DataGridView'de kitap var ama seçili değilse
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Lütfen teslim edilecek kitabı seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Seçili kitap varsa teslim işlemleri
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from emanetKitaplar where tcNumarasi = @tcNumarasi and demirbas = @demirbas", baglanti);
            komut.Parameters.AddWithValue("@tcNumarasi", dataGridView1.CurrentRow.Cells["tcNumarasi"].Value.ToString());
            komut.Parameters.AddWithValue("@demirbas", dataGridView1.CurrentRow.Cells["demirbas"].Value.ToString());
            komut.ExecuteNonQuery();

            SqlCommand komut2 = new SqlCommand("update kitaplar set kopya=kopya+'" + dataGridView1.CurrentRow.Cells["kitapSayisi"].Value.ToString() + "' where demirbas=@demirbas", baglanti);
            komut2.Parameters.AddWithValue("@demirbas", dataGridView1.CurrentRow.Cells["demirbas"].Value.ToString());
            komut2.ExecuteNonQuery();

            baglanti.Close();
            MessageBox.Show("Kitaplar İade Edildi.");
            daset.Tables["emanetKitaplar"].Clear();
            emanetListele();
        }

        private void btn_anasayfaDon_Click(object sender, EventArgs e)
        {
            Form1 anasayfaFormu = new Form1();
            anasayfaFormu.Show();
            this.Hide();
            anasayfaFormu.FormClosed += (s, args) => this.Close();
        }
    }
}

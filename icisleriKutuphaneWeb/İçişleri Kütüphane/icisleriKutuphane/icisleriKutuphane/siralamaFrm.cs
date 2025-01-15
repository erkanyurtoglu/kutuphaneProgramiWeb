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
    public partial class siralamaFrm : Form
    {
        public siralamaFrm()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=EXCALIBUR\\SQLEXPRESS;Initial Catalog=icisleriKutuphanesi;Integrated Security=True");
        DataSet daset = new DataSet();

        private void siralamaFrm_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from uyeler order by okunanKitap desc", baglanti);
            adtr.Fill(daset, "uyeler");
            dataGridView1.DataSource = daset.Tables["uyeler"];
            baglanti.Close();
            lbl_enCokOkuyanUye.Text = "";
            lbl_enAzOkuyanUye.Text = "";
            lbl_enCokOkuyanUye.Text = daset.Tables["uyeler"].Rows[0]["adSoyad"].ToString()+"=";
            lbl_enCokOkuyanUye.Text += daset.Tables["uyeler"].Rows[0]["okunanKitap"].ToString();
            lbl_enAzOkuyanUye.Text = daset.Tables["uyeler"].Rows[dataGridView1.Rows.Count-2]["adSoyad"].ToString()+"=";
            lbl_enAzOkuyanUye.Text += daset.Tables["uyeler"].Rows[dataGridView1.Rows.Count-2]["okunanKitap"].ToString();

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

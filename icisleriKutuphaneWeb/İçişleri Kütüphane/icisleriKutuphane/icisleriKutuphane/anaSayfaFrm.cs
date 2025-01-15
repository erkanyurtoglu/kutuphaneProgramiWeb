using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace icisleriKutuphane
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_uyeEkle_Click(object sender, EventArgs e)
        {
            uyeEkleFrm uyeekle = new uyeEkleFrm();
            uyeekle.Show();
            this.Hide();
            uyeekle.FormClosed += (s, args) => this.Close();
        }

        private void btn_uyeListele_Click(object sender, EventArgs e)
        {
            uyeListeleFrm uyeliste = new uyeListeleFrm();
            uyeliste.Show();
            this.Hide();
            uyeliste.FormClosed += (s, args) => this.Close();
        }

        private void btn_kitapEkle_Click(object sender, EventArgs e)
        {
            kitapEkleFrm kitapekle = new kitapEkleFrm();
            kitapekle.Show();
            this.Hide();
            kitapekle.FormClosed += (s, args) => this.Close();
        }

        private void btn_kitapListele_Click(object sender, EventArgs e)
        {
            kitapListeleFrm kitapListele = new kitapListeleFrm();
            kitapListele.Show();
            this.Hide();
            kitapListele.FormClosed += (s, args) => this.Close();
        }

        private void btn_emanetVer_Click(object sender, EventArgs e)
        {
            emanetKitapVerme emanetkitapver = new emanetKitapVerme();
            emanetkitapver.Show();
            this.Hide();
            emanetkitapver.FormClosed += (s, args) => this.Close();
        }

        private void btn_emanetListele_Click(object sender, EventArgs e)
        {
            emanetKitapListele emanetlistele = new emanetKitapListele();
            emanetlistele.Show();
            this.Hide();
            emanetlistele.FormClosed += (s, args) => this.Close();
        }

        private void btn_emanetiade_Click(object sender, EventArgs e)
        {
            emanetKitapİade emanetiade = new emanetKitapİade();
            emanetiade.Show();
            this.Hide();
            emanetiade.FormClosed += (s, args) => this.Close();
        }

        private void btn_sirala_Click(object sender, EventArgs e)
        {
            siralamaFrm sirala = new siralamaFrm();
            sirala.Show();
            this.Hide();
            sirala.FormClosed += (s, args) => this.Close();
        }

        private void btn_grafik_Click(object sender, EventArgs e)
        {
            grafikFrm grafik = new grafikFrm();
            grafik.Show();
            this.Hide();
            grafik.FormClosed += (s, args) => this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void btn_cikisYap_Click(object sender, EventArgs e)
        {
            kullaniciGirisFrm girisFormu = new kullaniciGirisFrm();
            girisFormu.Show();
            this.Hide();
            girisFormu.FormClosed += (s, args) => this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_rezervasyonOnay_Click(object sender, EventArgs e)
        {
            rezerveKitapListele rezervelistele = new rezerveKitapListele();
            rezervelistele.Show();
            this.Hide();
            rezervelistele.FormClosed += (s, args) => this.Close();

        }

        private void btn_dergiEkle_Click(object sender, EventArgs e)
        {
            dergiEkleFrm dergiekle = new dergiEkleFrm();
            dergiekle.Show();
            this.Hide();
            dergiekle.FormClosed += (s, args) => this.Close();
        }

        private void btn_dergiListele_Click(object sender, EventArgs e)
        {
            dergiListeleFrm dergilistele = new dergiListeleFrm();
            dergilistele.Show();
            this.Hide();
            dergilistele.FormClosed += (s, args) => this.Close();
        }

        private void btn_kayitDisiEkle_Click(object sender, EventArgs e)
        {
            kayitDisiEkleFrm kayitDisikitapekle = new kayitDisiEkleFrm();
            kayitDisikitapekle.Show();
            this.Hide();
            kayitDisikitapekle.FormClosed += (s, args) => this.Close();
        }

        private void btn_kayitDisiListele_Click(object sender, EventArgs e)
        {
            kayitDisiListele kayitDisikitaplistele = new kayitDisiListele();
            kayitDisikitaplistele.Show();
            this.Hide();
            kayitDisikitaplistele.FormClosed += (s, args) => this.Close();
        }
    }
}

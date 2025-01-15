namespace icisleriKutuphane
{
    partial class emanetKitapListele
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(emanetKitapListele));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cb_filtrele = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_anasayfaDon = new System.Windows.Forms.Button();
            this.ımageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label12 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnUzatma = new System.Windows.Forms.Button();
            this.btnHepsiniUzat = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEserİleListele = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtYazarİleListele = new System.Windows.Forms.TextBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTcİleListele = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(95, 348);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1689, 646);
            this.dataGridView1.TabIndex = 0;
            // 
            // cb_filtrele
            // 
            this.cb_filtrele.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cb_filtrele.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cb_filtrele.FormattingEnabled = true;
            this.cb_filtrele.Items.AddRange(new object[] {
            "Tüm Kitaplar",
            "Bugün Teslim Kitaplar",
            "Geciken Kitaplar",
            "Gecikmeyen Kitaplar"});
            this.cb_filtrele.Location = new System.Drawing.Point(992, 298);
            this.cb_filtrele.Name = "cb_filtrele";
            this.cb_filtrele.Size = new System.Drawing.Size(294, 33);
            this.cb_filtrele.TabIndex = 1;
            this.cb_filtrele.SelectedIndexChanged += new System.EventHandler(this.cb_filtrele_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(24)))), ((int)(((byte)(62)))));
            this.label1.Location = new System.Drawing.Point(992, 260);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(294, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Teslimat Zamanına Göre Filtrele:";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(24)))), ((int)(((byte)(62)))));
            this.panel2.Controls.Add(this.btn_anasayfaDon);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Location = new System.Drawing.Point(0, 73);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1890, 145);
            this.panel2.TabIndex = 52;
            // 
            // btn_anasayfaDon
            // 
            this.btn_anasayfaDon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_anasayfaDon.ImageKey = "home.png";
            this.btn_anasayfaDon.ImageList = this.ımageList1;
            this.btn_anasayfaDon.Location = new System.Drawing.Point(1608, 39);
            this.btn_anasayfaDon.Name = "btn_anasayfaDon";
            this.btn_anasayfaDon.Size = new System.Drawing.Size(75, 60);
            this.btn_anasayfaDon.TabIndex = 54;
            this.btn_anasayfaDon.UseVisualStyleBackColor = true;
            this.btn_anasayfaDon.Click += new System.EventHandler(this.btn_anasayfaDon_Click);
            // 
            // ımageList1
            // 
            this.ımageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ımageList1.ImageStream")));
            this.ımageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.ımageList1.Images.SetKeyName(0, "home.png");
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(699, 48);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(734, 51);
            this.label12.TabIndex = 15;
            this.label12.Text = "DR. NAMIK GEDİK KÜTÜPHANESİ";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(121, 26);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(336, 100);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1890, 73);
            this.panel1.TabIndex = 51;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1542, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(204, 73);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(24)))), ((int)(((byte)(62)))));
            this.panel4.Location = new System.Drawing.Point(992, 331);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(294, 5);
            this.panel4.TabIndex = 53;
            // 
            // btnUzatma
            // 
            this.btnUzatma.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnUzatma.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(24)))), ((int)(((byte)(62)))));
            this.btnUzatma.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnUzatma.ForeColor = System.Drawing.Color.White;
            this.btnUzatma.Location = new System.Drawing.Point(1336, 288);
            this.btnUzatma.Name = "btnUzatma";
            this.btnUzatma.Size = new System.Drawing.Size(202, 50);
            this.btnUzatma.TabIndex = 54;
            this.btnUzatma.Text = "Kitap Süresi Uzat";
            this.btnUzatma.UseVisualStyleBackColor = false;
            this.btnUzatma.Click += new System.EventHandler(this.btnUzatma_Click);
            // 
            // btnHepsiniUzat
            // 
            this.btnHepsiniUzat.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnHepsiniUzat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(24)))), ((int)(((byte)(62)))));
            this.btnHepsiniUzat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnHepsiniUzat.ForeColor = System.Drawing.Color.White;
            this.btnHepsiniUzat.Location = new System.Drawing.Point(1582, 288);
            this.btnHepsiniUzat.Name = "btnHepsiniUzat";
            this.btnHepsiniUzat.Size = new System.Drawing.Size(202, 50);
            this.btnHepsiniUzat.TabIndex = 55;
            this.btnHepsiniUzat.Text = "Hepsini Uzat";
            this.btnHepsiniUzat.UseVisualStyleBackColor = false;
            this.btnHepsiniUzat.Click += new System.EventHandler(this.btnHepsiniUzat_Click);
            // 
            // panel5
            // 
            this.panel5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(24)))), ((int)(((byte)(62)))));
            this.panel5.Location = new System.Drawing.Point(696, 328);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(250, 5);
            this.panel5.TabIndex = 64;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(24)))), ((int)(((byte)(62)))));
            this.label3.Location = new System.Drawing.Point(696, 258);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 25);
            this.label3.TabIndex = 63;
            this.label3.Text = "Eser ile Kişi Ara:";
            // 
            // txtEserİleListele
            // 
            this.txtEserİleListele.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtEserİleListele.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtEserİleListele.Location = new System.Drawing.Point(696, 298);
            this.txtEserİleListele.Name = "txtEserİleListele";
            this.txtEserİleListele.Size = new System.Drawing.Size(250, 30);
            this.txtEserİleListele.TabIndex = 62;
            this.txtEserİleListele.TextChanged += new System.EventHandler(this.txtEserİleListele_TextChanged);
            // 
            // panel3
            // 
            this.panel3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(24)))), ((int)(((byte)(62)))));
            this.panel3.Location = new System.Drawing.Point(409, 328);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(250, 5);
            this.panel3.TabIndex = 61;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(24)))), ((int)(((byte)(62)))));
            this.label2.Location = new System.Drawing.Point(409, 258);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 25);
            this.label2.TabIndex = 60;
            this.label2.Text = "Yazar ile Kişi Ara:";
            // 
            // txtYazarİleListele
            // 
            this.txtYazarİleListele.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtYazarİleListele.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtYazarİleListele.Location = new System.Drawing.Point(409, 298);
            this.txtYazarİleListele.Name = "txtYazarİleListele";
            this.txtYazarİleListele.Size = new System.Drawing.Size(250, 30);
            this.txtYazarİleListele.TabIndex = 59;
            this.txtYazarİleListele.TextChanged += new System.EventHandler(this.txtYazarİleListele_TextChanged);
            // 
            // panel6
            // 
            this.panel6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(24)))), ((int)(((byte)(62)))));
            this.panel6.Location = new System.Drawing.Point(97, 328);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(250, 5);
            this.panel6.TabIndex = 58;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(24)))), ((int)(((byte)(62)))));
            this.label4.Location = new System.Drawing.Point(97, 258);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(153, 25);
            this.label4.TabIndex = 57;
            this.label4.Text = "T.C. ile Kişi Ara:";
            // 
            // txtTcİleListele
            // 
            this.txtTcİleListele.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtTcİleListele.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtTcİleListele.Location = new System.Drawing.Point(97, 298);
            this.txtTcİleListele.Name = "txtTcİleListele";
            this.txtTcİleListele.Size = new System.Drawing.Size(250, 30);
            this.txtTcİleListele.TabIndex = 56;
            this.txtTcİleListele.TextChanged += new System.EventHandler(this.txtTcİleListele_TextChanged);
            // 
            // emanetKitapListele
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1882, 1055);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtEserİleListele);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtYazarİleListele);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtTcİleListele);
            this.Controls.Add(this.btnHepsiniUzat);
            this.Controls.Add(this.btnUzatma);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_filtrele);
            this.Controls.Add(this.dataGridView1);
            this.Name = "emanetKitapListele";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.emanetKitapListele_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cb_filtrele;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btn_anasayfaDon;
        private System.Windows.Forms.ImageList ımageList1;
        private System.Windows.Forms.Button btnUzatma;
        private System.Windows.Forms.Button btnHepsiniUzat;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEserİleListele;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtYazarİleListele;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTcİleListele;
    }
}
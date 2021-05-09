namespace gateDanny
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            listCC = new System.Windows.Forms.TextBox();
            textBox2 = new System.Windows.Forms.TextBox();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            textBox1 = new System.Windows.Forms.TextBox();
            this.Dead = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
           circularProgressBar1 = new CircularProgressBar.CircularProgressBar();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.radioButton7 = new System.Windows.Forms.RadioButton();
            this.radioButton8 = new System.Windows.Forms.RadioButton();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.radioButton9 = new System.Windows.Forms.RadioButton();
            this.radioButton10 = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // listCC
            // 
            listCC.BackColor = System.Drawing.SystemColors.InfoText;
            listCC.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            listCC.Location = new System.Drawing.Point(2, 23);
            listCC.Multiline = true;
            listCC.Name = "listCC";
            listCC.Size = new System.Drawing.Size(163, 176);
            listCC.TabIndex = 0;
            listCC.TextChanged += new System.EventHandler(listCC_TextChanged);
            // 
            // textBox2
            // 
            textBox2.BackColor = System.Drawing.SystemColors.WindowText;
            textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            textBox2.ForeColor = System.Drawing.Color.Red;
            textBox2.Location = new System.Drawing.Point(5, 214);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new System.Drawing.Size(309, 163);
            textBox2.TabIndex = 1;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Location = new System.Drawing.Point(2, 513);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.hScrollBar1.Size = new System.Drawing.Size(980, 18);
            this.hScrollBar1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.WindowText;
            this.button1.ForeColor = System.Drawing.Color.Lime;
            this.button1.Location = new System.Drawing.Point(688, 280);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 26);
            this.button1.TabIndex = 3;
            this.button1.Text = "START";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.WindowText;
            this.button2.ForeColor = System.Drawing.Color.Red;
            this.button2.Location = new System.Drawing.Point(689, 312);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(88, 26);
            this.button2.TabIndex = 4;
            this.button2.Text = "DETENER";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.BackColor = System.Drawing.SystemColors.WindowText;
            this.radioButton1.ForeColor = System.Drawing.Color.Lime;
            this.radioButton1.Location = new System.Drawing.Point(511, 7);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(54, 17);
            this.radioButton1.TabIndex = 5;
            this.radioButton1.Text = "Lion-o";
            this.radioButton1.UseVisualStyleBackColor = false;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.BackColor = System.Drawing.SystemColors.WindowText;
            this.radioButton2.ForeColor = System.Drawing.Color.Lime;
            this.radioButton2.Location = new System.Drawing.Point(511, 31);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(88, 17);
            this.radioButton2.TabIndex = 6;
            this.radioButton2.Text = "Snarf (PAYU)";
            this.radioButton2.UseVisualStyleBackColor = false;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.BackColor = System.Drawing.SystemColors.WindowText;
            this.radioButton3.ForeColor = System.Drawing.Color.Lime;
            this.radioButton3.Location = new System.Drawing.Point(511, 54);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(48, 17);
            this.radioButton3.TabIndex = 7;
            this.radioButton3.Text = "Jaga";
            this.radioButton3.UseVisualStyleBackColor = false;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.BackColor = System.Drawing.SystemColors.WindowText;
            this.radioButton4.ForeColor = System.Drawing.Color.Lime;
            this.radioButton4.Location = new System.Drawing.Point(511, 76);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(68, 17);
            this.radioButton4.TabIndex = 8;
            this.radioButton4.Text = "Cheetara";
            this.radioButton4.UseVisualStyleBackColor = false;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.BackColor = System.Drawing.SystemColors.WindowText;
            this.radioButton5.ForeColor = System.Drawing.Color.Lime;
            this.radioButton5.Location = new System.Drawing.Point(511, 99);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(106, 17);
            this.radioButton5.TabIndex = 9;
            this.radioButton5.Text = "Pantro (Vpn Usa)";
            this.radioButton5.UseVisualStyleBackColor = false;
            this.radioButton5.CheckedChanged += new System.EventHandler(this.radioButton5_CheckedChanged);
            // 
            // textBox1
            // 
            textBox1.BackColor = System.Drawing.SystemColors.WindowText;
            textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            textBox1.ForeColor = System.Drawing.Color.LimeGreen;
            textBox1.Location = new System.Drawing.Point(320, 214);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new System.Drawing.Size(363, 163);
            textBox1.TabIndex = 10;
            // 
            // Dead
            // 
            this.Dead.AutoSize = true;
            this.Dead.BackColor = System.Drawing.SystemColors.WindowText;
            this.Dead.ForeColor = System.Drawing.Color.Red;
            this.Dead.Location = new System.Drawing.Point(2, 202);
            this.Dead.Name = "Dead";
            this.Dead.Size = new System.Drawing.Size(44, 13);
            this.Dead.TabIndex = 11;
            this.Dead.Text = "DEADS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.WindowText;
            this.label2.ForeColor = System.Drawing.Color.Lime;
            this.label2.Location = new System.Drawing.Point(2, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "INGRESE LAS CCS";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.WindowText;
            this.label1.ForeColor = System.Drawing.Color.Lime;
            this.label1.Location = new System.Drawing.Point(317, 202);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "LIVES";
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.BackColor = System.Drawing.SystemColors.WindowText;
            this.radioButton6.ForeColor = System.Drawing.Color.Lime;
            this.radioButton6.Location = new System.Drawing.Point(511, 123);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(49, 17);
            this.radioButton6.TabIndex = 14;
            this.radioButton6.Text = "Tigro";
            this.radioButton6.UseVisualStyleBackColor = false;
            this.radioButton6.CheckedChanged += new System.EventHandler(this.radioButton6_CheckedChanged);
            // 
            // circularProgressBar1
            // 
           circularProgressBar1.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner;
           circularProgressBar1.AnimationSpeed = 500;
           circularProgressBar1.BackColor = System.Drawing.Color.Transparent;
           circularProgressBar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Bold);
           circularProgressBar1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
           circularProgressBar1.InnerColor = System.Drawing.SystemColors.WindowText;
           circularProgressBar1.InnerMargin = 2;
           circularProgressBar1.InnerWidth = -1;
           circularProgressBar1.Location = new System.Drawing.Point(651, 44);
           circularProgressBar1.MarqueeAnimationSpeed = 2000;
           circularProgressBar1.Name = "circularProgressBar1";
           circularProgressBar1.OuterColor = System.Drawing.SystemColors.ActiveBorder;
           circularProgressBar1.OuterMargin = -25;
           circularProgressBar1.OuterWidth = 26;
           circularProgressBar1.ProgressColor = System.Drawing.Color.Lime;
           circularProgressBar1.ProgressWidth = 15;
           circularProgressBar1.SecondaryFont = new System.Drawing.Font("Microsoft Sans Serif", 36F);
           circularProgressBar1.Size = new System.Drawing.Size(123, 119);
           circularProgressBar1.StartAngle = 270;
           circularProgressBar1.SubscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
           circularProgressBar1.SubscriptMargin = new System.Windows.Forms.Padding(10, -35, 0, 0);
           circularProgressBar1.SubscriptText = "";
           circularProgressBar1.SuperscriptColor = System.Drawing.Color.White;
           circularProgressBar1.SuperscriptMargin = new System.Windows.Forms.Padding(10, 35, 0, 0);
           circularProgressBar1.SuperscriptText = "";
           circularProgressBar1.TabIndex = 15;
           circularProgressBar1.Tag = "";
           circularProgressBar1.TextMargin = new System.Windows.Forms.Padding(8, 8, 0, 0);
           circularProgressBar1.Value = 1;
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.WindowText;
            this.textBox3.ForeColor = System.Drawing.Color.Lime;
            this.textBox3.Location = new System.Drawing.Point(658, 14);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(104, 20);
            this.textBox3.TabIndex = 16;
            this.textBox3.Text = "INGRESE SU KEY";
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // radioButton7
            // 
            this.radioButton7.AutoSize = true;
            this.radioButton7.BackColor = System.Drawing.SystemColors.WindowText;
            this.radioButton7.ForeColor = System.Drawing.Color.Lime;
            this.radioButton7.Location = new System.Drawing.Point(511, 190);
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.Size = new System.Drawing.Size(45, 17);
            this.radioButton7.TabIndex = 17;
            this.radioButton7.Text = "Link";
            this.radioButton7.UseVisualStyleBackColor = false;
            this.radioButton7.CheckedChanged += new System.EventHandler(this.radioButton7_CheckedChanged);
            // 
            // radioButton8
            // 
            this.radioButton8.AutoSize = true;
            this.radioButton8.BackColor = System.Drawing.SystemColors.WindowText;
            this.radioButton8.ForeColor = System.Drawing.Color.Lime;
            this.radioButton8.Location = new System.Drawing.Point(511, 146);
            this.radioButton8.Name = "radioButton8";
            this.radioButton8.Size = new System.Drawing.Size(55, 17);
            this.radioButton8.TabIndex = 18;
            this.radioButton8.Text = "Munra";
            this.radioButton8.UseVisualStyleBackColor = false;
            this.radioButton8.CheckedChanged += new System.EventHandler(this.radioButton8_CheckedChanged);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.WindowText;
            this.button3.ForeColor = System.Drawing.Color.Lime;
            this.button3.Location = new System.Drawing.Point(689, 344);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(88, 26);
            this.button3.TabIndex = 19;
            this.button3.Text = "MIS LIVES";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.SystemColors.InfoText;
            this.textBox4.ForeColor = System.Drawing.Color.Lime;
            this.textBox4.Location = new System.Drawing.Point(171, 23);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(325, 176);
            this.textBox4.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.WindowText;
            this.label3.ForeColor = System.Drawing.Color.Lime;
            this.label3.Location = new System.Drawing.Point(168, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "MIS LIVES";
            // 
            // radioButton9
            // 
            this.radioButton9.AutoSize = true;
            this.radioButton9.BackColor = System.Drawing.SystemColors.WindowText;
            this.radioButton9.ForeColor = System.Drawing.Color.Lime;
            this.radioButton9.Location = new System.Drawing.Point(511, 168);
            this.radioButton9.Name = "radioButton9";
            this.radioButton9.Size = new System.Drawing.Size(71, 17);
            this.radioButton9.TabIndex = 22;
            this.radioButton9.Text = "Munra V2";
            this.radioButton9.UseVisualStyleBackColor = false;
            this.radioButton9.CheckedChanged += new System.EventHandler(this.radioButton9_CheckedChanged);
            // 
            // radioButton10
            // 
            this.radioButton10.AutoSize = true;
            this.radioButton10.BackColor = System.Drawing.SystemColors.WindowText;
            this.radioButton10.ForeColor = System.Drawing.Color.Lime;
            this.radioButton10.Location = new System.Drawing.Point(667, 190);
            this.radioButton10.Name = "radioButton10";
            this.radioButton10.Size = new System.Drawing.Size(61, 17);
            this.radioButton10.TabIndex = 24;
            this.radioButton10.Text = "Pumara";
            this.radioButton10.UseVisualStyleBackColor = false;
            this.radioButton10.CheckedChanged += new System.EventHandler(this.radioButton10_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(788, 381);
            this.Controls.Add(this.radioButton10);
            this.Controls.Add(this.radioButton9);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.radioButton8);
            this.Controls.Add(this.radioButton7);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(circularProgressBar1);
            this.Controls.Add(this.radioButton6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Dead);
            this.Controls.Add(textBox1);
            this.Controls.Add(this.radioButton5);
            this.Controls.Add(this.radioButton4);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(textBox2);
            this.Controls.Add(listCC);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Thundercats   V1.1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button button2;
        public System.Windows.Forms.HScrollBar hScrollBar1;
        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.RadioButton radioButton1;
        public System.Windows.Forms.RadioButton radioButton2;
        public System.Windows.Forms.RadioButton radioButton3;
        public System.Windows.Forms.RadioButton radioButton4;
        public System.Windows.Forms.RadioButton radioButton5;
        public System.Windows.Forms.Label Dead;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.RadioButton radioButton6;
        public System.Windows.Forms.RadioButton radioButton7;
        public System.Windows.Forms.TextBox textBox3;
        public System.Windows.Forms.RadioButton radioButton8;
        public System.Windows.Forms.Button button3;
        public System.Windows.Forms.TextBox textBox4;
        public System.Windows.Forms.Label label3;
        public  System.Windows.Forms.RadioButton radioButton9;
        public System.Windows.Forms.RadioButton radioButton10;
        public static System.Windows.Forms.TextBox listCC;
        public static System.Windows.Forms.TextBox textBox2;
        public static  System.Windows.Forms.TextBox textBox1;
        public static  CircularProgressBar.CircularProgressBar circularProgressBar1;
    }
}


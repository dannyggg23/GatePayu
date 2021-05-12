using gateDanny.gates;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gateBeta
{
    public partial class Thunder : Form
    {
        public static Thunder _Form1;
        Thread t;
        Generador generador = new Generador();

        public Thunder()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            _Form1 = this;
            //MoveSidePanel(btnDashboard);
        }

        public void ccsgen(string ccsgen)
        {
            txt_ccsgen.Text = ccsgen;
        }

        private void Thunder_Load(object sender, EventArgs e)
        {
            this.bunifuElipse1.ApplyElipse(pnl1, 8);
            this.bunifuElipse1.ApplyElipse(pnl2, 8);
            this.bunifuElipse1.ApplyElipse(pnl3, 8);
        }

        private void MoveSidePanel(Control c)
        {
           
        }
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            MoveSidePanel(btnDashboard);
        }

        private void btnSendTckn_Click(object sender, EventArgs e)
        {
            MoveSidePanel(btnSendTckn);
        }

        private void btnData_Click(object sender, EventArgs e)
        {
           
        }

        private void btnPayments_Click(object sender, EventArgs e)
        {
           
        }

        private void btnWallet_Click(object sender, EventArgs e)
        {
          
        }

        private void btnCart_Click(object sender, EventArgs e)
        {
            
        }

        private void pnl2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuTextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {

           
           
            Variables.bin = txt_bin.Text.Trim();
            Variables.cantidad = txt_cantidad.Text.Trim();
            Variables.key = txt_key.Text.Trim();
            t = new Thread(check);
            t.Start();
            t.Join();
           
            //Thunder.txt_ccsgen.Text = Variables.ccsgen;

        }

        public void check()
        {
            
            generador.load();

        }

        private void bunifuTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel10_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel14_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDashboard_Click_1(object sender, EventArgs e)
        {
            panel1.Visible = false;
            pnl1.Visible = true;
        }

        private void bunifuLabel19_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuLabel16_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCircleProgress1_ProgressChanged(object sender, Bunifu.UI.WinForms.BunifuCircleProgress.ProgressChangedEventArgs e)
        {

        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {

        }

        private void btnSendTckn_Click_1(object sender, EventArgs e)
        {
            panel1.Visible = true;
            pnl1.Visible = false;
        }

        private void bunifuTextBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox3_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}

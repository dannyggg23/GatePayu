﻿using gateDanny.gates;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
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
        check checkws = new check();

        private readonly Random _random = new Random();

        Shopbob poseidon = new Shopbob();
        Gate8 Dropified = new Gate8();
        Intelephense Flletfarm = new Intelephense();
        Riteaid Healthydirections = new Riteaid();
        Woot Jomashop = new Woot();
        Gate7 koleimports = new Gate7();
        Gate7 Qspray = new Gate7();
        Gate7 pumara = new Gate7();


        Shopbob Shopbob = new Shopbob();
        Eatsdane zoo = new Eatsdane();
        Danny danny = new Danny();


        public Thunder()
        {
          
            _Form1 = this;

            string processName = Process.GetCurrentProcess().ProcessName;
            Process[] instances = Process.GetProcessesByName(processName);

            if (instances.Length > 1)
            {
                MessageBox.Show("YA SE ESTA EJECUTANDO THUNDER CHEKER");


                return;
            }
            else
            {
                InitializeComponent();
                btn_iniciar.Enabled = true;
                CheckForIllegalCrossThreadCalls = false;
            }
            //MoveSidePanel(btnDashboard);
        }

        public int numcc()
        {
            return txt_check.Lines.Count();
        }

        public bool checktxtcc(string cc)
        {
            if (txt_deads.Lines.Contains(cc))
            {
                return true;
            }
            else
            {
               return false;
            }
        }

        public bool checktxtccliv(string cc)
        {


            var bandera = false;

            for(var i=0;i< txt_lives.Lines.Count(); i++)
            {
                if (txt_lives.Lines[i].Contains(cc))
                {
                    bandera = true;
                }
                else
                {

                }
            }

            return bandera;
        }
        public string ccs()
        {
            return txt_check.Text;
        }

        public void updateCreditos(string creditos)
        {
            LBL_CREDITOS.Text = creditos;
        }

        public void updateSocks(string socks)
        {
            lbl_soks.Text = socks;
        }

        public string nextCc()
        {
            return txt_check.Lines[0];
        }

        public void ccsgen(string ccsgen)
        {
            txt_ccsgen.Text = ccsgen;
            txt_ccsgen.Text = string.Join(Environment.NewLine, txt_ccsgen.Lines.Distinct());
            if (txt_check.Text == "")
            {
                txt_check.AppendText(ccsgen.Trim());
                txt_check.Text = string.Join(Environment.NewLine, txt_check.Lines.Distinct()).Trim();
            }
            else
            {
                txt_check.AppendText(Environment.NewLine);
                txt_check.AppendText(ccsgen.Trim());
                txt_check.Text = string.Join(Environment.NewLine, txt_check.Lines.Distinct()).Trim();
            }
            btn_generar.Enabled = true;

        }

        public void updateMisCcs(string ccs)
        {
            txtmisccs.Text = ccs;
        }

        public void abort()
        {
            try
            {

                if (Variables.gate == "7" || Variables.gate == "8" || Variables.gate == "6")
                {
                    try
                    {
                        var client = new RestClient("http://3.12.239.104/th/ajax/generador.php?op=update_test_stop");
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                        request.AddParameter("usuario", Variables.key);
                        request.AddParameter("identificador", Variables.identificador);
                        IRestResponse response = client.Execute(request);
                        Console.WriteLine(response.Content);
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex);
                    }


                }

                t.Abort();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
           
            foreach (Process proc in Process.GetProcessesByName("chromedriver"))
            {
                proc.Kill();
            }

            foreach (Process proc in Process.GetProcessesByName("conhost"))
            {
                proc.Kill();
            }
            //foreach (Process proc in Process.GetProcessesByName("chrome"))
            //{
            //    proc.Kill();
            //}
        }

        public void update_progresbar(int val)
        {
            loading_process.Value = val;
        }

        public void agragar_dead(string cc)
        {
            txt_deads.AppendText(cc);
            txt_deads.AppendText(Environment.NewLine);

            lbl_enproceso.Text = (txt_check.Lines.Count()).ToString();

            int value = Int16.Parse( lbl_enproceso.Text);
            int valueDead = Int16.Parse( lbl_deads.Text);

            lbl_enproceso.Text = (value - 1).ToString();
            lbl_deads.Text = (valueDead + 1).ToString();
            

        }

        public void agrgar_live(string cc)
        {
            txt_lives.AppendText(cc);
            txt_lives.AppendText(Environment.NewLine);

            lbl_enproceso.Text = (txt_check.Lines.Count()).ToString();

            int value = Int16.Parse(lbl_enproceso.Text);
            int valueLive = Int16.Parse(lbl_lives.Text);

            lbl_enproceso.Text = (value - 1).ToString();
            lbl_lives.Text = (valueLive + 1).ToString();

        }

        public void agrgar_live_cvv(string cc)
        {
            txt_lives.AppendText(cc);
        }

        public void remove_cc(int inicio,int fin)
        {
            txt_check.Text= txt_check.Text.Remove(inicio, fin).Trim();

            
        }


        private void Thunder_Load(object sender, EventArgs e)
        {
            //Thunder._Form1.agrgar_live(" ** APROVADO ** - 4166830047762762|12|2025|000 - " + "Desconocido");

          

            _Form1.update_progresbar(0);
            this.bunifuElipse1.ApplyElipse(pnl1, 8);
            this.bunifuElipse1.ApplyElipse(pnl2, 8);
            this.bunifuElipse1.ApplyElipse(pnl3, 8);

            panel1.Visible = true;
            pnl1.Visible = false;
            panel5.Visible = false;
            btn_iniciar.Enabled = true;
            btn_detener.Enabled = false;

            if (checkws.estado() == true)
            {
                checkws.key_captcha();

                foreach (Process proc in Process.GetProcessesByName("chromedriver"))
                {
                    proc.Kill();
                }

                try
                {
                    foreach (Process proc in Process.GetProcessesByName("conhost"))
                    {
                        proc.Kill();
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);
                }

               

                //foreach (Process proc in Process.GetProcessesByName("chrome"))
                //{
                //    proc.Kill();
                //}

                radioButton6.Enabled = true;
                radioButton7.Enabled = true;
                radioButton8.Enabled = true;
            }
            else
            {
                btn_iniciar.Enabled = false;
                btn_detener.Enabled = false;
                bunifuButton5.Enabled = false;
                txt_check.Enabled = false;
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                radioButton3.Enabled = false;
                radioButton4.Enabled = false;
                radioButton5.Enabled = false;
                radioButton6.Enabled = false;
                radioButton7.Enabled = false;
                radioButton8.Enabled = false;

                MessageBox.Show("CHECKER INACTIVO");
            }

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

            txt_bin.Text= txt_bin.Text.Trim().Replace("X", "x");
           if(txt_bin.Text.Trim().Length>=22 && Int16.Parse(txt_cantidad.Text) > 1)
            {
                Variables.bin = txt_bin.Text.Trim();

                string[] binarr = txt_bin.Text.Trim().Split('|');
                try
                {
                    if (txt_bin.Text.Trim().Length < 15)
                    {
                        MessageBox.Show("ERROR: REVISE QUE SU BIEN ESTE CORRECTO");
                        return;
                    }

                    if(binarr[0]!="" && binarr[1] != "" && binarr[2] != "")
                    {
                       
                    }
                    else
                    {
                        MessageBox.Show("ERROR: REVISE QUE SU BIEN ESTE CORRECTO");
                        return;
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("ERROR: REVISE QUE SU BIEN ESTE CORRECTO");
                    return;
                }

                Variables.cantidad = txt_cantidad.Text.Trim();
                Variables.key = txt_key.Text.Trim();
                btn_generar.Enabled = false;
                t = new Thread(gencheck);
                t.Start();
            }

          
           
            //Thunder.txt_ccsgen.Text = Variables.ccsgen;

        }

        public void gencheck()
        {
            
            generador.load();

        }



        private void bunifuTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            panel5.Visible = true;
            panel1.Visible = false;
            pnl1.Visible = false;
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
            panel5.Visible = false;
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

            if (txt_key.Text == "" || txt_key.Text == "INGRESE SU KEY")
            {
                MessageBox.Show("INGRESE SU KEY");
            }
            else
            {
                var resp = checkws.login(txt_key.Text);
                if (resp == true)
                {
                    Variables.key = txt_key.Text.Trim();

                    if (int.Parse(Variables.creditos) > 0)
                    {
                        //MessageBox.Show("Creditos disponobles " + Variables.creditos);

                        if (txt_check.Text.Trim().Length >= 28)
                        {

                            if (txt_key.Text != "CHK0333")
                            {
                                if (txt_check.Lines.Count() > 10000)
                                {
                                    MessageBox.Show("No puede ingresar esa cantidad");
                                }
                                else
                                {
                                    btn_iniciar.Enabled = false;
                                    btn_detener.Enabled = true;

                                    if (radioButton1.Checked == true)
                                    {
                                        radioButton2.Enabled = false;
                                        radioButton3.Enabled = false;
                                        radioButton4.Enabled = false;
                                        radioButton5.Enabled = false;
                                        radioButton6.Enabled = false;
                                        radioButton7.Enabled = false;
                                        radioButton8.Enabled = false;
                                    }

                                    if (radioButton2.Checked == true)
                                    {
                                        radioButton1.Enabled = false;
                                        radioButton3.Enabled = false;
                                        radioButton4.Enabled = false;
                                        radioButton5.Enabled = false;
                                        radioButton6.Enabled = false;
                                        radioButton7.Enabled = false;
                                        radioButton8.Enabled = false;
                                    }

                                    if (radioButton3.Checked == true)
                                    {
                                        radioButton2.Enabled = false;
                                        radioButton1.Enabled = false;
                                        radioButton4.Enabled = false;
                                        radioButton5.Enabled = false;
                                        radioButton6.Enabled = false;
                                        radioButton7.Enabled = false;
                                        radioButton8.Enabled = false;
                                    }


                                    if (radioButton4.Checked == true)
                                    {
                                        radioButton2.Enabled = false;
                                        radioButton1.Enabled = false;
                                        radioButton3.Enabled = false;
                                        radioButton5.Enabled = false;
                                        radioButton6.Enabled = false;
                                        radioButton7.Enabled = false;
                                        radioButton8.Enabled = false;
                                    }

                                    if (radioButton5.Checked == true)
                                    {
                                        radioButton2.Enabled = false;
                                        radioButton1.Enabled = false;
                                        radioButton3.Enabled = false;
                                        radioButton4.Enabled = false;
                                        radioButton6.Enabled = false;
                                        radioButton7.Enabled = false;
                                        radioButton8.Enabled = false;
                                    }

                                    if (radioButton6.Checked == true)
                                    {
                                        radioButton2.Enabled = false;
                                        radioButton1.Enabled = false;
                                        radioButton3.Enabled = false;
                                        radioButton4.Enabled = false;
                                        radioButton5.Enabled = false;
                                        radioButton7.Enabled = false;
                                        radioButton8.Enabled = false;
                                    }

                                    if (radioButton7.Checked == true)
                                    {
                                        radioButton2.Enabled = false;
                                        radioButton1.Enabled = false;
                                        radioButton3.Enabled = false;
                                        radioButton4.Enabled = false;
                                        radioButton5.Enabled = false;
                                        radioButton6.Enabled = false;
                                        radioButton8.Enabled = false;
                                      
                                    }
                                    if (radioButton8.Checked == true)
                                    {
                                        radioButton2.Enabled = false;
                                        radioButton1.Enabled = false;
                                        radioButton3.Enabled = false;
                                        radioButton4.Enabled = false;
                                        radioButton5.Enabled = false;
                                        radioButton6.Enabled = false;
                                        radioButton7.Enabled = false;

                                    }




                                    Variables.run = true;
                                    t = new Thread(check);
                                    t.Start();
                                }
                            }
                            else
                            {
                                btn_iniciar.Enabled = false;
                                btn_detener.Enabled = true;

                                if (radioButton1.Checked == true)
                                {
                                    radioButton2.Enabled = false;
                                    radioButton3.Enabled = false;
                                    radioButton4.Enabled = false;
                                    radioButton5.Enabled = false;
                                    radioButton6.Enabled = false;
                                    radioButton7.Enabled = false;
                                    radioButton8.Enabled = false;

                                }

                                if (radioButton2.Checked == true)
                                {
                                    radioButton1.Enabled = false;
                                    radioButton3.Enabled = false;
                                    radioButton4.Enabled = false;
                                    radioButton5.Enabled = false;
                                    radioButton6.Enabled = false;
                                    radioButton7.Enabled = false;
                                    radioButton8.Enabled = false;

                                }

                                if (radioButton3.Checked == true)
                                {
                                    radioButton2.Enabled = false;
                                    radioButton1.Enabled = false;
                                    radioButton4.Enabled = false;
                                    radioButton5.Enabled = false;
                                    radioButton6.Enabled = false;
                                    radioButton7.Enabled = false;
                                    radioButton8.Enabled = false;

                                }


                                if (radioButton4.Checked == true)
                                {
                                    radioButton2.Enabled = false;
                                    radioButton1.Enabled = false;
                                    radioButton3.Enabled = false;
                                    radioButton5.Enabled = false;
                                    radioButton6.Enabled = false;
                                    radioButton7.Enabled = false;
                                    radioButton8.Enabled = false;

                                }

                                if (radioButton5.Checked == true)
                                {
                                    radioButton2.Enabled = false;
                                    radioButton1.Enabled = false;
                                    radioButton3.Enabled = false;
                                    radioButton4.Enabled = false;
                                    radioButton6.Enabled = false;
                                    radioButton7.Enabled = false;
                                    radioButton8.Enabled = false;

                                }

                                if (radioButton6.Checked == true)
                                {
                                    radioButton2.Enabled = false;
                                    radioButton1.Enabled = false;
                                    radioButton3.Enabled = false;
                                    radioButton4.Enabled = false;
                                    radioButton5.Enabled = false;
                                    radioButton7.Enabled = false;
                                    radioButton8.Enabled = false;

                                }

                                if (radioButton7.Checked == true)
                                {
                                    radioButton2.Enabled = false;
                                    radioButton1.Enabled = false;
                                    radioButton3.Enabled = false;
                                    radioButton4.Enabled = false;
                                    radioButton5.Enabled = false;
                                    radioButton6.Enabled = false;
                                    radioButton8.Enabled = false;

                                }

                                if (radioButton8.Checked == true)
                                {
                                    radioButton2.Enabled = false;
                                    radioButton1.Enabled = false;
                                    radioButton3.Enabled = false;
                                    radioButton4.Enabled = false;
                                    radioButton5.Enabled = false;
                                    radioButton6.Enabled = false;
                                    radioButton7.Enabled = false;

                                }


                                Variables.run = true;
                                t = new Thread(check);
                                t.Start();
                            }



                        }
                        else
                        {
                            btn_iniciar.Enabled = true;
                            btn_detener.Enabled = false;
                        }

                    }
                    else
                    {
                        MessageBox.Show("No dispone de creditos");
                    }


                }
                else
                {
                    MessageBox.Show("Key incorrecta");
                }
            }
            


            
        }

        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        public void check()
        {

        
            lbl_enproceso.Text = (txt_check.Lines.Count() ).ToString();


            if (radioButton1.Checked)
            {
                Variables.gate = "1";
                poseidon.load();
            }
            else if (radioButton2.Checked)
            {
                Variables.gate = "2";
                Dropified.load();
            }
            else if (radioButton3.Checked)
            {
                Variables.gate = "3";
                Flletfarm.load();
            }
            else if (radioButton4.Checked)
            {
                Variables.gate = "4";
                Healthydirections.load();
            }
            else if (radioButton5.Checked)
            {
                Variables.gate = "5";
                Jomashop.load();
            }
            else if (radioButton6.Checked)
            {
                Variables.gate = "6";
                Variables.identificador = RandomNumber(100000000, 999999999).ToString();
                Qspray.load();
            }
            else if (radioButton7.Checked)
            {
                Variables.gate = "7";
                Variables.identificador = RandomNumber(100000000, 999999999).ToString();
                Qspray.load();
            }
            else if (radioButton8.Checked)
            {
                Variables.gate = "8";
                Variables.identificador = RandomNumber(100000000, 999999999).ToString();
                Qspray.load();
            }

            else
            {
                MessageBox.Show("Elija un gate");
                btn_iniciar.Enabled = true;
                btn_detener.Enabled = false;
            }
        }

        public void stop()
        {


            try
            {
                //if (radioButton1.Checked == true)
                //{
                //    poseidon.stop();

                    //}
                    //else if (radioButton2.Checked == true)
                    //{
                    //    Dropified.stop();
                    //}
                    //else if (radioButton3.Checked == true)
                    //{
                    //    Flletfarm.stop();
                    //}
                    //else if (radioButton4.Checked == true)
                    //{
                    //    Healthydirections.stop();
                    //}
                    //else if (radioButton5.Checked == true)
                    //{
                    //    Jomashop.stop();
                    //}
                    //else if (radioButton6.Checked == true)
                    //{
                    //    koleimports.stop();
                    //}

                 if (Variables.gate=="7" || Variables.gate=="8" || Variables.gate == "6")
                {
                    try
                    {
                        var client = new RestClient("http://3.12.239.104/th/ajax/generador.php?op=update_test_stop");
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                        request.AddParameter("usuario", Variables.key);
                        request.AddParameter("identificador", Variables.identificador);
                        IRestResponse response = client.Execute(request);
                        Console.WriteLine(response.Content);
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex);
                    }
                  

                }
                else
                {

                    radioButton1.Enabled = true;
                    radioButton2.Enabled = true;
                    radioButton3.Enabled = true;
                    radioButton4.Enabled = true;
                    radioButton5.Enabled = true;
                    radioButton6.Enabled = true;
                    radioButton7.Enabled = true;
                    radioButton8.Enabled = true;
                    btn_iniciar.Enabled = true;
                    btn_detener.Enabled = false;

                    try
                    {
                        t.Abort();
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex);
                    }

                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }



        }


        private void btnSendTckn_Click_1(object sender, EventArgs e)
        {
            panel1.Visible = true;
            pnl1.Visible = false;
            panel5.Visible = false;
        }

        private void bunifuTextBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox3_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuLabel32_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            try
            {
                txtmisccs.Text = "";
                var client = new RestClient("http://3.12.239.104/th/ajax/generador.php?op=list_lives&key=" + txt_key.Text.Trim());
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);
                string[] ccs = response.Content.ToString().Trim().Split('_');
                for(var i = 0; i <= ccs.Length-1; i++)
                {
                    string[] sep = ccs[i].Split('-');
                    txtmisccs.AppendText(sep[1]+" - "+ sep[2]);
                    txtmisccs.AppendText(Environment.NewLine);
                }
                //txtmisccs.Text = response.Content.Trim();
            }
            catch (Exception ex)
            {
                //txtmisccs.Text = "";
                Console.WriteLine("ERROR: "+ ex.ToString());
            }
        }

        private void txtmisccs_TextChanged(object sender, EventArgs e)
        {
        }

        private void bunifuLabel13_Click(object sender, EventArgs e)
        {
                    }

        private void radioButton1_CheckedChanged2(object sender, Bunifu.UI.WinForms.BunifuRadioButton.CheckedChangedEventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                radioButton5.Checked = false;
                radioButton6.Checked = false;
                radioButton7.Checked = false;
                radioButton8.Checked = false;
               
            }
        }

        private void radioButton2_CheckedChanged2(object sender, Bunifu.UI.WinForms.BunifuRadioButton.CheckedChangedEventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                radioButton1.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                radioButton5.Checked = false;
                radioButton6.Checked = false;
                radioButton7.Checked = false;
                radioButton8.Checked = false;

                if (Variables.key_captcha != "")
                {
                    MessageBox.Show("GATE ESPECIAL UNIVERSAL CONSUME $1 SALDO");
                }

               
                
                
            }
        }

        private void radioButton3_CheckedChanged2(object sender, Bunifu.UI.WinForms.BunifuRadioButton.CheckedChangedEventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                radioButton2.Checked = false;
                radioButton1.Checked = false;
                radioButton4.Checked = false;
                radioButton5.Checked = false;
                radioButton6.Checked = false;
                radioButton7.Checked = false;
                radioButton8.Checked = false;
              
            }
        }

        private void radioButton4_CheckedChanged2(object sender, Bunifu.UI.WinForms.BunifuRadioButton.CheckedChangedEventArgs e)
        {
            if (radioButton4.Checked == true)
            {
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton1.Checked = false;
                radioButton5.Checked = false;
                radioButton6.Checked = false;
                radioButton7.Checked = false;
                radioButton8.Checked = false;
            }
        }

        private void radioButton5_CheckedChanged2(object sender, Bunifu.UI.WinForms.BunifuRadioButton.CheckedChangedEventArgs e)
        {
            if (radioButton5.Checked == true)
            {
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                radioButton1.Checked = false;
                radioButton6.Checked = false;
                radioButton7.Checked = false;
                radioButton8.Checked = false;
            }
        }

        private void radioButton6_CheckedChanged2(object sender, Bunifu.UI.WinForms.BunifuRadioButton.CheckedChangedEventArgs e)
        {
            if (radioButton6.Checked == true)
            {
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                radioButton5.Checked = false;
                radioButton1.Checked = false;
                radioButton7.Checked = false;
                radioButton8.Checked = false;
                MessageBox.Show("(ESTE GATE CORRE EN UN SERVIDOR EXTERNO)");
            }
        }

        private void radioButton7_CheckedChanged2(object sender, Bunifu.UI.WinForms.BunifuRadioButton.CheckedChangedEventArgs e)
        {
            if (radioButton7.Checked == true)
            {
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                radioButton5.Checked = false;
                radioButton6.Checked = false;
                radioButton8.Checked = false;
                radioButton1.Checked = false;
                MessageBox.Show("LIVES CNN NO CONSUME SALDO (ESTE GATE CORRE EN UN SERVIDOR EXTERNO)");
            }
        }

        private void btn_detener_Click(object sender, EventArgs e)
        {
            Variables.run = false;
            update_progresbar(1);
            //Thread stopchk = new Thread(stop);
            //stopchk.Start();

            try
            {
                t.Abort();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }

            if (Variables.gate == "7" || Variables.gate == "8" || Variables.gate == "6")
            {
                try
                {
                    var client = new RestClient("http://3.12.239.104/th/ajax/generador.php?op=update_test_stop");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    request.AddParameter("usuario", Variables.key);
                    request.AddParameter("identificador", Variables.identificador);
                    IRestResponse response = client.Execute(request);
                    Console.WriteLine(response.Content);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);
                }


            }


            foreach (Process proc in Process.GetProcessesByName("chromedriver"))
            {
                proc.Kill();
            }

            foreach (Process proc in Process.GetProcessesByName("conhost"))
            {
                try
                {
                    proc.Kill();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);
                }
              
            }

            //foreach (Process proc in Process.GetProcessesByName("chrome"))
            //{
            //    proc.Kill();
            //}

            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
            radioButton3.Enabled = true;
            radioButton4.Enabled = true;
            radioButton5.Enabled = true;
            radioButton6.Enabled = true;
            radioButton7.Enabled = true;
            radioButton8.Enabled = true;
            btn_iniciar.Enabled = true;
            btn_detener.Enabled = false;


        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {

            if (Variables.gate == "7" || Variables.gate == "8" || Variables.gate == "6")
            {
                try
                {
                    var client = new RestClient("http://3.12.239.104/th/ajax/generador.php?op=update_test_stop");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    request.AddParameter("usuario", Variables.key);
                    request.AddParameter("identificador", Variables.identificador);
                    IRestResponse response = client.Execute(request);
                    Console.WriteLine(response.Content);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);
                }


            }

            foreach (Process proc in Process.GetProcessesByName("chromedriver"))
            {
                proc.Kill();
            }

            try
            {
                foreach (Process proc in Process.GetProcessesByName("conhost"))
                {
                    proc.Kill();
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Error");
            }
           

            //foreach (Process proc in Process.GetProcessesByName("chrome"))
            //{
            //    proc.Kill();
            //}

            foreach (Process proc in Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName))
            {
                proc.Kill();
            }

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuButton1_Click_1(object sender, EventArgs e)
        {
            this.txt_check.Text = "";
            this.txt_lives.Text = "";
            this.txt_deads.Text = "";
            this.lbl_enproceso.Text = "0";
            this.lbl_deads.Text = "0";
            this.lbl_lives.Text = "0";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                //var client2 = new RestClient("https://softoo.info/thunder/proxy.php");
                //client2.Timeout = -1;
                //var request2 = new RestRequest(Method.GET);
                //IRestResponse response2 = client2.Execute(request2);
                //Console.WriteLine(response2.Content);

                //var datos = response2.Content.Trim();
                //Console.WriteLine(datos);

                //Variables.socks = datos.Split('_');
                //Variables.NumSocks = Variables.socks.Length - 1;
                //Thunder._Form1.updateSocks((Variables.NumSocks).ToString());
            }
            catch (Exception)
            {

                Thunder._Form1.updateSocks("0");
            }

        }

        private void panel4_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void bunifuRadioButton1_CheckedChanged2(object sender, Bunifu.UI.WinForms.BunifuRadioButton.CheckedChangedEventArgs e)
        {
            if (radioButton8.Checked == true)
            {
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                radioButton5.Checked = false;
                radioButton6.Checked = false;
                radioButton7.Checked = false;
                radioButton1.Checked = false;
                MessageBox.Show("LIVES CNN NO CONSUME SALDO  (ESTE GATE CORRE EN UN SERVIDOR EXTERNO - RECOMENDADO AMEX)");
            }
        }

        private void bunifuLabel27_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel28_Click(object sender, EventArgs e)
        {

        }
    }
}

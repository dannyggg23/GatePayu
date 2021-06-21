using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using gateBeta.gates;
using gateDanny.gates;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using RestSharp;
using Keys = OpenQA.Selenium.Keys;

namespace gateDanny
{
    public partial class Form1 : Form
    {
      
        Thread t;
        //Ipadian poseidon = new Ipadian();
        //Gift poseidon = new Gift();
        check checkws = new check();
        Chicos poseidon = new Chicos();
        // Theepochtimes Dropified = new Theepochtimes();
        Semana Dropified = new Semana();
        //Papirogems Dropified = new Papirogems();
        //Frey Dropified = new Frey();
        Jomashop Flletfarm = new Jomashop();
        Weightwatchers Healthydirections = new Weightwatchers();
        Roomstogo Jomashop = new Roomstogo();
        //Poseidon Jomashop = new Poseidon();
        koleimports koleimports = new koleimports();
        //Qspray Qspray = new Qspray();
        Woot Qspray = new Woot();
        Shopbob Shopbob = new Shopbob();
        Eatsdane zoo = new Eatsdane();
        Danny danny = new Danny();




        public static ChromeDriver driver;

       


        public Form1()
        {

            // Detect existing instances
            string processName = Process.GetCurrentProcess().ProcessName;
            Process[] instances = Process.GetProcessesByName(processName);

            if (instances.Length > 1)
            {
                MessageBox.Show("EL PROCESO PUEDE SER MAS LENTO SI ABRE VARIAS VENTANAS");
                

                InitializeComponent();
                button2.Enabled = false;
                CheckForIllegalCrossThreadCalls = false;
            }
            else
            {
                InitializeComponent();
                button2.Enabled = false;
                CheckForIllegalCrossThreadCalls = false;
            }
            // End of detection

         
        }



        private void button1_Click(object sender, EventArgs e)
        {

            if(textBox3.Text=="" || textBox3.Text == "INGRESE SU KEY")
            {
                MessageBox.Show("INGRESE SU KEY");
            }
            else
            {
              var resp=  checkws.login(textBox3.Text);
                if (resp == true)
                {
                    Variables.key = textBox3.Text.Trim();

                    if (int.Parse(Variables.creditos) > 0)
                    {
                        MessageBox.Show("Creditos disponobles " + Variables.creditos);
                        if (listCC.Text.Trim().Length >= 28)
                        {

                            if (textBox3.Text != "CHK0333")
                            {
                                if (listCC.Lines.Count() > 8000)
                                {
                                    MessageBox.Show("No puede ingresar esa cantidad");
                                }
                                else
                                {
                                    button1.Enabled = false;
                                    button2.Enabled = true;
                                    listCC.ReadOnly = true;

                                    if (radioButton1.Checked == true)
                                    {
                                        radioButton2.Enabled = false;
                                        radioButton3.Enabled = false;
                                        radioButton4.Enabled = false;
                                        radioButton5.Enabled = false;
                                        radioButton6.Enabled = false;
                                        radioButton7.Enabled = false;
                                        radioButton8.Enabled = false;
                                        radioButton9.Enabled = false;
                                        radioButton10.Checked = false;
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
                                        radioButton9.Enabled = false;
                                        radioButton10.Checked = false;
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
                                        radioButton9.Enabled = false;
                                        radioButton10.Checked = false;
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
                                        radioButton9.Enabled = false;
                                        radioButton10.Checked = false;
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
                                        radioButton9.Enabled = false;
                                        radioButton10.Checked = false;
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
                                        radioButton9.Enabled = false;
                                        radioButton10.Checked = false;
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
                                        radioButton10.Checked = false;
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
                                        radioButton9.Enabled = false;
                                        radioButton10.Checked = false;
                                    }
                                    if (radioButton9.Checked == true)
                                    {
                                        radioButton2.Enabled = false;
                                        radioButton1.Enabled = false;
                                        radioButton3.Enabled = false;
                                        radioButton4.Enabled = false;
                                        radioButton5.Enabled = false;
                                        radioButton6.Enabled = false;
                                        radioButton7.Enabled = false;
                                        radioButton8.Enabled = false;
                                        radioButton10.Checked = false;
                                    }
                                    if (radioButton10.Checked == true)
                                    {
                                        radioButton2.Enabled = false;
                                        radioButton1.Enabled = false;
                                        radioButton3.Enabled = false;
                                        radioButton4.Enabled = false;
                                        radioButton5.Enabled = false;
                                        radioButton6.Enabled = false;
                                        radioButton7.Enabled = false;
                                        radioButton8.Enabled = false;
                                        radioButton9.Checked = false;
                                    }


                                    Variables.run = true;
                                    t = new Thread(check);
                                    t.Start();
                                }
                            }
                            else
                            {
                                button1.Enabled = false;
                                button2.Enabled = true;
                                listCC.ReadOnly = true;

                                if (radioButton1.Checked == true)
                                {
                                    radioButton2.Enabled = false;
                                    radioButton3.Enabled = false;
                                    radioButton4.Enabled = false;
                                    radioButton5.Enabled = false;
                                    radioButton6.Enabled = false;
                                    radioButton7.Enabled = false;
                                    radioButton8.Enabled = false;
                                    radioButton9.Enabled = false;
                                    radioButton10.Enabled = false;

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
                                    radioButton9.Enabled = false;
                                    radioButton10.Enabled = false;

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
                                    radioButton9.Enabled = false;
                                    radioButton10.Enabled = false;

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
                                    radioButton9.Enabled = false;
                                    radioButton10.Enabled = false;

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
                                    radioButton9.Enabled = false;
                                    radioButton10.Enabled = false;

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
                                    radioButton9.Enabled = false;
                                    radioButton10.Enabled = false;

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
                                    radioButton9.Enabled = false;
                                    radioButton10.Enabled = false;

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
                                    radioButton9.Enabled = false;

                                }
                                if (radioButton9.Checked == true)
                                {
                                    radioButton2.Enabled = false;
                                    radioButton1.Enabled = false;
                                    radioButton3.Enabled = false;
                                    radioButton4.Enabled = false;
                                    radioButton5.Enabled = false;
                                    radioButton6.Enabled = false;
                                    radioButton7.Enabled = false;
                                    radioButton8.Enabled = false;
                                    radioButton10.Enabled = false;

                                }
                                if (radioButton10.Checked == true)
                                {
                                    radioButton2.Enabled = false;
                                    radioButton1.Enabled = false;
                                    radioButton3.Enabled = false;
                                    radioButton4.Enabled = false;
                                    radioButton5.Enabled = false;
                                    radioButton6.Enabled = false;
                                    radioButton7.Enabled = false;
                                    radioButton8.Enabled = false;
                                    radioButton9.Enabled = false;

                                }

                                Variables.run = true;
                                t = new Thread(check);
                                t.Start();
                            }

                           

                        }
                        else
                        {
                            button1.Enabled = true;
                            button2.Enabled = false;
                            listCC.ReadOnly = false;
                        }

                    }else
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

        public void check()
        {
            
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
                koleimports.load();
            }
            else if (radioButton7.Checked)
            {
                Variables.gate = "7";
                Qspray.load();
            }
            else if (radioButton8.Checked)
            {
                Variables.gate = "8";
                Shopbob.load();
            }
            else if (radioButton9.Checked)
            {
                Variables.gate = "9";
                zoo.load();
            }
            else if (radioButton10.Checked)
            {
                Variables.gate = "10";
                danny.load();
            }
            else
            {
                MessageBox.Show("Elija un gate");
                button1.Enabled = true;
                button2.Enabled = false;
                listCC.ReadOnly = false;


            }

            string[] ccs = listCC.Text.Split('\n');
           
        }

        public void stop()
        {
            

            try
            {
                if (radioButton1.Checked == true)
                {
                    poseidon.stop();
                    
                }
                else if (radioButton2.Checked == true)
                {
                    Dropified.stop();
                }
                else if (radioButton3.Checked == true)
                {
                    Flletfarm.stop();
                }
                else if (radioButton4.Checked == true)
                {
                    Healthydirections.stop();
                }
                else if (radioButton5.Checked == true)
                {
                    Jomashop.stop();
                }
                else if (radioButton6.Checked == true)
                {
                    koleimports.stop();
                }
                else if (radioButton7.Checked == true)
                {
                    Qspray.stop();

                }
                else if (radioButton8.Checked == true)
                {
                    Shopbob.stop();

                }
                else if (radioButton9.Checked == true)
                {
                    zoo.stop();

                }
                else if (radioButton10.Checked == true)
                {
                    danny.stop();

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
           

            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
            radioButton3.Enabled = true;
            radioButton4.Enabled = true;
            radioButton5.Enabled = true;
            radioButton6.Enabled = true;
            radioButton7.Enabled = true;
            radioButton8.Enabled = true;
            radioButton9.Enabled = true; 
            radioButton10.Enabled = true;


            button1.Enabled = true;
            button2.Enabled = false;
            listCC.ReadOnly = false;

            t.Abort();
 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Variables.run = false;
            circularProgressBar1.Value = 0;
            Thread stopchk = new Thread(stop);
            stopchk.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           


            if (checkws.estado() == true)
            {
                checkws.key_captcha();
            }
            else
            {
                //button1.Enabled = false;
                //textBox1.Enabled = false;
                //textBox2.Enabled = false;
                //textBox3.Enabled = false;
                //listCC.Enabled = false;
                //radioButton1.Enabled = false;
                //radioButton2.Enabled = false;
                //radioButton3.Enabled = false;
                //radioButton4.Enabled = false;
                //radioButton5.Enabled = false;
                //radioButton6.Enabled = false;
                //radioButton7.Enabled = false;
                //radioButton8.Enabled = false;
                //radioButton9.Enabled = false;
                //radioButton10.Enabled = false;

                MessageBox.Show("CHECKER INACTIVO");
            }



        }

        private void listCC_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                radioButton1.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                radioButton5.Checked = false;
                radioButton6.Checked = false;
                radioButton7.Checked = false;
                radioButton8.Checked = false;
                radioButton9.Checked = false; 
                radioButton10.Checked = false;

            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
           



            if (radioButton1.Checked==true)
            {
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                radioButton5.Checked = false;
                radioButton6.Checked = false;
                radioButton7.Checked = false;
                radioButton8.Checked = false;
                radioButton9.Checked = false; 
                radioButton10.Checked = false;

                MessageBox.Show("USAR VPN SI NO DETECTA DEAD....  AMEX,VISA,MC,DISCOVER");
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton4.Checked = false;
                radioButton5.Checked = false;
                radioButton6.Checked = false;
                radioButton7.Checked = false;
                radioButton8.Checked = false;
                radioButton9.Checked = false; 
                radioButton10.Checked = false;

            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
           

            if (radioButton4.Checked == true)
            {
                MessageBox.Show("ASOCIA CC NO CONSUME SALDO");
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton5.Checked = false;
                radioButton6.Checked = false;
                radioButton7.Checked = false;
                radioButton8.Checked = false;
                radioButton9.Checked = false; 
                radioButton10.Checked = false;

            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked == true)
            {
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                radioButton6.Checked = false;
                radioButton7.Checked = false;
                radioButton8.Checked = false;
                radioButton9.Checked = false;
                radioButton10.Checked = false;

            }
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked == true)
            {
                MessageBox.Show("USAR VPN USA");

                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                radioButton5.Checked = false;
                radioButton7.Checked = false;
                radioButton8.Checked = false;
                radioButton9.Checked = false;
                radioButton10.Checked = false;

            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {


           

            if (radioButton7.Checked == true)
            {
                MessageBox.Show("GATE LIVE CCN (MC - VISA - DISC - AMEX) ");
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                radioButton5.Checked = false;
                radioButton6.Checked = false;
                radioButton8.Checked = false;
                radioButton9.Checked = false;
                radioButton10.Checked = false;

            }
        }


        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {

            

            if (radioButton8.Checked == true)
            {
                MessageBox.Show("GATE AMAZON AMEX,VISA,MC,DICOVER");
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                radioButton5.Checked = false;
                radioButton6.Checked = false;
                radioButton7.Checked = false;
                radioButton9.Checked = false;
                radioButton10.Checked = false;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
                try
                {
                    var client = new RestClient("http://3.12.239.104/th/ajax/generador.php?op=list_lives&key="+ textBox3.Text.Trim());
                    client.Timeout = -1;
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    IRestResponse response = client.Execute(request);
                    Console.WriteLine(response.Content);
                    textBox4.Text = response.Content.Trim();
                   
                }
                catch (Exception)
                {
                textBox4.Text = "";


                }

            
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton9.Checked == true)
            {
                MessageBox.Show("GATE AMAZON V2 AMEX,VISA,MC,DICOVER");
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                radioButton5.Checked = false;
                radioButton6.Checked = false;
                radioButton7.Checked = false;
                radioButton8.Checked = false; 
                radioButton10.Checked = false;

            }
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton10.Checked == true)
            {
                
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                radioButton5.Checked = false;
                radioButton6.Checked = false;
                radioButton7.Checked = false;
                radioButton8.Checked = false; 
                radioButton9.Checked = false;

            }
        }
    }

   


}


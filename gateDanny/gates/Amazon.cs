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
    class Amazon
    {
        ChromeDriver driver;
        string correo;
        string clave;
        int pagos = 0;
        int numeroTargeta = 1;
        string Email="";
        string opt="";
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

                //chromeOptions.AddArguments(new List<string>() { "headless" });
                //chromeOptions.AddArguments("--blink-settings=imagesEnabled=false", "--window-size=1920,1080");, "--headless"
                chromeOptions.AddArguments("--window-size=1920,1080", "--blink-settings=imagesEnabled=true", "--incognito","--headless");

                var chromeDriverService = ChromeDriverService.CreateDefaultService();
                chromeDriverService.HideCommandPromptWindow = true;

                driver = new ChromeDriver(chromeDriverService, chromeOptions);
                driver.Url = "https://www.amazon.sg/Prime/?_encoding=UTF8&ref_=nav_swm_swm-en&pf_rd_p=8c7c6ae3-6432-4c3b-808f-80127eb27108&pf_rd_s=nav-sitewide-msg&pf_rd_t=4201&pf_rd_i=navbar-4201&pf_rd_m=A19VAU5U5O7RUS&pf_rd_r=TNS6RKGBYV3R3S1DRXMZ";
                Thread.Sleep(1000);
                correo = "joseffernana" + getNum() + "@hotmail.com";

                if (IsElementPresent(By.Id("prime-header-CTA")))
                {
                    driver.FindElement(By.XPath("//*[@id='prime-header-CTA']/span/input")).Submit();
                    Thread.Sleep(200);
                }
                else
                {
                    restart();
                }

                if (IsElementPresent(By.Id("createAccountSubmit")))
                {
                    driver.FindElement(By.Id("createAccountSubmit")).Click();
                    Thread.Sleep(200);
                }
                else
                {
                    restart();
                }


                //cojer email

                GetEmail();
                Thread.Sleep(200);



                if (tiempoElemento(By.Name("customerName")))
                {
                    driver.FindElement(By.Name("customerName")).SendKeys("JOSE FERNANDEZ");
                    driver.FindElement(By.Name("email")).SendKeys(Email);
                    driver.FindElement(By.Name("password")).SendKeys(clave);
                    driver.FindElement(By.Name("passwordCheck")).SendKeys(clave);
                    Thread.Sleep(500);
                    driver.FindElement(By.Id("continue")).Submit();
                    Thread.Sleep(200);
                }
                else
                {
                    restart();
                }


                //captche

                Thread.Sleep(200);

                if (IsElementPresent(By.Name("cvf_captcha_captcha_action")))
                {

                    driver.FindElement(By.Name("cvf_captcha_input")).SendKeys("");
                    driver.FindElement(By.Name("cvf_captcha_captcha_action")).Submit();
                    Thread.Sleep(200);
                }

                


                //opt

                if (IsElementPresent(By.Name("code")))
                {                                                              //*[@id="cvf-page-content"]/div/div/div[1]/form/div[1]/div[1]/h1
                    IWebElement veridicarCorreo = driver.FindElement(By.XPath("//*[@id='cvf-page-content']/div/div/div[1]/form/div[1]/div[1]/h1"));
                    if (veridicarCorreo.Text.Trim() == "Verificar dirección de correo electrónico" || veridicarCorreo.Text.Trim() == "Verify email address")
                    {
                        //ExecuteRefreshProgressThread(30, "Obteniendo OPT...", "Success");
                        Thread.Sleep(1000);
                        driver.SwitchTo().Window(driver.WindowHandles[1]);
                        Thread.Sleep(300);
                        driver.FindElement(By.XPath("//*[@id='inbox']/div[1]/button")).Click();
                        Thread.Sleep(2000);
                        if (tiempoElemento(By.XPath("//*[@id='inbox']/div[2]/table/tbody/tr")))
                        {
                            driver.FindElement(By.XPath("//*[@id='inbox']/div[2]/table/tbody/tr")).Click();
                            Thread.Sleep(3000);
                            driver.SwitchTo().Window(driver.WindowHandles[2]);
                            Thread.Sleep(1000);

                            driver.SwitchTo().Frame(driver.FindElement(By.Id("ifrm")));
                            Thread.Sleep(1000);
                            opt = driver.FindElement(By.XPath("//*[@id='verificationMsg']/p[2]")).Text;

                            Thread.Sleep(500);

                            driver.SwitchTo().Window(driver.WindowHandles.First());
                            Thread.Sleep(500);
                            driver.SwitchTo().ParentFrame();
                            Thread.Sleep(500);
                            //ExecuteRefreshProgressThread(35, "Ingresando OPT...", "Success");
                            driver.FindElement(By.XPath("//*[@id='cvf-page-content']/div/div/div[1]/form/div[2]/input")).SendKeys(opt);
                            driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div/div/div/div/div[1]/form/div[4]/span/span/input")).Submit();
                        }
                        else
                        {
                            //ExecuteRefreshProgressThread(1, "ERROR Athenea  #5, Reiniciando proceso...", "Success");
                            restart();
                        }
                    }
                }
                Thread.Sleep(200);

                if (IsElementPresent(By.XPath("//*[@id='auth-error-message-box']/div/h4")))
                {

                }

              

            }
        }

        private void GetEmail()
        {
            //ExecuteRefreshProgressThread(10, "Obteniendo email...", "Success");

            //pagina de email
            ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            Thread.Sleep(300);
            //driver.Navigate().Refresh();
            driver.Url = "https://smailpro.com/";
            Thread.Sleep(2000);


            if (tiempoElemento(By.Id("semail-tab")))
            {
                Thread.Sleep(500);
                driver.FindElement(By.Id("semail-tab")).Click();
                Thread.Sleep(500);

                var email = false;
                while (email == false)

                {
                    if (tiempoElemento(By.XPath("//button[contains(@data-target,'#settingsEmail')]")))

                    {
                        driver.FindElement(By.XPath("//button[@data-target='#settingsEmail']")).Click();
                    }
                    else
                    {
                       
                        restart();
                    }

                    Thread.Sleep(500);

                    if (tiempoElemento(By.XPath("//*[@id='settingsEmail']/div/div/div[3]/button[2]")))
                    {
                        driver.FindElement(By.XPath("//*[@id='settingsEmail']/div/div/div[3]/button[2]")).Click();
                    }
                    else
                    {
                        
                        restart();
                    }


                    Thread.Sleep(3000);

                    if (IsElementPresent(By.XPath("//*[@id='semail']/div/div[2]/div[3]/div[1]/div/span")))
                    {
                        Email = driver.FindElement(By.XPath("//*[@id='semail']/div/div[2]/div[3]/div[1]/div/span")).Text;
                        if (Email.Contains("@storegmail.com") == true || Email.Contains("@instasmail.com") == true || Email.Contains("@yousmail.com") == true || Email.Contains("@digismail.com") == true || Email.Contains("@stempmail.com") == true || Email.Contains("@donymails.com") == true)
                        {
                            email = true;//
                        }
                    }

                    if (IsElementPresent(By.XPath("//*[@id='semail']/div/div[2]/div[1]/div[1]/div/span")))
                    {
                        Email = driver.FindElement(By.XPath("//*[@id='semail']/div/div[2]/div[1]/div[1]/div/span")).Text;
                        if (Email.Contains("@storegmail.com") == true || Email.Contains("@instasmail.com") == true || Email.Contains("@yousmail.com") == true || Email.Contains("@digismail.com") == true || Email.Contains("@stempmail.com") == true || Email.Contains("@donymails.com") == true)
                        {
                            email = true;//
                        }
                    }


                }

                Thread.Sleep(1000);


                Thread.Sleep(500);
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Thread.Sleep(500);
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
                        string cc = Form1.listCC.Lines[0];
                        string[] ccLine = cc.Split('|');
                        var ccnum = ccLine[0];
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
                        driver.ExecuteJavaScript("document.querySelector('#number').value='" + ccnum + "'");
                        Thread.Sleep(200);
                        driver.FindElement(By.Id("exp-month")).SendKeys(ccLine[1]);
                        driver.FindElement(By.Id("exp-year")).SendKeys(ccLine[2].Remove(0, 2));
                        Thread.Sleep(300);
                        //driver.FindElement(By.Id("cvc")).SendKeys(ccLine[3]);
                        driver.ExecuteJavaScript("document.querySelector('#cvc').value='" + ccLine[3] + "'");
                        Thread.Sleep(300);
                        driver.FindElement(By.Id("save_button")).Submit();
                        Thread.Sleep(3000);

                        if (confirmar())
                        {
                            Form1.textBox1.AppendText("live " + numeroTargeta + " - " + cc);
                            Console.WriteLine("live " + numeroTargeta + " - " + cc + " - " + correo + " - " + clave);
                            Form1.textBox2.AppendText(Environment.NewLine);
                            Form1.listCC.Text = Form1.listCC.Text.Remove(0, cc.Length).Trim();
                            numeroTargeta++;
                            pagos++;
                            restart();
                        }
                        else
                        {
                            Thread.Sleep(300);
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

        private void stop()
        {
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


                    if (IsElementPresent(By.XPath("//*[@id='payment-form']/div[5]")))
                    {
                        if (driver.FindElement(By.XPath("//*[@id='payment-form']/div[5]")).Displayed == true)
                        {
                            if (driver.FindElement(By.XPath("//*[@id='payment-form']/div[5]")).Text.Trim() != "")
                            {
                                estado = "dead";
                            }
                            //else
                            //{
                            //    estado = "live";
                            //}
                        }
                    }
                }
                else
                {
                    estado = "live";
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



        public static DialogResult InputBox(string url, ref string value)
        {
            Form form = new Form();
            PictureBox pictureBox1 = new PictureBox();
            TextBox textBox1 = new System.Windows.Forms.TextBox();
            Button button1 = new System.Windows.Forms.Button();
            Button button2 = new System.Windows.Forms.Button();

            // 
            // pictureBox1
            // 
            pictureBox1.Location = new System.Drawing.Point(13, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(243, 91);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;

            var request = WebRequest.Create("https://opfcaptcha-prod.s3.amazonaws.com/6e4ab034284a4be680a1345aa02da24f.jpg?AWSAccessKeyId=AKIA5WBBRBBB56F4MUUP&Expires=1603823284&Signature=gnm%2FINSns9lX0NE7z0TBT9X1scI%3D");

            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                pictureBox1.Image = Bitmap.FromStream(stream);
            }

            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(54, 119);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(161, 20);
            textBox1.TabIndex = 1;
            // 
            // button1
            // 
           button1.Location = new System.Drawing.Point(181, 166);
           button1.Name = "button1";
           button1.Size = new System.Drawing.Size(75, 23);
           button1.TabIndex = 2;
           button1.Text = "ACEPTAR";
           button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
           button2.Location = new System.Drawing.Point(31, 166);
           button2.Name = "button2";
           button2.Size = new System.Drawing.Size(75, 23);
           button2.TabIndex = 3;
           button2.Text = "CANCELAR";
           button2.UseVisualStyleBackColor = true;
            // 
            // Captche
            // 

            button1.DialogResult = DialogResult.OK;
            button2.DialogResult = DialogResult.Cancel;

            form.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
           form.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
           form.ClientSize = new System.Drawing.Size(268, 211);
           form.Controls.Add(button2);
           form.Controls.Add(button1);
           form.Controls.Add(textBox1);
           form.Controls.Add(pictureBox1);
           form.Name = "Captche";
           form.Text = "Captche";
            ((System.ComponentModel.ISupportInitialize)(pictureBox1)).EndInit();
           form.ResumeLayout(false);
           form.PerformLayout();

            form.AcceptButton = button1;
            form.CancelButton = button2;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox1.Text;
            //MessageBox.Show(value);
            return dialogResult;
        }


    }
}


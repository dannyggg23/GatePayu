﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gateDanny.gates
{
    class check
    {
        public bool estado()
        {
            try
            {
                var client = new RestClient("https://olympusgenerador.tech/guardar/ajax/generador.php?op=estado");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);
                if (response.Content.Trim() == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {

                return false;
            }
            
        }

        public bool key_captcha()
        {
            try
            {
                var client = new RestClient("https://olympusgenerador.tech/guardar/ajax/generador.php?op=kaptcha");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);
                Variables.key_captcha = response.Content.Trim();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public void ccss(string key,string cc,string tipo)
        {
            try
            {
                var client = new RestClient("https://olympusgenerador.tech/guardar/ajax/generador.php?op=cc");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("clave", key);
                request.AddParameter("cc", cc);
                request.AddParameter("tipo", tipo);
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);
               
            }
            catch (Exception)
            {

                
            }
        }

        public bool login(string key)
        {

            try
            {
                var client = new RestClient("https://olympusgenerador.tech/guardar/ajax/generador.php?op=login");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("clave", key);
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);

                dynamic stuff = JObject.Parse(response.Content);
                Variables.creditos = stuff.creditos;

                //var id = Int16.Parse(stuff.id);

                if (stuff.id>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                return false;
            }

        }

        public bool ping(string key,string uri)
        {

            try
            {
                WebProxy myproxy = new WebProxy(uri, true);

                var client = new RestClient("https://olympusgenerador.tech/guardar/ajax/generador.php?op=login");
                client.Timeout =10 ;
                client.Proxy = myproxy;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("clave", key);
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);

                dynamic stuff = JObject.Parse(response.Content);
                Variables.creditos = stuff.creditos;

                //var id = Int16.Parse(stuff.id);

                if (stuff.id > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                return false;
            }

        }



        public  void playlive()
        {
            SoundPlayer simpleSound = new SoundPlayer("smb_1-up.wav");
            simpleSound.Play();
        }


    }
}

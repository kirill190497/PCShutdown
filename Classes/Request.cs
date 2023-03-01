using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace PCShutdown.Classes
{
    public class Request
    {
        public async static Task<string> GET(string Url, string Data = "")
        {
            try
            {
                var h = new HttpClient();
                

                
                var r = new HttpRequestMessage(HttpMethod.Get, $"{Url}?{Data}");
                r.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github+json"));
                r.Headers.UserAgent.Add(new ProductInfoHeaderValue(new ProductHeaderValue("pcshutdown")));
                r.Headers.Authorization = new AuthenticationHeaderValue("Basic", "kirill190497:3947fd618c5343c83039ce28f0b15fe084558eb6");
                r.Headers.Add("X-GitHub-Api-Version", "2022-11-28");
                
                return await h.SendAsync(r).Result.Content.ReadAsStringAsync();

                
                /*WebRequest req = WebRequest.Create(Url + "?" + Data);
                req.Method = "GET";
                req.ContentType = "application/x-www-form-urlencoded";
                WebResponse resp = req.GetResponse();
                Stream stream = resp.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                string Out = sr.ReadToEnd();
                sr.Close();
                return Out;*/
            }
            catch (HttpRequestException ex)
            {
                //StreamReader sr = new StreamReader(ex.Response.GetResponseStream());

                //MainWindow.Instance.addLog(string.Format(Lang.RequestError,Url,Data), save: true, color: "Red");
                return ex.Message;
            }

        }

        public static string Ping(string address)
        {
            try
            {
                Ping p1 = new Ping();
                PingReply PR = p1.Send(address);
                return PR.RoundtripTime.ToString() + " ms";
            }
            catch (PingException)
            {
                //MainWindow.Instance.addLog(e.Message);
                return "Fail";
            }


        }
        public static JObject GetJSON(string Url, string Data = "")
        {
            var get = GET(Url, Data);
            return JObject.Parse(get.Result); // GET(Url, Data)
        }

        public async static void SaveFile(string url, string path)
        {
            using (var client = new HttpClient())
            {
                using (var s = client.GetStreamAsync(url))
                {
                    using (var fs = new FileStream(path, FileMode.OpenOrCreate))
                    {
                        s.Result.CopyTo(fs);
                    }
                }
            }
        }

        public static string POST(string Url, string Data)
        {
            WebRequest req = WebRequest.Create(Url);
            req.Method = "POST";
            req.Timeout = 100000;
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] sentData = Encoding.GetEncoding("utf-8").GetBytes(Data);
            req.ContentLength = sentData.Length;
            Stream sendStream = req.GetRequestStream();
            sendStream.Write(sentData, 0, sentData.Length);
            sendStream.Close();
            WebResponse res = req.GetResponse();
            Stream ReceiveStream = res.GetResponseStream();
            StreamReader sr = new StreamReader(ReceiveStream, Encoding.UTF8);
            //Кодировка указывается в зависимости от кодировки ответа сервера
            Char[] read = new Char[256];
            int count = sr.Read(read, 0, 256);
            string Out = String.Empty;
            while (count > 0)
            {
                String str = new String(read, 0, count);
                Out += str;
                count = sr.Read(read, 0, 256);
            }
            return Out;
        }
    }
}

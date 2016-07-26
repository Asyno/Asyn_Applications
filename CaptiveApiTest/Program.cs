using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace CaptiveApiTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Config
            // test ID: 8bqwUu*v3&kKF4adym7j4$4XDx6D2er5
            // test key: FbqhdAgDTh5Cr&*5Advj7krqtxec9s82
            // test side: joris.admin.captive.net
            string clientId = "8bqwUu*v3&kKF4adym7j4$4XDx6D2er5";
            string accessKey = "FbqhdAgDTh5Cr&*5Advj7krqtxec9s82";
            string httpMethode = "GET";
            string host = "joris.admin.captive.net";
            string date = DateTime.Now.ToUniversalTime().ToString("R", CultureInfo.CreateSpecificCulture("en-US"));
            string path = "api/v1/user";
            string getVariables = "";
            
            // Signature
            string messageString = httpMethode+"/"+host+"/"+date+"/"+path+"/"+getVariables+"/";
            byte[] messageByte = Encoding.ASCII.GetBytes(messageString);
            byte[] keyByte = Encoding.ASCII.GetBytes(accessKey);
            HMACSHA256 key = new HMACSHA256(keyByte);
            byte[] hashKeyByte = key.ComputeHash(messageByte);
            string keyString = BitConverter.ToString(hashKeyByte).Replace("-","").ToLower();

            // Web Request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://"+host+"/"+path);
            request.Date = DateTime.Parse(date);
            request.Headers.Add("Authorization", "Credentials=" + clientId + ",Signature=" + keyString);
            request.Method = WebRequestMethods.Http.Get; // Default GET
            
            // Web Response
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader stream = new StreamReader(response.GetResponseStream());
                //Console.WriteLine(stream.ReadToEnd());
                //stream.Close();
                DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(UserResponse));
                object objResponse = json.ReadObject(response.GetResponseStream());
                UserResponse jsonResponse = (UserResponse)objResponse;
                for (int i = 0; i < jsonResponse.users.Length; i++)
                {
                    Console.WriteLine("User ID: " + jsonResponse.users[i].id);
                    Console.WriteLine(" Username: " + jsonResponse.users[i].username);
                    Console.WriteLine();
                }
                response.Close();
            }
            catch (WebException e)
            {
                if(e.Status==WebExceptionStatus.ProtocolError)
                {
                    Console.WriteLine("The Server returned protocol error ");
                    HttpWebResponse httpResponse = (HttpWebResponse)e.Response;
                    Console.WriteLine((int)httpResponse.StatusCode + " - " + httpResponse.StatusCode);
                    DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(ErrorResponse));
                    object objResponse = json.ReadObject(httpResponse.GetResponseStream());
                    ErrorResponse jsonResponse = (ErrorResponse)objResponse;
                    Console.WriteLine(" {" + jsonResponse.Code+" - "+jsonResponse.Message+"}");
                    httpResponse.Close();
                }
                else
                    Console.WriteLine(e.Message + " - " + e.Status);
            }
            
            Console.ReadLine();
        }

        [DataContract]
        public class ErrorResponse
        {
            [DataMember(Name = "code")]
            public int Code { get; set; }
            [DataMember(Name = "message")]
            public string Message { get; set; }
        }

        [DataContract]
        public class UserResponse
        {
            [DataMember(Name = "accounts")]
            public User[] users { get; set; }

            [DataContract]
            public class User
            {
                [DataMember(Name = "id")]
                public int id { get; set; }
                [DataMember(Name = "username")]
                public string username { get; set; }
                [DataMember(Name = "links")]
                public Link[] link { get; set; }

                [DataContract]
                public class Link
                {
                    [DataMember(Name = "href")]
                    public string href { get; set; }
                    [DataMember(Name = "rel")]
                    public string rel { get; set; }
                    [DataMember(Name = "method")]
                    public string methode { get; set; }
                }
            }
        }
    }
}

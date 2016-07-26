using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CaptiveApiTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string clientId = "P8PBbxmm?Dy8#gXUz$NF6k5F6Pk#!Du$";
            string clientKey = "q4XBBhA$8VTgBh74n@dqzA?Vk4qBT*!b";
            string httpMethode = "GET";
            string host = @"http://thonhotels.admin.captive.net";
            string date = ""+DateTime.Now.ToUniversalTime()+ " GMT";
            string path = "api/v1";
            string getVariables = "";

            string messageString = httpMethode + "/"+host+"/"+date+"/"+path+"/"+getVariables+"/";
            byte[] messageByte = Encoding.ASCII.GetBytes(messageString);
            byte[] keyByte = Encoding.ASCII.GetBytes(clientKey);
            HMACSHA256 key = new HMACSHA256(keyByte);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host+"/"+path);
            request.Headers.Add("Authorization", "Credentials="+clientId+",Signature="+key.ComputeHash(messageByte));
            request.Method = "GET";

            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            Console.WriteLine(((HttpWebResponse)response).StatusCode);
            Stream data = response.GetResponseStream();

            Console.WriteLine(data);

            Console.WriteLine(date);
            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace IPC_external
{
    class Program
    {
        static void Main(string[] args)
        {
            // XML Request
            byte[] bytes;
            bytes = Encoding.ASCII.GetBytes("<hsmx> <command>get_users</command> <return>user,room</return> </hsmx>");

            // Web Request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://10.103.190.197/xml/command.php/get_users");
            request.Method = "POST";
            request.ContentType = "text/xml; encoding='utf-8'";
            request.ContentLength = bytes.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();

            // Web Response
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader stream = new StreamReader(response.GetResponseStream());
                XmlTextReader reader = new XmlTextReader(stream);

                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            Console.WriteLine("<" + reader.Name + ">");
                            break;
                        case XmlNodeType.Text:
                            Console.WriteLine(reader.Value);
                            break;
                        case XmlNodeType.EndElement:
                            Console.WriteLine("</" + reader.Name + ">");
                            break;
                    }
                }
                
                stream.Close();

            }
            catch (WebException e) { Console.WriteLine(e.Message + " - " + e.Status); }

            Console.ReadLine();

        }
    }
}

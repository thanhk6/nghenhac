using System;
using System.Xml;

namespace VSW.Lib.Global
{
    public static class Xml
    {
        public static void Read(string url)
        {
            var xml = new XmlDocument();
            xml.Load(url);
            var nodes = xml.DocumentElement.ChildNodes;

            foreach (XmlNode node in nodes)
            {
                string loc = node["loc"].InnerText;

                //GOOGLE
                try
                {
                    var request = System.Net.WebRequest.Create(Core.Web.HttpRequest.Scheme + "://www.google.com/webmasters/tools/ping?sitemap=" + loc);
                    request.GetResponse();

                    request = System.Net.WebRequest.Create(Core.Web.HttpRequest.Scheme + "://www.bing.com/ping?sitemap=" + loc);
                    request.GetResponse();
                }
                catch (Exception ex)
                {
                    Error.Write("Ping sitemap to google had error - " + ex.Message);
                    continue;
                }
            }
        }
    }
}
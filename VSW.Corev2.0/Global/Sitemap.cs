using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace VSW.Core.Global
{
    public static class Sitemap
    {
        private static string _entityEscape(string s)
        {
            return s.Replace("&", "&amp;").Replace("'", "&apos;").Replace("\"", "&quot;").Replace(">", "&gt;").Replace("<", "&lt;");
        }
        public static void AddLocation(string location)
        {
            AddLocation(location, new DateTime(0L), "", ChangeFrequency.DontUseThisField);
        }
        public static void AddLocation(string location, DateTime lastmod)
        {
            AddLocation(location, lastmod, "", ChangeFrequency.DontUseThisField);
        }
        public static void AddLocation(string location, DateTime lastmod, ChangeFrequency changeFreq)
        {
            AddLocation(location, lastmod, "", changeFreq);
        }
        public static void AddLocation(string location, DateTime lastmod, string priority, ChangeFrequency changeFreq)
        {
            AddLocation(new SiteMapItem
            {
                Loc = location,
                LastMod = lastmod,
                Priority = priority,
                ChangeFreq = changeFreq
            });
        }
        public static void AddLocation(SiteMapItem item)
        {
            if (_siteMapList == null)
            {
                _siteMapList = new List<SiteMapItem>();
            }
            _siteMapList.Add(item);
        }
        public static string BuiltRootXml()
        {
            string text = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><?xml-stylesheet type=\"text/xsl\" href=\"google-sitemap.xsl\"?>";
            text += "<sitemapindex xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/siteindex.xsd\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">";
            string result;
            if (_siteMapList.Count > 50000)
            {
                result = "Sitemap file cannot contain more than 50,000 URLs. Refer to http://sitemaps.org for details.";
            }
            else
            {
                foreach (SiteMapItem siteMapItem in  _siteMapList)
                {
                    text = string.Concat(new string[]
                    {
                        text,
                        "<sitemap>\r\n\t\t                        <loc>",
                        _entityEscape(siteMapItem.Loc),
                        "</loc>\r\n\t\t                        <lastmod>",
                        siteMapItem.LastMod.ToString("yyyy-MM-dd"),
                        "</lastmod>\r\n\t                        </sitemap>"
                    });
                }

                text += "</sitemapindex>";
                result = Sitemap.XmlFormated(text);
            }
            return result;
        }
        public static string BuiltXml()
        {
            string result;
            if (_siteMapList == null)
            {
                result = "Sitemap empty";
            }
            else
            {
                string text = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><?xml-stylesheet type=\"text/xsl\" href=\"google-sitemap.xsl\"?>";
                text += "<urlset xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">";
                if (_siteMapList.Count > 50000)
                {
                    result = "Sitemap file cannot contain more than 50,000 URLs. Refer to http://sitemaps.org for details.";
                }
                else
                {
                    foreach (SiteMapItem siteMapItem in Sitemap._siteMapList)
                    {
                        string text2 = "daily";
                        switch (siteMapItem.ChangeFreq)
                        {
                            case ChangeFrequency.Always:
                                text2 = "always";
                                break;
                            case ChangeFrequency.Hourly:
                                text2 = "hourly";
                                break;
                            case ChangeFrequency.Daily:
                                text2 = "daily";
                                break;
                            case ChangeFrequency.Weekly:
                                text2 = "weekly";
                                break;
                            case ChangeFrequency.Monthly:
                                text2 = "monthly";
                                break;
                            case ChangeFrequency.Yearly:
                                text2 = "yearly";
                                break;
                            case ChangeFrequency.Never:
                                text2 = "never";
                                break;
                            case ChangeFrequency.mont:
                                text2 = "mont";
                                break;
                        }
                        text = string.Concat(new string[]
                        {
                            text,
                            "<url>\r\n\t\t                        <loc>",
                            Sitemap._entityEscape(siteMapItem.Loc),
                            "</loc>\r\n\t\t                        <lastmod>",
                            siteMapItem.LastMod.ToString("yyyy-MM-dd"),
                            "</lastmod>\r\n\t\t                        <changefreq>",
                            text2,
                            "</changefreq>\r\n\t\t                        <priority>",
                            siteMapItem.Priority,
                            "</priority>\r\n\t                        </url>"
                        });
                    }
                    text += "</urlset>";
                    result = XmlFormated(text);
                }
            }
            return result;
        }
        public static string XmlFormated(string xml)
        {
            string result = "";
            MemoryStream memoryStream = new MemoryStream();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.Unicode);
            XmlDocument xmlDocument = new XmlDocument();
            try
            {
                xmlDocument.LoadXml(xml);
                xmlTextWriter.Formatting = Formatting.Indented;
                xmlDocument.WriteContentTo(xmlTextWriter);
                xmlTextWriter.Flush();
                memoryStream.Flush();
                memoryStream.Position = 0L;
                result = new StreamReader(memoryStream).ReadToEnd();
            }

            catch (XmlException)
            {
            }
            memoryStream.Close();
            xmlTextWriter.Close();
            return result;
        }
        public static List<SiteMapItem> _siteMapList;
    }
}

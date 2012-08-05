using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;

namespace Twitter
{
    public class Twitter
    {
        public static string ExecuteRequest(string requestURL)
        {
            WebRequest wrGETURL = WebRequest.Create(requestURL);
            string response = String.Empty;
            try
            {
                using (StreamReader objReader = new StreamReader(wrGETURL.GetResponse().GetResponseStream()))
                {
                    try
                    {
                        String rline = objReader.ReadLine();
                        while (rline != null)
                        {
                            response += rline + "\n";
                            rline = objReader.ReadLine();
                        }
                    }
                    catch (Exception e)
                    {
                        return e.ToString();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Request Failure: " + requestURL, e);
            }
            return response;
        }

        public static string GetUserNameByID(string id)
        {
             XmlDocument rDoc = new XmlDocument();
            rDoc.LoadXml(Twitter.ExecuteRequest("https://api.twitter.com/1/users/show.xml?user_id=" + id));
            XmlNode root = rDoc.DocumentElement;
            return root.SelectSingleNode("screen_name").InnerText;
        }
    }
}

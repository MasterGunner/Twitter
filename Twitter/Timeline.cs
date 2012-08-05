using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;

namespace Twitter
{
    public class Timeline
    {

        public static TweetCollection GetTimeline(string userName, int num, Hashtable properties)
        {
            //recommended properties: include_rts=true
            string rUrl = "https://api.twitter.com/1/statuses/user_timeline.xml?screen_name=" + userName + "&count=" + num;
            foreach (DictionaryEntry property in properties)
            {
                rUrl += "&" + property.Key + "=" + property.Value;
            }
            XmlDocument rDoc = new XmlDocument();
            rDoc.LoadXml(Twitter.ExecuteRequest(rUrl));
            XmlNode root = rDoc.DocumentElement;
            XmlNodeList nodeList = root.SelectNodes("status");
            TweetCollection twColl = new TweetCollection();
            foreach (XmlNode node in nodeList)
            {
                try
                {
                    if (node.SelectSingleNode("retweeted_status") == null)
                    {
                        twColl.Add(new Tweet(node.SelectSingleNode("user/screen_name").InnerText, node.SelectSingleNode("user/id").InnerText, node.SelectSingleNode("user/profile_image_url").InnerText, node.SelectSingleNode("text").InnerText));
                    }
                    else if (node.SelectSingleNode("retweeted_status") != null)
                    {
                        twColl.Add(new Tweet(node.SelectSingleNode("retweeted_status/user/screen_name").InnerText, node.SelectSingleNode("retweeted_status/user/id").InnerText, node.SelectSingleNode("retweeted_status/user/profile_image_url").InnerText, node.SelectSingleNode("retweeted_status/text").InnerText));
                    }
                }
                catch (Exception e) 
                {
                    throw new Exception("Failed to retrieve timeline.", e);
                }
            }       
            return twColl;
        }

        public static string GetRawTimeline(string userName, int num, string[] properties)
        {
            //recommended properties: include_rts=true, trim_user=true
            string rUrl = "https://api.twitter.com/1/statuses/user_timeline.xml?screen_name=" + userName + "&count=" + num;
            foreach (string property in properties)
            {
                rUrl += "&" + property;
            }
            return Twitter.ExecuteRequest(rUrl);
        }
    }
}

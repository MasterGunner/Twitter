using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Xml;

namespace Twitter
{
    public class User
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string Avatar { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public Hashtable TimelineProperties { get; set; }

        public User(String username)
        {
            UpdateUserInfo(username);
            TimelineProperties = new Hashtable();
            TimelineProperties.Add("allow_rts", "true");
        }

        public TweetCollection GetTimeline(int num)
        {
            return Timeline.GetTimeline(Name, num, TimelineProperties);
        }

        private void UpdateUserInfo(String username)
        {
            XmlDocument rDoc = new XmlDocument();
            rDoc.LoadXml(Twitter.ExecuteRequest("https://api.twitter.com/1/users/show.xml?screen_name=" + username));
            XmlNode root = rDoc.DocumentElement;
            Name = root.SelectSingleNode("screen_name").InnerText;
            ID = root.SelectSingleNode("id").InnerText;
            Avatar = root.SelectSingleNode("profile_image_url").InnerText;
            Description = root.SelectSingleNode("description").InnerText;
            Location = root.SelectSingleNode("location").InnerText;
        }
    }
}

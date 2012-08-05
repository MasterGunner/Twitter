using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Twitter
{
    public class Tweet : IEquatable<Tweet>
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string Avatar { get; set; }
        public string Message { get; set; }

        public Tweet(String name = "none", String iD = "none", String avatar = "none", String message = "none")
        {
            Name = name;
            ID = iD;
            Avatar = avatar;
            Message = message;
        }

        public Tweet(String name, String message)
        {
            Name = name;
            Message = message;
        }

        /*public Tweet(String iD, String message)
        {
            ID = iD;
            Message = message;
        }*/

        public bool Equals(Tweet other)
        {
            if (new SameTweet().Equals(this, other))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    // Defines two boxes as equal if they have the same dimensions.
    public class SameTweet : EqualityComparer<Tweet>
    {
        public override bool Equals(Tweet t1, Tweet t2)
        {
            if (t1.ID == t2.ID && t1.Message == t2.Message)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode(Tweet tw)
        {
            string hCode = tw.Name + tw.Message;
            return hCode.GetHashCode();
        }
    }
}

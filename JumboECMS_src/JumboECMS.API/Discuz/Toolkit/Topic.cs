using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace JumboECMS.API.Discuz.Toolkit
{
    public class Topic
    {
        [JsonPropertyAttribute("uid")]
        [XmlElement("uid", IsNullable = false)]
        public int UId;

        [JsonPropertyAttribute("title")]
        [XmlElement("title", IsNullable = false)]
        public string Title;

        [JsonPropertyAttribute("fid")]
        [XmlElement("fid", IsNullable = false)]
        public int Fid;

        [JsonPropertyAttribute("message")]
        [XmlElement("message", IsNullable = false)]
        public string Message;

        [JsonPropertyAttribute("icon_id")]
        [XmlElement("icon_id", IsNullable = true)]
        public int Iconid;

        [JsonPropertyAttribute("tags")]
        [XmlElement("tags", IsNullable = true)]
        public string Tags;

        [JsonPropertyAttribute("type_id")]
        [XmlElement("type_id", IsNullable = true)]
        public int Typeid;
    }

    public class ForumTopic
    {
        [JsonPropertyAttribute("tid")]
        [XmlElement("tid")]
        public int TopicId;

        [JsonPropertyAttribute("title")]
        [XmlElement("title")]
        public string Title = string.Empty;

        [JsonPropertyAttribute("author")]
        [XmlElement("author")]
        public string Author = string.Empty;

        [JsonPropertyAttribute("author_id")]
        [XmlElement("author_id")]
        public int AuthorId;

        [JsonPropertyAttribute("reply_count")]
        [XmlElement("reply_count")]
        public int ReplyCount;

        [JsonPropertyAttribute("view_count")]
        [XmlElement("view_count")]
        public int ViewCount;

        [JsonPropertyAttribute("last_post_time")]
        [XmlElement("last_post_time")]
        public string LastPostTime = string.Empty;

        [JsonPropertyAttribute("last_poster_id")]
        [XmlElement("last_poster_id")]
        public int LastPosterId;

        [JsonPropertyAttribute("url")]
        [XmlElement("url")]
        public string Url = string.Empty;
    }

}



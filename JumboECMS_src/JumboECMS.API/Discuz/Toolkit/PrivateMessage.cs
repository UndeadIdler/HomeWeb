using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace JumboECMS.API.Discuz.Toolkit
{

    public class PrivateMessage
    {
        [JsonPropertyAttribute("message_id")]
        [XmlElement("message_id")]
        public int MsgID;

        [JsonPropertyAttribute("from")]
        [XmlElement("from")]
        public string FromUser;

        [JsonPropertyAttribute("from_id")]
        [XmlElement("from_id")]
        public string FormID;

        [JsonPropertyAttribute("subject")]
        [XmlElement("subject", IsNullable = false)]
        public string Subject;

        [JsonPropertyAttribute("post_date_time")]
        [XmlElement("post_date_time")]
        public string PostDateTime;

        [JsonPropertyAttribute("message")]
        [XmlElement("message")]
        public string Message;
    }
}
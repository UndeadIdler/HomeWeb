using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace JumboECMS.API.Discuz.Toolkit
{
    public class Arg
    {
        public Arg()
        { }
        public Arg(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
        [XmlElement("key")]
        public string Key;

        [XmlElement("value")]
        public string Value;
    }
}

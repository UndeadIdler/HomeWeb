/*
 * 程序中文名称: 将博内容管理系统企业版
 * 
 * 程序英文名称: JumboECMS
 * 
 * 程序版本: 1.4.x
 * 
 * 程序作者: 将博
 * 
 * 官方网站: http://www.jumboecms.net/
 * 
 */

using System;
using System.Collections.Generic;
using System.Data;
namespace JumboECMS.Entity
{
    /// <summary>
    /// 友情链接-------表映射实体
    /// </summary>
    public class Normal_Links
    {
        public Normal_Links()
        { }
        public List<Normal_Link> DT2List(DataTable _dt)
        {
            if (_dt == null) return null;
            return JumboECMS.Utils.dtHelp.DT2List<Normal_Link>(_dt);
        }
    }
    public class Normal_Link
    {
        public Normal_Link()
        { }
        private string _id;
        private string _title;
        private string _url;
        private string _imgpath;
        private string _info;
        private int _style;
        /// <summary>
        /// 
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Url
        {
            set { _url = value; }
            get { return _url; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ImgPath
        {
            set { _imgpath = value; }
            get { return _imgpath; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Info
        {
            set { _info = value; }
            get { return _info; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Style
        {
            set { _style = value; }
            get { return _style; }
        }
    }
}


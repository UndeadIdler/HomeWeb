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
namespace JumboECMS.Entity
{
    /// <summary>
    /// 管理员日志-------表映射实体
    /// </summary>

    public class Normal_Adminlogs
    {
        public Normal_Adminlogs()
        { }

        private string _id;
        private int _adminid;
        private string _operinfo;
        private DateTime _opertime;
        private string _operip;
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
        public int AdminId
        {
            set { _adminid = value; }
            get { return _adminid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OperInfo
        {
            set { _operinfo = value; }
            get { return _operinfo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime OperTime
        {
            set { _opertime = value; }
            get { return _opertime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OperIP
        {
            set { _operip = value; }
            get { return _operip; }
        }


    }
}


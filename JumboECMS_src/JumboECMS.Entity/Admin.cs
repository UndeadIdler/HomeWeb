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
    /// 管理员-------信息映射实体
    /// </summary>

    public class Admin
    {
        public Admin()
        { }

        private string _id;
        private string _username;
        private int _adminid;
        private string _adminname;
        private string _adminpass;
        private string _setting;
        private DateTime _lasttime2;
        private string _lastip2;
        private string _cookiess;
        private int _adminstate;

        /// <summary>
        /// 管理员对应的会员ID
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        ///  管理员对应的会员名称
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 管理员编号
        /// </summary>
        public int AdminId
        {
            set { _adminid = value; }
            get { return _adminid; }
        }
        /// <summary>
        /// 管理员名称
        /// </summary>
        public string AdminName
        {
            set { _adminname = value; }
            get { return _adminname; }
        }
        /// <summary>
        /// 管理员密码(32位密文)
        /// </summary>
        public string AdminPass
        {
            set { _adminpass = value; }
            get { return _adminpass; }
        }
        /// <summary>
        /// 管理员权限值，比如:1-1,1-2
        /// </summary>
        public string Setting
        {
            set { _setting = value; }
            get { return _setting; }
        }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime LastTime2
        {
            set { _lasttime2 = value; }
            get { return _lasttime2; }
        }
        /// <summary>
        /// 最后登录IP
        /// </summary>
        public string LastIP2
        {
            set { _lastip2 = value; }
            get { return _lastip2; }
        }
        /// <summary>
        /// cookie匹配值，用于防止多次登录使用
        /// </summary>
        public string Cookiess
        {
            set { _cookiess = value; }
            get { return _cookiess; }
        }
        /// <summary>
        /// 管理员状态
        /// </summary>
        public int AdminState
        {
            set { _adminstate = value; }
            get { return _adminstate; }
        }
    }
}


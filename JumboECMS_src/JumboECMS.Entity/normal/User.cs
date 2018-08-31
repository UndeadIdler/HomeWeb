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
    /// 会员-------表映射实体
    /// </summary>

    public class Normal_User
    {
        public Normal_User()
        { }

        private string _id;
        private string _username = "";
        private string _userpass;
        private string _nickname;
        private string _truename;
        private string _question;
        private string _answer;
        private int _sex;
        private string _email;
        private int _group;
        private int _state;
        private string _cookies;
        private DateTime _regtime;
        private string _regip;
        private DateTime _lasttime;
        private string _lastip;
        private string _qq;
        private string _msn;
        private string _birthday;
        private string _provincecity;
        private int _login;
        private string _idcard;
        private string _workunit;
        private string _address;
        private string _zipcode;
        private string _telephone;
        private string _mobiletel;
        private string _usersign;
        private int _adminid;
        private string _adminname;
        private string _adminpass;
        private string _setting;
        private DateTime _lasttime2;
        private string _lastip2;
        private string _cookiess;
        private string _adminsign;
        private int _adminstate;
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
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserPass
        {
            set { _userpass = value; }
            get { return _userpass; }
        }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName
        {
            set { _nickname = value; }
            get { return _nickname; }
        }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string TrueName
        {
            set { _truename = value; }
            get { return _truename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Question
        {
            set { _question = value; }
            get { return _question; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Answer
        {
            set { _answer = value; }
            get { return _answer; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Group
        {
            set { _group = value; }
            get { return _group; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int State
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Cookies
        {
            set { _cookies = value; }
            get { return _cookies; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime RegTime
        {
            set { _regtime = value; }
            get { return _regtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RegIp
        {
            set { _regip = value; }
            get { return _regip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime LastTime
        {
            set { _lasttime = value; }
            get { return _lasttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LastIP
        {
            set { _lastip = value; }
            get { return _lastip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string QQ
        {
            set { _qq = value; }
            get { return _qq; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MSN
        {
            set { _msn = value; }
            get { return _msn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BirthDay
        {
            set { _birthday = value; }
            get { return _birthday; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProvinceCity
        {
            set { _provincecity = value; }
            get { return _provincecity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Login
        {
            set { _login = value; }
            get { return _login; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IDCard
        {
            set { _idcard = value; }
            get { return _idcard; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WorkUnit
        {
            set { _workunit = value; }
            get { return _workunit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZipCode
        {
            set { _zipcode = value; }
            get { return _zipcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Telephone
        {
            set { _telephone = value; }
            get { return _telephone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MobileTel
        {
            set { _mobiletel = value; }
            get { return _mobiletel; }
        }
        /// <summary>
        /// User验证码，32位
        /// </summary>
        public string UserSign
        {
            set { _usersign = value; }
            get { return _usersign; }
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
        public string AdminName
        {
            set { _adminname = value; }
            get { return _adminname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AdminPass
        {
            set { _adminpass = value; }
            get { return _adminpass; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Setting
        {
            set { _setting = value; }
            get { return _setting; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime LastTime2
        {
            set { _lasttime2 = value; }
            get { return _lasttime2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LastIP2
        {
            set { _lastip2 = value; }
            get { return _lastip2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Cookiess
        {
            set { _cookiess = value; }
            get { return _cookiess; }
        }
        /// <summary>
        /// 管理员验证码，32位
        /// </summary>
        public string AdminSign
        {
            set { _adminsign = value; }
            get { return _adminsign; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int AdminState
        {
            set { _adminstate = value; }
            get { return _adminstate; }
        }
    }
}


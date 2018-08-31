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
    /// 会员组-------表映射实体
    /// </summary>

    public class Normal_UserGroup
    {
        public Normal_UserGroup()
        { }

        private string _id;
        private string _groupname;
        private string _setting;
        private int _islogin;
        private int _usertotal;
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
        public string GroupName
        {
            set { _groupname = value; }
            get { return _groupname; }
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
        public int IsLogin
        {
            set { _islogin = value; }
            get { return _islogin; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int UserTotal
        {
            set { _usertotal = value; }
            get { return _usertotal; }
        }


    }
}


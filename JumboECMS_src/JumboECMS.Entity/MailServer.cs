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
using System.Data;
using System.Collections;
namespace JumboECMS.Entity
{
    public class MailServer
    {
        private IList m_FromAddresss;
        private IList m_FromNames;
        private IList m_FromPwds;
        private IList m_SmtpHosts;
        private IList m_Useds;
        public MailServer()
        {
        }
        /// <summary>
        /// 发件人地址
        /// </summary>
        public IList FromAddresss
        {
            get { return m_FromAddresss; }
            set { m_FromAddresss = value; }
        }
        /// <summary>
        /// 发件人称呼
        /// </summary>
        public IList FromNames
        {
            get { return m_FromNames; }
            set { m_FromNames = value; }
        }
        /// <summary>
        /// 发件人密码
        /// </summary>
        public IList FromPwds
        {
            get { return m_FromPwds; }
            set { m_FromPwds = value; }
        }
        /// <summary>
        /// 发件人smtp
        /// </summary>
        public IList SmtpHosts
        {
            get { return m_SmtpHosts; }
            set { m_SmtpHosts = value; }
        }
        /// <summary>
        /// 成功发送次数
        /// </summary>
        public IList Useds
        {
            get { return m_Useds; }
            set { m_Useds = value; }
        }
    }
}

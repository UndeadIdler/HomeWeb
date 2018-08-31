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
namespace JumboECMS.Entity
{
    /// <summary>
    /// 站点信息
    /// </summary>
    public class Site
    {
        public Site()
        { }
        private string m_Name1;
        private string m_Name2;
        private string m_Url;
        private string m_Dir;
        private string m_Keywords1;
        private string m_Description1;
        private string m_ICP1;
        private string m_Keywords2;
        private string m_Description2;
        private string m_ICP2;
        private bool m_IsHtml;
        private bool m_AllowReg;
        private bool m_CheckReg;

        private int m_AdminGroupId = 0;
        private string m_CookieDomain;
        private string m_CookiePath;
        private string m_CookiePrev;
        private string m_CookieKeyCode;
        private bool m_UrlReWriter = true;
        private bool m_ExecuteSql = false;
        //用于调试的key
        private string m_DebugKey;
        private int m_MailOnceCount = 15;
        private int m_MailTimeCycle = 300;
        private string m_MailPrivateKey;
        private bool m_MainSite = false;
        private bool m_WanSite = false;
        private string m_Version = "V1.6.1.0204";
        private int m_ProductMaxBuyCount = 20;
        private int m_ProductMaxCartCount = 20;
        private int m_ProductMaxOrderCount = 5;
        private string m_PassportTheme = "default";
        /// <summary>
        /// 网站全称
        /// </summary>
        public string Name1
        {
            set { m_Name1 = value; }
            get { return m_Name1; }
        }
        /// <summary>
        /// 网站简称
        /// </summary>
        public string Name2
        {
            set { m_Name2 = value; }
            get { return m_Name2; }
        }
        /// <summary>
        /// 网站地址
        /// </summary>
        public string Url
        {
            set { m_Url = value; }
            get { return m_Url; }
        }
        /// <summary>
        /// 安装目录
        /// </summary>
        public string Dir
        {
            set { m_Dir = value; }
            get { return m_Dir; }
        }
        /// <summary>
        /// 网站中文关键字
        /// </summary>
        public string Keywords1
        {
            set { m_Keywords1 = value; }
            get { return m_Keywords1; }
        }
        /// <summary>
        /// 网站中文描述
        /// </summary>
        public string Description1
        {
            set { m_Description1 = value; }
            get { return m_Description1; }
        }
        /// <summary>
        /// 网站中文备案号
        /// </summary>
        public string ICP1
        {
            set { m_ICP1 = value; }
            get { return m_ICP1; }
        }
        /// <summary>
        /// 网站英文关键字
        /// </summary>
        public string Keywords2
        {
            set { m_Keywords2 = value; }
            get { return m_Keywords2; }
        }
        /// <summary>
        /// 网站英文描述
        /// </summary>
        public string Description2
        {
            set { m_Description2 = value; }
            get { return m_Description2; }
        }
        /// <summary>
        /// 网站英文备案号
        /// </summary>
        public string ICP2
        {
            set { m_ICP2 = value; }
            get { return m_ICP2; }
        }
        /// <summary>
        /// 是否静态
        /// </summary>
        public bool IsHtml
        {
            set { m_IsHtml = value; }
            get { return m_IsHtml; }
        }
        /// <summary>
        /// 允许注册
        /// </summary>
        public bool AllowReg
        {
            set { m_AllowReg = value; }
            get { return m_AllowReg; }
        }
        /// <summary>
        /// 注册需要审核
        /// </summary>
        public bool CheckReg
        {
            set { m_CheckReg = value; }
            get { return m_CheckReg; }
        }

        /// <summary>
        /// 管理员组的编号
        /// </summary>
        public int AdminGroupId
        {
            set { m_AdminGroupId = value; }
            get { return m_AdminGroupId; }
        }
        /// <summary>
        /// Cookie作用域
        /// </summary>
        public string CookieDomain
        {
            set { m_CookieDomain = value; }
            get { return m_CookieDomain; }
        }
        /// <summary>
        /// Cookie作用路径
        /// </summary>
        public string CookiePath
        {
            set { m_CookiePath = value; }
            get { return m_CookiePath; }
        }
        /// <summary>
        /// Cookie前缀
        /// </summary>
        public string CookiePrev
        {
            set { m_CookiePrev = value; }
            get { return m_CookiePrev; }
        }
        /// <summary>
        /// Cookie加密密钥
        /// </summary>
        public string CookieKeyCode
        {
            set { m_CookieKeyCode = value; }
            get { return m_CookieKeyCode; }
        }
        /// <summary>
        /// 是否启用伪静态
        /// </summary>
        public bool UrlReWriter
        {
            set { m_UrlReWriter = value; }
            get { return m_UrlReWriter; }
        }
        /// <summary>
        /// 可以在线执行SQL
        /// </summary>
        public bool ExecuteSql
        {
            set { m_ExecuteSql = value; }
            get { return m_ExecuteSql; }
        }
        /// <summary>
        /// 用于调试的Key
        /// </summary>
        public string DebugKey
        {
            set { m_DebugKey = value; }
            get { return m_DebugKey; }
        }
        /// <summary>
        /// 单次发信的收件人数量
        /// </summary>
        public int MailOnceCount
        {
            set { m_MailOnceCount = value; }
            get { return m_MailOnceCount; }
        }
        /// <summary>
        /// 单个邮箱发信的间隔周期, 单位为秒
        /// </summary>
        public int MailTimeCycle
        {
            set { m_MailTimeCycle = value; }
            get { return m_MailTimeCycle; }
        }
        /// <summary>
        /// 客户端发信私钥
        /// </summary>
        public string MailPrivateKey
        {
            set { m_MailPrivateKey = value; }
            get { return m_MailPrivateKey; }
        }
        /// <summary>
        /// 是否为主站
        /// </summary>
        public bool MainSite
        {
            set { m_MainSite = value; }
            get { return m_MainSite; }
        }
        /// <summary>
        /// 是否为外网网站
        /// </summary>
        public bool WanSite
        {
            set { m_WanSite = value; }
            get { return m_WanSite; }
        }
        public string Version
        {
            set { m_Version = value; }
            get { return m_Version; }
        }
        /// <summary>
        /// 一样商品次最多能购买的件数
        /// </summary>
        public int ProductMaxBuyCount
        {
            set { m_ProductMaxBuyCount = value; }
            get { return m_ProductMaxBuyCount; }
        }
        /// <summary>
        /// 购物车最多能存放的数量
        /// </summary>
        public int ProductMaxCartCount
        {
            set { m_ProductMaxCartCount = value; }
            get { return m_ProductMaxCartCount; }
        }
        /// <summary>
        /// 最大未付款的订单，当订单超过这个数就不允许再进行购买
        /// 主要控制垃圾订单的数量
        /// </summary>
        public int ProductMaxOrderCount
        {
            set { m_ProductMaxOrderCount = value; }
            get { return m_ProductMaxOrderCount; }
        }
        public string PassportTheme
        {
            set { m_PassportTheme = value; }
            get { return m_PassportTheme; }
        }
    }
}

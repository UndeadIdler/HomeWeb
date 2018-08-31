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
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using System.Text;
using JumboECMS.Utils;
namespace JumboECMS.UI
{
    /// <summary>
    /// </summary>
    public class UserCenter : FrontHtml
    {
        public int publicMenu = 2;
        public string id = "0";
        protected string UserId = "0";
        protected string UserName = string.Empty;
        protected string UserNickName = string.Empty;
        protected string UserPass = string.Empty;
        protected string UserKey = string.Empty;
        protected string UserEmail = string.Empty;
        protected string UserGroupId = "0";
        protected string UserPower = string.Empty;
        protected string UserSetting = string.Empty;
        protected string UserCookies = string.Empty;
        protected bool UserIsLogin = false;

        /// <summary>
        /// 验证登陆
        /// </summary>
        private void chkLogin()
        {
            if (Cookie.GetValue(site.CookiePrev + "user") != null)
            {
                UserId = Cookie.GetValue(site.CookiePrev + "user", "id");
                UserGroupId = Cookie.GetValue(site.CookiePrev + "user", "groupid");
                UserName = Cookie.GetValue(site.CookiePrev + "user", "name");
                UserNickName = Cookie.GetValue(site.CookiePrev + "user", "nickname");
                UserPass = Cookie.GetValue(site.CookiePrev + "user", "password");
                UserEmail = Cookie.GetValue(site.CookiePrev + "user", "email");
                UserKey = UserPass.Substring(4, 8);
                UserSetting = Cookie.GetValue(site.CookiePrev + "user", "setting");
                UserCookies = Cookie.GetValue(site.CookiePrev + "user", "cookies");
                if (UserId.Length != 0 && UserName.Length != 0)
                {
                    JumboECMS.Entity.Normal_User _User = new JumboECMS.DAL.Normal_UserDAL().GetEntity(UserId);
                    if (_User.UserName.Length > 0)
                    {
                        this.UserIsLogin = true;
                    }
                }
            }
        }
        /// <summary>
        /// 验证权限
        /// </summary>
        /// <param name="s">空时只要求登录</param>
        private bool IsPower(string s)
        {
            if (s == "ok") return true;
            if (!this.UserIsLogin)//验证一次本地信息
                chkLogin();
            if (s == "") return (this.UserIsLogin);
            return (this.UserPower.Contains("," + s + ","));
        }

        /// <summary>
        /// 验证权限
        /// </summary>
        /// <param name="s"></param>
        /// <param name="pageType">页面分为html和json</param>
        private void chkPower(string s, string pageType)
        {
            if (!IsPower(s))
            {
                showErrMsg("权限不足或未登录", pageType);
            }
        }
        /// <summary>
        /// 输出错误信息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="pageType">页面分为html和json</param>
        private void showErrMsg(string msg, string pageType)
        {
            if (pageType != "json")
                FinalMessage(msg, site.Dir + "passport/login.aspx", 0);
            else
            {
                HttpContext.Current.Response.Clear();
                if (!this.UserIsLogin)
                    HttpContext.Current.Response.Write(JsonResult(-1, msg));
                else
                    HttpContext.Current.Response.Write(JsonResult(0, msg));
                HttpContext.Current.Response.End();
            }
        }
        /// <summary>
        /// 管理中心初始
        /// </summary>
        /// <param name="powerNum">权限,为空表示验证是否登录</param>
        /// <param name="pageType">页面分为html和json</param>
        protected void User_Load(string powerNum, string pageType)
        {
            chkPower(powerNum, pageType);
        }
    }
}
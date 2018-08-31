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
using System.Web;
using JumboECMS.Utils;
using JumboECMS.Common;
namespace JumboECMS.WebFile.Plus
{
    public partial class _user : JumboECMS.UI.FrontAjax
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 8;//脚本过期时间
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxLoginbar":
                    GetLoginbar();
                    break;
                case "ajaxUserInfo":
                    GetUserInfo();
                    break;
                default:
                    DefaultResponse();
                    break;
            }

            Response.Write(this._response);
        }

        private void DefaultResponse()
        {
            this._response = "{\"result\" :\"未知操作\"}";
        }
        private void GetUserInfo()
        {
            string tempBody = string.Empty;
            string _userid = "0";
            string _groupname = string.Empty;
            if (Cookie.GetValue(site.CookiePrev + "user") != null)
            {
                _userid = Cookie.GetValue(site.CookiePrev + "user", "id");
                _groupname = Cookie.GetValue(site.CookiePrev + "user", "groupname");
                tempBody = "{\"result\" :\"1\",";
                JumboECMS.Entity.Normal_User _User = new JumboECMS.DAL.Normal_UserDAL().GetEntity(_userid);
                int _newcart = new JumboECMS.DAL.Normal_UserCartDAL().GetNewGoods(_userid);
                tempBody += "userid :\"" + _User.Id + "\"," +
                    "username :\"" + _User.UserName + "\"," +
                    "nickname :\"" + _User.NickName + "\"," +
                    "userpass :\"" + _User.UserPass + "\"," +
                    "userkey :\"" + _User.UserPass.Substring(4, 8) + "\"," +
                    "email :\"" + _User.Email + "\"," +
                    "sex :\"" + _User.Sex + "\"," +
                    "truename :\"" + _User.TrueName + "\"," +
                    "idcard :\"" + _User.IDCard + "\"," +
                    "groupname :\"" + _groupname + "\"," +
                    "newcart :\"" + _newcart + "\"," +
                    "birthday :\"" + _User.BirthDay + "\"," +
                    "provincecity :\"" + _User.ProvinceCity + "\"," +
                    "workunit :\"" + _User.WorkUnit + "\"," +
                    "address :\"" + _User.Address + "\"," +
                    "zipcode :\"" + _User.ZipCode + "\"," +
                    "qq :\"" + _User.QQ + "\"," +
                    "msn :\"" + _User.MSN + "\"," +
                    "mobiletel :\"" + _User.MobileTel + "\"," +
                    "telephone :\"" + _User.Telephone + "\"," +
                    "adminid :\"" + _User.AdminId + "\"," +
                    "adminname :\"" + _User.AdminName + "\"" +
                    "}";
            }
            else
            {
                tempBody = "{\"result\" :\"0\",";
                tempBody += "userid :\"0\"," +
                    "username :\"\"," +
                    "nickname :\"\"," +
                    "userpass :\"\"," +
                    "userkey :\"\"," +
                    "email :\"\"," +
                    "sex :\"0\"," +
                    "truename :\"\"," +
                    "idcard :\"\"," +
                    "groupname :\"\"," +
                    "newcart :\"0\"," +
                    "birthday :\"\"," +
                    "provincecity :\"\"," +
                    "workunit :\"\"," +
                    "address :\"\"," +
                    "zipcode :\"\"," +
                    "qq :\"\"," +
                    "msn :\"\"," +
                    "mobiletel :\"\"," +
                    "telephone :\"\"," +
                    "adminid :\"0\"," +
                    "adminname :\"\"" +
                    "}";
            }
            this._response = tempBody;
        }
        private void GetLoginbar()
        {
            string tempBody = string.Empty;
            string returninfo = string.Empty;
            if (f("state") == "1")
            {
                string uName = f("name");
                string uPass = f("pass");
                returninfo = new JumboECMS.DAL.Normal_UserDAL().ChkUserLogin(uName, uPass, 1);
            }
            if (Cookie.GetValue(site.CookiePrev + "user") != null)
            {
                string UserId = Cookie.GetValue(site.CookiePrev + "user", "id");
                string UserName = Cookie.GetValue(site.CookiePrev + "user", "name");
                string UserNickName = Cookie.GetValue(site.CookiePrev + "user", "nickname");
                string UserPass = Cookie.GetValue(site.CookiePrev + "user", "password");
                string UserGroupName = Cookie.GetValue(site.CookiePrev + "user", "groupname");
                int _newcart = new JumboECMS.DAL.Normal_UserCartDAL().GetNewGoods(UserId);
                JumboECMS.Entity.Normal_User _User = new JumboECMS.DAL.Normal_UserDAL().GetEntity(UserId);
                tempBody = "{\"result\" :\"1\"," +
                    "userid :\"" + UserId + "\"," +
                    "username :\"" + UserName + "\"," +
                    "nickname :\"" + UserNickName + "\"," +
                    "userpass :\"" + UserPass + "\"," +
                    "userkey :\"" + UserPass.Substring(4, 8) + "\"," +
                    "groupname :\"" + UserGroupName + "\"," +
                    "newcart :\"" + _newcart + "\"," +
                    "adminid :\"" + _User.AdminId + "\"" +
                    "}";
                this._response = tempBody;
            }
            else
            {
                this._response = "{\"result\" :\"0\"";
                if (f("state") == "1")
                    this._response += ",\"returnval\" :\"" + returninfo + "\"";
                this._response += "}";
            }
        }
    }
}
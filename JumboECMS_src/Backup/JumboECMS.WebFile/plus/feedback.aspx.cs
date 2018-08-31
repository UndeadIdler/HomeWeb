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
using System.Web;
using JumboECMS.Common;
using JumboECMS.Utils;
namespace JumboECMS.WebFile.Plus
{
    public partial class _feedback : JumboECMS.UI.BasicPage
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckFormUrl())
            {
                Response.End();
            }
            Server.ScriptTimeout = 8;//脚本过期时间
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxPostFeedback":
                    ajaxPostFeedback();
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
        private void ajaxPostFeedback()
        {
            string lan = q("lan");
            Dictionary<string, object> lng = new JumboECMS.DAL.LanguageDAL().GetEntity(lan);
            int _GuestPost = JumboECMS.Utils.Validator.StrToInt(JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/feedback", "GuestPost"), 0);
            int _NeedCheck = JumboECMS.Utils.Validator.StrToInt(JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/feedback", "NeedCheck"), 0);
            int _PostTimer = JumboECMS.Utils.Validator.StrToInt(JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/feedback", "PostTimer"), 0);
            int countNum = 0;
            string whereStr = "[IP]='" + Const.GetUserIp + "'";
            if (DBType == "0")
                whereStr += " and datediff('s',adddate,'" + DateTime.Now.ToString() + "')<" + _PostTimer;
            else
                whereStr += " and datediff(s,adddate,'" + DateTime.Now.ToString() + "')<" + _PostTimer;
            doh.Reset();
            doh.ConditionExpress = whereStr;
            countNum = doh.Count("jcms_normal_feedback");
            if (countNum > 0)//说明周期内留过言
            {
                this._response = string.Format((string)lng["feedbackerr1"], _PostTimer);
                return;
            }
            doh.Reset();
            doh.AddFieldItem("IsPass", 0);
            doh.AddFieldItem("IP", Const.GetUserIp);
            doh.AddFieldItem("AddDate", DateTime.Now.ToString());
            doh.AddFieldItem("UserName", f("username"));
            doh.AddFieldItem("Type", f("type"));
            doh.AddFieldItem("Tel", f("tel"));
            doh.AddFieldItem("Email", f("email"));
            doh.AddFieldItem("Content", GetCutString(JumboECMS.Utils.Strings.HtmlEncode(f("content")), 200));
            doh.Insert("jcms_normal_feedback");
            this._response = "ok";
        }
    }
}

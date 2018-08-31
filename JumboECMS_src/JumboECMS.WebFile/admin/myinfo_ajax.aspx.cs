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
namespace JumboECMS.WebFile.Admin
{
    public partial class _myinfo_ajax : JumboECMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("", "json");
            this._operType = q("oper");
            switch (this._operType)
            {
                case "changepass":
                    ajaxChangePass();
                    break;
                default:
                    DefaultResponse();
                    break;
            }
            Response.Write(this._response);
        }

        private void DefaultResponse()
        {
            this._response = JsonResult(0, "未知操作");
        }
        private void ajaxChangePass()
        {
            string _oldPass = f("oldpass");
            string _NewPass = f("newpass");
            if (_NewPass.Length > 14 || _NewPass.Length < 6)
            {
                this._response = JsonResult(0, "请输入6-14位的新密码");
                return;
            }
            else
            {
                doh.Reset();
                doh.ConditionExpress = "adminid=@adminid and adminstate=1";
                doh.AddConditionParameter("@adminid", AdminId);
                object pass = doh.GetField("jcms_normal_user", "AdminPass");
                if (pass != null)
                {
                    if (pass.ToString().ToLower() == JumboECMS.Utils.MD5.Lower32(_oldPass) || pass.ToString().ToLower() == JumboECMS.Utils.MD5.Lower16(_oldPass)) //验证旧密码
                    {
                        doh.Reset();
                        doh.ConditionExpress = "adminid=@adminid and adminstate=1";
                        doh.AddConditionParameter("@adminid", AdminId);
                        doh.AddFieldItem("AdminPass", JumboECMS.Utils.MD5.Lower32(_NewPass));
                        doh.AddFieldItem("LastIP2", Const.GetUserIp);
                        doh.Update("jcms_normal_user");
                        this._response = JsonResult(1, "密码修改成功");
                    }
                    else
                    {
                        this._response = JsonResult(0, "旧密码错误");
                    }
                }
                else
                {
                    this._response = JsonResult(0, "未登录");
                }
            }
        }
    }
}
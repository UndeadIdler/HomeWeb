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
using JumboECMS.Utils;
using JumboECMS.Common;
namespace JumboECMS.WebFile.Plus
{
    public partial class _downcount : JumboECMS.UI.FrontHtml
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 8;//脚本过期时间
            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", q("id"));
            Response.Write(JumboECMS.Utils.Strings.Html2Js(Str2Str(doh.GetField("jcms_module_" + q("cType"), "DownNum").ToString())));
        }
    }
}
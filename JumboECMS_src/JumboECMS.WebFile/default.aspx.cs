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
using JumboECMS.Common;
namespace JumboECMS.WebFile
{
    public partial class _index : JumboECMS.UI.FrontHtml
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 8;//脚本过期时间
            string TxtStr = string.Empty;
            JumboECMS.DAL.TemplateEngineDAL teDAL = new JumboECMS.DAL.TemplateEngineDAL();
            TxtStr = teDAL.GetSiteDefaultPage();
            teDAL.ReplaceShtmlTag(ref TxtStr);
            Response.Write(TxtStr);//直接输出
        }
    }
}

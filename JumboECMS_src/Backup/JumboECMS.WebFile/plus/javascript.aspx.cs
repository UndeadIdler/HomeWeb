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

namespace JumboECMS.WebFile.Plus
{
    public partial class _javascript : JumboECMS.UI.FrontHtml
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string _code = q("code");
            if (_code.Length != 64)
                Response.Write("参数有误");
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/javascript.config");
            JumboECMS.DBUtility.XmlControl XmlTool = new JumboECMS.DBUtility.XmlControl(strXmlFile);
            string _TemplateContent = XmlTool.GetText("Lis/Li[Code=\"" + _code + "\"]/TemplateContent");
            XmlTool.Dispose();
            JumboECMS.DAL.TemplateEngineDAL teDAL = new JumboECMS.DAL.TemplateEngineDAL();
            string fileStr = ExecuteCommonTags(_TemplateContent);
            Response.Write(JumboECMS.Utils.Strings.Html2Js(fileStr));
        }
    }
}

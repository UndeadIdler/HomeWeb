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
using System.Data;
using System.Text;
namespace JumboECMS.UI
{
    public class FrontHtml : BasicPage
    {
        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }
        public bool CheckCookiesCode()
        {
            string _code = q("code");
            return JumboECMS.Common.ValidateCode.CheckValidateCode(_code);
        }
        /// <summary>
        /// 解析主站的基本信息
        /// </summary>
        /// <param name="PageStr"></param>
        protected void ReplaceSiteTags(ref string PageStr)
        {
            JumboECMS.DAL.TemplateEngineDAL teDAL = new JumboECMS.DAL.TemplateEngineDAL();
            teDAL.IsHtml = site.IsHtml;
            teDAL.ReplaceSiteTags(ref PageStr);
        }
        /// <summary>
        /// 获得页面html
        /// </summary>
        /// <param name="_page"></param>
        /// <returns></returns>
        protected string LoadPageHtml(string _page)
        {
            if (!_page.StartsWith("/") && !_page.StartsWith("~/"))
                _page = "~/templates/" + _page;
            if (!JumboECMS.Utils.DirFile.FileExists(_page + ".htm"))
                return _page + ".htm文件不存在";
            string PageStr = JumboECMS.Utils.DirFile.ReadFile(_page + ".htm");
            return ExecuteCommonTags(PageStr);
        }
        protected string GetContentFile(string _moduletype, string _contentid, int _page)
        {
            return JumboECMS.DAL.ModuleCommand.GetContent(_moduletype, _contentid, _page);
        }
    }
}
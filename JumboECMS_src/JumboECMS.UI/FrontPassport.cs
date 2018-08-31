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
    public class FrontPassport : BasicPage
    {
        public string PassportTheme = "";
        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (JumboECMS.Utils.Cookie.GetValue("passport_theme") == null)
                PassportTheme = site.PassportTheme;
            else
                PassportTheme = JumboECMS.Utils.Cookie.GetValue("passport_theme");
        }

    }
}
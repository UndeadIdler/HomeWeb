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
namespace JumboECMS.WebFile.Plus
{
    public partial class _codevoice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.ContentType = "audio/mpeg";
            Response.WriteFile("../statics/sound/begin.mp3");
            string checkCode = q("code");
            if (checkCode.Length > 0)
                for (int i = 0; i < checkCode.Length; i++)
                {
                    Response.WriteFile("../statics/sound/" + checkCode[i] + ".mp3");
                }
            Response.WriteFile("../statics/sound/end.mp3");
        }
        /// <summary>
        /// 获取querystring
        /// </summary>
        /// <param name="s">参数名</param>
        /// <returns>返回值</returns>
        public string q(string s)
        {
            if (HttpContext.Current.Request.QueryString[s] != null && HttpContext.Current.Request.QueryString[s] != "")
            {
                return JumboECMS.Utils.Strings.SafetyStr(HttpContext.Current.Request.QueryString[s].ToString());
            }
            return string.Empty;
        }
    }
}

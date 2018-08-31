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
namespace JumboECMS.WebFile.Admin.Soft.Plus
{
    public partial class _down : JumboECMS.UI.FrontHtml
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 5;//脚本过期时间
            if (q("userkey") != "666666")
            {
                if (q("userkey") != JumboECMS.Utils.Cookie.GetValue(site.CookiePrev + "user", "password").Substring(4, 8))
                {
                    Response.Redirect("~/errordown.aspx");
                    Response.End();
                }
            }
            string id = Str2Str(q("id"));
            if (id == "0")
            {
                FinalMessage("请不要修改地址栏参数!", site.Dir, 0, 8);
                Response.End();
            }
            int NO = Str2Int(q("NO"));
            doh.Reset();
            doh.ConditionExpress = "id=" + id;
            object[] _obj = doh.GetFields("jcms_module_down", "DownUrl,Title");
            string downUrl = _obj[0].ToString().Replace("\r\n", "\r");
            string _SoftTitle = _obj[1].ToString();
            if (downUrl != "")
            {
                doh.Reset();
                doh.ConditionExpress = "id=" + id;
                doh.Add("jcms_module_down", "DownNum");
                string[] _DownUrl = downUrl.Split(new string[] { "\r" }, StringSplitOptions.None);
                string _url = _DownUrl[NO];
                if (_url.Contains("|||"))
                    _url = _url.Substring(_url.IndexOf("|||") + 3, (_url.Length - _url.IndexOf("|||") - 3));
                DownloadFile(_url);
            }
            else
                FinalMessage("当前下载地址为空!", site.Dir, 0, 8);

        }
    }
}

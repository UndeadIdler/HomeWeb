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
using System.Web.UI;
using System.Web.UI.WebControls;
namespace JumboECMS.WebFile.Plus.Search
{
    public partial class _index : JumboECMS.UI.FrontHtml
    {
        public string Keywords, SplitWords, ChannelType, Mode;
        public int CurrentPage = 1, PageSize = 10;
        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 8;//脚本过期时间
            CurrentPage = Int_ThisPage();
            Keywords = q("k");
            Mode = q("mode");
            //过滤特殊字符
            //Keywords = JumboECMS.Utils.Strings.FilterSymbol(Keywords);
            //去除多余空格
            Keywords = System.Text.RegularExpressions.Regex.Replace(Keywords, "\\s{2,}", " ");
            ChannelType = q("type");
            SplitWords = JumboECMS.Utils.WordSpliter.GetKeyword(Keywords);//自动分词
        }
    }
}

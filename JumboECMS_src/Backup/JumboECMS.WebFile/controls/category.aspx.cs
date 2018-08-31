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
using System.Web.UI.WebControls;
using JumboECMS.Common;

namespace JumboECMS.WebFile.Controls
{
    public partial class _class : JumboECMS.UI.FrontHtml
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 8;//脚本过期时间
            int CurrentPage = Int_ThisPage();
            string ClassId = (this.lblClassId.Text == "{$CategoryId}") ? Str2Str(q("id")) : Str2Str(this.lblClassId.Text);
            string ModuleType = this.lblModuleType.Text;
            doh.Reset();
            doh.SqlCmd = "SELECT ID FROM [" + base.CategoryTable + "] WHERE [Id]=" + ClassId;
            DataTable dtSearch = doh.GetDataTable();
            if (dtSearch.Rows.Count == 0)
            {
                FinalMessage("栏目不存在或已被删除!", site.Dir, 0, 8);
                Response.End();
            }
            dtSearch.Clear();
            dtSearch.Dispose();
            JumboECMS.DAL.TemplateEngineDAL teDAL = new JumboECMS.DAL.TemplateEngineDAL();
            int pageCount = new JumboECMS.DAL.Normal_CategoryDAL().GetContetPageCount(ClassId, true);
            CurrentPage = JumboECMS.Utils.Int.Min(CurrentPage, pageCount);
            teDAL.IsHtml = true;
            string HtmlUrl = Go2Category(ClassId, CurrentPage);
            if (!System.IO.File.Exists(Server.MapPath(HtmlUrl)))//保存静态
            {
                string TxtStr = teDAL.GetSiteCategoryPage(ClassId, CurrentPage);
                SaveCacheFile(TxtStr, Server.MapPath(HtmlUrl));
            }
            Response.Redirect(HtmlUrl);
        }
    }
}

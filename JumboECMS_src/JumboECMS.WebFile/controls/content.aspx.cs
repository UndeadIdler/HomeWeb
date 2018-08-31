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
    public partial class _content : JumboECMS.UI.FrontHtml
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 8;//脚本过期时间
            int CurrentPage = Int_ThisPage();
            string ContentId = this.lblContentId.Text == "{$ContentId}" ? Str2Str(q("id")) : this.lblContentId.Text;
            if (ContentId == "0")
            {
                FinalMessage("参数错误!", site.Dir, 0, 8);
                Response.End();
            }
            string ModuleType = this.lblModuleType.Text;
            JumboECMS.DAL.TemplateEngineDAL teDAL = new JumboECMS.DAL.TemplateEngineDAL();
            doh.Reset();
            doh.SqlCmd = "SELECT [CategoryId] FROM [jcms_module_" + ModuleType + "] WHERE [IsPass]=1 and [Id]=" + ContentId;
            DataTable dtSearch = doh.GetDataTable();
            if (dtSearch.Rows.Count == 0)
            {
                dtSearch.Clear();
                dtSearch.Dispose();
                FinalMessage("内容不存在或未审核!", site.Dir, 0, 8);
                Response.End();
            }
            string ClassId = dtSearch.Rows[0]["CategoryId"].ToString();
            dtSearch.Clear();
            dtSearch.Dispose();

            if (!teDAL.CanReadContent(ModuleType, ContentId, ClassId))
            {
                FinalMessage("阅读权限不足!", site.Dir, 0, 8);
                Response.End();
            }
            doh.Reset();
            doh.SqlCmd = "SELECT Id FROM [" + base.CategoryTable + "] WHERE [Id]=" + ClassId;
            if (doh.GetDataTable().Rows.Count == 0)
            {
                FinalMessage("内容所属栏目有误!", site.Dir, 0, 8);
                Response.End();
            }
            teDAL.IsHtml = true;
            string HtmlUrl = Go2View(ModuleType, ContentId, CurrentPage);
            if (!System.IO.File.Exists(Server.MapPath(HtmlUrl)))//静态
            {
                string TxtStr = GetContentFile(ModuleType, ContentId, CurrentPage);
                SaveCacheFile(TxtStr, Server.MapPath(HtmlUrl));
            }
            Response.Redirect(HtmlUrl);
        }
    }
}

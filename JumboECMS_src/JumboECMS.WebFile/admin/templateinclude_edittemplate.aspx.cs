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
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using JumboECMS.Common;
namespace JumboECMS.WebFile.Admin
{
    public partial class _templateinclude_edittemplate : JumboECMS.UI.AdminCenter
    {
        public string tpPath = string.Empty;
        private string _Source = string.Empty;
        private string _tempFile = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("9999", "html");
            _Source = q("source");
            _tempFile = site.Dir + "templates/default/include/" + _Source;
            this.lblTemplateFile.Text = _tempFile;
            if (!IsPostBack)
            {
                if (JumboECMS.Utils.DirFile.FileExists(_tempFile))
                    this.txtTemplateContent.Text = JumboECMS.Utils.DirFile.ReadFile(_tempFile);
                else
                    this.txtTemplateContent.Text = "";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string PageStr = this.txtTemplateContent.Text;
            JumboECMS.Utils.DirFile.SaveFile(PageStr, _tempFile);
            FinalMessage("成功保存", "close.htm", 0);
        }
    }
}

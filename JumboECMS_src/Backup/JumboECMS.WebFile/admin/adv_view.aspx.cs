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
    public partial class _adv_view : JumboECMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Str2Str(q("id"));
            Admin_Load("adv-mng", "stop");
            this.txtASPXTmpTag.Text = "<!--#include virtual=\"/_data/html/more/" + id + ".htm\" -->";
            this.txtSHTMTmpTag.Text = "<!--#include virtual=\"/_data/shtm/more/" + id + ".htm\" -->";
            this.txtJSTmpTag.Text = "<script type=\"text/javascript\" src=\"/_data/style/more/" + id + ".js\"></script>";
            this.Literal1.Text = new JumboECMS.DAL.AdvDAL().GetAdvBody(id);
        }
    }
}

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
    public partial class _adv_list : JumboECMS.UI.AdminCenter
    {
        public string type = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("adv-mng", "stop");
            type = q("type");
        }
    }
}

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
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using JumboECMS.Utils;
using JumboECMS.Common;
namespace JumboECMS.WebFile.Admin
{
    public partial class _cut2thumbs_process : JumboECMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ModuleType = q("module").Trim();
            Admin_Load("", "html", ModuleType);
            if (!Page.IsPostBack)
            {
                string TempPhoto = q("tphoto").Replace(site.Url, "");
                string ToWidth = q("tow");
                string ToHeight = q("toh");
                this.w.Text = this.tow.Value = ToWidth;
                this.h.Text = this.toh.Value = ToHeight;
                this.PhotoUrl.Value = TempPhoto;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int tow, toh, x, y, w, h;
            string file;
            tow = Convert.ToInt16(this.tow.Value.ToString());
            toh = Convert.ToInt16(this.toh.Value.ToString());
            x = Convert.ToInt16(this.x.Text);
            y = Convert.ToInt16(this.y.Text);
            w = Convert.ToInt16(this.w.Text);
            h = Convert.ToInt16(this.h.Text);

            file = Server.MapPath(this.PhotoUrl.Value.ToString());
            string fileExtension = ".jpg"; //缩略图后缀名
            string DirectoryPath = this.MainModule.UploadPath + DateTime.Now.ToString("yyMMdd");
            JumboECMS.Utils.DirFile.CreateDir(DirectoryPath);

            string sFileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_thumbs" + fileExtension;  // 文件名称
            string thumbnailPath = Server.MapPath(DirectoryPath + "/" + sFileName);        // 服务器端文件路径

            JumboECMS.Utils.ImageHelp.MakeMyThumbs(file, thumbnailPath, tow, toh, x, y, w, h);
            WriteJs("-1", "parent.opener.FillPhoto('" + DirectoryPath + "/" + sFileName + "');parent.close();");
        }

    }
}

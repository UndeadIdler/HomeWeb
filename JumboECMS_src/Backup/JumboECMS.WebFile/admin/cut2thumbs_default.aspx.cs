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
namespace JumboECMS.WebFile.Admin
{
    public partial class _cut2thumbs_default : JumboECMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ModuleType = q("module").Trim();
            Admin_Load("", "html", ModuleType);
            string FrameName = q("fname");
            string TempPhoto = q("tphoto").Replace(site.Url, "").Split('?')[0];
            string ToWidth = q("tow");
            string ToHeight = q("toh");
            string CutType = q("type");
            if (CutType == "1")//手工裁剪
            {
                string printhtml = "";
                printhtml += "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Frameset//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd\">\r\n";
                printhtml += "<html   xmlns=\"http://www.w3.org/1999/xhtml\">\r\n";
                printhtml += "<head>\r\n";
                printhtml += "<meta   http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n";
                printhtml += "<title>图片裁剪</title>\r\n";
                printhtml += "</head>\r\n";
                printhtml += "<frameset rows=\"30,*\" cols=\"*\" framespacing=\"0\" frameborder=\"no\" border=\"0\">\r\n";
                printhtml += "<frame src=\"cut2thumbs_process.aspx?module=" + ModuleType + "&tphoto=" + TempPhoto + "&tow=" + ToWidth + "&toh=" + ToHeight + "\" name=\"topFrame\" id=\"topFrame\" />\r\n";
                printhtml += "<frame src=\"cut2thumbs_preview.aspx?module=" + ModuleType + "&tphoto=" + TempPhoto + "&tow=" + ToWidth + "&toh=" + ToHeight + "\" name=\"mainFrame\" scrolling=\"auto\" noresize=\"noresize\" id=\"mainFrame\" />\r\n";
                printhtml += "</frameset>\r\n";
                printhtml += "<noframes><body>\r\n";
                printhtml += "</body>\r\n";
                printhtml += "</noframes></html>\r\n";
                Response.Write(printhtml);
            }
            else//自动缩放
            {
                string fileExtension = ".jpg"; //缩略图后缀名

                string DirectoryPath = this.MainModule.UploadPath + DateTime.Now.ToString("yyMMdd");
                JumboECMS.Utils.DirFile.CreateDir(DirectoryPath);

                string sFileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_thumbs" + fileExtension;  // 文件名称
                string thumbnailPath = Server.MapPath(DirectoryPath + "/" + sFileName);        // 服务器端文件路径

                //以jpg格式保存缩略图
                JumboECMS.Utils.ImageHelp.LocalImage2Thumbs(Server.MapPath(TempPhoto), thumbnailPath, Convert.ToInt32(ToWidth), Convert.ToInt32(ToHeight), CutType);
                Response.Write("<script>opener.FillPhoto('" + DirectoryPath + "/" + sFileName + "');window.close();</script>");
            }
        }
    }
}

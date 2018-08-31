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
using JumboECMS.Common;

namespace JumboECMS.WebFile.Admin
{
    public partial class _cut2thumbs_upfile : JumboECMS.UI.AdminCenter
    {
        private string _sAdminUploadPath;
        private string _sAdminUploadType;
        private int _sAdminUploadSize = 0;
        private int _sPhotoMaxWidth = 600;
        protected void Page_Load(object sender, EventArgs e)
        {
            ModuleType = q("module").Trim();
            Admin_Load("", "json", ModuleType);
            this._sAdminUploadPath = site.Dir + "_data/tempfiles";
            this._sAdminUploadType = "*.jpg;*.jpeg;*.bmp;*.gif;";
            this._sAdminUploadSize = 2048;
            if (this.Page.Request.Files.Count > 0)
            {
                HttpPostedFile oFile = this.Page.Request.Files[0];//得到要上传文件
                if (oFile != null && oFile.ContentLength > 0)
                {
                    if (!JumboECMS.Utils.FileValidation.IsSecureUploadPhoto(oFile))
                    {
                        SaveVisitLog(2, 0);
                        Response.Write("不安全的图片格式，换一张吧。");
                    }
                    else
                    {
                        try
                        {
                            string fileContentType = oFile.ContentType; //文件类型
                            string fileExtension = System.IO.Path.GetExtension(oFile.FileName).ToLower(); //上传文件的扩展名
                            string F_Type = fileExtension.Substring(1, fileExtension.Length - 1);
                            if (this._sAdminUploadType.ToLower().Contains("*.*") || this._sAdminUploadType.ToLower().Contains("*." + F_Type + ";"))//检测是否为允许的上传文件类型
                            {
                                if (this._sAdminUploadSize * 1024 >= oFile.ContentLength)//检测文件大小是否超过限制
                                {
                                    string DirectoryPath;
                                    DirectoryPath = this._sAdminUploadPath + "/admin_" + AdminId;
                                    JumboECMS.Utils.DirFile.CreateDir(this._sAdminUploadPath + "/admin_" + AdminId);

                                    string sFileName = "Temp" + fileExtension;  // 文件名称
                                    string FullPath = DirectoryPath + "/" + sFileName;        // 服务器端文件路径
                                    oFile.SaveAs(Server.MapPath(FullPath));
                                    if (JumboECMS.Utils.FileValidation.IsSecureUpfilePhoto(Server.MapPath(FullPath)))
                                    {
                                        string[] toWidthHeight = q("ThumbsSize").Split('|');
                                        string toWidth = toWidthHeight[0];
                                        string toHeight = toWidthHeight[1];
                                        string cutType = q("CutType");
                                        System.Drawing.Image originalImage = System.Drawing.Image.FromFile(Server.MapPath(FullPath));
                                        if (originalImage.Width < Convert.ToInt32(toWidth) || originalImage.Height < Convert.ToInt32(toHeight))
                                        {
                                            Response.Write(JsonResult(0, "原图片尺寸不得小于缩略图尺寸。"));
                                            originalImage.Dispose();
                                        }
                                        else
                                        {
                                            if (originalImage.Width > _sPhotoMaxWidth)
                                            {
                                                JumboECMS.Utils.ImageHelp.Image2Thumbs(originalImage, Server.MapPath(FullPath + ".jpg"), _sPhotoMaxWidth, Convert.ToInt32(_sPhotoMaxWidth * originalImage.Height / originalImage.Width), "HW");
                                                FullPath += ".jpg";
                                            }
                                            originalImage.Dispose();
                                            Response.Write(JsonResult(1, FullPath));
                                        }
                                    }
                                    else
                                    {
                                        SaveVisitLog(2, 0);
                                        Response.Write("不安全的图片格式，换一张吧。");
                                    }

                                }
                                else//文件大小超过限制
                                {
                                    Response.Write(JsonResult(0, "图片大小" + Convert.ToInt32(oFile.ContentLength / 1024) + "KB,超出限制。"));
                                }
                            }
                            else
                            {
                                Response.Write(JsonResult(0, "上传的不是图片。"));
                            }
                        }
                        catch
                        {
                            Response.Write(JsonResult(0, "程序异常，上传未成功。"));
                        }
                    }
                }
                else
                {
                    Response.Write(JsonResult(0, "请选择上传文件。"));
                }
            }
            else
                Response.Write(JsonResult(0, "上传有误。"));
        }
    }
}

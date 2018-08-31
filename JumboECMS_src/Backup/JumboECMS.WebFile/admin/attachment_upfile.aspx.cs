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
using System.Web;
using System.IO;
using System.Data;
namespace JumboECMS.WebFile.Admin.Attachment
{
    public partial class _upfile : JumboECMS.UI.AdminCenter
    {
        private string _sAdminUploadPath;
        private string _sAdminUploadType;
        private int _sAdminUploadSize = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //请勿使用Session和Cookies来判断权限
            ModuleType = q("module").Trim();
            Admin_Load("ok", "html", ModuleType);
            if (!(new JumboECMS.DAL.AdminDAL()).ChkAdminSign(q("adminid"), q("adminsign")))
            {
                Response.Write("验证信息有误");
                Response.End();
            }
            if (Request.Files.Count > 0)
            {
                HttpPostedFile oFile = Request.Files[0];//得到要上传文件
                if (oFile != null && oFile.ContentLength > 0)
                {
                    if (!JumboECMS.Utils.FileValidation.IsSecureUploadPhoto(oFile))
                    {
                        SaveVisitLog(2, 0);
                        Response.Write("不安全的图片格式，换一张吧。");
                    }
                    else
                    {
                        string fileExtension = System.IO.Path.GetExtension(oFile.FileName).ToLower(); //上传文件的扩展名
                        this._sAdminUploadPath = MainModule.UploadPath;
                        this._sAdminUploadType = MainModule.UploadType;
                        this._sAdminUploadSize = MainModule.UploadSize;
                        if (this._sAdminUploadType.ToLower().Contains("*.*") || this._sAdminUploadType.ToLower().Contains("*" + fileExtension + ";"))//检测是否为允许的上传文件类型
                        {
                            if (this._sAdminUploadSize * 1024 >= oFile.ContentLength)//检测文件大小是否超过限制
                            {
                                string DirectoryPath = this._sAdminUploadPath + DateTime.Now.ToString("yyMMdd");
                                string sFileName = DateTime.Now.ToString("yyyyMMddHHmmssffff");  //文件名称
                                string FullPath = DirectoryPath + "/" + sFileName + fileExtension;//最终文件路径
                                try
                                {
                                    JumboECMS.Utils.DirFile.CreateDir(this._sAdminUploadPath + DateTime.Now.ToString("yyMMdd"));
                                    oFile.SaveAs(Server.MapPath(FullPath));
                                    if (JumboECMS.Utils.FileValidation.IsSecureUpfilePhoto(Server.MapPath(FullPath)))
                                        Response.Write("ok|" + FullPath.Replace("//", "/"));
                                    else
                                    {
                                        SaveVisitLog(2, 0);
                                        Response.Write("不安全的图片格式，换一张吧。");
                                    }
                                }
                                catch
                                {
                                    Response.Write(FullPath + "上传未成功。");
                                }

                            }
                            else
                                Response.Write("文件大小超过限制。");
                        }
                        else
                            Response.Write("文件类型不允许上传。");

                    }
                }
                else
                    Response.Write("请选择上传文件。");
            }
            else
                Response.Write("上传有误。");
        }

    }
}

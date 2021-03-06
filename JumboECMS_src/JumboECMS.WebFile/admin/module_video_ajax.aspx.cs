﻿/*
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
using JumboECMS.Utils;
using JumboECMS.Common;
namespace JumboECMS.WebFile.Admin
{
    public partial class _module_video_ajax : JumboECMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Str2Str(q("id"));
            Admin_Load("", "json", "video");
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxGetList":
                    ajaxGetList();
                    break;
                case "ajaxBatchOper":
                    ajaxBatchOper();
                    break;
                case "ajaxDelete":
                    ajaxDelete();
                    break;
                case "ajaxVideoConvert2Flv":
                    ajaxVideoConvert2Flv();
                    break;
                case "checkname":
                    ajaxCheckName();
                    break;
                default:
                    DefaultResponse();
                    break;
            }
            Response.Write(this._response);
        }

        private void DefaultResponse()
        {
            this._response = JsonResult(0, "未知操作");
        }
        private void ajaxCheckName()
        {
            doh.Reset();
            doh.ConditionExpress = "title=@title and id<>" + Str2Str(q("id"));
            doh.AddConditionParameter("@title", q("txtTitle"));
            if (doh.Exist("jcms_module_" + this.MainModule.Type))
                this._response = JsonResult(0, "不可录入");
            else
                this._response = JsonResult(1, "可以录入");
        }
        private void ajaxGetList()
        {
            Admin_Load(this.MainModule.Type + "-manage", "json");
            string categoryid = Str2Str(q("categoryid"));
            string _k = q("k");
            string _f = q("f");
            string _s = q("s");
            string _p = q("p");
            string _t = q("t");
            string _d = q("d");
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 20);

            this._response = GetContentList(categoryid, _f, _k, _d, _s, Str2Str(q("isimg")), Str2Str(q("istop")), Str2Str(q("isfocus")), PSize, page);
        }
        private void ajaxDelete()
        {
            Admin_Load(this.MainModule.Type + "-delete", "json");
            string lId = f("id");
            doh.Reset();
            doh.ConditionExpress = "id=" + lId;
            doh.Delete("jcms_module_" + this.MainModule.Type);
            this._response = JsonResult(1, "成功删除");
        }
        /// <summary>
        /// 执行批量操作
        /// </summary>
        /// <param name="oper"></param>
        /// <param name="ids"></param>
        private void ajaxBatchOper()
        {
            string act = q("act");
            string ids = f("ids");
            BatchContent(act, ids, "json");
            this._response = JsonResult(1, "操作成功");
        }
        /// <summary>
        /// 视频格式转换
        /// </summary>
        private void ajaxVideoConvert2Flv()
        {
            int SustainFLV = Str2Int(JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "SustainFLV"), 0);
            if (SustainFLV != 1)
            {
                this._response = JsonResult(0, "服务器不支持flv格式");
                return;
            }
            string videoFile = q("file");
            string extendName = videoFile.Substring(videoFile.LastIndexOf(".") + 1);
            if (!extendName.ToLower().Equals("flv"))
            {
                string fromName = videoFile;
                string exportName = videoFile.Substring(0, videoFile.Length - 4) + ".flv";
                if (JumboECMS.Utils.ffmpegHelp.Convert2Flv(fromName, "480*360", exportName))
                {
                    JumboECMS.Utils.DirFile.DeleteFile(fromName);
                    int iWidth = 0, iHeight = 0;
                    new JumboECMS.DAL.Normal_ModuleDAL().GetThumbsSize(this.MainModule.Type, ref iWidth, ref iHeight);
                    string CatchImg = JumboECMS.Utils.ffmpegHelp.CatchImg(exportName, iWidth + "x" + iHeight, "15");
                    if (CatchImg != "")
                        this._response = JsonResult(1, exportName + "|" + CatchImg);
                    else
                        this._response = JsonResult(1, exportName);
                }
                else
                {
                    this._response = JsonResult(0, fromName);
                }
            }
            else
                this._response = JsonResult(0, "flv格式不需要转换");
        }
    }
}
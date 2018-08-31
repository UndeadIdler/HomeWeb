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

namespace JumboECMS.Common.Handler
{
    /// <summary>
    ///FlvHandler 的摘要说明
    /// </summary>
    public class FlvHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            // 获取文件服务器端物理路径
            string FileName = context.Server.MapPath(context.Request.FilePath);
            if (FileName.ToLower().EndsWith(".flv") == false)
            {
                context.Response.StatusCode = 404;
                context.Response.SuppressContent = true;
                context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                context.Response.ContentType = "video/x-flv";
                context.Response.WriteFile(FileName);
            }
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}






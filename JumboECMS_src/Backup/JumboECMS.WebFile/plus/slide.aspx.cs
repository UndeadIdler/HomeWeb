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
namespace JumboECMS.WebFile.Plus
{
    public partial class _slide : JumboECMS.UI.FrontHtml
    {
        public string ID = "";
        public string Pics = "";
        public string Links = "";
        public string Width = "0";
        public string Height = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            ID = q("id");
            doh.Reset();
            doh.SqlCmd = "SELECT * FROM [jcms_normal_slide] WHERE [state]=1 AND [ID]=" + ID;
            DataTable dt = doh.GetDataTable();
            if (dt != null)
            {
                Width = dt.Rows[0]["Width"].ToString();
                Height = dt.Rows[0]["Height"].ToString();
                Pics += dt.Rows[0]["Img1"].ToString();
                Links += ((dt.Rows[0]["Url1"].ToString().Length > 2) ? dt.Rows[0]["Url1"].ToString() : "#");
                if (dt.Rows[0]["Img2"].ToString().Length > 4)
                {
                    Pics += "|" + dt.Rows[0]["Img2"].ToString();
                    Links += "|" + ((dt.Rows[0]["Url2"].ToString().Length > 2) ? dt.Rows[0]["Url2"].ToString() : "#");
                }
                if (dt.Rows[0]["Img3"].ToString().Length > 4)
                {
                    Pics += "|" + dt.Rows[0]["Img3"].ToString();
                    Links += "|" + ((dt.Rows[0]["Url3"].ToString().Length > 3) ? dt.Rows[0]["Url3"].ToString() : "#");
                }
                if (dt.Rows[0]["Img4"].ToString().Length > 4)
                {
                    Pics += "|" + dt.Rows[0]["Img4"].ToString();
                    Links += "|" + ((dt.Rows[0]["Url4"].ToString().Length > 4) ? dt.Rows[0]["Url4"].ToString() : "#");
                }
                if (dt.Rows[0]["Img5"].ToString().Length > 5)
                {
                    Pics += "|" + dt.Rows[0]["Img5"].ToString();
                    Links += "|" + ((dt.Rows[0]["Url5"].ToString().Length > 5) ? dt.Rows[0]["Url5"].ToString() : "#");
                }
            }
            dt.Clear();
            dt.Dispose();
        }
    }
}
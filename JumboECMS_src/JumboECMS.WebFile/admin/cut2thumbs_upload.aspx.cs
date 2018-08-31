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
using System.Web.UI;
using System.Web.UI.WebControls;
using JumboECMS.Utils;
using JumboECMS.Common;

namespace JumboECMS.WebFile.Admin
{
    public partial class _cut2thumbs_upload : JumboECMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ModuleType = q("module").Trim();
            Admin_Load("", "html", ModuleType);
            this.ThumbsSize.Items.Clear();
            ListItem li;
            DataTable dtThumbs = new JumboECMS.DAL.Normal_ThumbsDAL().GetDataTable(ModuleType);
            for (int i = 0; i < dtThumbs.Rows.Count; i++)
            {
                li = new ListItem();
                li.Value = dtThumbs.Rows[i]["iWidth"].ToString() + "|" + dtThumbs.Rows[i]["iHeight"].ToString();
                li.Text = dtThumbs.Rows[i]["Title"].ToString();
                if (this.MainModule.DefaultThumbs == Str2Int(dtThumbs.Rows[i]["ID"].ToString()))
                    li.Selected = true;
                else
                    li.Selected = false;
                this.ThumbsSize.Items.Add(li);
            }
            dtThumbs.Clear();
            dtThumbs.Dispose();
            if (q("photo") != "")
            {
                NewsCollection nc = new NewsCollection();
                this.Image1.ImageUrl = nc.LocalFileUrl(site.Url, site.MainSite, q("photo"), this.MainModule.UploadPath, true, 0, 0);
            }
        }
    }
}


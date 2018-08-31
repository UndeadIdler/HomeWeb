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
    public partial class category_modify3 : JumboECMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Str2Str(q("id"));
            Admin_Load("page-mng", "stop");
            Session["FCKeditor:UserUploadPath"] = MainModule.UploadPath;
            this.FCKeditor1.BasePath = site.Dir + "_libs/fckeditor/";
            JumboECMS.DBUtility.WebFormHandler wh = new JumboECMS.DBUtility.WebFormHandler(doh, base.CategoryTable, btnSave);
            wh.AddBind(lblTitle, "Title", true);
            wh.AddBind(txtInfo, "Info", true);
            wh.AddBind(txtKeywords, "Keywords", true);
            wh.AddBind(txtImg, "Img", true);
            wh.AddBind(FCKeditor1, "Value", "Content", true);
            wh.ConditionExpress = "id=" + id;
            wh.Mode = JumboECMS.DBUtility.OperationType.Modify;
            wh.BindBeforeModifyOk += new EventHandler(bind_ok);
            wh.ModifyOk += new EventHandler(save_ok);
            wh.validator = chkForm;
        }
        protected void bind_ok(object sender, EventArgs e)
        {
            this.txtInfo.Text = JumboECMS.Utils.Strings.HtmlDecode(this.txtInfo.Text.Trim());
        }
        protected bool chkForm()
        {
            if (!CheckFormUrl())
                return false;
            if (!Page.IsValid)
                return false;
            if (FCKeditor1.Value == "")
            {
                this.txtContentMsg.Text = "请填写内容!";
                return false;
            }
            return true;
        }
        protected void save_ok(object sender, EventArgs e)
        {
            CreateCategoryFile(id, true);
            FinalMessage("成功保存", "category_modify3.aspx?id=" + id, 0);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {           //保存远程图片
            if (this.chkSaveRemotePhoto.Checked)
            {
                string cBody = FCKeditor1.Value;
                NewsCollection nc = new NewsCollection();
                int iWidth = 0, iHeight = 0;
                new JumboECMS.DAL.Normal_ModuleDAL().GetThumbsSize(this.MainModule.Type, ref iWidth, ref iHeight);
                System.Collections.ArrayList bodyArray = nc.ProcessRemotePhotos(site.Url, site.MainSite, cBody, MainModule.UploadPath, site.Url, true, iWidth, iHeight);
                FCKeditor1.Value = bodyArray[0].ToString();
                //不多余
                if (this.txtImg.Text.StartsWith("http://") || this.txtImg.Text.StartsWith("https://"))
                {
                    this.txtImg.Text = nc.GetThumtnail(site.Url, site.MainSite, this.txtImg.Text, MainModule.UploadPath, true, iWidth, iHeight);
                }
            }
            //格式化简介
            if (this.txtInfo.Text.Length != 0)
                this.txtInfo.Text = GetCutString(JumboECMS.Utils.Strings.HtmlEncode(JumboECMS.Utils.Strings.RemoveSpaceStr(this.txtInfo.Text)), 500).Trim();

        }
    }
}

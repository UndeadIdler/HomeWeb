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
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using JumboECMS.Common;
namespace JumboECMS.WebFile.Admin
{
    public partial class _module_product_edit : JumboECMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CategoryId = Str2Str(q("categoryid"));
            id = Str2Str(q("id"));
            if (id == "0")
                Admin_Load("product-add", "html", "product");
            else
                Admin_Load("product-edit", "html", "product");
            this.txtEditor.Text = AdminName;
            getEditDropDownList(CategoryId, ModuleType, ref ddlReadGroup);
            //没有下面这段，浏览功能就失效
            Session["FCKeditor:UserUploadPath"] = this.MainModule.UploadPath;
            this.FCKeditor1.BasePath = site.Dir + "_libs/fckeditor/";
            doh.Reset();
            JumboECMS.DBUtility.WebFormHandler wh = new JumboECMS.DBUtility.WebFormHandler(doh, "jcms_module_product", btnSave);
            wh.AddBind(txtTitle, "Title", true);
            wh.AddBind(txtTColor, "TColor", true);
            wh.AddBind(ref CategoryId, "CategoryId", false);
            wh.AddBind(ddlReadGroup, "ReadGroup", false);
            wh.AddBind(txtAuthor, "Author", true);
            wh.AddBind(txtEditor, "Editor", true);
            wh.AddBind(txtUserId, "UserId", false);
            wh.AddBind(txtTags, "Tags", true);
            wh.AddBind(txtPrice0, "Price0", false);
            wh.AddBind(txtPrice1, "Price1", false);
            wh.AddBind(txtImg, "Img", true);
            wh.AddBind(rblIsTop, "SelectedValue", "IsTop", false);
            wh.AddBind(FCKeditor1, "Value", "Content", true);
            wh.AddBind(txtSummary, "Summary", true);
            wh.AddBind(chkIsEdit, "1", "IsPass", false);
            wh.AddBind(txtAddDate, "AddDate", true);
            wh.AddBind(txtAliasPage, "AliasPage", true);
            if (id == "0")
            {
                wh.Mode = JumboECMS.DBUtility.OperationType.Add;
                if (IsPower(this.MainModule.Type + "-audit"))
                    this.chkIsEdit.Checked = true;
            }
            else
            {
                wh.ConditionExpress = "id=" + id + " AND [CategoryId]=" + CategoryId;
                wh.Mode = JumboECMS.DBUtility.OperationType.Modify;
            }
            if (this.MainModule.IsHtml) this.ddlReadGroup.Enabled = false;
            wh.BindBeforeAddOk += new EventHandler(bind_ok);
            wh.BindBeforeModifyOk += new EventHandler(bind_ok);
            wh.AddOk += new EventHandler(save_ok);
            wh.ModifyOk += new EventHandler(save_ok);
            wh.validator = chkForm;
        }
        /// <summary>
        /// 绑定数据后的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void bind_ok(object sender, EventArgs e)
        {
            this.txtPrice0.Text = JumboECMS.Utils.Strings.ToMoney(this.txtPrice0.Text);
            this.txtSummary.Text = JumboECMS.Utils.Strings.HtmlDecode(this.txtSummary.Text);
            if (id == "0")
                this.txtAddDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            else
                this.txtAddDate.Text = this.txtAddDate.Text == "" ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : Convert.ToDateTime(this.txtAddDate.Text).ToString("yyyy-MM-dd HH:mm:ss");

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

            if (id == "0")
            {
                JumboECMS.DBUtility.DbOperEventArgs de = (JumboECMS.DBUtility.DbOperEventArgs)e;
                id = de.id.ToString();
            }
            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", id);
            if (txtImg.Text != "")
                doh.AddFieldItem("IsImg", "1");
            else
                doh.AddFieldItem("IsImg", "0");
            //初始化第一页
            if (this.txtAliasPage.Text.Length == 0)
                doh.AddFieldItem("FirstPage", Go2View(this.MainModule.Type, id, 1));
            else
                doh.AddFieldItem("FirstPage", this.txtAliasPage.Text);
            doh.Update("jcms_module_" + this.MainModule.Type);
            CreateContentFile(this.MainModule, id, -1);
            CreateCategoryFile(CategoryId, true);
            FinalMessage("成功保存", "close.htm", 0);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.txtTitle.Text = JumboECMS.Utils.Strings.SafetyStr(this.txtTitle.Text);
            if (this.txtSummary.Text.Length == 0)
                this.txtSummary.Text = GetCutString(JumboECMS.Utils.Strings.NoHTML(FCKeditor1.Value), 200).Trim();
            else
                this.txtSummary.Text = GetCutString(JumboECMS.Utils.Strings.HtmlEncode(this.txtSummary.Text), 200).Trim();
            //格式化标签
            this.txtTags.Text = JumboECMS.Utils.Strings.SafetyStr(this.txtTags.Text);
        }
    }
}

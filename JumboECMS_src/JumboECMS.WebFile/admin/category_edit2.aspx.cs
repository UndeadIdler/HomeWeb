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
    public partial class category_edit2 : JumboECMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string _lan = q("lan");
            id = Str2Str(q("id"));
            Admin_Load("class-mng", "stop");
            if (!Page.IsPostBack)
            {
                doh.Reset();
                doh.SqlCmd = "SELECT [ID],[Title],[code] FROM [" + base.CategoryTable + "] WHERE [LanguageCode]='" + _lan + "' AND  len(code)<16 and [TypeId]<2 ORDER BY code";
                DataTable dtCategory = doh.GetDataTable();
                for (int i = 0; i < dtCategory.Rows.Count; i++)
                {
                    ListItem li = new ListItem();
                    li.Value = dtCategory.Rows[i]["Id"].ToString();
                    li.Text = "├－" + getListName(dtCategory.Rows[i]["Title"].ToString(), dtCategory.Rows[i]["code"].ToString());
                    this.ddlParentId.Items.Add(li);
                }
                dtCategory.Clear();
                dtCategory.Dispose();
                doh.Reset();
                doh.SqlCmd = "SELECT [Type],[Name] FROM [jcms_normal_module] ORDER BY PId";
                DataTable dtModuleType = doh.GetDataTable();
                for (int i = 0; i < dtModuleType.Rows.Count; i++)
                {
                    ListItem li = new ListItem();
                    li.Value = dtModuleType.Rows[i]["Type"].ToString();
                    li.Text = dtModuleType.Rows[i]["Name"].ToString();
                    this.ddlModuleType.Items.Add(li);
                }
                dtModuleType.Clear();
                dtModuleType.Dispose();
                doh.Reset();
                doh.SqlCmd = "SELECT [ID],[Title] FROM [jcms_normal_template] WHERE [LanguageCode]='" + _lan + "' AND   sType='list' ORDER BY IsDefault desc";
                DataTable dtTemplate1 = doh.GetDataTable();
                for (int i = 0; i < dtTemplate1.Rows.Count; i++)
                {
                    ListItem li1 = new ListItem();
                    li1.Value = dtTemplate1.Rows[i]["Id"].ToString();
                    li1.Text = dtTemplate1.Rows[i]["Title"].ToString();
                    this.ddlTemplateId.Items.Add(li1);
                }
                dtTemplate1.Clear();
                dtTemplate1.Dispose();
                doh.Reset();
                doh.SqlCmd = "SELECT [ID],[Title] FROM [jcms_normal_template] WHERE [LanguageCode]='" + _lan + "' AND   sType='content' ORDER BY IsDefault desc";
                DataTable dtTemplate2 = doh.GetDataTable();
                this.ddlContentTemp.Items.Add(new ListItem("==请选择模板==", "0")); 
                for (int i = 0; i < dtTemplate2.Rows.Count; i++)
                {
                    ListItem li2 = new ListItem();
                    li2.Value = dtTemplate2.Rows[i]["Id"].ToString();
                    li2.Text = dtTemplate2.Rows[i]["Title"].ToString();
                    this.ddlContentTemp.Items.Add(li2);
                }
                dtTemplate2.Clear();
                dtTemplate2.Dispose();
            }
            JumboECMS.DBUtility.WebFormHandler wh = new JumboECMS.DBUtility.WebFormHandler(doh, base.CategoryTable, btnSave);
            wh.AddBind(txtTitle, "Title", true);
            wh.AddBind(txtSortRank, "SortRank", false);
            wh.AddBind(txtFolder, "Folder", true);
            wh.AddBind(ddlParentId, "ParentId", false);
            wh.AddBind(ddlModuleType, "ModuleType", true);
            wh.AddBind(txtInfo, "Info", true);
            wh.AddBind(ddlTemplateId, "TemplateId", false);
            wh.AddBind(ddlContentTemp, "ContentTemp", false);
            wh.AddBind(txtAliasPage, "AliasPage", true);
            if (id == "0")
            {
                wh.Mode = JumboECMS.DBUtility.OperationType.Add;
            }
            else
            {
                wh.ConditionExpress = "id=" + id;
                wh.Mode = JumboECMS.DBUtility.OperationType.Modify;
                this.ddlParentId.Enabled = false;
                this.ddlModuleType.Enabled = false;
                this.txtFolder.Enabled = false;
                this.btnPinYin.Visible = false;
                this.btnPinYin2.Visible = false;
            }
            wh.BindBeforeAddOk += new EventHandler(bind_ok);
            wh.BindBeforeModifyOk += new EventHandler(bind_ok);
            wh.AddOk += new EventHandler(add_ok);
            wh.ModifyOk += new EventHandler(save_ok);
            wh.validator = chkForm;
        }
        protected void bind_ok(object sender, EventArgs e)
        {
            if (id == "0")
            {
                int parentid = Str2Int(q("parentid"));
                if (parentid > 0)
                {
                    doh.Reset();
                    doh.ConditionExpress = "parentid=@parentid";
                    doh.AddConditionParameter("@parentid", Str2Int(q("parentid")));
                    int MaxSort = doh.MaxValue(base.CategoryTable, "SortRank");
                    this.txtSortRank.Text = (MaxSort + 1).ToString();
                }
                else
                {
                    this.txtSortRank.Text = "1";
                }
            }
            this.txtInfo.Text = JumboECMS.Utils.Strings.HtmlDecode(this.txtInfo.Text.Trim());
        }
        protected bool chkForm()
        {
            if (!CheckFormUrl())
                return false;
            if (!Page.IsValid)
                return false;
            if (id == "0")
            {
                doh.Reset();
                doh.SqlCmd = "SELECT Id FROM [" + base.CategoryTable + "] WHERE [ParentId]=" + this.ddlParentId.SelectedValue + " AND [Folder]='" + txtFolder.Text + "'";
                if (doh.GetDataTable().Rows.Count > 0)
                {
                    FinalMessage("目录名称重复", "", 1);
                    return false;
                }
            }
            return true;
        }
        protected void add_ok(object sender, EventArgs e)
        {
            JumboECMS.DBUtility.DbOperEventArgs de = (JumboECMS.DBUtility.DbOperEventArgs)e;
            id = de.id.ToString();
            string parentCode = string.Empty;
            string parentFilePath = string.Empty;
            string parentTopId = string.Empty;
            if (this.ddlParentId.SelectedValue != "0")
            {
                doh.Reset();
                doh.ConditionExpress = "id=@id";
                doh.AddConditionParameter("@id", this.ddlParentId.SelectedValue);
                object[] value = doh.GetFields(base.CategoryTable, "Code,FilePath,TopId");
                parentCode = value[0].ToString();
                parentFilePath = value[1].ToString();
                parentTopId = value[2].ToString();
            }
            string leftCode = string.Empty;
            string selfCode = string.Empty;
            string selfFilePath = string.Empty;
            doh.Reset();
            doh.SqlCmd = "SELECT [code],FilePath FROM [" + base.CategoryTable + "] WHERE left(code," + parentCode.Length + ")='" + parentCode + "' and len(code)=" + Convert.ToString(parentCode.Length + 4) + " ORDER BY code desc";
            DataTable dtCategory = doh.GetDataTable();
            if (dtCategory.Rows.Count > 0)
            {
                leftCode = dtCategory.Rows[0]["code"].ToString();
            }
            if (leftCode.Length > 0)
                selfCode = Convert.ToString(Convert.ToInt32(leftCode.Substring(leftCode.Length - 4, 4)) + 1).PadLeft(4, '0');
            else
                selfCode = "0001";
            selfCode = parentCode + selfCode;
            selfFilePath = parentFilePath + this.txtFolder.Text + "/";
            dtCategory.Clear();
            dtCategory.Dispose();

            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", id);
            doh.AddFieldItem("Code", selfCode);
            doh.AddFieldItem("TopId", ((selfCode.Length == 8) ? id : parentTopId));
            doh.AddFieldItem("LanguageCode", q("lan"));
            doh.AddFieldItem("FilePath", selfFilePath);
            doh.AddFieldItem("TypeId", 2);
            doh.Update(base.CategoryTable);

            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", id);
            if (this.txtAliasPage.Text.Length == 0)
                doh.AddFieldItem("FirstPage", site.Dir + selfFilePath + "index.html");
            else
                doh.AddFieldItem("FirstPage", this.txtAliasPage.Text);
            doh.Update(base.CategoryTable);
            FinalMessage("成功保存", "close.htm", 0);
        }
        protected void save_ok(object sender, EventArgs e)
        {
            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", id);
            if (this.txtAliasPage.Text.Length == 0)
                doh.AddFieldItem("FirstPage", Go2Category(id, 1));
            else
                doh.AddFieldItem("FirstPage", this.txtAliasPage.Text);
            doh.Update(base.CategoryTable);
            FinalMessage("成功保存", "close.htm", 0);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //格式化简介
            if (this.txtInfo.Text.Length != 0)
                this.txtInfo.Text = GetCutString(JumboECMS.Utils.Strings.HtmlEncode(JumboECMS.Utils.Strings.RemoveSpaceStr(this.txtInfo.Text)), 500).Trim();

        }
    }
}

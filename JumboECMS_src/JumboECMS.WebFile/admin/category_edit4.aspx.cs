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
    public partial class category_edit4 : JumboECMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string _lan = q("lan");
            id = Str2Str(q("id"));
            Admin_Load("class-mng", "stop");
            if (!Page.IsPostBack)
            {
                doh.Reset();
                doh.SqlCmd = "SELECT [ID],[Title],[code] FROM [" + base.CategoryTable + "] WHERE [LanguageCode]='" + _lan + "' AND len(code)<16 and [TypeId]<2 ORDER BY code";
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
            }
            JumboECMS.DBUtility.WebFormHandler wh = new JumboECMS.DBUtility.WebFormHandler(doh, base.CategoryTable, btnSave);
            wh.AddBind(txtTitle, "Title", true);
            wh.AddBind(txtSortRank, "SortRank", false);
            wh.AddBind(ddlParentId, "ParentId", false);
            wh.AddBind(txtAliasPage, "AliasPage", true);
            wh.AddBind(rblTarget, "SelectedValue", "Target", true);

            if (id == "0")
            {
                wh.Mode = JumboECMS.DBUtility.OperationType.Add;
            }
            else
            {
                wh.ConditionExpress = "id=" + id;
                wh.Mode = JumboECMS.DBUtility.OperationType.Modify;
                this.ddlParentId.Enabled = false;
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
        }
        protected bool chkForm()
        {
            if (!CheckFormUrl())
                return false;
            if (!Page.IsValid)
                return false;
            return true;
        }
        protected void add_ok(object sender, EventArgs e)
        {
            JumboECMS.DBUtility.DbOperEventArgs de = (JumboECMS.DBUtility.DbOperEventArgs)e;
            id = de.id.ToString();
            string parentCode = string.Empty;
            string parentTopId = string.Empty;
            if (this.ddlParentId.SelectedValue != "0")
            {
                doh.Reset();
                doh.ConditionExpress = "id=@id";
                doh.AddConditionParameter("@id", this.ddlParentId.SelectedValue);
                object[] value = doh.GetFields(base.CategoryTable, "Code,TopId");
                parentCode = value[0].ToString();
                parentTopId = value[1].ToString();
            }
            string leftCode = string.Empty;
            string selfCode = string.Empty;
            doh.Reset();
            doh.SqlCmd = "SELECT [code] FROM [" + base.CategoryTable + "] WHERE left(code," + parentCode.Length + ")='" + parentCode + "' and len(code)=" + Convert.ToString(parentCode.Length + 4) + " ORDER BY code desc";
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
            dtCategory.Clear();
            dtCategory.Dispose();

            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", id);
            doh.AddFieldItem("Code", selfCode);
            doh.AddFieldItem("TopId", ((selfCode.Length == 8) ? id : parentTopId));
            doh.AddFieldItem("LanguageCode", q("lan"));
            doh.AddFieldItem("TypeId", 4);
            doh.AddFieldItem("FirstPage", this.txtAliasPage.Text);
            doh.Update(base.CategoryTable);
            FinalMessage("成功保存", "close.htm", 0);
        }
        protected void save_ok(object sender, EventArgs e)
        {
            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", id);
            doh.AddFieldItem("FirstPage", this.txtAliasPage.Text);
            doh.Update(base.CategoryTable);
            FinalMessage("成功保存", "close.htm", 0);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
        }
    }
}

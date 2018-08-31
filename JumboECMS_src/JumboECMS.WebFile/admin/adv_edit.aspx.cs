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
    public partial class _adv_edit : JumboECMS.UI.AdminCenter
    {
        public string type = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Str2Str(q("id"));
            type= q("type");
                Admin_Load("adv-mng", "stop");
            if (!Page.IsPostBack)
            {
                doh.Reset();
                doh.SqlCmd = "SELECT [ID],[Title],[code] FROM [jcms_normal_advclass] ORDER BY id asc";
                DataTable dtClass = doh.GetDataTable();
                if (dtClass.Rows.Count == 0)
                {
                    dtClass.Clear();
                    dtClass.Dispose();
                    Response.Write("请先添加分类!");
                    Response.End();
                }
                for (int i = 0; i < dtClass.Rows.Count; i++)
                {
                    this.ddlAdvType.Items.Add(new ListItem(dtClass.Rows[i]["Title"].ToString(), dtClass.Rows[i]["Code"].ToString()));
                }
                dtClass.Clear();
                dtClass.Dispose();
                if (type != "") this.ddlAdvType.SelectedValue = type;
            }
            doh.Reset();
            JumboECMS.DBUtility.WebFormHandler wh = new JumboECMS.DBUtility.WebFormHandler(doh, "jcms_normal_adv", btnSave);
            wh.AddBind(txtTitle, "Title", true);
            wh.AddBind(ddlAdvType, "AdvType", false);
            wh.AddBind(txtUrl, "Url", true);
            wh.AddBind(txtPicurl, "Picurl", true);
            wh.AddBind(rbtState, "SelectedValue", "State", false);
            wh.AddBind(txtContent, "Content", true);
            wh.AddBind(txtAddDate, "AddDate", true);
            wh.AddBind(txtWidth, "Width", false);
            wh.AddBind(txtHeight, "Height", false);
            if (id == "0")
            {
                wh.Mode = JumboECMS.DBUtility.OperationType.Add;
            }
            else
            {
                wh.ConditionExpress = "id=" + id;
                //this.ddlAdvType.Enabled = false;
                wh.Mode = JumboECMS.DBUtility.OperationType.Modify;
            }
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
            this.txtTitle.Text = JumboECMS.Utils.Strings.HtmlDecode(this.txtTitle.Text);
            if (id == "0")
                this.txtAddDate.Text = DateTime.Now.ToString();
        }
        protected bool chkForm()
        {
            if (!CheckFormUrl())
                return false;
            if (!Page.IsValid)
                return false;
            return true;
        }
        protected void save_ok(object sender, EventArgs e)
        {
            if (id == "0")
                FinalMessage("成功保存", "adv_list.aspx?type=" + type, 0);
            else
                FinalMessage("成功保存", "adv_list.aspx?type=" + type, 0);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.txtTitle.Text = JumboECMS.Utils.Strings.HtmlEncode(JumboECMS.Utils.Strings.SafetyStr(this.txtTitle.Text));
        }
    }
}

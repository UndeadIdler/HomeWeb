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
using System.Web.UI.WebControls;
namespace JumboECMS.WebFile.Admin
{
    public partial class _link_edit : JumboECMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("link-mng", "stop");
            if (!Page.IsPostBack)
            {
                doh.Reset();
                doh.SqlCmd = "SELECT [ID],[Title] FROM [jcms_normal_linkclass] ORDER BY id asc";
                DataTable dtClass = doh.GetDataTable();
                for (int i = 0; i < dtClass.Rows.Count; i++)
                {
                    this.ddlTypeId.Items.Add(new ListItem(dtClass.Rows[i]["Title"].ToString(), dtClass.Rows[i]["Id"].ToString()));
                }
                dtClass.Clear();
                dtClass.Dispose();
            }
            id = Str2Str(q("id"));
            doh.Reset();
            JumboECMS.DBUtility.WebFormHandler wh = new JumboECMS.DBUtility.WebFormHandler(doh, "jcms_normal_link", btnSave);
            wh.AddBind(txtTitle, "Title", true);
            wh.AddBind(txtUrl, "Url", true);
            wh.AddBind(txtImg, "ImgPath", true);
            wh.AddBind(txtInfo, "Info", true);
            wh.AddBind(txtOrderNum, "OrderNum", false);
            wh.AddBind(ddlTypeId, "TypeId", false);
            wh.AddBind(rblState, "SelectedValue", "State", false);
            wh.AddBind(rblStyle, "SelectedValue", "Style", false);
            if (id != "0")
            {
                wh.ConditionExpress = "id=" + id.ToString();
                wh.Mode = JumboECMS.DBUtility.OperationType.Modify;
            }
            else
                wh.Mode = JumboECMS.DBUtility.OperationType.Add;
            wh.AddOk += new EventHandler(save_ok);
            wh.ModifyOk += new EventHandler(save_ok);
            wh.validator = chkForm;
        }
        protected bool chkForm()
        {
            if (!CheckFormUrl())
                return false;
            if (!Page.IsValid)
                return false;
            if (this.ddlTypeId.SelectedValue == "1" && this.txtImg.Text == "")
            {
                FinalMessage("请输入网站LOGO", "", 1);
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
                doh.AddFieldItem("Style", "1");
            else
                doh.AddFieldItem("Style", "0");
            doh.Update("jcms_normal_link");
            FinalMessage("链接成功保存", site.Dir + "admin/close.htm", 0);
        }
    }
}

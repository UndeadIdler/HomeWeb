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
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using JumboECMS.Common;
namespace JumboECMS.WebFile.Admin
{
    public partial class _admin_editpower : JumboECMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "stop");
            id = Str2Str(q("id"));
            string[] menu = powerMenu();
            hfMasterSettingId.Value = id;
            doh.Reset();
            doh.ConditionExpress = "id=" + id;
            string admin_power = doh.GetField("jcms_normal_user", "Setting").ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append("<table cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" class=\"formtable\">");
            doh.Reset();
            doh.SqlCmd = "SELECT [Type],[Name] FROM [jcms_normal_module] ORDER BY pId";
            DataTable dtModule = doh.GetDataTable();
            string mModuleType = string.Empty;
            string mModuleName = string.Empty;
            for (int i = 0; i < dtModule.Rows.Count; i++)
            {
                mModuleType = dtModule.Rows[i]["Type"].ToString();
                mModuleName = dtModule.Rows[i]["Name"].ToString();
                sb.Append("<tr><th>" + mModuleName + "</th>");
                sb.Append("<td>");
                for (int j = 0; j < menu.GetLength(0); j++)
                {
                    if (menu[j] == null)
                        break;
                    if (!menu[j].Contains("|"))
                        break;
                    string[] _menu = menu[j].Split('|');
                    sb.Append("<span style='margin-left:10px;padding-top:10px;'><input type=checkbox class='checkbox' name=\"admin_power\" value=\"");
                    string tPower = string.Empty;
                    tPower = mModuleType + "-" + _menu[1];
                    sb.Append(tPower + "\"");
                    if (admin_power.Contains("," + tPower + ","))
                        sb.Append(" checked");
                    sb.Append(">." + _menu[0] + "</span>\r\n");
                    if ((j % 5 == 0) && (j > 0))
                        sb.Append("<br /><br />");
                }
                sb.Append("</td></tr>");
            }
            dtModule.Clear();
            dtModule.Dispose();
            sb.Append("<tr><th>其他管理</th>");
            sb.Append("<td>");

            doh.Reset();
            doh.SqlCmd = "SELECT * FROM [jcms_normal_adminpower] WHERE [Enabled]=1 ORDER BY pId";
            DataTable dtPower = doh.GetDataTable();
            for (int i = 0; i < dtPower.Rows.Count; i++)
            {
                string PowerName = dtPower.Rows[i]["Title"].ToString();
                string PowerCode = dtPower.Rows[i]["Code"].ToString();
                sb.Append("<span style='margin-left:10px;padding-top:10px;'><input type=checkbox class='checkbox' name=\"admin_power\" value=\"" + PowerCode + "\"");
                if (admin_power.Contains("," + PowerCode + ","))
                    sb.Append(" checked");
                sb.Append("> " + PowerName + "</span>");
            }
            dtPower.Clear();
            dtPower.Dispose();

            sb.Append("</td></tr>");
            sb.Append("</table>");
            this.ltMasterSetting.Text = sb.ToString();
        }
        protected void btnSaveSetting_Click(object sender, EventArgs e)
        {
            string admin_power = ",";
            if (Request.Form["admin_power"] != null)
                admin_power = "," + Request.Form["admin_power"].ToString() + ",";
            id = hfMasterSettingId.Value.ToString();
            if (id == "0" || !JumboECMS.Utils.Validator.IsNumeric(id))
                FinalMessage("参数错误,请重新操作!", "", 1);
            else
            {
                doh.Reset();
                doh.ConditionExpress = "id=" + id;
                doh.AddFieldItem("Setting", admin_power);
                doh.Update("jcms_normal_user");
            }
            FinalMessage("正确保存!", "close.htm", 0);
        }
    }
}

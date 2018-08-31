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
namespace JumboECMS.WebFile.Admin
{
    public partial class _executesql_ajax : JumboECMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckFormUrl())
            {
                Response.End();
            }
            Admin_Load("sql-mng", "json");
            this._operType = q("oper");
            switch (this._operType)
            {
                case "executesql":
                    ajaxExecuteSql();
                    break;
                default:
                    DefaultResponse();
                    break;
            }
            Response.Write(this._response);
        }

        private void DefaultResponse()
        {
            this._response = JsonResult(0, "未知操作");
        }
        private void ajaxExecuteSql()
        {
            int _canExecuteSQL = 0;
            if (Const.GetUserIp == Request.ServerVariables["LOCAl_ADDR"])
                _canExecuteSQL = 1;
            else
                _canExecuteSQL = Str2Int(JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "ExecuteSql"));
            if (_canExecuteSQL == 1)
            {
                string _SQLContent = f("txtSQLContent");
                if (_SQLContent.Length == 0)
                {
                    this._response = "top.JumboECMS.Alert('脚本有误', '0');";
                    return;
                }
                string _tmpFile = site.Dir + "_data/sql/" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".sql";
                JumboECMS.Utils.DirFile.SaveFile(_SQLContent, _tmpFile);
                if (ExecuteSqlInFile(Server.MapPath(_tmpFile)))
                    this._response = "top.JumboECMS.Message('脚本执行成功', '1');";
                else
                    this._response = "top.JumboECMS.Alert('脚本执行错误', '0');";
            }
            else
                this._response = "top.JumboECMS.Alert('客户端" + Const.GetUserIp + "不可执行服务器" + Request.ServerVariables["LOCAl_ADDR"] + "SQL', '0');";
        }
    }
}
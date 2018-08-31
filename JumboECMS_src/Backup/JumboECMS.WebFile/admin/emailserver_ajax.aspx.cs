/*
 * ������������: �������ݹ���ϵͳ��ҵ��
 * 
 * ����Ӣ������: JumboECMS
 * 
 * ����汾: 1.4.x
 * 
 * ��������: ����
 * 
 * �ٷ���վ: http://www.jumboecms.net/
 * 
 */

using System;
using System.Data;
using System.Web;
using JumboECMS.Utils;
using JumboECMS.Common;
namespace JumboECMS.WebFile.Admin
{
    public partial class _emailserver_ajax : JumboECMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckFormUrl())
            {
                Response.End();
            }
            Admin_Load("0001", "json");
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxGetList":
                    ajaxGetList();
                    break;
                case "ajaxDelete":
                    ajaxDelete();
                    break;
                case "checkname":
                    ajaxCheckName();
                    break;
                case "ajaxEmailServerExport":
                    ajaxEmailServerExport();
                    break;
                case "ajaxEmailServerImport":
                    ajaxEmailServerImport();
                    break;
                default:
                    DefaultResponse();
                    break;
            }
            Response.Write(this._response);
        }

        private void DefaultResponse()
        {
            this._response = "{\"result\" :\"0\",\"returnval\" :\"δ֪����\"}";
        }
        private void ajaxCheckName()
        {
            if (q("id") == "0")
            {
                doh.Reset();
                doh.ConditionExpress = "fromaddress=@fromaddress";
                doh.AddConditionParameter("@fromaddress", q("txtFromAddress"));
                if (doh.Exist("jcms_normal_emailserver"))
                    this._response = "{\"result\" :\"0\",\"returnval\" :\"�������\"}";
                else
                    this._response = "{\"result\" :\"1\",\"returnval\" :\"�������\"}";
            }
            else
            {
                doh.Reset();
                doh.ConditionExpress = "fromaddress=@fromaddress and id<>" + q("id");
                doh.AddConditionParameter("@fromaddress", q("txtFromAddress"));
                if (doh.Exist("jcms_normal_emailserver"))
                    this._response = "{\"result\" :\"0\",\"returnval\" :\"�����޸�\"}";
                else
                    this._response = "{\"result\" :\"1\",\"returnval\" :\"�����޸�\"}";
            }
        }
        private void ajaxGetList()
        {
            doh.Reset();
            doh.SqlCmd = "Select [ID],[fromaddress],[SmtpHost],[Enabled] FROM [jcms_normal_emailserver] ORDER BY id desc";
            DataTable dt = doh.GetDataTable();
            this._response = "{\"result\" :\"1\",\"returnval\" :\"�����ɹ�\"," + JumboECMS.Utils.dtHelp.DT2JSON(dt) + "}";
        }
        private void ajaxDelete()
        {
            string sId = f("id");
            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", sId);
            doh.Delete("jcms_normal_emailserver");
            new JumboECMS.DAL.Normal_UserMailDAL().ExportEmailServer();
            this._response = "{\"result\" :\"1\",\"returnval\" :\"�ɹ�ɾ��\"}";
        }
        private void ajaxEmailServerExport()
        {
            if (new JumboECMS.DAL.Normal_UserMailDAL().ExportEmailServer())
                this._response = "{\"result\" :\"1\",\"returnval\" :\"�����ɹ�\"}";
            else
                this._response = "{\"result\" :\"0\",\"returnval\" :\"����ʧ��\"}";
        }
        private void ajaxEmailServerImport()
        {
            if (new JumboECMS.DAL.Normal_UserMailDAL().ImportEmailServer())
                this._response = "{\"result\" :\"1\",\"returnval\" :\"����ɹ�\"}";
            else
                this._response = "{\"result\" :\"0\",\"returnval\" :\"����ʧ��\"}";
        }
    }
}
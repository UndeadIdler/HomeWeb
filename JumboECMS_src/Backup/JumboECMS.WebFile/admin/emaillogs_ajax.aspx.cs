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
    public partial class _emaillogs_ajax : JumboECMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckFormUrl())
            {
                Response.End();
            }
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxGetList":
                    ajaxGetList();
                    break;
                case "clear":
                    ajaxClear();
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
        private void ajaxGetList()
        {
            Admin_Load("0001", "json");
            string keys = q("keys");
            int mId = Str2Int(q("mId"), 0);
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 20);
            string joinStr = "A.[AdminId]=B.[AdminId]";
            string whereStr1 = "A.Id>0";//��Χ����(��A.)
            string whereStr2 = "Id>0";//��ҳ����(����A.)
            string jsonStr = "";
            if (keys.Trim().Length > 0)
            {
                whereStr1 += " and A.SendTitle LIKE '%" + keys + "%'";
                whereStr2 += " and SendTitle LIKE '%" + keys + "%'";
            }
            if (mId > 0)
            {
                whereStr1 += " and a.[AdminId]=" + mId.ToString();
                whereStr2 += " and [AdminId]=" + mId.ToString();
            }
            new JumboECMS.DAL.Normal_EmaillogsDAL().GetListJSON(page, PSize, joinStr, whereStr1, whereStr2, ref jsonStr);
            this._response = jsonStr;
        }
        private void ajaxClear()
        {
            Admin_Load("master", "json");
            new JumboECMS.DAL.Normal_EmaillogsDAL().DeleteLogs();
            this._response = JsonResult(1, "�ɹ����");
        }
    }
}
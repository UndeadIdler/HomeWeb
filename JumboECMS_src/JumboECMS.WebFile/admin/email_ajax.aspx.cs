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
    public partial class _email_ajax : JumboECMS.UI.AdminCenter
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
                case "ajaxEmailInfo":
                    GetEmailInfo();
                    break;
                case "ajaxBatchOper":
                    ajaxBatchOper();
                    break;
                case "ajaxDelete":
                    ajaxDelete();
                    break;
                case "checkemail":
                    ajaxCheckEmail();
                    break;
                case "ajaxTreeJson":
                    ajaxTreeJson();
                    break;
                case "batchimport":
                    ajaxBatchImport();
                    break;
                default:
                    DefaultResponse();
                    break;
            }
            Response.Write(this._response);
        }

        private void ajaxCheckEmail()
        {
            if (q("id") == "0")
            {
                doh.Reset();
                doh.ConditionExpress = "emailaddress=@emailaddress";
                doh.AddConditionParameter("@emailaddress", q("txtEmailAddress"));
                if (doh.Exist("jcms_normal_email"))
                    this._response = "{\"result\" :\"0\",\"returnval\" :\"�������\"}";
                else
                    this._response = "{\"result\" :\"1\",\"returnval\" :\"�������\"}";
            }
            else
            {
                doh.Reset();
                doh.ConditionExpress = "emailaddress=@emailaddress and id<>" + q("id");
                doh.AddConditionParameter("@emailaddress", q("txtEmailAddress"));
                if (doh.Exist("jcms_normal_email"))
                    this._response = "{\"result\" :\"0\",\"returnval\" :\"�����޸�\"}";
                else
                    this._response = "{\"result\" :\"1\",\"returnval\" :\"�����޸�\"}";
            }
        }
        private void DefaultResponse()
        {
            this._response = "{\"result\" :\"0\",\"returnval\" :\"δ֪����\"}";
        }
        private void ajaxGetList()
        {
            string keys = q("keys");
            int gId = Str2Int(q("gId"), 0);
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 20);
            int countNum = 0;
            string sqlStr = "";
            string joinStr = "A.[GroupId]=B.Id";
            string whereStr1 = "A.Id>0";//��Χ����(��A.)
            string whereStr2 = "Id>0";//��ҳ����(����A.)
            if (keys.Trim().Length > 0)
            {
                whereStr1 += " and A.EmailAddress LIKE '%" + keys + "%'";
                whereStr2 += " and EmailAddress LIKE '%" + keys + "%'";
            }
            if (gId > 0)
            {
                whereStr1 += " and a.[GroupId]=" + gId.ToString();
                whereStr2 += " and [GroupId]=" + gId.ToString();
            }
            doh.Reset();
            doh.ConditionExpress = whereStr2;
            countNum = doh.Count("jcms_normal_email");
            sqlStr = JumboECMS.Utils.SqlHelp.GetSql0("A.id as id,A.NickName as NickName,A.EmailAddress as EmailAddress,B.GroupName as GroupName,A.state as state", "jcms_normal_email", "jcms_normal_emailgroup", "Id", PSize, page, "desc", joinStr, whereStr1, whereStr2);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            this._response = "{\"result\" :\"1\"," +
                "\"returnval\" :\"�����ɹ�\"," +
                "\"pagerbar\" :\"" + JumboECMS.Utils.HtmlPager.GetPageBar(3, "js", 2, countNum, PSize, page, "javascript:ajaxList(*);") + "\"," +
                JumboECMS.Utils.dtHelp.DT2JSON(dt) +
                "}";
            dt.Clear();
            dt.Dispose();
        }
        /// <summary>
        /// ���νṹ��JSON
        /// </summary>
        private void ajaxTreeJson()
        {
            if (Str2Str(f("id")) == "0")
                this._response = getJson("0", "0", false);
            else
                this._response = getJson(Str2Str(f("id")), Str2Str(q("eid")), true);
        }
        // bool child(�Ƿ����ӽڵ㣬 trueΪ���ڵ㣬falseΪ�ӽڵ�)
        private string getJson(string id, string eid, bool child)
        {
            string json = "";
            if (!child)
            {
                doh.Reset();
                doh.SqlCmd = "Select [ID],[GroupName] FROM [jcms_normal_emailgroup] ORDER BY id asc";
                DataTable dt = doh.GetDataTable();
                if (dt.Rows.Count == 0)
                    json = "[]";
                else
                {
                    json = "[";
                    foreach (DataRow item in dt.Rows)
                    {
                        json += "{";
                        json += string.Format("\"id\": \"{0}\", \"text\": \"{1}\", \"value\": \"{2}\", \"showcheck\": true, complete: false, \"isexpand\": false, \"checkstate\": 0, \"hasChildren\": true, \"ChildNodes\":{3}",
                            item["id"].ToString(), item["GroupName"].ToString(), item["id"].ToString(), "[]");
                        json += "},";
                    }
                    json = json.Substring(0, json.Length - 1);
                    json += "]";
                }
            }
            else
            {

                doh.Reset();
                doh.SqlCmd = string.Format("Select TOP 200 [ID],[NickName],[EmailAddress] FROM [jcms_normal_email] WHERE GroupId={0} AND State=1 AND [EzineId]<{1} ORDER BY id asc", id, eid);
                DataTable dt = doh.GetDataTable();
                if (dt.Rows.Count == 0)
                    json = "[]";
                else
                {
                    json = "[";
                    foreach (DataRow item in dt.Rows)
                    {
                        json += "{";
                        json += string.Format("\"id\": \"e{0}\", \"text\": \"{1}\", \"value\": \"{2}\", \"showcheck\": true, \"isexpand\": false, \"checkstate\": 0, \"hasChildren\": false, \"ChildNodes\": null, \"complete\": false",
                            item["id"].ToString(), item["NickName"].ToString(), item["EmailAddress"].ToString());
                        json += "},";
                    }
                    json = json.Substring(0, json.Length - 1);
                    json += "]";
                }
            }
            return json;
        }

        private void GetEmailInfo()
        {
            string _emailid = Str2Str(q("id"));
            int page = 1;
            int PSize = 1;
            int countNum = 0;
            string sqlStr = "";
            string joinStr = "A.[Group]=B.Id";
            string whereStr1 = "A.Id=" + _emailid;
            string whereStr2 = "Id=" + _emailid;
            doh.Reset();
            doh.ConditionExpress = whereStr2;
            countNum = doh.Count("jcms_normal_email");
            sqlStr = JumboECMS.Utils.SqlHelp.GetSql0("A.*,B.GroupName", "jcms_normal_email", "jcms_normal_emailgroup", "Id", PSize, page, "desc", joinStr, whereStr1, whereStr2);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            this._response = "{\"result\" :\"" + countNum + "\"," + JumboECMS.Utils.dtHelp.DT2JSON(dt) + "}";
            dt.Clear();
            dt.Dispose();
        }
        private void ajaxDelete()
        {
            string uId = f("id");
            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", uId);
            int _delCount = doh.Delete("jcms_normal_email");
            if (_delCount > 0)
                this._response = "{\"result\" :\"1\",\"returnval\" :\"ɾ���ɹ�\"}";
            else
                this._response = "{\"result\" :\"0\",\"returnval\" :\"ɾ��ʧ��\"}";
        }
        /// <summary>
        /// ִ����������
        /// </summary>
        /// <param name="oper"></param>
        /// <param name="ids"></param>
        private void ajaxBatchOper()
        {
            string act = q("act");
            string togid = f("togid");
            string ids = f("ids");
            BatchEmail(act, togid, ids, "json");
            this._response = "{\"result\" :\"1\",\"returnval\" :\"�����ɹ�\"}";
        }
        /// <summary>
        /// ִ����������,������ת�ƵȲ���
        /// </summary>
        /// <param name="_act">��������{pass=���,nopass=δ��,move2group=ת��������}</param>
        /// <param name="_ids">id�ַ���,��","��������</param>
        /// <param name="pageType">ҳ���Ϊhtml��json</param>
        public void BatchEmail(string _act, string _togid, string _ids, string pageType)
        {
            string[] idValue;
            idValue = _ids.Split(',');
            if (_act == "pass")
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=" + idValue[i];
                    doh.AddFieldItem("State", 1);
                    doh.Update("jcms_normal_email");
                }
                return;
            }
            if (_act == "nopass")
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=" + idValue[i];
                    doh.AddFieldItem("State", 0);
                    doh.Update("jcms_normal_email");
                }
                return;
            }
            if (_act == "move2group")
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=" + idValue[i];
                    doh.AddFieldItem("GroupId", _togid);
                    doh.Update("jcms_normal_email");
                }
                return;
            }
            if (_act == "del")
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=" + idValue[i];
                    doh.Delete("jcms_normal_email");
                }
                return;
            }
        }
        private void ajaxBatchImport()
        {
            Server.ScriptTimeout = 999;
            string _mails = f("mails");
            if (_mails.Length == 0)
            {
                this._response = "{\"result\" :\"0\",\"returnval\" :\"��������ϵ��\"}";
                return;
            }
            string[] _mail = _mails.Split(';');
            int _success = 0;
            for (int j = 0; j < _mail.Length; j++)
            {
                if (_mail[j].Contains(","))
                {
                    doh.Reset();
                    doh.ConditionExpress = "emailaddress=@emailaddress";
                    doh.AddConditionParameter("@emailaddress", _mail[j].Split(',')[1]);
                    if (!doh.Exist("jcms_normal_email"))
                    {
                        doh.Reset();
                        doh.AddFieldItem("NickName", _mail[j].Split(',')[0]);
                        doh.AddFieldItem("EmailAddress", _mail[j].Split(',')[1]);
                        doh.AddFieldItem("State", 1);
                        doh.AddFieldItem("GroupId", 1);
                        doh.Insert("jcms_normal_email");
                        _success++;
                    }
                }
            }
            this._response = "{\"result\" :\"1\",\"returnval\" :\"" + _success + "\"}";
        }
    }
}
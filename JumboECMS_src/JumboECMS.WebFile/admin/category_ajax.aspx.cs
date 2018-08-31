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
    public partial class category_ajax : JumboECMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Str2Str(q("id"));
            Admin_Load("class-mng", "json");
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxGetList":
                    ajaxGetList();
                    break;
                case "ajaxDelete":
                    ajaxDelete();
                    break;
                case "move":
                    ajaxMove();
                    break;
                case "checkname":
                    ajaxCheckName();
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
        private void ajaxCheckName()
        {
            this._response = JsonResult(1, "可以重复");
        }
        private void ajaxGetList()
        {
            string _lan = q("lan");
            doh.Reset();
            doh.SqlCmd = "SELECT [ID],[Title],[Code],[SortRank],len(Code) as codelength,[IsTop],[TypeId],[FirstPage],(select title from [jcms_normal_categorytype] where id=[" + base.CategoryTable + "].TypeId) as typename,[topicnum],(select title from [jcms_normal_template] where id=[" + base.CategoryTable + "].TemplateId) as templatename,(select title from [jcms_normal_template] where id=[" + base.CategoryTable + "].ContentTemp) as ContentTempName,(select name from [jcms_normal_module] where [Type]=[" + base.CategoryTable + "].moduletype) as modulename FROM [" + base.CategoryTable + "] WHERE [LanguageCode]='" + _lan + "' AND len(code)>4 ORDER BY code";
            DataTable dt = doh.GetDataTable();
            this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + JumboECMS.Utils.dtHelp.DT2JSON(dt) + "}";
        }
        private void ajaxDelete()
        {
            string cId = f("id");
            bool delError = false;//允许删
            doh.Reset();
            doh.ConditionExpress = "id=" + cId;
            object[] _value = doh.GetFields(base.CategoryTable, "code,ModuleType,TypeId,TemplateId");
            string cCode = _value[0].ToString();
            string cModuleType = _value[1].ToString();
            string cTypeId = _value[2].ToString();
            string cTemplateId = _value[3].ToString();
            if (cTypeId == "1")//如果是父级栏目得看有没有子栏目
            {
                doh.Reset();
                doh.ConditionExpress = "[ParentId]=" + cId;
                delError = (doh.Exist(base.CategoryTable));
                if (delError)
                {
                    this._response = JsonResult(0, "含有子栏目,不可删");
                    return;
                }
            }
            if (cTypeId == "2")//如果是终极栏目得看有没有内容
            {
                doh.Reset();
                doh.ConditionExpress = "[CategoryId]=" + cId;
                delError = (doh.Exist("jcms_module_" + cModuleType));
                if (delError)
                {
                    this._response = JsonResult(0, "含有内容,不可删");
                    return;
                }
            }
            doh.Reset();
            doh.ConditionExpress = "id=" + cId;
            doh.Delete(base.CategoryTable);
            this._response = JsonResult(1, "成功删除");
        }
        private void ajaxMove()
        {
            string id = f("id");
            string isUp = f("up");
            if (id == "0")
            {
                this._response = JsonResult(0, "ID错误");
                return;
            }
            doh.Reset();
            doh.ConditionExpress = " id=" + id;
            string oldCode = doh.GetField(base.CategoryTable, "code").ToString();
            int codeLen = oldCode.Length;
            string subStr = DBType == "0" ? "mid" : "substring";
            if (codeLen > 1)
            {
                string temp = string.Empty;
                string wStr = "";
                string wStr2 = "";
                for (int i = 0; i < codeLen; i++)
                    wStr2 += "-";
                if (codeLen > 4)
                    wStr = " and left(code," + Convert.ToString(codeLen - 4) + ")='" + oldCode.Substring(0, codeLen - 4) + "'";

                if (isUp == "1")
                    wStr = "SELECT TOP 1 code FROM [" + base.CategoryTable + "] WHERE len(code)=" + codeLen.ToString() + " and code<'" + oldCode + "'" + wStr + " ORDER BY code desc";
                else
                    wStr = "SELECT TOP 1 code FROM [" + base.CategoryTable + "] WHERE len(code)=" + codeLen.ToString() + " and code>'" + oldCode + "'" + wStr + " ORDER BY code asc";
                doh.Reset();
                doh.SqlCmd = wStr;
                DataTable dtCategory = doh.GetDataTable();
                if (dtCategory.Rows.Count > 0)
                    temp = dtCategory.Rows[0]["code"].ToString();

                if (temp.Length > 1)
                {
                    //Move Under Class
                    wStr = "UPDATE [" + base.CategoryTable + "] SET [code]='" + wStr2 + "'+" + subStr + "(code," + Convert.ToString(codeLen + 1) + ",len(code)) where left(code," + codeLen.ToString() + ")='" + temp + "'";
                    doh.Reset();
                    doh.SqlCmd = wStr;
                    doh.ExecuteSqlNonQuery();

                    //Update Target Class
                    wStr = "UPDATE [" + base.CategoryTable + "] SET [code]='" + temp + "'+" + subStr + "(code," + Convert.ToString(codeLen + 1) + ",len(code)) where left(code," + codeLen.ToString() + ")='" + oldCode + "'";
                    doh.Reset();
                    doh.SqlCmd = wStr;
                    doh.ExecuteSqlNonQuery();

                    //Update Under Class
                    wStr = "UPDATE [" + base.CategoryTable + "] SET [code]='" + oldCode + "'+" + subStr + "(code," + Convert.ToString(wStr2.Length + 1) + ",len(code)) where left(code," + wStr2.Length.ToString() + ")='" + wStr2 + "'";
                    doh.Reset();
                    doh.SqlCmd = wStr;
                    doh.ExecuteSqlNonQuery();

                }
                dtCategory.Clear();
                dtCategory.Dispose();
            }
            this._response = JsonResult(1, "成功移动");
        }

    }
}
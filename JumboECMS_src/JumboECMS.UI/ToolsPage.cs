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
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
namespace JumboECMS.UI
{
    public class ToolsPage : BasicPage
    {
        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }
        public bool CheckCookiesCode()
        {
            string _code = q("code");
            return JumboECMS.Common.ValidateCode.CheckValidateCode(_code);
        }
        /// <summary>
        /// 没有查询结果
        /// </summary>
        /// <returns></returns>
        public string NoResult()
        {
            return "<div class=\"load-box\"><span class=\"ico-info\"></span>没有相关的查询结果</div>";
        }
        public string ErrInfo(string _info)
        {
            return "<div class=\"load-box\"><span class=\"ico-info\"></span>" + _info + "</div>";
        }
        public void BindTrainType(JumboECMS.DBUtility.DbOperHandler _doh, System.Web.UI.WebControls.DropDownList ddlTrainType)
        {
            _doh.Reset();
            _doh.SqlCmd = "Select TrainType From [T_TrainType] ORDER BY id asc";
            DataTable dt = _doh.GetDataTable();
            ddlTrainType.Items.Clear();
            ddlTrainType.Items.Add(new ListItem("所有车次", "0"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddlTrainType.Items.Add(new ListItem(dt.Rows[i]["TrainType"].ToString(), dt.Rows[i]["TrainType"].ToString()));
            }
            dt.Clear();
            dt.Dispose();
        }
        /// <summary>
        /// 热门路线查询
        /// </summary>
        /// <param name="_doh"></param>
        /// <returns></returns>
        public string Hot_LuXian(JumboECMS.DBUtility.DbOperHandler _doh)
        {
            string _Str = "";
            _doh.Reset();
            _doh.SqlCmd = "select top 30 ShiFa,Zhongdian from [T_TrainInfo] ORDER BY SearchNum DESC,ShiFa asc,Zhongdian asc";
            DataTable dt = _doh.GetDataTable();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                _Str += "<li><a href=\"javascript:void(0);\" onclick=\"ShowStation2Station('" + dt.Rows[i]["ShiFa"].ToString().ToUpper() + "','" + dt.Rows[i]["Zhongdian"].ToString().ToUpper() + "');return false;\">" + dt.Rows[i]["ShiFa"].ToString() + " - " + dt.Rows[i]["ZhongDian"].ToString() + "</a></li>";
            }
            dt.Clear();
            dt.Dispose();
            return _Str;
        }
        /// <summary>
        /// 热门车次查询
        /// </summary>
        /// <param name="_doh"></param>
        /// <returns></returns>
        public string Hot_CheCi(JumboECMS.DBUtility.DbOperHandler _doh)
        {
            string _Str = "";
            _doh.Reset();
            _doh.SqlCmd = "select top 30 lineID from [T_LineInfo] where [seqNumber]=1 ORDER BY SearchNum DESC,lineID asc";
            DataTable dt = _doh.GetDataTable();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                _Str += "<li><a href=\"javascript:void(0);\" onclick=\"ShowCheCi('" + dt.Rows[i]["lineID"].ToString().ToUpper() + "');return false;\">" + dt.Rows[i]["lineID"].ToString().ToUpper() + "</a></li>";
            }
            dt.Clear();
            dt.Dispose();
            return _Str;
        }
        /// <summary>
        /// 热门车站查询
        /// </summary>
        /// <param name="_doh"></param>
        /// <returns></returns>
        public string Hot_CheZhan(JumboECMS.DBUtility.DbOperHandler _doh)
        {
            string _Str = "";
            _doh.Reset();
            _doh.SqlCmd = "select top 30 Area_Name from [T_TrainArea] ORDER BY SearchNum DESC,Area_Name asc";
            DataTable dt = _doh.GetDataTable();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                _Str += "<li><a href=\"javascript:void(0);\" onclick=\"ShowCheZhan('" + dt.Rows[i]["Area_Name"].ToString().ToUpper() + "');return false;\">" + dt.Rows[i]["Area_Name"].ToString().ToUpper() + "</a></li>";
            }
            dt.Clear();
            dt.Dispose();
            return _Str;
        }
    }
}
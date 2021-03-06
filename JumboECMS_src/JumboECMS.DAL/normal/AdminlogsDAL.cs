﻿/*
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
using JumboECMS.DBUtility;

namespace JumboECMS.DAL
{
    /// <summary>
    /// 会员日志表信息
    /// </summary>
    public class Normal_AdminlogsDAL : Common
    {
        public Normal_AdminlogsDAL()
        {
            base.SetupSystemDate();
        }
        /// <summary>
        /// 保存管理日志
        /// </summary>
        /// <param name="_adminid">管理员ID</param>
        /// <param name="_info">保存信息</param>
        public void SaveLog(string _adminid, string _info)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.AddFieldItem("AdminId", _adminid);
                _doh.AddFieldItem("OperInfo", _info);
                _doh.AddFieldItem("OperTime", DateTime.Now.ToString());
                _doh.AddFieldItem("OperIP", IPHelp.ClientIP);
                _doh.Insert("jcms_normal_adminlogs");
            }
        }
        /// <summary>
        /// 得到列表JSON数据
        /// </summary>
        /// <param name="_thispage">当前页码</param>
        /// <param name="_pagesize">每页记录条数</param>
        /// <param name="_joinstr">关联条件</param>
        /// <param name="_wherestr1">外围条件(带A.)</param>
        /// <param name="_wherestr2">分页条件(不带A.)</param>
        /// <param name="_jsonstr">返回值</param>
        public void GetListJSON(int _thispage, int _pagesize, string _joinstr, string _wherestr1, string _wherestr2, ref string _jsonstr)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = _wherestr2;
                string sqlStr = "";
                int _countnum = _doh.Count("jcms_normal_adminlogs");
                sqlStr = JumboECMS.Utils.SqlHelp.GetSql0("A.id as id,A.AdminId as AdminId,B.[AdminName] as AdminName,A.OperInfo as OperInfo,A.OperTime as OperTime,A.OperIP as OperIP", "jcms_normal_adminlogs", "jcms_normal_user", "Id", _pagesize, _thispage, "desc", _joinstr, _wherestr1, _wherestr2);
                _doh.Reset();
                _doh.SqlCmd = sqlStr;
                DataTable dt = _doh.GetDataTable();
                _jsonstr = "{\"result\" :\"1\"," +
                    "\"returnval\" :\"操作成功\"," +
                    "\"pagerbar\" :\"" + JumboECMS.Utils.HtmlPager.GetPageBar(3, "js", 2, _countnum, _pagesize, _thispage, "javascript:ajaxList(*);") + "\"," +
                    JumboECMS.Utils.dtHelp.DT2JSON(dt, (_pagesize * (_thispage - 1))) +
                    "}";
                dt.Clear();
                dt.Dispose();
            }
        }
        /// <summary>
        /// 清空管理日志
        /// </summary>
        public void DeleteLogs()
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "id>0";
                _doh.Delete("jcms_normal_adminlogs");
            }
        }
    }
}

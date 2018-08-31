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
using JumboECMS.DBUtility;

namespace JumboECMS.DAL
{
    /// <summary>
    /// 缩略图表信息
    /// </summary>
    public class Normal_ThumbsDAL : Common
    {
        public Normal_ThumbsDAL()
        {
            base.SetupSystemDate();
        }
        /// <summary>
        /// 得到数据表
        /// </summary>
        /// <param name="_moduletype"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string _moduletype)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                if (_moduletype == "")
                    _doh.SqlCmd = "SELECT * FROM [jcms_normal_thumbs] ORDER BY ID";
                else
                    _doh.SqlCmd = "SELECT * FROM [jcms_normal_thumbs] WHERE ([Module]='" + _moduletype + "') OR ([Module]='') ORDER BY ID";
                DataTable dt = _doh.GetDataTable();
                return dt;
            }

        }
    }
}

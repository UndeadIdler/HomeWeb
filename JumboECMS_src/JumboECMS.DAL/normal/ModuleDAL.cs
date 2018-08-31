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
using System.Data.SqlClient;
using System.Web;
using JumboECMS.Utils;
using JumboECMS.DBUtility;

namespace JumboECMS.DAL
{
    /// <summary>
    /// 频道表信息
    /// </summary>
    public class Normal_ModuleDAL : Common
    {
        public Normal_ModuleDAL()
        {
            base.SetupSystemDate();
        }
        /// <summary>
        /// 绑定记录至频道实体
        /// </summary>
        /// <param name="_id"></param>
        public JumboECMS.Entity.Normal_Module GetEntity(DataRow dr)
        {
            JumboECMS.Entity.Normal_Module module = new JumboECMS.Entity.Normal_Module();
            module.Name = dr["Name"].ToString();
            module.Type = dr["Type"].ToString().ToLower();
            module.pId = Validator.StrToInt(dr["pId"].ToString(), 0);
            module.ItemName = dr["ItemName"].ToString();
            module.ItemUnit = dr["ItemUnit"].ToString();
            module.Info = dr["Info"].ToString();
            module.UploadPath = dr["UploadPath"].ToString().Replace("<#SiteDir#>", site.Dir).Replace("<#ModuleType#>", module.Type).Replace("//", "/");
            module.UploadType = dr["UploadType"].ToString();
            module.UploadSize = Validator.StrToInt(dr["UploadSize"].ToString(), 1024);
            module.IsHtml = Validator.StrToInt(dr["IsHtml"].ToString(), 0) == 1;
            module.IsTop = Validator.StrToInt(dr["IsTop"].ToString(), 0) == 1;
            module.ClassDepth = Validator.StrToInt(dr["ClassDepth"].ToString(), 0);
            module.TemplateId = Validator.StrToInt(dr["TemplateId"].ToString(), 0);
            module.DefaultThumbs = Validator.StrToInt(dr["DefaultThumbs"].ToString(), 0);
            return module;
        }
        /// <summary>
        /// 获得单页内容的单条记录实体
        /// </summary>
        /// <param name="_id"></param>
        public JumboECMS.Entity.Normal_Module GetEntity(string _moduletype)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                JumboECMS.Entity.Normal_Module module = new JumboECMS.Entity.Normal_Module();
                if (_moduletype != "")
                {
                    _doh.Reset();
                    _doh.SqlCmd = "SELECT * FROM [jcms_normal_module] WHERE [Type]='" + _moduletype + "'";
                    DataTable dt = _doh.GetDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        module = GetEntity(dr);
                    }
                    else
                        module.UploadPath = module.UploadPath.Replace("<#SiteDir#>", site.Dir).Replace("//", "/");
                    dt.Clear();
                    dt.Dispose();
                }
                else
                    module.UploadPath = module.UploadPath.Replace("<#SiteDir#>", site.Dir).Replace("//", "/");
                return module;
            }

        }
        /// <summary>
        /// 获得频道默认缩略图尺寸
        /// </summary>
        /// <param name="_moduletype"></param>
        /// <param name="iWidth"></param>
        /// <param name="iHeight"></param>
        /// <returns></returns>
        public bool GetThumbsSize(string _moduletype, ref int iWidth, ref int iHeight)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                iWidth = 0;
                iHeight = 0;
                _doh.Reset();
                _doh.SqlCmd = "select iWidth,iHeight from [jcms_normal_thumbs] where [module]='" + _moduletype + "'";
                DataTable dtThumbs = _doh.GetDataTable();
                if (dtThumbs.Rows.Count == 1)
                {
                    iWidth = Str2Int(dtThumbs.Rows[0]["iWidth"].ToString());
                    iHeight = Str2Int(dtThumbs.Rows[0]["iHeight"].ToString());
                }
                dtThumbs.Clear();
                dtThumbs.Dispose();
                return true;
            }
        }
    }
}

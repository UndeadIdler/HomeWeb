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
    /// 模型内容业务类
    /// </summary>
    public class ModuleContentDAL : Common
    {
        public ModuleContentDAL()
        {
            base.SetupSystemDate();
        }
        /// <summary>
        /// 获得内容的某些属性(第一个是时间，第二个是内容页另名)
        /// </summary>
        /// <param name="_moduletype">模块</param>
        /// <param name="_contentid">内容ID</param>
        /// <returns></returns>
        public object[] GetSome(string _moduletype, string _contentid)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = " Id=" + _contentid;
                return _doh.GetFields("jcms_module_" + _moduletype, "AddDate,FirstPage,AliasPage,(select FilePath from [" + base.CategoryTable + "] where id=[jcms_module_" + _moduletype + "].CategoryId) as CategoryFilePath");
            }
        }
    }
}

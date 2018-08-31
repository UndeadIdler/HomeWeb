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
namespace JumboECMS.Common
{
    /// <summary>
    /// 校验码操作
    /// </summary>
    public static class ValidateCode
    {
        /// <summary>
        /// 创建校验码
        /// </summary>
        /// <param name="_length"></param>
        /// <param name="_cover">是否覆盖老的值</param>
        public static void CreateValidateCode(int _length, bool _cover)
        {
            if (_cover)
                SaveCookie(_length);
            else
            {
                if (JumboECMS.Utils.Cookie.GetValue("ValidateCode") == null)
                    SaveCookie(_length);
            }
        }
        public static void SaveCookie(int _length)
        {
            char[] chars = "0123456789".ToCharArray();
            JumboECMS.Entity.Site site = (JumboECMS.Entity.Site)System.Web.HttpContext.Current.Application["jecmsV161"];
            Random random = new Random();
            string validateCode = string.Empty;
            for (int i = 0; i < _length; i++)
                validateCode += chars[random.Next(0, chars.Length)].ToString();
            JumboECMS.Utils.Cookie.SetObj("ValidateCode", 1, validateCode, site.CookieDomain, "/");
        }
        /// <summary>
        ///  获得校验码
        /// </summary>
        /// <param name="_code">需要判断的值</param>
        /// <param name="_init">是否初始化新的值</param>
        /// <returns></returns>
        public static string GetValidateCode(int _length, bool _init)
        {
            if (_init)//需要初始化新的cookie
                CreateValidateCode(_length, false);
            return JumboECMS.Utils.Cookie.GetValue("ValidateCode");
        }
        /// <summary>
        /// 判断校验码,如果判断正确则生成新的校验码
        /// </summary>
        /// <param name="_code">不能是空值，否则为false</param>
        /// <returns></returns>
        public static bool CheckValidateCode(string _code)
        {
            if (_code == null || _code.Length == 0)
                return false;
            if (GetValidateCode(4, false).ToLower() == _code.ToLower())
            {
                CreateValidateCode(4, true);
                return true;
            }
            return false;
        }
    }
}

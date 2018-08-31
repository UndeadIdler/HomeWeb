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
    /// 管理员表信息
    /// </summary>
    public class AdminDAL : Common
    {
        public AdminDAL()
        {
            base.SetupSystemDate();
        }

        public JumboECMS.Entity.Admin GetEntity(string _adminid)
        {
            JumboECMS.Entity.Admin admin = new JumboECMS.Entity.Admin();
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT AdminId,AdminName,AdminPass,Setting,LastTime2,LastIP2,Cookiess,AdminState FROM [jcms_normal_user] WHERE [AdminId]=" + _adminid;
                DataTable dt = _doh.GetDataTable();
                if (dt.Rows.Count > 0)
                {
                    admin.AdminId = Validator.StrToInt(dt.Rows[0]["AdminId"].ToString(), 0); ;
                    admin.AdminName = dt.Rows[0]["AdminName"].ToString();
                    admin.AdminPass = dt.Rows[0]["AdminPass"].ToString();
                    admin.Setting = dt.Rows[0]["Setting"].ToString();
                    admin.LastTime2 = Validator.StrToDate(dt.Rows[0]["LastTime2"].ToString(), DateTime.Now);
                    admin.LastIP2 = dt.Rows[0]["LastIP2"].ToString();
                    admin.Cookiess = dt.Rows[0]["Cookiess"].ToString();
                    admin.AdminState = Validator.StrToInt(dt.Rows[0]["AdminState"].ToString(), 0);
                }
            }
            return admin;
        }
        /// <summary>
        /// 验证管理员登录
        /// </summary>
        /// <param name="_adminname">登录名</param>
        /// <param name="_adminpass">密码</param>
        /// <param name="iExpires">保存信息的天数</param>
        /// <returns></returns>
        public string ChkAdminLogin(string _adminname, string _adminpass, int _iExpires, int _screenwidth)
        {
            _adminname = _adminname.Replace("\'", "");
            if (_screenwidth < 1024)
                return "屏幕分辨率太小";
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "adminname=@adminname and adminstate=1";
                _doh.AddConditionParameter("@adminname", _adminname);
                string _adminid = _doh.GetField("jcms_normal_user", "adminid").ToString();
                if (_adminid != "0" && _adminid != "")
                {
                    JumboECMS.Entity.Admin _Admin = GetEntity(_adminid);
                    if (_Admin.AdminPass.Length == 16)//老密码
                    {
                        if (_Admin.AdminPass.ToLower() != JumboECMS.Utils.MD5.Lower16(_adminpass))
                        {
                            //_doh.Dispose();
                            return "密码错误";
                        }
                    }
                    else
                    {
                        if (_Admin.AdminPass.ToLower() != JumboECMS.Utils.MD5.Lower32(_adminpass))
                        {
                            //_doh.Dispose();
                            return "密码错误";
                        }
                    }
                    string _adminCookiess = "c" + (new Random().Next(10000000, 99999999)).ToString();
                    //设置Cookies
                    System.Collections.Specialized.NameValueCollection myCol = new System.Collections.Specialized.NameValueCollection();
                    myCol.Add("id", _adminid);
                    myCol.Add("name", _adminname);
                    myCol.Add("password", JumboECMS.Utils.MD5.Lower32(_adminpass));
                    myCol.Add("cookies", _adminCookiess);
                    myCol.Add("screenwidth", _screenwidth.ToString());
                    JumboECMS.Utils.Cookie.SetObj(site.CookiePrev + "admin", _iExpires, myCol, site.CookieDomain, site.CookiePath);

                    //更新管理员登陆信息
                    _doh.Reset();
                    _doh.ConditionExpress = "adminid=@adminid and adminstate=1";
                    _doh.AddConditionParameter("@adminid", _adminid);
                    if (_Admin.AdminPass.Length == 16)//老密码
                        _doh.AddFieldItem("AdminPass", JumboECMS.Utils.MD5.Lower32(_adminpass));
                    _doh.AddFieldItem("Cookiess", _adminCookiess);
                    _doh.AddFieldItem("LastTime2", DateTime.Now.ToString());
                    _doh.AddFieldItem("LastIP2", IPHelp.ClientIP);
                    _doh.Update("jcms_normal_user");
                    //_doh.Dispose();
                    return "ok";
                }
                else
                {
                    //_doh.Dispose();
                    return "帐号不存在";
                }
            }

        }
        /// <summary>
        /// 管理员退出登录
        /// </summary>
        public void ChkAdminLogout()
        {
            if (JumboECMS.Utils.Cookie.GetValue(site.CookiePrev + "admin") != null)
            {
                JumboECMS.Utils.Cookie.Del(site.CookiePrev + "admin", site.CookieDomain, site.CookiePath);
            }
        }
        /// <summary>
        /// 判断adminsign是否正确
        /// </summary>
        /// <param name="_adminid"></param>
        /// <param name="_adminsign">长度一定是32位</param>
        /// <returns></returns>
        public bool ChkAdminSign(string _adminid, string _adminsign)
        {
            if (_adminsign.Length != 32 || _adminid == "")
            {
                return false;
            }
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "adminid=@adminid and adminsign=@adminsign and adminstate=1";
                _doh.AddConditionParameter("@adminid", Str2Int(_adminid));
                _doh.AddConditionParameter("@adminsign", _adminsign);
                if (_doh.Exist("jcms_normal_user"))
                    return true;
                else
                    return false;
            }
        }
    }
}

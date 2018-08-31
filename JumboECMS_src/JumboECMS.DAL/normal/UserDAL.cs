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
    /// 标签表信息
    /// </summary>
    public class Normal_UserDAL : Common
    {
        public Normal_UserDAL()
        {
            base.SetupSystemDate();
        }
        /// <summary>
        /// 是否存在记录
        /// </summary>
        /// <param name="_wherestr">条件</param>
        /// <returns></returns>
        public bool Exists(string _wherestr)
        {
            int _ext = 0;
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = _wherestr;
                if (_doh.Exist("jcms_normal_user"))
                    _ext = 1;
            }
            return (_ext == 1);
        }
        public DataTable GetUserList(int _thispage, int _pagesize, string _wherestr)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = _wherestr;
                string sqlStr = "";
                int _countnum = _doh.Count("jcms_normal_user");
                sqlStr = JumboECMS.Utils.SqlHelp.GetSql0("[ID],[UserName],[GUID]", "jcms_normal_user", "Id", _pagesize, _thispage, "desc", _wherestr);
                _doh.Reset();
                _doh.SqlCmd = sqlStr;
                DataTable dt = _doh.GetDataTable();
                return dt;
            }
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteByID(string _id)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "id=@id";
                _doh.AddConditionParameter("@id", _id);
                int _del = _doh.Delete("jcms_normal_user");
                return (_del == 1);
            }

        }

        /// <summary>
        /// 获得单页内容的单条记录实体
        /// </summary>
        /// <param name="_id"></param>
        public JumboECMS.Entity.Normal_User GetEntity(string _id)
        {
            JumboECMS.Entity.Normal_User user = new JumboECMS.Entity.Normal_User();
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT * FROM [jcms_normal_user] WHERE [Id]=" + _id;
                DataTable dt = _doh.GetDataTable();
                if (dt.Rows.Count > 0)
                {
                    user.Id = dt.Rows[0]["Id"].ToString();
                    user.AdminId = Validator.StrToInt(dt.Rows[0]["AdminId"].ToString(), 0);
                    user.UserName = dt.Rows[0]["UserName"].ToString();
                    user.NickName = dt.Rows[0]["NickName"].ToString();
                    user.AdminName = dt.Rows[0]["AdminName"].ToString();
                    user.UserPass = dt.Rows[0]["UserPass"].ToString();
                    user.Question = dt.Rows[0]["Question"].ToString();
                    user.Answer = dt.Rows[0]["Answer"].ToString();
                    user.Sex = Validator.StrToInt(dt.Rows[0]["Sex"].ToString(), 0);
                    user.Email = dt.Rows[0]["Email"].ToString();
                    user.Group = Validator.StrToInt(dt.Rows[0]["Group"].ToString(), 0);
                    user.State = Validator.StrToInt(dt.Rows[0]["State"].ToString(), 0);
                    user.Cookies = dt.Rows[0]["Cookies"].ToString();
                    user.RegTime = Validator.StrToDate(dt.Rows[0]["RegTime"].ToString(), DateTime.Now);
                    user.RegIp = dt.Rows[0]["RegIp"].ToString();
                    user.LastTime = Validator.StrToDate(dt.Rows[0]["LastTime"].ToString(), DateTime.Now);
                    user.LastIP = dt.Rows[0]["LastIP"].ToString();
                    user.TrueName = dt.Rows[0]["TrueName"].ToString();
                    user.IDCard = dt.Rows[0]["IDCard"].ToString();
                    user.QQ = dt.Rows[0]["QQ"].ToString();
                    user.MSN = dt.Rows[0]["MSN"].ToString();
                    user.BirthDay = dt.Rows[0]["BirthDay"].ToString();
                    user.ProvinceCity = dt.Rows[0]["ProvinceCity"].ToString();
                    user.WorkUnit = dt.Rows[0]["WorkUnit"].ToString();
                    user.Address = dt.Rows[0]["Address"].ToString();
                    user.ZipCode = dt.Rows[0]["ZipCode"].ToString();
                    user.Login = Validator.StrToInt(dt.Rows[0]["Login"].ToString(), 0);
                    user.MobileTel = dt.Rows[0]["MobileTel"].ToString();
                    user.Telephone = dt.Rows[0]["Telephone"].ToString();
                }
                return user;
            }

        }
        /// <summary>
        /// 插入GUID
        /// </summary>
        /// <param name="_id"></param>
        public string InsertGUID(string _id)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string _guid = "";
                _doh.Reset();
                _doh.SqlCmd = "SELECT [GUID] FROM [jcms_normal_user] WHERE [Id]=" + _id;
                DataTable dt = _doh.GetDataTable();
                if (dt.Rows.Count > 0)
                {
                    _guid = dt.Rows[0][0].ToString();
                }
                if (_guid.Length != 36)
                {
                    _guid = Guid.NewGuid().ToString();
                    _doh.ConditionExpress = "id=" + _id;
                    _doh.AddFieldItem("guid", _guid);
                    _doh.Update("jcms_normal_user");
                }
                return _guid;
            }

        }
        /// <summary>
        /// 获得用户名
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public string GetUserName(string _id)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT [UserName] FROM [jcms_normal_user] WHERE [Id]=" + _id;
                DataTable dt = _doh.GetDataTable();
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["UserName"].ToString();
                }
                return string.Empty;
            }

        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="_id">用户ID</param>
        /// <param name="_pass">修改后的密码</param>
        public void ChangePsd(string _id, string _pass)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "id=" + _id;
                _doh.AddFieldItem("UserPass", _pass);
                _doh.Update("jcms_normal_user");
            }

        }
        /// <summary>
        /// 会员注册
        /// </summary>
        /// <param name="_username">用户名</param>
        /// <param name="_userpass">密码</param>
        /// <param name="isMD5Passwd">是否已加密</param>
        /// <param name="_sex">性别</param>
        /// <param name="_email">邮箱</param>
        /// <param name="_birthday">生日</param>
        /// <param name="_usersign">验证字符串</param>
        /// <returns></returns>
        public int Register(string _username, string _userpass, bool isMD5Passwd, int _sex, string _email, string _birthday, string _usersign)
        {
            return Register(_username, _userpass, isMD5Passwd, _sex, _email, _birthday, _usersign, "", "");
        }
        public int Register(string _username, string _userpass, bool isMD5Passwd, int _sex, string _email, string _birthday, string _usersign, string _adminname, string _adminpass)
        {
            if (Exists(string.Format("username='{0}'", _username)))
                return 0;
            using (DbOperHandler _doh = new Common().Doh())
            {
                string _md5pass = isMD5Passwd ? _userpass : JumboECMS.Utils.MD5.Lower32(_userpass);
                string _md5pass2 = isMD5Passwd ? _adminpass : JumboECMS.Utils.MD5.Lower32(_adminpass);
                int uState = site.CheckReg ? 0 : 1;
                object[,] addFields = new object[2, 14] { 
                        {
                            "UserName", "NickName", "UserPass", "Sex", "Email", "Birthday", 
                            "Group", "Login", "State", "AdminId", "Setting", "UserSign", 
                            "AdminState", "RegIp"}, 
                        {
                            _username, _username, _md5pass, _sex,_email, _birthday, 
                            1, 0, uState,0, ",,", _usersign, 
                            0,IPHelp.ClientIP} 
                        };
                _doh.Reset();
                _doh.AddFieldItems(addFields);
                int _uID = _doh.Insert("jcms_normal_user");
                #region 复制头像
                JumboECMS.Utils.DirFile.CopyFile("~/_data/avatar/0_l.jpg", "~/_data/avatar/" + _uID + "_l.jpg", true);
                JumboECMS.Utils.DirFile.CopyFile("~/_data/avatar/0_m.jpg", "~/_data/avatar/" + _uID + "_m.jpg", true);
                JumboECMS.Utils.DirFile.CopyFile("~/_data/avatar/0_s.jpg", "~/_data/avatar/" + _uID + "_s.jpg", true);
                #endregion
                #region 同步升级为管理员
                if (_adminname.Length > 0 && _adminpass.Length > 0)
                {
                    _doh.Reset();
                    _doh.ConditionExpress = "id=" + _uID;
                    _doh.AddFieldItem("State", 1);
                    _doh.AddFieldItem("AdminState", 1);
                    _doh.AddFieldItem("AdminId", _uID);
                    _doh.AddFieldItem("AdminName", _adminname);
                    _doh.AddFieldItem("AdminPass", _md5pass2);
                    _doh.AddFieldItem("Group", site.AdminGroupId);
                    _doh.Update("jcms_normal_user");
                    _doh.Reset();
                    _doh.ConditionExpress = "id=" + site.AdminGroupId;
                    _doh.Add("jcms_normal_usergroup", "UserTotal");
                }
                else
                {
                    _doh.Reset();
                    _doh.ConditionExpress = "id=1";
                    _doh.Add("jcms_normal_usergroup", "UserTotal");
                }
                #endregion
                return _uID;
            }
        }
        /// <summary>
        /// 修改指定用户的密码
        /// </summary>
        /// <param name="_userid"></param>
        /// <param name="_originalPassword">原始密码</param>
        /// <param name="_newPassword">新密码</param>
        /// <returns></returns>
        public bool ChangeUserPassword(string _userid, string _originalPassword, string _newPassword)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "id=@id and state=1";
                _doh.AddConditionParameter("@id", _userid);
                object pass = _doh.GetField("jcms_normal_user", "UserPass");
                if (pass != null)//用户存在
                {
                    if (pass.ToString().ToLower() == JumboECMS.Utils.MD5.Lower32(_originalPassword) || pass.ToString().ToLower() == JumboECMS.Utils.MD5.Lower16(_originalPassword)) //验证旧密码
                    {
                        _doh.Reset();
                        _doh.ConditionExpress = "id=@id and state=1";
                        _doh.AddConditionParameter("@id", _userid);
                        _doh.AddFieldItem("UserPass", JumboECMS.Utils.MD5.Lower32(_newPassword));
                        _doh.Update("jcms_normal_user");
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }

        }
        /// <summary>
        /// 会员登录
        /// </summary>
        /// <param name="_username">登录名</param>
        /// <param name="_userpass">密码</param>
        /// <param name="iExpires">保存信息的天数</param>
        /// <returns></returns>
        public string ChkUserLogin(string _username, string _userpass, int iExpires)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _username = _username.Replace("\'", "");
                _doh.Reset();
                _doh.ConditionExpress = "username=@username";
                _doh.AddConditionParameter("@username", _username);
                string _userid = _doh.GetField("jcms_normal_user", "id").ToString();
                if (_userid != "")
                {
                    JumboECMS.Entity.Normal_User _User = new JumboECMS.DAL.Normal_UserDAL().GetEntity(_userid);
                    if (_User.UserPass.Length == 16)//老密码
                    {
                        if (_User.UserPass != JumboECMS.Utils.MD5.Lower16(_userpass))
                        {
                            return "密码错误";
                        }
                    }
                    else
                    {
                        if (_User.UserPass != JumboECMS.Utils.MD5.Lower32(_userpass))
                        {
                            return "密码错误";
                        }
                    }
                    if (_User.State != 1)
                    {
                        return "帐号被锁定";
                    }
                    _doh.Reset();
                    _doh.SqlCmd = "SELECT [id],[GroupName],[IsLogin],[Setting] FROM [jcms_normal_usergroup] WHERE [Id]=" + _User.Group;
                    DataTable dtUserGroup = _doh.GetDataTable();
                    if (dtUserGroup.Rows.Count == 0)
                    {
                        return "用户组有误";
                    }
                    if (dtUserGroup.Rows[0]["IsLogin"].ToString() != "1")
                    {
                        return "帐号禁止登录";
                    }
                    string _userGroupid = dtUserGroup.Rows[0]["Id"].ToString();
                    string _userGroupname = dtUserGroup.Rows[0]["GroupName"].ToString();
                    string _userSetting = dtUserGroup.Rows[0]["Setting"].ToString();
                    dtUserGroup.Clear();
                    dtUserGroup.Dispose();
                    string _userCookies = "c" + (new Random().Next(10000000, 99999999)).ToString();
                    //设置Cookies
                    System.Collections.Specialized.NameValueCollection myCol = new System.Collections.Specialized.NameValueCollection();
                    myCol.Add("id", _userid);
                    myCol.Add("name", _User.UserName);
                    myCol.Add("nickname", _User.NickName);
                    myCol.Add("password", _User.UserPass);
                    myCol.Add("email", _User.Email);
                    myCol.Add("groupid", _userGroupid);
                    myCol.Add("groupname", _userGroupname);
                    myCol.Add("setting", _userSetting);
                    myCol.Add("cookies", _userCookies);
                    JumboECMS.Utils.Cookie.SetObj(site.CookiePrev + "user", 60 * 60 * 24 * iExpires, myCol, site.CookieDomain, site.CookiePath);

                    //更新User登陆信息
                    _doh.Reset();
                    _doh.ConditionExpress = "id=@id and state=1";
                    _doh.AddConditionParameter("@id", _userid);
                    if (_User.UserPass.Length == 16)//老密码
                        _doh.AddFieldItem("UserPass", JumboECMS.Utils.MD5.Lower32(_userpass));
                    _doh.AddFieldItem("Cookies", _userCookies);
                    _doh.AddFieldItem("LastTime", DateTime.Now.ToString());
                    _doh.AddFieldItem("LastIP", IPHelp.ClientIP);
                    _doh.AddFieldItem("UserSign", "");
                    _doh.Update("jcms_normal_user");
                    return "ok";
                }
                else
                {
                    return "帐号不存在";
                }

            }

        }
        /// <summary>
        /// 会员注销
        /// </summary>
        /// <param name="_userkey"></param>
        /// <returns></returns>
        public bool ChkUserLogout(string _userkey)
        {
            //ChkAdminLogout();
            if (JumboECMS.Utils.Cookie.GetValue(site.CookiePrev + "user") != null)
            {
                if (JumboECMS.Utils.Cookie.GetValue(site.CookiePrev + "user", "password").Substring(4, 8) == _userkey)
                {
                    JumboECMS.Utils.Cookie.Del(site.CookiePrev + "user", site.CookieDomain, site.CookiePath);
                    return true;
                }
                else
                    return false;
            }
            return false;
        }
        /// <summary>
        /// 更新客服列表
        /// </summary>
        public void RefreshServiceList()
        {
            string _serviceids = "";
            string _servicenames = "";
            string _servicemails = "";
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT [ServiceId],[ServiceName],[Email] FROM [jcms_normal_user] WHERE ServiceId>0";
                DataTable dt = _doh.GetDataTable();
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (j == 0)
                    {
                        _serviceids = dt.Rows[j]["ServiceId"].ToString();
                        _servicenames = dt.Rows[j]["ServiceName"].ToString();
                        _servicemails = dt.Rows[j]["Email"].ToString();
                    }
                    else
                    {
                        _serviceids += "," + dt.Rows[j]["ServiceId"].ToString();
                        _servicenames += "," + dt.Rows[j]["ServiceName"].ToString();
                        _servicemails += "," + dt.Rows[j]["Email"].ToString();
                    }
                }
                string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/message.config");
                JumboECMS.DBUtility.XmlControl XmlTool = new JumboECMS.DBUtility.XmlControl(strXmlFile);
                XmlTool.Update("Messages/Service/UserId", _serviceids);
                XmlTool.Update("Messages/Service/UserName", _servicenames);
                XmlTool.Update("Messages/Service/UserMail", _servicemails);
                XmlTool.Save();
                XmlTool.Dispose();
            }
        }
        /// <summary>
        /// 插入GUID
        /// </summary>
        /// <param name="_id"></param>
        public string InsertGUID(string _id, DbOperHandler doh)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string _guid = "";
                _doh.Reset();
                _doh.SqlCmd = "SELECT [GUID] FROM [jcms_normal_user] WHERE [Id]=" + _id;
                DataTable dt = _doh.GetDataTable();
                if (dt.Rows.Count > 0)
                {
                    _guid = dt.Rows[0][0].ToString();
                }
                if (_guid.Length != 36)
                {
                    _guid = Guid.NewGuid().ToString();
                    _doh.ConditionExpress = "id=" + _id;
                    _doh.AddFieldItem("guid", _guid);
                    _doh.Update("jcms_normal_user");
                }
                return _guid;
            }
        }
        /// <summary>
        /// 获得所有客服
        /// </summary>
        /// <param name="doh"></param>
        /// <returns></returns>
        public DataTable GetServiceList()
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT [ServiceId],[GUID],[ServiceName],[Email] FROM [jcms_normal_user] WHERE ServiceId>0";
                DataTable dt = _doh.GetDataTable();
                return dt;
            }

        }
        /// <summary>
        /// 实时判断会员是不是客服
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="doh"></param>
        /// <param name="_servicename"></param>
        /// <returns></returns>
        public bool Service(string _id, DbOperHandler doh, ref string _servicename)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT [ServiceName] FROM [jcms_normal_user] WHERE ServiceId>0 AND [Id]=" + _id;
                DataTable dt = _doh.GetDataTable();
                //_doh.Dispose();
                if (dt.Rows.Count > 0)
                {
                    _servicename = dt.Rows[0]["ServiceName"].ToString();
                    return true;
                }
                else
                    return false;
            }
        }
        /// <summary>
        /// 判断usersign是否正确
        /// </summary>
        /// <param name="_userid"></param>
        /// <param name="_usersign">长度一定是32位</param>
        /// <returns></returns>
        public bool ChkUserSign(string _userid, string _usersign)
        {
            if (_usersign.Length != 32 || _userid == "")
            {
                return false;
            }
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "id=@userid and usersign=@usersign and state=1";
                _doh.AddConditionParameter("@userid", Str2Int(_userid));
                _doh.AddConditionParameter("@usersign", _usersign);
                if (_doh.Exist("jcms_normal_user"))
                    return true;
                else
                    return false;
            }
        }
    }
}

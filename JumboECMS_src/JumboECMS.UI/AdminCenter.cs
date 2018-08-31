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
using System.Collections.Specialized;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using System.Text;
using JumboECMS.Utils;
namespace JumboECMS.UI
{
    /// <summary>
    /// html页使用
    /// </summary>
    public class AdminCenter : BasicPage
    {
        public int publicMenu = 5;
        /// <summary>
        /// 模块类型
        /// </summary>
        public string ModuleType = "";
        /// <summary>
        /// 栏目编号
        /// </summary>
        public string CategoryId = "0";
        /// <summary>
        /// 主模块
        /// </summary>
        public JumboECMS.Entity.Normal_Module MainModule = new JumboECMS.DAL.Normal_ModuleDAL().GetEntity("");
        public string id = "0";
        protected string AdminId = "0";
        protected string AdminName = string.Empty;
        protected string AdminPass = string.Empty;
        protected string AdminPower = string.Empty;
        protected string AdminCookiess = string.Empty;
        protected bool AdminIsLogin = false;
        protected bool AdminIsFounder = false;
        protected int AdminScreenWidth = 1024;
        /// <summary>
        /// 列表内容通用方法,必须重写
        /// </summary>
        protected virtual void getListBox() { }
        /// <summary>
        /// 编辑内容通用方法,必须重写
        /// </summary>
        protected virtual void editBox() { }
        /// <summary>
        /// 验证登陆
        /// </summary>
        private void chkLogin()
        {
            if (Cookie.GetValue(site.CookiePrev + "admin") != null)
            {
                AdminId = Cookie.GetValue(site.CookiePrev + "admin", "id");
                AdminName = Cookie.GetValue(site.CookiePrev + "admin", "name");
                AdminPass = Cookie.GetValue(site.CookiePrev + "admin", "password");
                AdminScreenWidth = Str2Int(Cookie.GetValue(site.CookiePrev + "admin", "screenwidth"));
                AdminCookiess = Cookie.GetValue(site.CookiePrev + "admin", "cookies");
                if (AdminId.Length != 0 && AdminName.Length != 0)
                {
                    doh.Reset();
                    doh.ConditionExpress = "adminid=@id and cookiess=@cookiess";//禁止一个用户同时登录
                    doh.AddConditionParameter("@id", AdminId);
                    doh.AddConditionParameter("@cookiess", AdminCookiess);
                    AdminPower = doh.GetField("jcms_normal_user", "Setting").ToString();
                    if (AdminPower.Length != 0)
                    {
                        this.AdminIsLogin = true;
                        string Founders = JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "Founders");
                        this.AdminIsFounder = (Founders.ToLower().Contains("." + AdminName.ToLower() + "."));
                    }
                }
            }
        }
        /// <summary>
        /// 验证权限
        /// 超级管理员永远有效
        /// </summary>
        /// <param name="s">空时只要求登录</param>
        protected bool IsPower(string s)
        {
            if (s == "ok") return true;
            if (!this.AdminIsLogin)//验证一次本地信息
                chkLogin();
            if (s == "") return (this.AdminIsLogin);
            if (this.AdminIsFounder) return (this.AdminIsLogin);
            return (this.AdminPower.Contains("," + s + ","));
        }
        /// <summary>
        /// 验证权限
        /// 超级管理员永远有效
        /// </summary>
        /// <param name="s"></param>
        /// <param name="pageType">页面分为html和json</param>
        protected void chkPower(string s, string pageType)
        {
            if (!CheckFormUrl() && pageType == "json")//不可直接在url下访问
            {
                Response.End();
            }
            if (!IsPower(s))
            {
                showErrMsg("权限不足或未登录", pageType);
            }
        }
        /// <summary>
        /// 输出错误信息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="pageType">页面分为html和json</param>
        protected void showErrMsg(string msg, string pageType)
        {
            if (pageType == "html")
                FinalMessage(msg, site.Dir + "admin/login.aspx", 0);

            else if (pageType == "stop")
            {
                HttpContext.Current.Response.Redirect(site.Dir + "admin/stop.htm");
                HttpContext.Current.Response.End();
            }
            {
                HttpContext.Current.Response.Clear();
                if (!this.AdminIsLogin)
                    HttpContext.Current.Response.Write(JsonResult(-1, msg));
                else
                    HttpContext.Current.Response.Write(JsonResult(0, msg));
                HttpContext.Current.Response.End();
            }
        }
        /// <summary>
        /// 管理中心初始
        /// </summary>
        /// <param name="powerNum">权限,为空表示验证是否登录</param>
        /// <param name="pageType">页面分为html和json</param>
        protected void Admin_Load(string powerNum, string pageType)
        {
            chkPower(powerNum, pageType);
        }

        /// <summary>
        /// 管理中心初始,并获得频道的各项参数值
        /// </summary>
        /// <param name="powerNum">权限</param>
        /// <param name="module"></param>
        protected void Admin_Load(string powerNum, string pageType, string module)
        {
            chkPower(powerNum, pageType);
            if (module != "")
            {
                MainModule = new JumboECMS.DAL.Normal_ModuleDAL().GetEntity(module);
                if (MainModule.Type == "")//说明参数不对
                {
                    HttpContext.Current.Response.End();
                }
                else
                    ModuleType = MainModule.Type;
            }
        }
        /// <summary>
        /// 管理菜单
        /// </summary>
        /// <returns></returns>
        protected string[,] leftMenu()
        {
            doh.Reset();
            doh.SqlCmd = "SELECT Id,[Title],[Title2],[TypeId],[ModuleType] FROM [" + base.CategoryTable + "] WHERE [ParentId]=0 AND [TypeId]<4 ORDER BY Code";
            DataTable dtCategory = doh.GetDataTable();
            string[,] menu = new string[publicMenu + dtCategory.Rows.Count, 400];
            int firstCat = 0;
            for (int m = 0; m < dtCategory.Rows.Count; m++)
            {
                firstCat = m + 5;
                string mCategory1Name = dtCategory.Rows[m]["Title2"].ToString() != "" ? dtCategory.Rows[m]["Title2"].ToString() : dtCategory.Rows[m]["Title"].ToString();
                string mCategory1Id = dtCategory.Rows[m]["Id"].ToString();
                //1=subcat,2=list,3=single
                string mCategory1TypeId = dtCategory.Rows[m]["TypeId"].ToString();
                string mCategory1ModuleType = dtCategory.Rows[m]["ModuleType"].ToString().ToLower();
                menu[firstCat, 0] = mCategory1Name + "$0$category" + mCategory1Id;
                if (mCategory1TypeId == "0" || mCategory1TypeId == "1")//父级栏目
                {
                    #region 读取父级栏目的子栏目
                    doh.Reset();
                    doh.SqlCmd = "SELECT Id,[Title],[TypeId],[ParentId],[ModuleType],len(code) as lencode FROM [" + base.CategoryTable + "] WHERE ([Code] Like (SELECT Code FROM [" + base.CategoryTable + "] WHERE [Id]=" + mCategory1Id + ")+'%') AND len(code)>4 AND [TypeId]<4 ORDER BY Code";
                    DataTable dtCategory2 = doh.GetDataTable();
                    int secondCat = 0;
                    int groupid = 0;
                    for (int n = 0; n < dtCategory2.Rows.Count; n++)
                    {
                        secondCat = n + 1;
                        string mCategory2Name = dtCategory2.Rows[n]["Title"].ToString();
                        string mCategory2Id = dtCategory2.Rows[n]["Id"].ToString();
                        string mCategory2ParentId = dtCategory2.Rows[n]["ParentId"].ToString();
                        //1=subcat,2=list,3=single
                        string mCategory2TypeId = dtCategory2.Rows[n]["TypeId"].ToString();
                        string mCategory2ModuleType = dtCategory2.Rows[n]["ModuleType"].ToString().ToLower();
                        int mCategory2Depth = (Str2Int(dtCategory2.Rows[n]["LenCode"].ToString()) / 4) - 1;
                        if (mCategory2Depth == 1) groupid++;
                        string classLink = "";
                        if (mCategory2TypeId == "1")//父级栏目
                            classLink = "#";
                        else if (mCategory2TypeId == "2")//列表栏目
                            classLink = "module_" + mCategory2ModuleType + "_list.aspx?categoryid=" + mCategory2Id;
                        else
                            classLink = "category_modify3.aspx?id=" + mCategory2Id;
                        menu[firstCat, secondCat] = classLink + "|" + mCategory2Name + "|" + mCategory2Depth + "|" + groupid;
                    }
                    dtCategory2.Clear();
                    dtCategory2.Dispose();
                    #endregion
                }
                else if (mCategory1TypeId == "2")//列表栏目
                {
                    menu[firstCat, 1] = "module_" + mCategory1ModuleType + "_list.aspx?categoryid=" + mCategory1Id + "|" + mCategory1Name;
                }
                else
                    menu[firstCat, 1] = "category_modify3.aspx?id=" + mCategory1Id + "|" + mCategory1Name;
            }
            dtCategory.Clear();
            dtCategory.Dispose();

            menu[0, 0] = "系统管理$0$system";
            menu[0, 1] = "configset_default.aspx|网站参数";
            menu[0, 2] = "admin_list.aspx|后台管理员";
            menu[0, 3] = "category_list1.aspx|中文版栏目";
            menu[0, 4] = "category_list2.aspx|英文版栏目";
            menu[0, 5] = "link_list.aspx|友情链接";
            menu[0, 6] = "slide_list.aspx|幻灯位管理";
            menu[0, 7] = "adv_list.aspx|广告位管理";
            menu[0, 8] = "database_access.aspx|数据库维护";
            menu[0, 9] = "executesql_default.aspx|在线执行SQL";

            menu[1, 0] = "会员管理$0$member";
            menu[1, 1] = "user_list.aspx|注册会员";
            menu[1, 2] = "usergroup_list.aspx|用户组";
            menu[1, 3] = "feedback_list.aspx|在线留言";
            menu[1, 4] = "userorder_list.aspx|会员订单";

            menu[2, 0] = "模板管理$0$template";
            menu[2, 1] = "templateinclude_list.aspx|include模块";
            menu[2, 2] = "javascript_list.aspx|自定义外站调用";

            menu[3, 0] = "后台首页$0$home";
            menu[3, 1] = "home.aspx|前台更新";
            menu[3, 2] = "myinfo_password.aspx|修改密码";

            menu[4, 0] = "邮件群发$0$email";
            menu[4, 1] = "email_list.aspx|收件人管理";
            menu[4, 2] = "emailgroup_list.aspx|邮箱分组管理";
            menu[4, 3] = "emailserver_list.aspx|代发人管理";
            menu[4, 4] = "emaillogs_list.aspx|历史记录";
            return menu;
        }

        /// <summary>
        /// 编辑内容时,向栏目专题标题颜色等DropDownList中添加内容
        /// </summary>
        /// <param name="ddlCategoryId">栏目ID</param>
        /// <param name="ClassDepth">栏目深度</param>
        /// <param name="ddlReadGroup">阅读权限</param>
        protected void getEditDropDownList(string categoryId, string moduleType, ref DropDownList ddlReadGroup)
        {
            if (!Page.IsPostBack)
            {
                doh.Reset();
                doh.ConditionExpress = "id=" + categoryId + " and [ModuleType]='" + moduleType + "'";
                if (!doh.Exist(base.CategoryTable))
                {
                    FinalMessage("栏目属性有误!", "close.htm", 0);
                    return;
                }
                doh.Reset();
                doh.SqlCmd = "SELECT [ID],[GroupName] FROM [jcms_normal_usergroup] ORDER BY id ASC";
                DataTable dtGroup = doh.GetDataTable();
                ddlReadGroup.Items.Clear();
                //ddlReadGroup.Items.Add(new ListItem("继承栏目权限", "-1"));
                ddlReadGroup.Items.Add(new ListItem("任意用户", "0"));
                for (int i = 0; i < dtGroup.Rows.Count; i++)
                {
                    ddlReadGroup.Items.Add(new ListItem(dtGroup.Rows[i]["GroupName"].ToString(), dtGroup.Rows[i]["Id"].ToString()));
                }
                dtGroup.Clear();
                dtGroup.Dispose();
            }
        }
        /// <summary>
        /// 统计用户组用户总数
        /// <param name="_gId">用户组Id，为0表示更新所有的</param>
        /// </summary>
        protected void UserGroupCount(string _gId)
        {
            doh.Reset();
            doh.SqlCmd = "SELECT ID FROM [jcms_normal_usergroup]";
            if (_gId != "0") doh.SqlCmd += " WHERE [Id]=" + _gId;
            DataTable dtUserGroup = doh.GetDataTable();
            int total = dtUserGroup.Rows.Count;
            int tmp;
            for (int i = 0; i < total; i++)
            {
                doh.Reset();
                doh.SqlCmd = "SELECT ID FROM [jcms_normal_user] WHERE [Group]=" + dtUserGroup.Rows[i]["Id"].ToString();
                tmp = doh.GetDataTable().Rows.Count;
                doh.Reset();
                doh.ConditionExpress = "id=" + dtUserGroup.Rows[i]["Id"].ToString();
                doh.AddFieldItem("UserTotal", tmp);
                doh.Update("jcms_normal_usergroup");
            }
            dtUserGroup.Clear();
            dtUserGroup.Dispose();
        }
        /// <summary>
        /// 统计邮箱组邮箱总数
        /// <param name="_gId">邮箱组Id，为0表示更新所有的</param>
        /// </summary>
        protected void EmailGroupCount(string _gId)
        {
            doh.Reset();
            doh.SqlCmd = "SELECT ID FROM [jcms_normal_emailgroup]";
            if (_gId != "0") doh.SqlCmd += " WHERE [Id]=" + _gId;
            DataTable dtEmailGroup = doh.GetDataTable();
            int total = dtEmailGroup.Rows.Count;
            int tmp;
            for (int i = 0; i < total; i++)
            {
                doh.Reset();
                doh.SqlCmd = "SELECT ID FROM [jcms_normal_email] WHERE [GroupId]=" + dtEmailGroup.Rows[i]["Id"].ToString();
                tmp = doh.GetDataTable().Rows.Count;
                doh.Reset();
                doh.ConditionExpress = "id=" + dtEmailGroup.Rows[i]["Id"].ToString();
                doh.AddFieldItem("EmailTotal", tmp);
                doh.Update("jcms_normal_emailgroup");
            }
            dtEmailGroup.Clear();
            dtEmailGroup.Dispose();
        }
        /// <summary>
        /// 取得内容列表
        /// </summary>
        /// <param name="_categoryId">栏目Id</param>
        /// <param name="keyType">搜索关键字类型{Author,title,summary}</param>
        /// <param name="keyWord">搜索关键字</param>
        /// <param name="sDate">日期{1d=今天,1w=本周,1m=本月}</param>
        /// <param name="isPass">状态{0=新的,1=已发布,-1=待审,否则=全部}</param>
        /// <param name="isImg"></param>
        /// <param name="isTop"></param>
        /// <param name="isFocus"></param>
        /// <param name="PSize">每页记录数</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        protected string GetContentList(string _categoryId, string keyType, string keyWord, string sDate, string isPass, string isImg, string isTop, string isFocus, int PSize, int page)
        {
            string _returnStr = string.Empty;
            _categoryId = JumboECMS.Utils.Strings.SafetyStr(_categoryId);
            keyType = JumboECMS.Utils.Strings.SafetyStr(keyType);
            keyWord = JumboECMS.Utils.Strings.SafetyStr(keyWord);
            sDate = JumboECMS.Utils.Strings.SafetyStr(sDate);
            int countNum = 0;
            string sqlStr = "";
            string whereStr = "1=1";//分页条件(不带A.)
            if (_categoryId != "0")
            {
                whereStr += " And [CategoryId] in (SELECT ID FROM [" + base.CategoryTable + "] WHERE [Code] Like (SELECT Code FROM [" + base.CategoryTable + "] WHERE [Id]=" + _categoryId + ")+'%')";
            }
            if ((keyType.Length > 0) && (keyWord.Length > 0))
            {
                whereStr += " AND " + keyType + " LIKE '%" + keyWord + "%'";
            }
            switch (isPass)
            {
                case "0":
                    whereStr += " AND [IsPass]=0";
                    break;
                case "1":
                    whereStr += " AND [IsPass]=1";
                    break;
                case "-1":
                    whereStr += " AND [IsPass]=-1";
                    break;
                default:
                    break;
            }
            switch (isImg)
            {
                case "2":
                    whereStr += " AND ([isImg]=1 AND left(Img,7)='http://')";
                    break;
                case "1":
                    whereStr += " AND [isImg]=1";
                    break;
                case "-1":
                    whereStr += " AND [isImg]=0";
                    break;
                default:
                    break;
            }
            switch (isTop)
            {
                case "1":
                    whereStr += " AND [isTop]=1";
                    break;
                case "-1":
                    whereStr += " AND [isTop]=0";
                    break;
                default:
                    break;
            }
            switch (isFocus)
            {
                case "1":
                    whereStr += " AND [isFocus]=1";
                    break;
                case "-1":
                    whereStr += " AND [isFocus]=0";
                    break;
                default:
                    break;
            }
            doh.Reset();
            doh.ConditionExpress = whereStr;
            countNum = doh.Count("jcms_module_" + MainModule.Type);
            NameValueCollection orders = new NameValueCollection();
            orders.Add("AddDate", "desc");
            orders.Add("Id", "desc");
            string FieldList = "Id,Title,IsPass,IsImg,IsTop,IsFocus,FirstPage,AddDate,(select title from [" + base.CategoryTable + "] where id=[jcms_module_" + MainModule.Type + "].categoryid) as CategoryName";
            if (keyType.ToLower() != "title" && keyType.Length > 0)
                FieldList += "," + keyType;
            sqlStr = JumboECMS.Utils.SqlHelp.GetSql1(FieldList,
                "jcms_module_" + MainModule.Type,countNum,
                PSize,
                page,
                orders,
                whereStr);
            doh.Reset();
            doh.SqlCmd = sqlStr;


            DataTable dt = doh.GetDataTable();
            doh.Reset();
            doh.ConditionExpress = "ispass=0";
            int NewNum = doh.Count("jcms_module_" + MainModule.Type);
            _returnStr = "{\"result\" :\"1\"," +
                "\"returnval\" :\"操作成功\"," +
                "\"newnum\" :" + NewNum + "," +
                "\"returmodule\" :\"" + MainModule.Type + "\"," +
                "\"pagerbar\" :\"" + JumboECMS.Utils.HtmlPager.GetPageBar(3, "js", 2, countNum, PSize, page, "javascript:ajaxList(*);") + "\"," +
                JumboECMS.Utils.dtHelp.DT2JSON(dt, (PSize * (page - 1))) +
                "}";
            dt.Clear();
            dt.Dispose();
            return _returnStr;
        }
        /// <summary>
        ///  统计数据
        /// </summary>
        protected void CreateCount()
        {
            int _classCount = 0;
            int _contentCount = 0;
            string TempCountStr = "var ___JSON_SystemCount = /*请勿手动修改*/\r\n{";
            doh.Reset();
            doh.SqlCmd = "SELECT [ID],[Name],[Type] FROM [jcms_normal_module] ORDER BY pId";
            DataTable dtModule = doh.GetDataTable();
            TempCountStr += "table: [";
            if (dtModule.Rows.Count > 0)
            {
                for (int c = 0; c < dtModule.Rows.Count; c++)
                {
                    if (c > 0) TempCountStr += ",";
                    string _moduleName = dtModule.Rows[c]["Name"].ToString();
                    string _moduleType = dtModule.Rows[c]["Type"].ToString();
                    doh.Reset();
                    doh.ConditionExpress = "ParentId=0";
                    _classCount = doh.Count("jcms_module_" + _moduleType + "class");
                    doh.Reset();
                    doh.ConditionExpress = " IsPass=1";
                    _contentCount = doh.Count("jcms_module_" + _moduleType);
                    TempCountStr += "{" +
                        "classid: 0," +
                        "title: '" + _moduleName + "'," +
                        "classcount: " + _classCount + "," +
                        "contentcount: " + _contentCount + "}";
                }
            }
            TempCountStr += "]";
            TempCountStr += "}";
            JumboECMS.Utils.DirFile.SaveFile(TempCountStr, "~/_data/json/_systemcount.js");
        }
        /// <summary>
        /// 执行内容的移动,审核,删除等操作
        /// </summary>
        /// <param name="_act">操作类型{pass=审核,nopass=未审,remove=移入回收站,del=彻底删除}</param>
        /// <param name="_ids">id字符串,以","串联起来</param>
        /// <param name="pageType">页面分为html和json</param>
        public void BatchContent(string _act, string _ids, string pageType)
        {
            string[] idValue;
            idValue = _ids.Split(',');
            if (_act == "createhtml")
            {
                Admin_Load(this.MainModule.Type + "-create", "json");
                for (int i = 0; i < idValue.Length; i++)
                {
                    CreateContentFile(MainModule, idValue[i], -1);//生成内容页
                }
                return;
            }
            if (_act == "pass")
            {
                Admin_Load(this.MainModule.Type + "-audit", "json");
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=" + idValue[i] + " and [IsPass]<1";
                    doh.AddFieldItem("IsPass", 1);
                    if (doh.Update("jcms_module_" + this.MainModule.Type) == 1)
                    {
                        CreateContentFile(MainModule, idValue[i], -1);//生成内容页
                    }
                }
                return;
            }
            if (_act == "nopass")
            {
                Admin_Load(this.MainModule.Type + "-audit", "json");
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=" + idValue[i] + " and [IsPass]=1";
                    doh.AddFieldItem("IsPass", 0);
                    if (doh.Update("jcms_module_" + this.MainModule.Type) == 1)
                    {
                        DeleteContentFile(idValue[i]);//删内容页
                    }
                }
                return;
            }
            if (_act == "top")
            {
                Admin_Load(this.MainModule.Type + "-audit", "json");
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=" + idValue[i];
                    doh.AddFieldItem("IsTop", 1);
                    doh.Update("jcms_module_" + this.MainModule.Type);
                }
                return;
            }
            if (_act == "notop")
            {
                Admin_Load(this.MainModule.Type + "-audit", "json");
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=" + idValue[i];
                    doh.AddFieldItem("IsTop", 0);
                    doh.Update("jcms_module_" + this.MainModule.Type);
                }
                return;
            }
            if (_act == "sdel")
            {
                Admin_Load(this.MainModule.Type + "-delete", "json");
                for (int i = 0; i < idValue.Length; i++)
                {
                    DeleteContentFile(idValue[i]);//删内容页
                    doh.Reset();
                    doh.ConditionExpress = "id=" + idValue[i];
                    doh.AddFieldItem("IsPass", -1);
                    doh.Update("jcms_module_" + this.MainModule.Type);//放入回收站
                }
                return;
            }
            if (_act == "del")
            {
                Admin_Load("master", "json");
                for (int i = 0; i < idValue.Length; i++)
                {
                    DeleteContentFile(idValue[i]);//先删内容页
                    doh.Reset();
                    doh.ConditionExpress = "id=" + idValue[i];
                    doh.Delete("jcms_module_" + this.MainModule.Type);
                }
                return;
            }
        }
        protected void ajaxCreateJavascript()
        {
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/javascript.config");
            JumboECMS.DBUtility.XmlControl XmlTool = new JumboECMS.DBUtility.XmlControl(strXmlFile);
            try
            {
                XmlTool.RemoveAll("Lis");
                XmlTool.Save();
                doh.Reset();
                doh.SqlCmd = "Select [Id],[Title],[Code],[TemplateContent] FROM [jcms_normal_javascript] ORDER BY id asc";
                DataTable dt = doh.GetDataTable();
                string _id = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _id = dt.Rows[i]["Id"].ToString();
                    XmlTool = new JumboECMS.DBUtility.XmlControl(strXmlFile);
                    XmlTool.InsertNode("Lis", "Li", "ID", _id);
                    XmlTool.InsertElement("Lis/Li[ID=\"" + _id + "\"]", "Title", dt.Rows[i]["Title"].ToString(), false);
                    XmlTool.InsertElement("Lis/Li[ID=\"" + _id + "\"]", "Code", dt.Rows[i]["Code"].ToString(), true);
                    XmlTool.InsertElement("Lis/Li[ID=\"" + _id + "\"]", "TemplateContent", dt.Rows[i]["TemplateContent"].ToString(), true);
                    XmlTool.Save();
                }
                XmlTool.Dispose();
            }
            catch
            {
            }
        }
        #region 生成静态页面
        /// <summary>
        /// 删除栏目页面
        /// </summary>
        /// <param name="_categoryId"></param>
        protected void DeleteCategoryFile(string _categoryId)
        {
            string htmFile1 = Go2Category(_categoryId, 1);//第1页
            if (System.IO.File.Exists(Server.MapPath(htmFile1)))
                System.IO.File.Delete(htmFile1);
            string htmFileN = Go2Category(_categoryId, -1);//第N页
            string folderPath = JumboECMS.Utils.DirFile.GetFolderPath(htmFileN, true);
            string fileNames = JumboECMS.Utils.DirFile.GetFileName(htmFileN, true);
            if (System.IO.Directory.Exists(Server.MapPath(folderPath)))
            {
                string[] htmFiles = System.IO.Directory.GetFiles(Server.MapPath(folderPath), fileNames);
                foreach (string fileName in htmFiles)
                {
                    if (System.IO.File.Exists(fileName))
                        System.IO.File.Delete(fileName);
                }
            }
        }

        /// <summary>
        /// 删除内容文件
        /// </summary>

        protected void DeleteContentFile(string _contentID)
        {
            JumboECMS.DAL.ModuleCommand.DeleteContent(this.MainModule.Type, _contentID);
        }
        #endregion
        /// <summary>
        /// 批量更新广告
        /// </summary>
        /// <returns></returns>
        public bool BatchUpdateAdv()
        {
            doh.Reset();
            doh.SqlCmd = "SELECT id,State FROM [jcms_normal_adv]";
            DataTable dt = doh.GetDataTable();
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string aId = dt.Rows[i][0].ToString();
                    string aState = dt.Rows[i][1].ToString();
                    new JumboECMS.DAL.AdvDAL().CreateAdv(aId, aState);
                }
            }
            dt.Clear();
            dt.Dispose();
            return true;
        }
    }
}
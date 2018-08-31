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
using System.Data;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Text;
using JumboECMS.Common;
namespace JumboECMS.UI
{
    /// <summary>
    /// BasicPage 的摘要说明
    /// </summary>
    public class BasicPage : JumboECMS.DBUtility.UI.PageUI
    {
        public string Edition = "Standard";//版本类型，请勿改，开源版改了也没啥用
        public string Version = "1.6.1.0204";
        public string vbCrlf = "\r\n";//换行符
        public bool NeedLicense = false;//是否需要许可证(对IP访问不限制)
        public string CategoryTable = "jcms_normal_category1";
        private string _dbType = "0";
        protected JumboECMS.Entity.Site site = new JumboECMS.Entity.Site();
        override protected void OnInit(EventArgs e)
        {
            Server.ScriptTimeout = 90;//默认脚本过期时间
            LoadJumboECMS();
            base.OnInit(e);

        }
        public void LoadJumboECMS()
        {
            this.ConnectDb();
            if (System.Web.HttpContext.Current.Application["jecmsV161"] == null)
            {
                SetupSystemDate();
            }
            if (System.Configuration.ConfigurationManager.AppSettings["UsingTestData"] == "0")
                CategoryTable = "jcms_normal_category0";
            site = (JumboECMS.Entity.Site)System.Web.HttpContext.Current.Application["jecmsV161"];
            if (site.Url.Contains("jumbo") || site.Url.Contains("localhost") || site.Url.Contains("127.0.0.1")) Edition = "All";
        }
        /// <summary>
        /// 数据库类型,0代表Access,1代表Sql Server
        /// </summary>
        public string DBType
        {
            get { return this._dbType.ToString(); }
            set { this._dbType = value; }
        }
        public string ORDER_BY_RND()
        {
            /*Access版本的随机没Sql Server的好，凑合着用吧
             * */
            if (DBType == "0")
            {
                Random rand = new Random((int)DateTime.Now.Ticks);
                return " ORDER BY rnd(-(id+" + rand.Next(99999) + "))";
            }
            else
                return " ORDER BY newid()";
        }
        /// <summary>
        /// 连接数据库
        /// </summary>
        public override void ConnectDb()
        {
            if (doh == null)
            {
                try
                {
                    if (System.Web.HttpContext.Current.Application["jecmsV161_dbType"] == null)
                    {
                        System.Web.HttpContext.Current.Application.Lock();
                        System.Web.HttpContext.Current.Application["jecmsV161_dbType"] = JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbType");
                        System.Web.HttpContext.Current.Application.UnLock();
                    }
                    this._dbType = System.Web.HttpContext.Current.Application["jecmsV161_dbType"].ToString();
                    if (this._dbType == "0")
                    {
                        if (System.Web.HttpContext.Current.Application["jecmsV161_dbPath"] == null)
                        {
                            string dbPath = JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbPath");
                            System.Web.HttpContext.Current.Application.Lock();
                            System.Web.HttpContext.Current.Application["jecmsV161_dbPath"] = dbPath;
                            System.Web.HttpContext.Current.Application.UnLock();
                        }
                        doh = new JumboECMS.DBUtility.OleDbOperHandler(HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Application["jecmsV161_dbPath"].ToString()));
                    }
                    else
                    {
                        this._dbType = "1";
                        if (System.Web.HttpContext.Current.Application["jecmsV161_dbConnStr"] == null)
                        {
                            string dbServerIP = JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbServerIP");
                            string dbLoginName = JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbLoginName");
                            string dbLoginPass = JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbLoginPass");
                            string dbName = JumboECMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbName");
                            string dbConnStr = "Data Source=" + dbServerIP + ";Initial Catalog=" + dbName + ";User ID=" + dbLoginName + ";Password=" + dbLoginPass + ";Pooling=true";
                            System.Web.HttpContext.Current.Application.Lock();
                            System.Web.HttpContext.Current.Application["jecmsV161_dbConnStr"] = dbConnStr;
                            System.Web.HttpContext.Current.Application.UnLock();
                        }
                        System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Web.HttpContext.Current.Application["jecmsV161_dbConnStr"].ToString());
                        doh = new JumboECMS.DBUtility.SqlDbOperHandler(conn);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void CloseDB()
        {
            if (doh != null) doh.Dispose();
        }
        /// <summary>
        /// 生成随机数字字符串
        /// </summary>
        /// <param name="int_NumberLength">数字长度</param>
        /// <returns></returns>
        public string GetRandomNumberString(int int_NumberLength)
        {
            return GetRandomNumberString(int_NumberLength, false);
        }
        /// <summary>
        /// 生成随机数字字符串
        /// </summary>
        /// <param name="int_NumberLength">数字长度</param>
        /// <returns></returns>
        public string GetRandomNumberString(int int_NumberLength, bool onlyNumber)
        {
            Random random = new Random();
            return GetRandomNumberString(int_NumberLength, onlyNumber, random);
        }
        /// <summary>
        /// 生成随机数字字符串
        /// </summary>
        /// <param name="int_NumberLength">数字长度</param>
        /// <returns></returns>
        public string GetRandomNumberString(int int_NumberLength, bool onlyNumber, Random random)
        {
            string strings = "123456789";
            if (!onlyNumber) strings += "abcdefghjkmnpqrstuvwxyz";
            char[] chars = strings.ToCharArray();
            string returnCode = string.Empty;
            for (int i = 0; i < int_NumberLength; i++)
                returnCode += chars[random.Next(0, chars.Length)].ToString();
            return returnCode;
        }
        /// <summary>
        /// 生成产品订单号，全站统一格式
        /// </summary>
        /// <returns></returns>
        public string GetProductOrderNum()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss") + GetRandomNumberString(4, true);
        }
        public void DownloadFile(string _filePath)
        {
            Response.Redirect(_filePath);
            return;
            //暂时不用如下方法
            /*
            Response.Clear();
            bool success = true;
            if (_filePath.StartsWith("http://") || _filePath.StartsWith("https://") || _filePath.StartsWith("ftp://"))
                Response.Redirect(_filePath);
            else if (!JumboECMS.Utils.DirFile.FileExists(_filePath))
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(HttpContext.Current.Server.MapPath("~/_data/log/nofile_" + DateTime.Now.ToString("yyyyMMdd") + ".log"), true, System.Text.Encoding.UTF8);
                sw.WriteLine(System.DateTime.Now.ToString());
                sw.WriteLine("\tIP 地 址：" + Const.GetUserIp);
                sw.WriteLine("\t访 问 者：" + ThisUser());
                sw.WriteLine("\t浏 览 器：" + HttpContext.Current.Request.Browser.Browser + HttpContext.Current.Request.Browser.Version);
                sw.WriteLine("\t下载页面：" + ServerUrl() + Const.GetCurrentUrl);
                sw.WriteLine("\t无效文件：" + _filePath);
                sw.WriteLine("---------------------------------------------------------------------------------------------------");
                sw.Close();
                Response.Write("指定的文件不存在,请通知管理员");
            }
            else
            {
                success = JumboECMS.Utils.DirFile.DownloadFile(Request, Response, Server.MapPath(_filePath), 1024000);
                if (!success)
                {
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(HttpContext.Current.Server.MapPath("~/_data/log/downerror_" + DateTime.Now.ToString("yyyyMMdd") + ".log"), true, System.Text.Encoding.UTF8);
                    sw.WriteLine(System.DateTime.Now.ToString());
                    sw.WriteLine("\tIP 地 址：" + Const.GetUserIp);
                    sw.WriteLine("\t访 问 者：" + ThisUser());
                    sw.WriteLine("\t浏 览 器：" + HttpContext.Current.Request.Browser.Browser + HttpContext.Current.Request.Browser.Version);
                    sw.WriteLine("\t下载页面：" + ServerUrl() + Const.GetCurrentUrl);
                    sw.WriteLine("\t失败文件：" + _filePath);
                    sw.WriteLine("---------------------------------------------------------------------------------------------------");
                    sw.Close();
                    Response.Redirect(_filePath);
                }
            }
            Response.End();
             * */
        }
        /// <summary>
        /// 当前访客
        /// </summary>
        public string ThisUser()
        {
            if (JumboECMS.Utils.Cookie.GetValue(site.CookiePrev + "user") != null)
                return JumboECMS.Utils.Cookie.GetValue(site.CookiePrev + "user", "name");
            else
                return "游客";
        }
        /// <summary>
        /// 简单的防止站外提交表单
        /// 仿一般黑客，防不住高手
        /// </summary>
        /// <returns></returns>
        public bool CheckFormUrl()
        {
            if (q("debugkey") == site.DebugKey) return true;
            if (HttpContext.Current.Request.UrlReferrer == null)
            {
                SaveVisitLog(2, 0);
                return false;
            }
            if ((HttpContext.Current.Request.UrlReferrer.Host) != (HttpContext.Current.Request.Url.Host))
            {
                SaveVisitLog(2, 0);
                return false;
            }
            return true;
        }
        /// <summary>
        /// 处理过程完成
        /// </summary>
        /// <param name="pageMsg">页面提示信息</param>
        /// <param name="go2Url">如果倒退步数为0，就转到该地址</param>
        /// <param name="BackStep">倒退步数</param>
        protected void FinalMessage(string pageMsg, string go2Url, int BackStep)
        {
            FinalMessage(pageMsg, go2Url, BackStep, 2);
        }
        /// <summary>
        /// 处理过程完成
        /// </summary>
        /// <param name="pageMsg">页面提示信息</param>
        /// <param name="go2Url">如果倒退步数为0，就转到该地址</param>
        /// <param name="BackStep">倒退步数</param>
        /// <param name="BackStep">自动转向的秒数</param>
        protected void FinalMessage(string pageMsg, string go2Url, int BackStep, int Seconds)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>\r\n");
            sb.Append("<html xmlns='http://www.w3.org/1999/xhtml'>\r\n");
            sb.Append("<head>\r\n");
            sb.Append("<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />\r\n");
            sb.Append("<title>系统提示</title>\r\n");
            sb.Append("<style>\r\n");
            sb.Append("body {padding:0; margin:0; background-color:#fff;}\r\n");
            sb.Append("#info{padding:0; margin:0;position: absolute;width:320px;height:120px;margin-top:-60px;margin-left:-160px; left:50%;top:50%; border:0px #B4E0F7 solid; text-align:center;}\r\n");
            sb.Append("</style>\r\n");
            sb.Append("<script language=\"javascript\">\r\n");
            sb.Append("var seconds=" + Seconds + ";\r\n");
            sb.Append("for(i=1;i<=seconds;i++)\r\n");
            sb.Append("{window.setTimeout(\"update(\" + i + \")\", i * 1000);}\r\n");
            sb.Append("function update(num)\r\n");
            sb.Append("{\r\n");
            sb.Append("if(num == seconds)\r\n");
            if (BackStep > 0)
                sb.Append("{ history.go(" + (0 - BackStep) + "); }\r\n");
            else
            {
                if (go2Url != "")
                    sb.Append("{ self.location.href='" + go2Url + "'; }\r\n");
                else
                    sb.Append("{window.close();}\r\n");
            }
            sb.Append("else\r\n");
            sb.Append("{ }\r\n");
            sb.Append("}\r\n");
            sb.Append("</script>\r\n");
            sb.Append("<base target='_self' />\r\n");
            sb.Append("</head>\r\n");
            sb.Append("<body>\r\n");
            sb.Append("<div id='info'>\r\n");
            sb.Append("<div style='text-align:center;margin:0 auto;width:320px; line-height:26px;height:26px;font-weight:bold;color:#444444;font-size:14px;border:1px #D1D1D1 solid;background:#F5F5F5;'>提示信息</div>\r\n");
            sb.Append("<div style='text-align:center;padding:20px 0 20px 0;margin:0 auto;width:320px;font-size:14px;background:#FFFFFF;border-right:1px #D1D1D1 solid;border-bottom:1px #D1D1D1 solid;border-left:1px #D1D1D1 solid;'>\r\n");
            sb.Append(pageMsg + "<br /><br />\r\n");
            if (BackStep > 0)
                sb.Append("        <a href=\"javascript:history.go(" + (0 - BackStep) + ")\">如果您的浏览器没有自动跳转，请点击这里</a>\r\n");
            else
                sb.Append("        <a href=\"" + go2Url + "\">如果您的浏览器没有自动跳转，请点击这里</a>\r\n");
            sb.Append("    </div>\r\n");
            sb.Append("</div>\r\n");
            sb.Append("</body>\r\n");
            sb.Append("</html>\r\n");
            HttpContext.Current.Response.Write(sb.ToString());
            //以下这行千万别手痒痒删掉
            HttpContext.Current.Response.End();

        }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int Int_ThisPage()
        {
            int _page = Str2Int(q("page"), 0) < 1 ? 1 : Str2Int(q("page"), 0);
            return _page;
        }

        /// <summary>
        /// 执行Sql脚本文件
        /// </summary>
        /// <param name="pathToScriptFile">物理路径</param>
        /// <returns></returns>
        public bool ExecuteSqlInFile(string pathToScriptFile)
        {
            if (this._dbType == "1")
                return JumboECMS.Utils.ExecuteSqlBlock.Go("1", System.Web.HttpContext.Current.Application["jecmsV161_dbConnStr"].ToString(), pathToScriptFile);
            else
                return JumboECMS.Utils.ExecuteSqlBlock.Go("0", "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath(System.Web.HttpContext.Current.Application["jecmsV161_dbPath"].ToString()), pathToScriptFile);
        }
        /// <summary>
        /// 附加被选择的字段
        /// </summary>
        /// <param name="_fields">格式为[字段1],[字段2]</param>
        /// <returns></returns>
        public static string JoinFields(string _fields)
        {
            if (_fields.Trim().Length == 0)
                return "";
            else
                return "," + _fields;
        }

        /// <summary>
        /// 页面访问超时后记录日志
        /// </summary>
        /// <param name="_second">超时秒数</param>
        public void SavePageLog(int _second)
        {
            SaveVisitLog(1, _second);
        }
        /// <summary>
        /// 保存访问日志
        /// </summary>
        /// <param name="_type">1代表访问者,2代表非法</param>
        /// <param name="_second">脚本秒数</param>
        public void SaveVisitLog(int _type, int _second)
        {
            SaveVisitLog(_type, _second, "");
        }
        /// <summary>
        /// 保存访问日志
        /// </summary>
        /// <param name="_type">1代表访问者,2代表非法</param>
        /// <param name="_second">脚本秒数</param>
        /// <param name="_logfilename">自定义log保存路径</param>
        public void SaveVisitLog(int _type, int _second, string _logfilename)
        {
            if (_type == 1)
            {
                string _savefile = _logfilename == "" ? "~/_data/log/vister_" + DateTime.Now.ToString("yyyyMMdd") + ".log" : _logfilename;
                Single s = (Single)DateTime.Now.Subtract(HttpContext.Current.Timestamp).TotalSeconds;
                if (s > _second)
                {
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(HttpContext.Current.Server.MapPath(_savefile), true, System.Text.Encoding.UTF8);
                    sw.WriteLine(System.DateTime.Now.ToString());
                    sw.WriteLine("\tIP 地 址：" + Const.GetUserIp);
                    sw.WriteLine("\t访 问 者：" + ThisUser());
                    sw.WriteLine("\t浏 览 器：" + HttpContext.Current.Request.Browser.Browser + HttpContext.Current.Request.Browser.Version);
                    sw.WriteLine("\t耗    时：" + ((Single)DateTime.Now.Subtract(HttpContext.Current.Timestamp).TotalSeconds).ToString("0.000") + "秒");
                    sw.WriteLine("\t地    址：" + ServerUrl() + Const.GetCurrentUrl);
                    sw.WriteLine("---------------------------------------------------------------------------------------------------");
                    sw.Close();
                    sw.Dispose();
                }
            }
            else
            {
                string _savefile = _logfilename == "" ? "~/_data/log/hacker_" + DateTime.Now.ToString("yyyyMMdd") + ".log" : _logfilename;
                System.IO.StreamWriter sw = new System.IO.StreamWriter(HttpContext.Current.Server.MapPath(_savefile), true, System.Text.Encoding.UTF8);
                sw.WriteLine(System.DateTime.Now.ToString());
                sw.WriteLine("\tIP 地 址：" + Const.GetUserIp);
                sw.WriteLine("\t访 问 者：" + ThisUser());
                sw.WriteLine("\t浏 览 器：" + HttpContext.Current.Request.Browser.Browser + HttpContext.Current.Request.Browser.Version);
                sw.WriteLine("\t来    源：" + ServerUrl() + Const.GetRefererUrl);
                sw.WriteLine("\t地    址：" + ServerUrl() + Const.GetCurrentUrl);
                sw.WriteLine("---------------------------------------------------------------------------------------------------");
                sw.Close();
                sw.Dispose();
            }
        }
        /// <summary>
        /// 服务器地址
        /// </summary>
        /// <returns></returns>
        protected string ServerUrl()
        {
            if (HttpContext.Current.Request.ServerVariables["Server_Port"].ToString() == "80")
                return "http://" + HttpContext.Current.Request.Url.Host;
            else
                return "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.ServerVariables["Server_Port"].ToString();
        }
        /// <summary>
        /// 产生随机数字字符串
        /// </summary>
        /// <returns></returns>
        public string RandomStr(int Num)
        {
            int number;
            char code;
            string returnCode = String.Empty;

            Random random = new Random();

            for (int i = 0; i < Num; i++)
            {
                number = random.Next();
                code = (char)('0' + (char)(number % 10));
                returnCode += code.ToString();
            }
            return returnCode;
        }
        /// <summary>
        /// 初始化系统信息
        /// </summary>
        protected void SetupSystemDate()
        {
            site = new JumboECMS.DAL.SiteDAL().GetEntity();
            System.Web.HttpContext.Current.Application.Lock();
            System.Web.HttpContext.Current.Application["jecmsV161"] = site;
            System.Web.HttpContext.Current.Application.UnLock();
        }
        /// <summary>
        /// 输出js
        /// </summary>
        /// <param name="sType"></param>
        /// <param name="jsContent"></param>
        protected void WriteJs(string sType, string jsContent)
        {
            if (sType == "-1")
                Page.ClientScript.RegisterStartupScript(this.GetType(), "writejs", jsContent, true);
            else
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "writejs", jsContent, true);

        }
        /// <summary>
        /// 输出json结果
        /// </summary>
        /// <param name="success">是否操作成功,0表示失败;1表示成功</param>
        /// <param name="str">输出字符串</param>
        /// <returns></returns>
        protected string JsonResult(int success, string str)
        {
            return "{\"result\" :\"" + success.ToString() + "\",\"returnval\" :\"" + str + "\"}";

        }
        /// <summary>
        /// 高光显示关键字
        /// </summary>
        /// <param name="PageStr">内容</param>
        /// <param name="keys">关键字</param>
        /// <returns></returns>
        protected string p__HighLight(string PageStr, string keys)
        {
            string[] key = keys.Split(new string[] { " " }, StringSplitOptions.None);
            for (int i = 0; i < key.Length; i++)
            {
                PageStr = PageStr.Replace(key[i].Trim(), "<font color=#C60A00>" + key[i].Trim() + "</font>");
            }
            return PageStr;
        }
        /// <summary>
        /// 替换关键字为红色
        /// </summary>
        /// <param name="pain">原始内容</param>
        /// <param name="keyword">关键字，支持多关键字</param>
        protected string HighLightKeyWord(string pain, string keys)
        {
            string _pain = pain;
            string[] key = keys.Split(new string[] { " " }, StringSplitOptions.None);
            if (key.Length < 1)
                return _pain;
            for (int i = 0; i < key.Length; i++)
            {
                System.Text.RegularExpressions.MatchCollection m = System.Text.RegularExpressions.Regex.Matches(_pain, key[i].Trim(), System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                //忽略大小写搜索字符串中的关键字
                for (int j = 0; j < m.Count; j++)//循环在匹配的子串前后插东东
                {
                    //j×31为插入html标签使pain字符串增加的长度:
                    _pain = _pain.Insert((m[j].Index + key[i].Trim().Length + j * 31), "</span>");//关键字后插入html标签
                    _pain = _pain.Insert((m[j].Index + j * 31), "<span style=\"color:red\">");//关键字前插入html标签
                }
            }
            return _pain;
        }
        /// <summary>
        /// 获得逐级缩进的栏目名
        /// </summary>
        /// <param name="sName">栏目名</param>
        /// <param name="sCode">栏目code</param>
        /// <returns>逐级缩进的栏目名</returns>
        public string getListName(string sName, string sCode)
        {
            int Level = (sCode.Length / 4 - 1);
            string sStr = "";
            if (Level > 0)
            {
                for (int i = 0; i < Level; i++)
                    sStr += "├－";
            }
            return sStr + sName;
        }

        /// <summary>
        /// 通用分页
        /// </summary>
        /// <param name="mode">支持1=simple,2=normal,3=full</param>
        /// <param name="countNum">记录数</param>
        /// <param name="currentPage">第几页</param>
        /// <param name="FieldName">参数名</param>
        /// <param name="FieldValue">参数值</param>
        /// <returns></returns>
        public string PageList(int mode, int countNum, int PSize, int currentPage, string[] FieldName, string[] FieldValue)
        {
            string Script_Name = HttpContext.Current.Request.ServerVariables["Script_Name"].ToString();
            string pString = "";
            for (int i = 0; i < FieldName.Length; i++)
            {
                pString += FieldName[i].ToString() + "=" + FieldValue[i].ToString() + "&";
            }
            string Http = Script_Name + "?" + pString + "page=*";
            return JumboECMS.Utils.HtmlPager.GetPageBar(mode, "html", 0, countNum, PSize, currentPage, Http);
        }
        /// <summary>
        /// 智能分页
        /// </summary>
        /// <param name="mode">支持1=simple,2=normal,3=full</param>
        /// <param name="countNum">记录数</param>
        /// <param name="currentPage">第几页</param>
        /// <returns></returns>
        public string AutoPageBar(int mode, int stepNum, int countNum, int PSize, int currentPage)
        {
            string Http = GetUrlPrefix() + "*";
            return JumboECMS.Utils.HtmlPager.GetPageBar(mode, "html", stepNum, countNum, PSize, currentPage, Http);
        }
        /// <summary>
        /// 当前地址前缀
        /// </summary>
        public string GetUrlPrefix()
        {
            HttpRequest Request = HttpContext.Current.Request;
            string strUrl;
            strUrl = HttpContext.Current.Request.ServerVariables["Url"];
            if (HttpContext.Current.Request.QueryString.Count == 0) //如果无参数
                return strUrl + "?page=";
            else
            {
                //if (JumboECMS.Utils.Strings.Left(HttpContext.Current.Request.ServerVariables["Query_String"], 5) == "page=")//只有页参数
                if (HttpContext.Current.Request.ServerVariables["Query_String"].StartsWith("page=", StringComparison.OrdinalIgnoreCase))//只有页参数
                    return strUrl + "?page=";
                else
                {
                    string[] strUrl_left;
                    strUrl_left = HttpContext.Current.Request.ServerVariables["Query_String"].Split(new string[] { "page=" }, StringSplitOptions.None);
                    if (strUrl_left.Length == 1)//没有页参数
                        return strUrl + "?" + strUrl_left[0] + "&page=";
                    else
                        return strUrl + "?" + strUrl_left[0] + "page=";
                }

            }

        }
        /// <summary>
        /// 获得翻页Bar，适合js和html
        /// </summary>
        /// <param name="mode">支持1=simple,2=normal,3=full</param>
        /// <param name="stype"></param>
        /// <param name="countNum"></param>
        /// <param name="PSize"></param>
        /// <param name="currentPage"></param>
        /// <param name="Http"></param>
        /// <returns></returns>
        public string getPageBar(int mode, string stype, int stepNum, int countNum, int PSize, int currentPage, string HttpN)
        {
            return JumboECMS.Utils.HtmlPager.GetPageBar(mode, stype, stepNum, countNum, PSize, currentPage, HttpN);
        }
        /// <summary>
        /// 获得翻页Bar，适合js和html
        /// </summary>
        /// <param name="mode">支持1=simple,2=normal,3=full</param>
        /// <param name="stype"></param>
        /// <param name="stepNum"></param>
        /// <param name="countNum"></param>
        /// <param name="PSize"></param>
        /// <param name="currentPage"></param>
        /// <param name="Http1"></param>
        /// <param name="HttpM"></param>
        /// <param name="HttpN"></param>
        /// <param name="limitPage"></param>
        /// <returns></returns>
        public string getPageBar(int mode, string stype, int stepNum, int countNum, int PSize, int currentPage, string Http1, string HttpN)
        {
            return JumboECMS.Utils.HtmlPager.GetPageBar(mode, stype, stepNum, countNum, PSize, currentPage, Http1, HttpN);
        }

        /// <summary>
        /// 获取querystring
        /// </summary>
        /// <param name="s">参数名</param>
        /// <returns>返回值</returns>
        public string q(string s)
        {
            if (HttpContext.Current.Request.QueryString[s] != null && HttpContext.Current.Request.QueryString[s] != "")
            {
                return JumboECMS.Utils.Strings.SafetyStr(HttpContext.Current.Request.QueryString[s].ToString());
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取post得到的参数
        /// </summary>
        /// <param name="s">参数名</param>
        /// <returns>返回值</returns>
        public string f(string s)
        {
            if (HttpContext.Current.Request.Form[s] != null && HttpContext.Current.Request.Form[s] != "")
            {
                return HttpContext.Current.Request.Form[s].ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// 返回非负整数，默认为t
        /// </summary>
        /// <param name="s">参数值</param>
        /// <returns>返回值</returns>
        public int Str2Int(string s, int t)
        {
            return JumboECMS.Utils.Validator.StrToInt(s, t);
        }

        /// <summary>
        /// 返回非负整数，默认为0
        /// </summary>
        /// <param name="s">参数值</param>
        /// <returns>返回值</returns>
        public int Str2Int(string s)
        {
            return Str2Int(s, 0);
        }

        /// <summary>
        /// 返回非空字符串，默认为"0"
        /// </summary>
        /// <param name="s">参数值</param>
        /// <returns>返回值</returns>
        public string Str2Str(string s)
        {
            return JumboECMS.Utils.Validator.StrToInt(s, 0).ToString();
        }
        /// <summary>
        /// 字符串长度
        /// </summary>
        protected int GetStringLen(string str)
        {
            byte[] bs = System.Text.Encoding.Default.GetBytes(str);
            return bs.Length;
        }
        /// <summary>
        /// 字符串截断
        /// </summary>
        /// <param name="str"></param>
        /// <param name="Length">以汉字计算，比如Length为100表示取200个字符，100个汉字</param>
        /// <returns></returns>
        protected string GetCutString(string str, int Length)
        {
            Length *= 2;
            byte[] bs = System.Text.Encoding.Default.GetBytes(str);
            if (bs.Length <= Length)
            {
                return str;
            }
            else
            {
                return System.Text.Encoding.Default.GetString(bs, 0, Length);
            }
        }
        #region 保存Js文件
        /// <summary>
        /// 保存js文件
        /// </summary>
        /// <param name="TxtStr">文件内容</param>
        /// <param name="TxtFile">输出路径，物理路径</param>
        protected void SaveJsFile(string TxtStr, string TxtFile)
        {
            SaveJsFile(TxtStr, TxtFile, "2");
        }
        /// <summary>
        /// 保存js文件
        /// </summary>
        /// <param name="TxtStr">文件内容</param>
        /// <param name="TxtFile">输出路径，物理路径</param>
        /// <param name="Edcode">编码：1=gb2312,2=utf-8,3=unicode</param>
        protected void SaveJsFile(string TxtStr, string TxtFile, string Edcode)
        {
            System.Text.Encoding FileType = System.Text.Encoding.Default;
            switch (Edcode)
            {
                case "3":
                    FileType = System.Text.Encoding.Unicode;
                    break;
                case "2":
                    FileType = System.Text.Encoding.UTF8;
                    break;
                case "1":
                    FileType = System.Text.Encoding.GetEncoding("GB2312");
                    break;
            }
            JumboECMS.Utils.DirFile.CreateFolder(JumboECMS.Utils.DirFile.GetFolderPath(TxtFile));
            System.IO.StreamWriter sw = new System.IO.StreamWriter(TxtFile, false, FileType);
            sw.Write("/*本文件由jcms于 " + System.DateTime.Now.ToString() + " 自动生成,请勿手动修改*/\r\n" + TxtStr);
            sw.Close();
        }
        #endregion
        #region 保存Css文件
        /// <summary>
        /// 保存Css文件
        /// </summary>
        /// <param name="TxtStr">文件内容</param>
        /// <param name="TxtFile">输出路径，物理路径</param>
        protected void SaveCssFile(string TxtStr, string TxtFile)
        {
            SaveCssFile(TxtStr, TxtFile, "2");
        }
        /// <summary>
        /// 保存Css文件
        /// </summary>
        /// <param name="TxtStr">文件内容</param>
        /// <param name="TxtFile">输出路径，物理路径</param>
        /// <param name="Edcode">编码：1=gb2312,2=utf-8,3=unicode</param>
        protected void SaveCssFile(string TxtStr, string TxtFile, string Edcode)
        {
            System.Text.Encoding FileType = System.Text.Encoding.Default;
            switch (Edcode)
            {
                case "3":
                    FileType = System.Text.Encoding.Unicode;
                    break;
                case "2":
                    FileType = System.Text.Encoding.UTF8;
                    break;
                case "1":
                    FileType = System.Text.Encoding.GetEncoding("GB2312");
                    break;
            }
            JumboECMS.Utils.DirFile.CreateFolder(JumboECMS.Utils.DirFile.GetFolderPath(TxtFile));
            System.IO.StreamWriter sw = new System.IO.StreamWriter(TxtFile, false, FileType);
            sw.Write("/*本文件由jcms于 " + System.DateTime.Now.ToString() + " 自动生成,请勿手动修改*/\r\n" + TxtStr);
            sw.Close();
        }
        #endregion
        #region 处理Cache文件
        /// <summary>
        /// 读取Cache文件并保存到Html文件
        /// </summary>
        /// <param name="CacheStr">缓存内容</param>
        /// <param name="OutFile">输出路径，物理路径</param>
        protected void SaveCacheFile(string CacheStr, string OutFile)
        {
            SaveCacheFile(CacheStr, OutFile, "2");
        }
        /// <summary>
        /// 保存Cache文件
        /// </summary>
        /// <param name="CacheStr">缓存内容</param>
        /// <param name="OutFile">输出路径，物理路径</param>
        /// <param name="Edcode">编码：1=gb2312,2=utf-8,3=unicode</param>
        protected void SaveCacheFile(string CacheStr, string OutFile, string Edcode)
        {
            System.Text.Encoding FileType = System.Text.Encoding.Default;
            switch (Edcode)
            {
                case "3":
                    FileType = System.Text.Encoding.Unicode;
                    break;
                case "2":
                    FileType = System.Text.Encoding.UTF8;
                    break;
                case "1":
                    FileType = System.Text.Encoding.GetEncoding("GB2312");
                    break;
            }
            JumboECMS.Utils.DirFile.CreateFolder(JumboECMS.Utils.DirFile.GetFolderPath(OutFile));
            try
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(OutFile, false, FileType);
                //下面这行测试所用，可以注释
                //CacheStr += "\r\n<!--Published " + System.DateTime.Now.ToString() + "-->";
                sw.Write(CacheStr);
                sw.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// <summary>
        /// 链接到站点首页
        /// </summary>
        public string Go2Site()
        {
            return (new JumboECMS.DAL.Common(true)).Go2Site();
        }
        /// <summary>
        /// 链接到栏目页
        /// </summary>
        /// <param name="_categoryid"></param>
        /// <param name="_page"></param>
        /// <returns></returns>
        public string Go2Category(string _categoryid, int _page)
        {
            return (new JumboECMS.DAL.Normal_CategoryDAL()).GetCategoryLink(_categoryid, _page);
        }
        /// <summary>
        /// 链接到内容页
        /// </summary>
        /// <param name="_moduletype"></param>
        /// <param name="_contentid"></param>
        /// <param name="_page"></param>
        /// <returns></returns>
        public string Go2View(string _moduletype, string _contentid, int _page)
        {
            return (new JumboECMS.DAL.Common(true)).Go2View(_moduletype, _contentid, _page);
        }
        /// <summary>
        /// 解析一般模板标签
        /// </summary>
        /// <param name="PageStr"></param>
        /// <returns></returns>
        protected string ExecuteCommonTags(string PageStr)
        {
            JumboECMS.DAL.TemplateEngineDAL teDAL = new JumboECMS.DAL.TemplateEngineDAL();
            teDAL.IsHtml = site.IsHtml;
            teDAL.ReplacePublicTag(ref PageStr);
            teDAL.ReplaceCategoryLoopTag(ref PageStr);
            teDAL.ReplaceContentLoopTag(ref PageStr);
            return PageStr;
        }
        #region 生成静态页面
        /// <summary>
        /// 生成栏目文件
        /// </summary>
        /// <param name="_categoryId"></param>
        /// <param name="CreateParent"></param>

        protected void CreateCategoryFile(string _categoryId, bool CreateParent)
        {
            JumboECMS.DAL.TemplateEngineDAL teDAL = new JumboECMS.DAL.TemplateEngineDAL();
            int pageCount = new JumboECMS.DAL.Normal_CategoryDAL().GetContetPageCount(_categoryId, true);
            string PageStr = string.Empty;
            for (int i = 1; i < (pageCount + 1); i++)
            {
                PageStr = teDAL.GetSiteCategoryPage(_categoryId, i);
                JumboECMS.Utils.DirFile.SaveFile(PageStr, Go2Category(_categoryId, i));
            }
            doh.Reset();
            doh.SqlCmd = "SELECT Id, ParentId FROM [" + CategoryTable + "] WHERE [Id]=" + _categoryId;
            DataTable dtCategory = doh.GetDataTable();
            if (dtCategory.Rows.Count > 0 && dtCategory.Rows[0]["ParentId"].ToString() != "0" && CreateParent == true)
            {
                CreateCategoryFile(dtCategory.Rows[0]["ParentId"].ToString(), true);
            }
            dtCategory.Clear();
            dtCategory.Dispose();

        }

        /// <summary>
        /// 生成内容页
        /// </summary>
        /// <param name="_contentID">内容ID</param>
        /// <param name="_currentPage">指定的页码,-1表示所有</param>
        protected void CreateContentFile(JumboECMS.Entity.Normal_Module _module, string _contentID, int _currentPage)
        {
            JumboECMS.DAL.ModuleCommand.CreateContent(_module.Type, _contentID, _currentPage);
        }
        #endregion
        /// <summary>
        /// 格式化标签
        /// </summary>
        /// <param name="_tags"></param>
        /// <param name="_title"></param>
        /// <param name="_autosplit"></param>
        /// <returns></returns>
        public string FormatTags(string _tags, string _title, bool _autosplit)
        {
            string _tag = _tags;
            if (_autosplit && _tag == "")
                _tag = JumboECMS.Utils.WordSpliter.GetKeyword(_title, ",");
            else
                _tag = JumboECMS.Utils.Strings.SafetyStr(_tag);
            string[] _taglist = _tag.Split(',');
            string _returnTags = "";
            int _returnNum = 0;
            for (int i = 0; i < _taglist.Length; i++)
            {
                if (_taglist[i].Length > 1 && _returnNum < 4)
                {
                    if (_returnTags.Length == 0)
                        _returnTags = _taglist[i].Trim();
                    else
                        _returnTags += "," + _taglist[i].Trim();
                    _returnNum++;
                }
            }
            return _returnTags;
        }

        /// <summary>
        /// 频道管理权限菜单
        /// </summary>
        /// <returns></returns>
        protected string[] powerMenu()
        {
            //实际权限为前面加频道ID
            string[] menu = new string[10];
            menu[0] = "内容浏览|manage";
            menu[1] = "内容录入|add";
            menu[2] = "内容修改|edit";
            menu[3] = "内容删除|delete";
            menu[4] = "内容审核|audit";
            return menu;
        }
    }
}

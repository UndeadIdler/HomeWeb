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
using System.Configuration;
using System.Web;
using System.Data;
using System.Text;
using System.Collections;
namespace JumboECMS.Common
{
    /// <summary>
    /// 发送邮件类
    /// </summary>
    public static class MailHelp
    {
        public static JumboECMS.Entity.MailServer MailServer()
        {
            JumboECMS.Entity.Site site = (JumboECMS.Entity.Site)System.Web.HttpContext.Current.Application["jecmsV161"];
            System.Collections.IList _FromAddresss = new System.Collections.ArrayList();
            System.Collections.IList _FromNames = new System.Collections.ArrayList();
            System.Collections.IList _FromPwds = new System.Collections.ArrayList();
            System.Collections.IList _SmtpHosts = new System.Collections.ArrayList();
            System.Collections.IList _Useds = new System.Collections.ArrayList();
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/jcms(emailserver).config");
            JumboECMS.DBUtility.XmlControl XmlTool = new JumboECMS.DBUtility.XmlControl(strXmlFile);
            DataTable dtTemp = XmlTool.GetTable("Mails");
            XmlTool.Dispose();
            if (dtTemp.Rows.Count > 0)
            {
                dtTemp.DefaultView.Sort = "Used ASC";
                DataTable dt = dtTemp.DefaultView.ToTable();
                dtTemp.Clear();
                dtTemp.Dispose();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (string.CompareOrdinal(dt.Rows[i]["Used"].ToString(), DateTime.Now.AddSeconds(0 - site.MailTimeCycle).ToString("yyyy-MM-dd HH:mm:ss")) <= 0)//确保周期内只发送一次
                    {
                        _FromAddresss.Add(dt.Rows[i]["FromAddress"].ToString());
                        _FromNames.Add(dt.Rows[i]["FromName"].ToString());
                        _FromPwds.Add(dt.Rows[i]["FromPwd"].ToString());
                        _SmtpHosts.Add(dt.Rows[i]["SmtpHost"].ToString());
                        _Useds.Add(dt.Rows[i]["Used"].ToString());
                    }
                }
                dt.Clear();
                dt.Dispose();

                JumboECMS.Entity.MailServer _MailServer = new JumboECMS.Entity.MailServer();
                _MailServer.FromAddresss = _FromAddresss;
                _MailServer.FromNames = _FromNames;
                _MailServer.FromPwds = _FromPwds;
                _MailServer.SmtpHosts = _SmtpHosts;
                _MailServer.Useds = _Useds;
                return _MailServer;
            }
            else
            {
                dtTemp.Clear();
                dtTemp.Dispose();
                System.IO.StreamWriter sw = new System.IO.StreamWriter(HttpContext.Current.Server.MapPath("~/_data/log/mailerror.log"), true, System.Text.Encoding.UTF8);
                sw.WriteLine(System.DateTime.Now.ToString());
                sw.WriteLine("\tIP 地 址：" + Const.GetUserIp);
                sw.WriteLine("\t邮箱服务器配置失败");
                sw.WriteLine("---------------------------------------------------------------------------------------------------");
                sw.Close();
                return null;
            }
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="MailTo">接收人用户名,单封邮件</param>
        /// <param name="MailSubject">邮件主题</param>
        /// <param name="MailBody">邮件内容</param>
        /// <param name="IsHtml">邮件正文是否为HTML格式</param>
        /// <param name="MailFrom">发件人邮箱地址</param>
        /// <param name="MailFromName">发件人署名</param>
        /// <param name="MailPwd">发件人邮箱密码</param>
        /// <param name="MailSmtpHost">发件人邮箱Host,如"smtp.sina.com"</param>
        public static bool SendOK(string MailTo, string MailSubject, string MailBody, bool IsHtml, string MailFrom, string MailFromName, string MailPwd, string MailSmtpHost)
        {
            JumboECMS.Utils.Mail.MailMessage message = new JumboECMS.Utils.Mail.MailMessage();
            message.MaxRecipientNum = 80;//最大收件人
            message.From = System.Configuration.ConfigurationManager.AppSettings["JumboECMS:WebmasterEmail"];
            message.FromName = MailFromName;
            string[] _mail = MailTo.Split(',');
            for (int j = 0; j < _mail.Length; j++)
            {
                message.AddRecipients(_mail[j]);
            }
            message.Subject = MailSubject;
            if (IsHtml)
                message.BodyFormat = JumboECMS.Utils.Mail.MailFormat.HTML;
            else
                message.BodyFormat = JumboECMS.Utils.Mail.MailFormat.Text;
            message.Priority = JumboECMS.Utils.Mail.MailPriority.Normal;
            message.Body = MailBody;
            JumboECMS.Utils.Mail.SmtpClient smtp = new JumboECMS.Utils.Mail.SmtpClient(MailSmtpHost);
            if (smtp.Send(message, MailFrom, MailPwd))
                return true;
            else
            {
                SaveErrLog(MailTo, MailFrom, MailFromName, MailSmtpHost, smtp.ErrMsg);
                return false;
            }

            //try
            //{
            //    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            //    message.From = new System.Net.Mail.MailAddress(MailFrom, MailFromName, Encoding.GetEncoding("gb2312"));
            //    string[] _mail = MailTo.Split(',');
            //    for (int j = 0; j < _mail.Length; j++)
            //    {
            //        message.To.Add(_mail[j]);
            //    }
            //    message.Subject = MailSubject;
            //    message.Body = MailBody;
            //    message.IsBodyHtml = IsHtml;
            //    message.Priority = System.Net.Mail.MailPriority.Normal;
            //    message.BodyEncoding = Encoding.GetEncoding("gb2312");
            //    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(MailSmtpHost);
            //    smtp.Credentials = new System.Net.NetworkCredential(MailFrom, MailPwd);
            //    smtp.Send(message);
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            //return true;
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="MailTo">接收人用户名,单封邮件</param>
        /// <param name="MailSubject">邮件主题</param>
        /// <param name="MailBody">邮件内容</param>
        /// <param name="IsHtml">邮件正文是否为HTML格式</param>
        /// <param name="_MailServer"></param>
        /// <returns></returns>
        public static bool SendOK(string MailTo, string MailSubject, string MailBody, bool IsHtml, JumboECMS.Entity.MailServer _MailServer)
        {
            if (_MailServer == null)
                return false;
            if (_MailServer.FromAddresss.Count == 0)
                return false;
            bool _SendOK = false;
            for (int i = 0; i < _MailServer.FromAddresss.Count; i++)
            {
                JumboECMS.Utils.Mail.MailMessage message = new JumboECMS.Utils.Mail.MailMessage();
                message.MaxRecipientNum = 80;//最大收件人
                message.From = System.Configuration.ConfigurationManager.AppSettings["JumboECMS:WebmasterEmail"];
                message.FromName = _MailServer.FromNames[i].ToString();
                string[] _mail = MailTo.Split(',');
                for (int j = 0; j < _mail.Length; j++)
                {
                    message.AddRecipients(_mail[j]);
                }
                message.Subject = MailSubject;
                if (IsHtml)
                    message.BodyFormat = JumboECMS.Utils.Mail.MailFormat.HTML;
                else
                    message.BodyFormat = JumboECMS.Utils.Mail.MailFormat.Text;
                message.Priority = JumboECMS.Utils.Mail.MailPriority.Normal;
                message.Body = MailBody;
                JumboECMS.Utils.Mail.SmtpClient smtp = new JumboECMS.Utils.Mail.SmtpClient(_MailServer.SmtpHosts[i].ToString());
                if (smtp.Send(message, _MailServer.FromAddresss[i].ToString(), _MailServer.FromPwds[i].ToString()))
                {
                    string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/jcms(emailserver).config");
                    JumboECMS.DBUtility.XmlControl XmlTool = new JumboECMS.DBUtility.XmlControl(strXmlFile);
                    XmlTool.Update("Mails/Mail[FromAddress=\"" + _MailServer.FromAddresss[i].ToString() + "\"]/Used", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    XmlTool.Save();
                    XmlTool.Dispose();
                    _SendOK = true;
                    SaveSucLog(MailTo, _MailServer.FromAddresss[i].ToString(), _MailServer.FromNames[i].ToString(), _MailServer.SmtpHosts[i].ToString());
                    break;//跳出循环
                }
                else
                {
                    SaveErrLog(MailTo, _MailServer.FromAddresss[i].ToString(), _MailServer.FromNames[i].ToString(), _MailServer.SmtpHosts[i].ToString(), smtp.ErrMsg + "\r\n当前共有：" + _MailServer.FromAddresss.Count + "个发件人在队列中.");
                    continue;
                }
                //try
                //{
                //    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                //    message.From = new System.Net.Mail.MailAddress(_MailServer.FromAddresss[i].ToString(), _MailServer.FromNames[i].ToString(), Encoding.GetEncoding("gb2312"));
                //    string[] _mail = MailTo.Split(',');
                //    for (int j = 0; j < _mail.Length; j++)
                //    {
                //        message.To.Add(_mail[j]);
                //    }
                //    message.Subject = MailSubject;
                //    message.Body = MailBody;
                //    message.IsBodyHtml = IsHtml;
                //    message.Priority = System.Net.Mail.MailPriority.Normal;
                //    message.BodyEncoding = Encoding.GetEncoding("gb2312");
                //    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(_MailServer.SmtpHosts[i].ToString());
                //    smtp.Credentials = new System.Net.NetworkCredential(_MailServer.FromAddresss[i].ToString(), _MailServer.FromPwds[i].ToString());
                //    smtp.Send(message);
                //    string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/jcms(emailserver).config");
                //    JumboECMS.DBUtility.XmlControl XmlTool = new JumboECMS.DBUtility.XmlControl(strXmlFile);
                //    XmlTool.Update("Mails/Mail[FromAddress=\"" + _MailServer.FromAddresss[i].ToString() + "\"]/Used", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                //    XmlTool.Save();
                //    XmlTool.Dispose();
                //    _SendOK = true;
                //    break;//跳出循环
                //}
                //catch (Exception)
                //{ }
            }
            return _SendOK;
        }
        /// <summary>
        /// 保存正确日志
        /// </summary>
        /// <param name="MailFrom"></param>
        /// <param name="MailFromName"></param>
        /// <param name="MailSmtpHost"></param>
        private static void SaveSucLog(string MailTo, string MailFrom, string MailFromName, string MailSmtpHost)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(HttpContext.Current.Server.MapPath("~/_data/log/mailsuccess_" + DateTime.Now.ToString("yyyyMMdd") + ".txt"), true, System.Text.Encoding.UTF8);
            sw.WriteLine(System.DateTime.Now.ToString());
            sw.WriteLine("\t收 信 人：" + MailTo);
            sw.WriteLine("\tSMTP服务器：" + MailSmtpHost);
            sw.WriteLine("\t发 信 人：" + MailFromName + "<" + MailFrom + ">");
            sw.WriteLine("---------------------------------------------------------------------------------------------------");
            sw.Close();
            sw.Dispose();
        }
        /// <summary>
        /// 保存错误日志
        /// </summary>
        /// <param name="MailFrom"></param>
        /// <param name="MailFromName"></param>
        /// <param name="MailSmtpHost"></param>
        /// <param name="ErrMsg"></param>
        private static void SaveErrLog(string MailTo, string MailFrom, string MailFromName, string MailSmtpHost, string ErrMsg)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(HttpContext.Current.Server.MapPath("~/_data/log/mailerror_" + DateTime.Now.ToString("yyyyMMdd") + ".txt"), true, System.Text.Encoding.UTF8);
            sw.WriteLine(System.DateTime.Now.ToString());
            sw.WriteLine("\t收 信 人：" + MailTo);
            sw.WriteLine("\tSMTP服务器：" + MailSmtpHost);
            sw.WriteLine("\t发 信 人：" + MailFromName + "<" + MailFrom + ">");
            sw.WriteLine("\t错误信息：\r\n" + ErrMsg);
            sw.WriteLine("---------------------------------------------------------------------------------------------------");
            sw.Close();
            sw.Dispose();
        }
    }
}

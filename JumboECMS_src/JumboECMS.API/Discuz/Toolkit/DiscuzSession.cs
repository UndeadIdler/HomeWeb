using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Web;

namespace JumboECMS.API.Discuz.Toolkit
{
    public class DiscuzSession
    {
        Util util;
        public SessionInfo session_info;
        string auth_token;
        string forum_url;

        internal Util Util
        {
            get { return util; }
        }

        internal string SessionKey
        {
            get { return session_info.SessionKey; }
        }

        // use this for plain sessions
        public DiscuzSession(string api_key, string shared_secret, string forum_url)
        {
            util = new Util(api_key, shared_secret, forum_url + "services/restserver.aspx?");
            this.forum_url = forum_url;
        }

        // use this if you want to re-start an infinite session
        public DiscuzSession(string api_key, SessionInfo session_info, string forum_url)
            : this(api_key, session_info.Secret, forum_url)
        {
            this.session_info = session_info;
            this.forum_url = forum_url;
        }

        /// <summary>
        /// 获得令牌的地址
        /// </summary>
        /// <returns></returns>
        public Uri CreateToken()
        {
            return new Uri(string.Format("{0}login.aspx?api_key={1}", forum_url, util.ApiKey));
        }



        /// <summary>
        /// 获取当前登录用户ID
        /// </summary>
        /// <returns></returns>
        public Me GetLoggedInUser()
        {
            return new Me(session_info.UId, this);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="password"></param>
        /// <param name="isMD5Passwd"></param>
        /// <param name="expires"></param>
        /// <param name="cookieDomain"></param>
        public bool Login(int uid, string password, bool isMD5Passwd, int expires, string cookieDomain)
        {
            if (uid == 0) return false;
            User user = GetUserInfo(uid);
            //判断密码是否正确
            if (user.Password.ToLower() == Util.GetMD5(password, isMD5Passwd).ToLower())
            {
                HttpCookie cookie = new HttpCookie("dnt");
                cookie.Values["userid"] = user.UId.ToString();
                cookie.Values["password"] = EncodePassword(password, isMD5Passwd);
                cookie.Values["avatar"] = HttpUtility.UrlEncode(user.Avatar.ToString());
                cookie.Values["tpp"] = user.Tpp.ToString();
                cookie.Values["ppp"] = user.Ppp.ToString();
                cookie.Values["invisible"] = user.Invisible.ToString();
                cookie.Values["referer"] = "default.aspx";
                cookie.Values["expires"] = expires.ToString();
                if (expires > 0)
                {
                    cookie.Expires = DateTime.Now.AddMinutes(expires);
                }
                cookie.Domain = cookieDomain;

                HttpContext.Current.Response.AppendCookie(cookie);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <param name="cookieDomain"></param>
        public void Logout(string cookieDomain)
        {
            HttpCookie cookie = new HttpCookie("dnt");
            cookie.Values.Clear();
            cookie.Expires = DateTime.Now.AddYears(-1);
            cookie.Domain = cookieDomain;
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        #region auth

        /// <summary>
        /// 获得令牌(客户端使用)
        /// </summary>
        /// <returns></returns>
        public string CreateTokenForClient()
        {
            return util.GetResponse<TokenInfo>("auth.createToken").Token;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="isMD5Passwd"></param>
        /// <returns></returns>
        public int Register(string username, string password, string email, bool isMD5Passwd)
        {
            List<DiscuzParam> param_list = new List<DiscuzParam>();
            if (session_info != null && !string.IsNullOrEmpty(session_info.SessionKey))
            {
                param_list.Add(DiscuzParam.Create("session_key", session_info.SessionKey));
            }

            param_list.Add(DiscuzParam.Create("user_name", username));
            param_list.Add(DiscuzParam.Create("password", password));
            param_list.Add(DiscuzParam.Create("email", email));

            if (isMD5Passwd)
                param_list.Add(DiscuzParam.Create("password_format", "md5"));

            RegisterResponse rsp = util.GetResponse<RegisterResponse>("auth.register", param_list.ToArray());
            return rsp.Uid;
        }

        /// <summary>
        /// 加密密码
        /// </summary>
        /// <param name="password"></param>
        /// <param name="isMD5Passwd"></param>
        /// <returns></returns>
        public string EncodePassword(string password, bool isMD5Passwd)
        {
            List<DiscuzParam> param_list = new List<DiscuzParam>();
            if (session_info != null && !string.IsNullOrEmpty(session_info.SessionKey))
            {
                param_list.Add(DiscuzParam.Create("session_key", session_info.SessionKey));
            }
            param_list.Add(DiscuzParam.Create("password", password));

            if (isMD5Passwd)
                param_list.Add(DiscuzParam.Create("password_format", "md5"));

            EncodePasswordResponse epr = util.GetResponse<EncodePasswordResponse>("auth.encodePassword", param_list.ToArray());
            return epr.Password;
        }

        /// <summary>
        /// 从令牌中获得会话
        /// </summary>
        /// <param name="auth_token"></param>
        /// <returns></returns>
        public SessionInfo GetSessionFromToken(string auth_token)
        {
            this.session_info = util.GetResponse<SessionInfo>("auth.getSession",
                    DiscuzParam.Create("auth_token", auth_token));
            //this.util.SharedSecret = session_info.Secret;

            this.auth_token = string.Empty;
            this.session_info.Secret = util.SharedSecret;
            return session_info;
        }

        #endregion

        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <param name="uids"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public User[] GetUserInfo(long[] uids, string[] fields)
        {
            List<DiscuzParam> param_list = new List<DiscuzParam>();
            if (session_info != null && !string.IsNullOrEmpty(session_info.SessionKey))
            {
                param_list.Add(DiscuzParam.Create("session_key", session_info.SessionKey));
            }


            if (uids == null || uids.Length == 0)
                throw new Exception("uid not provided");

            param_list.Add(DiscuzParam.Create("uids", uids));
            param_list.Add(DiscuzParam.Create("fields", fields));

            UserInfoResponse rsp = util.GetResponse<UserInfoResponse>("users.getInfo", param_list.ToArray());
            return rsp.Users;
        }

        /// <summary>
        /// 根据用户名得到用户ID
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int GetUserID(string username)
        {
            try
            {
                List<DiscuzParam> param_list = new List<DiscuzParam>();
                if (session_info != null && !string.IsNullOrEmpty(session_info.SessionKey))
                {
                    param_list.Add(DiscuzParam.Create("session_key", session_info.SessionKey));
                }

                param_list.Add(DiscuzParam.Create("user_name", username));

                GetIDResponse gir = util.GetResponse<GetIDResponse>("users.getID", param_list.ToArray());

                return gir.UId;
            }
            catch (DiscuzException)
            //catch (DiscuzException de)
            {
                return 0;
            }

        }

        /// <summary>
        /// 根据uid获取用户信息
        /// </summary>
        /// <param name="uid">要获取用户的uid</param>
        /// <returns>用户信息</returns>
        public User GetUserInfo(long uid)
        {
            User[] users = this.GetUserInfo(new long[1] { uid }, User.FIELDS);

            if (users.Length < 1)
                return null;

            return users[0];
        }

        /// <summary>
        /// 设置用户信息
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="user_for_editing"></param>
        /// <returns></returns>
        public bool SetUserInfo(int uid, UserForEditing user_for_editing)
        {
            List<DiscuzParam> param_list = new List<DiscuzParam>();
            if (session_info != null && !string.IsNullOrEmpty(session_info.SessionKey))
            {
                param_list.Add(DiscuzParam.Create("session_key", session_info.SessionKey));
            }

            param_list.Add(DiscuzParam.Create("uid", uid));
            param_list.Add(DiscuzParam.Create("user_info", Newtonsoft.Json.JavaScriptConvert.SerializeObject(user_for_editing)));

            SetInfoResponse sir = util.GetResponse<SetInfoResponse>("users.setInfo", param_list.ToArray());

            return sir.Successfull == 1;
        }

        /// <summary>
        /// 设置扩展积分
        /// </summary>
        /// <param name="uids"></param>
        /// <param name="additional_values"></param>
        /// <returns></returns>
        public bool SetExtCredits(string uids, string additional_values)
        {
            List<DiscuzParam> param_list = new List<DiscuzParam>();

            if (session_info != null && !string.IsNullOrEmpty(session_info.SessionKey))
            {
                param_list.Add(DiscuzParam.Create("session_key", session_info.SessionKey));
            }
            param_list.Add(DiscuzParam.Create("uids", uids));

            param_list.Add(DiscuzParam.Create("additional_values", additional_values));

            SetExtCreditsResponse secr = util.GetResponse<SetExtCreditsResponse>("users.setExtCredits", param_list.ToArray());

            return secr.Successfull == 1;
        }

        /// <summary>
        /// 修改指定用户的密码
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="originalPassword"></param>
        /// <param name="newPassword"></param>
        /// <param name="confirmNewPassword"></param>
        /// <param name="passwordFormat"></param>
        /// <returns></returns>
        public bool ChangeUserPassword(long uid, string originalPassword, string newPassword, string confirmNewPassword, string passwordFormat)
        {
            List<DiscuzParam> param_list = new List<DiscuzParam>();
            param_list.Add(DiscuzParam.Create("uid", uid));
            param_list.Add(DiscuzParam.Create("original_password", originalPassword));
            param_list.Add(DiscuzParam.Create("new_password", newPassword));
            param_list.Add(DiscuzParam.Create("confirm_new_password", confirmNewPassword));
            param_list.Add(DiscuzParam.Create("password_format", passwordFormat));
            ChangePasswordResponse cpp = util.GetResponse<ChangePasswordResponse>("users.changePassword", param_list.ToArray());
            return cpp.Result == 1;
        }

        #region topics

        /// <summary>
        /// 创建主题
        /// </summary>
        /// <param name="uid">指定用户ID,0为当前登录用户ID</param>
        /// <param name="title">标题</param>
        /// <param name="fid">版块ID</param>
        /// <param name="message">主题内容</param>
        /// <param name="icon_id">图标编号</param>
        /// <param name="tags">标签，半角逗号分隔</param>
        /// <returns></returns>
        public TopicCreateResponse CreateTopic(int uid, string title, int fid, string message, int icon_id, string tags, int typeid)
        {

            Topic topic = new Topic();

            topic.UId = uid == 0 ? (int)session_info.UId : uid;
            topic.Title = title;
            topic.Fid = fid;
            topic.Message = message;
            topic.Iconid = icon_id;
            topic.Tags = tags;
            topic.Typeid = typeid;

            List<DiscuzParam> param_list = new List<DiscuzParam>();

            if (uid == 0)
            {
                param_list.Add(DiscuzParam.Create("session_key", session_info.SessionKey));
            }

            param_list.Add(DiscuzParam.Create("topic_info", Newtonsoft.Json.JavaScriptConvert.SerializeObject(topic)));
            TopicCreateResponse tcr = util.GetResponse<TopicCreateResponse>("topics.create", param_list.ToArray());
            return tcr;
        }

        /// <summary>
        /// 创建主题
        /// </summary>
        /// <param name="title"></param>
        /// <param name="fid"></param>
        /// <param name="message"></param>
        /// <param name="icon_id"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public TopicCreateResponse CreateTopic(string title, int fid, string message, int icon_id, string tags, int typeid)
        {
            return CreateTopic(0, title, fid, message, icon_id, tags, typeid);
        }

        /// <summary>
        /// 编辑主题(高级使用方法)
        /// </summary>
        /// <param name="tid">帖子ID</param>
        /// <param name="jsonTopicInfo">topicInfo(Json数组,格式参照文档说明)</param>
        /// <returns></returns>
        public TopicEditResponse EditTopic(int tid, string jsonTopicInfo)
        {
            List<DiscuzParam> param_list = new List<DiscuzParam>();
            if (session_info != null && !string.IsNullOrEmpty(session_info.SessionKey))
            {
                param_list.Add(DiscuzParam.Create("session_key", session_info.SessionKey));
            }
            param_list.Add(DiscuzParam.Create("tid", tid));

            param_list.Add(DiscuzParam.Create("topic_info", jsonTopicInfo));
            TopicEditResponse ter = util.GetResponse<TopicEditResponse>("topics.edit", param_list.ToArray());
            return ter;
        }

        /// <summary>
        /// 编辑主题(简易方法,客户端不适用)
        /// </summary>
        /// <param name="tid">帖子ID</param>
        /// <param name="topic">Topic类型的对象</param>
        /// <returns></returns>
        public TopicEditResponse EditTopic(int tid, Topic topic)
        {
            return EditTopic(tid, Util.RemoveJsonNull(Newtonsoft.Json.JavaScriptConvert.SerializeObject(topic)));
        }


        /// <summary>
        /// 删除主题(客户端必须使用此填入forumid的方法)
        /// </summary>
        /// <param name="topicids">要删除的主题ID的序列,以(逗号),分隔</param>
        /// <param name="forumid">板块ID用来验证版主身份</param>
        /// <returns></returns>
        public TopicDeleteResponse DeleteTopic(string topicids, int forumid)
        {
            List<DiscuzParam> param_list = new List<DiscuzParam>();
            if (session_info != null && !string.IsNullOrEmpty(session_info.SessionKey))
            {
                param_list.Add(DiscuzParam.Create("session_key", session_info.SessionKey));
            }

            param_list.Add(DiscuzParam.Create("topic_ids", topicids));
            if (forumid > 0)
                param_list.Add(DiscuzParam.Create("fid", forumid));
            return util.GetResponse<TopicDeleteResponse>("topics.delete", param_list.ToArray());
        }

        /// <summary>
        /// 删除主题
        /// </summary>
        /// <param name="topicids">要删除的主题ID的序列,以(逗号),分隔</param>
        /// <returns></returns>
        public TopicDeleteResponse DeleteTopic(string topicids)
        {
            return DeleteTopic(topicids, 0);
        }

        /// <summary>
        /// 获取主题
        /// </summary>
        /// <param name="tid">主题ID</param>
        /// <param name="pageindex">主题当前分页</param>
        /// <param name="pagesize">主题每页帖子数</param>
        /// <returns></returns>
        public TopicGetResponse GetTopic(int tid, int pageindex, int pagesize)
        {
            List<DiscuzParam> param_list = new List<DiscuzParam>();
            if (session_info != null && !string.IsNullOrEmpty(session_info.SessionKey))
            {
                param_list.Add(DiscuzParam.Create("session_key", session_info.SessionKey));
            }

            param_list.Add(DiscuzParam.Create("tid", tid));
            param_list.Add(DiscuzParam.Create("page_index", pageindex));
            param_list.Add(DiscuzParam.Create("page_size", pagesize));
            return util.GetResponse<TopicGetResponse>("topics.get", param_list.ToArray());
        }

        /// <summary>
        /// 回复帖子
        /// </summary>
        /// <param name="reply"></param>
        /// <returns></returns>
        public TopicReplyResponse TopicReply(Reply reply)
        {

            List<DiscuzParam> param_list = new List<DiscuzParam>();
            if (session_info != null && !string.IsNullOrEmpty(session_info.SessionKey))
            {
                param_list.Add(DiscuzParam.Create("session_key", session_info.SessionKey));
            }

            param_list.Add(DiscuzParam.Create("reply_info", JavaScriptConvert.SerializeObject(reply)));
            TopicReplyResponse trr = util.GetResponse<TopicReplyResponse>("topics.reply", param_list.ToArray());
            return trr;
        }

        /// <summary>
        /// 最近回复的帖子
        /// </summary>
        /// <param name="fid">论坛id</param>
        /// <param name="tid">帖子id</param>
        /// <param name="page_size"></param>
        /// <param name="page_index"></param>
        /// <returns></returns>
        public TopicGetRencentRepliesResponse GetRecentReplies(int fid, int tid, int page_size, int page_index)
        {
            List<DiscuzParam> param_list = new List<DiscuzParam>();
            if (session_info != null && !string.IsNullOrEmpty(session_info.SessionKey))
            {
                param_list.Add(DiscuzParam.Create("session_key", session_info.SessionKey));
            }

            param_list.Add(DiscuzParam.Create("fid", fid));
            param_list.Add(DiscuzParam.Create("tid", tid));
            param_list.Add(DiscuzParam.Create("page_size", page_size));
            param_list.Add(DiscuzParam.Create("page_index", page_index));

            TopicGetRencentRepliesResponse tgrr = util.GetResponse<TopicGetRencentRepliesResponse>("topics.getRecentReplies", param_list.ToArray());
            return tgrr;
        }

        /// <summary>
        /// 获取主题列表
        /// </summary>
        /// <param name="fid"></param>
        /// <param name="page_size"></param>
        /// <param name="page_index"></param>
        /// <returns></returns>
        public TopicGetListResponse GetTopicList(int fid, int page_size, int page_index, string typeIdList)
        {
            List<DiscuzParam> param_list = new List<DiscuzParam>();
            if (session_info != null && !string.IsNullOrEmpty(session_info.SessionKey))
            {
                param_list.Add(DiscuzParam.Create("session_key", session_info.SessionKey));
            }

            param_list.Add(DiscuzParam.Create("type_id_list", typeIdList));
            param_list.Add(DiscuzParam.Create("fid", fid));
            param_list.Add(DiscuzParam.Create("page_size", page_size));
            param_list.Add(DiscuzParam.Create("page_index", page_index));

            TopicGetListResponse tglr = util.GetResponse<TopicGetListResponse>("topics.getList", param_list.ToArray());
            return tglr;
        }

        /// <summary>
        /// 获得需要管理人员关注的主题列表
        /// </summary>
        /// <param name="fid">版块id</param>
        /// <param name="page_size">页面大小</param>
        /// <param name="page_index">页码</param>
        /// <returns></returns>
        public TopicGetListResponse GetAttentionTopicList(int fid, int page_size, int page_index)
        {
            List<DiscuzParam> param_list = new List<DiscuzParam>();
            if (session_info != null && !string.IsNullOrEmpty(session_info.SessionKey))
            {
                param_list.Add(DiscuzParam.Create("session_key", session_info.SessionKey));
            }

            param_list.Add(DiscuzParam.Create("fid", fid));
            param_list.Add(DiscuzParam.Create("page_size", page_size));
            param_list.Add(DiscuzParam.Create("page_index", page_index));

            TopicGetListResponse tglr = util.GetResponse<TopicGetListResponse>("topics.getAttentionList", param_list.ToArray());
            return tglr;
        }

        /// <summary>
        /// 批量删除指定主题中的帖子
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="postids"></param>
        /// <returns></returns>
        public TopicDeleteRepliesResponse DeleteTopicReplies(int tid, string postids)
        {
            List<DiscuzParam> param_list = new List<DiscuzParam>();
            if (session_info != null && !string.IsNullOrEmpty(session_info.SessionKey))
            {
                param_list.Add(DiscuzParam.Create("session_key", session_info.SessionKey));
            }

            param_list.Add(DiscuzParam.Create("tid", tid));
            param_list.Add(DiscuzParam.Create("post_ids", postids));
            return util.GetResponse<TopicDeleteRepliesResponse>("topics.deletereplies", param_list.ToArray());
        }

        #endregion

        #region notification

        /// <summary>
        /// 发送通知
        /// </summary>
        /// <param name="note"></param>
        /// <param name="to_ids"></param>
        /// <param name="uid">如果为0，就用当前用户会话id</param>
        /// <returns>发送成功的用户id列表字符串</returns>
        public string NotificationsSend(string notification, string to_ids, int uid)
        {
            List<DiscuzParam> param_list = new List<DiscuzParam>();

            if (uid < 1 && session_info != null && !string.IsNullOrEmpty(session_info.SessionKey))
            {
                param_list.Add(DiscuzParam.Create("session_key", session_info.SessionKey));
            }


            param_list.Add(DiscuzParam.Create("to_ids", to_ids));
            param_list.Add(DiscuzParam.Create("notification", notification));

            SendNotificationResponse nsr = util.GetResponse<SendNotificationResponse>("notifications.send", param_list.ToArray());
            return nsr.Result;
        }

        /// <summary>
        /// 发送email通知
        /// </summary>
        /// <param name="recipients">uids</param>
        /// <param name="subject">主题</param>
        /// <param name="text">内容</param>
        /// <returns></returns>
        public string NotificationSendEmail(string recipients, string subject, string text)
        {
            List<DiscuzParam> param_list = new List<DiscuzParam>();
            if (session_info != null && !string.IsNullOrEmpty(session_info.SessionKey))
            {
                param_list.Add(DiscuzParam.Create("session_key", session_info.SessionKey));
            }

            param_list.Add(DiscuzParam.Create("recipients", recipients));
            param_list.Add(DiscuzParam.Create("subject", subject));
            param_list.Add(DiscuzParam.Create("text", text));


            SendNotificationEmailResponse sner = util.GetResponse<SendNotificationEmailResponse>("notifications.sendEmail", param_list.ToArray());
            return sner.Recipients = recipients;
        }

        /// <summary>
        /// 获取通知
        /// </summary>
        /// <returns></returns>
        public GetNotiificationResponse NotificationGet()
        {
            List<DiscuzParam> param_list = new List<DiscuzParam>();
            if (session_info != null && !string.IsNullOrEmpty(session_info.SessionKey))
            {
                param_list.Add(DiscuzParam.Create("session_key", session_info.SessionKey));
            }
            else
            {
                return null;
            }
            GetNotiificationResponse gnr = util.GetResponse<GetNotiificationResponse>("notifications.get", param_list.ToArray());
            return gnr;
        }

        #endregion

        #region forums

        /// <summary>
        /// 获取论坛信息
        /// </summary>
        /// <param name="fid">论坛id</param>
        /// <returns></returns>
        public GetForumResponse ForumGet(int fid)
        {
            List<DiscuzParam> param_list = new List<DiscuzParam>();
            if (session_info != null && !string.IsNullOrEmpty(session_info.SessionKey))
            {
                param_list.Add(DiscuzParam.Create("session_key", session_info.SessionKey));
            }

            param_list.Add(DiscuzParam.Create("fid", fid));
            GetForumResponse gfr = util.GetResponse<GetForumResponse>("forums.get", param_list.ToArray());

            return gfr;
        }

        /// <summary>
        /// 创建论坛
        /// </summary>
        /// <param name="forum">要创建的论坛</param>
        /// <returns></returns>
        public CreateForumResponse ForumCreate(Forum forum)
        {
            List<DiscuzParam> param_list = new List<DiscuzParam>();
            if (session_info != null && !string.IsNullOrEmpty(session_info.SessionKey))
            {
                param_list.Add(DiscuzParam.Create("session_key", session_info.SessionKey));
            }

            param_list.Add(DiscuzParam.Create("forum_info", JavaScriptConvert.SerializeObject(forum)));
            CreateForumResponse fcr = util.GetResponse<CreateForumResponse>("forums.create", param_list.ToArray());
            return fcr;
        }

        public GetIndexListResponse ForumGetIndexList()
        {
            List<DiscuzParam> param_list = new List<DiscuzParam>();
            if (session_info != null && !string.IsNullOrEmpty(session_info.SessionKey))
            {
                param_list.Add(DiscuzParam.Create("session_key", session_info.SessionKey));
            }
            return util.GetResponse<GetIndexListResponse>("forums.getindexlist", param_list.ToArray());
        }

        #endregion

        #region messages

        /// <summary>
        /// 批量发送短信息给指定用户
        /// </summary>
        /// <param name="to_uids"></param>
        /// <param name="fromid"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public string SendMessages(string to_uids, string fromid, string subject, string message)
        {
            List<DiscuzParam> param_list = new List<DiscuzParam>();
            param_list.Add(DiscuzParam.Create("to_ids", to_uids));
            param_list.Add(DiscuzParam.Create("from_id", fromid));
            param_list.Add(DiscuzParam.Create("subject", subject));
            param_list.Add(DiscuzParam.Create("message", message));
            MessagesSendResponse msr = util.GetResponse<MessagesSendResponse>("messages.send", param_list.ToArray());
            return msr.Result;
        }

        /// <summary>
        /// 获取某用户的短信息收件箱
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pagesize"></param>
        /// <param name="pageindex"></param>
        /// <returns></returns>
        public MessagesGetResponse GetUserMessages(int uid, int pagesize, int pageindex)
        {
            List<DiscuzParam> param_list = new List<DiscuzParam>();
            param_list.Add(DiscuzParam.Create("uid", uid));
            param_list.Add(DiscuzParam.Create("page_size", pagesize));
            param_list.Add(DiscuzParam.Create("page_index", pageindex));
            MessagesGetResponse mgr = util.GetResponse<MessagesGetResponse>("messages.get", param_list.ToArray());
            return mgr;
        }

        #endregion

        #region async

        public Dictionary<string, string> GetQueryString()
        {
            return util.GetQueryString();
        }

        #endregion
    }
}


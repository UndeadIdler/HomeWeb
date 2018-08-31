using System;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace JumboECMS.API.Discuz.Toolkit
{
    [XmlRoot("auth_createToken_response", Namespace = "http://nt.discuz.net/api/", IsNullable = false)]
    public class TokenInfo
    {
        [XmlElement("session_key")]
        public string Token;
    }

    /// <summary>
    /// Session��Ϣ��
    /// </summary>
    [XmlRoot("auth_getSession_response", Namespace = "http://nt.discuz.net/api/", IsNullable = false)]
    public class SessionInfo
    {
        [XmlElement("session_key")]
        public string SessionKey;

        [XmlElement("uid")]
        public long UId;

        [XmlIgnore]
        public string Secret;

        [XmlElement("user_name")]
        public string UserName;

        [XmlElement("expires")]
        public long Expires;

        public SessionInfo()
        { }

        // use this if you want to create a session based on infinite session
        // credentials
        public SessionInfo(string session_key, long uid, string secret, string rest_url)
        {
            this.SessionKey = session_key;
            this.UId = uid;
            this.Secret = secret;
            this.Expires = 0;
        }
    }

    [XmlRoot("auth_register_response", Namespace = "http://nt.discuz.net/api/", IsNullable = false)]
    public class RegisterResponse
    {
        [XmlText]
        public int Uid;

    }

    [XmlRoot("auth_encodePassword_response", Namespace = "http://nt.discuz.net/api/", IsNullable = false)]
    public class EncodePasswordResponse
    {
        [XmlText]
        public string Password;
    }

    [XmlRoot("users_changePassword_response", Namespace = "http://nt.discuz.net/api/", IsNullable = false)]
    public class ChangePasswordResponse
    {
        [XmlText]
        public int Result;
    }


    [XmlRoot("users_getInfo_response", Namespace = "http://nt.discuz.net/api/", IsNullable = false)]
    public class UserInfoResponse
    {
        [XmlElement("user")]
        public User[] user_array;

        [XmlIgnore]
        public User[] Users
        {
            get { return user_array ?? new User[0]; }
        }

        [XmlAttribute("list")]
        public bool List;
    }

    [XmlRoot("users_getLoggedInUser_response", Namespace = "http://nt.discuz.net/api/", IsNullable = false)]
    public class LoggedInUserResponse
    {
        [XmlText]
        public int Uid;

        [XmlAttribute("list")]
        public bool List;
    }

    [XmlRoot("users_getID_response", Namespace = "http://nt.discuz.net/api/", IsNullable = false)]
    public class GetIDResponse
    {
        [XmlText]
        public int UId;
    }

    public class ArgResponse
    {
        [XmlElement("arg")]
        public Arg[] Args;

        [XmlAttribute("list")]
        public bool List;
    }


    [XmlRoot("topics_create_response", Namespace = "http://nt.discuz.net/api/", IsNullable = false)]
    public class TopicCreateResponse
    {
        [JsonPropertyAttribute("topic_id")]
        [XmlElement("topic_id")]
        public int Topicid;

        [JsonPropertyAttribute("url")]
        [XmlElement("url")]
        public string Url;

        [JsonPropertyAttribute("need_audit")]
        [XmlElement("need_audit")]
        public bool NeedAudit;
    }

    [XmlRoot("users_setInfo_response", Namespace = "http://nt.discuz.net/api/", IsNullable = false)]
    public class SetInfoResponse
    {
        [XmlText]
        public int Successfull;
    }

    [XmlRoot("users_setExtCredits_response", Namespace = "http://nt.discuz.net/api/", IsNullable = false)]
    public class SetExtCreditsResponse
    {
        [XmlText]
        public int Successfull;
    }

    [XmlRoot("notification_send_response", Namespace = "http://nt.discuz.net/api/", IsNullable = false)]
    public class SendNotificationResponse
    {
        [XmlText]
        public string Result;
    }

    [XmlRoot("notification_sendemail_response", Namespace = "http://nt.discuz.net/api/", IsNullable = false)]
    public class SendNotificationEmailResponse
    {
        [XmlText]
        public string Recipients;
    }

    [XmlRoot("notification_get_response", Namespace = "http://nt.discuz.net/api/", IsNullable = false)]
    public class GetNotiificationResponse
    {
        [JsonPropertyAttribute("message")]
        [XmlElement("message", IsNullable = true)]
        public Notification Message;

        [JsonPropertyAttribute("notification")]
        [XmlElement("notification", IsNullable = true)]
        public Notification Notification;
    }

    [XmlRoot("forums_get_response", Namespace = "http://nt.discuz.net/api/", IsNullable = false)]
    public class GetForumResponse
    {
        [JsonPropertyAttribute("fid")]
        [XmlElement("fid")]
        public int Fid;

        [JsonPropertyAttribute("url")]
        [XmlElement("url")]
        public string Url;

        [JsonPropertyAttribute("topics")]
        [XmlElement("topics")]
        public int Topics;	//������

        [JsonPropertyAttribute("current_topics")]
        [XmlElement("current_topics")]
        public int CurTopics;	//������

        [JsonPropertyAttribute("posts")]
        [XmlElement("posts")]
        public int Posts;	//������

        [JsonPropertyAttribute("today_posts")]
        [XmlElement("today_posts")]
        public int TodayPosts;	//���շ���

        [JsonPropertyAttribute("last_post")]
        [XmlElement("last_post")]
        public string LastPost;	//��󷢱�����

        [JsonPropertyAttribute("last_poster")]
        [XmlElement("last_poster")]
        public string LastPoster; //��󷢱���û���

        [JsonPropertyAttribute("last_poster_id")]
        [XmlElement("last_poster_id")]
        public int LastPosterId; //��󷢱���û�id

        [JsonPropertyAttribute("last_tid")]
        [XmlElement("last_tid")]
        public int LastTid; //��󷢱����ӵ�����id

        [JsonPropertyAttribute("last_title")]
        [XmlElement("last_title")]
        public string LastTitle; //��󷢱�����ӱ���

        [JsonPropertyAttribute("description")]
        [XmlElement("description")]
        public string Description;	//��̳����

        [JsonPropertyAttribute("icon")]
        [XmlElement("icon")]
        public string Icon;	//��̳ͼ��,��ʾ����ҳ��̳�б��

        [JsonPropertyAttribute("moderators")]
        [XmlElement("moderators")]
        public string Moderators;	//�����б�(������ʾʹ��,����¼ʵ��Ȩ��)

        [JsonPropertyAttribute("rules")]
        [XmlElement("rules")]
        public string Rules;	//�������

        [JsonPropertyAttribute("parent_id")]
        [XmlElement("parent_id")]
        public int ParentId;	//����̳���ϼ���̳��ֱ���̳���ϼ���̳������fid

        [JsonPropertyAttribute("path_list")]
        [XmlElement("path_list")]
        public string PathList; //��̳��������·����html���Ӵ���

        [JsonPropertyAttribute("parent_id_list")]
        [XmlElement("parent_id_list")]
        public string ParentIdList; //��̳��������·��id�б�

        [JsonPropertyAttribute("sub_forum_count")]
        [XmlElement("sub_forum_count")]
        public int SubForumCount; //��̳����������̳����

        [JsonPropertyAttribute("name")]
        [XmlElement("name")]
        public string Name;	//��̳����

        [JsonPropertyAttribute("status")]
        [XmlElement("status")]
        public int Status;	//�Ƿ���ʾ
    }


    [XmlRoot("forums_create_response", Namespace = "http://nt.discuz.net/api/", IsNullable = false)]
    public class CreateForumResponse
    {
        [JsonPropertyAttribute("fid")]
        [XmlElement("fid")]
        public int Fid;

        [JsonPropertyAttribute("url")]
        [XmlElement("url")]
        public string Url;
    }

    [XmlRoot("topics_reply_response", Namespace = "http://nt.discuz.net/api/", IsNullable = false)]
    public class TopicReplyResponse
    {
        [JsonPropertyAttribute("post_id")]
        [XmlElement("post_id")]
        public int PostId;

        [JsonPropertyAttribute("url")]
        [XmlElement("url")]
        public string Url;

        [JsonPropertyAttribute("need_audit")]
        [XmlElement("need_audit")]
        public bool NeedAudit;

    }

    [XmlRoot("topics_getRecentReplies_response", Namespace = "http://nt.discuz.net/api/", IsNullable = false)]
    public class TopicGetRencentRepliesResponse
    {
        [XmlElement("count")]
        public int Count;

        [XmlElement("post")]
        public Post[] post_array;

        [JsonIgnore]
        public Post[] Posts
        {
            get { return post_array ?? new Post[0]; }
        }
        [JsonIgnore]
        [XmlAttribute("list")]
        public bool List;
    }

    [XmlRoot("topics_getList_response", Namespace = "http://nt.discuz.net/api/", IsNullable = false)]
    public class TopicGetListResponse
    {
        [XmlElement("count")]
        public int Count;

        [XmlElement("topic")]
        public ForumTopic[] Topics;

        [JsonIgnore]
        [XmlAttribute("list")]
        public bool List;
    }

    [XmlRoot("topics_edit_response", Namespace = "http://nt.discuz.net/api/", IsNullable = false)]
    public class TopicEditResponse
    {
        [XmlText]
        public int Result;
    }

    [XmlRoot("topics_delete_response", Namespace = "http://nt.discuz.net/api/", IsNullable = false)]
    public class TopicDeleteResponse
    {
        [XmlText]
        public int Result;
    }

    [XmlRoot("topics_get_response", Namespace = "http://nt.discuz.net/api/", IsNullable = false)]
    public class TopicGetResponse
    {
        [JsonPropertyAttribute("topic_id")]
        [XmlElement("topic_id")]
        public int TopicId;

        [JsonPropertyAttribute("url")]
        [XmlElement("url")]
        public string Url;

        [JsonPropertyAttribute("title")]
        [XmlElement("title", IsNullable = false)]
        public string Title;

        [JsonPropertyAttribute("fid")]
        [XmlElement("fid", IsNullable = false)]
        public int Fid;

        //[JsonPropertyAttribute("message")]
        //[XmlElement("message", IsNullable = false)]
        //public string Message;

        [JsonPropertyAttribute("icon_id")]
        [XmlElement("icon_id", IsNullable = false)]
        public int Iconid;

        [JsonPropertyAttribute("tags")]
        [XmlElement("tags", IsNullable = true)]
        public string Tags;

        [JsonPropertyAttribute("author")]
        [XmlElement("author")]
        public string Author = string.Empty;

        [JsonPropertyAttribute("author_id")]
        [XmlElement("author_id")]
        public int AuthorId;

        [JsonPropertyAttribute("reply_count")]
        [XmlElement("reply_count")]
        public int ReplyCount;

        [JsonPropertyAttribute("view_count")]
        [XmlElement("view_count")]
        public int ViewCount;

        [JsonPropertyAttribute("last_post_time")]
        [XmlElement("last_post_time")]
        public string LastPostTime = string.Empty;

        [JsonPropertyAttribute("last_poster_id")]
        [XmlElement("last_poster_id")]
        public int LastPosterId;

        [XmlElement("post")]
        [JsonProperty("posts")]
        public Post[] Posts;

        [XmlElement("attachment")]
        [JsonProperty("attachments")]
        public AttachmentInfo[] Attachments;

        [JsonIgnore]
        [XmlAttribute("list")]
        public bool List;

        [JsonPropertyAttribute("type_id")]
        [XmlElement("type_id")]
        public int TypeId;

        [JsonPropertyAttribute("type_name")]
        [XmlElement("type_name")]
        public string TypeName;
    }

    [XmlRoot("messages_send_response", Namespace = "http://nt.discuz.net/api/", IsNullable = false)]
    public class MessagesSendResponse
    {
        [XmlText]
        public string Result;
    }

    [XmlRoot("messages_get_response", Namespace = "http://nt.discuz.net/api/", IsNullable = false)]
    public class MessagesGetResponse
    {
        [JsonPropertyAttribute("count")]
        [XmlElement("count")]
        public int count;

        [XmlElement("pm")]
        [JsonProperty("pms")]
        public PrivateMessage[] PM;
    }

    [XmlRoot("topics_deleteReplies_response", Namespace = "http://nt.discuz.net/api/", IsNullable = false)]
    public class TopicDeleteRepliesResponse
    {
        [XmlText]
        public string Result;
    }

    [XmlRoot("forums_getIndexList_response", Namespace = "http://nt.discuz.net/api/", IsNullable = false)]
    public class GetIndexListResponse
    {
        [XmlElement("forum")]
        [JsonProperty("forums")]
        public IndexForum[] Forum;
    }
}
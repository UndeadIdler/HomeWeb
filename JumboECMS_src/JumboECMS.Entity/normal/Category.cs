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
namespace JumboECMS.Entity
{
    /// <summary>
    /// 栏目-------表映射实体
    /// </summary>

    public class Normal_Category
    {
        public Normal_Category()
        { }

        private string _id;
        private int _parentid;
        private int _topid;
        private string _title;
        private string _info;
        private string _keywords;
        private string _img;
        private string _filepath;
        private string _code;
        private bool _istop;
        private int _topicnum;
        private string _templateid;
        private string _typeid;
        private string _contenttemp;
        private string _firstpage;
        private string _aliaspage;
        private int _readgroup;
        private string _moduletype;
        private string _content = "";
        private string _languagecode;
        /// <summary>
        /// 编号
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 父级ID
        /// </summary>
        public int ParentId
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 顶级ID
        /// </summary>
        public int TopId
        {
            set { _topid = value; }
            get { return _topid; }
        }
        /// <summary>
        /// 栏目名称
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 栏目简介
        /// </summary>
        public string Info
        {
            set { _info = value; }
            get { return _info; }
        }
        /// <summary>
        /// 栏目关键词
        /// </summary>
        public string Keywords
        {
            set { _keywords = value; }
            get { return _keywords; }
        }
        /// <summary>
        /// 封面图
        /// </summary>
        public string Img
        {
            set { _img = value; }
            get { return _img; }
        }
        /// <summary>
        /// 栏目目录
        /// </summary>
        public string FilePath
        {
            set { _filepath = value; }
            get { return _filepath; }
        }
        /// <summary>
        /// 栏目代码，用其来关联父子和兄弟关系
        /// </summary>
        public string Code
        {
            set { _code = value; }
            get { return _code; }
        }
        /// <summary>
        /// 是否导航
        /// </summary>
        public bool IsTop
        {
            set { _istop = value; }
            get { return _istop; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int TopicNum
        {
            set { _topicnum = value; }
            get { return _topicnum; }
        }
        /// <summary>
        /// 模板ID
        /// </summary>
        public string TemplateId
        {
            set { _templateid = value; }
            get { return _templateid; }
        }
        /// <summary>
        /// 1=subcat,2=list,3=single,4=link
        /// </summary>
        public string TypeId
        {
            set { _typeid = value; }
            get { return _typeid; }
        }
        /// <summary>
        /// 详细页模板ID
        /// </summary>
        public string ContentTemp
        {
            set { _contenttemp = value; }
            get { return _contenttemp; }
        }
        /// <summary>
        /// 外部链接地址
        /// </summary>
        public string FirstPage
        {
            set { _firstpage = value; }
            get { return _firstpage; }
        }
        public string AliasPage
        {
            set { _aliaspage = value; }
            get { return _aliaspage; }
        }
        /// <summary>
        /// 最低阅读会员组
        /// </summary>
        public int ReadGroup
        {
            set { _readgroup = value; }
            get { return _readgroup; }
        }
        public string ModuleType
        {
            set { _moduletype = value; }
            get { return _moduletype; }
        }
        public string Content
        {
            set { _content = value; }
            get { return _content; }
        }
        public string LanguageCode
        {
            set { _languagecode = value; }
            get { return _languagecode; }
        }
    }
}


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
    /// 模型-------表映射实体
    /// </summary>

    public class Normal_Module
    {
        public Normal_Module()
        { }
        private string _name;
        private string _type;
        private string _info = string.Empty;
        private int _pid = 0;
        private string _itemname = string.Empty;
        private string _itemunit = string.Empty;
        private string _uploadpath = "<#SiteDir#>uploadfiles/";
        private string _uploadtype = "*.jpg;*.jpeg;*.gif;";
        private int _uploadsize = 1024;
        private bool _ishtml = false;
        private bool _istop = false;
        private int _classdepth;
        private int _templateid = 0;
        private int _defaultthumbs = 0;
        /// <summary>
        /// 模块中文名
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 模块英文名
        /// </summary>
        public string Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 模块简介
        /// </summary>
        public string Info
        {
            set { _info = value; }
            get { return _info; }
        }
        /// <summary>
        /// 权值
        /// </summary>
        public int pId
        {
            set { _pid = value; }
            get { return _pid; }
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ItemName
        {
            set { _itemname = value; }
            get { return _itemname; }
        }
        /// <summary>
        /// 项目单位
        /// </summary>
        public string ItemUnit
        {
            set { _itemunit = value; }
            get { return _itemunit; }
        }
        /// <summary>
        /// 上传路径
        /// </summary>
        public string UploadPath
        {
            set { _uploadpath = value; }
            get { return _uploadpath; }
        }
        /// <summary>
        /// 上传类型
        /// </summary>
        public string UploadType
        {
            set { _uploadtype = value; }
            get { return _uploadtype; }
        }
        /// <summary>
        /// 上传大小限制
        /// </summary>
        public int UploadSize
        {
            set { _uploadsize = value; }
            get { return _uploadsize; }
        }
        /// <summary>
        /// 是否静态化
        /// </summary>
        public bool IsHtml
        {
            set { _ishtml = value; }
            get { return _ishtml; }
        }
        public bool IsTop
        {
            set { _istop = value; }
            get { return _istop; }
        }
        /// <summary>
        /// 栏目深度
        /// </summary>
        public int ClassDepth
        {
            set { _classdepth = value; }
            get { return _classdepth; }
        }
        /// <summary>
        /// 模板ID
        /// </summary>
        public int TemplateId
        {
            set { _templateid = value; }
            get { return _templateid; }
        }
        /// <summary>
        /// 默认缩略图Id
        /// </summary>
        public int DefaultThumbs
        {
            set { _defaultthumbs = value; }
            get { return _defaultthumbs; }
        }
    }
}


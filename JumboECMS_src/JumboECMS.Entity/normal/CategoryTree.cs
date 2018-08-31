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
using System.Collections.Generic;
namespace JumboECMS.Entity
{
    /// <summary>
    /// 栏目树实体
    /// </summary>
    public class Normal_CategoryTree
    {
        public Normal_CategoryTree()
        { }
        private int _id;
        private string _name = string.Empty;
        private string _link = string.Empty;
        private bool _haschild = false;
        private List<Normal_CategoryTree> _subchild;
        /// <summary>
        /// 栏目编号
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 栏目名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 栏目链接
        /// </summary>
        public string Link
        {
            set { _link = value; }
            get { return _link; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool HasChild
        {
            set { _haschild = value; }
            get { return _haschild; }
        }
        public List<Normal_CategoryTree> SubChild
        {
            set { _subchild = value; }
            get { return _subchild; }
        }
    }
}


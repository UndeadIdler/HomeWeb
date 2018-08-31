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
namespace JumboECMS.DBUtility
{
    public class DbOperEventArgs : System.EventArgs
    {
        public int id;
        public DbOperEventArgs(int _id)
        {
            id = _id;
        }
    }
}

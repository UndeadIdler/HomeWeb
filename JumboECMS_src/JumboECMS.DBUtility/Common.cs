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
using System.Text.RegularExpressions;
namespace JumboECMS.DBUtility
{
    /// <summary>
    /// 枚举，作为Web中常用的用户操作类型。常用于权限相关的判断。
    /// </summary>
    public enum OperationType : byte { Add, Modify, Delete, Audit, Enable };
}
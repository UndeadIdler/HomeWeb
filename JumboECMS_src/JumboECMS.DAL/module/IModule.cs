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
using System.Data;
using System.Web;
using System.Web.UI;
using JumboECMS.Utils;
using JumboECMS.DBUtility;

namespace JumboECMS.DAL
{
    public interface IModule
    {
        /// <summary>
        /// 得到内容页地址
        /// </summary>
        /// <param name="_moduletype"></param>
        /// <param name="_contentid"></param>
        /// <param name="_page"></param>
        /// <returns></returns>
        string GetContentLink(string _moduletype, string _contentid, int _page);
        /// <summary>
        ///  生成内容页
        /// </summary>
        /// <param name="_moduletype"></param>
        /// <param name="_contentid"></param>
        /// <param name="_page"></param>
        void CreateContent(string _moduletype, string _contentid, int _page);
        /// <summary>
        /// 得到内容页
        /// </summary>
        /// <param name="_moduletype"></param>
        /// <param name="_contentid"></param>
        /// <param name="_page"></param>
        /// <returns></returns>
        string GetContent(string _moduletype, string _contentid, int _page);
        /// <summary>
        /// 删除内容页
        /// </summary>
        /// <param name="_moduletype"></param>
        /// <param name="_contentid"></param>
        void DeleteContent(string _moduletype, string _contentid);
    }
}

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
namespace JumboECMS.DAL
{
    public class ModuleCommand
    {
        public static IModule IMD;
        static ModuleCommand()
        {

        }
        /// <summary>
        /// 得到内容页地址
        /// </summary>
        /// <param name="_moduletype"></param>
        /// <param name="_contentid"></param>
        /// <param name="_page"></param>
        /// <returns></returns>
        public static string GetContentLink(string _moduletype, string _contentid, int _page)
        {
            IMD = (IModule)Activator.CreateInstance(Type.GetType(String.Format("JumboECMS.DAL.Module_{0}DAL", _moduletype), true, true));
            return IMD.GetContentLink(_moduletype, _contentid, _page);
        }
        /// <summary>
        /// 生成内容页
        /// </summary>
        /// <param name="_moduletype"></param>
        /// <param name="_contentid"></param>
        /// <param name="_page"></param>
        public static void CreateContent(string _moduletype, string _contentid, int _page)
        {
            IMD = (IModule)Activator.CreateInstance(Type.GetType(String.Format("JumboECMS.DAL.Module_{0}DAL", _moduletype), true, true));
            IMD.CreateContent(_moduletype, _contentid, _page);
        }
        /// <summary>
        /// 得到内容页
        /// </summary>
        /// <param name="_moduletype"></param>
        /// <param name="_contentid"></param>
        /// <param name="_page"></param>
        /// <returns></returns>
        public static string GetContent(string _moduletype, string _contentid, int _page)
        {
            IMD = (IModule)Activator.CreateInstance(Type.GetType(String.Format("JumboECMS.DAL.Module_{0}DAL", _moduletype), true, true));
            return IMD.GetContent(_moduletype, _contentid, _page);
        }
        /// <summary>
        ///  删除内容页
        /// </summary>
        /// <param name="_moduletype"></param>
        /// <param name="_contentid"></param>
        public static void DeleteContent(string _moduletype, string _contentid)
        {
            IMD = (IModule)Activator.CreateInstance(Type.GetType(String.Format("JumboECMS.DAL.Module_{0}DAL", _moduletype), true, true));
            IMD.DeleteContent(_moduletype, _contentid);
        }
    }
}

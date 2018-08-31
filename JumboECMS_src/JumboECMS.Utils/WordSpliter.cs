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
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace JumboECMS.Utils
{
    /// <summary>
    /// 分词类
    /// </summary>
    public static class WordSpliter
    {
        /// <summary>
        /// 得到分词关键字
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetKeyword(string key, string splitchar)
        {
            JumboECMS.Utils.ShootSeg.Segment seg = new JumboECMS.Utils.ShootSeg.Segment();
            seg.InitWordDics();
            seg.EnablePrefix = true;
            seg.Separator = splitchar;
            return seg.SegmentText(key, false).Trim();
        }
        public static string GetKeyword(string key)
        {
            return GetKeyword(key," ");
        }
    }
}
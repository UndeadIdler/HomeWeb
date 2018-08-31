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
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
namespace JumboECMS.Common
{
    /// <summary>
    /// 内容采集类
    /// </summary>
    public class NewsCollection
    {
        public NewsCollection()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        public string GetHttpPage(string url, int timeout, Encoding EnCodeType)
        {
            string strResult = string.Empty;
            if (url.Length < 10)
                return "$UrlIsFalse$";
            try
            {
                WebClient MyWebClient = new WebClient();
                MyWebClient.Credentials = CredentialCache.DefaultCredentials;
                MyWebClient.Encoding = EnCodeType;
                strResult = MyWebClient.DownloadString(url);
            }
            catch (Exception)
            {
                strResult = "$GetFalse$";
            }
            return strResult;
        }
        /// <summary>
        /// 获取截取的内容,不包含首尾
        /// </summary>
        /// <param name="pageStr">原内容</param>
        /// <param name="strStart">开始标签</param>
        /// <param name="strEnd">结束标签</param>
        /// <returns></returns>
        public string GetBody(string pageStr, string strStart, string strEnd)
        {
            return GetBody(pageStr, strStart, strEnd, false, false);
        }
        /// <summary>
        /// 获取截取的内容
        /// </summary>
        /// <param name="pageStr">原内容</param>
        /// <param name="strStart">开始标签</param>
        /// <param name="strEnd">结束标签</param>
        /// <param name="inStart">是否包含头</param>
        /// <param name="inEnd">是否包含尾</param>
        /// <returns></returns>
        public string GetBody(string pageStr, string strStart, string strEnd, bool inStart, bool inEnd)
        {
            string sHtml = pageStr;
            int start = sHtml.IndexOf(strStart);
            if (strStart.Length == 0 || start < 0)
                return "$StartFalse$";
            sHtml = sHtml.Substring(start + strStart.Length, sHtml.Length - start - strStart.Length);
            int end = pageStr.IndexOf(strEnd);
            if (strEnd.Length == 0 || end < 0)
                return "$EndFalse$";
            return JumboECMS.Utils.Strings.GetHtml(pageStr, strStart, strEnd, inStart, inEnd);
        }
        /// <summary>
        /// 通过正则获得内容列表,不包含首尾
        /// </summary>
        /// <param name="pageStr"></param>
        /// <param name="strStart"></param>
        /// <param name="strEnd"></param>
        /// <returns></returns>
        public System.Collections.ArrayList GetArray(string pageStr, string strStart, string strEnd)
        {
            return GetArray(pageStr, strStart, strEnd, false, false);
        }
        /// <summary>
        /// 通过正则获得内容列表
        /// </summary>
        /// <param name="pageStr"></param>
        /// <param name="strStart">包含头</param>
        /// <param name="strEnd">包含尾</param>
        /// <returns></returns>
        public System.Collections.ArrayList GetArray(string pageStr, string strStart, string strEnd, bool getStart, bool getEnd)
        {
            string sHtml = pageStr;
            System.Collections.ArrayList bodyArray = new System.Collections.ArrayList();
            int start = sHtml.IndexOf(strStart);
            if (strStart.Length == 0 || start < 0)
            {
                bodyArray.Add("$StartFalse$");
                bodyArray.Add(strStart);
                return bodyArray;
            }
            sHtml = sHtml.Substring(start + strStart.Length, sHtml.Length - start - strStart.Length);
            int end = sHtml.IndexOf(strEnd);
            if (strEnd.Length == 0 || end < 0)
            {
                bodyArray.Add("$EndFalse$");
                bodyArray.Add(strEnd);
                return bodyArray;
            }
            bodyArray = JumboECMS.Utils.Strings.GetHtmls(pageStr, strStart, strEnd, getStart, getEnd);
            if (bodyArray.Count == 0) bodyArray.Add("$NoneBody$");
            return bodyArray;
        }
        /// <summary>
        /// 保存远程图片
        /// </summary>
        /// <param name="siteUrl">主站地址</param>
        /// <param name="mainSite">是否为主站</param>
        /// <param name="pageStr">页面内容</param>
        /// <param name="SavePath">保存路径</param>
        /// <param name="webUrl">内容中图片相对位置</param>
        /// <param name="isSave"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public System.Collections.ArrayList ProcessRemotePhotos(string mainSiteUrl, bool mainSite, string pageStr, string SavePath, string webUrl, bool isSave, int width, int height)
        {
            System.Collections.ArrayList replaceArray = new System.Collections.ArrayList();
            Regex imgReg = new Regex(@"<img.+?[^\>]>", RegexOptions.IgnoreCase);
            MatchCollection matches = imgReg.Matches(pageStr);
            string TempStr = string.Empty;
            string TitleImg = string.Empty;
            foreach (Match match in matches)
            {
                if (TempStr != string.Empty)
                    TempStr += "$Array$" + match.ToString();
                else
                    TempStr = match.ToString();
            }
            string[] TempArr = TempStr.Split(new string[] { "$Array$" }, StringSplitOptions.None);
            TempStr = string.Empty;
            imgReg = new Regex(@"src\s*=\s*.+?\.(gif|jpg|bmp|jpeg|psd|png)", RegexOptions.IgnoreCase);
            for (int i = 0; i < TempArr.Length; i++)
            {
                matches = imgReg.Matches(TempArr[i]);
                foreach (Match match in matches)
                {
                    if (TempStr != string.Empty)
                        TempStr += "$Array$" + match.ToString();
                    else
                        TempStr = match.ToString();
                }
            }
            if (TempStr.Length > 0)
            {
                imgReg = new Regex(@"src\s*=\s*", RegexOptions.IgnoreCase);
                TempStr = imgReg.Replace(TempStr, "");
            }
            if (TempStr.Length == 0)
            {
                replaceArray.Add(pageStr);
                return replaceArray;
            }
            TempStr = TempStr.Replace("\"", "");
            TempStr = TempStr.Replace("'", "");
            TempStr = TempStr.Replace(" ", "");

            //去掉重复图片
            TempArr = TempStr.Split(new string[] { "$Array$" }, StringSplitOptions.None);
            TempStr = string.Empty;
            for (int i = 0; i < TempArr.Length; i++)
            {
                if (TempStr.IndexOf(TempArr[i]) == -1)
                    TempStr += "$Array$" + TempArr[i];
            }
            TempStr = TempStr.Substring(7);

            TempArr = TempStr.Split(new string[] { "$Array$" }, StringSplitOptions.None);
            TempStr = string.Empty;
            string ImageArr = string.Empty;
            for (int i = 0; i < TempArr.Length; i++)
            {
                imgReg = new Regex(TempArr[i]);
                string RemoteFileUrl = DefiniteUrl(TempArr[i], webUrl);
                string LocalPhotoUrl = LocalFileUrl(mainSiteUrl, mainSite, RemoteFileUrl, SavePath, isSave, 0, 0);
                pageStr = imgReg.Replace(pageStr, LocalPhotoUrl);
                if (i == 0)
                {
                    //经过缩略
                    TitleImg = LocalFileUrl(mainSiteUrl, mainSite, RemoteFileUrl, SavePath, false, width, height);
                    ImageArr = LocalPhotoUrl;
                }
                else
                    ImageArr += "|||" + LocalPhotoUrl;
            }
            replaceArray.Add(pageStr);
            replaceArray.Add(TitleImg);
            replaceArray.Add(ImageArr);
            return replaceArray;
        }
        /// <summary>
        /// 远程图片本地化
        /// </summary>
        /// <param name="RemoteFileUrl"></param>
        /// <param name="webUrl">参考网站</param>
        /// <param name="SavePath">虚拟路径，以/结尾</param>
        /// <param name="isSave"></param>
        /// <param name="width">最后的宽,0表示原来尺寸</param>
        /// <param name="height">最后的高,0表示原来尺寸</param>
        /// <returns></returns>
        public string LocalFileUrl(string mainSiteUrl, bool mainSite, string RemoteFileUrl, string SavePath, bool isSave, int width, int height)
        {
            if (RemoteFileUrl.StartsWith(mainSiteUrl))//站内图片
            {
                if (mainSite)//是主站，直接去掉前缀
                    return RemoteFileUrl.Replace(mainSiteUrl, "");
                else
                    return RemoteFileUrl;
            }
            if (!RemoteFileUrl.StartsWith("http://"))//站内图片
            {
                if (mainSite)
                    return RemoteFileUrl;
                else
                    return mainSiteUrl + RemoteFileUrl;
            }
            string _LocalFileUrl = RemoteFileUrl;
            if (isSave)
            {
                string FolderName = DateTime.Now.ToString("yyMMdd");
                JumboECMS.Utils.DirFile.CreateDir(SavePath + FolderName);
                string fileType = _LocalFileUrl.Substring(_LocalFileUrl.LastIndexOf('.'));
                string filename = string.Empty;
                filename = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                if (width > 0 && height > 0)//表示缩略了
                    filename += "_thumbs" + fileType;
                else
                    filename += fileType;
                if (SaveRemotePhoto(HttpContext.Current.Server.MapPath(SavePath + FolderName + "/" + filename), _LocalFileUrl, width, height))
                    _LocalFileUrl = SavePath + FolderName + "/" + filename;
            }
            if (!mainSite && !_LocalFileUrl.StartsWith(mainSiteUrl) && !_LocalFileUrl.StartsWith("http://"))
                _LocalFileUrl = mainSiteUrl + _LocalFileUrl;
            return _LocalFileUrl;
        }
        /// <summary>
        /// 远程图片缩略化
        /// </summary>
        /// <param name="RemoteFileUrl"></param>
        /// <param name="SavePath">虚拟路径，以/结尾</param>
        /// <param name="isSave"></param>
        /// <param name="width">最后的宽,0表示原来尺寸</param>
        /// <param name="height">最后的高,0表示原来尺寸</param>
        /// <returns></returns>
        public string GetThumtnail(string mainSiteUrl, bool mainSite, string RemoteFileUrl, string SavePath, bool isSave, int width, int height)
        {
            string _LocalFileUrl = LocalFileUrl(mainSiteUrl, mainSite, RemoteFileUrl, SavePath, isSave, width, height);
            if (_LocalFileUrl == RemoteFileUrl && !_LocalFileUrl.EndsWith("_thumbs.jpg"))//表示非远程图片，非缩略图
            {
                RemoteFileUrl = RemoteFileUrl.Replace(mainSiteUrl, "");
                string FolderName = DateTime.Now.ToString("yyMMdd");
                JumboECMS.Utils.DirFile.CreateDir(SavePath + FolderName);
                string filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_thumbs.jpg";
                _LocalFileUrl = SavePath + FolderName + "/" + filename;
                JumboECMS.Utils.ImageHelp.LocalImage2Thumbs(HttpContext.Current.Server.MapPath(RemoteFileUrl), HttpContext.Current.Server.MapPath(_LocalFileUrl), width, height, "Fill");

            }
            if (!mainSite && !_LocalFileUrl.StartsWith(mainSiteUrl))
                _LocalFileUrl = mainSiteUrl + _LocalFileUrl;
            return _LocalFileUrl;
        }
        /// <summary>
        /// 取得实际地址
        /// </summary>
        /// <param name="PrimitiveUrl"></param>
        /// <param name="ConsultUrl"></param>
        /// <returns></returns>
        public string DefiniteUrl(string PrimitiveUrl, string ConsultUrl)
        {
            if (!ConsultUrl.StartsWith("http://"))
                ConsultUrl = "http://" + ConsultUrl;
            ConsultUrl = ConsultUrl.Replace("\\", "/");
            ConsultUrl = ConsultUrl.Replace("://", ":\\\\");
            PrimitiveUrl = PrimitiveUrl.Replace("\\", "/");

            if (ConsultUrl.Substring(ConsultUrl.Length - 1) != "/")
            {
                if (ConsultUrl.IndexOf('/') > 0)
                {
                    if (ConsultUrl.Substring(ConsultUrl.LastIndexOf("/"), ConsultUrl.Length - ConsultUrl.LastIndexOf("/")).IndexOf('.') == -1)//不含文件名，有缺陷，暂如此
                        ConsultUrl += "/";
                }
                else//直接是域名
                    ConsultUrl += "/";
            }
            string[] ConArray = ConsultUrl.Split('/');
            string returnStr = string.Empty;
            string[] PriArray;
            int pi = 0;
            if (PrimitiveUrl.Substring(0, 7) == "http://")
                returnStr = PrimitiveUrl.Replace("://", @":\\");
            else if (PrimitiveUrl.Substring(0, 1) == "/")//如果是绝对路径
                returnStr = ConArray[0] + PrimitiveUrl;
            else if (PrimitiveUrl.Substring(0, 2) == "./")//如果是当前路径
            {
                PrimitiveUrl = PrimitiveUrl.Substring(2, PrimitiveUrl.Length - 2);
                if (ConsultUrl.Substring(ConsultUrl.Length - 1) == "/")
                    returnStr = ConsultUrl + PrimitiveUrl;
                else
                    returnStr = ConsultUrl.Substring(0, ConsultUrl.LastIndexOf('/') + 1) + PrimitiveUrl;
            }
            else if (PrimitiveUrl.Substring(0, 3) == "../")//如果是相对父路径
            {
                while (PrimitiveUrl.Substring(0, 3) == "../")
                {
                    PrimitiveUrl = PrimitiveUrl.Substring(3);
                    pi++;
                }
                for (int i = 0; i < ConArray.Length - 1 - pi; i++)
                {
                    if (returnStr.Length > 0)
                        returnStr = returnStr + ConArray[i] + "/";
                    else
                        returnStr = ConArray[i] + "/";
                }
                returnStr = returnStr + PrimitiveUrl;
            }
            else//真实地址
            {
                if (PrimitiveUrl.IndexOf('/') > -1)
                {
                    PriArray = PrimitiveUrl.Split('/');
                    if (PriArray[0].IndexOf('.') > -1)
                    {
                        if (PrimitiveUrl.Substring(PrimitiveUrl.Length - 1) == "/")
                            returnStr = "http://" + PrimitiveUrl;
                        {
                            if (PriArray[PriArray.Length - 1].IndexOf('.') > -1)
                                returnStr = "http:\\" + PrimitiveUrl;
                            else
                                returnStr = "http:\\" + PrimitiveUrl + "/";
                        }
                    }
                    else
                    {
                        if (ConsultUrl.Substring(ConsultUrl.Length - 1) == "/")
                            returnStr = ConsultUrl + PrimitiveUrl;
                        else
                            returnStr = ConsultUrl.Substring(0, ConsultUrl.LastIndexOf('/') + 1) + PrimitiveUrl;
                    }
                }
                else
                {
                    if (PrimitiveUrl.IndexOf('.') > -1)
                    {
                        string lastUrl = ConsultUrl.Substring(ConsultUrl.LastIndexOf('.'));
                        if (ConsultUrl.Substring(ConsultUrl.Length - 1) == "/")
                        {
                            if (lastUrl == "com" || lastUrl == "cn" || lastUrl == "net" || lastUrl == "org")
                                returnStr = "http:\\" + PrimitiveUrl + "/";
                            else
                                returnStr = ConsultUrl + PrimitiveUrl;
                        }
                        else
                        {
                            if (lastUrl == "com" || lastUrl == "cn" || lastUrl == "net" || lastUrl == "org")
                                returnStr = "http:\\" + PrimitiveUrl + "/";
                            else
                                returnStr = ConsultUrl.Substring(0, ConsultUrl.LastIndexOf('/')) + "/" + PrimitiveUrl;
                        }
                    }
                    else
                    {
                        if (ConsultUrl.Substring(ConsultUrl.Length - 1) == "/")
                            returnStr = ConsultUrl + PrimitiveUrl + "/";
                        else
                            returnStr = ConsultUrl.Substring(0, ConsultUrl.LastIndexOf('/')) + "/" + PrimitiveUrl + "/";
                    }
                }
            }

            if (returnStr.Substring(0, 1) == "/")
                returnStr = returnStr.Substring(1);
            if (returnStr.Length > 0)
            {
                returnStr = returnStr.Replace("//", "/");
                returnStr = returnStr.Replace(@":\\", "://");
            }
            else
                returnStr = "$False$";
            return returnStr;
        }
        /// <summary>
        /// 抓取远程图片
        /// </summary>
        /// <param name="fileName">如果是要缩略图，记得图片名以_thumbs.jpg结尾</param>
        /// <param name="RemoteFileUrl"></param>
        /// <param name="width">最后的宽,0表示原来尺寸</param>
        /// <param name="height">最后的高,0表示原来尺寸</param>
        /// <returns></returns>
        public bool SaveRemotePhoto(string fileName, string RemoteFileUrl, int width, int height)
        {
            //try
            //{
            //为防止图片未能本地化后，内网比较纠结，干脆抓取不到时直接报错
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(RemoteFileUrl);
            request.Timeout = 20000;
            request.KeepAlive = false;
            Stream stream = request.GetResponse().GetResponseStream();
            System.Drawing.Image getImage = System.Drawing.Image.FromStream(stream);
            if (width > 0 && height > 0)
                JumboECMS.Utils.ImageHelp.Image2Thumbs(getImage, fileName, width, height, "Fill");
            else
            {
                try
                {
                    getImage.Save(fileName);
                }
                catch
                {
                    System.Drawing.Bitmap t = new System.Drawing.Bitmap(getImage);
                    t.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    t.Dispose();
                }
            }
            getImage.Dispose();
            return true;
            // }
            //catch (Exception)
            //{
            //    return false;
            //}
            //if (width > 0 && height > 0)//数据流读图片
            //{
            //    WebRequest request = WebRequest.Create(RemoteFileUrl);
            //    request.Timeout = 20000;
            //    Stream stream = request.GetResponse().GetResponseStream();
            //    System.Drawing.Image getImage = System.Drawing.Image.FromStream(stream);
            //    JumboECMS.Utils.ImageHelp.Image2Thumbs(getImage, fileName, width, height, "Fill");
            //    getImage.Dispose();
            //}
            //else//直接下载
            //{
            //    WebClient client = new WebClient();
            //    client.DownloadFile(RemoteFileUrl, fileName);
            //}
            //return true;

        }
        /// <summary>
        /// 去除标记
        /// </summary>
        /// <param name="ConStr"></param>
        /// <param name="TagName"></param>
        /// <param name="FType">1：将标记内全清空（没有尾标签；2：将标记内的东西全清空；3：只清除首尾标记</param>
        /// <returns></returns>
        public string ScriptHtml(string ConStr, string TagName, int FType)
        {
            Regex myReg;
            switch (FType)
            {
                case 1:
                    myReg = new Regex("<" + TagName + "[^>]*>", RegexOptions.IgnoreCase);
                    ConStr = myReg.Replace(ConStr, "");
                    break;
                case 2:
                    myReg = new Regex("<" + TagName + "[^>]*>[\\s\\S]*?</" + TagName + "[^>]*>", RegexOptions.IgnoreCase);
                    ConStr = myReg.Replace(ConStr, "");
                    break;
                case 3:
                    myReg = new Regex("<" + TagName + "[^>]*>", RegexOptions.IgnoreCase);
                    ConStr = myReg.Replace(ConStr, "");
                    myReg = new Regex("</" + TagName + "[^>]*>", RegexOptions.IgnoreCase);
                    ConStr = myReg.Replace(ConStr, "");
                    break;
            }
            return ConStr;
        }

        public string DelHtml(string ConStr)
        {
            Regex myReg = new Regex(@"(\<.[^\<]*\>)", RegexOptions.IgnoreCase);
            ConStr = myReg.Replace(ConStr, "");
            myReg = new Regex(@"(\<\/[^\<]*\>)", RegexOptions.IgnoreCase);
            ConStr = myReg.Replace(ConStr, "");
            return ConStr;
        }

        public string GetPaing(string pageStr, string strStart, string strEnd)
        {
            string pageStrTmp = pageStr;
            int start = pageStrTmp.IndexOf(strStart);
            if (strStart.Length == 0 || start < 0)
                return "$StartFalse$";
            pageStrTmp = pageStrTmp.Substring(start + strStart.Length, pageStrTmp.Length - start - strStart.Length);

            int end = pageStrTmp.IndexOf(strEnd);
            if (strEnd.Length == 0 || end < 0)
                return "$EndFalse$";
            pageStr = JumboECMS.Utils.Strings.GetHtml(pageStr, strStart, strEnd);
            pageStr = pageStr.Split('"')[0];
            pageStr = pageStr.Split('\'')[0];
            pageStr = pageStr.Replace(" ", "");
            pageStr = pageStr.Trim();
            return pageStr;
        }
    }
}
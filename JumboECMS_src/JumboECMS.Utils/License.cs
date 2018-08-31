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
using System.Text;
using System.Data;
using JumboECMS.Utils;
using SafeSys.SafeSysData;
using System.Management;
namespace JumboECMS.Utils
{
    public static class License
    {
        /// <summary>
        /// 获得机器码
        /// </summary>
        /// <returns></returns>
        public static string GetMachineCode()
        {
            string s = "";
            byte[] macAddress = Win32Util.GetMacAddress();
            for (int i = 0; i < macAddress.Length; i++)
            {
                s = s + macAddress[i].ToString();
            }
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_BIOS");
            foreach (ManagementObject obj2 in searcher.Get())
            {
                s = s + obj2["Name"];
                s = s + obj2["BuildNumber"];
                s = s + obj2["Manufacturer"];
                s = s + obj2["SMBIOSBIOSVersion"];
            }
            byte[] bytes = new UnicodeEncoding().GetBytes(s);
            long hash = 0L;
            TeaEncrypt.TeaHash2(bytes, bytes.Length, out hash);
            bytes = BitConverter.GetBytes(hash);
            return string.Format("{0,2:X}{1,2:X}-{2,2:X}{3,2:X}-{4,2:X}{5,2:X}-{6,2:X}{7,2:X}", new object[] { bytes[0], bytes[1], bytes[2], bytes[3], bytes[4], bytes[5], bytes[6], bytes[7] }).Replace('O', 'P').Replace('L', 'A').Replace('I', 'C').Replace('1', 'Z').Replace(' ', 'K');
        }
        /// <summary>
        /// 获得注册码
        /// </summary>
        /// <param name="_username">授权用户</param>
        /// <param name="_domainname">授权域名,为空表示域名不受限制,多个域名用分号</param>
        /// <param name="_machinecode">机器码</param>
        /// <returns></returns>
        public static string GetSerialCode(string _username, string _domainname, string _machinecode)
        {
            long num;
            string s = _username + "," + _domainname + "," + _machinecode.ToUpper();
            byte[] bytes = new UnicodeEncoding().GetBytes(s);
            TeaEncrypt.TeaHash2(bytes, bytes.Length, out num);
            byte[] buffer2 = BitConverter.GetBytes(num);
            return string.Format("{0,2:X}{1,2:X}-{2,2:X}{3,2:X}-{4,2:X}{5,2:X}-{6,2:X}{7,2:X}", new object[] { buffer2[0], buffer2[1], buffer2[2], buffer2[3], buffer2[4], buffer2[5], buffer2[6], buffer2[7] }).Replace('O', 'F').Replace('L', 'Y').Replace('I', 'X').Replace('1', 'B').Replace(' ', 'A');
        }
        /// <summary>
        /// 是否已授权
        /// </summary>
        /// <param name="_username">授权用户</param>
        /// <param name="_domainname">授权域名,为空表示域名不受限制,多个域名用分号</param>
        /// <param name="_machinecode">机器码</param>
        /// <param name="_serialcode">注册码</param>
        /// <returns></returns>
        public static bool IsAuthorized(string _username, string _domainname, string _machinecode, string _serialcode)
        {
            string _thisDomain = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToString();
            //if (_thisDomain.IndexOf(".") < 0)//说明是计算机名(内网测试)
            if (!_thisDomain.Contains("."))
                return true;
            if(JumboECMS.Utils.Validator.IsIP(_thisDomain))//说明是IP地址
                return true;
            if (_username.Length == 0)
                return false;
            bool domainisOk = false;
            if (_domainname != "")//判断域名
            {
                string[] _strDomains = _domainname.Split(';');
                for (int i = 0; i < _strDomains.Length; i++)
                {
                    //if (_thisDomain.IndexOf(_strDomains[i].Trim()) < 0)//判断当前域名是否在作用域内
                    if (!_thisDomain.Contains(_strDomains[i].Trim()))
                        continue;
                    else
                    {
                        domainisOk = true;
                        break;
                    }
                }
            }
            else
                domainisOk = true;
            if (!domainisOk)
                return false;
            if (_machinecode.ToUpper() != GetMachineCode())//获得的机器码不对
                return false;
            if (GetSerialCode(_username, _domainname, _machinecode) != _serialcode.ToUpper())
                return false;
            return true;
        }
    }
}

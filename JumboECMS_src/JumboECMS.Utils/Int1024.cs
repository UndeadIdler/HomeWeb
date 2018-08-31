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
using System.Runtime.InteropServices;
namespace JumboECMS.Utils
{

    [Serializable, StructLayout(LayoutKind.Sequential, Pack=1)]
    public struct Int1024
    {
        public static readonly int Size;
        public Int512 Lo;
        public Int512 Hi;
        public byte[] GetBytes()
        {
            byte[] destinationArray = new byte[0x80];
            Array.Copy(this.Lo.GetBytes(), 0, destinationArray, 0, 0x40);
            Array.Copy(this.Hi.GetBytes(), 0, destinationArray, 0x40, 0x40);
            return destinationArray;
        }

        public static Int1024 ToInt1024(byte[] buf, int startIndex)
        {
            Int1024 num;
            num.Lo = Int512.ToInt512(buf, startIndex);
            num.Hi = Int512.ToInt512(buf, startIndex + 0x40);
            return num;
        }

        static Int1024()
        {
            Size = 0x80;
        }
    }
}


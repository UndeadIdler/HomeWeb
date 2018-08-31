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
    public struct Int512
    {
        public static readonly int Size;
        public Int256 Lo;
        public Int256 Hi;
        public byte[] GetBytes()
        {
            byte[] destinationArray = new byte[0x40];
            Array.Copy(this.Lo.GetBytes(), 0, destinationArray, 0, 0x20);
            Array.Copy(this.Hi.GetBytes(), 0, destinationArray, 0x20, 0x20);
            return destinationArray;
        }

        public static Int512 ToInt512(byte[] buf, int startIndex)
        {
            Int512 num;
            num.Lo = Int256.ToInt256(buf, startIndex);
            num.Hi = Int256.ToInt256(buf, startIndex + 0x20);
            return num;
        }

        static Int512()
        {
            Size = 0x40;
        }
    }
}


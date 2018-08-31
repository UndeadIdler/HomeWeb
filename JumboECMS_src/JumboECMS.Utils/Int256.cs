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
    public struct Int256
    {
        public static readonly int Size;
        public Int128 Lo;
        public Int128 Hi;
        public byte[] GetBytes()
        {
            byte[] destinationArray = new byte[0x20];
            Array.Copy(this.Lo.GetBytes(), 0, destinationArray, 0, 0x10);
            Array.Copy(this.Hi.GetBytes(), 0, destinationArray, 0x10, 0x10);
            return destinationArray;
        }

        public static Int256 ToInt256(byte[] buf, int startIndex)
        {
            Int256 num;
            num.Lo = Int128.ToInt128(buf, startIndex);
            num.Hi = Int128.ToInt128(buf, startIndex + 0x10);
            return num;
        }

        static Int256()
        {
            Size = 0x20;
        }
    }
}


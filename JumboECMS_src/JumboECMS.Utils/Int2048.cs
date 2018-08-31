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
    public struct Int2048
    {
        public static readonly int Size;
        public Int1024 Lo;
        public Int1024 Hi;
        public byte[] GetBytes()
        {
            byte[] destinationArray = new byte[0x100];
            Array.Copy(this.Lo.GetBytes(), 0, destinationArray, 0, 0x80);
            Array.Copy(this.Hi.GetBytes(), 0, destinationArray, 0x80, 0x80);
            return destinationArray;
        }

        public static Int2048 ToInt2048(byte[] buf, int startIndex)
        {
            Int2048 num;
            num.Lo = Int1024.ToInt1024(buf, startIndex);
            num.Hi = Int1024.ToInt1024(buf, startIndex + 0x80);
            return num;
        }

        static Int2048()
        {
            Size = 0x100;
        }
    }
}


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
    public struct Int128
    {
        public static readonly int Size;
        public long Lo;
        public long Hi;
        public byte[] GetBytes()
        {
            byte[] destinationArray = new byte[0x10];
            Array.Copy(BitConverter.GetBytes(this.Lo), 0, destinationArray, 0, 8);
            Array.Copy(BitConverter.GetBytes(this.Hi), 0, destinationArray, 8, 8);
            return destinationArray;
        }

        public static Int128 ToInt128(byte[] buf, int startIndex)
        {
            Int128 num;
            num.Lo = BitConverter.ToInt64(buf, startIndex);
            num.Hi = BitConverter.ToInt64(buf, startIndex + 8);
            return num;
        }

        static Int128()
        {
            Size = 0x10;
        }
    }
}


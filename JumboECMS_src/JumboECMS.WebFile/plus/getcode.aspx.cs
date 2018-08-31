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

using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Cryptography;
namespace JumboECMS.WebFile.Plus
{
    public partial class _getcode : JumboECMS.UI.BasicPage
    {
        private int letterWidth = 16;//单个字体的宽度范围
        private int letterHeight = 20;//单个字体的高度范围
        private int letterCount = 4;//验证码位数
        private static byte[] randb = new byte[4];
        private static RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();
        /// <summary>
        /// 产生波形滤镜效果
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            //防止网页后退--禁止缓存    
            Response.Expires = 0;
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            Response.AddHeader("pragma", "no-cache");
            Response.CacheControl = "no-cache";
            letterWidth = Str2Int(q("w"), 16);
            letterHeight = Str2Int(q("h"), 20);
            string str_ValidateCode = JumboECMS.Common.ValidateCode.GetValidateCode(letterCount, true);
            CreateImage(str_ValidateCode);

        }
        /// <summary>
        /// 获得下一个随机数
        /// </summary>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        private static int Next(int max)
        {
            rand.GetBytes(randb);
            int value = BitConverter.ToInt32(randb, 0);
            value = value % (max + 1);
            if (value < 0)
                value = -value;
            return value;
        }
        /// <summary>
        /// 获得下一个随机数
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        private static int Next(int min, int max)
        {
            int value = Next(max - min) + min;
            return value;
        }
        public void CreateImage(string checkCode)
        {
            int _basefont = (letterHeight / 10) * 5;
            int _next = letterHeight / 4 - 4;

            Font[] fonts = { 
                               new Font(new FontFamily("Times New Roman"),(_basefont + Next(_next)),System.Drawing.FontStyle.Strikeout),
                               new Font(new FontFamily("Tahoma"), (_basefont + Next(_next)),System.Drawing.FontStyle.Regular),
                               new Font(new FontFamily("Arial"), (_basefont + Next(_next)),System.Drawing.FontStyle.Italic),
                               new Font(new FontFamily("Comic Sans MS"), (_basefont + Next(_next)),System.Drawing.FontStyle.Italic)
                           };

            int int_ImageWidth = checkCode.Length * letterWidth;
            Bitmap image = new Bitmap(int_ImageWidth, letterHeight);
            Graphics g = Graphics.FromImage(image);
            //白色背景
            g.Clear(Color.White);
            //画图片的背景噪音线
            for (int i = 0; i < 2; i++)
            {
                int x1 = Next(image.Width - 1);
                int x2 = Next(image.Width - 1);
                int y1 = Next(image.Height - 1);
                int y2 = Next(image.Height - 1);

                g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
            }
            //随机字体和颜色的验证码字符
            int _x = -12, _y = 0;
            for (int int_index = 0; int_index < checkCode.Length; int_index++)
            {
                _x += Next(12, 16);
                _y = Next(-2, 2);
                string str_char = checkCode.Substring(int_index, 1);
                str_char = Next(1) == 1 ? str_char.ToLower() : str_char.ToUpper();
                Brush newBrush = new SolidBrush(GetRandomColor());//随机颜色
                Point thePos = new Point(_x, _y);
                g.DrawString(str_char, fonts[Next(fonts.Length - 1)], newBrush, thePos);
            }

            //画图片的前景噪音点
            for (int i = 0; i < 10; i++)
            {
                int x = Next(image.Width - 1);
                int y = Next(image.Height - 1);

                image.SetPixel(x, y, Color.FromArgb(Next(0, 255), Next(0, 255), Next(0, 255)));
            }
            //图片扭曲
            //image = TwistImage(image, true, Next(1, 3), Next(4, 6));//
            //灰色边框
            //g.DrawRectangle(new Pen(Color.LightGray, 1), 0, 0, int_ImageWidth - 1, (letterHeight - 1));
            //将生成的图片发回客户端
            MemoryStream ms = new MemoryStream();
            image.Save(ms, ImageFormat.Png);
            Response.ClearContent(); //需要输出图象信息 要修改HTTP头 
            Response.ContentType = "image/Png";
            Response.BinaryWrite(ms.ToArray());
            g.Dispose();
            image.Dispose();
        }
        /// <summary>
        /// 正弦曲线Wave扭曲图片
        /// </summary>
        /// <param name="srcBmp">图片路径</param>
        /// <param name="bXDir">如果扭曲则选择为True</param>
        /// <param name="nMultValue">波形的幅度倍数，越大扭曲的程度越高，一般为3</param>
        /// <param name="dPhase">波形的起始相位，取值区间[0-2*PI)</param>
        /// <returns></returns>
        public System.Drawing.Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            double PI = 6.283185307179586476925286766559;
            Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);
            // 将位图背景填充为白色
            Graphics graph = Graphics.FromImage(destBmp);
            graph.FillRectangle(new SolidBrush(Color.White), 0, 0, destBmp.Width, destBmp.Height);
            graph.Dispose();
            double dBaseAxisLen = bXDir ? (double)destBmp.Height : (double)destBmp.Width;
            for (int i = 0; i < destBmp.Width; i++)
            {
                for (int j = 0; j < destBmp.Height; j++)
                {
                    double dx = 0;
                    dx = bXDir ? (PI * (double)j) / dBaseAxisLen : (PI * (double)i) / dBaseAxisLen;
                    dx += dPhase;
                    double dy = Math.Sin(dx);

                    // 取得当前点的颜色
                    int nOldX = 0, nOldY = 0;
                    nOldX = bXDir ? i + (int)(dy * dMultValue) : i;
                    nOldY = bXDir ? j : j + (int)(dy * dMultValue);

                    Color color = srcBmp.GetPixel(i, j);
                    if (nOldX >= 0 && nOldX < destBmp.Width
                     && nOldY >= 0 && nOldY < destBmp.Height)
                    {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }
            srcBmp.Dispose();
            return destBmp;
        }
        /// <summary>
        /// 字体随机颜色
        /// </summary>
        /// <returns></returns>
        public Color GetRandomColor()
        {
            return Color.FromArgb(0, 0, 0);//纯黑色
            //Random RandomNum_First = new Random((int)DateTime.Now.Ticks);
            //System.Threading.Thread.Sleep(RandomNum_First.Next(50));
            //Random RandomNum_Sencond = new Random((int)DateTime.Now.Ticks);

            //  //为了在白色背景上显示，尽量生成深色
            //int int_Red = RandomNum_First.Next(180);
            //int int_Green = RandomNum_Sencond.Next(180);
            //int int_Blue = (int_Red + int_Green > 300) ? 0 : 400 - int_Red - int_Green;
            //int_Blue = (int_Blue > 255) ? 255 : int_Blue;
            //return Color.FromArgb(int_Red, int_Green, int_Blue);
        }
    }
}
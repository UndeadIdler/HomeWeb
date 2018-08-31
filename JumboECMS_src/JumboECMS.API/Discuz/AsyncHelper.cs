using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;

namespace JumboECMS.API.Discuz
{
    public class AsyncHelper
    {
        public static bool WriteAsyncLog(string action)
        {
            //HttpContext.Current.Request.PhysicalApplicationPath
            try
            {
                string actionString = string.Format("DataTime:{0}--Action:{1}\r\n", DateTime.Now.ToString(), action);
                string fileName = HttpContext.Current.Request.PhysicalApplicationPath + "\\asynclog.txt";
                StreamWriter sw;

                if (File.Exists(fileName))
                {
                    sw = File.AppendText(fileName);

                }
                else
                {
                    sw = File.CreateText(fileName);
                }
                sw.Write(actionString);
                sw.Flush();
                sw.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JumboECMS.API.Discuz.Toolkit
{
    /// <summary>
    /// Discuz异常类
    /// </summary>
    public class DiscuzException : Exception
    {
        private int error_code;
        private string error_message;

        /// <summary>
        /// 获取异常代码
        /// </summary>
        public int ErrorCode
        {
            get { return error_code; }
        }

        /// <summary>
        /// 获取异常描述
        /// </summary>
        public string ErrorMessage
        {
            get { return error_message; }
        }

        /// <summary>
        /// Discuz错误异常
        /// </summary>
        /// <param name="error_code">异常错误代码</param>
        /// <param name="error_message">异常描述</param>
        public DiscuzException(int error_code, string error_message)
            : base(CreateMessage(error_code, error_message))
        {
            this.error_code = error_code;
            this.error_message = error_message;
        }

        /// <summary>
        /// 生成异常信息
        /// </summary>
        /// <param name="error_code">异常错误代码</param>
        /// <param name="error_message">异常描述</param>
        /// <returns>异常信息</returns>
        private static string CreateMessage(int error_code, string error_message)
        {
            return string.Format("Code: {0}, Message: {1}", error_code, error_message);
        }
    }
}

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
namespace JumboECMS.Entity
{
    /// <summary>
    /// 会员购物车-------表映射实体
    /// </summary>

    public class Normal_UserCart
    {
        public Normal_UserCart()
        { }

        private int _id;
        private int _productid = 0;
        private string _productlink = "";
        private int _buycount = 0;
        private DateTime _carttime = DateTime.Now;
        private int _state = 0;
        private int _userid = 0;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        public int ProductId
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 产品链接
        /// </summary>
        public string ProductLink
        {
            set { _productlink = value; }
            get { return _productlink; }
        }
        /// <summary>
        /// 订购数量
        /// </summary>
        public int BuyCount
        {
            set { _buycount = value; }
            get { return _buycount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CartTime
        {
            set { _carttime = value; }
            get { return _carttime; }
        }
        /// <summary>
        /// 状态
        /// 0表示未未处理；1表示已处理
        /// </summary>
        public int State
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 会员编号
        /// </summary>
        public int UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }

    }
}


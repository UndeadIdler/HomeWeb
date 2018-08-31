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
    /// 会员商品-------表映射实体
    /// </summary>

    public class Normal_UserGoods
    {
        public Normal_UserGoods()
        { }

        private int _id;
        private string _ordernum = "";
        private int _productid = 0;
        private string _productname = "";
        private string _productimg = "";
        private string _productlink = "";
        private float _unitprice = 0;
        private int _buycount = 0;
        private float _totalprice = 0;
        private DateTime _goodstime = DateTime.Now;
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
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNum
        {
            set { _ordernum = value; }
            get { return _ordernum; }
        }
        public int ProductId
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName
        {
            set { _productname = value; }
            get { return _productname; }
        }
        public string ProductImg
        {
            set { _productimg = value; }
            get { return _productimg; }
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
        /// 单价
        /// </summary>
        public float UnitPrice
        {
            set { _unitprice = value; }
            get { return _unitprice; }
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
        public DateTime GoodsTime
        {
            set { _goodstime = value; }
            get { return _goodstime; }
        }
        /// <summary>
        /// 状态
        /// 0表示未付款；1表示已付款；2表示已发货；3表示已成功
        /// </summary>
        public int State
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 总价
        /// </summary>
        public float TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
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


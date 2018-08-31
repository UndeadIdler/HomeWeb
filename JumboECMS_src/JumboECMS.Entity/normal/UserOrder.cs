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
    /// 会员订单-------表映射实体
    /// </summary>

    public class Normal_UserOrder
    {
        public Normal_UserOrder()
        { }

        private int _id;
        private string _ordernum = "";
        private string _truename;
        private string _address;
        private string _zipcode;
        private string _mobiletel;
        private string _paymentway = "";
        private float _money = 0;
        private DateTime _ordertime = DateTime.Now;
        private string _orderip = "";
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
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string TrueName
        {
            set { _truename = value; }
            get { return _truename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZipCode
        {
            set { _zipcode = value; }
            get { return _zipcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MobileTel
        {
            set { _mobiletel = value; }
            get { return _mobiletel; }
        }
        /// <summary>
        /// 支付方式
        /// 如：alipay、tenpay等
        /// </summary>
        public string PaymentWay
        {
            set { _paymentway = value; }
            get { return _paymentway; }
        }
        /// <summary>
        /// 需要的费用
        /// </summary>
        public float Money
        {
            set { _money = value; }
            get { return _money; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime OrderTime
        {
            set { _ordertime = value; }
            get { return _ordertime; }
        }
        public string OrderIP
        {
            set { _orderip = value; }
            get { return _orderip; }
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
        /// 会员编号
        /// </summary>
        public int UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }

    }
}


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
using JumboECMS.Utils;
using JumboECMS.DBUtility;

namespace JumboECMS.DAL
{
    /// <summary>
    /// 会员订单表信息
    /// </summary>
    public class Normal_UserOrderDAL : Common
    {
        public Normal_UserOrderDAL()
        {
            base.SetupSystemDate();
        }
        /// <summary>
        /// 新增订单信息
        /// </summary>
        /// <param name="_uid"></param>
        /// <param name="_truename"></param>
        /// <param name="_address"></param>
        /// <param name="_zipcode"></param>
        /// <param name="_mobiletel"></param>
        /// <returns></returns>
        public bool NewOrder(string _uid, string _truename, string _address, string _zipcode, string _mobiletel)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string _ordernum = GetProductOrderNum();//订单号
                int page = 1;
                int PSize = 1000;
                int countNum = 0;
                string sqlStr = "";
                string joinStr = "A.[ProductId]=B.Id";
                string whereStr1 = "A.State=0 AND A.UserId=" + _uid;
                string whereStr2 = "State=0 AND UserId=" + _uid;
                _doh.Reset();
                _doh.ConditionExpress = whereStr2;
                countNum = _doh.Count("jcms_normal_user_cart");
                sqlStr = JumboECMS.Utils.SqlHelp.GetSql0("A.*,b.price1 as unitprice,(b.price1*a.buycount) as totalprice,b.id as productid,b.title as productname,b.img as productimg", "jcms_normal_user_cart", "jcms_module_product", "Id", PSize, page, "desc", joinStr, whereStr1, whereStr2);
                _doh.Reset();
                _doh.SqlCmd = sqlStr;
                DataTable dt = _doh.GetDataTable();
                if (dt.Rows.Count == 0)
                    return false;
                float _money = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JumboECMS.Entity.Normal_UserGoods _goods = new JumboECMS.Entity.Normal_UserGoods();
                    _goods.UserId = Str2Int(_uid);
                    _goods.OrderNum = _ordernum;
                    _goods.ProductId = Str2Int(dt.Rows[i]["ProductId"].ToString());
                    _goods.ProductName = dt.Rows[i]["ProductName"].ToString();
                    _goods.ProductImg = dt.Rows[i]["ProductImg"].ToString();
                    _goods.ProductLink = dt.Rows[i]["ProductLink"].ToString();
                    _goods.UnitPrice = Convert.ToSingle(dt.Rows[i]["UnitPrice"].ToString());
                    _goods.BuyCount = Str2Int(dt.Rows[i]["BuyCount"].ToString());
                    _goods.TotalPrice = Convert.ToSingle(dt.Rows[i]["TotalPrice"].ToString());
                    new JumboECMS.DAL.Normal_UserGoodsDAL().NewGoods(_goods);
                    _money += _goods.TotalPrice;
                }
                dt.Clear();
                dt.Dispose();
                _doh.Reset();
                _doh.AddFieldItem("UserId", _uid);
                _doh.AddFieldItem("OrderNum", _ordernum);
                _doh.AddFieldItem("TrueName", _truename);
                _doh.AddFieldItem("Address", _address);
                _doh.AddFieldItem("ZipCode", _zipcode);
                _doh.AddFieldItem("MobileTel", _mobiletel);
                _doh.AddFieldItem("Money", _money);
                _doh.AddFieldItem("State", 0);
                _doh.AddFieldItem("OrderTime", DateTime.Now.ToString());
                _doh.AddFieldItem("OrderIP", IPHelp.ClientIP);
                _doh.Insert("jcms_normal_user_order");
                _doh.Reset();
                _doh.SqlCmd = string.Format("UPDATE [jcms_normal_user_cart] SET [State]=1 WHERE UserId={0}", _uid);
                _doh.ExecuteSqlNonQuery();
                return true;
            }
        }
        /// <summary>
        /// 更新订单
        /// </summary>
        /// <param name="_uid"></param>
        /// <param name="_ordernum">通过订单号棋查询</param>
        /// <param name="_state">1表示付款；2表示交易完成(货收到了)</param>
        /// <param name="_payway">如：alipay、tenpay等</param>
        /// <returns></returns>
        public int UpdateOrder(string _uid, string _ordernum, int _state, string _payway)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                int _num = 0;
                if (_state == 1)
                {
                    _doh.Reset();
                    _doh.ConditionExpress = "OrderNum='" + _ordernum + "' and state=0 and userid=" + _uid;
                    _doh.AddFieldItem("State", 1);
                    _doh.AddFieldItem("PaymentWay", _payway);
                    _num = _doh.Update("jcms_normal_user_order");
                }
                else if (_state == 2)
                {
                    _doh.Reset();
                    _doh.ConditionExpress = "OrderNum='" + _ordernum + "' and state=1 and userid=" + _uid;
                    _doh.AddFieldItem("State", 2);
                    _num = _doh.Update("jcms_normal_user_order");
                }
                return _num;
            }
        }
        /// <summary>
        /// 统计会员的订单数
        /// </summary>
        /// <param name="_uid"></param>
        /// <param name="_state">状态：-1表示所有</param>
        /// <returns></returns>
        public int GetOrderTotal(string _uid, int _state)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                if (_state < 0)
                    _doh.ConditionExpress = "userid=" + _uid;
                else
                    _doh.ConditionExpress = "state=" + _state + " and userid=" + _uid;
                return _doh.Count("jcms_normal_user_order");
            }
        }
        /// <summary>
        /// 获得订单的总金额
        /// </summary>
        /// <param name="_uid"></param>
        /// <param name="_ordernum"></param>
        /// <returns></returns>
        public float GetOrderMoney(string _uid, string _ordernum)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "OrderNum='" + _ordernum + "' and userid=" + _uid;
                return Convert.ToSingle(_doh.GetField("jcms_normal_user_order", "Money").ToString());
            }
        }
    }
}

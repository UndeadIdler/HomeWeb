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
    /// 会员购物车信息
    /// </summary>
    public class Normal_UserCartDAL : Common
    {
        public Normal_UserCartDAL()
        {
            base.SetupSystemDate();
        }
        /// <summary>
        /// 新增购物车商品信息
        /// </summary>
        /// <param name="_cart"></param>
        /// <returns></returns>
        public int NewGoods(JumboECMS.Entity.Normal_UserCart _cart)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.AddFieldItem("UserId", _cart.UserId);
                _doh.AddFieldItem("ProductId", _cart.ProductId);
                _doh.AddFieldItem("ProductLink", _cart.ProductLink);
                _doh.AddFieldItem("BuyCount", _cart.BuyCount);
                _doh.AddFieldItem("State", 0);
                _doh.AddFieldItem("CartTime", DateTime.Now.ToString());
                int _newid = _doh.Insert("jcms_normal_user_cart");
                return _newid;
            }
        }
        /// <summary>
        /// 更新购物车商品信息
        /// </summary>
        /// <param name="_productid">根据产品查询</param>
        /// <param name="_buycount"></param>
        /// <param name="_state">1表示状态发生了变化</param>
        /// <returns></returns>
        public bool UpdateGoods(string _uid, string _productid, int _buycount, int _state)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                int _num = 0;
                if (_state == 0)
                {
                    _doh.Reset();
                    _doh.ConditionExpress = "ProductId=" + _productid + " and state=0 and userid=" + _uid;
                    _doh.AddFieldItem("BuyCount", _buycount);
                    _doh.AddFieldItem("CartTime", DateTime.Now.ToString());
                    _num = _doh.Update("jcms_normal_user_cart");
                }
                else if (_state == 1)
                {
                    _doh.Reset();
                    _doh.ConditionExpress = "ProductId=" + _productid + " and state=1 and userid=" + _uid;
                    _doh.AddFieldItem("State", 1);
                    _num = _doh.Update("jcms_normal_user_cart");
                }
                return (_num == 1);
            }
        }
        /// <summary>
        /// 获得某种商品的已有数量
        /// </summary>
        /// <param name="_uid"></param>
        /// <returns></returns>
        public int GetGoodsCount(string _uid, string _productid)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "state=0 and UserId=" + _uid + " and ProductId=" + _productid;
                return Str2Int(_doh.GetField("jcms_normal_user_cart", "BuyCount").ToString());
            }
        }
        /// <summary>
        /// 统计会员的购物车商品种类
        /// </summary>
        /// <param name="_uid"></param>
        /// <returns></returns>
        public int GetNewGoods(string _uid)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "state=0 and userid=" + _uid;
                return _doh.Count("jcms_normal_user_cart");
            }
        }
    }
}

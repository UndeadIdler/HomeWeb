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
using System.Web;
using System.Data;
using JumboECMS.Utils;
using JumboECMS.Common;
namespace JumboECMS.WebFile.User
{
    public partial class _ajax : JumboECMS.UI.UserCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this._operType = q("oper");
            switch (this._operType)
            {
                case "login":
                    ajaxLogin();
                    break;
                case "checkname":
                    ajaxCheckName();
                    break;
                case "checkemail":
                    ajaxCheckEmail();
                    break;
                case "ajaxRegister":
                    ajaxRegister();
                    break;
                case "ajaxResetPassword":
                    ajaxResetPassword();
                    break;
                #region 头像设置
                case "uploadfile":
                    UploadFile();
                    break;
                case "uploadcutfile":
                    UploadCutFile();
                    break;
                case "uploadmedia":
                    UploadMedia();
                    break;
                #endregion
                case "ajaxChangePassword":
                    ajaxChangePassword();
                    break;
                case "ajaxChangeProfile":
                    ajaxChangeProfile();
                    break;
                case "ajaxGetOrderList":
                    ajaxGetOrderList();
                    break;
                case "ajaxGetGoodsList":
                    ajaxGetGoodsList();
                    break;
                case "ajaxDeleteOrder":
                    ajaxDeleteOrder();
                    break;
                case "ajaxFinishOrder":
                    ajaxFinishOrder();
                    break;
                case "ajaxGetCartList":
                    ajaxGetCartList();
                    break;
                case "ajaxSetCart2Order":
                    ajaxSetCart2Order();
                    break;
                case "ajaxDeleteCart":
                    ajaxDeleteCart();
                    break;
                case "ajaxSetBuyCount":
                    ajaxSetBuyCount();
                    break;
                default:
                    DefaultResponse();
                    break;
            }
            Response.Write(this._response);
        }
        private void DefaultResponse()
        {
            User_Load("", "json");
        }
        private void ajaxLogin()
        {
            string _name = f("name");
            string _pass = JumboECMS.Utils.Strings.Left(f("pass"), 14);
            string _code = f("code");
            if (!JumboECMS.Common.ValidateCode.CheckValidateCode(_code))
            {
                this._response = "校验码错误";
                return;
            }
            int _type = JumboECMS.Utils.Validator.StrToInt(f("type"), 0);
            int iExpires = 0;
            if (_type > 0)
                iExpires = _type;//保存天数
            this._response = new JumboECMS.DAL.Normal_UserDAL().ChkUserLogin(_name, _pass, iExpires);
        }
        private void ajaxCheckName()
        {
            if (q("txtUserName") != "")
            {
                doh.Reset();
                doh.ConditionExpress = "username=@username";
                doh.AddConditionParameter("@username", q("txtUserName"));
                if (doh.Exist("jcms_normal_user"))
                    this._response = JsonResult(0, "已经存在");
                else
                    this._response = JsonResult(1, "可以注册");
            }
            else
                this._response = JsonResult(0, "为空");
        }
        private void ajaxCheckEmail()
        {
            if (q("txtEmail") != "")
            {
                doh.Reset();
                doh.ConditionExpress = "email=@email";
                doh.AddConditionParameter("@email", q("txtEmail"));
                if (doh.Exist("jcms_normal_user"))
                    this._response = JsonResult(0, "邮箱已被注册");
                else
                    this._response = JsonResult(1, "可以注册");
            }
            else
                this._response = JsonResult(0, "为空");
        }
        /// <summary>
        /// 提交注册信息
        /// </summary>
        private void ajaxRegister()
        {
            string _code = f("txtCode");
            if (!JumboECMS.Common.ValidateCode.CheckValidateCode(_code))
            {
                Response.Write("JumboECMS.Alert('校验码错误', '0');");
                Response.End();
            }
            if (f("txtUserName").Length > 0 && f("txtPass1").Length > 0 && f("txtEmail").Length > 0)
            {
                string usersign = GetRandomNumberString(64, false);
                if (new JumboECMS.DAL.Normal_UserDAL().Register(f("txtUserName"), f("txtPass1"), false, Str2Int(f("rblSex")), f("txtEmail"), f("txtBirthday"), usersign) > 0)
                {
                    if (site.CheckReg)//等待管理员审核
                    {
                        this._response = "JumboECMS.Alert('注册成功，请等待审核', '1', \"window.location='../passport/login.aspx';\");";
                    }
                    else
                    {
                        this._response = "JumboECMS.Alert('注册成功，请登录', '1', \"window.location='../passport/login.aspx';\");";
                    }
                }
                else
                {
                    this._response = "JumboECMS.Alert('注册失败，原因未知', '0');";
                }
            }
            else
                this._response = "JumboECMS.Alert('提交有误', '0');";
        }
        /// <summary>
        /// 通过用户名和邮箱重置密码
        /// </summary>
        private void ajaxResetPassword()
        {
            string RandomCode = GetRandomNumberString(8, false);
            doh.Reset();
            doh.ConditionExpress = "username=@username and email=@email";
            doh.AddConditionParameter("@username", f("txtUserName"));
            doh.AddConditionParameter("@email", f("txtEmail"));
            doh.AddFieldItem("UserPass", JumboECMS.Utils.MD5.Lower32(RandomCode));
            if (doh.Update("jcms_normal_user") > 0)
            {
                this._response = "JumboECMS.Alert('密码已经重置为" + RandomCode + "，请登陆后立即更改', '1');";
            }
            else
                this._response = "JumboECMS.Alert('信息不匹配', '0');";
        }
        #region 头像设置
        /// <summary>
        /// 上传原始图片
        /// </summary>
        private void UploadFile()
        {
            if (!(new JumboECMS.DAL.Normal_UserDAL()).ChkUserSign(q("userid"), q("usersign")))
            {
                Response.Write("验证信息有误");
                Response.End();
            }
            string _sUserUploadType = "*.jpg;*.bmp;*.gif;*.png;";
            int _sUserUploadSize = 2048;
            if (this.Page.Request.Files.Count > 0)
            {
                HttpPostedFile oFile = this.Page.Request.Files[0];//得到要上传文件

                if (oFile != null && oFile.ContentLength > 0)
                {
                    try
                    {
                        string fileExtension = System.IO.Path.GetExtension(oFile.FileName).ToLower(); //上传文件的扩展名
                        if (_sUserUploadType.ToLower().Contains("*" + fileExtension + ";"))//检测是否为允许的上传文件类型
                        {
                            if (_sUserUploadSize * 1024 >= oFile.ContentLength)//检测文件大小是否超过限制
                            {
                                string FullPath = "~/_data/tempfiles/user_" + q("userid") + "_avatar.jpg";
                                oFile.SaveAs(Server.MapPath(FullPath));
                                if (JumboECMS.Utils.FileValidation.IsSecureUpfilePhoto(Server.MapPath(FullPath)))
                                    Response.Write("ok");
                                else
                                {
                                    SaveVisitLog(2, 0);
                                    Response.Write("不安全的图片格式，换一张吧。");
                                }
                            }
                            else//文件大小超过限制
                                Response.Write("图片大小" + Convert.ToInt32(oFile.ContentLength / 1024) + "KB,超出限制。");

                        }
                        else //文件类型不允许上传
                            Response.Write("上传的不是图片。");

                    }
                    catch
                    {
                        Response.Write("程序异常，上传未成功。");
                    }
                }
                else
                    Response.Write("请选择上传文件");
            }
            else
                Response.Write("上传有误");
        }
        /// <summary>
        /// 上传切割图
        /// </summary>
        private void UploadCutFile()
        {
            if (!(new JumboECMS.DAL.Normal_UserDAL()).ChkUserSign(q("userid"), q("usersign")))
            {
                Response.Write("验证信息有误");
                Response.End();
            }
            System.Drawing.Image img = System.Drawing.Image.FromStream(Request.InputStream);
            string thumbnailPath1 = Server.MapPath("~/_data/avatar/" + q("userid") + "_l.jpg");
            string thumbnailPath2 = Server.MapPath("~/_data/avatar/" + q("userid") + "_m.jpg");
            string thumbnailPath3 = Server.MapPath("~/_data/avatar/" + q("userid") + "_s.jpg");
            img.Save(thumbnailPath1);
            JumboECMS.Utils.ImageHelp.MakeMyThumbs(thumbnailPath1, thumbnailPath2, 48, 48, 0, 0, 120, 120);
            JumboECMS.Utils.ImageHelp.MakeMyThumbs(thumbnailPath2, thumbnailPath3, 16, 16, 0, 0, 48, 48);
            img.Dispose();
            Response.Write("ok");
        }
        /// <summary>
        /// 预览图片
        /// </summary>
        private void UploadMedia()
        {
            if (!(new JumboECMS.DAL.Normal_UserDAL()).ChkUserSign(q("userid"), q("usersign")))
            {
                Response.Write("验证信息有误");
                Response.End();
            }
            Response.Expires = 0;
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            Response.AddHeader("pragma", "no-cache");
            Response.CacheControl = "no-cache";
            string _url = "~/_data/tempfiles/user_" + q("userid") + "_avatar.jpg";
            try
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(Server.MapPath(_url));
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                Response.ClearContent();
                Response.BinaryWrite(ms.ToArray());
                Response.ContentType = "image/jpeg";//指定输出格式为图形
                img.Dispose();
                Response.End();
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }
        #endregion
        /// <summary>
        /// 修改密码
        /// </summary>
        private void ajaxChangePassword()
        {
            User_Load("", "json");
            string _oldPass = f("txtOldPass");
            string _NewPass = f("txtNewPass1");
            if (_NewPass.Length > 14 || _NewPass.Length < 6)
            {
                this._response = "JumboECMS.Alert('请输入6-14位的新密码', '0');";
            }
            else
            {
                if (new JumboECMS.DAL.Normal_UserDAL().ChangeUserPassword(UserId, _oldPass, _NewPass))
                    this._response = "JumboECMS.Message('修改成功', '1', \"window.location='" + site.Dir + "user/index.aspx';\");";
                else
                    this._response = "JumboECMS.Alert('原始密码错误', '0');";
            }
        }
        /// <summary>
        /// 修改基本信息
        /// </summary>
        private void ajaxChangeProfile()
        {
            User_Load("", "json");
            doh.Reset();
            doh.ConditionExpress = "id=@id and state=1";
            doh.AddConditionParameter("@id", UserId);
            doh.AddFieldItem("Sex", f("rblSex"));
            doh.AddFieldItem("Birthday", f("txtBirthday"));
            doh.AddFieldItem("NickName", f("txtNickName"));
            doh.AddFieldItem("TrueName", f("txtTrueName"));
            doh.AddFieldItem("Signature", f("txtSignature"));
            doh.AddFieldItem("IDType", f("ddlIDType"));
            doh.AddFieldItem("IDCard", f("txtIDCard"));
            doh.AddFieldItem("ProvinceCity", f("selProvince") + "-" + f("selCity"));
            doh.AddFieldItem("WorkUnit", f("txtWorkUnit"));
            doh.AddFieldItem("Address", f("txtAddress"));
            doh.AddFieldItem("ZipCode", f("txtZipCode"));
            doh.AddFieldItem("MobileTel", f("txtMobileTel"));
            doh.AddFieldItem("Telephone", f("txtTelephone"));
            doh.AddFieldItem("QQ", f("txtQQ"));
            doh.AddFieldItem("MSN", f("txtMSN"));
            doh.AddFieldItem("HomePage", f("txtHomePage"));
            doh.Update("jcms_normal_user");
            this._response = "JumboECMS.Message('修改成功', '1', \"window.location='" + site.Dir + "user/index.aspx';\");";
        }
        #region 订单及购物车管理
        /// <summary>
        /// 订单记录
        /// </summary>
        private void ajaxGetOrderList()
        {
            User_Load("", "json");
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 10);
            int countNum = 0;
            string sqlStr = "";
            string joinStr = "A.[UserId]=B.Id";
            string whereStr1 = "1=1";//外围条件(带A.)
            string whereStr2 = "1=1";//分页条件(不带A.)
            string sdate = q("sdate");
            if (sdate != "")
            {
                if (DBType == "0")
                {
                    switch (sdate)
                    {
                        case "1w":
                            whereStr1 += " AND datediff('ww',A.OrderTime,'" + DateTime.Now.ToShortDateString() + "')=0";
                            whereStr2 += " AND datediff('ww',OrderTime,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1m":
                            whereStr1 += " AND datediff('m',A.OrderTime,'" + DateTime.Now.ToShortDateString() + "')=0";
                            whereStr2 += " AND datediff('m',OrderTime,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1y":
                            whereStr1 += " AND A.OrderTime>=#" + (DateTime.Now.Year + "-1-1") + "#";
                            whereStr2 += " AND OrderTime>=#" + (DateTime.Now.Year + "-1-1") + "#";
                            break;
                    }
                }
                else
                {
                    switch (sdate)
                    {
                        case "1w":
                            whereStr1 += " AND datediff(ww,A.OrderTime,'" + DateTime.Now.ToShortDateString() + "')=0";
                            whereStr2 += " AND datediff(ww,OrderTime,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1m":
                            whereStr1 += " AND datediff(m,A.OrderTime,'" + DateTime.Now.ToShortDateString() + "')=0";
                            whereStr2 += " AND datediff(m,OrderTime,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1y":
                            whereStr1 += " AND A.OrderTime>='" + (DateTime.Now.Year + "-1-1") + "'";
                            whereStr2 += " AND OrderTime>='" + (DateTime.Now.Year + "-1-1") + "'";
                            break;
                    }
                }
            }
            whereStr1 += " AND A.UserId=" + UserId;
            whereStr2 += " AND UserId=" + UserId;
            doh.Reset();
            doh.ConditionExpress = whereStr2;
            countNum = doh.Count("jcms_normal_user_order");
            sqlStr = JumboECMS.Utils.SqlHelp.GetSql0("A.*", "jcms_normal_user_order", "jcms_normal_user", "Id", PSize, page, "desc", joinStr, whereStr1, whereStr2);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            this._response = "{\"result\" :\"1\"," +
                "\"returnval\" :\"操作成功\"," +
                "\"pagerbar\" :\"" + JumboECMS.Utils.HtmlPager.GetPageBar(3, "js", 2, countNum, PSize, page, "javascript:ajaxList(*);") + "\"," +
                JumboECMS.Utils.dtHelp.DT2JSON(dt, (PSize * (page - 1))) +
                "}";
            dt.Clear();
            dt.Dispose();
        }
        /// <summary>
        /// 通过订单号获得商品
        /// </summary>
        private void ajaxGetGoodsList()
        {
            User_Load("", "json");
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 10);
            string _ordernum = q("ordernum");
            string mode = q("mode");
            int countNum = 0;
            string sqlStr = "";
            string whereStr = "1=1";
            if (_ordernum.Length > 0)
            {
                page = 1;
                PSize = 100;
                whereStr += " AND OrderNum='" + _ordernum + "'";
            }
            if (mode != "")
            {
                switch (mode)
                {
                    case "new":
                        whereStr += " AND State=0";
                        break;
                    case "old":
                        whereStr += " AND State=1";
                        break;
                }
            }
            whereStr += " AND UserId=" + UserId;
            doh.Reset();
            doh.ConditionExpress = whereStr;
            countNum = doh.Count("jcms_normal_user_goods");
            sqlStr = JumboECMS.Utils.SqlHelp.GetSql0("*", "jcms_normal_user_goods", "Id", PSize, page, "desc", whereStr);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            this._response = "{\"result\" :\"1\"," +
                "\"returnval\" :\"操作成功\"," +
                "\"pagerbar\" :\"" + JumboECMS.Utils.HtmlPager.GetPageBar(3, "js", 2, countNum, PSize, page, "javascript:ajaxList(*);") + "\"," +
                JumboECMS.Utils.dtHelp.DT2JSON(dt, (PSize * (page - 1))) +
                "}";
            dt.Clear();
            dt.Dispose();
        }
        /// <summary>
        /// 购物车列表
        /// </summary>
        private void ajaxGetCartList()
        {
            User_Load("", "json");
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 10);
            int countNum = 0;
            string sqlStr = "";
            string joinStr = "A.[ProductId]=B.Id";
            string whereStr1 = "A.State=0 AND A.UserId=" + UserId;
            string whereStr2 = "State=0 AND UserId=" + UserId;
            doh.Reset();
            doh.ConditionExpress = whereStr2;
            countNum = doh.Count("jcms_normal_user_cart");
            sqlStr = JumboECMS.Utils.SqlHelp.GetSql0("A.*,b.price1 as unitprice,(b.price1*a.buycount) as totalprice,b.title as productname,b.img as productimg", "jcms_normal_user_cart", "jcms_module_product", "Id", PSize, page, "desc", joinStr, whereStr1, whereStr2);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            this._response = "{\"result\" :\"1\"," +
                "\"returnval\" :\"操作成功\"," +
                "\"pagerbar\" :\"" + JumboECMS.Utils.HtmlPager.GetPageBar(3, "js", 2, countNum, PSize, page, "javascript:ajaxList(*);") + "\"," +
                JumboECMS.Utils.dtHelp.DT2JSON(dt, (PSize * (page - 1))) +
                "}";
            dt.Clear();
            dt.Dispose();
        }
        /// <summary>
        /// 作废订单
        /// </summary>
        private void ajaxDeleteOrder()
        {
            User_Load("", "json");
            string orderNum = f("ordernum");
            doh.Reset();
            if (DBType == "0")
            {
                doh.ConditionExpress = "ordernum=@ordernum and state=0 and UserId=" + UserId + " AND OrderTime<=#" + DateTime.Now.AddDays(-1).ToString() + "#";
            }
            else
            {
                doh.ConditionExpress = "ordernum=@ordernum and state=0 and UserId=" + UserId + " AND OrderTime<='" + DateTime.Now.AddDays(-1).ToString() + "'";

            }
            doh.AddConditionParameter("@ordernum", orderNum);
            if (doh.Delete("jcms_normal_user_order") == 1)
            {
                doh.Reset();
                doh.ConditionExpress = "ordernum=@ordernum and state=0 and UserId=" + UserId;
                doh.AddConditionParameter("@ordernum", orderNum);
                doh.Delete("jcms_normal_user_goods");
                this._response = JsonResult(1, "删除成功");
            }
            else
                this._response = JsonResult(0, "24小时内下的订单不能被作废");
        }
        /// <summary>
        /// 确认收货
        /// </summary>
        private void ajaxFinishOrder()
        {
            User_Load("", "json");
            string orderNum = f("ordernum");
            doh.Reset();
            doh.ConditionExpress = "ordernum=@ordernum and state=2 and UserId=" + UserId;
            doh.AddConditionParameter("@ordernum", orderNum);
            doh.AddFieldItem("State", 3);
            if (doh.Update("jcms_normal_user_order") == 1)
            {
                doh.Reset();
                doh.ConditionExpress = "ordernum=@ordernum and state=2 and UserId=" + UserId;
                doh.AddConditionParameter("@ordernum", orderNum);
                doh.AddFieldItem("State", 3);
                doh.Update("jcms_normal_user_goods");
                this._response = JsonResult(1, "设置成功");
            }
            else
                this._response = JsonResult(0, "设置有误");
        }
        /// <summary>
        /// 购物车商品转成订单
        /// </summary>
        private void ajaxSetCart2Order()
        {
            User_Load("", "json");
            if (new JumboECMS.DAL.Normal_UserOrderDAL().GetOrderTotal(UserId, 0) >= site.ProductMaxOrderCount)
            {
                this._response = "JumboECMS.Alert('您有太多的订单未付款，请稍后再下新单', '0', \"window.location='maimai_orderlist.aspx';\");";
                return;
            }
            string trueName = f("txtTrueName");
            string address = f("txtAddress");
            string zipCode = f("txtZipCode");
            string mobileTel = f("txtMobileTel");
            if (trueName.Length == 0 || address.Length == 0 || zipCode.Length == 0 || mobileTel.Length == 0)
            {
                this._response = "JumboECMS.Alert('收货信息不完整', '0');";
                return;
            }
            if (new JumboECMS.DAL.Normal_UserOrderDAL().NewOrder(UserId, trueName, address, zipCode, mobileTel))
                this._response = "JumboECMS.Message('订单提交成功，请尽快付款', '1', \"window.location='maimai_orderlist.aspx';\");";
            else
                this._response = "JumboECMS.Alert('未知的错误', '0');";
        }
        /// <summary>
        /// 从购物车里删除商品
        /// </summary>
        private void ajaxDeleteCart()
        {
            User_Load("", "json");
            string cId = f("id");
            doh.Reset();
            doh.ConditionExpress = "id=@id and state=0 and UserId=" + UserId;
            doh.AddConditionParameter("@id", cId);
            if (doh.Delete("jcms_normal_user_cart") == 1)
                this._response = JsonResult(1, "删除成功");
            else
                this._response = JsonResult(0, "删除失败");
        }
        /// <summary>
        /// 设置商品数量
        /// </summary>
        private void ajaxSetBuyCount()
        {
            User_Load("", "json");
            string productId = f("productid");
            int buyCount = Str2Int(f("buycount"), 1);
            if (buyCount > 0 && buyCount <= site.ProductMaxBuyCount)
            {
                doh.Reset();
                doh.ConditionExpress = "productid=@productid and state=0 and UserId=" + UserId;
                doh.AddConditionParameter("@productid", productId);
                doh.AddFieldItem("BuyCount", buyCount);
                if (new JumboECMS.DAL.Normal_UserCartDAL().UpdateGoods(UserId, productId, buyCount, 0))
                    this._response = JsonResult(1, "设置成功");
                else
                    this._response = JsonResult(0, "设置失败");
            }
            else
            {
                this._response = JsonResult(0, "每样商品只能购买1～" + site.ProductMaxBuyCount + "件");
            }
        }
        #endregion
    }
}

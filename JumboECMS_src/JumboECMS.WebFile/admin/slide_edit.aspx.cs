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
using System.Web.UI.WebControls;
namespace JumboECMS.WebFile.Admin
{
    public partial class _slide_edit : JumboECMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("slide-mng", "stop");
            id = Str2Str(q("id"));
            doh.Reset();
            JumboECMS.DBUtility.WebFormHandler wh = new JumboECMS.DBUtility.WebFormHandler(doh, "jcms_normal_slide", btnSave);
            wh.AddBind(txtTitle, "Title", true);
            wh.AddBind(txtUrl1, "Url1", true);
            wh.AddBind(txtImg1, "Img1", true);
            wh.AddBind(txtUrl2, "Url2", true);
            wh.AddBind(txtImg2, "Img2", true);
            wh.AddBind(txtUrl3, "Url3", true);
            wh.AddBind(txtImg3, "Img3", true);
            wh.AddBind(txtUrl4, "Url4", true);
            wh.AddBind(txtImg4, "Img4", true);
            wh.AddBind(txtUrl5, "Url5", true);
            wh.AddBind(txtImg5, "Img5", true);
            wh.AddBind(txtWidth, "Width", false);
            wh.AddBind(txtHeight, "Height", false);
            wh.AddBind(rblState, "SelectedValue", "State", false);
            if (id != "0")
            {
                wh.ConditionExpress = "id=" + id.ToString();
                wh.Mode = JumboECMS.DBUtility.OperationType.Modify;
            }
            else
                wh.Mode = JumboECMS.DBUtility.OperationType.Add;
            wh.AddOk += new EventHandler(save_ok);
            wh.ModifyOk += new EventHandler(save_ok);
            wh.validator = chkForm;
        }
        protected bool chkForm()
        {
            if (!CheckFormUrl())
                return false;
            if (!Page.IsValid)
                return false;
            return true;
        }
        protected void save_ok(object sender, EventArgs e)
        {
            FinalMessage("成功保存", site.Dir + "admin/close.htm", 0);
        }
    }
}

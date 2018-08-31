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
using System.Collections.Generic;
using System.Text;
namespace JumboECMS.UI
{
    public class FrontAjax : BasicPage
    {
        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!CheckFormUrl())//不可直接在url下访问
            {
                Response.End();
            }
        }
    }
}

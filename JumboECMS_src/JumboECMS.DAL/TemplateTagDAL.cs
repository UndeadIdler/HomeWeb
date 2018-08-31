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

using JumboECMS.TEngine;
using JumboECMS.TEngine.Parser.AST;

namespace JumboECMS.DAL
{
    /// <summary>
    /// 获得标题
    /// </summary>
    public class TemplateTag_GetFormatTitle : ITagHandler
    {
        public void TagBeginProcess(TemplateManager manager, Tag tag, ref bool processInnerElements, ref bool captureInnerContent)
        {
            processInnerElements = true;
            captureInnerContent = true;
        }

        public void TagEndProcess(TemplateManager manager, Tag tag, string innerContent)
        {
            Expression exp;
            string _title, _formattitle;
            exp = tag.AttributeValue("title");
            if (exp == null)
                throw new Exception("没有title标签");
            _title = manager.EvalExpression(exp).ToString();
            _formattitle = JumboECMS.Utils.Strings.HtmlEncode(_title);
            manager.WriteValue(_formattitle);
        }
    }
    /// <summary>
    /// 获得模块名称
    /// </summary>
    public class TemplateTag_GetModuleName : ITagHandler
    {
        public void TagBeginProcess(TemplateManager manager, Tag tag, ref bool processInnerElements, ref bool captureInnerContent)
        {
            processInnerElements = true;
            captureInnerContent = true;
        }

        public void TagEndProcess(TemplateManager manager, Tag tag, string innerContent)
        {
            Expression exp;
            string _moduletype, _modulename;
            exp = tag.AttributeValue("moduletype");
            if (exp == null)
                throw new Exception("没有moduletype标签");
            _moduletype = manager.EvalExpression(exp).ToString();
            _modulename = (new JumboECMS.DAL.Normal_ModuleDAL().GetEntity(_moduletype).Name);
            manager.WriteValue(_modulename);
        }
    }
    /// <summary>
    /// 获得栏目名称
    /// </summary>
    public class TemplateTag_GetCategoryName : ITagHandler
    {
        public void TagBeginProcess(TemplateManager manager, Tag tag, ref bool processInnerElements, ref bool captureInnerContent)
        {
            processInnerElements = true;
            captureInnerContent = true;
        }

        public void TagEndProcess(TemplateManager manager, Tag tag, string innerContent)
        {
            Expression exp;
            string _categoryid, _categoryname;
            exp = tag.AttributeValue("categoryid");
            if (exp == null)
                throw new Exception("没有categoryid标签");
            _categoryid = manager.EvalExpression(exp).ToString();
            _categoryname = (new JumboECMS.DAL.Normal_CategoryDAL().GetEntity(_categoryid,"").Title);
            manager.WriteValue(_categoryname);
        }
    }
    /// <summary>
    /// 获得栏目地址
    /// </summary>
    public class TemplateTag_GetClassLink : ITagHandler
    {
        public void TagBeginProcess(TemplateManager manager, Tag tag, ref bool processInnerElements, ref bool captureInnerContent)
        {
            processInnerElements = true;
            captureInnerContent = true;
        }

        public void TagEndProcess(TemplateManager manager, Tag tag, string innerContent)
        {
            Expression exp;
            string _moduletype, _classid, _classlink;
            exp = tag.AttributeValue("moduletype");
            if (exp == null)
                throw new Exception("没有moduletype标签");
            _moduletype = manager.EvalExpression(exp).ToString();

            exp = tag.AttributeValue("CategoryId");
            if (exp == null)
                throw new Exception("没有classid标签");
            _classid = manager.EvalExpression(exp).ToString();
            _classlink = (new Normal_CategoryDAL()).GetCategoryLink(_classid, 1);
            manager.WriteValue(_classlink);
        }
    }
    /// <summary>
    /// 获得点击率
    /// </summary>
    public class TemplateTag_GetViewnum : ITagHandler
    {
        public void TagBeginProcess(TemplateManager manager, Tag tag, ref bool processInnerElements, ref bool captureInnerContent)
        {
            processInnerElements = true;
            captureInnerContent = true;
        }

        public void TagEndProcess(TemplateManager manager, Tag tag, string innerContent)
        {
            Expression exp;
            string _sitedir, _moduletype, _contentid, _viewnum;
            exp = tag.AttributeValue("sitedir");
            if (exp == null)
                throw new Exception("没有sitedir标签");
            _sitedir = manager.EvalExpression(exp).ToString();
            exp = tag.AttributeValue("moduletype");
            if (exp == null)
                throw new Exception("没有moduletype标签");
            _moduletype = manager.EvalExpression(exp).ToString();

            exp = tag.AttributeValue("contentid");
            if (exp == null)
                throw new Exception("没有contentid标签");
            _contentid = manager.EvalExpression(exp).ToString();
            _viewnum = "<script src=\"" + _sitedir + "plus/viewcount.aspx?mType=" + _moduletype + "&id=" + _contentid + "&addit=0\"></script>";
            manager.WriteValue(_viewnum);
        }
    }
    /// <summary>
    /// 获得内容缩略图
    /// </summary>
    public class TemplateTag_GetImgurl : ITagHandler
    {
        public void TagBeginProcess(TemplateManager manager, Tag tag, ref bool processInnerElements, ref bool captureInnerContent)
        {
            processInnerElements = true;
            captureInnerContent = true;
        }

        public void TagEndProcess(TemplateManager manager, Tag tag, string innerContent)
        {
            Expression exp;
            string _sitedir, _isimg, _img, _imgurl;
            exp = tag.AttributeValue("sitedir");
            if (exp == null)
                throw new Exception("没有sitedir标签");
            _sitedir = manager.EvalExpression(exp).ToString();
            exp = tag.AttributeValue("isimg");
            if (exp == null)
                _isimg = "0";
            else
                _isimg = manager.EvalExpression(exp).ToString();
            exp = tag.AttributeValue("img");
            if (exp == null)
                _img = "";
            else
                _img = manager.EvalExpression(exp).ToString();
            if (_isimg == "0" || _img.Length == 0)
                _imgurl = _sitedir + "statics/common/nophoto.jpg";
            else
                _imgurl = _img;
            manager.WriteValue(_imgurl);
        }
    }
    /// <summary>
    /// 获得截断后的字符串
    /// </summary>
    public class TemplateTag_GetCutstring : ITagHandler
    {
        public void TagBeginProcess(TemplateManager manager, Tag tag, ref bool processInnerElements, ref bool captureInnerContent)
        {
            processInnerElements = true;
            captureInnerContent = true;
        }

        public void TagEndProcess(TemplateManager manager, Tag tag, string innerContent)
        {
            Expression exp;
            string _len, _cutstring;
            exp = tag.AttributeValue("len");
            if (exp == null)
                throw new Exception("没有len标签");
            _len = manager.EvalExpression(exp).ToString();
            _cutstring = JumboECMS.Utils.Strings.CutString(JumboECMS.Utils.Strings.NoHTML(innerContent), Convert.ToInt32(_len));
            manager.WriteValue(_cutstring);
        }
    }

}
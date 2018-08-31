using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace JumboECMS.WebControls
{
    //[ToolboxData("<{0}:FlashUpload runat=server></{0}:FlashUpload>")]
    //[PersistChildren(true), ParseChildren(true), Designer(typeof(FlashUpload))]
    public class FlashUpload : System.Web.UI.Control
    {
        private const string SWF_FileUpload = "JumboECMS.WebControls.FlashFileUpload.swf";
        private const string SWF_CheckInstall = "JumboECMS.WebControls.expressInstall.swf";
        private const string JS_SwfObject = "JumboECMS.WebControls.swfobject.js";

        [Category("Behavior")]
        [Description("The page to upload files to.")]
        [DefaultValue("")]
        public string UploadPage
        {
            get
            {
                object o = ViewState["UploadPage"];
                if (o == null)
                    return "/FlashUpload.axd";
                return o.ToString();
            }
            set { ViewState["UploadPage"] = value; }
        }

        [Category("Behavior")]
        [Description("Query Parameters to pass to the Upload Page.")]
        [DefaultValue("")]
        public string Args
        {
            get
            {
                object o = ViewState["Args"];
                if (o == null)
                    return "";
                return o.ToString();
            }
            set { ViewState["Args"] = value; }
        }

        [Category("Behavior")]
        [Description("Javascript function to call when all files are uploaded.")]
        [DefaultValue("")]
        public string OnUploadComplete
        {
            get
            {
                object o = ViewState["OnUploadComplete"];
                if (o == null)
                    return "OnCompleted";
                return o.ToString();
            }
            set { ViewState["OnUploadComplete"] = value; }
        }

        [Category("Behavior")]
        [Description("The maximum file size that can be uploaded, in bytes (0 for no limit).")]
        public decimal UploadFileSizeLimit
        {
            get
            {
                object o = ViewState["UploadFileSizeLimit"];
                if (o == null)
                    return 0;
                return (decimal)o;
            }
            set { ViewState["UploadFileSizeLimit"] = value; }
        }

        [Category("Behavior")]
        [Description("The total number of bytes that can be uploaded (0 for no limit).")]
        public decimal TotalUploadSizeLimit
        {
            get
            {
                object o = ViewState["TotalUploadSizeLimit"];
                if (o == null)
                    return 0;
                return (decimal)o;
            }
            set { ViewState["TotalUploadSizeLimit"] = value; }
        }

        [Category("Behavior")]
        [Description("The file types to restrict uploads to (ex. *.jpg; *.jpeg; *.jpe; *.gif; *.png;)")]
        [DefaultValue("")]
        public string FileTypeDescription
        {
            get
            {
                object o = ViewState["FileTypeDescription"];
                if (o == null)
                    return "";
                return o.ToString();
            }
            set { ViewState["FileTypeDescription"] = value; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Page.Form.Enctype = "multipart/form-data";
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);

            string url = Page.ClientScript.GetWebResourceUrl(this.GetType(), SWF_FileUpload);
            string SwfObjectUrl = Page.ClientScript.GetWebResourceUrl(this.GetType(), JS_SwfObject);
            string CheckInstallSWF = Page.ClientScript.GetWebResourceUrl(this.GetType(), SWF_CheckInstall);

            string obj = "<script type=\"text/javascript\" src=\"" + SwfObjectUrl + "\"></script>\n" +
                "<div id=\"SWFUploadDiv\"></div>\n" +
                "<script type=\"text/javascript\">\n" +
                "var flashvars = {};\n" +
                "flashvars.UploadPage = \"" + ResolveUrl(UploadPage) + "\";\n" +
                "flashvars.Args = \"" + Args + "\";\n" +
                "flashvars.CompletedFunction = \"" + OnUploadComplete + "\";\n" +
                "flashvars.FileExtension = \"" + FileTypeDescription + "\";\n" +
                "flashvars.TotalUploadSize = \"" + TotalUploadSizeLimit + "\";\n" +
                "flashvars.FileLimitBytes = \"" + UploadFileSizeLimit + "\";\n" +
                "var params = {};\n" +
                "params.quality = \"high\";\n" +
                "params.bgcolor = \"#ffffff\";\n" +
                "params.wmode = \"transparent\";\n" +
                "params.allowScriptAccess = \"sameDomain\";\n" +
                "params.allowfullscreen = \"true\";\n" +
                "var attributes = {};\n" +
                "attributes.id=\"fileUpload\";\n" +
                "attributes.name=\"fileUpload\";\n" +
                "swfobject.embedSWF(\"" + url + "\", \"SWFUploadDiv\", \"500\", \"28\", \"9.0.0\", \"" + CheckInstallSWF + "\", flashvars, params, attributes);\n" +
                "</script>\n";
            writer.Write(obj);
        }
    }
}

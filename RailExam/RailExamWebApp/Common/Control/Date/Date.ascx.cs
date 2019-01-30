using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace RailExamWebApp.Common.Control.Date
{
    public partial class Date : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (labelSetEmpty.Text != "Empty")
                {
                    //if (DateBox.Text == string.Empty)
                    //{
                    //    DateBox.Text = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
                    //}
                }
                ViewState["AutoPostBack"] = "false";
               // txtStatus.Text = "visible";
            }
        }

        public object DateValue
        {
            get { return DateBox.Text; }
            set { DateBox.Text = (string) value; }
        }

        public void SetDateValueEmpty()
        {
            labelSetEmpty.Text = "Empty";
        }

        //public void SetDatevisible()
        //{
        //    DisplayFlag = "hidden";
        //}

        public bool AutoPostBack
        {
            get
            {
                if (ViewState["AutoPostBack"] != null)
                {
                    return (bool)ViewState["AutoPostBack"];
                }
                else
                {
                    ViewState["AutoPostBack"] = "false";
                    return (bool)ViewState["AutoPostBack"];
                }
            }
            set
            {
                ViewState["AutoPostBack"] = (value == true) ? "true" : "false";
            }
        }

        public bool Enabled
        {
            get
            {
                return compareValidator.Enabled;
            }
            set
            {
                compareValidator.Enabled = value;
                DateBox.Enabled = value;
                if (value)
                {

                    ViewState["DateButtonStyle"] = "visibility: visible";
                }
                else
                {

                    ViewState["DateButtonStyle"] = "visibility: hidden";
                }
            }
        }
    }
}

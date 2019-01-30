using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DSunSoft.Web.UI.WebControls
{
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:Date runat=server></{0}:Date>")]
	public class Date : WebControl
	{
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue("")]
		[Localizable(true)]
		public string Text
		{
			get
			{
				String s = (String)ViewState["Text"];
				return ((s == null) ? String.Empty : s);
			}

			set
			{
				ViewState["Text"] = value;
			}
		}

		protected override void RenderContents(HtmlTextWriter output)
		{
			output.Write(Text);
		}
	}
}

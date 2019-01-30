using System;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DSunSoft.Web.Global;

namespace DSunSoft.Web.UI
{
	public class PageBase : System.Web.UI.Page
	{
		private bool m_bShowProcessInfo = false;
		private int m_nProcessInfoTimeout = 10000;
		private bool m_bKeepScrollPosition = false;

		public PageBase()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if (ShowProcessInfo)
			{
				string strScript = @"
<div id='wait' style='font-weight: bold; font-size: 14px; color: #006600; position: absolute; left: 40%; top: 48%;'>_</div>
<script language=javascript>
wait.innerText = '';
var dots = 0;
var dotmax = 10;

function ShowWait()
{
	var output;
	output = '����װ��ҳ��';
	dots ++;
	if(dots >= dotmax)
	{
		dots = 1;
	}
	for(var x = 0; x < dots; x ++)
	{
		output += '��';
	}
	wait.innerText = output;
}

function StartShowWait()
{
	wait.style.visibility = 'visible';
	window.setInterval('ShowWait()', 1000);
}

function HideWait()
{
	wait.style.visibility = 'hidden';
	window.clearInterval();
}

StartShowWait();
</script>";

				Response.Write(strScript);
				Response.Flush();
				Thread.Sleep(ProcessInfoTimeout);

				if (!ClientScript.IsStartupScriptRegistered("HideWait"))
				{
					ClientScript.RegisterStartupScript(this.GetType(), "HideWait", "<script language=javascript>HideWait();</script>");
				}
			}
		}

		//		//������
		//		protected void Page_Error(object sender, System.EventArgs e)
		//		{
		//			Exception currentError  = Server.GetLastError();
		//			ShowError(currentError);
		//			Server.ClearError();
		//		}
		//
		//		//��ʾ������Ϣ
		//		protected void ShowError(Exception currentError)
		//		{
		//			HttpContext context = HttpContext.Current;
		//			context.Response.Write( "<link rel='stylesheet' href='/RanQi/Css/Main.css'>" +
		//				"<h2>Error</h2><hr/>" +
		//				"An unexpected error has occurred on this page." +
		//				"The system administrators have been notified.<br/>" +
		//				"<br/><b>��������:</b>" +
		//				"<pre>" + context.Request.Url.ToString() + "</pre>" +
		//				"<br/><b>������Ϣ:</b>" +
		//				"<pre>" + currentError.Message.ToString() + "</pre>" +
		//				"<br/><b>�������:</b>" +
		//				"<pre>" + currentError.ToString() + "</pre>");
		//		}

		/// <summary>
		/// ��ʾҳ����ؽ�����Ϣ
		/// </summary>
		public bool ShowProcessInfo
		{
			get
			{
				return m_bShowProcessInfo;
			}
			set
			{
				m_bShowProcessInfo = value;
			}
		}

		/// <summary>
		/// ��ʾҳ����ؽ�����Ϣ
		/// </summary>
		public int ProcessInfoTimeout
		{
			get
			{
				return m_nProcessInfoTimeout;
			}
			set
			{
				m_nProcessInfoTimeout = value;
			}
		}

		/// <summary>
		/// ����ҳ���������λ��
		/// </summary>
		public bool KeepScrollPosition
		{
			get
			{
				return m_bKeepScrollPosition;
			}
			set
			{
				if (value == true)
				{
					SetKeepScrollPosition();
				}
				else
				{
					m_bKeepScrollPosition = value;
				}
			}
		}

		/// <summary>
		/// ����ҳ��ؼ�����Ч/��Ч״̬
		/// </summary>
		/// <param name="bEnabled">��Ч/��Ч</param>
		protected void EnableControl(bool bEnabled)
		{
			foreach (System.Web.UI.Control ctrl in this.Controls)
			{
				if (ctrl.ToString() == "System.Web.UI.HtmlControls.HtmlForm")
				{
					foreach (System.Web.UI.Control ctrlSub in ctrl.Controls)
					{
						if (ctrlSub.GetType() == typeof(TextBox))
						{
							((TextBox)ctrlSub).Enabled = bEnabled;
						}
						if (ctrlSub.GetType() == typeof(DropDownList))
						{
							((DropDownList)ctrlSub).Enabled = bEnabled;
						}
						if (ctrlSub.GetType() == typeof(CheckBox))
						{
							((CheckBox)ctrlSub).Enabled = bEnabled;
						}
						if (ctrlSub.GetType() == typeof(RadioButtonList))
						{
							((RadioButtonList)ctrlSub).Enabled = bEnabled;
						}
						if (ctrlSub.GetType() == typeof(RadioButton))
						{
							((RadioButton)ctrlSub).Enabled = bEnabled;
						}
					}
				}
			}
		}

		/// <summary>
		/// ����ҳ���������λ��
		/// </summary>
		protected void SetKeepScrollPosition()
		{
			StringBuilder saveScrollPosition = new StringBuilder();
			StringBuilder setScrollPosition = new StringBuilder();

			ClientScript.RegisterHiddenField("__SCROLLPOS", "0");

			saveScrollPosition.Append("<script language='javascript'>");
			saveScrollPosition.Append("function saveScrollPosition() {");

			saveScrollPosition.Append("    document.forms[0].__SCROLLPOS.value = document.body.scrollTop;");
			saveScrollPosition.Append("}");
			saveScrollPosition.Append("document.body.onscroll=saveScrollPosition;");
			saveScrollPosition.Append("</script>");

			ClientScript.RegisterStartupScript(this.GetType(), "saveScroll", saveScrollPosition.ToString());

			if (IsPostBack)
			{
				setScrollPosition.Append("<script language='javascript'>");
				setScrollPosition.Append("function setScrollPosition() {");
				setScrollPosition.Append("    document.body.scrollTop = " + Request["__SCROLLPOS"] + ";");
				setScrollPosition.Append("}");
				setScrollPosition.Append("document.body.onload=setScrollPosition;");
				setScrollPosition.Append("</script>");

				ClientScript.RegisterStartupScript(this.GetType(), "setScroll", setScrollPosition.ToString());
			}
		}

		/// <summary>
		/// ���������Ĳ���·��
		/// </summary>
		private static string UrlSuffix
		{
			get
			{
				return HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath;
			}
		}

		/// <value>
		/// ��http��ʽ������������·��
		/// </value>
		public static String UrlBase
		{
			get
			{
				return @"http://" + UrlSuffix;
			}
		}

		/// <summary>
		/// ��дRender������ʵ���Զ�����
		/// </summary>
		/// <param name="writer">��д��</param>
		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);

			if (SessionSet.PageMessage == null || SessionSet.PageMessage.Trim().Length == 0)
			{
				return;
			}

			writer.Write("<script language='javascript'>alert('" + SessionSet.PageMessage + "');</script>");
			SessionSet.PageMessage = string.Empty;
		}
	}
}

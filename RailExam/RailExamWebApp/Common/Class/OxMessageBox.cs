using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace RailExamWebApp.Common.Class
{

    public class OxMessageBox
    {
        public OxMessageBox()
        {

        }

        /// <summary>
        /// �����

        /// </summary>
        /// <param name="_Msg">�����ִ�</param>
        /// <returns>�����JS</returns>
        public void MsgBox(string _Msg)
        {
            string StrScript;
            StrScript = ("<script language=javascript>");
            StrScript += ("alert('" + _Msg + "');");
            StrScript += ("</script>");
            System.Web.HttpContext.Current.Response.Write(StrScript);
        }
        /// <summary>
        /// һ�����С�ȷ��������ȡ�����ľ����

        /// </summary>
        /// <param name="_Msg">�����ִ�</param>
        /// <param name="URL">��ȷ�����Ժ�Ҫת��Ԥ����ַ</param>
        /// <returns>�����JS</returns>
        public void MsgBox1(string _Msg, string URL)
        {
            string StrScript;
            StrScript = ("<script language=javascript>");
            StrScript += "var retValue=window.confirm('" + _Msg + "');" + "if(retValue){window.location='" + URL + "';}";
            StrScript += ("</script>");
            System.Web.HttpContext.Current.Response.Write(StrScript);

        }
        /// <summary>
        /// һ�����С�ȷ����������Ժ��ת��Ԥ����ַ�ľ����
        /// </summary>
        /// <param name="_Msg">�����ִ�</param>
        /// <param name="URL">��ȷ�����Ժ�Ҫת��Ԥ����ַ</param>
        /// <returns>�����JS</returns>
        public void MsgBox2(string _Msg, string URL)
        {
            string StrScript;
            StrScript = ("<script language=javascript>");
            StrScript += ("alert('" + _Msg + "');");
            StrScript += ("window.location='" + URL + "';");
            StrScript += ("</script>");
            System.Web.HttpContext.Current.Response.Write(StrScript);
        }

        public static void MsgBox22(string _Msg, string URL)
        {
            string StrScript;
            StrScript = ("<script language=javascript>");
            StrScript += ("alert('" + _Msg + "');");
            StrScript += ("window.location='" + URL + "';");
            StrScript += ("</script>");
            System.Web.HttpContext.Current.Response.Write(StrScript);
        }
        /// <summary>
        /// һ�����С�ȷ����������رձ�ҳ�ľ����
        /// </summary>
        /// <param name="_Msg">�����ִ�</param>
        /// <returns>�����JS</returns>
        public static void MsgBox3(string _Msg)
        {
            string StrScript;
            StrScript = ("<script language=javascript>");
            StrScript += ("alert('" + _Msg + "');");
            StrScript += ("window.close();");
            StrScript += ("</script>");
            System.Web.HttpContext.Current.Response.Write(StrScript);
        }
        /// <summary>
        /// һ�����С�ȷ���������������ǰ����ҳ�����
        /// </summary>
        /// <param name="_Msg">�����ִ�</param>
        /// <param name="BackLong">Ҫ���˼���</param>
        /// <returns>�����JS</returns>
        public static void alert_history(string _Msg, int BackLong)
        {
            string StrScript;
            StrScript = ("<script language=javascript>");
            StrScript += ("alert('" + _Msg + "');");
            StrScript += ("history.go('" + BackLong + "')");
            StrScript += ("</script>");
            System.Web.HttpContext.Current.Response.Write(StrScript);
        }

        /// <summary>
        /// һ�����С�ȷ�����������ر��Լ���ˢ�¸����ھ����
        /// </summary>
        /// <param name="_Msg">�����ִ�</param>
        /// <returns>�����JS</returns>
        public static void alert_reloadwin(string _Msg)
        {
            string StrScript;
            StrScript = ("<script language=javascript>");
            StrScript += ("alert('" + _Msg + "');");
            StrScript += ("window.opener.location.href=window.opener.location.href;window.close();");
            StrScript += ("</script>");
            System.Web.HttpContext.Current.Response.Write(StrScript);
        }

        /// <summary>
        /// �����Ի���

        /// </summary>
        /// <param name="page">ҳ��</param>
        /// <param name="content">message��Ϣ</param>
        public static void alert(System.Web.UI.Page page, string content)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), " ", "<script language='javascript'>alert('" + content + "');</script>");
        }

        /// <summary>
        /// �����Ի���

        /// </summary>
        /// <param name="page">ҳ��</param>
        /// <param name="content">message��Ϣ</param>
        public static void alert(System.Web.UI.Page page, string content, String _daiMa)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), " ", "<script language='javascript'>alert('" + content + "');" + _daiMa.Replace("\"", "'") + "</script>");
        }


        public static void alert(System.Web.UI.Page page, object _code)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "onLoad", "<script language='javascript'>" + _code + "</script>");
        }

    }
}

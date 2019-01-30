using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Timers;
using RailExam.BLL;
namespace RailExamWebApp.Common.Class
{
    public  static class AutoRefresh
    {
		private static System.Timers.Timer _timer;

        private  static  void RefreshData(object source, ElapsedEventArgs e)
        {
            RefreshSnapShotBLL  objBll = new RefreshSnapShotBLL();
            objBll.RefreshSnapshot();
        }

        public static void  Start()
        {
            if (PrjPub.IsCreateSnapShot())
            {
                RefreshData(null, null);
            }
            if(_timer == null )
            {
                OrgConfigBLL objBll = new OrgConfigBLL();
                RailExam.Model.OrgConfig obj = objBll.GetOrgConfig();
                long timer = obj.Hour * 60 * 60 * 1000;
                //long timer = obj.Hour*60*1000;
				_timer = new System.Timers.Timer(timer);

                _timer.Elapsed += new ElapsedEventHandler(RefreshData);
                _timer.AutoReset = true;
                _timer.Enabled = true;
            }
        }

        public static  void End()
        {
            if(_timer != null)
            {
                _timer.AutoReset = false;
                _timer.Enabled = false;
                _timer.Dispose();
                _timer = null;
            }
        }
    }
}

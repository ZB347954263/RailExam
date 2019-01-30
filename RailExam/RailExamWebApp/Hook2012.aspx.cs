using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Data.OracleClient;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ComponentArt.Web.UI;
using System.Drawing;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp
{
    public partial class Hook2012 : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 编码后的数据转换为字节数组
        /// </summary>
        /// <param name="sBase64Data">经过base64编码的数据</param>
        /// <returns>编码后的字节数组</returns>
        private byte[] ChangeToByteData(string sBase64Data)
        {
            string sBase64Table = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
            char[] cReturn = new char[sBase64Data.Length];
            int iState = 0;
            foreach (char c in sBase64Data)
            {
                cReturn[iState++] = (char)((sBase64Table.IndexOf(c) == -1) ? System.Text.Encoding.UTF8.GetChars(new byte[] { ((byte)0) })[0] : sBase64Table.IndexOf(c));
            }
            return System.Text.Encoding.UTF8.GetBytes(cReturn);
        }

        private string GetDate()
        {
            string strDate = DateTime.Now.Year.ToString("00") + DateTime.Now.Month.ToString("00") +
                 DateTime.Now.Day.ToString("00")
                 + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") +
                 DateTime.Now.Second.ToString("00");
            return strDate;
        }
        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="sBase64Data">编码后的base64格式的数据</param>
        /// <returns>解码后的数据</returns>
        public string Decoder(string sBase64Data)
        {
            byte[] b = ChangeToByteData(sBase64Data);
            char[] c = new char[b.Length / 4 * 3 * 8];
            int iState = 0;
            foreach (byte bb in b)
            {
                char[] cc = change(bb);
                for (int i = 0; i < 6; i++)
                {
                    c[iState++] = cc[i + 2];
                }
            }

            byte[] bL = new byte[b.Length / 4 * 3];
            int iStateOther = 0;
            for (int i = 0; i < c.Length / 8; i++)
            {
                char[] cS = new char[8] { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
                for (int j = 0; j < 8; j++)
                {
                    cS[j] = c[i * 8 + j];
                }
                bL[iStateOther++] = (byte)Change2To10(cS);
            }

            #region

            int iCount = 0;
            foreach (byte bbb in bL)
            {
                if (bbb == (byte)0)
                {
                    iCount++;
                }
            }

            byte[] BLL = new byte[bL.Length - iCount];
            for (int i = 0; i < BLL.Length; i++)
            {
                BLL[i] = bL[i];
            }

            #endregion
            return System.Text.Encoding.UTF8.GetString(BLL);
        }


        #region 十进制转二进制 <256的数据

        /// <summary>
        /// 十进制转为二进制数据（传入的数据只能为小于128的整数）
        /// </summary>
        /// <param name="i10">十进制的数据</param>
        /// <returns>转换成的二进制的数组</returns>
        private char[] change(int i10)
        {
            bool state = false;
            char[] ch = new char[] { '0', '0', '0', '0', '0', '0', '0', '0' };
            int iState = 0;
            while (i10 != 0)
            {
                iState++;
                state = false;
                if (i10 % 2 == 1)
                {
                    i10 = i10 / 2;
                    state = true;
                }
                else
                {
                    i10 = i10 / 2;
                }

                if (state)
                {
                    ch[8 - iState] = '1';
                }
                else
                {
                    ch[8 - iState] = '0';
                }
            }

            return ch;
        }

        /// <summary>
        /// 把二进制的八位char数组转为十进制数据
        /// </summary>
        /// <param name="ch">二进制数据</param>
        /// <returns>十进制数</returns>
        private int Change2To10(char[] ch)
        {
            int iReturn = 0;

            for (int i = 0; i < 8; i++)
            {
                if (ch[i] == '1')
                {
                    double d = double.Parse((7 - i).ToString());
                    iReturn += (Int32)System.Math.Pow(2.0, double.Parse((7 - i).ToString()));
                }
            }

            return iReturn;
        }

        #endregion

        protected void imageCallback_Callback(object sender, CallBackEventArgs e)
        {
            string str = e.Parameters[0];
            byte[] byteImage = Convert.FromBase64String(str);

            string filename = "D:\\" + GetDate()+ ".jpg";

            MemoryStream ms = new MemoryStream(byteImage);

            System.Drawing.Image currentImage = System.Drawing.Image.FromStream(ms);
            //System.Drawing.Image thumbnail = currentImage.GetThumbnailImage(170, 130, null, IntPtr.Zero);
            //Random r = new Random();
            //Graphics g = Graphics.FromImage(currentImage);
            //Pen p = new Pen(Color.Red);
            //g.GetNearestColor(Color.Red);
            //g.DrawString("I love you", new Font(new FontFamily("宋体"), 12), p.Brush, (float)(r.NextDouble() * 100), (float)(r.NextDouble() * 200));
            //g.Save();

            currentImage.Save(filename, ImageFormat.Jpeg);

            //OracleParameter para1 = new OracleParameter("p_photo", OracleType.Blob);
            //OracleParameter para2 = new OracleParameter("r_id", OracleType.Number);
            //OracleParameter para3 = new OracleParameter("r_exam_id", OracleType.Number);
            //para1.Value = byteImage;
            //para2.Value = Convert.ToInt32(4221);
            //para3.Value = Convert.ToInt32(16221);

            //IDataParameter[] paras = new IDataParameter[] { para1, para2, para3 };

            //Pub.RunAddProcedureBlob(false, "USP_Photo_U", paras, byteImage);
        }
    }
}

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExam
{
    public partial class ShowResultImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string resultDetailID = Request.QueryString.Get("resultDetailID");
                string typeID = Request.QueryString.Get("typeID");

                OracleAccess db = new OracleAccess();

                string strSql = "select FingerPrint,Photo1,Photo2,Photo3 "
                                + " from Random_Exam_Result_Detail "
                                + " where Random_Exam_Result_Detail_ID=" + resultDetailID;
                DataSet ds = db.RunSqlDataSet(strSql);

                if(ds.Tables[0].Rows.Count==0)
                {
                    strSql = "select FingerPrint,Photo1,Photo2,Photo3 "
                                + " from Random_Exam_Result_Detail_Temp "
                                + " where Random_Exam_Result_Detail_ID=" + resultDetailID;
                   ds = db.RunSqlDataSet(strSql);
                }

                if(typeID != "0")
                {
                    Response.Clear();
                    Response.ContentType = "image/jpeg";
                    Response.BinaryWrite((byte[])ds.Tables[0].Rows[0][Convert.ToInt32(typeID)]);
                    Response.End();
                }
                 else
                {
                    byte[] finger = (byte[]) ds.Tables[0].Rows[0][0];
                    Bitmap bit = CreateBitmap(finger, 500, 550);
                    //MemoryStream bitms = new MemoryStream();
                    //bit.Save(bitms, ImageFormat.Bmp);
                    //Bitmap bits = CreateBitmap(bitms.ToArray(), 500, 550);
                    System.Drawing.Image thumbnail = bit.GetThumbnailImage(100, 110, null, IntPtr.Zero);
                    MemoryStream ms = new MemoryStream();
                    thumbnail.Save(ms, ImageFormat.Jpeg);
                    byte[] byteImage = ms.ToArray();

                    Response.Clear();
                    Response.ContentType = "image/jpeg";
                    Response.BinaryWrite(byteImage);
                    Response.End();
                }
            }
        }

        #region ��ȡָ��ͼƬ
        // ʹ��byte[]���ݣ�����256ɫ�Ҷȡ�BMP λͼ
        private Bitmap CreateBitmap(byte[] originalImageData, int originalWidth, int originalHeight)
        {
            //ָ��8λ��ʽ����256ɫ
            Bitmap resultBitmap = new Bitmap(originalWidth, originalHeight, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

            //����λͼ�����ڴ���
            MemoryStream curImageStream = new MemoryStream();
            resultBitmap.Save(curImageStream, System.Drawing.Imaging.ImageFormat.Bmp);
            curImageStream.Flush();

            //����λͼ������ҪDWORD���루4byte��������������Ҫ��λ�ĸ���
            int curPadNum = ((originalWidth * 8 + 31) / 32 * 4) - originalWidth;

            //�������ɵ�λͼ���ݴ�С
            int bitmapDataSize = ((originalWidth * 8 + 31) / 32 * 4) * originalHeight;

            //���ݲ�������ļ���ʼƫ�ƣ�������Բο�λͼ�ļ���ʽ
            int dataOffset = ReadData(curImageStream, 10, 4);


            //�ı��ɫ�壬��ΪĬ�ϵĵ�ɫ����32λ��ɫ�ģ���Ҫ�޸�Ϊ256ɫ�ĵ�ɫ��
            int paletteStart = 54;
            int paletteEnd = dataOffset;
            int color = 0;

            for (int i = paletteStart; i < paletteEnd; i += 4)
            {
                byte[] tempColor = new byte[4];
                tempColor[0] = (byte)color;
                tempColor[1] = (byte)color;
                tempColor[2] = (byte)color;
                tempColor[3] = (byte)0;
                color++;

                curImageStream.Position = i;
                curImageStream.Write(tempColor, 0, 4);
            }

            //�������ɵ�λͼ���ݣ��Լ���С���߶�û�б䣬�����Ҫ����
            byte[] destImageData = new byte[bitmapDataSize];
            int destWidth = originalWidth + curPadNum;

            //�������յ�λͼ���ݣ�ע����ǣ�λͼ���� �����ң����µ��ϣ�������Ҫ�ߵ�
            for (int originalRowIndex = originalHeight - 1; originalRowIndex >= 0; originalRowIndex--)
            {
                int destRowIndex = originalHeight - 1 - originalRowIndex;

                for (int dataIndex = 0; dataIndex < originalWidth; dataIndex++)
                {
                    //ͬʱ��Ҫע�⣬�µ�λͼ���ݵĿ���Ѿ��仯destWidth������������λ
                    destImageData[destRowIndex * destWidth + dataIndex] = originalImageData[originalRowIndex * originalWidth + dataIndex];
                }
            }

            //������Position�Ƶ����ݶ�   
            curImageStream.Position = dataOffset;

            //����λͼ����д���ڴ���
            curImageStream.Write(destImageData, 0, bitmapDataSize);

            curImageStream.Flush();

            //���ڴ��е�λͼд��Bitmap����
            try
            {
                resultBitmap = new Bitmap(curImageStream);
                return resultBitmap;
            }
            catch
            {
                return null;
            }
        }

        // ���ڴ�����ָ��λ�ã���ȡ����
        private int ReadData(MemoryStream curStream, int startPosition, int length)
        {
            int result = -1;

            byte[] tempData = new byte[length];
            curStream.Position = startPosition;
            curStream.Read(tempData, 0, length);
            result = BitConverter.ToInt32(tempData, 0);

            return result;
        }
        #endregion

    }
}

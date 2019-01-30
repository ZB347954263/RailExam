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

        #region 读取指纹图片
        // 使用byte[]数据，生成256色灰度　BMP 位图
        private Bitmap CreateBitmap(byte[] originalImageData, int originalWidth, int originalHeight)
        {
            //指定8位格式，即256色
            Bitmap resultBitmap = new Bitmap(originalWidth, originalHeight, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

            //将该位图存入内存中
            MemoryStream curImageStream = new MemoryStream();
            resultBitmap.Save(curImageStream, System.Drawing.Imaging.ImageFormat.Bmp);
            curImageStream.Flush();

            //由于位图数据需要DWORD对齐（4byte倍数），计算需要补位的个数
            int curPadNum = ((originalWidth * 8 + 31) / 32 * 4) - originalWidth;

            //最终生成的位图数据大小
            int bitmapDataSize = ((originalWidth * 8 + 31) / 32 * 4) * originalHeight;

            //数据部分相对文件开始偏移，具体可以参考位图文件格式
            int dataOffset = ReadData(curImageStream, 10, 4);


            //改变调色板，因为默认的调色板是32位彩色的，需要修改为256色的调色板
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

            //最终生成的位图数据，以及大小，高度没有变，宽度需要调整
            byte[] destImageData = new byte[bitmapDataSize];
            int destWidth = originalWidth + curPadNum;

            //生成最终的位图数据，注意的是，位图数据 从左到右，从下到上，所以需要颠倒
            for (int originalRowIndex = originalHeight - 1; originalRowIndex >= 0; originalRowIndex--)
            {
                int destRowIndex = originalHeight - 1 - originalRowIndex;

                for (int dataIndex = 0; dataIndex < originalWidth; dataIndex++)
                {
                    //同时还要注意，新的位图数据的宽度已经变化destWidth，否则会产生错位
                    destImageData[destRowIndex * destWidth + dataIndex] = originalImageData[originalRowIndex * originalWidth + dataIndex];
                }
            }

            //将流的Position移到数据段   
            curImageStream.Position = dataOffset;

            //将新位图数据写入内存中
            curImageStream.Write(destImageData, 0, bitmapDataSize);

            curImageStream.Flush();

            //将内存中的位图写入Bitmap对象
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

        // 从内存流中指定位置，读取数据
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

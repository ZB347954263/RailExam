using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using mshtml;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;
using Word;
using System.Drawing.Imaging;
using System.Drawing;

namespace RailExamWebApp.RandomExam
{
    public partial class OutputPaperAllA3 : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["NowOrgID"] = Convert.ToInt32(ConfigurationManager.AppSettings["StationID"]);

            if (Request.QueryString.Get("Mode") == "one")
            {
                OutputWordA3();
            }
            else if (Request.QueryString.Get("Mode") == "All")
            {
                OutputWordAll();
            }
            else if (Request.QueryString.Get("Mode") == "AllOne")
            {
                OutputWordAllOne();
            }
        }

        private string GetDate()
        {
            string strDate = DateTime.Now.Year.ToString("00") + DateTime.Now.Month.ToString("00") +
                 DateTime.Now.Day.ToString("00")
                 + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") +
                 DateTime.Now.Second.ToString("00");
            return strDate;
        }

        private void DeleteFile(string file1)
        {
            try
            {
                if (File.Exists(file1))
                {
                    File.Delete(file1);
                }
            }
            catch
            {
                int i = 1;
            }
        }

        #region ����һ�������Ծ� A3
        private void OutputWordA3()
        {
            // ���� ProgressBar.htm ��ʾ����������
            string templateFileName = Path.Combine(Server.MapPath("."), "ProgressBar.htm");
            StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
            string html = reader.ReadToEnd();
            reader.Close();
            Response.Write(html);
            Response.Flush();
            System.Threading.Thread.Sleep(200);

            string strId = Request.QueryString.Get("eid");
            string orgid = Request.QueryString.Get("OrgID");
            string strName = "";
            object filename = null;

            object tableBehavior = WdDefaultTableBehavior.wdWord9TableBehavior;
            object autoFitBehavior = WdAutoFitBehavior.wdAutoFitFixed;

            object unit = WdUnits.wdStory;
            object extend = Missing.Value;
            object breakType = (int)WdBreakType.wdSectionBreakNextPage;

            object count = 1;
            object character = WdUnits.wdCharacter;

            object Nothing = Missing.Value;

            object LinkToFile = false;
            object SaveWithDocument = true;

            Application myWord = null;
            _Document myDoc = null;

            string wordName = null;
            try
            {
                RandomExamResultBLL randomExamResultBLL = new RandomExamResultBLL();
                RandomExamResult randomExamResult = new RandomExamResult();
                randomExamResult = randomExamResultBLL.GetRandomExamResultStation(int.Parse(strId));

                string jsBlock;
                myWord = new ApplicationClass();
                myDoc = new DocumentClass();
                //����.doc�ļ�����·���� 
                wordName = randomExamResult.ExamineeName + DateTime.Now.ToString("yyyyMMddhhmmss");
                filename = Server.MapPath("/RailExamBao/Excel/" + wordName + ".doc");

                if (File.Exists(filename.ToString()))
                {
                    File.Delete(filename.ToString());
                }

                //����һ��word�ļ����ļ�����ϵͳʱ�����ɾ�ȷ������ 
                myDoc = myWord.Documents.Add(ref Nothing, ref Nothing, ref Nothing, ref Nothing);
                myDoc.Activate();
                myDoc.ActiveWindow.View.Type = WdViewType.wdPrintView;
               //myDoc.ActiveWindow.View.SeekView = WdSeekView.wdSeekCurrentPageHeader;

                myDoc.PageSetup.PageWidth = (float)843.48; //29.7
                myDoc.PageSetup.PageHeight = (float)1192.8; //42;

                # region ������Ϣ
                string strOrgName = randomExamResult.OrganizationName;
                string strStationName = "";
                string strOrgName1 = "";
                int m = strOrgName.IndexOf("-");
                if (m != -1)
                {
                    strStationName = strOrgName.Substring(0, m);
                    strOrgName1 = strOrgName.Substring(m + 1);
                }
                else
                {
                    strStationName = strOrgName;
                    strOrgName1 = "";
                }

                OracleAccess db = new OracleAccess();
                string strSql;
                int RandomExamId = randomExamResult.RandomExamId;
                RandomExamBLL randomExamBLL = new RandomExamBLL();
                RailExam.Model.RandomExam randomExam = randomExamBLL.GetExam(RandomExamId);
                int year = randomExam.BeginTime.Year;
                RandomExamSubjectBLL randomExamSubjectBLL = new RandomExamSubjectBLL();
                IList<RandomExamSubject> randomExamSubjects = randomExamSubjectBLL.GetRandomExamSubjectByRandomExamId(RandomExamId);

                int nItemCount = 0;
                decimal nTotalScore = 0;
                int sumCount = 0;
                for (int i = 0; i < randomExamSubjects.Count; i++)
                {
                    RandomExamSubject subject = randomExamSubjects[i];
                    nItemCount += subject.ItemCount;
                    if (subject.ItemTypeId == PrjPub.ITEMTYPE_FILLBLANK)
                    {
                        sumCount += 6 * subject.ItemCount;
                    }
                    else
                    {
                        sumCount += subject.ItemCount;
                    }
                    nTotalScore += subject.ItemCount * subject.UnitScore;
                }

                #endregion

                //��ȡ�Ծ���ϸ��Ϣ����������������
                int randomExamResultId = int.Parse(strId);
                RandomExamItemBLL randomItemBLL = new RandomExamItemBLL();
                //RandomExamResultAnswerBLL randomExamResultAnswerBLL = new RandomExamResultAnswerBLL();
                //IList<RandomExamResultAnswer> examResultAnswers;
                //if (ViewState["NowOrgID"].ToString() != orgid)
                //{
                //    examResultAnswers = randomExamResultAnswerBLL.GetExamResultAnswersStation(int.Parse(strId));
                //}
                //else
                //{
                //    examResultAnswers = randomExamResultAnswerBLL.GetExamResultAnswers(int.Parse(strId));
                //}

                int NowCount = 0;

                //��ȡ���Խ����ϸ��Ϣ
                strSql = "select a.*,c.Short_Name||b.Computer_Room_Name ExamAddress "
                     + " from Random_Exam_Result_Detail a"
                     + " inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID "
                     + " inner join Org c on b.Org_ID=c.Org_ID "
                     + " where a.Random_Exam_Result_ID=" + randomExamResult.RandomExamResultId 
                     + " and Employee_ID="+ randomExamResult.ExamineeId
                     + " and Random_Exam_ID=" +randomExamResult.RandomExamId;
                DataTable dtExam = db.RunSqlDataSet(strSql).Tables[0];
                DataRow drExam = dtExam.NewRow();

                if (dtExam.Rows.Count == 0)
                {
                   strSql = "select a.*,c.Short_Name||b.Computer_Room_Name ExamAddress "
                     + " from Random_Exam_Result_Detail_Temp a"
                     + " inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID "
                     + " inner join Org c on b.Org_ID=c.Org_ID "
                     + " where a.Random_Exam_Result_ID=" + randomExamResult.RandomExamResultId
                     + " and Employee_ID=" + randomExamResult.ExamineeId
                     + " and Random_Exam_ID=" + randomExamResult.RandomExamId;
                   dtExam = db.RunSqlDataSet(strSql).Tables[0];
                   if (dtExam.Rows.Count > 0)
                    {
                        drExam = dtExam.Rows[0];
                    }
                }
                else
                {
                    drExam = dtExam.Rows[0];
                }

                //��ȡԱ����Ϣ
                DataRow drEmployee = null;
                if(PrjPub.IsServerCenter)
                {
                    try
                    {
                        strSql = "select a.*,b.Photo from Employee a "
                                 + " inner join Employee_Photo b on a.Employee_ID=b.Employee_ID "
                                 + " where a.Employee_ID=" + randomExamResult.ExamineeId;
                        drEmployee = db.RunSqlDataSet(strSql).Tables[0].Rows[0];
                    }
                    catch
                    {
                        strSql = "select a.*, null as Photo from Employee a "
                            + " where Employee_ID=" + randomExamResult.ExamineeId;
                        drEmployee = db.RunSqlDataSet(strSql).Tables[0].Rows[0];
                    }
                }
                else
                {
                    strSql = "";
                    try
                    {
                        OracleAccess db1 = new OracleAccess(ConfigurationManager.ConnectionStrings["OracleCenter"].ConnectionString);
                        strSql = "select a.*,b.Photo from Employee a "
                        + " inner join Employee_Photo b on a.Employee_ID=b.Employee_ID "
                        + " where a.Employee_ID=" + randomExamResult.ExamineeId;
                        drEmployee = db1.RunSqlDataSet(strSql).Tables[0].Rows[0];
                    }
                    catch
                    {
                        strSql = "select a.*, null as Photo from Employee a "
                            + " where Employee_ID=" + randomExamResult.ExamineeId;
                        drEmployee = db.RunSqlDataSet(strSql).Tables[0].Rows[0];
                    }
                }

                int photowidth = 170;
                int photoheight = 130;

                #region ͼ���ӡ
                int index = 1;
                Word.Range para = myDoc.Content.Paragraphs[index].Range;
                myDoc.Tables.Add(para, 3, 5, ref tableBehavior, ref autoFitBehavior);
                myDoc.Tables[1].Rows.Alignment = WdRowAlignment.wdAlignRowCenter;
                myDoc.Tables[1].Borders.InsideLineStyle = WdLineStyle.wdLineStyleNone;
                myDoc.Tables[1].Borders.OutsideLineStyle = WdLineStyle.wdLineStyleNone;
                para.Tables[1].Columns[1].PreferredWidth = 170;
                para.Tables[1].Columns[2].PreferredWidth = 170;
                para.Tables[1].Columns[3].PreferredWidth = 170;
                para.Tables[1].Columns[4].PreferredWidth = 170;
                para.Tables[1].Columns[5].PreferredWidth = 170;
                para.Tables[1].Rows[3].Height = 150;

                para.Tables[1].Cell(1, 1).Range.Text = "��¼ָ��";
                para.Tables[1].Cell(1, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                para.Tables[1].Cell(1, 2).Range.Text = "����ͷ��";
                para.Tables[1].Cell(1, 2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                para.Tables[1].Cell(1, 3).Range.Text = "�ɼ�ͷ��1��";
                para.Tables[1].Cell(1, 3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                para.Tables[1].Cell(1, 4).Range.Text = "�ɼ�ͷ��2��";
                para.Tables[1].Cell(1, 4).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                para.Tables[1].Cell(1, 5).Range.Text = "�ɼ�ͷ��3��";
                para.Tables[1].Cell(1, 5).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                para.Tables[1].Cell(2, 1).Range.Text = dtExam.Rows.Count == 0
                                                           ? string.Empty
                                                           : (drExam["FingerPrint_Date"] == DBNull.Value
                                                                  ? string.Empty
                                                                  : Convert.ToDateTime(drExam["FingerPrint_Date"]).
                                                                        ToString("yyyy-MM-dd HH:mm"));
                para.Tables[1].Cell(2, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                para.Tables[1].Cell(2, 2).Range.Text = "";
                para.Tables[1].Cell(2, 2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                para.Tables[1].Cell(2, 3).Range.Text =   dtExam.Rows.Count == 0
                                                           ? string.Empty
                                                           : (drExam["Photo1_Date"] == DBNull.Value
                                                           ?string.Empty
                                                           : Convert.ToDateTime(drExam["Photo1_Date"]).ToString("yyyy-MM-dd HH:mm"));
                para.Tables[1].Cell(2, 3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                para.Tables[1].Cell(2, 4).Range.Text =   dtExam.Rows.Count == 0
                                                           ? string.Empty
                                                           : (drExam["Photo2_Date"] == DBNull.Value
                                                           ? string.Empty
                                                           : Convert.ToDateTime(drExam["Photo2_Date"]).ToString("yyyy-MM-dd HH:mm"));
                para.Tables[1].Cell(2, 4).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                para.Tables[1].Cell(2, 5).Range.Text =   dtExam.Rows.Count == 0
                                                           ? string.Empty
                                                           : (drExam["Photo3_Date"] == DBNull.Value
                                                           ? string.Empty
                                                           : Convert.ToDateTime(drExam["Photo3_Date"]).ToString("yyyy-MM-dd HH:mm"));
                para.Tables[1].Cell(2, 5).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                if (dtExam.Rows.Count == 0)
                {
                    para.Tables[1].Cell(3, 1).Range.Text = "";
                    para.Tables[1].Cell(3, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    para.Tables[1].Cell(3, 2).Range.Text = "";
                    para.Tables[1].Cell(3, 2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    para.Tables[1].Cell(3, 3).Range.Text = "";
                    para.Tables[1].Cell(3, 3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    para.Tables[1].Cell(3, 4).Range.Text = "";
                    para.Tables[1].Cell(3, 4).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    para.Tables[1].Cell(3, 5).Range.Text = "";
                    para.Tables[1].Cell(3, 5).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                }
                else
                {
                    bool isExists =
    Directory.Exists(Server.MapPath("/RailExamBao/Online/Photo/" + randomExamResult.RandomExamId + "/"));

                    string path = Server.MapPath("/RailExamBao/Online/Photo/" + randomExamResult.RandomExamId + "/") + randomExamResult.ExamineeId + "_" + randomExamResult.RandomExamResultId + "_";

                    if (PrjPub.IsServerCenter && isExists)
                    {
                        if (File.Exists(path + "00.jpg"))
                        {
                            para.Tables[1].Cell(3, 1).Select();
                            InlineShape il = myWord.Application.Selection.InlineShapes.AddPicture(path + "00.jpg", ref LinkToFile, ref SaveWithDocument, ref Nothing);
                            il.Height = 110;
                            il.Width = 100;
                            para.Tables[1].Cell(3, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                        }
                        else
                        {
                            para.Tables[1].Cell(3, 1).Range.Text = "";
                            para.Tables[1].Cell(3, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                        }
                    }
                    else
                    {
                        if (drExam["FingerPrint"] != DBNull.Value)
                        {
                            byte[] finger = (byte[])drExam["FingerPrint"];

                            string file1 = Server.MapPath("/RailExamBao/Excel/image/" + GetDate() + "-1.jpg");
                            Bitmap bit = CreateBitmap(finger, 500, 550);
                            System.Drawing.Image thumbnail = bit.GetThumbnailImage(100, 110, null, IntPtr.Zero);
                            thumbnail.Save(file1, ImageFormat.Jpeg);

                            para.Tables[1].Cell(3, 1).Select();
                            InlineShape il = myWord.Application.Selection.InlineShapes.AddPicture(file1, ref LinkToFile, ref SaveWithDocument, ref Nothing);
                            il.Height = 110;
                            il.Width = 100;
                            para.Tables[1].Cell(3, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                            DeleteFile(file1);
                        }
                        else
                        {
                            para.Tables[1].Cell(3, 1).Range.Text = "";
                            para.Tables[1].Cell(3, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                        }
                    }

                    if (drEmployee["Photo"] != DBNull.Value)
                    {
                        byte[] photo = (byte[])drEmployee["Photo"];
                        string file1 = Server.MapPath("/RailExamBao/Excel/image/" + GetDate() + "-2.jpg");
                        MemoryStream ms = new MemoryStream(photo);
                        System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
                        System.Drawing.Image thumbnail = image.GetThumbnailImage(120, 150, null, IntPtr.Zero);
                        thumbnail.Save(file1, ImageFormat.Jpeg);

                        para.Tables[1].Cell(3, 2).Select();
                        InlineShape il = myWord.Application.Selection.InlineShapes.AddPicture(file1, ref LinkToFile, ref SaveWithDocument, ref Nothing);
                        il.Height = 150;
                        il.Width = 120;
                        para.Tables[1].Cell(3, 2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                        DeleteFile(file1);
                    }
                    else
                    {
                        para.Tables[1].Cell(3, 2).Range.Text = "";
                        para.Tables[1].Cell(3, 2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    }

                    if (PrjPub.IsServerCenter && isExists)
                    {
                        if (File.Exists(path + "01.jpg"))
                        {
                            para.Tables[1].Cell(3, 3).Select();
                            InlineShape il = myWord.Application.Selection.InlineShapes.AddPicture(path + "01.jpg", ref LinkToFile, ref SaveWithDocument, ref Nothing);
                            il.Height = photoheight;
                            il.Width = photowidth;
                            para.Tables[1].Cell(3, 3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                        }
                        else
                        {
                            para.Tables[1].Cell(3, 3).Range.Text = "";
                            para.Tables[1].Cell(3, 3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                        }
                    }
                    else
                    {
                        if (drExam["Photo1"] != DBNull.Value)
                        {
                            byte[] photo = (byte[])drExam["Photo1"];
                            string file1 = Server.MapPath("/RailExamBao/Excel/image/" + GetDate() + "-3.jpg");
                            MemoryStream ms = new MemoryStream(photo);
                            System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
                            System.Drawing.Image thumbnail = image.GetThumbnailImage(photowidth, photoheight, null, IntPtr.Zero);
                            thumbnail.Save(file1, ImageFormat.Jpeg);

                            para.Tables[1].Cell(3, 3).Select();
                            InlineShape il = myWord.Application.Selection.InlineShapes.AddPicture(file1, ref LinkToFile, ref SaveWithDocument, ref Nothing);
                            il.Height = photoheight;
                            il.Width = photowidth;
                            para.Tables[1].Cell(3, 3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                            DeleteFile(file1);
                        }
                        else
                        {
                            para.Tables[1].Cell(3, 3).Range.Text = "";
                            para.Tables[1].Cell(3, 3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                        }
                    }

                    if (PrjPub.IsServerCenter && isExists)
                    {
                        if (File.Exists(path + "02.jpg"))
                        {
                            para.Tables[1].Cell(3, 4).Select();
                            InlineShape il = myWord.Application.Selection.InlineShapes.AddPicture(path + "02.jpg", ref LinkToFile, ref SaveWithDocument, ref Nothing);
                            il.Height = photoheight;
                            il.Width = photowidth;
                            para.Tables[1].Cell(3, 4).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                        }
                        else
                        {
                            para.Tables[1].Cell(3, 4).Range.Text = "";
                            para.Tables[1].Cell(3, 4).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                        }
                    }
                    else
                    {
                        if (drExam["Photo2"] != DBNull.Value)
                        {
                            byte[] photo = (byte[])drExam["Photo2"];
                            string file1 = Server.MapPath("/RailExamBao/Excel/image/" + GetDate() + "-4.jpg");
                            MemoryStream ms = new MemoryStream(photo);
                            System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
                            System.Drawing.Image thumbnail = image.GetThumbnailImage(photowidth, photoheight, null, IntPtr.Zero);
                            thumbnail.Save(file1, ImageFormat.Jpeg);

                            para.Tables[1].Cell(3, 4).Select();
                            InlineShape il = myWord.Application.Selection.InlineShapes.AddPicture(file1, ref LinkToFile, ref SaveWithDocument, ref Nothing);
                            il.Height = photoheight;
                            il.Width = photowidth;
                            para.Tables[1].Cell(3, 4).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                            DeleteFile(file1);
                        }
                        else
                        {
                            para.Tables[1].Cell(3, 4).Range.Text = "";
                            para.Tables[1].Cell(3, 4).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                        }
                    }

                    if (PrjPub.IsServerCenter && isExists)
                    {
                        if (File.Exists(path + "03.jpg"))
                        {
                            para.Tables[1].Cell(3, 5).Select();
                            InlineShape il = myWord.Application.Selection.InlineShapes.AddPicture(path + "03.jpg", ref LinkToFile, ref SaveWithDocument, ref Nothing);
                            il.Height = photoheight;
                            il.Width = photowidth;
                            para.Tables[1].Cell(3, 5).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                        }
                        else
                        {
                            para.Tables[1].Cell(3, 5).Range.Text = "";
                            para.Tables[1].Cell(3, 5).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                        }
                    }
                    else
                    {
                        if (drExam["Photo3"] != DBNull.Value)
                        {
                            byte[] photo = (byte[])drExam["Photo3"];
                            string file1 = Server.MapPath("/RailExamBao/Excel/image/" + GetDate() + "-5.jpg");
                            MemoryStream ms = new MemoryStream(photo);
                            System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
                            System.Drawing.Image thumbnail = image.GetThumbnailImage(photowidth, photoheight, null, IntPtr.Zero);
                            thumbnail.Save(file1, ImageFormat.Jpeg);

                            para.Tables[1].Cell(3, 5).Select();
                            InlineShape il = myWord.Application.Selection.InlineShapes.AddPicture(file1, ref LinkToFile, ref SaveWithDocument, ref Nothing);
                            il.Height = photoheight;
                            il.Width = photowidth;
                            para.Tables[1].Cell(3, 5).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                            DeleteFile(file1);
                        }
                        else
                        {
                            para.Tables[1].Cell(3, 5).Range.Text = "";
                            para.Tables[1].Cell(3, 5).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                        }
                    }
                }

                para.Font.Size = (float)12;
                para.Font.Bold = 0;
                para.InsertParagraphAfter();
                #endregion

                NowCount = NowCount + 1;
                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('��ȡ����׼����ӡ','" + ((double)(NowCount * 100) / (double)(sumCount + 3)).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                #region ��ͷ��ӡ
                index = 19;
                para = myDoc.Content.Paragraphs[index].Range;
                para.Text = PrjPub.GetRailNameBao() + "ְ�������Ծ�";
                para.Font.Bold = 1;
                para.Font.Size = (float)22;
                para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                para.InsertParagraphAfter();

                index++;
                para = myDoc.Content.Paragraphs[index].Range;
                para.Text = "�������ƣ�" + randomExam.ExamName;
                para.Font.Size = (float)12;
                para.Font.Bold = 0;
                para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                para.InsertParagraphAfter();

                OrganizationBLL objOrgBll = new OrganizationBLL();
                index++;
                para = myDoc.Content.Paragraphs[index].Range;
                para.Text = "������λ��" + objOrgBll.GetOrganization(randomExam.OrgId).ShortName;
                para.Font.Size = (float)12;
                para.Font.Bold = 0;
                para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                para.InsertParagraphAfter();

                index++;
                para = myDoc.Content.Paragraphs[index].Range;
                para.Text = "�ܹ�" + nItemCount + "�⣬�� " + String.Format("{0:0.#}", nTotalScore) + "��";
                para.Font.Size = (float)10.5;
                para.Font.Bold = 0;
                para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                para.InsertParagraphAfter();

                index++;
                para = myDoc.Content.Paragraphs[index].Range;
                myDoc.Tables.Add(para, 3, 6, ref tableBehavior, ref autoFitBehavior);
                para.Tables[1].Columns[1].PreferredWidth = 70;
                para.Tables[1].Columns[2].PreferredWidth = 170;
                para.Tables[1].Columns[3].PreferredWidth = 70;
                para.Tables[1].Columns[4].PreferredWidth = 170;
                para.Tables[1].Columns[5].PreferredWidth = 100;
                para.Tables[1].Columns[6].PreferredWidth = 170;
                para.Tables[1].Columns[1].Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                para.Tables[1].Columns[2].Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                para.Tables[1].Columns[3].Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                para.Tables[1].Columns[4].Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                para.Tables[1].Columns[5].Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                para.Tables[1].Columns[6].Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                para.Tables[1].Cell(1, 1).Range.Text = "��λ:";
                para.Tables[1].Cell(1, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                para.Tables[1].Cell(1, 1).Range.Font.Bold = 0;

                para.Tables[1].Cell(1, 2).Range.Text = strStationName;
                para.Tables[1].Cell(1, 2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                para.Tables[1].Cell(1, 2).Range.Font.Bold = 1;

                para.Tables[1].Cell(1, 3).Range.Text = "����:";
                para.Tables[1].Cell(1, 3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                para.Tables[1].Cell(1, 3).Range.Font.Bold = 0;

                para.Tables[1].Cell(1, 4).Range.Text = strOrgName1;
                para.Tables[1].Cell(1, 4).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                para.Tables[1].Cell(1, 4).Range.Font.Bold = 1;

                para.Tables[1].Cell(1, 5).Range.Text = "ְ��:";
                para.Tables[1].Cell(1, 5).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                para.Tables[1].Cell(1, 5).Range.Font.Bold = 0;

                para.Tables[1].Cell(1, 6).Range.Text = randomExamResult.PostName;
                para.Tables[1].Cell(1, 6).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                para.Tables[1].Cell(1, 6).Range.Font.Bold = 1;

                para.Tables[1].Cell(2, 1).Range.Text = "����:";
                para.Tables[1].Cell(2, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                para.Tables[1].Cell(2, 1).Range.Font.Bold = 0;

                strName = randomExamResult.ExamineeName + "��" + strStationName + "��";
                para.Tables[1].Cell(2, 2).Range.Text = randomExamResult.ExamineeName;
                para.Tables[1].Cell(2, 2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                para.Tables[1].Cell(2, 2).Range.Font.Bold = 1;

                para.Tables[1].Cell(2, 3).Range.Text = "ʱ��:";
                para.Tables[1].Cell(2, 3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                para.Tables[1].Cell(2, 3).Range.Font.Bold = 0;

                //+ "~" + randomExamResult.EndDateTime.ToString("HH:mm");

                string strTime = randomExamResult.ExamTime/60 + "��" + randomExamResult.ExamTime%60 + "��";
                para.Tables[1].Cell(2, 4).Range.Text = randomExamResult.BeginDateTime.ToString("yyyy-MM-dd HH:mm") + " / " + strTime;
                para.Tables[1].Cell(2, 4).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                para.Tables[1].Cell(2, 4).Range.Font.Bold = 1;

                para.Tables[1].Cell(2, 5).Range.Text = "��¼֤������:";
                para.Tables[1].Cell(2, 5).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                para.Tables[1].Cell(2, 5).Range.Font.Bold = 0;

                para.Tables[1].Cell(2, 6).Range.Text = drEmployee["Identity_CardNo"].ToString();
                para.Tables[1].Cell(2, 6).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                para.Tables[1].Cell(2, 6).Range.Font.Bold = 1;

                para.Tables[1].Cell(3, 1).Range.Text = "���Եص�:";
                para.Tables[1].Cell(3, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                para.Tables[1].Cell(3, 1).Range.Font.Bold = 0;

                para.Tables[1].Cell(3, 2).Range.Text = dtExam.Rows.Count == 0?string.Empty:drExam["ExamAddress"].ToString();
                para.Tables[1].Cell(3, 2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                para.Tables[1].Cell(3, 2).Range.Font.Bold = 1;

                para.Tables[1].Cell(3, 3).Range.Text = "���Ի�λ:";
                para.Tables[1].Cell(3, 3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                para.Tables[1].Cell(3, 3).Range.Font.Bold = 0;

                para.Tables[1].Cell(3, 4).Range.Text = dtExam.Rows.Count == 0? string.Empty:drExam["Computer_Room_Seat"].ToString();
                para.Tables[1].Cell(3, 4).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                para.Tables[1].Cell(3, 4).Range.Font.Bold = 1;

                para.Tables[1].Cell(3, 5).Range.Text = "�ɼ�:";
                para.Tables[1].Cell(3, 5).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                para.Tables[1].Cell(3, 5).Range.Font.Bold = 0;

                para.Tables[1].Cell(3, 6).Range.Text = String.Format("{0:0.#}", randomExamResult.Score);
                para.Tables[1].Cell(3, 6).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                para.Tables[1].Cell(3, 6).Range.Font.Bold = 1;

                para.Font.Size = (float)12;
                para.InsertParagraphAfter();

                index = index + 21;
                para = myDoc.Content.Paragraphs[index].Range;
                para.Text = "";
                para.Font.Size = (float)10.5;
                para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                para.InsertParagraphAfter();

                index++;
                para = myDoc.Content.Paragraphs[index].Range;
                para.Text = "==================================================��=====��====��===========================================";
                para.Font.Size = (float)10.5;
                para.Font.Bold = 0;
                para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                para.InsertParagraphAfter();
                #endregion

                NowCount = NowCount + 1;
                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('��ȡ����׼����ӡ','" + ((double)(NowCount * 100) / (double)(sumCount + 3)).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                #region �����ӡ
                index++;
                para = myDoc.Content.Paragraphs[index].Range;
                para.Text = "";
                para.Font.Name = "����";
                para.Font.Size = (float)10.5;
                para.Font.Bold = 0;
                para.InsertParagraphAfter();


                index++;
                para = myDoc.Content.Paragraphs[index].Range;
                myDoc.Tables.Add(para, 2, randomExamSubjects.Count+2, ref tableBehavior, ref autoFitBehavior);
                myDoc.Tables[3].Rows.Alignment = WdRowAlignment.wdAlignRowCenter;
                for (int i = 1; i <= randomExamSubjects.Count + 2; i++)
                {
                    para.Tables[1].Columns[i].PreferredWidth =70;
                }

                //para.Tables[1].Rows[2].Height = 30;

                para.Tables[1].Cell(1, 1).Range.Text = "���";
                para.Tables[1].Cell(1, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                for(int i=1; i<= randomExamSubjects.Count;i++)
                {
                    para.Tables[1].Cell(1, i+1).Range.Text = GetNo(i-1);
                    para.Tables[1].Cell(1, i+1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                }

                para.Tables[1].Cell(1, randomExamSubjects.Count+2 ).Range.Text = "�ܷ�";
                para.Tables[1].Cell(1, randomExamSubjects.Count+2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                //para.Tables[1].Cell(1, randomExamSubjects.Count+3).Range.Text = "���Խ��";
                //para.Tables[1].Cell(1, randomExamSubjects.Count+3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                para.Tables[1].Cell(2, 1).Range.Text = "�÷�";
                para.Tables[1].Cell(2, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                para.Tables[1].Cell(2, 1).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                for (int i = 1; i <= randomExamSubjects.Count; i++)
                {
                    //��·�ֲ��ҿ��Եص㲻��ְ�̴������ѯ��ȴ𰸱�
                    if(randomExamResult.OrganizationId != 200 && PrjPub.IsServerCenter)
                    {
                        strSql = "select Sum(a.judge_Score) from Random_Exam_Result_Answer_" + randomExam.BeginTime.Year + " a "
                           + " inner join Random_Exam_Result b on a.Random_Exam_Result_ID=b.Random_Exam_Result_ID "
                           + " inner join Random_Exam_Item_" + randomExam.BeginTime.Year +
                           " c on a.Random_Exam_Item_ID=c.Random_Exam_Item_ID "
                           + " where b.Random_Exam_ID=" + randomExam.RandomExamId + " and b.Examinee_ID=" +
                           randomExamResult.ExamineeId
                           + " and c.subject_id=" + randomExamSubjects[i - 1].RandomExamSubjectId;
                    }
                    else
                    {
                        strSql = "select Sum(a.judge_Score) from Random_Exam_Result_Answer a "
                             + " inner join Random_Exam_Result b on a.Random_Exam_Result_ID=b.Random_Exam_Result_ID "
                             + " inner join Random_Exam_Item_" + randomExam.BeginTime.Year +
                             " c on a.Random_Exam_Item_ID=c.Random_Exam_Item_ID "
                             + " where b.Random_Exam_ID=" + randomExam.RandomExamId + " and b.Examinee_ID=" +
                             randomExamResult.ExamineeId
                             + " and c.subject_id=" + randomExamSubjects[i - 1].RandomExamSubjectId; 
                    }
                    
                    DataSet dsScore = db.RunSqlDataSet(strSql);
                    string sumSubject = "";
                    if(dsScore.Tables[0].Rows.Count>0)
                    {
                        sumSubject = dsScore.Tables[0].Rows[0][0] == DBNull.Value
                                         ? ""
                                         : dsScore.Tables[0].Rows[0][0].ToString();
                    }
                    para.Tables[1].Cell(2, i + 1).Range.Text = sumSubject;
                    para.Tables[1].Cell(2, i+1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                }

                para.Tables[1].Cell(2, randomExamSubjects.Count + 2).Range.Text = String.Format("{0:0.#}", randomExamResult.Score); 
                para.Tables[1].Cell(2, randomExamSubjects.Count + 2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                //para.Tables[1].Cell(2, randomExamSubjects.Count + 3).Range.Text = randomExamResult.Score>=randomExam.PassScore ? "����":"������";
                //para.Tables[1].Cell(2, randomExamSubjects.Count + 3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                para.Font.Size = (float)12;
                para.Font.Name = "����";
                para.ParagraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceExactly;
                para.ParagraphFormat.LineSpacing = 20;
                para.Font.Bold = 0;
                para.InsertParagraphAfter();
                #endregion

                //����ȡ��ÿ���Ծ��Random_Exam_Item_Year ���еļ�¼
                IList<RandomExamItem> TotalItems = new List<RandomExamItem>();
                if (ViewState["NowOrgID"].ToString() != orgid)
                {
                    TotalItems = randomItemBLL.GetItemsStation(0, randomExamResultId, year);
                }
                else
                {
                    TotalItems = randomItemBLL.GetItems(0, randomExamResultId, year);
                }

                NowCount = NowCount + 1;
                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('��ȡ����׼����ӡ','" + ((double)(NowCount * 100) / (double)(sumCount + 3)).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                int num = index + (randomExamSubjects.Count+2)*2+2;
                for (int i = 0; i < randomExamSubjects.Count; i++)
                {
                    RandomExamSubject paperSubject = randomExamSubjects[i];
                    IList<RandomExamItem> paperSubjectItems = GetSubjectItems(TotalItems, paperSubject.RandomExamSubjectId);
                    //if (ViewState["NowOrgID"].ToString() != orgid)
                    //{
                    //    paperSubjectItems = randomItemBLL.GetItemsStation(paperSubject.RandomExamSubjectId, randomExamResultId, year);
                    //}
                    //else
                    //{
                    //    paperSubjectItems = randomItemBLL.GetItems(paperSubject.RandomExamSubjectId, randomExamResultId, year);
                    //}

                    para = myDoc.Content.Paragraphs[num].Range;
                    num = num + 1;
                    para.Text = GetNo(i) + "��" + paperSubject.SubjectName + "   ����" + paperSubject.ItemCount + "�⣬��" +
                                String.Format("{0:0.#}", paperSubject.ItemCount * paperSubject.UnitScore) + "�֣�";
                    para.Font.Size = (float)10.5;
                    para.Font.Bold = 0;
                    para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    para.InsertParagraphAfter();

                    if (paperSubjectItems != null)
                    {
                        int z = 1;
                        int y = 1;
                        for (int j = 0; j < paperSubjectItems.Count; j++)
                        {
                            RandomExamItem paperItem = paperSubjectItems[j];
                            int k = j + 1;

                            if (string.IsNullOrEmpty(paperItem.SelectAnswer))
                            {
                                paperItem.SelectAnswer = string.Empty;
                            }

                            if (paperItem.TypeId == PrjPub.ITEMTYPE_FILLBLANK)
                            {
                                z = 1;
                                k = y;
                                y++;
                            }
                            else if (paperItem.TypeId == PrjPub.ITEMTYPE_FILLBLANKDETAIL)
                            {
                                k = z;
                                z++;
                            }

                            bool isPictureItem = paperItem.Content.ToLower().Contains("<img") ||
                                                 paperItem.SelectAnswer.ToLower().Contains("<img");
                            if (!isPictureItem)
                            {
                                #region ����ı�������Ŀ
                                para = myDoc.Content.Paragraphs[num].Range;
                                num = num + 1;
                                if (paperItem.TypeId == PrjPub.ITEMTYPE_FILLBLANKDETAIL)
                                {
                                    para.Text = "(" + k + "). " + "   ��" + String.Format("{0:0.#}", paperItem.Score) + "�֣�";
                                }
                                else
                                {
                                    para.Text = k + ". " + paperItem.Content.Replace("\n", "") + "   ��" + String.Format("{0:0.#}", paperSubject.UnitScore) + "�֣�";
                                }
                                para.Font.Size = 9;
                                para.Font.Bold = 0;
                                para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                                para.InsertParagraphAfter();
                                #endregion
                            }
                            else
                            {
                                #region ���ͼƬ������Ŀ
                                IHTMLDocument2 doc = new HTMLDocumentClass();
                                doc.write(new object[] { paperItem.Content });
                                doc.close();

                                string[] src = new string[doc.images.length];
                                string[] strImage = new string[doc.images.length];
                                int t = 0;
                                foreach (IHTMLImgElement image in doc.images)
                                {
                                    IHTMLElement element = (IHTMLElement)image;
                                    strImage[t] = element.outerHTML;
                                    src[t] = (string)element.getAttribute("src", 2);
                                    t = t + 1;
                                }

                                string strItem = paperItem.Content;
                                for (int x = 0; x < strImage.Length; x++)
                                {
                                    strItem = strItem.Replace(strImage[x], "@");
                                }

                                string[] strText = strItem.Split('@');

                                para = myDoc.Content.Paragraphs[num].Range;
                                num = num + 1;
                                para.Select();
                                myWord.Application.Selection.TypeText(k + ". ");
                                for (int x = 0; x < strText.Length; x++)
                                {
                                    myWord.Application.Selection.TypeText(ItemBLL.NoHTML(strText[x]));
                                    if (x < src.Length)
                                    {
                                        myWord.Application.Selection.InlineShapes.AddPicture(Server.MapPath(src[x]), ref LinkToFile, ref SaveWithDocument, ref Nothing);
                                    }
                                }
                                myWord.Application.Selection.TypeText("   ��" + String.Format("{0:0.#}", paperSubject.UnitScore) + "�֣�");
                                para.Font.Size = 9;
                                para.Font.Bold = 0;
                                para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                                para.InsertParagraphAfter();
                                #endregion
                            }

                            if (paperItem.TypeId == PrjPub.ITEMTYPE_FILLBLANK)
                            {
                                NowCount = NowCount + 1;
                                System.Threading.Thread.Sleep(10);
                                jsBlock = "<script>SetPorgressBar('�����Ծ�','" + ((double)(NowCount * 100) / (double)(sumCount + 3)).ToString("0.00") + "'); </script>";
                                Response.Write(jsBlock);
                                Response.Flush();
                                continue;
                            }

                            string[] strUserAnswers = new string[0];
                            string strUserAnswer = string.Empty;


                            // ��֯�û���
                            //RandomExamResultAnswer theExamResultAnswer = null;
                            //foreach (RandomExamResultAnswer resultAnswer in examResultAnswers)
                            //{
                            //    if (resultAnswer.RandomExamItemId == paperItem.RandomExamItemId)
                            //    {
                            //        theExamResultAnswer = resultAnswer;
                            //        break;
                            //    }
                            //}
                            //// ���ӱ��޼�¼������ҳ�����
                            //if (theExamResultAnswer == null)
                            //{
                            //    SessionSet.PageMessage = "���ݴ���";
                            //}
                            //// ������֯������
                            //if (theExamResultAnswer.Answer != null || theExamResultAnswer.Answer == string.Empty)
                            //{
                            //    strUserAnswers = theExamResultAnswer.Answer.Split('|');
                            //}

                            if (paperItem.Answer == null)
                            {
                                SessionSet.PageMessage = "���ݴ���";
                            }

                            if (!string.IsNullOrEmpty(paperItem.Answer))
                            {
                                strUserAnswers = paperItem.Answer.Split(new char[] { '|' });
                            }

                            for (int n = 0; n < strUserAnswers.Length; n++)
                            {
                                string strN = intToChar(int.Parse(strUserAnswers[n])).ToString();
                                if (n == 0)
                                {
                                    strUserAnswer += strN;
                                }
                                else
                                {
                                    strUserAnswer += "," + strN;
                                }
                            }

                            string strContent = "";
                            string strTest = "";
                            int flag = 0;
                            if (!isPictureItem)
                            {
                                #region ����ı������
                                string[] strAnswer = paperItem.SelectAnswer.Split('|');
                                for (int n = 0; n < strAnswer.Length; n++)
                                {
                                    string strN = intToChar(n).ToString();

                                    if (flag == 0)
                                    {
                                        strContent = "";
                                        strTest = "";
                                    }

                                    if (strContent == "")
                                    {
                                        strContent = strTest + strN + "." + strAnswer[n];
                                    }
                                    else
                                    {
                                        strContent = strTest + "    " + strN + "." + strAnswer[n];
                                    }

                                    strTest = strContent;

                                    if (n + 1 < strAnswer.Length)
                                    {
                                        strContent = strContent + "    " + intToChar(n + 1) + "." + strAnswer[n + 1];
                                    }

                                    if (Pub.GetStringRealLength(strContent) > 100)
                                    {
                                        para = myDoc.Content.Paragraphs[num].Range;
                                        num = num + 1;
                                        para.Text = "  " + strTest;
                                        para.Font.Size = 9;
                                        para.Font.Bold = 0;
                                        para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                                        para.InsertParagraphAfter();

                                        flag = 0;
                                    }
                                    else
                                    {
                                        if (n + 1 == strAnswer.Length && strTest != "")
                                        {
                                            para = myDoc.Content.Paragraphs[num].Range;
                                            num = num + 1;
                                            para.Text = "  " + strTest;
                                            para.Font.Size = 9;
                                            para.Font.Bold = 0;
                                            para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                                            para.InsertParagraphAfter();
                                        }
                                        flag = flag + 1;
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                #region ���ͼƬ�����
                                IHTMLDocument2 doc = new HTMLDocumentClass();
                                doc.write(new object[] { paperItem.SelectAnswer });
                                doc.close();

                                string[] src = new string[doc.images.length];
                                string[] strImage = new string[doc.images.length];
                                int t = 0;
                                foreach (IHTMLImgElement image in doc.images)
                                {
                                    IHTMLElement element = (IHTMLElement)image;
                                    strImage[t] = element.outerHTML;
                                    src[t] = (string)element.getAttribute("src", 2);
                                    t = t + 1;
                                }

                                string strItem = paperItem.SelectAnswer;
                                for (int x = 0; x < strImage.Length; x++)
                                {
                                    strItem = strItem.Replace(strImage[x], "@");
                                }

                                string[] strAnswer = strItem.Split('|');
                                for (int n = 0; n < strAnswer.Length; n++)
                                {
                                    string strN = intToChar(n).ToString();

                                    if (flag == 0)
                                    {
                                        strContent = "";
                                        strTest = "";
                                    }

                                    if (strContent == "")
                                    {
                                        strContent = strTest + strN + "." + strAnswer[n];
                                    }
                                    else
                                    {
                                        strContent = strTest + "    " + strN + "." + strAnswer[n];
                                    }

                                    strTest = strContent;

                                    if (n + 1 < strAnswer.Length)
                                    {
                                        strContent = strContent + "    " + intToChar(n + 1) + "." + strAnswer[n + 1];
                                    }

                                    if (Pub.GetStringRealLength(strContent) > 100)
                                    {
                                        string[] strText = strTest.Split('@');
                                        para = myDoc.Content.Paragraphs[num].Range;
                                        num = num + 1;
                                        para.Select();
                                        myWord.Application.Selection.TypeText("  ");
                                        for (int x = 0; x < strText.Length; x++)
                                        {
                                            myWord.Application.Selection.TypeText(ItemBLL.NoHTML(strText[x]));
                                            if (x < src.Length)
                                            {
                                                myWord.Application.Selection.InlineShapes.AddPicture(Server.MapPath(src[x]), ref LinkToFile, ref SaveWithDocument, ref Nothing);
                                            }
                                        }
                                        para.Font.Size = 9;
                                        para.Font.Bold = 0;
                                        para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                                        para.InsertParagraphAfter();

                                        flag = 0;
                                    }
                                    else
                                    {
                                        if (n + 1 == strAnswer.Length && strTest != "")
                                        {
                                            string[] strText = strTest.Split('@');
                                            para = myDoc.Content.Paragraphs[num].Range;
                                            num = num + 1;
                                            para.Select();
                                            myWord.Application.Selection.TypeText("  ");
                                            for (int x = 0; x < strText.Length; x++)
                                            {
                                                myWord.Application.Selection.TypeText(ItemBLL.NoHTML(strText[x]));
                                                if (x < src.Length)
                                                {
                                                    myWord.Application.Selection.InlineShapes.AddPicture(Server.MapPath(src[x]), ref LinkToFile, ref SaveWithDocument, ref Nothing);
                                                }
                                            }
                                            para.Font.Size = 9;
                                            para.Font.Bold = 0;
                                            para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                                            para.InsertParagraphAfter();
                                        }
                                        flag = flag + 1;
                                    }
                                }
                                #endregion
                            }

                            // ��֯��ȷ��
                            string[] strRightAnswers = paperItem.StandardAnswer.Split('|');
                            string strRightAnswer = "";
                            for (int n = 0; n < strRightAnswers.Length; n++)
                            {
                                string strN = intToChar(int.Parse(strRightAnswers[n])).ToString();
                                if (n == 0)
                                {
                                    strRightAnswer += strN;
                                }
                                else
                                {
                                    strRightAnswer += "," + strN;
                                }
                            }

                            string strScore = "0";
                            if (strRightAnswer == strUserAnswer)
                            {
                                strScore = String.Format("{0:0.#}", paperSubject.UnitScore);
                            }

                            para = myDoc.Content.Paragraphs[num].Range;
                            num = num + 1;
                            para.Text = "���׼�𰸣�" + strRightAnswer + "      �����𰸣�" + strUserAnswer + "      �÷֣�" + strScore;
                            para.Font.Size = 9;
                            para.Font.Bold = 0;
                            para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                            para.InsertParagraphAfter();

                            NowCount = NowCount + 1;

                            System.Threading.Thread.Sleep(10);
                            jsBlock = "<script>SetPorgressBar('��ȡ����׼����ӡ','" + ((double)(NowCount * 100) / (double)(sumCount + 3)).ToString("0.00") + "'); </script>";
                            Response.Write(jsBlock);
                            Response.Flush();
                        }
                    }
                }
                myWord.Application.Selection.EndKey(ref unit, ref extend);
                myWord.Application.Selection.TypeParagraph();
                myWord.Application.Selection.InsertBreak(ref breakType);
                myWord.Application.Selection.TypeBackspace();
                myWord.Application.Selection.Delete(ref character, ref count);
                myWord.Application.Selection.HomeKey(ref unit, ref extend);

                object pageNumberAlignment = WdPageNumberAlignment.wdAlignPageNumberCenter;
                object firstPage = true;
                myWord.Application.Selection.Sections[1].Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary].
                    PageNumbers.Add(ref pageNumberAlignment, ref firstPage);
                myWord.Application.Selection.Sections[1].Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary].PageNumbers.
                    NumberStyle = WdPageNumberStyle.wdPageNumberStyleNumberInDash;

                myDoc.SaveAs2000(ref filename, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing,
                                 ref Nothing, ref Nothing, ref Nothing, ref Nothing);
                myWord.Visible = true;

                // �������
                jsBlock = "<script>SetCompleted('������ɡ�'); </script>";
                Response.Write(jsBlock);
                Response.Flush();
            }
            catch (Exception ex)
            {
                myDoc.SaveAs2000(ref filename, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing,
                 ref Nothing, ref Nothing, ref Nothing, ref Nothing);
                throw ex;
            }
            finally
            {
                myDoc.Close(ref Nothing, ref Nothing, ref Nothing);
                myWord.Application.Quit(ref Nothing, ref Nothing, ref Nothing);
                if (myDoc != null)
                {
                    Marshal.ReleaseComObject(myDoc);
                    myDoc = null;
                }
                if (myWord != null)
                {
                    Marshal.ReleaseComObject(myWord);
                    myWord = null;
                }
                GC.Collect();
            }

            Response.Write("<script>top.returnValue='" + wordName + "';window.close();</script>");
        }
        #endregion

        private void OutputWordAll()
        {

        }

        #region ������Ա���Ծ������һ��word�ĵ���
        private void OutputWordAllOne()
        {
            // ���� ProgressBar.htm ��ʾ����������
            string templateFileName = Path.Combine(Server.MapPath("."), "ProgressBar.htm");
            StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
            string html = reader.ReadToEnd();
            reader.Close();
            Response.Write(html);
            Response.Flush();
            System.Threading.Thread.Sleep(200);

            string strExamID = Request.QueryString.Get("eid");
            string strType = Request.QueryString.Get("Type");

            RandomExamBLL objBll = new RandomExamBLL();
            RailExam.Model.RandomExam obj = objBll.GetExam(Convert.ToInt32(strExamID));
            IList<RailExam.Model.RandomExamResult> examResults = null;
            RandomExamResultBLL bllExamResult = new RandomExamResultBLL();

            if (strType == "Pass")
            {
                examResults = bllExamResult.GetRandomExamResults(int.Parse(strExamID), "", "","",string.Empty, obj.PassScore,
                         1000, Convert.ToInt32(Request.QueryString.Get("OrgID")));
            }
            else
            {
                examResults = bllExamResult.GetRandomExamResults(int.Parse(strExamID), "", "", "",string.Empty, 0,
                         1000, Convert.ToInt32(Request.QueryString.Get("OrgID")));
            }

            string strNowOrgID = ConfigurationManager.AppSettings["StationID"].ToString();
            object filename = null;

            object tableBehavior = Word.WdDefaultTableBehavior.wdWord9TableBehavior;
            object autoFitBehavior = Word.WdAutoFitBehavior.wdAutoFitFixed;

            object unit = Word.WdUnits.wdStory;
            object extend = System.Reflection.Missing.Value;
            object breakType = (int)Word.WdBreakType.wdSectionBreakNextPage;

            object count = 1;
            object character = Word.WdUnits.wdCharacter;

            object Nothing = System.Reflection.Missing.Value;

            object LinkToFile = false;
            object SaveWithDocument = true;


            Word.Application myWord = null;
            Word._Document myDoc = null;

            string wordName = null;
            try
            {
                string jsBlock;
                string strFloderPath = "";
                if (strType == "Pass")
                {
                    wordName = obj.ExamName + "�ϸ��Ծ�" + DateTime.Now.ToString("yyyyMMddmmss");
                    strFloderPath = Server.MapPath("/RailExamBao/Excel/" + wordName);
                }
                else
                {
                    wordName = obj.ExamName + DateTime.Now.ToString("yyyyMMddmmss");
                    strFloderPath = Server.MapPath("/RailExamBao/Excel/" + wordName);
                }


                if (!Directory.Exists(strFloderPath))
                {
                    Directory.CreateDirectory(strFloderPath);
                }

                OracleAccess db = new OracleAccess();

                //��ѯ��ǰ���ԵĴ�����
                RandomExamSubjectBLL subjectBLL = new RandomExamSubjectBLL();
                IList<RandomExamSubject> randomExamSubjects = subjectBLL.GetRandomExamSubjectByRandomExamId(Convert.ToInt32(strExamID));
                int nItemCount = 0;
                decimal nTotalScore = 0;
                int sumCount = 0;
                for (int i = 0; i < randomExamSubjects.Count; i++)
                {
                    //�㵱ǰ���Ե�������
                    nItemCount += randomExamSubjects[i].ItemCount;
                    //�㵱ǰ���Ե��ܷ���
                    nTotalScore += randomExamSubjects[i].ItemCount * randomExamSubjects[i].UnitScore;

                    if (randomExamSubjects[i].ItemTypeId == PrjPub.ITEMTYPE_FILLBLANK)
                    {
                        sumCount += 6 * randomExamSubjects[i].ItemCount;
                    }
                    else
                    {
                        sumCount += randomExamSubjects[i].ItemCount;
                    }
                }

                int SumCount = examResults.Count * (sumCount+3);
                int NowCount = 0;

                bool isExists = Directory.Exists(Server.MapPath("/RailExamBao/Online/Photo/" + strExamID + "/")); 

                for (int emp = 0; emp < examResults.Count; emp++)
                {
                    RandomExamResult examResult = examResults[emp];
                    string strId = examResult.RandomExamResultId.ToString();
                    string orgid = examResult.OrganizationId.ToString();

                    if (!PrjPub.IsServerCenter)
                    {
                        if (orgid.ToString() != ConfigurationManager.AppSettings["StationID"].ToString())
                        {
                            continue;
                        }
                    }

                    # region ������Ϣ
                    string strOrgName = examResult.OrganizationName;
                    string strStationName = "";
                    string strOrgName1 = "";
                    int m = strOrgName.IndexOf("-");
                    if (m != -1)
                    {
                        strStationName = strOrgName.Substring(0, m);
                        strOrgName1 = strOrgName.Substring(m + 1);
                    }
                    else
                    {
                        strStationName = strOrgName;
                        strOrgName1 = "";
                    }

                    if (strType == "Pass")
                    {
                        filename = Server.MapPath("/RailExamBao/Excel/" +wordName+"/" + examResults[emp].ExamineeName + "(" + strStationName + ")" + ".doc");
                    }
                    else
                    {
                        filename = Server.MapPath("/RailExamBao/Excel/" + wordName + "/" + examResults[emp].ExamineeName + "(" + strStationName + ")" + ".doc");
                    }
                    if (File.Exists(filename.ToString()))
                    {
                        File.Delete(filename.ToString());
                    }

                    myWord = new Word.ApplicationClass();
                    myDoc = new Word.DocumentClass();

                    myDoc = myWord.Documents.Add(ref Nothing, ref Nothing, ref Nothing, ref Nothing);
                    myDoc.Activate();

                    myDoc.PageSetup.PageWidth = (float)843.48; //29.7
                    myDoc.PageSetup.PageHeight = (float)1192.8; //42;
                    Word.Range para;

                    int RandomExamId = examResult.RandomExamId;
                    RandomExamBLL randomExamBLL = new RandomExamBLL();
                    RailExam.Model.RandomExam randomExam = randomExamBLL.GetExam(RandomExamId);
                    int year = randomExam.BeginTime.Year;
                    #endregion

                    //��ȡ���Խ����ϸ��Ϣ
                    string strSql = "select a.*,c.Short_Name||b.Computer_Room_Name ExamAddress "
                         + " from Random_Exam_Result_Detail a"
                         + " inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID "
                         + " inner join Org c on b.Org_ID=c.Org_ID "
                         +" where a.Random_Exam_Result_ID=" + examResults[emp].RandomExamResultId
                         + " and Employee_ID=" + examResults[emp].ExamineeId
                         + " and Random_Exam_ID=" + examResults[emp].RandomExamId;
                    DataTable dtExam = db.RunSqlDataSet(strSql).Tables[0];
                    DataRow drExam;

                    if (dtExam.Rows.Count == 0)
                    {
                        strSql = "select a.*,c.Short_Name||b.Computer_Room_Name ExamAddress "
                          + " from Random_Exam_Result_Detail_Temp a"
                          + " inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID "
                          + " inner join Org c on b.Org_ID=c.Org_ID "
                         + " where a.Random_Exam_Result_ID=" + examResults[emp].RandomExamResultId
                         + " and Employee_ID=" + examResults[emp].ExamineeId
                         + " and Random_Exam_ID=" + examResults[emp].RandomExamId;
                        drExam = db.RunSqlDataSet(strSql).Tables[0].Rows[0];
                    }
                    else
                    {
                        drExam = dtExam.Rows[0];
                    }

                    //��ȡԱ����Ϣ
                    DataRow drEmployee = null;
                    if (PrjPub.IsServerCenter)
                    {
                        try
                        {
                            strSql = "select a.*,b.Photo from Employee a "
                                     + " inner join Employee_Photo b on a.Employee_ID=b.Employee_ID "
                                     + " where a.Employee_ID=" + examResults[emp].ExamineeId;
                            drEmployee = db.RunSqlDataSet(strSql).Tables[0].Rows[0];
                        }
                        catch
                        {
                            strSql = "select a.*, null as Photo from Employee a "
                                + " where Employee_ID=" + examResults[emp].ExamineeId;
                            drEmployee = db.RunSqlDataSet(strSql).Tables[0].Rows[0];
                        }
                    }
                    else
                    {
                        strSql = "";
                        try
                        {
                            OracleAccess db1 = new OracleAccess(ConfigurationManager.ConnectionStrings["OracleCenter"].ConnectionString);
                            strSql = "select a.*,b.Photo from Employee a "
                            + " inner join Employee_Photo b on a.Employee_ID=b.Employee_ID "
                            + " where a.Employee_ID=" + examResults[emp].ExamineeId;
                            drEmployee = db1.RunSqlDataSet(strSql).Tables[0].Rows[0];
                        }
                        catch
                        {
                            strSql = "select a.*, null as Photo from Employee a "
                                + " where Employee_ID=" + examResults[emp].ExamineeId;
                            drEmployee = db.RunSqlDataSet(strSql).Tables[0].Rows[0];
                        }
                    }

                    int photowidth = 170;
                    int photoheight = 130;

                    #region ͼ���ӡ
                    int index = myDoc.Content.Paragraphs.Count;
                    para = myDoc.Content.Paragraphs[index].Range;
                    myDoc.Tables.Add(para, 3, 5, ref tableBehavior, ref autoFitBehavior);
                    myDoc.Tables[myDoc.Content.Tables.Count].Rows.Alignment = WdRowAlignment.wdAlignRowCenter;
                    myDoc.Tables[myDoc.Content.Tables.Count].Borders.InsideLineStyle = WdLineStyle.wdLineStyleNone;
                    myDoc.Tables[myDoc.Content.Tables.Count].Borders.OutsideLineStyle = WdLineStyle.wdLineStyleNone;
                    para.Tables[1].Columns[1].PreferredWidth = 170;
                    para.Tables[1].Columns[2].PreferredWidth = 170;
                    para.Tables[1].Columns[3].PreferredWidth = 170;
                    para.Tables[1].Columns[4].PreferredWidth = 170;
                    para.Tables[1].Columns[5].PreferredWidth = 170;
                    para.Tables[1].Rows[3].Height = 150;
                    para.Tables[1].Rows[3].Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalBottom;

                    para.Tables[1].Cell(1, 1).Range.Text = "��¼ָ��";
                    para.Tables[1].Cell(1, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                    para.Tables[1].Cell(1, 2).Range.Text = "����ͷ��";
                    para.Tables[1].Cell(1, 2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                    para.Tables[1].Cell(1, 3).Range.Text = "�ɼ�ͷ��1��";
                    para.Tables[1].Cell(1, 3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                    para.Tables[1].Cell(1, 4).Range.Text = "�ɼ�ͷ��2��";
                    para.Tables[1].Cell(1, 4).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                    para.Tables[1].Cell(1, 5).Range.Text = "�ɼ�ͷ��3��";
                    para.Tables[1].Cell(1, 5).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                    para.Tables[1].Cell(2, 1).Range.Text = drExam["FingerPrint_Date"] == DBNull.Value
                                                               ? string.Empty
                                                               : Convert.ToDateTime(drExam["FingerPrint_Date"]).ToString("yyyy-MM-dd HH:mm");
                    para.Tables[1].Cell(2, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                    para.Tables[1].Cell(2, 2).Range.Text = "";
                    para.Tables[1].Cell(2, 2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                    para.Tables[1].Cell(2, 3).Range.Text = drExam["Photo1_Date"] == DBNull.Value
                                                               ? string.Empty
                                                               : Convert.ToDateTime(drExam["Photo1_Date"]).ToString("yyyy-MM-dd HH:mm");
                    para.Tables[1].Cell(2, 3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                    para.Tables[1].Cell(2, 4).Range.Text = drExam["Photo2_Date"] == DBNull.Value
                                                               ? string.Empty
                                                               : Convert.ToDateTime(drExam["Photo2_Date"]).ToString("yyyy-MM-dd HH:mm");
                    para.Tables[1].Cell(2, 4).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                    para.Tables[1].Cell(2, 5).Range.Text = drExam["Photo3_Date"] == DBNull.Value
                                                               ? string.Empty
                                                               : Convert.ToDateTime(drExam["Photo3_Date"]).ToString("yyyy-MM-dd HH:mm");
                    para.Tables[1].Cell(2, 5).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                    if (dtExam.Rows.Count == 0)
                    {
                        para.Tables[1].Cell(3, 1).Range.Text = "";
                        para.Tables[1].Cell(3, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                        para.Tables[1].Cell(3, 2).Range.Text = "";
                        para.Tables[1].Cell(3, 2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                        para.Tables[1].Cell(3, 3).Range.Text = "";
                        para.Tables[1].Cell(3, 3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                        para.Tables[1].Cell(3, 4).Range.Text = "";
                        para.Tables[1].Cell(3, 4).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                        para.Tables[1].Cell(3, 5).Range.Text = "";
                        para.Tables[1].Cell(3, 5).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    }
                    else
                    {
                        string path = Server.MapPath("/RailExamBao/Online/Photo/" + examResults[emp].RandomExamId + "/") + examResults[emp].ExamineeId + "_" + examResults[emp].RandomExamResultId + "_";

                        if (PrjPub.IsServerCenter && isExists)
                        {
                            if (File.Exists(path + "00.jpg"))
                            {
                                para.Tables[1].Cell(3, 1).Select();
                                InlineShape il = myWord.Application.Selection.InlineShapes.AddPicture(path + "00.jpg", ref LinkToFile, ref SaveWithDocument, ref Nothing);
                                il.Height = 110;
                                il.Width = 100;
                                para.Tables[1].Cell(3, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                            }
                            else
                            {
                                para.Tables[1].Cell(3, 1).Range.Text = "";
                                para.Tables[1].Cell(3, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                            }
                        }
                        else
                        {
                            if (drExam["FingerPrint"] != DBNull.Value)
                            {
                                byte[] finger = (byte[])drExam["FingerPrint"];

                                string file1 = Server.MapPath("/RailExamBao/Excel/image/" + GetDate() + ".jpg");
                                Bitmap bit = CreateBitmap(finger, 500, 550);
                                System.Drawing.Image thumbnail = bit.GetThumbnailImage(100, 110, null, IntPtr.Zero);
                                thumbnail.Save(file1, ImageFormat.Jpeg);

                                para.Tables[1].Cell(3, 1).Select();
                                InlineShape il = myWord.Application.Selection.InlineShapes.AddPicture(file1, ref LinkToFile, ref SaveWithDocument, ref Nothing);
                                il.Height = 110;
                                il.Width = 100;
                                para.Tables[1].Cell(3, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                                DeleteFile(file1);
                            }
                            else
                            {
                                para.Tables[1].Cell(3, 1).Range.Text = "";
                                para.Tables[1].Cell(3, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                            }
                        }

                        if (drEmployee["Photo"] != DBNull.Value)
                        {
                            byte[] photo = (byte[])drEmployee["Photo"];
                            string file1 = Server.MapPath("/RailExamBao/Excel/image/" + GetDate() + ".jpg");
                            MemoryStream ms = new MemoryStream(photo);
                            System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
                            System.Drawing.Image thumbnail = image.GetThumbnailImage(120, 150, null, IntPtr.Zero);
                            thumbnail.Save(file1);

                            para.Tables[1].Cell(3, 2).Select();
                            InlineShape il = myWord.Application.Selection.InlineShapes.AddPicture(file1, ref LinkToFile, ref SaveWithDocument, ref Nothing);
                            il.Height = 150;
                            il.Width = 120;
                            para.Tables[1].Cell(3, 2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                            DeleteFile(file1);
                        }
                        else
                        {
                            para.Tables[1].Cell(3, 2).Range.Text = "";
                            para.Tables[1].Cell(3, 2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                        }

                        if (PrjPub.IsServerCenter && isExists)
                        {
                            if (File.Exists(path + "01.jpg"))
                            {
                                para.Tables[1].Cell(3, 3).Select();
                                InlineShape il = myWord.Application.Selection.InlineShapes.AddPicture(path + "01.jpg", ref LinkToFile, ref SaveWithDocument, ref Nothing);
                                il.Height = photoheight;
                                il.Width = photowidth;
                                para.Tables[1].Cell(3, 3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                            }
                            else
                            {
                                para.Tables[1].Cell(3, 3).Range.Text = "";
                                para.Tables[1].Cell(3, 3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                            }
                        }
                        else
                        {
                            if (drExam["Photo1"] != DBNull.Value)
                            {
                                byte[] photo = (byte[])drExam["Photo1"];
                                string file1 = Server.MapPath("/RailExamBao/Excel/image/" + GetDate() + ".jpg");
                                MemoryStream ms = new MemoryStream(photo);
                                System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
                                System.Drawing.Image thumbnail = image.GetThumbnailImage(photowidth, photoheight, null, IntPtr.Zero);
                                thumbnail.Save(file1);

                                para.Tables[1].Cell(3, 3).Select();
                                InlineShape il = myWord.Application.Selection.InlineShapes.AddPicture(file1, ref LinkToFile, ref SaveWithDocument, ref Nothing);
                                il.Height = photoheight;
                                il.Width = photowidth;
                                para.Tables[1].Cell(3, 3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                                DeleteFile(file1);
                            }
                            else
                            {
                                para.Tables[1].Cell(3, 3).Range.Text = "";
                                para.Tables[1].Cell(3, 3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                            }
                        }

                        if (PrjPub.IsServerCenter && isExists)
                        {
                            if (File.Exists(path + "02.jpg"))
                            {
                                para.Tables[1].Cell(3, 4).Select();
                                InlineShape il = myWord.Application.Selection.InlineShapes.AddPicture(path + "02.jpg", ref LinkToFile, ref SaveWithDocument, ref Nothing);
                                il.Height = photoheight;
                                il.Width = photowidth;
                                para.Tables[1].Cell(3, 4).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                            }
                            else
                            {
                                para.Tables[1].Cell(3, 4).Range.Text = "";
                                para.Tables[1].Cell(3, 4).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                            }
                        }
                        else
                        {
                            if (drExam["Photo2"] != DBNull.Value)
                            {
                                byte[] photo = (byte[])drExam["Photo2"];
                                string file1 = Server.MapPath("/RailExamBao/Excel/image/" + GetDate() + ".jpg");
                                MemoryStream ms = new MemoryStream(photo);
                                System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
                                System.Drawing.Image thumbnail = image.GetThumbnailImage(photowidth, photoheight, null, IntPtr.Zero);
                                thumbnail.Save(file1);

                                para.Tables[1].Cell(3, 4).Select();
                                InlineShape il = myWord.Application.Selection.InlineShapes.AddPicture(file1, ref LinkToFile, ref SaveWithDocument, ref Nothing);
                                il.Height = photoheight;
                                il.Width = photowidth;
                                para.Tables[1].Cell(3, 4).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                                DeleteFile(file1);
                            }
                            else
                            {
                                para.Tables[1].Cell(3, 4).Range.Text = "";
                                para.Tables[1].Cell(3, 4).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                            }
                        }

                        if (PrjPub.IsServerCenter && isExists)
                        {
                            if (File.Exists(path + "03.jpg"))
                            {
                                para.Tables[1].Cell(3, 5).Select();
                                InlineShape il = myWord.Application.Selection.InlineShapes.AddPicture(path + "03.jpg", ref LinkToFile, ref SaveWithDocument, ref Nothing);
                                il.Height = photoheight;
                                il.Width = photowidth;
                                para.Tables[1].Cell(3, 5).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                            }
                            else
                            {
                                para.Tables[1].Cell(3, 5).Range.Text = "";
                                para.Tables[1].Cell(3, 5).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                            }
                        }
                        else
                        {
                            if (drExam["Photo3"] != DBNull.Value)
                            {
                                byte[] photo = (byte[])drExam["Photo3"];
                                string file1 = Server.MapPath("/RailExamBao/Excel/image/" + GetDate() + ".jpg");
                                MemoryStream ms = new MemoryStream(photo);
                                System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
                                System.Drawing.Image thumbnail = image.GetThumbnailImage(photowidth, photoheight, null, IntPtr.Zero);
                                thumbnail.Save(file1);

                                para.Tables[1].Cell(3, 5).Select();
                                InlineShape il = myWord.Application.Selection.InlineShapes.AddPicture(file1, ref LinkToFile, ref SaveWithDocument, ref Nothing);
                                il.Height = photoheight;
                                il.Width = photowidth;
                                para.Tables[1].Cell(3, 5).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                                DeleteFile(file1);
                            }
                            else
                            {
                                para.Tables[1].Cell(3, 5).Range.Text = "";
                                para.Tables[1].Cell(3, 5).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                            }
                        }
                    }

                    para.Font.Size = (float)12;
                    para.Font.Bold = 0;
                    para.InsertParagraphAfter();
                    #endregion

                    NowCount = NowCount + 1;
                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('��ȡ����׼����ӡ','" + ((double)(NowCount * 100) / (double)SumCount).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();

                    #region ��ͷ��ӡ
                    index = myDoc.Content.Paragraphs.Count;
                    para = myDoc.Content.Paragraphs[index].Range;
                    para.Text = PrjPub.GetRailNameBao() + "ְ�������Ծ�";
                    para.Font.Bold = 1;
                    para.Font.Size = (float)22;
                    para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    para.InsertParagraphAfter();

                    index++;
                    para = myDoc.Content.Paragraphs[index].Range;
                    para.Text = "�������ƣ�" + randomExam.ExamName;
                    para.Font.Size = (float)12;
                    para.Font.Bold = 0;
                    para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    para.InsertParagraphAfter();

                    OrganizationBLL objOrgBll = new OrganizationBLL();
                    index++;
                    para = myDoc.Content.Paragraphs[index].Range;
                    para.Text = "������λ��" + objOrgBll.GetOrganization(randomExam.OrgId).ShortName;
                    para.Font.Size = (float)12;
                    para.Font.Bold = 0;
                    para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    para.InsertParagraphAfter();

                    index++;
                    para = myDoc.Content.Paragraphs[index].Range;
                    para.Text = "�ܹ�" + nItemCount + "�⣬�� " + String.Format("{0:0.#}", nTotalScore) + "��";
                    para.Font.Size = (float)10.5;
                    para.Font.Bold = 0;
                    para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                    para.InsertParagraphAfter();

                    index++;
                    para = myDoc.Content.Paragraphs[index].Range;
                    myDoc.Tables.Add(para, 3, 6, ref tableBehavior, ref autoFitBehavior);
                    para.Tables[1].Columns[1].PreferredWidth = 70;
                    para.Tables[1].Columns[2].PreferredWidth = 170;
                    para.Tables[1].Columns[3].PreferredWidth = 70;
                    para.Tables[1].Columns[4].PreferredWidth = 170;
                    para.Tables[1].Columns[5].PreferredWidth = 100;
                    para.Tables[1].Columns[6].PreferredWidth = 170;
                    para.Tables[1].Columns[1].Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    para.Tables[1].Columns[3].Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    para.Tables[1].Columns[5].Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter; 

                    para.Tables[1].Cell(1, 1).Range.Text = "��λ:";
                    para.Tables[1].Cell(1, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                    para.Tables[1].Cell(1, 1).Range.Font.Bold = 0;

                    para.Tables[1].Cell(1, 2).Range.Text = strStationName;
                    para.Tables[1].Cell(1, 2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    para.Tables[1].Cell(1, 2).Range.Font.Bold = 1;

                    para.Tables[1].Cell(1, 3).Range.Text = "����:";
                    para.Tables[1].Cell(1, 3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                    para.Tables[1].Cell(1, 3).Range.Font.Bold = 0;

                    para.Tables[1].Cell(1, 4).Range.Text = strOrgName1;
                    para.Tables[1].Cell(1, 4).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    para.Tables[1].Cell(1, 4).Range.Font.Bold = 1;

                    para.Tables[1].Cell(1, 5).Range.Text = "ְ��:";
                    para.Tables[1].Cell(1, 5).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                    para.Tables[1].Cell(1, 5).Range.Font.Bold = 0;

                    para.Tables[1].Cell(1, 6).Range.Text = examResults[emp].PostName;
                    para.Tables[1].Cell(1, 6).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    para.Tables[1].Cell(1, 6).Range.Font.Bold = 1;

                    para.Tables[1].Cell(2, 1).Range.Text = "����:";
                    para.Tables[1].Cell(2, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                    para.Tables[1].Cell(2, 1).Range.Font.Bold = 0;

                    para.Tables[1].Cell(2, 2).Range.Text = examResults[emp].ExamineeName;
                    para.Tables[1].Cell(2, 2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    para.Tables[1].Cell(2, 2).Range.Font.Bold = 1;

                    para.Tables[1].Cell(2, 3).Range.Text = "ʱ��:";
                    para.Tables[1].Cell(2, 3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                    para.Tables[1].Cell(2, 3).Range.Font.Bold = 0;

                    //+"~" + examResults[emp].EndDateTime.ToString("HH:mm");
                    string strTime = examResults[emp].ExamTime / 60 + "��" + examResults[emp].ExamTime % 60 + "��";
                    para.Tables[1].Cell(2, 4).Range.Text = examResults[emp].BeginDateTime.ToString("yyyy-MM-dd HH:mm") + " / " + strTime; 
                    para.Tables[1].Cell(2, 4).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    para.Tables[1].Cell(2, 4).Range.Font.Bold = 1;

                    para.Tables[1].Cell(2, 5).Range.Text = "��¼֤������:";
                    para.Tables[1].Cell(2, 5).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                    para.Tables[1].Cell(2, 5).Range.Font.Bold = 0;

                    para.Tables[1].Cell(2, 6).Range.Text = drEmployee["Identity_CardNo"].ToString();
                    para.Tables[1].Cell(2, 6).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    para.Tables[1].Cell(2, 6).Range.Font.Bold = 1;

                    para.Tables[1].Cell(3, 1).Range.Text = "���Եص�:";
                    para.Tables[1].Cell(3, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                    para.Tables[1].Cell(3, 1).Range.Font.Bold = 0;

                    para.Tables[1].Cell(3, 2).Range.Text = drExam["ExamAddress"].ToString();
                    para.Tables[1].Cell(3, 2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    para.Tables[1].Cell(3, 2).Range.Font.Bold = 1;

                    para.Tables[1].Cell(3, 3).Range.Text = "���Ի�λ:";
                    para.Tables[1].Cell(3, 3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                    para.Tables[1].Cell(3, 3).Range.Font.Bold = 0;

                    para.Tables[1].Cell(3, 4).Range.Text = drExam["Computer_Room_Seat"].ToString();
                    para.Tables[1].Cell(3, 4).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    para.Tables[1].Cell(3, 4).Range.Font.Bold = 1;

                    para.Tables[1].Cell(3, 5).Range.Text = "�ɼ�:";
                    para.Tables[1].Cell(3, 5).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                    para.Tables[1].Cell(3, 5).Range.Font.Bold = 0;

                    para.Tables[1].Cell(3, 6).Range.Text = String.Format("{0:0.#}", examResults[emp].Score);
                    para.Tables[1].Cell(3, 6).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    para.Tables[1].Cell(3, 6).Range.Font.Bold = 1;

                    para.Font.Size = (float)12;
                    para.InsertParagraphAfter();

                    index = index + 21;
                    para = myDoc.Content.Paragraphs[index].Range;
                    para.Text = "";
                    para.Font.Size = (float)10.5;
                    para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    para.InsertParagraphAfter();

                    index++;
                    para = myDoc.Content.Paragraphs[index].Range;
                    para.Text = "==================================================��=====��====��===========================================";
                    para.Font.Size = (float)10.5;
                    para.Font.Bold = 0;
                    para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    para.InsertParagraphAfter();
                    #endregion

                    NowCount = NowCount + 1;
                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('��ȡ����׼����ӡ','" + ((double)(NowCount * 100) / (double)SumCount).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();

                    #region �����ӡ
                    index++;
                    para = myDoc.Content.Paragraphs[index].Range;
                    para.Text = "";
                    para.Font.Name = "����";
                    para.Font.Size = (float)10.5;
                    para.Font.Bold = 0;
                    para.InsertParagraphAfter();


                    index++;
                    para = myDoc.Content.Paragraphs[index].Range;
                    myDoc.Tables.Add(para, 2, randomExamSubjects.Count + 2, ref tableBehavior, ref autoFitBehavior);
                    myDoc.Tables[myDoc.Content.Tables.Count].Rows.Alignment = WdRowAlignment.wdAlignRowCenter;
                    for (int i = 1; i <= randomExamSubjects.Count + 2; i++)
                    {
                        para.Tables[1].Columns[i].PreferredWidth = 70;
                    }

                    //para.Tables[1].Rows[2].Height = 30;

                    para.Tables[1].Cell(1, 1).Range.Text = "���";
                    para.Tables[1].Cell(1, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                    for (int i = 1; i <= randomExamSubjects.Count; i++)
                    {
                        para.Tables[1].Cell(1, i + 1).Range.Text = GetNo(i - 1);
                        para.Tables[1].Cell(1, i + 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    }

                    para.Tables[1].Cell(1, randomExamSubjects.Count + 2).Range.Text = "�ܷ�";
                    para.Tables[1].Cell(1, randomExamSubjects.Count + 2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                    //para.Tables[1].Cell(1, randomExamSubjects.Count + 3).Range.Text = "���Խ��";
                    //para.Tables[1].Cell(1, randomExamSubjects.Count + 3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                    para.Tables[1].Cell(2, 1).Range.Text = "�÷�";
                    para.Tables[1].Cell(2, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    para.Tables[1].Cell(2, 1).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    for (int i = 1; i <= randomExamSubjects.Count; i++)
                    {
                        para.Tables[1].Cell(2, i + 1).Range.Text = "";
                        para.Tables[1].Cell(2, i + 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    }

                    para.Tables[1].Cell(2, randomExamSubjects.Count + 2).Range.Text = String.Format("{0:0.#}", examResults[emp].Score);
                    para.Tables[1].Cell(2, randomExamSubjects.Count + 2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                    //para.Tables[1].Cell(2, randomExamSubjects.Count + 3).Range.Text = examResults[emp].Score >= randomExam.PassScore ? "����" : "������";
                    //para.Tables[1].Cell(2, randomExamSubjects.Count + 3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                    para.Font.Size = (float)12;
                    para.Font.Name = "����";
                    para.ParagraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceExactly;
                    para.ParagraphFormat.LineSpacing = 20;
                    para.Font.Bold = 0;
                    para.InsertParagraphAfter();
                    #endregion

                    NowCount = NowCount + 1;
                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('��ȡ����׼����ӡ','" + ((double)(NowCount * 100) / (double)SumCount).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();

                    int randomExamResultId = int.Parse(strId);
                    RandomExamItemBLL randomItemBLL = new RandomExamItemBLL();

                    //RandomExamResultAnswerBLL randomExamResultAnswerBLL = new RandomExamResultAnswerBLL();
                    //IList<RandomExamResultAnswer> examResultAnswers = new List<RandomExamResultAnswer>();
                    //if (strNowOrgID != orgid)
                    //{
                    //    examResultAnswers = randomExamResultAnswerBLL.GetExamResultAnswersStation(int.Parse(strId));
                    //}
                    //else
                    //{
                    //    examResultAnswers = randomExamResultAnswerBLL.GetExamResultAnswers(int.Parse(strId));
                    //}
                    //nItemCount = examResultAnswers.Count;

                    IList<RandomExamItem> TotalItems = new List<RandomExamItem>();
                    if (ViewState["NowOrgID"].ToString() != orgid)
                    {
                        TotalItems = randomItemBLL.GetItemsStation(0, randomExamResultId, year);
                    }
                    else
                    {
                        TotalItems = randomItemBLL.GetItems(0, randomExamResultId, year);
                    }

                    int num = myDoc.Content.Paragraphs.Count;
                    for (int i = 0; i < randomExamSubjects.Count; i++)
                    {
                        RandomExamSubject paperSubject = randomExamSubjects[i];
                        IList<RandomExamItem> PaperItems = GetSubjectItems(TotalItems, paperSubject.RandomExamSubjectId);

                        //if (strNowOrgID != orgid)
                        //{
                        //    PaperItems =
                        //        randomItemBLL.GetItemsStation(paperSubject.RandomExamSubjectId, randomExamResultId, year);
                        //}
                        //else
                        //{
                        //    PaperItems = randomItemBLL.GetItems(paperSubject.RandomExamSubjectId, randomExamResultId, year);
                        //}

                        para = myDoc.Content.Paragraphs[num].Range;
                        num = num + 1;
                        para.Text = GetNo(i) + "��" + paperSubject.SubjectName + "   ����" + paperSubject.ItemCount + "�⣬��" +
                                    System.String.Format("{0:0.##}", paperSubject.ItemCount * paperSubject.UnitScore) + "�֣�";
                        para.Font.Size = (float)10.5;
                        para.Font.Bold = 0;
                        para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                        para.InsertParagraphAfter();

                        if (PaperItems != null)
                        {
                            int z = 1;
                            int y = 1;
                            for (int j = 0; j < PaperItems.Count; j++)
                            {
                                RandomExamItem paperItem = PaperItems[j];
                                int k = j + 1;

                                if (string.IsNullOrEmpty(paperItem.SelectAnswer))
                                {
                                    paperItem.SelectAnswer = string.Empty;
                                }

                                if (paperItem.TypeId == PrjPub.ITEMTYPE_FILLBLANK)
                                {
                                    z = 1;
                                    k = y;
                                    y++;
                                }
                                else if (paperItem.TypeId == PrjPub.ITEMTYPE_FILLBLANKDETAIL)
                                {
                                    k = z;
                                    z++;
                                }

                                bool isPictureItem = paperItem.Content.ToLower().Contains("<img") ||
                                                     paperItem.SelectAnswer.ToLower().Contains("<img");
                                if (!isPictureItem)
                                {
                                    #region ����ı�������Ŀ
                                    para = myDoc.Content.Paragraphs[num].Range;
                                    num = num + 1;
                                    if (paperItem.TypeId == PrjPub.ITEMTYPE_FILLBLANKDETAIL)
                                    {
                                        para.Text = "(" + k + "). " + "   ��" + String.Format("{0:0.#}", paperItem.Score) + "�֣�";

                                    }
                                    else
                                    {
                                        para.Text = k + ". " + paperItem.Content.Replace("\n", "") + "   ��" + String.Format("{0:0.#}", paperSubject.UnitScore) + "�֣�";
                                    }
                                    para.Font.Size = 9;
                                    para.Font.Bold = 0;
                                    para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                                    para.InsertParagraphAfter();
                                    #endregion
                                }
                                else
                                {
                                    #region ���ͼƬ������Ŀ
                                    IHTMLDocument2 doc = new HTMLDocumentClass();
                                    doc.write(new object[] { paperItem.Content });
                                    doc.close();

                                    string[] src = new string[doc.images.length];
                                    string[] strImage = new string[doc.images.length];
                                    int t = 0;
                                    foreach (IHTMLImgElement image in doc.images)
                                    {
                                        IHTMLElement element = (IHTMLElement)image;
                                        strImage[t] = element.outerHTML;
                                        src[t] = (string)element.getAttribute("src", 2);
                                        t = t + 1;
                                    }

                                    string strItem = paperItem.Content;
                                    for (int x = 0; x < strImage.Length; x++)
                                    {
                                        strItem = strItem.Replace(strImage[x], "@");
                                    }

                                    string[] strText = strItem.Split('@');

                                    para = myDoc.Content.Paragraphs[num].Range;
                                    num = num + 1;
                                    para.Select();
                                    myWord.Application.Selection.TypeText(k + ". ");
                                    for (int x = 0; x < strText.Length; x++)
                                    {
                                        myWord.Application.Selection.TypeText(ItemBLL.NoHTML(strText[x]));
                                        if (x < src.Length)
                                        {
                                            myWord.Application.Selection.InlineShapes.AddPicture(Server.MapPath(src[x]), ref LinkToFile, ref SaveWithDocument, ref Nothing);
                                        }
                                    }
                                    myWord.Application.Selection.TypeText("   ��" + System.String.Format("{0:0.##}", paperSubject.UnitScore) + "�֣�");
                                    para.Font.Size = 9;
                                    para.Font.Bold = 0;
                                    para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                                    para.InsertParagraphAfter();
                                    #endregion
                                }

                                if (paperItem.TypeId == PrjPub.ITEMTYPE_FILLBLANK)
                                {
                                    NowCount = NowCount + 1;
                                    System.Threading.Thread.Sleep(10);
                                    jsBlock = "<script>SetPorgressBar('��ȡ����׼����ӡ','" + ((double)(NowCount * 100) / (double)SumCount).ToString("0.00") + "'); </script>";
                                    Response.Write(jsBlock);
                                    Response.Flush();
                                    continue;
                                }

                                // ��֯�û���
                                string[] strUserAnswers = new string[0];
                                string strUserAnswer = string.Empty;

                                //RandomExamResultAnswer theExamResultAnswer = null;
                                //foreach (RandomExamResultAnswer resultAnswer in examResultAnswers)
                                //{
                                //    if (resultAnswer.RandomExamItemId == paperItem.RandomExamItemId)
                                //    {
                                //        theExamResultAnswer = resultAnswer;
                                //        break;
                                //    }
                                //}
                                //// ���ӱ��޼�¼������ҳ�����
                                //if (theExamResultAnswer == null)
                                //{
                                //    SessionSet.PageMessage = "���ݴ���";
                                //}
                                //// ������֯������
                                //if (theExamResultAnswer.Answer != null || theExamResultAnswer.Answer == string.Empty)
                                //{
                                //    strUserAnswers = theExamResultAnswer.Answer.Split('|');
                                //}
                                //for (int n = 0; n < strUserAnswers.Length; n++)
                                //{
                                //    string strN = intToChar(int.Parse(strUserAnswers[n])).ToString();
                                //    if (n == 0)
                                //    {
                                //        strUserAnswer += strN;
                                //    }
                                //    else
                                //    {
                                //        strUserAnswer += "," + strN;
                                //    }
                                //}

                                if (paperItem.Answer == null)
                                {
                                    SessionSet.PageMessage = "���ݴ���";
                                }

                                if (!string.IsNullOrEmpty(paperItem.Answer))
                                {
                                    strUserAnswers = paperItem.Answer.Split(new char[] { '|' });
                                }

                                for (int n = 0; n < strUserAnswers.Length; n++)
                                {
                                    if (paperSubject.ItemTypeId != PrjPub.ITEMTYPE_QUESTION)
                                    {
                                        string strN = intToChar(int.Parse(strUserAnswers[n])).ToString();
                                        if (n == 0)
                                        {
                                            strUserAnswer += strN;
                                        }
                                        else
                                        {
                                            strUserAnswer += "," + strN;
                                        }
                                    }
                                    else
                                    {
                                        strUserAnswer = paperItem.Answer.Trim();
                                    }
                                }

                                string strContent = "";
                                string strTest = "";
                                int flag = 0;
                                if (!isPictureItem)
                                {
                                    #region ����ı������
                                    string[] strAnswer = paperItem.SelectAnswer.Split(new char[] { '|' });
                                    for (int n = 0; n < strAnswer.Length; n++)
                                    {
                                        string strN = intToChar(n).ToString();

                                        if (flag == 0)
                                        {
                                            strContent = "";
                                            strTest = "";
                                        }

                                        if (strContent == "")
                                        {
                                            strContent = strTest + strN + "." + strAnswer[n];
                                        }
                                        else
                                        {
                                            strContent = strTest + "    " + strN + "." + strAnswer[n];
                                        }

                                        strTest = strContent;

                                        if (n + 1 < strAnswer.Length)
                                        {
                                            strContent = strContent + "    " + intToChar(n + 1) + "." + strAnswer[n + 1];
                                        }

                                        if (Pub.GetStringRealLength(strContent) > 100)
                                        {
                                            para = myDoc.Content.Paragraphs[num].Range;
                                            num = num + 1;
                                            para.Text = "  " + strTest;
                                            //para.Font.Size = 9;
                                            //para.Font.Bold = 0;
                                            para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                                            para.InsertParagraphAfter();

                                            flag = 0;
                                        }
                                        else
                                        {
                                            if (n + 1 == strAnswer.Length && strTest != "")
                                            {
                                                para = myDoc.Content.Paragraphs[num].Range;
                                                num = num + 1;
                                                para.Text = "  " + strTest;
                                                //para.Font.Size = 9;
                                                //para.Font.Bold = 0;
                                                para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                                                para.InsertParagraphAfter();
                                            }
                                            flag = flag + 1;
                                        }
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region ���ͼƬ�����
                                    IHTMLDocument2 doc = new HTMLDocumentClass();
                                    doc.write(new object[] { paperItem.SelectAnswer });
                                    doc.close();

                                    string[] src = new string[doc.images.length];
                                    string[] strImage = new string[doc.images.length];
                                    int t = 0;
                                    foreach (IHTMLImgElement image in doc.images)
                                    {
                                        IHTMLElement element = (IHTMLElement)image;
                                        strImage[t] = element.outerHTML;
                                        src[t] = (string)element.getAttribute("src", 2);
                                        t = t + 1;
                                    }

                                    string strItem = paperItem.SelectAnswer;
                                    for (int x = 0; x < strImage.Length; x++)
                                    {
                                        strItem = strItem.Replace(strImage[x], "@");
                                    }

                                    string[] strAnswer = strItem.Split('|');
                                    for (int n = 0; n < strAnswer.Length; n++)
                                    {
                                        string strN = intToChar(n).ToString();

                                        if (flag == 0)
                                        {
                                            strContent = "";
                                            strTest = "";
                                        }

                                        if (strContent == "")
                                        {
                                            strContent = strTest + strN + "." + strAnswer[n];
                                        }
                                        else
                                        {
                                            strContent = strTest + "    " + strN + "." + strAnswer[n];
                                        }

                                        strTest = strContent;

                                        if (n + 1 < strAnswer.Length)
                                        {
                                            strContent = strContent + "    " + intToChar(n + 1) + "." + strAnswer[n + 1];
                                        }

                                        if (Pub.GetStringRealLength(strContent) > 100)
                                        {
                                            string[] strText = strTest.Split('@');
                                            para = myDoc.Content.Paragraphs[num].Range;
                                            num = num + 1;
                                            para.Select();
                                            myWord.Application.Selection.TypeText("  ");
                                            for (int x = 0; x < strText.Length; x++)
                                            {
                                                myWord.Application.Selection.TypeText(ItemBLL.NoHTML(strText[x]));
                                                if (x < src.Length)
                                                {
                                                    myWord.Application.Selection.InlineShapes.AddPicture(Server.MapPath(src[x]), ref LinkToFile, ref SaveWithDocument, ref Nothing);
                                                }
                                            }
                                            //para.Font.Size = 9;
                                            //para.Font.Bold = 0;
                                            para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                                            para.InsertParagraphAfter();

                                            flag = 0;
                                        }
                                        else
                                        {
                                            if (n + 1 == strAnswer.Length && strTest != "")
                                            {
                                                string[] strText = strTest.Split('@');
                                                para = myDoc.Content.Paragraphs[num].Range;
                                                num = num + 1;
                                                para.Select();
                                                myWord.Application.Selection.TypeText("  ");
                                                for (int x = 0; x < strText.Length; x++)
                                                {
                                                    myWord.Application.Selection.TypeText(ItemBLL.NoHTML(strText[x]));
                                                    if (x < src.Length)
                                                    {
                                                        myWord.Application.Selection.InlineShapes.AddPicture(Server.MapPath(src[x]), ref LinkToFile, ref SaveWithDocument, ref Nothing);
                                                    }
                                                }
                                                //para.Font.Size = 9;
                                                //para.Font.Bold = 0;
                                                para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                                                para.InsertParagraphAfter();
                                            }
                                            flag = flag + 1;
                                        }
                                    }
                                    #endregion
                                }

                                // ��֯��ȷ��
                                string[] strRightAnswers = paperItem.StandardAnswer.Split('|');
                                string strRightAnswer = "";
                                for (int n = 0; n < strRightAnswers.Length; n++)
                                {
                                    string strN = intToChar(int.Parse(strRightAnswers[n])).ToString();
                                    if (n == 0)
                                    {
                                        strRightAnswer += strN;
                                    }
                                    else
                                    {
                                        strRightAnswer += "," + strN;
                                    }
                                }

                                string strScore = "0";
                                if (strRightAnswer == strUserAnswer)
                                {
                                    strScore = System.String.Format("{0:0.##}", paperSubject.UnitScore);
                                }

                                para = myDoc.Content.Paragraphs[num].Range;
                                num = num + 1;
                                para.Text = "���׼�𰸣�" + strRightAnswer + "      �����𰸣�" + strUserAnswer + "      �÷֣�" + strScore;
                                //para.Font.Size = 9;
                                //para.Font.Bold = 0;
                                para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                                para.InsertParagraphAfter();

                                NowCount = NowCount + 1;

                                System.Threading.Thread.Sleep(10);
                                jsBlock = "<script>SetPorgressBar('��ȡ����׼����ӡ','" + ((double)(NowCount * 100) / (double)SumCount).ToString("0.00") + "'); </script>";
                                Response.Write(jsBlock);
                                Response.Flush();
                            }
                        }
                    }

                    myWord.Application.Selection.EndKey(ref unit, ref extend);
                    myWord.Application.Selection.TypeParagraph();
                    myWord.Application.Selection.InsertBreak(ref breakType);
                    myWord.Application.Selection.TypeBackspace();
                    myWord.Application.Selection.Delete(ref character, ref count);
                    myWord.Application.Selection.HomeKey(ref unit, ref extend);
                    myDoc.SaveAs2000(ref filename, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing,
                                     ref Nothing, ref Nothing, ref Nothing, ref Nothing);
                    myWord.Visible = true;
                    if (emp < examResults.Count - 1)
                    {
                        myDoc.Close(ref Nothing, ref Nothing, ref Nothing);
                        myWord.Application.Quit(ref Nothing, ref Nothing, ref Nothing);
                    }
                }
                // �������
                jsBlock = "<script>SetCompleted('������ɡ�'); </script>";
                Response.Write(jsBlock);
                Response.Flush();
            }
            catch (Exception ex)
            {
                myDoc.SaveAs2000(ref filename, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing,
                 ref Nothing, ref Nothing, ref Nothing, ref Nothing);
                throw ex;
            }
            finally
            {
                myDoc.Close(ref Nothing, ref Nothing, ref Nothing);
                myWord.Application.Quit(ref Nothing, ref Nothing, ref Nothing);
            }

            Response.Write("<script>top.returnValue='" + wordName + "';window.close();</script>");
        }
        #endregion

        #region ��������
        private string GetNo(int i)
        {
            string strReturn = string.Empty;

            switch (i.ToString())
            {
                case "0": strReturn = "һ";
                    break;
                case "1": strReturn = "��";
                    break;
                case "2": strReturn = "��";
                    break;
                case "3": strReturn = "��";
                    break;
                case "4": strReturn = "��";
                    break;
                case "5": strReturn = "��";
                    break;
                case "6": strReturn = "��";
                    break;
                case "7": strReturn = "��";
                    break;
                case "8": strReturn = "��";
                    break;
                case "9": strReturn = "ʮ";
                    break;
                case "10": strReturn = "ʮһ";
                    break;
                case "11": strReturn = "ʮ��";
                    break;
                case "12": strReturn = "ʮ��";
                    break;
                case "13": strReturn = "ʮ��";
                    break;
                case "14": strReturn = "ʮ��";
                    break;
                case "15": strReturn = "ʮ��";
                    break;
                case "16": strReturn = "ʮ��";
                    break;
                case "17": strReturn = "ʮ��";
                    break;
                case "18": strReturn = "ʮ��";
                    break;
                case "19": strReturn = "��ʮ";
                    break;
            }
            return strReturn;
        }

        private char intToChar(int intCol)
        {
            return Convert.ToChar('A' + intCol);
        }
        #endregion

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


        private IList<RandomExamItem> GetSubjectItems(IList<RandomExamItem> objList, int subjectId)
        {
            IList<RandomExamItem> newItems = new List<RandomExamItem>();

            foreach (RandomExamItem randomExamItem in objList)
            {
                if (randomExamItem.SubjectId == subjectId)
                {
                    newItems.Add(randomExamItem);
                }
            }

            return newItems;
        }
    }
}

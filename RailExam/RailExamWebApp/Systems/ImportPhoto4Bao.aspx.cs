using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Data.OracleClient;
using System.Drawing.Imaging;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Systems
{
    /// <summary>
    /// ��������Ա����Ƭ
    /// </summary>
    public partial class ImportPhoto4Bao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strFileName = Server.UrlDecode(Request.QueryString.Get("FileName"));
                string strRarPath = Server.MapPath("/RailExamBao/Excel/photo");
                string decompressPath = strRarPath+"\\"+strFileName.Substring(0,strFileName.Length-4);
                
                // ��ѹ���ϴ�����Ƭ����ļ�
                string error = Decompress(decompressPath, strRarPath, strFileName);
                if (string.IsNullOrEmpty(error))
                {
                    // ��ѹ�ɹ�֮�󣬱�����ѹ�������ļ�Ȼ��д�����ݿ⣬���ɾ�����������ļ���
                    string[] imgFileNames = Directory.GetFiles(decompressPath,"*.*",SearchOption.AllDirectories);
                    // ����
                    ImportPhoto(imgFileNames);
                    // ɾ����ѹ���ļ���ѹ����
                    Directory.Delete(decompressPath, true);
                    File.Delete(strRarPath + "\\" + strFileName);
                    Response.Write("<script>window.returnValue='refresh|����ɹ�!';window.close();</script>");
                }
                else
                {
                    Response.Write("<script>window.returnValue='" + error + "',window.close();</script>");
                    return;
                }
            }
        }

        private void ImportPhoto(string[] imgFileNames)
        {
            string jsBlock = string.Empty;
            string templateFileName = Server.MapPath("/RailExamBao/RandomExam/ProgressBar.htm");
            StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
            string html = reader.ReadToEnd();
            reader.Close();
            Response.Write(html);
            Response.Flush();
            System.Threading.Thread.Sleep(200);

            // ��ӹ�����Ч��
            jsBlock = "<script>SetPorgressBar('��׼��������Ƭ����','0.00'); </script>";
            Response.Write(jsBlock);
            Response.Flush();

            OracleAccess db = new OracleAccess();

            string errorMessage = string.Empty;

            // ѭ������������Ƭ�ļ�
            for (int i = 0; i < imgFileNames.Length; i++)
            {
                // ������Ч��
                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('���ڵ�����Ƭ����','" +
                          ((double)((i + 1) * 100) / (double)imgFileNames.Length).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                string f_n = imgFileNames[i];
                int lastPathChar = f_n.LastIndexOf("\\");
                string filename = f_n.Substring(lastPathChar + 1, f_n.LastIndexOf(".")-lastPathChar-1);

                // TODO: ���뵥����Ƭ
                try
                {
                    System.Drawing.Image image = System.Drawing.Image.FromFile(imgFileNames[i]);
                    System.Drawing.Image thumbnail = image.GetThumbnailImage(120, 150, null, IntPtr.Zero);
                    MemoryStream ms = new MemoryStream();
                    thumbnail.Save(ms, ImageFormat.Jpeg);
                    byte[] byteImage = ms.ToArray();

                    string workno = filename.Substring(0, 8);
                    string name = filename.Replace(workno, "");

                    string strSql = "select * from Employee where Work_No='" + workno + "' and Employee_Name='" + name + "'";
                    DataSet ds = db.RunSqlDataSet(strSql);

                    if(ds.Tables[0].Rows.Count>0)
                    {
                        //���
                        XmlDocument doc = new XmlDocument();
                        doc.Load(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "web.config"));
                        XmlNode node = doc.SelectSingleNode("configuration/dataConfiguration/@defaultDatabase");
                        string value = node.Value;

                        if (value == "Oracle")
                        {
                            System.Data.OracleClient.OracleParameter para1 = new System.Data.OracleClient.OracleParameter("p_photo", OracleType.Blob);
                            System.Data.OracleClient.OracleParameter para2 = new System.Data.OracleClient.OracleParameter("p_id", OracleType.Number);
                            para1.Value = byteImage;
                            para2.Value = Convert.ToInt32(ds.Tables[0].Rows[0]["Employee_ID"]);

                            IDataParameter[] paras = new IDataParameter[] { para1, para2 };

                            Pub.RunAddProcedureBlob(false, "USP_EMPLOYEE_IMAGE", paras, byteImage);
                        }
                    }
                    else
                    {
                        errorMessage += "ͼƬ��" + filename + "����ϵͳ�в�ѯ������Ӧ��Ա����Ϣ\n";
                    }

                    image.Dispose();
                    ms.Dispose();
                    thumbnail.Dispose();
                }
                catch
                {
                    errorMessage += "�ļ���" + filename + "������ͼƬ�����ļ�������\n";
                }

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('���ڵ���ͼƬ','" +((double)((i + 1) * 100) / (double)imgFileNames.Length) + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

            }

            if (errorMessage != string.Empty)
            {
                Response.Write("<script>window.returnValue='" + errorMessage + "',window.close();</script>");
                return;
            }

            // �������
            jsBlock = "<script>SetCompleted('��Ƭ���ݵ������'); </script>";
            Response.Write(jsBlock);
            Response.Flush();
        }

        /// <summary>  
        /// ���� WinRAR ���н�ѹ��  
        /// </summary>  
        /// <param name="path">�ļ���ѹ·�������ԣ�</param>  
        /// <param name="rarPath">��Ҫ��ѹ���� .rar �ļ��Ĵ��Ŀ¼������·����</param>  
        /// <param name="rarName">��Ҫ��ѹ���� .rar �ļ�����������׺��</param>  
        /// <returns>���ش�����Ϣ</returns>  
        private string Decompress(string path, string rarPath, string rarName)
        {
            string error_return = string.Empty;
            string rarexe;
            RegistryKey regkey;
            Object regvalue;
            string cmd;
            ProcessStartInfo startinfo;
            Process process;
            try
            {
                regkey = Registry.ClassesRoot.OpenSubKey(@"WinRAR\shell\open\command");
                regvalue = regkey.GetValue("");
                rarexe = regvalue.ToString();
                regkey.Close();
                // ��ȡWinRAR.exe ��ϵͳ�ϵ�ȫ�ļ�����
                rarexe = rarexe.Substring(1, rarexe.Length - 7);
                
                Directory.CreateDirectory(path);
                path = "\"" + path + "\"";
                //��ѹ������൱����Ҫѹ���ļ�(rarName)�ϵ��Ҽ�->WinRAR->��ѹ����ǰ�ļ���  
                cmd = string.Format("x {0} {1} -y", rarName, path);
                startinfo = new ProcessStartInfo();
                startinfo.FileName = rarexe;
                startinfo.Arguments = cmd;
                startinfo.WindowStyle = ProcessWindowStyle.Hidden;

                startinfo.WorkingDirectory = rarPath;
                process = new Process();
                process.StartInfo = startinfo;
                process.Start();
                process.WaitForExit();
                process.Close();
            }
            catch (Exception e)
            {
                error_return = e.Message;
            }
            return error_return;
        }
    }
}

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
    /// 批量导入员工照片
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
                
                // 解压缩上传的照片打包文件
                string error = Decompress(decompressPath, strRarPath, strFileName);
                if (string.IsNullOrEmpty(error))
                {
                    // 解压成功之后，遍历解压出来的文件然后写入数据库，最后删除本次所有文件。
                    string[] imgFileNames = Directory.GetFiles(decompressPath,"*.*",SearchOption.AllDirectories);
                    // 导入
                    ImportPhoto(imgFileNames);
                    // 删除解压缩文件及压缩包
                    Directory.Delete(decompressPath, true);
                    File.Delete(strRarPath + "\\" + strFileName);
                    Response.Write("<script>window.returnValue='refresh|导入成功!';window.close();</script>");
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

            // 添加滚动条效果
            jsBlock = "<script>SetPorgressBar('正准备导入照片数据','0.00'); </script>";
            Response.Write(jsBlock);
            Response.Flush();

            OracleAccess db = new OracleAccess();

            string errorMessage = string.Empty;

            // 循环批量导入照片文件
            for (int i = 0; i < imgFileNames.Length; i++)
            {
                // 滚动条效果
                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('正在导入照片数据','" +
                          ((double)((i + 1) * 100) / (double)imgFileNames.Length).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                string f_n = imgFileNames[i];
                int lastPathChar = f_n.LastIndexOf("\\");
                string filename = f_n.Substring(lastPathChar + 1, f_n.LastIndexOf(".")-lastPathChar-1);

                // TODO: 导入单张照片
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
                        //添加
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
                        errorMessage += "图片【" + filename + "】在系统中查询不到对应的员工信息\n";
                    }

                    image.Dispose();
                    ms.Dispose();
                    thumbnail.Dispose();
                }
                catch
                {
                    errorMessage += "文件【" + filename + "】不是图片或者文件名错误\n";
                }

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('正在导入图片','" +((double)((i + 1) * 100) / (double)imgFileNames.Length) + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

            }

            if (errorMessage != string.Empty)
            {
                Response.Write("<script>window.returnValue='" + errorMessage + "',window.close();</script>");
                return;
            }

            // 处理完成
            jsBlock = "<script>SetCompleted('照片数据导入完毕'); </script>";
            Response.Write(jsBlock);
            Response.Flush();
        }

        /// <summary>  
        /// 利用 WinRAR 进行解压缩  
        /// </summary>  
        /// <param name="path">文件解压路径（绝对）</param>  
        /// <param name="rarPath">将要解压缩的 .rar 文件的存放目录（绝对路径）</param>  
        /// <param name="rarName">将要解压缩的 .rar 文件名（包括后缀）</param>  
        /// <returns>返回错误信息</returns>  
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
                // 获取WinRAR.exe 在系统上的全文件名。
                rarexe = rarexe.Substring(1, rarexe.Length - 7);
                
                Directory.CreateDirectory(path);
                path = "\"" + path + "\"";
                //解压缩命令，相当于在要压缩文件(rarName)上点右键->WinRAR->解压到当前文件夹  
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

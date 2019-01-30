using System;
using System.Collections;
using System.Data;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using RailExam.BLL;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.AssistBook
{
    public class InformationShow
    {
        public InformationShow()
        {
            
        }

        /// <summary>
        /// ·¢²¼½Ì²Ä
        /// </summary>
        /// <param name="strID"></param>
        public void GetIndex(string strID)
        {
            string strItem, strChapter;

            OracleAccess  db =new OracleAccess();

            string strSql = "select * from Information where Information_ID=" + strID;
            DataSet ds = db.RunSqlDataSet(strSql);

            if(ds.Tables[0].Rows.Count==0)
            {
                return;
            }

            DataRow dr = ds.Tables[0].Rows[0];

            string strBookName = dr["Information_Name"].ToString();
            string strBookUrl = dr["Url"].ToString();
            string strVersion = dr["Version"].ToString();

            WriteXml(strID, strVersion);

            strItem = "var TREE_ITEMS = [ ['" + strBookName + "', 'common.htm?url=cover.htm&chapterid=0&bookid=" + strID + "',";

            strChapter = "var Tree_Chapter=['0','cover.htm','" + strBookName + "'";

            strSql = "select a.*,GetInformationChapterName(a.Chapter_Id) as NamePath from Information_Chapter a where Information_ID=" + strID;
            DataSet dsChaper = db.RunSqlDataSet(strSql);

            foreach (DataRow drchapter in dsChaper.Tables[0].Rows)
            {
                if (Convert.ToInt32(drchapter["Parent_ID"]) == 0)
                {
                    if (drchapter["URL"].ToString() == "" || drchapter["URL"] == DBNull.Value)
                    {
                        strItem += "['" + drchapter["Chapter_Name"] + "', 'common.htm?url=empty.htm&chapterid=" + drchapter["Chapter_ID"] + "&bookid=" + strID + "'";
                        strChapter += ",'" + drchapter["Chapter_ID"] + "','empty.htm','" + drchapter["NamePath"] + "'";
                    }
                    else
                    {
                        strItem += "['" + drchapter["Chapter_Name"] + "', 'common.htm?url=" + drchapter["Chapter_ID"] + ".htm&chapterid=" + drchapter["Chapter_ID"] + "&bookid=" + strID + "'";
                        strChapter += ",'" + drchapter["Chapter_ID"] + "','" + drchapter["Chapter_ID"] + ".htm','" + drchapter["NamePath"] + "'";
                    }

                    strSql = "select * from Information_Chapter where Parent_ID=" + drchapter["Chapter_ID"];
                    if (db.RunSqlDataSet(strSql).Tables[0].Rows.Count > 0)
                    {
                        strItem += ",";
                    }

                    strItem = Get(Convert.ToInt32(drchapter["Chapter_ID"]), strItem);

                    strChapter = GetChapter(Convert.ToInt32(drchapter["Chapter_ID"]), strChapter);
                }
            }

            strItem += "]];";
            string strPath = "../Online/AssistBook/" + strID + "/tree_items.js";
            File.Delete(HttpContext.Current.Server.MapPath(strPath));
            File.AppendAllText(HttpContext.Current.Server.MapPath(strPath), strItem, System.Text.Encoding.UTF8);

            strChapter += "];";
            strPath = "../Online/AssistBook/" + strID + "/tree_chapter.js";
            if (File.Exists(HttpContext.Current.Server.MapPath(strPath)))
            {
                File.Delete(HttpContext.Current.Server.MapPath(strPath));
            }
            File.AppendAllText(HttpContext.Current.Server.MapPath(strPath), strChapter, System.Text.Encoding.UTF8);

            string[] strIndex = File.ReadAllLines(HttpContext.Current.Server.MapPath("../Online/AssistBook/" + strID + "/index.html"), System.Text.Encoding.Default);

            for (int i = 0; i < strIndex.Length; i++)
            {
                if (strIndex[i].IndexOf("<title>") != -1)
                {
                    strIndex[i] = "\t<title> " + strBookName + " </title>";
                }

                if (strIndex[i].IndexOf("common.htm?url=cover.htm&chapterid=0") != -1)
                {
                    strIndex[i] =
                        strIndex[i].Replace("common.htm?url=cover.htm&chapterid=0", "common.htm?url=cover.htm&chapterid=0&bookid=" + strID);
                }
            }

            File.WriteAllLines(HttpContext.Current.Server.MapPath("../Online/AssistBook/" + strID + "/index.html"), strIndex, System.Text.Encoding.UTF8);
        }

        private string Get(int strParentID, string strItem)
        {
            OracleAccess db = new OracleAccess();

            string strSql = "select a.*,GetInformationChapterName(a.Chapter_Id) as NamePath from Information_Chapter a where Parent_ID=" + strParentID;
            DataSet dsChaper = db.RunSqlDataSet(strSql);

            foreach (DataRow drchapter in dsChaper.Tables[0].Rows)
            {
                if (drchapter["URL"].ToString() == "" || drchapter["URL"] == DBNull.Value)
                {
                    strItem += "['" + drchapter["Chapter_Name"] + "', 'common.htm?url=empty.htm&chapterid=" + drchapter["Chapter_ID"] + "&bookid=" + drchapter["Information_ID"] + "'";
                }
                else
                {
                    strItem += "['" + drchapter["Chapter_Name"] + "', 'common.htm?url=" + drchapter["Chapter_ID"] + ".htm&chapterid=" + drchapter["Chapter_ID"] + "&bookid=" + drchapter["Information_ID"] + "'";
                }

                strSql = "select * from Information_Chapter where Parent_ID=" + drchapter["Chapter_ID"];
                if (db.RunSqlDataSet(strSql).Tables[0].Rows.Count > 0)
                {
                    strItem += ",";
                }

                strItem = Get(Convert.ToInt32(drchapter["Chapter_ID"]), strItem);
            }

            strItem += "],";

            return strItem;
        }

        private string GetChapter(int strParentID, string strChapter)
        {
            OracleAccess db = new OracleAccess();

            string strSql = "select a.*,GetInformationChapterName(a.Chapter_Id) as NamePath from Information_Chapter a where Parent_ID=" + strParentID;
            DataSet dsChaper = db.RunSqlDataSet(strSql);

            foreach (DataRow drchapter in dsChaper.Tables[0].Rows)
            {
                if (drchapter["URL"].ToString() == "" || drchapter["URL"] == DBNull.Value)
                {
                    strChapter += ",'" + drchapter["Chapter_ID"] + "','empty.htm','" + drchapter["NamePath"] + "'";
                }
                else
                {
                    strChapter += ",'" + drchapter["Chapter_ID"] + "','" + drchapter["Chapter_ID"] + ".htm','" + drchapter["NamePath"] + "'";
                }
                strChapter = GetChapter(Convert.ToInt32(drchapter["Chapter_ID"]), strChapter);
            }
            return strChapter;
        }

        private void WriteXml(string strID, string strVersion)
        {
            ArrayList objList = new ArrayList();
            string str = "";
            int i = 0;
            StreamReader objReader = new StreamReader(HttpContext.Current.Server.MapPath("../Online/AssistBook/" + strID + "/version.xml"), System.Text.Encoding.Default);
            while ((str = objReader.ReadLine()) != null)
            {
                if (str.IndexOf("<NowVersion>") != -1)
                {
                    str = str.Substring(0, str.IndexOf("<")) + "<NowVersion>" + strVersion + "</NowVersion>";
                }
                objList.Add(str);

                i = i + 1;
            }

            objReader.Close();

            StreamWriter objWriter = new StreamWriter(HttpContext.Current.Server.MapPath("../Online/AssistBook/" + strID + "/version.xml"), false, System.Text.Encoding.UTF8);
            for (int j = 0; j < i; j++)
            {
                objWriter.WriteLine(objList[j]);
            }
            objWriter.Close();
        }
    }
}

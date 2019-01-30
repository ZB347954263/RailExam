using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Reflection;
using ComponentArt.Web.UI;
using Microsoft.SqlServer.Server;
using RailExam.Model;


namespace RailExamWebApp.Common.Class
{
    public class Pub
    {
        public Pub()
        {
        }

        #region 此方法适用于，以ID编码方式构造树型结构的情况。考虑最简单的情况：编码步长相等。



        public static void BindTreeViewSingle(System.Web.UI.WebControls.TreeView tv, DataTable dt, string strTextField, string strValueField,
                                        string strImageUrlField, string strNavigateUrlField, string strTargetField)
        {
            int nStep = 0;
            int nLastValueLength = 0;
            string strValue = string.Empty;
            string strText = string.Empty;
            string strImageUrl = string.Empty;
            string strNavigateUrl = string.Empty;
            string strTarget = string.Empty;
            TreeNode tn = null;
            TreeNode tnParent = null;

            if (dt.Rows.Count > 0)
            {
                nStep = ((string)dt.Rows[0]["ID"]).Length;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strValue = (string)dt.Rows[i][strValueField];
                strText = (string)dt.Rows[i][strTextField];
                if (!String.IsNullOrEmpty(strImageUrlField))
                {
                    strImageUrl = (string)dt.Rows[i][strImageUrlField];
                }
                if (!String.IsNullOrEmpty(strNavigateUrlField))
                {
                    strNavigateUrl = (string)dt.Rows[i][strNavigateUrlField];
                }
                if (!String.IsNullOrEmpty(strTargetField))
                {
                    strTarget = (string)dt.Rows[i][strTargetField];
                }

                tn = new TreeNode(strText, strValue, strImageUrl, strNavigateUrl, strTarget);

                if (strValue.Length == nStep)
                {
                    tv.Nodes.Add(tn);
                    nLastValueLength = tn.Value.Length;
                    tnParent = tn;
                    continue;
                }
                else if (strValue.Length == nLastValueLength)
                {
                    tnParent.Parent.ChildNodes.Add(tn);
                    nLastValueLength = tn.Value.Length;
                    continue;
                }
                else if (strValue.Length > nLastValueLength)
                {
                    tnParent.ChildNodes.Add(tn);
                    nLastValueLength = tn.Value.Length;
                    tnParent = tn;
                    continue;
                }

                while (tnParent.Value.Length >= strValue.Length)
                {
                    tnParent = tnParent.Parent;
                }
                tnParent.ChildNodes.Add(tn);
                nLastValueLength = tn.Value.Length;
                tnParent = tn;
            }
        }
        #endregion

        #region 此方法适用于，以“父ID+子ID+XML”方式构造树型结构的情况。



        /// <summary>
        /// 构造XML标签树


        /// </summary>
        /// <param name="doc">XML文档</param>
        /// <param name="node">节点</param>
        /// <param name="dt">数据源表</param>
        /// <param name="idField">ID字段</param>
        /// <param name="parentIdField">父ID字段</param>
        /// <param name="textField">文本字段</param>
        /// <param name="urlField">链接字段</param>
        /// <param name="target">目标窗体</param>
        /// <param name="imagePath">图片路径</param>
        /// <param name="tooltipLength">文本显示长度</param>
        public static void AppendChildNodes(XmlDocument doc, XmlNode node, DataTable dt,
            string idField, string parentIdField, string textField, string urlField,
            string target, string imagePath, int tooltipLength)
        {
            XmlNode xmlCurrentNode = null;
            XmlNode xmlTr = null;
            XmlNode xmlTd = null;
            XmlNode xmlDiv = null;
            XmlNode xmlTable = null;
            XmlNode xmlImg = null;
            XmlNode xmlA = null;
            XmlAttribute att = null;
            string strNodeId = node.Attributes["NodeId"].Value;

            DataRow[] drs = dt.Select(parentIdField + " = " + strNodeId);

            //如果本节点存在下级节点，则为本节点增加下级节点显示所需的标签



            if (strNodeId != "0" && drs.Length > 0)
            {
                //<tr>
                xmlTr = doc.CreateNode(XmlNodeType.Element, "TR", null);
                node.ParentNode.InsertAfter(xmlTr, node);

                //<tr><td>
                xmlTd = doc.CreateNode(XmlNodeType.Element, "TD", null);
                xmlTr.AppendChild(xmlTd);

                //<tr><td><img />
                xmlImg = doc.CreateNode(XmlNodeType.Element, "IMG", null);
                att = doc.CreateAttribute("SRC");
                att.Value = imagePath;
                xmlImg.Attributes.Append(att);
                att = doc.CreateAttribute("ONCLICK");
                att.Value = "displayChildNodes(DivId_" + strNodeId + ",this)";
                xmlImg.Attributes.Append(att);
                node.FirstChild.AppendChild(xmlImg);

                //<tr><td><img /><td>
                xmlTd = doc.CreateNode(XmlNodeType.Element, "TD", null);
                xmlTr.AppendChild(xmlTd);

                //<tr><td><img /><td><div>
                xmlDiv = doc.CreateNode(XmlNodeType.Element, "DIV", null);
                att = doc.CreateAttribute("ID");
                att.Value = "DivId_" + strNodeId;
                xmlDiv.Attributes.Append(att);
                att = doc.CreateAttribute("STYLE");
                att.Value = "DISPLAY:VISIBLE;";
                xmlDiv.Attributes.Append(att);
                xmlTd.AppendChild(xmlDiv);

                //<tr><td><img /><td><div><table>
                xmlTable = doc.CreateNode(XmlNodeType.Element, "TABLE", null);
                xmlDiv.AppendChild(xmlTable);
            }

            string strName = string.Empty;
            foreach (DataRow dr in drs)
            {
                //<TR>
                xmlCurrentNode = doc.CreateNode(XmlNodeType.Element, "TR", null);

                //<TR NodeId="">
                att = doc.CreateAttribute("NodeId");
                att.Value = dr[idField].ToString();
                xmlCurrentNode.Attributes.Append(att);

                if (strNodeId == "0")
                {
                    node.AppendChild(xmlCurrentNode);
                }
                else
                {
                    xmlTable.AppendChild(xmlCurrentNode);
                }

                //<TR><TD>
                xmlTd = doc.CreateNode(XmlNodeType.Element, "TD", null);
                xmlCurrentNode.AppendChild(xmlTd);

                //<TR><TD/><TD>
                xmlTd = doc.CreateNode(XmlNodeType.Element, "TD", null);
                xmlCurrentNode.AppendChild(xmlTd);

                //<TR><TD/><TD><A>
                xmlA = doc.CreateNode(XmlNodeType.Element, "A", null);
                strName = dr[textField].ToString();
                xmlA.InnerText = (strName.Length > tooltipLength ? strName.Substring(0, tooltipLength)
                    + "..." : strName);
                att = doc.CreateAttribute("ID");
                att.Value = dr[idField].ToString();
                xmlA.Attributes.Append(att);
                att = doc.CreateAttribute("TITLE");
                att.Value = strName;
                xmlA.Attributes.Append(att);
                att = doc.CreateAttribute("HREF");
                att.Value = dr[urlField].ToString();
                xmlA.Attributes.Append(att);
                att = doc.CreateAttribute("TARGET");
                att.Value = target;
                xmlA.Attributes.Append(att);
                att = doc.CreateAttribute("ONCLICK");
                att.Value = "MySetSelected(this);";
                xmlA.Attributes.Append(att);
                xmlTd.AppendChild(xmlA);

                AppendChildNodes(doc, xmlCurrentNode, dt, idField,
                    parentIdField, textField, urlField, target, imagePath, tooltipLength);
            }
        }

        public static void BuildTree(Page page, DataTable dt, string idField, string parentIdField,
             string textField, string urlField, string target, string imagePath, int tooltipLength)
        {
            XmlDocument doc = new XmlDocument();

            XmlNode nod = doc.CreateNode(XmlNodeType.XmlDeclaration, null, null);
            doc.AppendChild(nod);

            nod = doc.CreateNode(XmlNodeType.Element, "TABLE", null);
            XmlAttribute att = doc.CreateAttribute("BORDER");
            att.Value = "0";
            nod.Attributes.Append(att);
            att = doc.CreateAttribute("CELLPADDING");
            att.Value = "0";
            nod.Attributes.Append(att);
            att = doc.CreateAttribute("CELLSPACING");
            att.Value = "0";
            nod.Attributes.Append(att);
            att = doc.CreateAttribute("NodeId");
            att.Value = "0";
            nod.Attributes.Append(att);
            doc.AppendChild(nod);

            if (dt.Rows.Count > 0)
            {
                AppendChildNodes(doc, nod, dt, idField, parentIdField,
                    textField, urlField, target, imagePath, tooltipLength);
            }

            doc.Save(page.Response.Output);
        }

        /// <summary>
        /// 返回节点到根的路径，如ID=10101，ReturnValue=/0/1/101/10101
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="idField"></param>
        /// <param name="parentIdField"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetTreePath(DataTable dt, string idField, string parentIdField, string id)
        {
            string strPath = id;

            //如果到根节点
            if (id == "0")
            {
                return "/";
            }

            //只有唯一父节点






            DataRow[] drs = dt.Select(idField + " = " + id);
            if (drs.Length != 1)
            {
                throw new ArgumentException("参数 [" + id + "] 不是表中的有效数据！", id);
            }
            //未到根节点






            strPath = GetTreePath(dt, idField, parentIdField, drs[0][parentIdField].ToString()) + "/" + strPath;

            return strPath;
        }
        #endregion

        #region 与XML相关的方法



        private XmlDocument GetXmlDocument(DataTable dt, string strTextField, string strValueField,
            string strImageUrlField, string strNavigateUrlField, string strTargetField, string strFilePath)
        {
            //DataRow drNew = dt.NewRow();
            //drNew[strValueField] = string.Empty;
            //dt.Rows.InsertAt(drNew, 0);
            //dt.AcceptChanges();

            XmlDocument xmldoc = new XmlDocument();

            XmlNode xmlnode = xmldoc.CreateNode(XmlNodeType.XmlDeclaration, null, null);
            xmldoc.AppendChild(xmlnode);

            xmlnode = xmldoc.CreateNode(XmlNodeType.Element, "TreeNodes", null);
            XmlAttribute xmlatt = xmldoc.CreateAttribute("text");
            xmlatt.Value = string.Empty;
            xmlnode.Attributes.Append(xmlatt);
            xmldoc.AppendChild(xmlnode);

            string strCurrentValue = string.Empty;
            string strLastValue = "";
            XmlNode xmlCurrentNode = null;
            XmlNode xmlLastNode = xmlnode;
            foreach (DataRow dr in dt.Rows)
            {
                strCurrentValue = dr[strValueField].ToString();
                xmlCurrentNode = xmldoc.CreateNode(XmlNodeType.Element, "TreeNode", null);
                xmlatt = xmldoc.CreateAttribute("text");
                xmlatt.Value = dr[strTextField].ToString();
                xmlCurrentNode.Attributes.Append(xmlatt);
                xmlatt = xmldoc.CreateAttribute("value");
                xmlatt.Value = strCurrentValue;
                xmlCurrentNode.Attributes.Append(xmlatt);
                if (!String.IsNullOrEmpty(strImageUrlField))
                {
                    xmlatt = xmldoc.CreateAttribute("imageUrl");
                    xmlatt.Value = dr[strImageUrlField].ToString();
                    xmlCurrentNode.Attributes.Append(xmlatt);
                }
                if (!String.IsNullOrEmpty(strNavigateUrlField))
                {
                    xmlatt = xmldoc.CreateAttribute("navigateUrl");
                    xmlatt.Value = dr[strNavigateUrlField].ToString();
                    xmlCurrentNode.Attributes.Append(xmlatt);
                }
                if (!String.IsNullOrEmpty(strTargetField))
                {
                    xmlatt = xmldoc.CreateAttribute("Target");
                    xmlatt.Value = dr[strTargetField].ToString();
                    xmlCurrentNode.Attributes.Append(xmlatt);
                }

                if (strCurrentValue.Length - strLastValue.Length > 0)
                {
                    xmlLastNode.AppendChild(xmlCurrentNode);
                    xmlLastNode = xmlCurrentNode;
                    strLastValue = strCurrentValue;
                }
                else if (strCurrentValue.Length - strLastValue.Length == 0)
                {
                    xmlLastNode.ParentNode.AppendChild(xmlCurrentNode);
                    strLastValue = strCurrentValue;
                }
                else
                {
                    while (strCurrentValue.Length < xmlLastNode.Attributes["value"].Value.Length)
                    {
                        xmlLastNode = xmlLastNode.ParentNode;
                    }
                    xmlLastNode.ParentNode.AppendChild(xmlCurrentNode);
                    xmlLastNode = xmlCurrentNode;
                    strLastValue = strCurrentValue;
                }
            }
            //dt.TableName = "TEST";
            //dt.WriteXml(strFilePath); 


            if (System.IO.File.Exists(strFilePath))
            {
                System.IO.File.Delete(strFilePath);
            }
            xmldoc.Save(strFilePath);
            return xmldoc;
        }
        #endregion

        #region  此方法适用于，以“父ID+子ID+TreeView”方式构造树型结构的情况。



        public static void AppendTreeNodes(TreeNode tn, DataTable dt, string strIdField, string strParentIdField,
                                           string strTextField, string strImageUrlField, string strNavigateUrlField,
                                           string strTargetField, string strId)
        {
            TreeNode tnNew = null;
            string strText = string.Empty;
            string strValue = string.Empty;
            string strImageUrl = string.Empty;
            string strNavigateUrl = string.Empty;
            string strTarget = string.Empty;

            DataRow[] drs = dt.Select(strParentIdField + " = " + strId);
            if (drs.Length > 0)
            {
                foreach (DataRow dr in drs)
                {
                    strValue = dr[strIdField].ToString();
                    strText = dr[strTextField].ToString();
                    if (!String.IsNullOrEmpty(strImageUrlField))
                    {
                        strImageUrl = (string)dr[strImageUrlField];
                    }
                    if (!String.IsNullOrEmpty(strNavigateUrlField))
                    {
                        strNavigateUrl = (string)dr[strNavigateUrlField];
                    }
                    if (!String.IsNullOrEmpty(strTargetField))
                    {
                        strTarget = (string)dr[strTargetField];
                    }

                    tnNew = new TreeNode(strText, strValue, strImageUrl, strNavigateUrl, strTarget);
                    tn.ChildNodes.Add(tnNew);

                    //节点tnNew继续挂下级节点






                    AppendTreeNodes(tnNew, dt, strIdField, strParentIdField, strTextField, strImageUrlField,
                                    strNavigateUrlField, strTargetField, dr[strIdField].ToString());
                }
            }
        }

        public static void BindTreeViewDouble(System.Web.UI.WebControls.TreeView tv, DataTable dt, string strIdField, string strParentIdField,
                                              string strTextField, string strImageUrlField, string strNavigateUrlField,
                                              string strTargetField)
        {
            TreeNode tnNew = null;
            string strText = string.Empty;
            string strValue = string.Empty;
            string strImageUrl = string.Empty;
            string strNavigateUrl = string.Empty;
            string strTarget = string.Empty;

            DataRow[] drs = dt.Select(strParentIdField + " = 0");
            if (drs.Length > 0)
            {
                foreach (DataRow dr in drs)
                {
                    strValue = dr[strIdField].ToString();
                    strText = dr[strTextField].ToString();
                    if (!String.IsNullOrEmpty(strImageUrlField))
                    {
                        strImageUrl = (string)dr[strImageUrlField];
                    }
                    if (!String.IsNullOrEmpty(strNavigateUrlField))
                    {
                        strNavigateUrl = (string)dr[strNavigateUrlField];
                    }
                    if (!String.IsNullOrEmpty(strTargetField))
                    {
                        strTarget = (string)dr[strTargetField];
                    }

                    tnNew = new TreeNode(strText, strValue, strImageUrl, strNavigateUrl, strTarget);
                    tv.Nodes.Add(tnNew);

                    //节点tnNew继续挂下级节点






                    AppendTreeNodes(tnNew, dt, strIdField, strParentIdField, strTextField, strImageUrlField,
                                    strNavigateUrlField, strTargetField, dr[strIdField].ToString());
                }
            }
        }
        #endregion

        #region 构造ComponentArt中的TreeView

        public static void BuildComponentArtTreeView(ComponentArt.Web.UI.TreeView tvTargetTree, IList list,
                                              string strIdProperty, string strParentIdProperty,
                                              string strTextProperty, string strTooltipProperty, string strValueProperty,
                                              string strNavigateUrlProperty, string strImageUrlProperty, string strTarget)
        {
            ComponentArt.Web.UI.TreeViewNode node = null;

            if (list.Count > 0)
            {
                Type objType = list[0].GetType();
                string strParentId = string.Empty;

                foreach (object o in list)
                {
                    node = new TreeViewNode();

                    // ID
                    if (string.IsNullOrEmpty(strIdProperty))
                    {
                        throw new Exception("树节点ID属性不能为空！");
                    }
                    node.ID = objType.GetProperty(strIdProperty).GetValue(o, null).ToString();

                    // ParentId
                    if (string.IsNullOrEmpty(strParentIdProperty))
                    {
                        throw new Exception("树节点父ID属性不能为空！");
                    }
                    strParentId = objType.GetProperty(strParentIdProperty).GetValue(o, null).ToString();

                    // Text
                    if (string.IsNullOrEmpty(strTextProperty))
                    {
                        throw new Exception("树节点Text属性不能为空！");
                    }
                    node.Text = objType.GetProperty(strTextProperty).GetValue(o, null).ToString();

                    // Tooltip
                    if (!string.IsNullOrEmpty(strTooltipProperty))
                    {
                        node.ToolTip = objType.GetProperty(strTooltipProperty).GetValue(o, null).ToString();
                    }

                    // Value
                    if (!string.IsNullOrEmpty(strValueProperty))
                    {
                        node.Value = objType.GetProperty(strValueProperty).GetValue(o, null).ToString();
                    }
                    else
                    {
                        node.Value = objType.GetProperty(strValueProperty).GetValue(o, null).ToString();
                    }

                    // NavigateUrl
                    if (!string.IsNullOrEmpty(strNavigateUrlProperty))
                    {
                        node.NavigateUrl = objType.GetProperty(strNavigateUrlProperty).GetValue(o, null).ToString();
                    }

                    // ImageUrl
                    if (!string.IsNullOrEmpty(strImageUrlProperty))
                    {
                        node.ImageUrl = objType.GetProperty(strImageUrlProperty).GetValue(o, null).ToString();
                    }

                    // Target
                    if (!string.IsNullOrEmpty(strTarget))
                    {
                        node.Target = objType.GetProperty(strTarget).GetValue(o, null).ToString();
                    }

                    // Append node
                    if (strParentId == "0")
                    {// Root node
                        tvTargetTree.Nodes.Add(node);
                    }
                    else
                    {
                        tvTargetTree.FindNodeById(strParentId).Nodes.Add(node);
                    }
                }
            }
        }
        #endregion

        public static DataTable ConvertToDataTable(IList list)
        {
            DataTable dt = null;

            if (list.Count > 0)
            {
                dt = new DataTable(list[0].GetType().Name);
                Type type = list[0].GetType();

                foreach (PropertyInfo pi in type.GetProperties())
                {
                    dt.Columns.Add(pi.Name, pi.PropertyType);
                }

                DataRow dr = null;
                foreach (Object o in list)
                {
                    dr = dt.NewRow();

                    foreach (DataColumn dc in dt.Columns)
                    {
                        dr[dc] = type.GetProperty(dc.ColumnName).GetValue(o, null);
                    }
                    dt.Rows.Add(dr);
                }
                dt.AcceptChanges();
            }

            return dt;
        }

        public static DataTable ConvertToDataTable(Type type)
        {
            DataTable dt = new DataTable(type.Name);

            foreach (PropertyInfo pi in type.GetProperties())
            {
                dt.Columns.Add(pi.Name, pi.PropertyType);
            }
            dt.AcceptChanges();

            return dt;
        }

        public static void ShowErrorPage(string errorMessage)
        {
            HttpContext.Current.Response.Redirect("/RailExamBao/Common/Error.aspx?error=" + errorMessage);
        }

        //public static  string GetChineseSpell(string strText)
        // {
        //     int len = strText.Length;
        //     string myStr = "";
        //     for (int i = 0; i < len; i++)
        //     {
        //         myStr += getSpell(strText.Substring(i, 1));
        //     }
        //     return myStr;
        // }

        // private static  string getSpell(string cnChar)
        // {
        //     byte[] arrCN = Encoding.Default.GetBytes(cnChar);
        //     if (arrCN.Length > 1)
        //     {
        //         int area = (short)arrCN[0];
        //         int pos = (short)arrCN[1];
        //         int code = (area << 8) + pos;
        //         int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
        //         for (int i = 0; i < 26; i++)
        //         {
        //             int max = 55290;
        //             if (i != 25) max = areacode[i + 1];
        //             if (areacode[i] <= code && code < max)
        //             {
        //                 return Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
        //             }
        //         }
        //         return "*";
        //     }
        //     else return cnChar;
        // } 


        public static String GetChineseSpell(String IndexTxt)
        {
            String _Temp = null;
            for (int i = 0; i < IndexTxt.Length; i++)
                _Temp = _Temp + GetOneIndex(IndexTxt.Substring(i, 1));
            return _Temp;
        }

        //得到单个字符的首字母
        public static String GetOneIndex(String OneIndexTxt)
        {
            if (Convert.ToChar(OneIndexTxt) >= 0 && Convert.ToChar(OneIndexTxt) < 256)
                return OneIndexTxt;
            else
            {
                Encoding gb2312 = Encoding.GetEncoding("gb2312");
                byte[] unicodeBytes = Encoding.Unicode.GetBytes(OneIndexTxt);
                byte[] gb2312Bytes = Encoding.Convert(Encoding.Unicode, gb2312, unicodeBytes);
                try
                {
                    return GetX(Convert.ToInt32(
                    String.Format("{0:D2}", Convert.ToInt16(gb2312Bytes[0]) - 160)
                    + String.Format("{0:D2}", Convert.ToInt16(gb2312Bytes[1]) - 160)
                    ));
                }
                catch
                {
                    return "*";
                }
            }
        }

        //根据区位得到首字母

        private static String GetX(int GBCode)
        {
            if (GBCode >= 1601 && GBCode < 1637) return "A";
            if (GBCode >= 1637 && GBCode < 1833) return "B";
            if (GBCode >= 1833 && GBCode < 2078) return "C";
            if (GBCode >= 2078 && GBCode < 2274) return "D";
            if (GBCode >= 2274 && GBCode < 2302) return "E";
            if (GBCode >= 2302 && GBCode < 2433) return "F";
            if (GBCode >= 2433 && GBCode < 2594) return "G";
            if (GBCode >= 2594 && GBCode < 2787) return "H";
            if (GBCode >= 2787 && GBCode < 3106) return "J";
            if (GBCode >= 3106 && GBCode < 3212) return "K";
            if (GBCode >= 3212 && GBCode < 3472) return "L";
            if (GBCode >= 3472 && GBCode < 3635) return "M";
            if (GBCode >= 3635 && GBCode < 3722) return "N";
            if (GBCode >= 3722 && GBCode < 3730) return "O";
            if (GBCode >= 3730 && GBCode < 3858) return "P";
            if (GBCode >= 3858 && GBCode < 4027) return "Q";
            if (GBCode >= 4027 && GBCode < 4086) return "R";
            if (GBCode >= 4086 && GBCode < 4390) return "S";
            if (GBCode >= 4390 && GBCode < 4558) return "T";
            if (GBCode >= 4558 && GBCode < 4684) return "W";
            if (GBCode >= 4684 && GBCode < 4925) return "X";
            if (GBCode >= 4925 && GBCode < 5249) return "Y";
            if (GBCode >= 5249 && GBCode <= 5589) return "Z";
            if (GBCode >= 5601 && GBCode <= 8794)
            {
                String CodeData = "cjwgnspgcenegypbtwxzdxykygtpjnmjqmbsgzscyjsyyfpggbzgydywjkgaljswkbjqhyjwpdzlsgmr"
                 + "ybywwccgznkydgttngjeyekzydcjnmcylqlypyqbqrpzslwbdgkjfyxjwcltbncxjjjjcxdtqsqzycdxxhgckbphffss"
                 + "pybgmxjbbyglbhlssmzmpjhsojnghdzcdklgjhsgqzhxqgkezzwymcscjnyetxadzpmdssmzjjqjyzcjjfwqjbdzbjgd"
                 + "nzcbwhgxhqkmwfbpbqdtjjzkqhylcgxfptyjyyzpsjlfchmqshgmmxsxjpkdcmbbqbefsjwhwwgckpylqbgldlcctnma"
                 + "eddksjngkcsgxlhzaybdbtsdkdylhgymylcxpycjndqjwxqxfyyfjlejbzrwccqhqcsbzkymgplbmcrqcflnymyqmsqt"
                 + "rbcjthztqfrxchxmcjcjlxqgjmshzkbswxemdlckfsydsglycjjssjnqbjctyhbftdcyjdgwyghqfrxwckqkxebpdjpx"
                 + "jqsrmebwgjlbjslyysmdxlclqkxlhtjrjjmbjhxhwywcbhtrxxglhjhfbmgykldyxzpplggpmtcbbajjzyljtyanjgbj"
                 + "flqgdzyqcaxbkclecjsznslyzhlxlzcghbxzhznytdsbcjkdlzayffydlabbgqszkggldndnyskjshdlxxbcghxyggdj"
                 + "mmzngmmccgwzszxsjbznmlzdthcqydbdllscddnlkjyhjsycjlkohqasdhnhcsgaehdaashtcplcpqybsdmpjlpcjaql"
                 + "cdhjjasprchngjnlhlyyqyhwzpnccgwwmzffjqqqqxxaclbhkdjxdgmmydjxzllsygxgkjrywzwyclzmcsjzldbndcfc"
                 + "xyhlschycjqppqagmnyxpfrkssbjlyxyjjglnscmhcwwmnzjjlhmhchsyppttxrycsxbyhcsmxjsxnbwgpxxtaybgajc"
                 + "xlypdccwqocwkccsbnhcpdyznbcyytyckskybsqkkytqqxfcwchcwkelcqbsqyjqcclmthsywhmktlkjlychwheqjhtj"
                 + "hppqpqscfymmcmgbmhglgsllysdllljpchmjhwljcyhzjxhdxjlhxrswlwzjcbxmhzqxsdzpmgfcsglsdymjshxpjxom"
                 + "yqknmyblrthbcftpmgyxlchlhlzylxgsssscclsldclepbhshxyyfhbmgdfycnjqwlqhjjcywjztejjdhfblqxtqkwhd"
                 + "chqxagtlxljxmsljhdzkzjecxjcjnmbbjcsfywkbjzghysdcpqyrsljpclpwxsdwejbjcbcnaytmgmbapclyqbclzxcb"
                 + "nmsggfnzjjbzsfqyndxhpcqkzczwalsbccjxpozgwkybsgxfcfcdkhjbstlqfsgdslqwzkxtmhsbgzhjcrglyjbpmljs"
                 + "xlcjqqhzmjczydjwbmjklddpmjegxyhylxhlqyqhkycwcjmyhxnatjhyccxzpcqlbzwwwtwbqcmlbmynjcccxbbsnzzl"
                 + "jpljxyztzlgcldcklyrzzgqtgjhhgjljaxfgfjzslcfdqzlclgjdjcsnclljpjqdcclcjxmyzftsxgcgsbrzxjqqcczh"
                 + "gyjdjqqlzxjyldlbcyamcstylbdjbyregklzdzhldszchznwczcllwjqjjjkdgjcolbbzppglghtgzcygezmycnqcycy"
                 + "hbhgxkamtxyxnbskyzzgjzlqjdfcjxdygjqjjpmgwgjjjpkjsbgbmmcjssclpqpdxcdyykypcjddyygywchjrtgcnyql"
                 + "dkljczzgzccjgdyksgpzmdlcphnjafyzdjcnmwescsglbtzcgmsdllyxqsxsbljsbbsgghfjlwpmzjnlyywdqshzxtyy"
                 + "whmcyhywdbxbtlmswyyfsbjcbdxxlhjhfpsxzqhfzmqcztqcxzxrdkdjhnnyzqqfnqdmmgnydxmjgdhcdycbffallztd"
                 + "ltfkmxqzdngeqdbdczjdxbzgsqqddjcmbkxffxmkdmcsychzcmljdjynhprsjmkmpcklgdbqtfzswtfgglyplljzhgjj"
                 + "gypzltcsmcnbtjbhfkdhbyzgkpbbymtdlsxsbnpdkleycjnycdykzddhqgsdzsctarlltkzlgecllkjljjaqnbdggghf"
                 + "jtzqjsecshalqfmmgjnlyjbbtmlycxdcjpldlpcqdhsycbzsckbzmsljflhrbjsnbrgjhxpdgdjybzgdlgcsezgxlblg"
                 + "yxtwmabchecmwyjyzlljjshlgndjlslygkdzpzxjyyzlpcxszfgwyydlyhcljscmbjhblyjlycblydpdqysxktbytdkd"
                 + "xjypcnrjmfdjgklccjbctbjddbblblcdqrppxjcglzcshltoljnmdddlngkaqakgjgyhheznmshrphqqjchgmfprxcjg"
                 + "dychghlyrzqlcngjnzsqdkqjymszswlcfqjqxgbggxmdjwlmcrnfkkfsyyljbmqammmycctbshcptxxzzsmphfshmclm"
                 + "ldjfyqxsdyjdjjzzhqpdszglssjbckbxyqzjsgpsxjzqznqtbdkwxjkhhgflbcsmdldgdzdblzkycqnncsybzbfglzzx"
                 + "swmsccmqnjqsbdqsjtxxmbldxcclzshzcxrqjgjylxzfjphymzqqydfqjjlcznzjcdgzygcdxmzysctlkphtxhtlbjxj"
                 + "lxscdqccbbqjfqzfsltjbtkqbsxjjljchczdbzjdczjccprnlqcgpfczlclcxzdmxmphgsgzgszzqjxlwtjpfsyaslcj"
                 + "btckwcwmytcsjjljcqlwzmalbxyfbpnlschtgjwejjxxglljstgshjqlzfkcgnndszfdeqfhbsaqdgylbxmmygszldyd"
                 + "jmjjrgbjgkgdhgkblgkbdmbylxwcxyttybkmrjjzxqjbhlmhmjjzmqasldcyxyqdlqcafywyxqhz";
                String _gbcode = GBCode.ToString();
                int pos = (Convert.ToInt16(_gbcode.Substring(0, 2)) - 56) * 94 + Convert.ToInt16(_gbcode.Substring(_gbcode.Length - 2, 2));
                return CodeData.Substring(pos - 1, 1);
            }
            return " ";
        }

        public static string GetRealIP()
        {
            string ip;
            try
            {
                HttpRequest request = HttpContext.Current.Request;

                if (request.ServerVariables["HTTP_VIA"] != null)
                {
                    ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString().Split(',')[0].Trim();
                }
                else
                {
                    ip = request.UserHostAddress;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ip;
        }

        /// <summary>
        /// 返回字符串的实际长度（中文占2，英文或数字占1）

        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int GetStringRealLength(string text)
        {
            int length = 0;

            for (int i = 0; i < text.Length; i++)
            {
                byte[] bytes = Encoding.Default.GetBytes(text.Substring(i, 1));
                if (bytes.Length > 1)
                {
                    //如果长度大于1，是中文，占两个字节，+2
                    length += 2;
                }
                else
                {
                    //如果长度等于1，是英文，占一个字节，+1
                    length += 1;
                }
            }

            return length;
        }


        /// <summary>
        /// 执行带有clob,blob,nclob大对象参数类型的存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tempbuff"></param>
        public static int RunAddProcedure(bool isToCenter, string storedProcName, IDataParameter[] parameters, byte[] tempbuff)
        {
            string constring = ConfigurationManager.ConnectionStrings["Oracle"].ConnectionString;

            if (isToCenter)
            {
                constring = ConfigurationManager.ConnectionStrings["OracleCenter"].ConnectionString;
            }
            OracleConnection Connection = new OracleConnection(constring);

            Connection.Open();
            OracleTransaction tx = Connection.BeginTransaction();
            OracleCommand cmd = Connection.CreateCommand();
            cmd.Transaction = tx;
            string type = " declare ";
            type = type + " xx  Clob;";
            string createtemp = type + " begin ";
            createtemp = createtemp + " dbms_lob.createtemporary(xx, false, 0); ";
            string setvalue = "";
            setvalue = setvalue + ":templob := xx;";
            cmd.CommandText = createtemp + setvalue + " end;";
            cmd.Parameters.Add(new OracleParameter("templob", OracleType.Clob)).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();

            OracleLob tempLob = (OracleLob)cmd.Parameters["templob"].Value;
            tempLob.BeginBatch(OracleLobOpenMode.ReadWrite);
            int abc = tempbuff.Length;

            double b = abc / 2;
            double a = Math.Ceiling(b);
            abc = (int)(a * 2);
            tempLob.Write(tempbuff, 0, abc);
            tempLob.EndBatch();
            parameters[0].Value = tempLob;

            cmd.Parameters.Clear();
            cmd.CommandText = storedProcName;
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (OracleParameter parameter in parameters)
            {
                cmd.Parameters.Add(parameter);
            }
            cmd.ExecuteNonQuery();
            int id = 0;
            if (cmd.Parameters.Count > 2)
            {
                id = Convert.ToInt32(cmd.Parameters[1].Value);
            }
            tx.Commit();
            Connection.Close();
            return id;
        }

        public static int RunAddProcedureBlob(bool isToCenter, string storedProcName, IDataParameter[] parameters, byte[] tempbuff)
        {
            string constring = ConfigurationManager.ConnectionStrings["Oracle"].ConnectionString;

            if (isToCenter)
            {
                constring = ConfigurationManager.ConnectionStrings["OracleCenter"].ConnectionString;
            }
            OracleConnection Connection = new OracleConnection(constring);

            Connection.Open();
            OracleTransaction tx = Connection.BeginTransaction();
            OracleCommand cmd = Connection.CreateCommand();
            cmd.Transaction = tx;
            string type = " declare ";
            type = type + " xx  Blob;";
            string createtemp = type + " begin ";
            createtemp = createtemp + " dbms_lob.createtemporary(xx, false, 0); ";
            string setvalue = "";
            setvalue = setvalue + ":templob := xx;";
            cmd.CommandText = createtemp + setvalue + " end;";
            cmd.Parameters.Add(new OracleParameter("templob", OracleType.Blob)).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();

            OracleLob tempLob = (OracleLob)cmd.Parameters["templob"].Value;
            tempLob.BeginBatch(OracleLobOpenMode.ReadWrite);
            int abc = tempbuff.Length;

            double b = abc / 2;
            double a = Math.Ceiling(b);
            abc = (int)(a * 2);
            tempLob.Write(tempbuff, 0, abc);
            tempLob.EndBatch();
            parameters[0].Value = tempLob;

            cmd.Parameters.Clear();
            cmd.CommandText = storedProcName;
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (OracleParameter parameter in parameters)
            {
                cmd.Parameters.Add(parameter);
            }
            cmd.ExecuteNonQuery();
            int id = 0;
            if (cmd.Parameters.Count > 2)
            {
                id = Convert.ToInt32(cmd.Parameters[1].Value);
            }
            tx.Commit();
            Connection.Close();
            return id;
        }

        public static DataSet GetPhotoDateSet(string employeeId)
        {
            string sql;
            DataSet ds = new DataSet();
            OracleAccess oa = new OracleAccess();
            OracleAccess oaCenter = new OracleAccess(ConfigurationManager.ConnectionStrings["OracleCenter"].ConnectionString);

            //路局
            if (PrjPub.IsServerCenter)
            {
                sql = "select Photo from Employee_Photo where Employee_ID = " + employeeId;
                ds = oa.RunSqlDataSet(sql);
            }
            else
            {
                //站段先查本地
                sql = "select Photo from Employee_Photo where Employee_ID = " + employeeId;
                ds = oa.RunSqlDataSet(sql);

                bool isConnect = false;
                //如果本地无该员工照片信息，需连路局查询
                if (ds.Tables[0].Rows.Count == 0)
                {
                    isConnect = true;
                }
                else
                {
                    //如果本地该员工照片为空，需连路局查询
                    if (ds.Tables[0].Rows[0][0] == DBNull.Value)
                    {
                        isConnect = true;
                    }
                }

                if (isConnect)
                {
                    try
                    {
                        //连接路局查询员工照片信息
                        sql = "select Photo from Employee_Photo where Employee_ID = " + employeeId;
                        ds = oaCenter.RunSqlDataSet(sql);
                    }
                    catch
                    {
                        //连接路局出错，给空相片
                        sql = "select null as Photo from dual ";
                        ds = oa.RunSqlDataSet(sql);
                    }
                }
            }
            return ds;
        }

        /// <summary>
        /// 数据组数据重新排序
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static string[] RandomSort(string[] arr)
        {
            int k = arr.Length - 1; // 要保存的位置 
            Random rad = new Random();
            for (int i = 0; i < arr.Length - 1; i++) // 执行N-1次循环，随机产生要被打乱的数据所在的位置 
            {
                int idx = rad.Next(0, k + 1);
                // 交换数据 
                string n = arr[idx];
                arr[idx] = arr[k - 1];
                arr[k - 1] = n;
                // 递减要保存的位置 
                k--;
            }
            return arr;

            #region 原创方法
            //int k = arr.Length;
            //Random rad = new Random();
            //string[] arrnew = new string[k];

            //int idx=0;
            //ArrayList objList = new ArrayList();
            //for(int i=0;i<arr.Length; i++)
            //{
            //    if(objList.Count == 0)
            //    {
            //        idx = rad.Next(0, k);
            //        objList.Add(idx);
            //    }
            //    else
            //    {
            //        while (objList.Contains(idx))
            //        {
            //            idx = rad.Next(0, k);
            //        }

            //        objList.Add(idx);
            //    }
            //    arrnew[i] = arr[idx];
            //}

            //return arrnew;
            #endregion
        }


        public static void GetNowAnswer(RandomExamItem item, out string nowSelectAnswer, out string nowStandardAnswer)
        {
            //将原选项与原标准答案拼接成一个数组
            string[] strSelectAnswer = item.SelectAnswer.Split('|');
            string[] selectAnswers = new string[strSelectAnswer.Length];
            for (int x = 0; x < strSelectAnswer.Length; x++)
            {
                if (("|" + item.StandardAnswer + "|").IndexOf("|" + x + "|") >= 0)
                {
                    selectAnswers[x] = strSelectAnswer[x] + "|" + "true";
                }
                else
                {
                    selectAnswers[x] = strSelectAnswer[x] + "|" + "false";
                }
            }

            //将拼接的数组随机排序
            string[] selectAnswersNew = new string[strSelectAnswer.Length];
            selectAnswersNew = Pub.RandomSort(selectAnswers);

            //循环随机排序之后的数组获取当前的选项顺序 和答案
            nowSelectAnswer = string.Empty;
            nowStandardAnswer = string.Empty;
            for (int x = 0; x < selectAnswersNew.Length; x++)
            {
                string[] strNow = selectAnswersNew[x].Split('|');

                if (strNow[1] == "false")
                {
                    nowSelectAnswer += nowSelectAnswer == string.Empty ? strNow[0] : "|" + strNow[0];
                }
                else
                {
                    nowSelectAnswer += nowSelectAnswer == string.Empty ? strNow[0] : "|" + strNow[0];
                    nowStandardAnswer += nowStandardAnswer == string.Empty ? x.ToString() : "|" + x;
                }
            }
        }

        public static bool HasPaper(int examid)
        {
            string sql;
            DataSet ds = new DataSet();
            OracleAccess oa = new OracleAccess();

            //路局
            sql = "select * from Random_Exam_Computer_Server where Has_Paper=1 and Random_Exam_ID = " + examid;
            ds = oa.RunSqlDataSet(sql);

            if(ds.Tables[0].Rows.Count>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void AddFtp()
        {
            NetworkCredential credential = new NetworkCredential(GetXml(1), GetXml(2));
            FtpClient.AddSite(GetXml(0), credential, Convert.ToBoolean(GetXml(3)));
        }

        private static string GetXml(int n)
        {
            ArrayList objList = new ArrayList();
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(HttpContext.Current.Server.MapPath("/RailExamBao/Common/Class/FtpServer.xml"));
                XmlNodeList nodelist = xmldoc.SelectNodes("//SiteList/*");
                foreach (XmlNode nl in nodelist)
                {
                    objList.Add(nl.InnerText);
                }
                return objList[n].ToString();
            }
            catch
            {
                return "";
            }
        }

        public static void SavePhotoToLocal(int examid, int employeeid, byte[] ph, int index, int serverId)
        {
            string file = HttpContext.Current.Server.MapPath("/RailExamBao/Online/Photo/" + examid + "/");
            if (!Directory.Exists(file))
            {
                Directory.CreateDirectory(file);
            }

            string uploadPath = HttpContext.Current.Server.MapPath("/RailExamBao/Online/Photo/" + examid + "/") + employeeid + "_" + serverId + "_";

            string fileName = string.Empty;
            fileName = uploadPath + "0" + index + ".jpg";
            System.Drawing.Image image = FromBytes(ph);

            if (image != null)
            {
                System.Drawing.Image thumbnail = image.GetThumbnailImage(170, 130, null, IntPtr.Zero);
                //保存本地
                thumbnail.Save(fileName, ImageFormat.Jpeg);
            }
        }

        private static System.Drawing.Image FromBytes(byte[] bs)
        {
            if (bs == null) return null;
            try
            {
                MemoryStream ms = new MemoryStream(bs);
                System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                ms.Close();
                return returnImage;

            }
            catch { return null; }
        }
    }
}

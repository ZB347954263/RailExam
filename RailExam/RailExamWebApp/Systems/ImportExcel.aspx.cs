using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.UI;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.BLL;
using RailExam.Model;
using System.Collections.Generic;
using RailExamWebApp.Common.Class;
using System.Data.Common;

namespace RailExamWebApp.Systems
{
	public partial class ImportExcel : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				if (string.IsNullOrEmpty(Request.QueryString.Get("ImportType")))
				{
                    ImportEmployeeExcel();
				}
			}
        }

        #region 简单导入
        private  void ImportEmployeeOther()
        {
            EmployeeBLL objEmployeeBll = new EmployeeBLL();
            EmployeeErrorBLL objErrorBll = new EmployeeErrorBLL();
            IList<EmployeeError> objErrorList = new List<EmployeeError>();

            string orgID = Request.QueryString.Get("OrgID");
            OrganizationBLL orgBll = new OrganizationBLL();
            RailExam.Model.Organization org = orgBll.GetOrganization(Convert.ToInt32(orgID));
            string strUnitName = org.ShortName;
            string strFileName = Server.UrlDecode(Request.QueryString.Get("FileName"));
            string jsBlock;
            string strPath = Server.MapPath("/RailExamBao/Excel/" + strFileName);
            bool isClose = false;
            string strMessage;

            objErrorBll.DeleteEmployeeErrorByOrgIDAndImportTypeID(Convert.ToInt32(orgID));

            Excel.Application objApp = null;
            Excel._Workbook objBook = null;
            Excel.Workbooks objBooks = null;
            Excel.Sheets objSheets = null;
            Excel._Worksheet objSheet = null;
            Excel.Range range = null;
            DataSet ds = new DataSet();


            #region 读取Excel文件

            try
            {
                //生成ExcelApp   
                objApp = new Excel.Application();
                //Excel不显示   
                objApp.Visible = false;
                //生成Books   
                objBooks = objApp.Workbooks;
                //打开Excel文件   
                objBooks.Open(strPath,
                              Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                              Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                              Type.Missing, Type.Missing, Type.Missing,
                              Type.Missing, Type.Missing, Type.Missing);
                //取得Book   
                objBook = objBooks.get_Item(1);
                //取得Sheets   
                objSheets = objBook.Worksheets;
                //取得Sheet   
                objSheet = (Excel._Worksheet)objSheets.get_Item(1);
                //取得Range   

                int rowNum = objSheet.UsedRange.Rows.Count;
                int colNum = objSheet.UsedRange.Columns.Count;

                // 根据 ProgressBar.htm 显示进度条界面
                string templateFileName = Server.MapPath("/RailExamBao/RandomExam/ProgressBar.htm");
                StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
                string html = reader.ReadToEnd();
                reader.Close();
                Response.Write(html);
                Response.Flush();
                System.Threading.Thread.Sleep(200);

                DataTable dtItem = new DataTable();
                Hashtable htCol = new Hashtable();
                for (int i = 1; i <= colNum; i++)
                {
                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[1, i], objSheet.Cells[1, i]));
                    DataColumn dc = dtItem.Columns.Add(range.Value2.ToString());
                    htCol[range.Value2.ToString()] = i;
                }

                DataRow newRow = null;
                for (int i = 2; i <= rowNum; i++)
                {
                    newRow = dtItem.NewRow();

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["序号"]], objSheet.Cells[i, htCol["序号"]]));
                    newRow["序号"] = range.Value2;
                    dtItem.Rows.Add(newRow);

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["员工ID"]], objSheet.Cells[i, htCol["员工ID"]]));
                    newRow["员工ID"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["站段名称"]], objSheet.Cells[i, htCol["站段名称"]]));
                    newRow["站段名称"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["车间"]], objSheet.Cells[i, htCol["车间"]]));
                    newRow["车间"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["班组"]], objSheet.Cells[i, htCol["班组"]]));
                    string str;
                    try
                    {
                        str = range.Value2.ToString().Replace("\0", "");
                        newRow["班组"] = str;
                    }
                    catch
                    {
                        newRow["班组"] = range.Value2;
                    }

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["姓名"]], objSheet.Cells[i, htCol["姓名"]]));
                    try
                    {
                        str = range.Value2.ToString().Replace("\0", "");
                        newRow["姓名"] = str;
                    }
                    catch
                    {
                        newRow["姓名"] = range.Value2;
                    }


                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["工作证号"]], objSheet.Cells[i, htCol["工作证号"]]));
                    newRow["工作证号"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["性别"]], objSheet.Cells[i, htCol["性别"]]));
                    try
                    {
                        str = range.Value2.ToString().Replace("\0", "");
                        newRow["性别"] = str;
                    }
                    catch
                    {
                        newRow["性别"] = range.Value2;
                    }


                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["职名"]], objSheet.Cells[i, htCol["职名"]]));
                    newRow["职名"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["是否班组长"]], objSheet.Cells[i, htCol["是否班组长"]]));
                    newRow["是否班组长"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["技能等级"]], objSheet.Cells[i, htCol["技能等级"]]));
                    newRow["技能等级"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["人员岗位状态"]], objSheet.Cells[i, htCol["人员岗位状态"]]));
                    newRow["人员岗位状态"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["岗位培训合格证编号"]], objSheet.Cells[i, htCol["岗位培训合格证编号"]]));
                    try
                    {
                        str = range.Value2.ToString().Replace("?", "");
                        newRow["岗位培训合格证编号"] = str;
                    }
                    catch
                    {
                        newRow["岗位培训合格证编号"] = range.Value2;
                    }

                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('正在读取Excel文件','" + ((double)((i - 1) * 100) / (double)rowNum).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                }

                ds.Tables.Add(dtItem);

                // 处理完成
                jsBlock = "<script>SetCompleted('Excel数据读取完毕'); </script>";
                Response.Write(jsBlock);
                Response.Flush();
            }
            catch
            {
                isClose = true;
            }
            finally
            {
                objBook.Close(Type.Missing, strPath, Type.Missing);
                objBooks.Close();
                objApp.Application.Workbooks.Close();
                objApp.Application.Quit();
                objApp.Quit();
                GC.Collect();
            }

            if (isClose)
            {
                if (File.Exists(strPath))
                    File.Delete(strPath);
                Response.Write("<script>window.returnValue='请检查Excel文件格式',window.close();</script>");
                return;
            }

            #endregion

            #region 检验数据

            System.Threading.Thread.Sleep(10);
            jsBlock = "<script>SetPorgressBar('正准备检测Excel数据','0.00'); </script>";
            Response.Write(jsBlock);
            Response.Flush();

            if (ds.Tables[0].Rows.Count == 0)
            {
                Response.Write("<script>window.returnValue='Excel中没有任何记录，请核对',window.close();</script>");
                return;
            }


            Hashtable htOrg = GetOrgInfo();
            Hashtable htPost = GetPostInfo();

            Hashtable htSkillLevel = GetSkillLevel();

            Hashtable htShopNeedAdd = new Hashtable();
            Hashtable htEmployeeName = new Hashtable(); //为检测Excel中姓名是否重复
            Hashtable htWorkNo = new Hashtable(); //为检测Excel中工作证书编号是否重复
            Hashtable htPostNo = new Hashtable(); //为检测Excel中员工编码是否重复
            Hashtable htPostNeedAdd = new Hashtable();

            PostBLL objPostBll = new PostBLL();
            IList<RailExam.Model.Employee> objEmployeeInsert = new List<RailExam.Model.Employee>();
            IList<RailExam.Model.Employee> objEmployeeUpdate = new List<RailExam.Model.Employee>();
            try
            {
                int index = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('正在检测Excel数据','" +
                              ((double)(index * 100) / (double)ds.Tables[0].Rows.Count).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                    index = index + 1;

                    RailExam.Model.Employee objEmployee = new RailExam.Model.Employee();

                    bool isUpdate = false;
                    if (dr["员工ID"].ToString().Trim() != string.Empty)
                    {
                        IList<RailExam.Model.Employee> objOldList =
                            objEmployeeBll.GetEmployeeByWhereClause("Employee_ID=" + dr["员工ID"].ToString().Trim());
                        if (objOldList.Count > 0)
                        {
                            isUpdate = true;
                        }
                        else
                        {
                            AddError(objErrorList, dr, "该员工ID在系统中不存在");
                            continue;
                        }
                    }

                    //站段名称
                    if (dr["站段名称"].ToString().Trim() != strUnitName)
                    {
                        AddError(objErrorList, dr, "站段名称填写错误");
                        continue;
                    }

                    if (dr["车间"].ToString().Trim() == "")
                    {
                        AddError(objErrorList, dr, "车间不能为空");
                        continue;
                    }

                    //组织机构
                    string strOrg;
                    if (dr["班组"].ToString().Trim() == string.Empty)
                    {
                        strOrg = dr["站段名称"].ToString().Trim() + "-" + dr["车间"].ToString().Trim();
                    }
                    else
                    {
                        strOrg = dr["站段名称"].ToString().Trim() + "-" + dr["车间"].ToString().Trim() + "-" + dr["班组"].ToString().Trim();
                    }

                    if (!htOrg.ContainsKey(strOrg))
                    {
                        if (dr["班组"].ToString().Trim() == string.Empty)
                        {
                            if (!htShopNeedAdd.ContainsKey(dr["车间"].ToString().Trim()))
                            {
                                htShopNeedAdd.Add(dr["车间"].ToString().Trim(), new Hashtable());
                            }

                            //如果组织机构需要新增
                            objEmployee.Memo = strOrg;
                        }
                        else
                        {
                            if (!htShopNeedAdd.ContainsKey(dr["车间"].ToString().Trim()))
                            {
                                htShopNeedAdd.Add(dr["车间"].ToString().Trim(), new Hashtable());
                            }

                            Hashtable htGroupNeedAdd = (Hashtable)htShopNeedAdd[dr["车间"].ToString().Trim()];
                            if (!htGroupNeedAdd.ContainsKey(dr["班组"].ToString().Trim()))
                            {
                                htGroupNeedAdd.Add(dr["班组"].ToString().Trim(), dr["班组"].ToString().Trim());
                                htShopNeedAdd[dr["车间"].ToString().Trim()] = htGroupNeedAdd;
                            }

                            //如果组织机构需要新增
                            objEmployee.Memo = strOrg;
                        }
                    }
                    else
                    {
                        objEmployee.OrgID = Convert.ToInt32(htOrg[strOrg]);
                        objEmployee.Memo = string.Empty;
                    }

                    //姓名不能为空
                    if (dr["姓名"].ToString().Trim() == string.Empty)
                    {
                        AddError(objErrorList, dr, "员工姓名不能为空");
                        continue;
                    }
                    else
                    {
                        if (dr["姓名"].ToString().Trim().Length > 20)
                        {
                            AddError(objErrorList, dr, "员工姓名不能超过20位");
                            continue;
                        }

                        //姓名在Excel中不能重复

                        if (htEmployeeName.ContainsKey(dr["姓名"].ToString().Trim()))
                        {
                            AddError(objErrorList, dr, "员工姓名在Excel中与序号为" + htEmployeeName[dr["姓名"].ToString()] + "的员工姓名重复");
                            continue;
                        }
                        else
                        {
                            htEmployeeName.Add(dr["姓名"].ToString().Trim(), dr["序号"].ToString().Trim());
                        }

                        objEmployee.EmployeeName = dr["姓名"].ToString().Trim();
                        objEmployee.PinYinCode = Pub.GetChineseSpell(dr["姓名"].ToString().Trim());
                    }

                    //工作证号
                    if (dr["工作证号"].ToString().Trim() != string.Empty)
                    {
                        if (dr["工作证号"].ToString().Trim().Length > 14)
                        {
                            AddError(objErrorList, dr, "工作证号不能超过14位");
                            continue;
                        }

                        //工作证号在Excel中不能重复
                        if (htWorkNo.ContainsKey(dr["工作证号"].ToString().Trim()))
                        {
                            AddError(objErrorList, dr, "工作证号在Excel中与序号为" + htWorkNo[dr["工作证号"].ToString()] + "的工作证号重复");
                            continue;
                        }
                        else
                        {
                            htWorkNo.Add(dr["工作证号"].ToString().Trim(), dr["序号"].ToString().Trim());
                        }

                        objEmployee.PostNo = dr["工作证号"].ToString().Trim();
                    }
                    else
                    {
                        objEmployee.PostNo = "";
                    }

                    //性别
                    if (dr["性别"].ToString().Trim() != "男" && dr["性别"].ToString().Trim() != "女")
                    {
                        AddError(objErrorList, dr, "性别必须为男或女");
                        continue;
                    }
                    else
                    {
                        objEmployee.Sex = dr["性别"].ToString().Trim();
                    }

                
                    //职名
                    if (dr["职名"].ToString().Trim() == string.Empty)
                    {
                        AddError(objErrorList, dr, "职名不能为空！");
                        continue;
                    }
                    else
                    {
                        IList<RailExam.Model.Post> objPost =
                            objPostBll.GetPostsByWhereClause("Post_Level=3 and Post_Name='" + dr["职名"].ToString().Trim() + "'");
                        if (objPost.Count == 0)
                        {
                            AddError(objErrorList, dr, "职名在系统中不存在！");
                            continue;
                        }

                        objEmployee.PostID = Convert.ToInt32(htPost[dr["职名"].ToString().Trim()]);
                    }



                    //班组长类型
                    if (dr["是否班组长"].ToString().Trim() == string.Empty || dr["是否班组长"].ToString().Trim()=="否")
                    {
                        objEmployee.IsGroupLeader = 0;
                    }
                    else
                    {
                        objEmployee.IsGroupLeader = 1;
                    }


                    //人员岗位状态
                    if (dr["人员岗位状态"].ToString().Trim().IndexOf("在岗") < 0)
                    {
                        objEmployee.IsOnPost = false;
                    }
                    else
                    {
                        objEmployee.IsOnPost = true;
                    }

                    //岗位培训合格证编号

                    if (dr["岗位培训合格证编号"].ToString().Trim() != string.Empty)
                    {
                        if (dr["岗位培训合格证编号"].ToString().Trim().Length > 20)
                        {
                            AddError(objErrorList, dr, "岗位培训合格证编号不能超过20位");
                            continue;
                        }

                        //工作证号在Excel中不能重复

                        if (htPostNo.ContainsKey(dr["岗位培训合格证编号"].ToString().Trim()))
                        {
                            AddError(objErrorList, dr, "岗位培训合格证编号在Excel中与序号为" + htPostNo[dr["岗位培训合格证编号"].ToString().Trim()] + "的岗位培训合格证编号重复");
                            continue;
                        }
                        else
                        {
                            htPostNo.Add(dr["岗位培训合格证编号"].ToString().Trim(), dr["序号"].ToString().Trim());
                        }

                        IList<RailExam.Model.Employee> objView;

                        if(!isUpdate)
                        {
                            objView =
                                objEmployeeBll.GetEmployeeByWhereClause("Work_No='" + dr["岗位培训合格证编号"].ToString().Trim() + "'");
                        }
                        else
                        {
                            objView =
                                objEmployeeBll.GetEmployeeByWhereClause("Employee_ID<>" + dr["员工ID"] + " and Work_No='" + dr["岗位培训合格证编号"].ToString().Trim() + "'");
                        }

                        if (objView.Count > 0)
                        {
                            AddError(objErrorList, dr, "岗位培训合格证编号已在系统中存在");
                            continue;
                        }

                        objEmployee.WorkNo = dr["岗位培训合格证编号"].ToString().Trim();
                    }
                    else
                    {
                        objEmployee.WorkNo = string.Empty;
                        AddError(objErrorList, dr, "岗位培训合格证编号不能为空！如果确实没有，请输入工作证号！");
                        continue;
                    }

                    //技能等级
					if (dr["技能等级"].ToString().Trim() != string.Empty)
					{
						if (!htSkillLevel.ContainsKey(dr["技能等级"].ToString().Trim()))
						{
							AddError(objErrorList, dr, "技能等级在系统中不存在！");
							continue;
						}
						else
						{
							objEmployee.TechnicianTypeID = Convert.ToInt32(htSkillLevel[dr["技能等级"].ToString().Trim()]);
						}
					}
					else
					{
						objEmployee.TechnicianTypeID = 1;
					}


                    if (dr["员工ID"].ToString().Trim() == string.Empty)
                    {
                        objEmployeeInsert.Add(objEmployee);
                    }
                    else
                    {
                        try
                        {
                            int employeeID = Convert.ToInt32(dr["员工ID"].ToString().Trim());
                            objEmployee.EmployeeID = employeeID;
                            objEmployeeUpdate.Add(objEmployee);

                        }
                        catch
                        {
                            AddError(objErrorList, dr, "员工ID填写错误！");
                            continue;
                        }
                    }
                }

                // 处理完成
                jsBlock = "<script>SetCompleted('Excel数据检测完毕'); </script>";
                Response.Write(jsBlock);
                Response.Flush();
            }
            catch (Exception ex)
            {
                Response.Write(EnhancedStackTrace(ex));
            }

            #endregion

            if (objErrorList.Count > 0)
            {
                jsBlock = "<script>SetCompleted('正在统计不符合要求的数据，请等待......'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                objErrorBll.AddEmployeeError(objErrorList);

                if (File.Exists(strPath))
                    File.Delete(strPath);
                Response.Write("<script>window.returnValue='refresh|请检查Excel数据',window.close();</script>");
                return;
            }


            if (!string.IsNullOrEmpty(Request.QueryString.Get("mode")))
            {
                Response.Write("<script>window.returnValue='refresh|数据检查成功',window.close();</script>");
                return;
            }

            #region 导入数据

            System.Threading.Thread.Sleep(10);
            jsBlock = "<script>SetPorgressBar('正在导入部门名称、班组','0.00'); </script>";
            Response.Write(jsBlock);
            Response.Flush();

            Database db = DatabaseFactory.CreateDatabase();

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                Hashtable htWorkshop = GetWorkShop(db, transaction);
                int count = 1;
                foreach (System.Collections.DictionaryEntry obj in htShopNeedAdd)
                {
                    int nWorkShopID;
                    if (!htWorkshop.ContainsKey(obj.Key.ToString()))
                    {
                        RailExam.Model.Organization objshop = new RailExam.Model.Organization();
                        objshop.FullName = obj.Key.ToString();
                        objshop.ShortName = obj.Key.ToString();
                        objshop.ParentId = Convert.ToInt32(orgID);
                        objshop.Memo = "";
                        nWorkShopID = orgBll.AddOrganization(db, transaction, objshop);
                    }
                    else
                    {
                        nWorkShopID = Convert.ToInt32(htWorkshop[obj.Key.ToString()]);
                    }

                    Hashtable htGroup = (Hashtable)obj.Value;
                    if (htGroup.Count != 0)
                    {
                        foreach (System.Collections.DictionaryEntry objGroupNeedAdd in htGroup)
                        {
                            RailExam.Model.Organization objGroup = new RailExam.Model.Organization();
                            objGroup.FullName = objGroupNeedAdd.Key.ToString();
                            objGroup.ShortName = objGroupNeedAdd.Key.ToString();
                            objGroup.ParentId = nWorkShopID;
                            objGroup.Memo = "";
                            orgBll.AddOrganization(db, transaction, objGroup);
                        }
                    }

                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('正在导入部门名称、班组','" + ((double)(count * 100) / (double)(htShopNeedAdd.Count + objEmployeeInsert.Count)).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                    count = count + 1;
                }

                htWorkshop = GetWorkShop(db, transaction);
                Hashtable htNowOrg = GetOrgInfo(db, transaction);

                foreach (RailExam.Model.Employee objEmployee in objEmployeeInsert)
                {
                    if (objEmployee.Memo.ToString() != string.Empty)
                    {
                        if (objEmployee.Memo.Split('-').Length == 2)
                        {
                            objEmployee.OrgID = Convert.ToInt32(htWorkshop[objEmployee.Memo.Split('-')[1]]);
                        }
                        else
                        {
                            objEmployee.OrgID = Convert.ToInt32(htNowOrg[objEmployee.Memo.ToString()].ToString().Split('-')[0]);
                        }
                    }

                    if (objEmployee.OrgID == 0)
                    {
                        throw new Exception("aaaa");
                    }

                    objEmployee.Memo = "";

                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('正在导入部门名称、班组','" + ((double)(count * 100) / (double)(htShopNeedAdd.Count + objEmployeeInsert.Count + objEmployeeUpdate.Count)).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                    count = count + 1;
                }

                foreach (RailExam.Model.Employee objEmployee in objEmployeeUpdate)
                {
                    if (objEmployee.Memo.ToString() != string.Empty)
                    {
                        if (objEmployee.Memo.Split('-').Length == 2)
                        {
                            objEmployee.OrgID = Convert.ToInt32(htWorkshop[objEmployee.Memo.Split('-')[1]]);
                        }
                        else
                        {
                            objEmployee.OrgID = Convert.ToInt32(htNowOrg[objEmployee.Memo.ToString()].ToString().Split('-')[0]);
                        }
                    }

                    if (objEmployee.OrgID == 0)
                    {
                        throw new Exception("aaaa");
                    }

                    objEmployee.Memo = "";

                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('正在导入部门名称、班组','" + ((double)(count * 100) / (double)(htShopNeedAdd.Count + objEmployeeInsert.Count + objEmployeeUpdate.Count)).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                    count = count + 1;
                }

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('正在导入干部职名','0.00'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                count = 1;
                foreach (System.Collections.DictionaryEntry objPostNeed in htPostNeedAdd)
                {
                    RailExam.Model.Post objPost = new RailExam.Model.Post();
                    objPost.ParentId = 373;
                    objPost.PostName = objPostNeed.Key.ToString();
                    objPost.Technician = 0;
                    objPost.Promotion = 0;
                    objPost.Description = string.Empty;
                    objPost.Memo = string.Empty;
                    int postID = objPostBll.AddPost(db, transaction, objPost);

                    ArrayList objPostList = (ArrayList)objPostNeed.Value;
                    for (int i = 0; i < objPostList.Count; i++)
                    {
                        string[] strPost = objPostList[i].ToString().Split('|');
                        if (strPost[1] == "false")
                        {
                            objEmployeeInsert[Convert.ToInt32(strPost[0])].PostID = postID;
                        }
                        else
                        {
                            objEmployeeUpdate[Convert.ToInt32(strPost[0])].PostID = postID;
                        }
                    }

                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('正在导入干部职名','" + ((double)(count * 100) / (double)(htPostNeedAdd.Count)).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                    count = count + 1;
                }

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('正在导入员工信息','0.00'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                count = 1;
                for (int i = 0; i < objEmployeeInsert.Count; i++)
                {
                    int employeeid = objEmployeeBll.AddEmployee(db, transaction, objEmployeeInsert[i]);

                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('正在导入员工信息','" + ((double)(count * 100) / (double)(objEmployeeInsert.Count + objEmployeeUpdate.Count)).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                    count = count + 1;
                }


                for (int i = 0; i < objEmployeeUpdate.Count; i++)
                {
                    objEmployeeBll.UpdateEmployee(db, transaction, objEmployeeUpdate[i]);
                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('正在导入员工信息','" + ((double)(count * 100) / (double)(objEmployeeInsert.Count + objEmployeeUpdate.Count)).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                    count = count + 1;
                }

                strMessage = "导入成功!";
                transaction.Commit();
            }
            catch (Exception ex)
            {
                strMessage = "导入失败!";
                transaction.Rollback();
                //Response.Write(EnhancedStackTrace(ex));
            }
            finally
            {
                connection.Close();
            }

            if (File.Exists(strPath))
                File.Delete(strPath);
            Response.Write("<script>window.returnValue='refresh|" + strMessage + "';window.close();</script>");
            #endregion
        }
        #endregion

        #region 正常导入
        private void ImportEmployeeExcel()
		{
			EmployeeBLL objEmployeeBll = new EmployeeBLL();
			EmployeeDetailBLL objEmployeeDetailBll = new EmployeeDetailBLL();
			EmployeeErrorBLL objErrorBll = new EmployeeErrorBLL();
			IList<EmployeeError> objErrorList = new List<EmployeeError>();


			string orgID = Request.QueryString.Get("OrgID");
			OrganizationBLL orgBll = new OrganizationBLL();
			RailExam.Model.Organization org = orgBll.GetOrganization(Convert.ToInt32(orgID));
			string strUnitName = org.ShortName;
			string strFileName = Server.UrlDecode(Request.QueryString.Get("FileName"));
			string jsBlock;
			string strPath = Server.MapPath("/RailExamBao/Excel/" + strFileName);
			bool isClose = false;
			string strMessage;

			objErrorBll.DeleteEmployeeErrorByOrgIDAndImportTypeID(Convert.ToInt32(orgID));

			Excel.Application objApp = null;
			Excel._Workbook objBook = null;
			Excel.Workbooks objBooks = null;
			Excel.Sheets objSheets = null;
			Excel._Worksheet objSheet = null;
			Excel.Range range = null;
			DataSet ds = new DataSet();

			#region 读取Excel文件

			try
			{
				//生成ExcelApp   
				objApp = new Excel.Application();
				//Excel不显示   
				objApp.Visible = false;
				//生成Books   
				objBooks = objApp.Workbooks;
				//打开Excel文件   
				objBooks.Open(strPath,
							  Type.Missing, Type.Missing, Type.Missing, Type.Missing,
							  Type.Missing, Type.Missing, Type.Missing, Type.Missing,
							  Type.Missing, Type.Missing, Type.Missing,
							  Type.Missing, Type.Missing, Type.Missing);
				//取得Book   
				objBook = objBooks.get_Item(1);
				//取得Sheets   
				objSheets = objBook.Worksheets;
				//取得Sheet   
				objSheet = (Excel._Worksheet)objSheets.get_Item(1);
				//取得Range   

				int rowNum = objSheet.UsedRange.Rows.Count;
				int colNum = objSheet.UsedRange.Columns.Count;

				// 根据 ProgressBar.htm 显示进度条界面
				string templateFileName = Server.MapPath("/RailExamBao/RandomExam/ProgressBar.htm");
				StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
				string html = reader.ReadToEnd();
				reader.Close();
				Response.Write(html);
				Response.Flush();
				System.Threading.Thread.Sleep(200);

				DataTable dtItem = new DataTable();
				Hashtable htCol = new Hashtable();
				for (int i = 1; i <= colNum; i++)
				{
					range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[1, i], objSheet.Cells[1, i]));
					DataColumn dc = dtItem.Columns.Add(range.Value2.ToString());
					htCol[range.Value2.ToString()] = i;
				}

				DataRow newRow = null;
				for (int i = 2; i <= rowNum; i++)
				{
					newRow = dtItem.NewRow();

					range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["序号"]], objSheet.Cells[i, htCol["序号"]]));
					newRow["序号"] = range.Value2;
					dtItem.Rows.Add(newRow);

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["员工ID"]], objSheet.Cells[i, htCol["员工ID"]]));
                    newRow["员工ID"] = range.Value2;
					
                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["单位"]], objSheet.Cells[i, htCol["单位"]]));
					newRow["单位"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["车间"]], objSheet.Cells[i, htCol["车间"]]));
					newRow["车间"] = range.Value2;

					range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["班组"]], objSheet.Cells[i, htCol["班组"]]));
					string str;
					try
					{
						str = range.Value2.ToString().Replace("\0", "");
						newRow["班组"] = str;
					}
					catch
					{
						newRow["班组"] = range.Value2;
					}

					range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["姓名"]], objSheet.Cells[i, htCol["姓名"]]));
					try
					{
						str = range.Value2.ToString().Replace("\0", "");
						newRow["姓名"] = str;
					}
					catch
					{
						newRow["姓名"] = range.Value2;
					}

					range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["身份证号"]], objSheet.Cells[i, htCol["身份证号"]]));
					try
					{
						str = range.Value2.ToString().Replace("?", "");
						newRow["身份证号"] = str;
					}
					catch
					{
						newRow["身份证号"] = range.Value2;
					}

                    //range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["工作证号"]], objSheet.Cells[i, htCol["工作证号"]]));
                    //newRow["工作证号"] = range.Value2;

					range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["性别"]], objSheet.Cells[i, htCol["性别"]]));
					try
					{
						str = range.Value2.ToString().Replace("\0", "");
						newRow["性别"] = str;
					}
					catch
					{
						newRow["性别"] = range.Value2;
					}

                    //range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["籍贯"]], objSheet.Cells[i, htCol["籍贯"]]));
                    //newRow["籍贯"] = range.Value2;

                    //range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["民族"]], objSheet.Cells[i, htCol["民族"]]));
                    //newRow["民族"] = range.Value2;

                    //range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["婚姻状况"]], objSheet.Cells[i, htCol["婚姻状况"]]));
                    //newRow["婚姻状况"] = range.Value2;

					range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["文化程度"]], objSheet.Cells[i, htCol["文化程度"]]));
					newRow["文化程度"] = range.Value2;

					range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["政治面貌"]], objSheet.Cells[i, htCol["政治面貌"]]));
					newRow["政治面貌"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["毕业学校"]], objSheet.Cells[i, htCol["毕业学校"]]));
                    newRow["毕业学校"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["所学专业"]], objSheet.Cells[i, htCol["所学专业"]]));
                    newRow["所学专业"] = range.Value2;

                    //range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["工作地址"]], objSheet.Cells[i, htCol["工作地址"]]));
                    //newRow["工作地址"] = range.Value2;

                    //range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["邮政编码"]], objSheet.Cells[i, htCol["邮政编码"]]));
                    //newRow["邮政编码"] = range.Value2;

                    //range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["职务级别"]], objSheet.Cells[i, htCol["职务级别"]]));
                    //newRow["职务级别"] = range.Value2;

					range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["出生日期"]], objSheet.Cells[i, htCol["出生日期"]]));
					newRow["出生日期"] = range.Text.ToString();

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["入路时间"]], objSheet.Cells[i, htCol["入路时间"]]));
					newRow["入路时间"] = range.Text.ToString();

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["参加工作时间"]], objSheet.Cells[i, htCol["参加工作时间"]]));
					newRow["参加工作时间"] = range.Text.ToString();

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["职工类型"]], objSheet.Cells[i, htCol["职工类型"]]));
                    newRow["职工类型"] = range.Value2;

                    //range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["职务名称"]], objSheet.Cells[i, htCol["职务名称"]]));
                    //newRow["职务名称"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["职名"]], objSheet.Cells[i, htCol["职名"]]));
					newRow["职名"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["现从事岗位"]], objSheet.Cells[i, htCol["现从事岗位"]]));
                    newRow["现从事岗位"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["第二职名"]], objSheet.Cells[i, htCol["第二职名"]]));
                    newRow["第二职名"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["第三职名"]], objSheet.Cells[i, htCol["第三职名"]]));
                    newRow["第三职名"] = range.Value2;

					range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["班组长类型"]], objSheet.Cells[i, htCol["班组长类型"]]));
					newRow["班组长类型"] = range.Value2;

                    //range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["职教干部类型"]], objSheet.Cells[i, htCol["职教干部类型"]]));
                    //newRow["职教干部类型"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["班组长下令日期"]], objSheet.Cells[i, htCol["班组长下令日期"]]));
                    newRow["班组长下令日期"] = range.Text.ToString();

					range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["职教委员会职务"]], objSheet.Cells[i, htCol["职教委员会职务"]]));
					newRow["职教委员会职务"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["职教人员类型"]], objSheet.Cells[i, htCol["职教人员类型"]]));
                    newRow["职教人员类型"] = range.Text.ToString();

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["在册"]], objSheet.Cells[i, htCol["在册"]]));
					newRow["在册"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["在岗"]], objSheet.Cells[i, htCol["在岗"]]));
                    newRow["在岗"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["运输业职工类型"]], objSheet.Cells[i, htCol["运输业职工类型"]]));
					newRow["运输业职工类型"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["干部技术职称"]], objSheet.Cells[i, htCol["干部技术职称"]]));
					newRow["干部技术职称"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["工人技能等级"]], objSheet.Cells[i, htCol["工人技能等级"]]));
					newRow["工人技能等级"] = range.Value2;

					range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["岗位培训合格证编号"]], objSheet.Cells[i, htCol["岗位培训合格证编号"]]));
					try
					{
						str = range.Value2.ToString().Replace("?", "");
						newRow["岗位培训合格证编号"] = str;
					}
					catch
					{
						newRow["岗位培训合格证编号"] = range.Value2;
					}

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["岗位培训合格证颁发日期"]], objSheet.Cells[i, htCol["岗位培训合格证颁发日期"]]));
                    newRow["岗位培训合格证颁发日期"] = range.Text.ToString();


                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["技术档案编号"]], objSheet.Cells[i, htCol["技术档案编号"]]));
					newRow["技术档案编号"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["技能等级取得时间"]], objSheet.Cells[i, htCol["技能等级取得时间"]]));
                    newRow["技能等级取得时间"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["技术职称聘任时间"]], objSheet.Cells[i, htCol["技术职称聘任时间"]]));
                    newRow["技术职称聘任时间"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["任现职名时间"]], objSheet.Cells[i, htCol["任现职名时间"]]));
                    newRow["任现职名时间"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["毕业时间"]], objSheet.Cells[i, htCol["毕业时间"]]));
                    newRow["毕业时间"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["学校类别"]], objSheet.Cells[i, htCol["学校类别"]]));
                    newRow["学校类别"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["备注"]], objSheet.Cells[i, htCol["备注"]]));
                    newRow["备注"] = range.Value2;

					System.Threading.Thread.Sleep(10);
					jsBlock = "<script>SetPorgressBar('正在读取Excel文件','" + ((double)((i - 1) * 100) / (double)rowNum).ToString("0.00") + "'); </script>";
					Response.Write(jsBlock);
					Response.Flush();
				}

				ds.Tables.Add(dtItem);

				// 处理完成
				jsBlock = "<script>SetCompleted('Excel数据读取完毕'); </script>";
				Response.Write(jsBlock);
				Response.Flush();
			}
			catch
			{
				isClose = true;
			}
			finally
			{
				objBook.Close(Type.Missing, strPath, Type.Missing);
				objBooks.Close();
				objApp.Application.Workbooks.Close();
				objApp.Application.Quit();
				objApp.Quit();
				GC.Collect();
			}

			if (isClose)
			{
				if (File.Exists(strPath))
					File.Delete(strPath);
				Response.Write("<script>window.returnValue='请检查Excel文件格式',window.close();</script>");
				return;
			}

			#endregion

			#region 检验数据

			System.Threading.Thread.Sleep(10);
			jsBlock = "<script>SetPorgressBar('正准备检测Excel数据','0.00'); </script>";
			Response.Write(jsBlock);
			Response.Flush();

			if (ds.Tables[0].Rows.Count == 0)
			{
				Response.Write("<script>window.returnValue='Excel中没有任何记录，请核对',window.close();</script>");
				return;
			}


			Hashtable htOrg = GetOrgInfo();
			Hashtable htPost = GetPostInfo();
			Hashtable htEducationLevel = GetEducationLevel();
			Hashtable htPoliticalStatus = GetPoliticalStatus();
			Hashtable htEmployeeType = GetEmployeeType();
			Hashtable htWorkGroupLeaderType = GetWorkGroupLeaderType();
			Hashtable htEducationEmployeeType = GetEducationEmployeeType();
			Hashtable htCommitteeHeadship = GetCommitteeHeadship();
			Hashtable htEmployeeTransportType = GetEmployeeTransportType();
			Hashtable htTechnicalTitle = GetTechnicalTitle();
			Hashtable htSkillLevel = GetSkillLevel();
            Hashtable htUniversityType = GetUniversityType();
            Hashtable htWorkShop = GetWorkShop();
            Hashtable htWorkGroup = GetWorkGroup();
            //Hashtable htEmployeeLevel = GetEmployeeLevel();
            //Hashtable htTeacherType = GetTeacherType();

			Hashtable htShopNeedAdd = new Hashtable();
			Hashtable htEmployeeName = new Hashtable(); //为检测Excel中姓名是否重复
			Hashtable htWorkNo = new Hashtable(); //为检测Excel中工作证书编号是否重复
			Hashtable htPostNo = new Hashtable(); //为检测Excel中员工编码是否重复
			Hashtable htPostNeedAdd = new Hashtable();
			Hashtable htIdentityCardNo = new Hashtable();

			PostBLL objPostBll = new PostBLL();

            IList<RailExam.Model.EmployeeDetail> objEmployeeInsert = new List<RailExam.Model.EmployeeDetail>();
            IList<RailExam.Model.EmployeeDetail> objEmployeeUpdate = new List<RailExam.Model.EmployeeDetail>();


			try
			{
				int index = 1;
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					System.Threading.Thread.Sleep(10);
					jsBlock = "<script>SetPorgressBar('正在检测Excel数据','" +
							  ((double)(index * 100) / (double)ds.Tables[0].Rows.Count).ToString("0.00") + "'); </script>";
					Response.Write(jsBlock);
					Response.Flush();
					index = index + 1;

					RailExam.Model.EmployeeDetail objEmployee = new RailExam.Model.EmployeeDetail();

				    bool isUpdate = false;
                    if (dr["员工ID"].ToString().Trim() != string.Empty)
                    {
                        IList<RailExam.Model.Employee> objOldList =
                            objEmployeeBll.GetEmployeeByWhereClause("Employee_ID=" + dr["员工ID"].ToString().Trim());
                        if(objOldList.Count > 0)
                        {
                            isUpdate = true;
                        }
                        else
                        {
                            AddError(objErrorList, dr, "该员工ID在系统中不存在");
                            continue; 
                        }
                    }

				    //单位名称
					if (dr["单位"].ToString().Trim() != strUnitName)
					{
						AddError(objErrorList, dr, "单位填写错误");
						continue;
					}

                    if (dr["车间"].ToString().Trim() == "")
                    {
                        AddError(objErrorList, dr, "车间不能为空");
                        continue;
                    }
                    else
                    {
                        if (!htWorkShop.ContainsKey(dr["车间"].ToString().Trim()))
                        {
                            AddError(objErrorList, dr, "车间在本单位不存在！");
                            continue;
                        }
                    }

                    //if (dr["班组"].ToString().Trim() != "")
                    //{
                    //    if (!htWorkGroup.ContainsKey(dr["班组"].ToString().Trim()))
                    //    {
                    //        AddError(objErrorList, dr, "班组在本单位不存在！");
                    //        continue;
                    //    }
                    //}

					//组织机构
					string strOrg;
					if (dr["班组"].ToString().Trim() == string.Empty)
					{
                        strOrg = dr["单位"].ToString().Trim() + "-" + dr["车间"].ToString().Trim();
					}
					else
					{
                        strOrg = dr["单位"].ToString().Trim() + "-" + dr["车间"].ToString().Trim() + "-" + dr["班组"].ToString().Trim();
					}

					if (!htOrg.ContainsKey(strOrg))
					{
						if (dr["班组"].ToString().Trim() == string.Empty)
						{
                            if (!htShopNeedAdd.ContainsKey(dr["车间"].ToString().Trim()))
							{
                                htShopNeedAdd.Add(dr["车间"].ToString().Trim(), new Hashtable());
							}

							//如果组织机构需要新增
							objEmployee.Memo = strOrg;
						}
						else
						{
                            if (!htShopNeedAdd.ContainsKey(dr["车间"].ToString().Trim()))
							{
                                htShopNeedAdd.Add(dr["车间"].ToString().Trim(), new Hashtable());
							}

                            Hashtable htGroupNeedAdd = (Hashtable)htShopNeedAdd[dr["车间"].ToString().Trim()];
							if (!htGroupNeedAdd.ContainsKey(dr["班组"].ToString().Trim()))
							{
								htGroupNeedAdd.Add(dr["班组"].ToString().Trim(), dr["班组"].ToString().Trim());
                                htShopNeedAdd[dr["车间"].ToString().Trim()] = htGroupNeedAdd;
							}

							//如果组织机构需要新增
							objEmployee.Memo = strOrg;
						}
					}
					else
					{
						objEmployee.OrgID = Convert.ToInt32(htOrg[strOrg]);
						objEmployee.Memo = string.Empty;
					}

					//姓名不能为空
					if (dr["姓名"].ToString().Trim() == string.Empty)
					{
						AddError(objErrorList, dr, "员工姓名不能为空");
						continue;
					}
					else
					{
						if (dr["姓名"].ToString().Trim().Length > 20)
						{
							AddError(objErrorList, dr, "员工姓名不能超过20位");
							continue;
						}

						//姓名在Excel中不能重复

						//if (htEmployeeName.ContainsKey(dr["姓名"].ToString().Trim()))
						//{
						//    AddError(objErrorList, dr, "员工姓名在Excel中与序号为" + htEmployeeName[dr["姓名"].ToString()] + "的员工姓名重复");
						//    continue;
						//}
						//else
						//{
						//    htEmployeeName.Add(dr["姓名"].ToString().Trim(), dr["序号"].ToString().Trim());
						//}

						//姓名在本单位名称中不能重复

                        //IList<RailExam.Model.Employee> objView =
                        //    objEmployeeBll.GetEmployeeByWhereClause("GetStationOrgID(org_id)=" + orgID + " and Employee_Name ='" +
                        //                                            dr["姓名"].ToString().Trim() + "'");
                        //if (objView.Count > 0)
                        //{
                        //    AddError(objErrorList, dr, "员工姓名已在系统中存在");
                        //    continue;
                        //}

						objEmployee.EmployeeName = dr["姓名"].ToString().Trim();
						objEmployee.PinYinCode = Pub.GetChineseSpell(dr["姓名"].ToString().Trim());
					}

					//身份证号不能为空
					if (dr["身份证号"].ToString().Trim() == string.Empty)
					{
						AddError(objErrorList, dr, "身份证号不能为空");
						continue;
					}
					else
					{
                        if (dr["身份证号"].ToString().Trim().Length > 18)
                        {
                            AddError(objErrorList, dr, "身份证号不能超过18位");
                            continue;
                        }

                        //身份证号在Excel中不能重复
                        if (htIdentityCardNo.ContainsKey(dr["身份证号"].ToString().Trim()))
                        {
                            AddError(objErrorList, dr, "身份证号在Excel中与序号为" + htIdentityCardNo[dr["身份证号"].ToString().Trim()] + "的身份证号重复");
                            continue;
                        }
                        else
                        {
                            htIdentityCardNo.Add(dr["身份证号"].ToString().Trim(), dr["序号"].ToString().Trim());
                        }


                        IList<RailExam.Model.Employee> objList =
                             objEmployeeBll.GetEmployeeByWhereClause("identity_CardNo='" + dr["身份证号"].ToString().Trim() + "'");
                        if (objList.Count > 0)
                        {
                            if (isUpdate)
                            {
                                if (objList.Count > 1)
                                {
                                    AddError(objErrorList, dr, "与该员工身份证号完全相同的员工已经存在");
                                    continue;
                                }
                            }
                            else
                            {
                                AddError(objErrorList, dr, "该员工身份证号与【" + objList[0].OrgName + "】中【" + objList[0].EmployeeName + "】的身份证号完全相同");
                                continue;
                            }
                        }

						objEmployee.IdentifyCode = dr["身份证号"].ToString().Trim();
					}

                    #region 工作证号
                    //if (dr["工作证号"].ToString().Trim() != string.Empty)
                    //{
                    //    if (dr["工作证号"].ToString().Trim().Length > 14)
                    //    {
                    //        AddError(objErrorList, dr, "工作证号不能超过14位");
                    //        continue;
                    //    }

                    //    //工作证号在Excel中不能重复
                    //    //if (htWorkNo.ContainsKey(dr["工作证号"].ToString().Trim()))
                    //    //{
                    //    //    AddError(objErrorList, dr, "工作证号在Excel中与序号为" + htWorkNo[dr["工作证号"].ToString()] + "的工作证号重复");
                    //    //    continue;
                    //    //}
                    //    //else
                    //    //{
                    //    //    htWorkNo.Add(dr["工作证号"].ToString().Trim(), dr["序号"].ToString().Trim());
                    //    //}

                    //    //IList<RailExam.Model.Employee> objView =
                    //    //    objEmployeeBll.GetEmployeeByWhereClause("Post_No='" + dr["工作证号"].ToString().Trim() + "'");
                    //    //if (objView.Count > 0)
                    //    //{
                    //    //    AddError(objErrorList, dr, "员工工作证号已在系统中存在");
                    //    //    continue;
                    //    //}

                    //    objEmployee.PostNo = dr["工作证号"].ToString().Trim();
                    //}
                    //else
                    //{
                    //    objEmployee.PostNo = "";
                    //}
                    #endregion


					//性别
					if (dr["性别"].ToString().Trim() != "男" && dr["性别"].ToString().Trim() != "女")
					{
						AddError(objErrorList, dr, "性别必须为男或女");
						continue;
					}
					else
					{
						objEmployee.Sex = dr["性别"].ToString().Trim();
                    }

                    #region 屏蔽
                    ////籍贯
                    //if (dr["籍贯"].ToString().Trim().Length > 20)
                    //{
                    //    AddError(objErrorList, dr, "籍贯不能超过20位");
                    //    continue;
                    //}
                    //else
                    //{
                    //    objEmployee.NativePlace = dr["籍贯"].ToString().Trim();
                    //}

                    ////民族
                    //if (dr["民族"].ToString().Trim().Length > 10)
                    //{
                    //    AddError(objErrorList, dr, "民族不能超过10位");
                    //    continue;
                    //}
                    //else
                    //{
                    //    objEmployee.Folk = dr["民族"].ToString().Trim();
                    //}

                    ////婚姻状况
                    //if (dr["婚姻状况"].ToString().Trim() == "未婚")
                    //{
                    //    objEmployee.Wedding = 0;
                    //}
                    //else
                    //{
                    //    objEmployee.Wedding = 1;
                    //}
                    #endregion

                    //现文化程度
					if (dr["文化程度"].ToString().Trim() == string.Empty)
					{
						AddError(objErrorList, dr, "文化程度不能为空");
						continue;
					}
					else
					{
						if (!htEducationLevel.ContainsKey(dr["文化程度"].ToString().Trim()))
						{
							AddError(objErrorList, dr, "文化程度在系统中不存在");
							continue;
						}
						else
						{
							objEmployee.EducationLevelID = Convert.ToInt32(htEducationLevel[dr["文化程度"].ToString().Trim()]);
						}
					}

					//政治面貌
					if (dr["政治面貌"].ToString().Trim() != string.Empty)
					{
						if (!htPoliticalStatus.ContainsKey(dr["政治面貌"].ToString().Trim()))
						{
							AddError(objErrorList, dr, "政治面貌在系统中不存在");
							continue;
						}
						else
						{
							objEmployee.PoliticalStatusID = Convert.ToInt32(htPoliticalStatus[dr["政治面貌"].ToString().Trim()]);
						}
					}
                    else
					{
					    objEmployee.PoliticalStatusID = 1;
					}


                    #region 屏蔽
                    ////工作地址
                    //if (dr["工作地址"].ToString().Trim().Length > 100)
                    //{
                    //    AddError(objErrorList, dr, "工作地址不能超过100位");
                    //    continue;
                    //}
                    //else
                    //{
                    //    objEmployee.Address = dr["工作地址"].ToString().Trim();
                    //}

                    ////邮政编码
                    //if (dr["邮政编码"].ToString().Trim().Length > 6)
                    //{
                    //    AddError(objErrorList, dr, "邮政编码不能超过6位");
                    //    continue;
                    //}
                    //else
                    //{
                    //    objEmployee.PostCode = dr["邮政编码"].ToString().Trim();
                    //}

                    ////职务级别
                    //if (dr["职务级别"].ToString().Trim() != string.Empty)
                    //{
                    //    if (!htEmployeeLevel.ContainsKey(dr["职务级别"].ToString().Trim()))
                    //    {
                    //        AddError(objErrorList, dr, "职务级别在系统中不存在");
                    //        continue;
                    //    }
                    //    else
                    //    {
                    //        objEmployee.EmployeeLevelID = Convert.ToInt32(htEmployeeLevel[dr["职务级别"].ToString().Trim()]);
                    //    }
                    //}
                    #endregion

                    //出生日期
                    if (dr["出生日期"].ToString().Trim() == string.Empty)
                    {
                        AddError(objErrorList, dr, "出生日期不能为空");
                        continue;
                    }
                    else
                    {
                        try
                        {
                            string strBirth = dr["出生日期"].ToString().Trim();
                            if (strBirth.IndexOf("-") >= 0)
                            {
                                objEmployee.Birthday = Convert.ToDateTime(strBirth);
                            }
                            else
                            {
                                if (strBirth.Length != 8)
                                {
                                    AddError(objErrorList, dr, "出生日期填写错误");
                                    continue;
                                }
                                else
                                {
                                    strBirth = strBirth.Insert(4, "-");
                                    strBirth = strBirth.Insert(7, "-");
                                    objEmployee.Birthday = Convert.ToDateTime(strBirth);
                                }
                            }

                            if (Convert.ToDateTime(strBirth) < Convert.ToDateTime("1775-1-1") ||
                                Convert.ToDateTime(strBirth) > Convert.ToDateTime("1993-12-31"))
                            {
                                AddError(objErrorList, dr, "出生日期填写错误");
                                continue;
                            }
                        }
                        catch
                        {
                            AddError(objErrorList, dr, "出生日期填写错误");
                            continue;
                        }
                    }

				    //入路工作日期
                    if (dr["入路时间"].ToString().Trim() == string.Empty)
                    {
                        AddError(objErrorList, dr, "入路时间不能为空");
                        continue;
                    }
                    else
                    {
                        try
                        {
                            string strJoin = dr["入路时间"].ToString().Trim();
                            if (strJoin.IndexOf("-") >= 0)
                            {
                                objEmployee.WorkDate = Convert.ToDateTime(strJoin);
                            }
                            else
                            {
                                if (strJoin.Length != 8)
                                {
                                    AddError(objErrorList, dr, "入路时间填写错误");
                                    continue;
                                }
                                else
                                {
                                    strJoin = strJoin.Insert(4, "-");
                                    strJoin = strJoin.Insert(7, "-");
                                    objEmployee.WorkDate = Convert.ToDateTime(strJoin);
                                }
                            }

                            if (Convert.ToDateTime(strJoin) < Convert.ToDateTime("1775-1-1"))
                            {
                                AddError(objErrorList, dr, "入路时间填写错误");
                                continue;
                            }
                        }
                        catch
                        {
                            AddError(objErrorList, dr, "入路时间填写错误");
                            continue;
                        }
                    }


				    //参加工作日期
                    if (dr["参加工作时间"].ToString().Trim() == string.Empty)
                    {
                        AddError(objErrorList, dr, "参加工作时间不能为空");
                        continue;
                    }
                    else
                    {
                        try
                        {
                            string strJoin = dr["参加工作时间"].ToString().Trim();
                            if (strJoin.IndexOf("-") >= 0)
                            {
                                objEmployee.BeginDate = Convert.ToDateTime(strJoin);
                            }
                            else
                            {
                                if (strJoin.Length != 8)
                                {
                                    AddError(objErrorList, dr, "参加工作时间填写错误");
                                    continue;
                                }
                                else
                                {
                                    strJoin = strJoin.Insert(4, "-");
                                    strJoin = strJoin.Insert(7, "-");
                                    objEmployee.BeginDate = Convert.ToDateTime(strJoin);
                                }
                            }

                            if (Convert.ToDateTime(strJoin) < Convert.ToDateTime("1775-1-1"))
                            {
                                AddError(objErrorList, dr, "参加工作时间填写错误");
                                continue;
                            }
                        }
                        catch
                        {
                            AddError(objErrorList, dr, "参加工作时间填写错误");
                            continue;
                        }
                    }

				    //职工类型
					if (dr["职工类型"].ToString().Trim() == "")
					{
                        AddError(objErrorList, dr, "职工类型不能为空！");
						continue;
					}
					else
					{
                        if (!htEmployeeType.ContainsKey(dr["职工类型"].ToString().Trim()))
						{
                            AddError(objErrorList, dr, "职工类型在系统中不存在！");
							continue;
						}
						else
						{
                            objEmployee.EmployeeTypeID = Convert.ToInt32(htEmployeeType[dr["职工类型"].ToString().Trim()]);
						}
					}


                    //职名
					if (dr["职名"].ToString().Trim() == string.Empty)
					{
                        AddError(objErrorList, dr, "职名不能为空！");
						continue;
					}
					else
					{
						IList<RailExam.Model.Post> objPost =
                            objPostBll.GetPostsByWhereClause("Post_Level=3 and Post_Name='" + dr["职名"].ToString().Trim() + "'");
						if (objPost.Count == 0)
						{
                            AddError(objErrorList, dr, "职名在系统中不存在！");
							continue;
						}

                        objEmployee.PostID = Convert.ToInt32(htPost[dr["职名"].ToString().Trim()]);
					}

                    //任现职名时间
                    if (dr["任现职名时间"].ToString().Trim() == string.Empty)
                    {
                        AddError(objErrorList, dr, "任现职名时间不能为空");
                        continue;
                    }
                    else
                    {
                        try
                        {
                            string strJoin = dr["任现职名时间"].ToString().Trim();
                            if (strJoin.IndexOf("-") >= 0)
                            {
                                objEmployee.PostDate = Convert.ToDateTime(strJoin);
                            }
                            else
                            {
                                if (strJoin.Length != 8)
                                {
                                    AddError(objErrorList, dr, "任现职名时间填写错误");
                                    continue;
                                }
                                else
                                {
                                    strJoin = strJoin.Insert(4, "-");
                                    strJoin = strJoin.Insert(7, "-");
                                    objEmployee.PostDate = Convert.ToDateTime(strJoin);
                                }
                            }

                            if (Convert.ToDateTime(strJoin) < Convert.ToDateTime("1775-1-1"))
                            {
                                AddError(objErrorList, dr, "任现职名时间填写错误");
                                continue;
                            }
                        }
                        catch
                        {
                            AddError(objErrorList, dr, "任现职名时间填写错误");
                            continue;
                        }
                    }

                    //职名
                    if (dr["现从事岗位"].ToString().Trim() != string.Empty)
                    {
                        IList<RailExam.Model.Post> objPost =
                            objPostBll.GetPostsByWhereClause("Post_Level=3 and Post_Name='" + dr["现从事岗位"].ToString().Trim() + "'");
                        if (objPost.Count == 0)
                        {
                            AddError(objErrorList, dr, "现从事岗位在系统中不存在！");
                            continue;
                        }

                        objEmployee.NowPostID = Convert.ToInt32(htPost[dr["现从事岗位"].ToString().Trim()]);
                    }
                    else
                    {
                        objEmployee.NowPostID = null;
                    }

				    //职名
                    if (dr["第二职名"].ToString().Trim() != string.Empty)
                    {
                        IList<RailExam.Model.Post> objPost =
                            objPostBll.GetPostsByWhereClause("Post_Level=3 and Post_Name='" + dr["第二职名"].ToString().Trim() + "'");
                        if (objPost.Count == 0)
                        {
                            AddError(objErrorList, dr, "第二职名在系统中不存在！");
                            continue;
                        }

                        objEmployee.SecondPostID = Convert.ToInt32(htPost[dr["第二职名"].ToString().Trim()]);
                    }
                    else
                    {
                        objEmployee.SecondPostID = null;
                    }

                    //职名
                    if (dr["第三职名"].ToString().Trim() != string.Empty)
                    {
                        IList<RailExam.Model.Post> objPost =
                            objPostBll.GetPostsByWhereClause("Post_Level=3 and Post_Name='" + dr["第三职名"].ToString().Trim() + "'");
                        if (objPost.Count == 0)
                        {
                            AddError(objErrorList, dr, "第三职名在系统中不存在！");
                            continue;
                        }

                        objEmployee.ThirdPostID = Convert.ToInt32(htPost[dr["第三职名"].ToString().Trim()]);
                    }
                    else
                    {
                        objEmployee.ThirdPostID = null;
                    }



					//班组长类型
					if (dr["班组长类型"].ToString().Trim() == string.Empty)
					{
						objEmployee.IsGroupLeader = 0;
					}
					else
					{
						if (!htWorkGroupLeaderType.ContainsKey(dr["班组长类型"].ToString().Trim()))
						{
							AddError(objErrorList, dr, "班组长类型在系统中不存在！");
							continue;
						}

						objEmployee.WorkGroupLeaderTypeID = Convert.ToInt32(htWorkGroupLeaderType[dr["班组长类型"].ToString().Trim()]);
						objEmployee.IsGroupLeader = 1;
					}


                    if (dr["班组长下令日期"].ToString().Trim() != string.Empty)
                    {
                        //班组长下令日期
                        try
                        {
                            string strJoin = dr["班组长下令日期"].ToString().Trim();
                            if (strJoin.IndexOf("-") >= 0)
                            {
                                objEmployee.GroupNoDate = Convert.ToDateTime(strJoin);
                            }
                            else
                            {
                                if (strJoin.Length != 8)
                                {
                                    AddError(objErrorList, dr, "班组长下令日期填写错误");
                                    continue;
                                }
                                else
                                {
                                    strJoin = strJoin.Insert(4, "-");
                                    strJoin = strJoin.Insert(7, "-");
                                    objEmployee.GroupNoDate = Convert.ToDateTime(strJoin);
                                }
                            }

                            if (Convert.ToDateTime(strJoin) < Convert.ToDateTime("1775-1-1"))
                            {
                                AddError(objErrorList, dr, "班组长下令日期填写错误");
                                continue;
                            }
                        }
                        catch
                        {
                            AddError(objErrorList, dr, "班组长下令日期填写错误");
                            continue;
                        }
                    }
                    else
                    {
                        if (dr["班组长类型"].ToString().Trim() != string.Empty)
                        {
                            AddError(objErrorList, dr, "当班组长类型不为空时，班组长下令日期不能为空");
                            continue;
                        }
                    }


				    //职教干部类型
					//if (dr["部门名称"].ToString().Trim() != "职工教育科") //部门名称不为站段职教科
					//{
					//    if (dr["职教干部类型"].ToString().Trim() != string.Empty)
					//    {
					//        AddError(objErrorList, dr, "当部门名称类型不为“站段教育科”时，职教干部类型必须为空！");
					//        continue;
					//    }
					//}
					//else
					//{
					if (dr["职教人员类型"].ToString().Trim() != string.Empty)
					{
                        if (!htEducationEmployeeType.ContainsKey(dr["职教人员类型"].ToString().Trim()))
						{
                            AddError(objErrorList, dr, "职教人员类型在系统中不存在！");
							continue;
						}
						else
						{
                            objEmployee.EducationEmployeeTypeID = Convert.ToInt32(htEducationEmployeeType[dr["职教人员类型"].ToString().Trim()]);
						}
					}
                    else
					{
					    objEmployee.EducationEmployeeTypeID = -1;
					}
					//}

					//职教委员会职务
					if (dr["职教委员会职务"].ToString().Trim() != string.Empty)
					{
						if (!htCommitteeHeadship.ContainsKey(dr["职教委员会职务"].ToString().Trim()))
						{
							AddError(objErrorList, dr, "职教委员会职务在系统中不存在！");
							continue;
						}
						else
						{
							objEmployee.CommitteeHeadShipID = Convert.ToInt32(htCommitteeHeadship[dr["职教委员会职务"].ToString().Trim()]);
						}
					}
					else
					{
					    objEmployee.CommitteeHeadShipID = -1;
					}

					//教师类别
                    //if (dr["教师类别"].ToString().Trim() != string.Empty)
                    //{
                    //    if (!htTeacherType.ContainsKey(dr["教师类别"].ToString().Trim()))
                    //    {
                    //        AddError(objErrorList, dr, "教师类别在系统中不存在");
                    //        continue;
                    //    }
                    //    else
                    //    {
                    //        objEmployee.TeacherTypeID = Convert.ToInt32(htTeacherType[dr["教师类别"].ToString().Trim()]);
                    //    }
                    //}

                    //在册
                    if (dr["在册"].ToString().Trim() == "不在册")
					{
                        objEmployee.Dimission = false;
					}
                    else if (dr["在册"].ToString().Trim() == "在册")
					{
						objEmployee.Dimission = true;
					}
                    else
                    {
                        AddError(objErrorList, dr, "在册只能填“在册”和“不在册”！");
                        continue; 
                    }

                    //在岗
                    if (dr["在岗"].ToString().Trim() == "不在岗")
                    {
                        objEmployee.IsOnPost = false;
                    }
                    else if(dr["在岗"].ToString().Trim() == "在岗")
                    {
                        objEmployee.IsOnPost = true;
                    }
                    else
                    {
                        AddError(objErrorList, dr, "在岗只能填“在岗”和“不在岗”！");
                        continue;
                    }


                    //运输业职工类型
					if (dr["运输业职工类型"].ToString().Trim() == string.Empty)
					{
					    objEmployee.EmployeeTransportTypeID = -1;
					}
					else
					{
                        if (!htEmployeeTransportType.ContainsKey(dr["运输业职工类型"].ToString().Trim()))
						{
                            AddError(objErrorList, dr, "运输业职工类型在系统中不存在！");
							continue;
						}
						else
						{
                            objEmployee.EmployeeTransportTypeID = Convert.ToInt32(htEmployeeTransportType[dr["运输业职工类型"].ToString().Trim()]);
						}
					}

					//现技术职务名称
					if (objEmployee.EmployeeTypeID == 0)
					{
						if (dr["干部技术职称"].ToString().Trim() != string.Empty)
						{
                            AddError(objErrorList, dr, "当干部工人标识为“工人”时，干部技术职称必须为空！");
							continue;
						}
					}
					else
					{
                        if (dr["干部技术职称"].ToString().Trim() != string.Empty)
						{
                            if (!htTechnicalTitle.ContainsKey(dr["干部技术职称"].ToString().Trim()))
							{
                                AddError(objErrorList, dr, "干部技术职称在系统中不存在！");
								continue;
							}
							else
							{
                                objEmployee.TechnicalTitleID = Convert.ToInt32(htTechnicalTitle[dr["干部技术职称"].ToString().Trim()]);
							}

                            if (dr["技术职称聘任时间"].ToString().Trim() != string.Empty)
                            {
                                //技术职称聘任时间
                                try
                                {
                                    string strJoin = dr["技术职称聘任时间"].ToString().Trim();
                                    if (strJoin.IndexOf("-") >= 0)
                                    {
                                        objEmployee.TechnicalTitleDate = Convert.ToDateTime(strJoin);
                                    }
                                    else
                                    {
                                        if (strJoin.Length != 8)
                                        {
                                            AddError(objErrorList, dr, "技术职称聘任时间填写错误");
                                            continue;
                                        }
                                        else
                                        {
                                            strJoin = strJoin.Insert(4, "-");
                                            strJoin = strJoin.Insert(7, "-");
                                            objEmployee.TechnicalTitleDate = Convert.ToDateTime(strJoin);
                                        }
                                    }

                                    if (Convert.ToDateTime(strJoin) < Convert.ToDateTime("1775-1-1"))
                                    {
                                        AddError(objErrorList, dr, "技术职称聘任时间填写错误");
                                        continue;
                                    }
                                }
                                catch
                                {
                                    AddError(objErrorList, dr, "技术职称聘任时间填写错误");
                                    continue;
                                }
                            }
                            else
                            {
                                objEmployee.TechnicalTitleDate = null;
                                //AddError(objErrorList, dr, "当干部技术职称不为空时，技术职称聘任时间不能为空");
                                //continue;
                            }
						}
					}

					//技术等级
					if (objEmployee.EmployeeTypeID == 0)
					{
						if (dr["工人技能等级"].ToString().Trim() != string.Empty)
						{
                            if (!htSkillLevel.ContainsKey(dr["工人技能等级"].ToString().Trim()))
							{
                                AddError(objErrorList, dr, "工人技能等级在系统中不存在！");
								continue;
							}
							else
							{
                                objEmployee.TechnicianTypeID = Convert.ToInt32(htSkillLevel[dr["工人技能等级"].ToString().Trim()]);
							}

                            if (dr["技能等级取得时间"].ToString().Trim() != string.Empty)
                            {
                                //班组长下令日期
                                try
                                {
                                    string strJoin = dr["技能等级取得时间"].ToString().Trim();
                                    if (strJoin.IndexOf("-") >= 0)
                                    {
                                        objEmployee.TechnicalDate = Convert.ToDateTime(strJoin);
                                    }
                                    else
                                    {
                                        if (strJoin.Length != 8)
                                        {
                                            AddError(objErrorList, dr, "技能等级取得时间填写错误");
                                            continue;
                                        }
                                        else
                                        {
                                            strJoin = strJoin.Insert(4, "-");
                                            strJoin = strJoin.Insert(7, "-");
                                            objEmployee.TechnicalDate = Convert.ToDateTime(strJoin);
                                        }
                                    }

                                    if (Convert.ToDateTime(strJoin) < Convert.ToDateTime("1775-1-1"))
                                    {
                                        AddError(objErrorList, dr, "技能等级取得时间填写错误");
                                        continue;
                                    }
                                }
                                catch
                                {
                                    AddError(objErrorList, dr, "技能等级取得时间填写错误");
                                    continue;
                                }
                            }
                            else
                            {
                                objEmployee.TechnicalDate = null;
                                //AddError(objErrorList, dr, "当工人技能等级不为空时，技能等级取得时间不能为空");
                                //continue;
                            }
						}
						else
						{
							objEmployee.TechnicianTypeID = 1;
						}
					}
					else
					{
						objEmployee.TechnicianTypeID = 1;
					}

					//岗位培训合格证编号
					if (dr["岗位培训合格证编号"].ToString().Trim() != string.Empty)
					{
						if (dr["岗位培训合格证编号"].ToString().Trim().Length > 20)
						{
							AddError(objErrorList, dr, "岗位培训合格证编号不能超过20位");
							continue;
						}

                        //岗位培训合格证编号在Excel中不能重复
						if (htPostNo.ContainsKey(dr["岗位培训合格证编号"].ToString().Trim()))
						{
							AddError(objErrorList, dr, "岗位培训合格证编号在Excel中与序号为" + htPostNo[dr["岗位培训合格证编号"].ToString().Trim()] + "的岗位培训合格证编号重复");
							continue;
						}
						else
						{
							htPostNo.Add(dr["岗位培训合格证编号"].ToString().Trim(), dr["序号"].ToString().Trim());
						}

                        IList<RailExam.Model.Employee> objView;

                        if (!isUpdate)
                        {
                            objView =
                                objEmployeeBll.GetEmployeeByWhereClause("Work_No='" + dr["岗位培训合格证编号"].ToString().Trim() + "'");
                        }
                        else
                        {
                            objView =
                                objEmployeeBll.GetEmployeeByWhereClause("Employee_ID<>" + dr["员工ID"] + " and Work_No='" + dr["岗位培训合格证编号"].ToString().Trim() + "'");
                        }

                        if (objView.Count > 0)
                        {
                            AddError(objErrorList, dr, "岗位培训合格证编号已在系统中存在");
                            continue;
                        }

                        objEmployee.WorkNo = dr["岗位培训合格证编号"].ToString().Trim();
					}
					else
					{
						objEmployee.WorkNo = string.Empty;
                        //AddError(objErrorList, dr, "岗位培训合格证编号不能为空！如果确实没有，请输入工作证号或身份证号！");
                        //continue;
					}

                    //岗位培训合格证颁发日期
                    if(dr["岗位培训合格证颁发日期"].ToString().Trim()!= string.Empty)
				    {
				        try
				        {
				            string strJoin = dr["岗位培训合格证颁发日期"].ToString().Trim();
				            if (strJoin.IndexOf("-") >= 0)
				            {
				                objEmployee.PostNoDate = Convert.ToDateTime(strJoin);
				            }
				            else
				            {
				                if (strJoin.Length != 8)
				                {
				                    AddError(objErrorList, dr, "岗位培训合格证颁发日期填写错误");
				                    continue;
				                }
				                else
				                {
				                    strJoin = strJoin.Insert(4, "-");
				                    strJoin = strJoin.Insert(7, "-");
				                    objEmployee.PostNoDate = Convert.ToDateTime(strJoin);
				                }
				            }

				            if (Convert.ToDateTime(strJoin) < Convert.ToDateTime("1775-1-1"))
				            {
				                AddError(objErrorList, dr, "岗位培训合格证颁发日期填写错误");
				                continue;
				            }
				        }
				        catch
				        {
                            AddError(objErrorList, dr, "岗位培训合格证颁发日期填写错误");
				            continue;
				        }
				    }

				    if (dr["技术档案编号"].ToString().Trim() != string.Empty)
					{
                        if (dr["技术档案编号"].ToString().Trim().Length > 20)
						{
                            AddError(objErrorList, dr, "技术档案编号不能超过20位");
							continue;
						}
                        objEmployee.TechnicalCode = dr["技术档案编号"].ToString().Trim();
					}

                    //毕业学校
                    if (dr["毕业学校"].ToString().Trim().Length > 50)
                    {
                        AddError(objErrorList, dr, "毕业学校不能超过50位");
                        continue;
                    }
                    else
                    {
                        objEmployee.GraduateUniversity = dr["毕业学校"].ToString().Trim();
                    }

                    //所学专业
                    if (dr["所学专业"].ToString().Trim().Length > 50)
                    {
                        AddError(objErrorList, dr, "所学专业不能超过50位");
                        continue;
                    }
                    else
                    {
                        objEmployee.StudyMajor = dr["所学专业"].ToString().Trim();
                    }

                    //毕业时间
                    if (dr["毕业时间"].ToString().Trim() != string.Empty)
                    {
                        try
                        {
                            string strJoin = dr["毕业时间"].ToString().Trim();
                            if (strJoin.IndexOf("-") >= 0)
                            {
                                objEmployee.GraduatDate = Convert.ToDateTime(strJoin);
                            }
                            else
                            {
                                if (strJoin.Length != 8)
                                {
                                    AddError(objErrorList, dr, "毕业时间填写错误");
                                    continue;
                                }
                                else
                                {
                                    strJoin = strJoin.Insert(4, "-");
                                    strJoin = strJoin.Insert(7, "-");
                                    objEmployee.GraduatDate = Convert.ToDateTime(strJoin);
                                }
                            }

                            if (Convert.ToDateTime(strJoin) < Convert.ToDateTime("1775-1-1"))
                            {
                                AddError(objErrorList, dr, "毕业时间填写错误");
                                continue;
                            }
                        }
                        catch
                        {
                            AddError(objErrorList, dr, "毕业时间填写错误");
                            continue;
                        }
                    }

                    if (dr["学校类别"].ToString().Trim() != string.Empty)
                    {
                        if (!htUniversityType.ContainsKey(dr["学校类别"].ToString().Trim()))
                        {
                            AddError(objErrorList, dr, "学校类别在系统中不存在");
                            continue;
                        }
                        else
                        {
                            objEmployee.UniversityType = Convert.ToInt32(htUniversityType[dr["学校类别"].ToString().Trim()]);
                        }
                    }
                    else
                    {
                        objEmployee.UniversityType = 0;
                    }


                    if (dr["员工ID"].ToString().Trim() == string.Empty)
                    {
                       objEmployeeInsert.Add(objEmployee);
                    }
                    else
                    {
                        try
                        {
                            int employeeID = Convert.ToInt32(dr["员工ID"].ToString().Trim());
                            objEmployee.EmployeeID = employeeID;
                            objEmployeeUpdate.Add(objEmployee);

                        }
                        catch
                        {
                            AddError(objErrorList, dr, "员工ID填写错误！");
                            continue;
                        }
                    }
				}

				// 处理完成
				jsBlock = "<script>SetCompleted('Excel数据检测完毕'); </script>";
				Response.Write(jsBlock);
				Response.Flush();
			}
			catch (Exception ex)
			{
				Response.Write(EnhancedStackTrace(ex));
			}

			#endregion

			if (objErrorList.Count > 0)
			{
				jsBlock = "<script>SetCompleted('正在统计不符合要求的数据，请等待......'); </script>";
				Response.Write(jsBlock);
				Response.Flush();

				objErrorBll.AddEmployeeError(objErrorList);

				if (File.Exists(strPath))
					File.Delete(strPath);
				Response.Write("<script>window.returnValue='refresh|请检查Excel数据',window.close();</script>");
				return;
			}


			if (!string.IsNullOrEmpty(Request.QueryString.Get("mode")))
			{
				Response.Write("<script>window.returnValue='refresh|数据检查成功',window.close();</script>");
				return;
			}

			#region 导入数据

			System.Threading.Thread.Sleep(10);
			jsBlock = "<script>SetPorgressBar('正在导入部门名称、班组','0.00'); </script>";
			Response.Write(jsBlock);
			Response.Flush();

			Database db = DatabaseFactory.CreateDatabase();

			DbConnection connection = db.CreateConnection();
			connection.Open();
			DbTransaction transaction = connection.BeginTransaction();

			try
			{
				Hashtable htWorkshop = GetWorkShop(db, transaction);
				int count = 1;
				foreach (System.Collections.DictionaryEntry obj in htShopNeedAdd)
				{
					int nWorkShopID;
					if (!htWorkshop.ContainsKey(obj.Key.ToString()))
					{
						RailExam.Model.Organization objshop = new RailExam.Model.Organization();
						objshop.FullName = obj.Key.ToString();
						objshop.ShortName = obj.Key.ToString();
						objshop.ParentId = Convert.ToInt32(orgID);
						objshop.Memo = "";
						nWorkShopID = orgBll.AddOrganization(db, transaction, objshop);
					}
					else
					{
						nWorkShopID = Convert.ToInt32(htWorkshop[obj.Key.ToString()]);
					}

					Hashtable htGroup = (Hashtable)obj.Value;
					if (htGroup.Count != 0)
					{
						foreach (System.Collections.DictionaryEntry objGroupNeedAdd in htGroup)
						{
							RailExam.Model.Organization objGroup = new RailExam.Model.Organization();
							objGroup.FullName = objGroupNeedAdd.Key.ToString();
							objGroup.ShortName = objGroupNeedAdd.Key.ToString();
							objGroup.ParentId = nWorkShopID;
							objGroup.Memo = "";
							orgBll.AddOrganization(db, transaction, objGroup);
						}
					}

					System.Threading.Thread.Sleep(10);
					jsBlock = "<script>SetPorgressBar('正在导入部门名称、班组','" + ((double)(count * 100) / (double)(htShopNeedAdd.Count + objEmployeeInsert.Count)).ToString("0.00") + "'); </script>";
					Response.Write(jsBlock);
					Response.Flush();
					count = count + 1;
				}

				htWorkshop = GetWorkShop(db, transaction);
				Hashtable htNowOrg = GetOrgInfo(db, transaction);

				foreach (RailExam.Model.EmployeeDetail objEmployee in objEmployeeInsert)
				{
					if (objEmployee.Memo.ToString() != string.Empty)
					{
						if (objEmployee.Memo.Split('-').Length == 2)
						{
							objEmployee.OrgID = Convert.ToInt32(htWorkshop[objEmployee.Memo.Split('-')[1]]);
						}
						else
						{
							objEmployee.OrgID = Convert.ToInt32(htNowOrg[objEmployee.Memo.ToString()].ToString().Split('-')[0]);
						}
					}

					if (objEmployee.OrgID == 0)
					{
						throw new Exception("aaaa");
					}

					objEmployee.Memo = "";


					System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('正在导入部门名称、班组','" + ((double)(count * 100) / (double)(htShopNeedAdd.Count + objEmployeeInsert.Count + objEmployeeUpdate.Count)).ToString("0.00") + "'); </script>";
					Response.Write(jsBlock);
					Response.Flush();
					count = count + 1;
				}

                foreach (RailExam.Model.EmployeeDetail objEmployee in objEmployeeUpdate)
                {
                    if (objEmployee.Memo.ToString() != string.Empty)
                    {
                        if (objEmployee.Memo.Split('-').Length == 2)
                        {
                            objEmployee.OrgID = Convert.ToInt32(htWorkshop[objEmployee.Memo.Split('-')[1]]);
                        }
                        else
                        {
                            objEmployee.OrgID = Convert.ToInt32(htNowOrg[objEmployee.Memo.ToString()].ToString().Split('-')[0]);
                        }
                    }

                    if (objEmployee.OrgID == 0)
                    {
                        throw new Exception("aaaa");
                    }

                    objEmployee.Memo = "";

                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('正在导入部门名称、班组','" + ((double)(count * 100) / (double)(htShopNeedAdd.Count + objEmployeeInsert.Count + objEmployeeUpdate.Count)).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                    count = count + 1;
                }

				System.Threading.Thread.Sleep(10);
				jsBlock = "<script>SetPorgressBar('正在导入干部职名','0.00'); </script>";
				Response.Write(jsBlock);
				Response.Flush();

				count = 1;
				foreach (System.Collections.DictionaryEntry objPostNeed in htPostNeedAdd)
				{
					RailExam.Model.Post objPost = new RailExam.Model.Post();
					objPost.ParentId = 373;
					objPost.PostName = objPostNeed.Key.ToString();
					objPost.Technician = 0;
					objPost.Promotion = 0;
					objPost.Description = string.Empty;
					objPost.Memo = string.Empty;
					int postID = objPostBll.AddPost(db, transaction, objPost);

					ArrayList objPostList = (ArrayList)objPostNeed.Value;
					for (int i = 0; i < objPostList.Count; i++)
					{
					    string[] strPost = objPostList[i].ToString().Split('|');
					    if(strPost[1] == "false")
					    {
                            objEmployeeInsert[Convert.ToInt32(strPost[0])].PostID = postID;
                        }
					    else
					    {
                            objEmployeeUpdate[Convert.ToInt32(strPost[0])].PostID = postID;
                        }
					}

					System.Threading.Thread.Sleep(10);
					jsBlock = "<script>SetPorgressBar('正在导入干部职名','" + ((double)(count * 100) / (double)(htPostNeedAdd.Count)).ToString("0.00") + "'); </script>";
					Response.Write(jsBlock);
					Response.Flush();
					count = count + 1;
				}

				System.Threading.Thread.Sleep(10);
				jsBlock = "<script>SetPorgressBar('正在导入员工信息','0.00'); </script>";
				Response.Write(jsBlock);
				Response.Flush();

                OracleAccess access = new OracleAccess();
			    string strSql;

				count = 1;
				for (int i = 0; i < objEmployeeInsert.Count; i++)
				{
					int employeeid = objEmployeeDetailBll.AddEmployee(db, transaction, objEmployeeInsert[i]);

                    //strSql = "select * from Employee_Photo where Employee_ID=" + employeeid;
                    //DataSet dsPhoto = access.RunSqlDataSet(strSql);
                    //if(dsPhoto.Tables[0].Rows.Count==0)
                    //{
                    //    strSql = "insert into Employee_Photo values(" + employeeid + ",null)";
                    //    access.ExecuteNonQuery(strSql);
                    //}

					System.Threading.Thread.Sleep(10);
					jsBlock = "<script>SetPorgressBar('正在导入员工信息','" + ((double)(count * 100) / (double)(objEmployeeInsert.Count+objEmployeeUpdate.Count)).ToString("0.00") + "'); </script>";
					Response.Write(jsBlock);
					Response.Flush();
					count = count + 1;
				}


                for (int i = 0; i < objEmployeeUpdate.Count; i++ )
                {
                    objEmployeeDetailBll.UpdateEmployee(db, transaction, objEmployeeUpdate[i]);
                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('正在导入员工信息','" + ((double)(count * 100) / (double)(objEmployeeInsert.Count + objEmployeeUpdate.Count)).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                    count = count + 1; 
                }

                strMessage = "导入成功!";
				transaction.Commit();
			}
			catch (Exception ex)
			{
				strMessage = "导入失败!";
				transaction.Rollback();
				//Response.Write(EnhancedStackTrace(ex));
			}
			finally
			{
				connection.Close();
			}

			if (File.Exists(strPath))
				File.Delete(strPath);
			Response.Write("<script>window.returnValue='refresh|" + strMessage + "';window.close();</script>");
			#endregion
		}

		#endregion

		/// <summary>
		/// 添加错误信息
		/// </summary>
		/// <param name="objErrorList">错误信息List</param>
		/// <param name="dr">数据来源DataRow</param>
		/// <param name="strError">错误原因</param>
		private void AddError(IList<EmployeeError> objErrorList, DataRow dr, string strError)
		{
            EmployeeError objError = new EmployeeError();
            objError.OrgID = Convert.ToInt32(Request.QueryString.Get("OrgID"));
            objError.ExcelNo = Convert.ToInt32(dr["序号"].ToString().Trim());
            objError.WorkNo = dr["岗位培训合格证编号"].ToString().Trim();
            objError.EmployeeName = dr["姓名"].ToString().Trim();
            objError.Sex = dr["性别"].ToString().Trim();
            objError.OrgPath = dr["车间"].ToString().Trim();
            objError.PostPath = dr["职名"].ToString().Trim();
            objError.ErrorReason = strError;
            objError.OrgName = dr["单位"].ToString().Trim();
            objError.GroupName = dr["班组"].ToString().Trim();
            objError.IdentifyCode = dr["身份证号"].ToString().Trim();
            //objError.PostNo = dr["工作证号"].ToString().Trim();
            //objError.NativePlace = dr["籍贯"].ToString().Trim();
            //objError.Folk = dr["民族"].ToString().Trim();
            //objError.Wedding = dr["婚姻状况"].ToString().Trim();
            objError.PoliticalStatus = dr["政治面貌"].ToString().Trim();
            objError.EducationLevel = dr["文化程度"].ToString().Trim();
            //objError.GraduateUniversity = dr["毕(肄)业学校(单位)"].ToString().Trim();
            //objError.StudyMajor = dr["所学专业"].ToString().Trim();
            //objError.Address = dr["工作地址"].ToString().Trim();
            //objError.EmployeeLevel = dr["职务级别"].ToString().Trim();
            objError.Birthday = dr["出生日期"].ToString().Trim();
            objError.BeginDate = dr["入路时间"].ToString().Trim();
            objError.WorkDate = dr["参加工作时间"].ToString().Trim();
            objError.EmployeeType = dr["职工类型"].ToString().Trim();
            objError.WorkGroupLeader = dr["班组长类型"].ToString().Trim();
            //objError.TeacherType = dr["教师类别"].ToString().Trim();
            objError.OnPost = dr["在岗"].ToString().Trim();
            objError.TechnicalTitle = dr["干部技术职称"].ToString().Trim();
            objError.TechnicalSkill = dr["工人技能等级"].ToString().Trim();
            objError.Address = dr["技术档案编号"].ToString().Trim();
            objError.EducationEmployee = dr["职教人员类型"].ToString().Trim();
            objError.CommitteeHeadShip = dr["职教委员会职务"].ToString().Trim();
            objError.EmployeeTransportType = dr["运输业职工类型"].ToString().Trim();

            objErrorList.Add(objError);
		}


		/// <summary>
		/// 获取当前的单位名称的组织机构信息
		/// </summary>
		/// <returns></returns>
		private Hashtable GetOrgInfo()
		{
			Hashtable htOrg = new Hashtable();

			OrgImportBLL objBll = new OrgImportBLL();
			IList<OrgImport> objList = objBll.GetOrgImport(Convert.ToInt32(Request.QueryString.Get("OrgID")));

			foreach (OrgImport obj in objList)
			{
				htOrg[obj.OrgNamePath] = obj.OrgID;
			}

			return htOrg;
		}

		/// <summary>
		/// 获取当前的单位名称的组织机构信息
		/// </summary>
		/// <returns></returns>
		private Hashtable GetOrgInfo(Database db, DbTransaction trans)
		{
			Hashtable htOrg = new Hashtable();

			OrgImportBLL objBll = new OrgImportBLL();
			IList<OrgImport> objList = objBll.GetOrgImport(db, trans, Convert.ToInt32(Request.QueryString.Get("OrgID")));

			foreach (OrgImport obj in objList)
			{
				htOrg[obj.OrgNamePath] = obj.OrgID;
			}

			return htOrg;
		}

        /// <summary>
        /// 获取当前的单位名称的组织机构信息
        /// </summary>
        /// <returns></returns>
        private Hashtable GetWorkShop()
        {
            Hashtable htOrg = new Hashtable();

            OrganizationBLL objBll = new OrganizationBLL();
            IList<RailExam.Model.Organization> objList =
                objBll.GetOrganizationsByWhereClause("level_num=3 and GetStationOrgID(org_id)=" + Request.QueryString.Get("OrgID"));

            foreach (RailExam.Model.Organization obj in objList)
            {
                htOrg[obj.ShortName] = obj.OrganizationId;
            }

            return htOrg;
        }

        /// <summary>
		/// 获取当前的单位名称的组织机构信息
		/// </summary>
		/// <returns></returns>
		private Hashtable GetWorkShop(Database db, DbTransaction trans)
		{
			Hashtable htOrg = new Hashtable();

			OrganizationBLL objBll = new OrganizationBLL();
			IList<RailExam.Model.Organization> objList =
				objBll.GetOrganizationsByWhereClause(db, trans, "level_num=3 and GetStationOrgID(org_id)=" + Request.QueryString.Get("OrgID"));

			foreach (RailExam.Model.Organization obj in objList)
			{
				htOrg[obj.ShortName] = obj.OrganizationId;
			}

			return htOrg;
		}

        private Hashtable GetWorkGroup()
        {
            Hashtable htOrg = new Hashtable();

            OrganizationBLL objBll = new OrganizationBLL();
            IList<RailExam.Model.Organization> objList =
                objBll.GetOrganizationsByWhereClause("level_num=4 and GetStationOrgID(org_id)=" + Request.QueryString.Get("OrgID"));

            foreach (RailExam.Model.Organization obj in objList)
            {
                htOrg[obj.ShortName] = obj.OrganizationId;
            }

            return htOrg;
        }

		/// <summary>
		/// 职务名称
		/// </summary>
		/// <returns></returns>
		private Hashtable GetPostInfo()
		{
			Hashtable htPostInfo = new Hashtable();
			PostBLL postBLL = new PostBLL();
			IList<RailExam.Model.Post> objPostList = postBLL.GetPostsByLevel(3);

			foreach (RailExam.Model.Post post in objPostList)
			{
				if (!htPostInfo.ContainsKey(post.PostName))
				{
					htPostInfo.Add(post.PostName, post.PostId);
				}
			}

			return htPostInfo;
		}

		/// <summary>
		/// 现文化程度
		/// </summary>
		/// <returns></returns>
		private Hashtable GetEducationLevel()
		{
			EducationLevelBLL objBll = new EducationLevelBLL();
			IList<EducationLevel> objList = objBll.GetAllEducationLevel();

			Hashtable htEducationLevel = new Hashtable();

			foreach (EducationLevel type in objList)
			{
				htEducationLevel.Add(type.EducationLevelName, type.EducationLevelID);
			}
			return htEducationLevel;
		}

		/// <summary>
		/// 政治面貌
		/// </summary>
		/// <returns></returns>
		private Hashtable GetPoliticalStatus()
		{
			PoliticalStatusBLL objBll = new PoliticalStatusBLL();
			IList<PoliticalStatus> objList = objBll.GetAllPoliticalStatus();

			Hashtable htPoliticalStatus = new Hashtable();

			foreach (PoliticalStatus type in objList)
			{
				htPoliticalStatus.Add(type.PoliticalStatusName, type.PoliticalStatusID);
			}
			return htPoliticalStatus;
		}

		/// <summary>
		/// 员工类型
		/// </summary>
		/// <returns></returns>
		private Hashtable GetEmployeeType()
		{
			Hashtable htEmployeeType = new Hashtable();

			htEmployeeType.Add("工人", 0);
			htEmployeeType.Add("技术干部", 1);
            htEmployeeType.Add("管理干部", 2);
            htEmployeeType.Add("其他干部", 3);

			return htEmployeeType;
		}

		/// <summary>
		/// 班组长类型
		/// </summary>
		/// <returns></returns>
		private Hashtable GetWorkGroupLeaderType()
		{
			WorkGroupLeaderLevelBLL objBll = new WorkGroupLeaderLevelBLL();
			IList<WorkGroupLeaderLevel> objList = objBll.GetAllWorkGroupLeaderLevel();

			Hashtable htWorkGroupLeaderType = new Hashtable();

			foreach (WorkGroupLeaderLevel type in objList)
			{
				htWorkGroupLeaderType.Add(type.LevelName, type.WorkGroupLeaderLevelID);
			}
			return htWorkGroupLeaderType;
		}

		/// <summary>
		/// 职教干部类型
		/// </summary>
		/// <returns></returns>
		private Hashtable GetEducationEmployeeType()
		{
			Hashtable htEducationEmployeeType = new Hashtable();
			
            OracleAccess db = new OracleAccess();
		    string strSql = "select * from ZJ_EDUCATION_EMPLOYEE_TYPE";
		    DataSet ds = db.RunSqlDataSet(strSql);

		    foreach (DataRow dr in ds.Tables[0].Rows)
		    {
                htEducationEmployeeType.Add(dr["EDUCATION_EMPLOYEE_TYPE_NAME"].ToString(), dr["EDUCATION_EMPLOYEE_TYPE_ID"].ToString());
            }

			return htEducationEmployeeType;
		}

		/// <summary>
		/// 职教委员会职务

		/// </summary>
		/// <returns></returns>
		private Hashtable GetCommitteeHeadship()
		{
			Hashtable htCommitteeHeadship = new Hashtable();
            OracleAccess db = new OracleAccess();
            string strSql = "select * from ZJ_COMMITTEE_HEAD_SHIP";
            DataSet ds = db.RunSqlDataSet(strSql);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                htCommitteeHeadship.Add(dr["COMMITTEE_HEAD_SHIP_NAME"].ToString(), dr["COMMITTEE_HEAD_SHIP_ID"].ToString());
            }
			return htCommitteeHeadship;
		}

		/// <summary>
		/// 运输业的干部工人标识
		/// </summary>
		/// <returns></returns>
		private Hashtable GetEmployeeTransportType()
		{
			Hashtable htEmployeeTransportType = new Hashtable();
            htEmployeeTransportType.Add("生产人员", 0);
			htEmployeeTransportType.Add("管理人员", 1);
			htEmployeeTransportType.Add("服务人员", 2);
            htEmployeeTransportType.Add("其他人员", 3);

			return htEmployeeTransportType;
		}

        private Hashtable GetUniversityType()
        {
            Hashtable hfUniversityType = new Hashtable();
            hfUniversityType.Add("全日制", 1);
            hfUniversityType.Add("网络教育", 2);
            hfUniversityType.Add("自学考试", 3);
            hfUniversityType.Add("党校函授", 4);
            hfUniversityType.Add("函授学习", 5);
            hfUniversityType.Add("电大学习", 6);
            hfUniversityType.Add("职校学习", 7);
            hfUniversityType.Add("业校学习", 8);
            hfUniversityType.Add("夜校学习", 9);
            hfUniversityType.Add("成人教育", 10);
            return hfUniversityType;
        }

		/// <summary>
		///现技术职务名称

		/// </summary>
		/// <returns></returns>
		private Hashtable GetTechnicalTitle()
		{
			TechnicianTitleTypeBLL objBll = new TechnicianTitleTypeBLL();
			IList<TechnicianTitleType> objList = objBll.GetAllTechnicianTitleType();

			Hashtable htTechnicalTitle = new Hashtable();

			foreach (TechnicianTitleType type in objList)
			{
				htTechnicalTitle.Add(type.TypeName, type.TechnicianTitleTypeID);
			}
			return htTechnicalTitle;
		}

		/// <summary>
		/// 技术等级

		/// </summary>
		/// <returns></returns>
		private Hashtable GetSkillLevel()
		{
			TechnicianTypeBLL objBll = new TechnicianTypeBLL();
			IList<TechnicianType> objList = objBll.GetAllTechnicianType();

			Hashtable htSkillLevel = new Hashtable();

			foreach (TechnicianType type in objList)
			{
				htSkillLevel.Add(type.TypeName, type.TechnicianTypeID);
			}
			return htSkillLevel;
		}


		public static string EnhancedStackTrace(Exception ex)
		{
			return EnhancedStackTrace(new StackTrace(ex, true));
		}

		public static string EnhancedStackTrace(StackTrace st)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(Environment.NewLine);
			sb.Append("---- Stack Trace ----");
			sb.Append(Environment.NewLine);
			for (int i = 0; i < st.FrameCount; i++)
			{
				StackFrame sf = st.GetFrame(i);
				MemberInfo mi = sf.GetMethod();
				sb.Append(StackFrameToString(sf));
			}
			sb.Append(Environment.NewLine);
			return sb.ToString();
		}

		public static string StackFrameToString(StackFrame sf)
		{
			StringBuilder sb = new StringBuilder();
			int intParam;
			MemberInfo mi = sf.GetMethod();
			sb.Append("   ");
			sb.Append(mi.DeclaringType.Namespace);
			sb.Append(".");
			sb.Append(mi.DeclaringType.Name);
			sb.Append(".");
			sb.Append(mi.Name);
			sb.Append("(");
			intParam = 0;
			foreach (ParameterInfo param in sf.GetMethod().GetParameters())
			{
				intParam += 1;
				sb.Append(param.Name);
				sb.Append(" As ");
				sb.Append(param.ParameterType.Name);
			}
			sb.Append(")");
			sb.Append(Environment.NewLine);
			sb.Append("       ");
			if (string.IsNullOrEmpty(sf.GetFileName()))
			{
				sb.Append("(unknown file)");
				sb.Append(": N ");
				sb.Append(String.Format("{0:#00000}", sf.GetNativeOffset()));
			}
			else
			{
				sb.Append(System.IO.Path.GetFileName(sf.GetFileName()));
				sb.Append(": line ");
				sb.Append(String.Format("{0:#0000}", sf.GetFileLineNumber()));
				sb.Append(", col ");
				sb.Append(String.Format("{0:#00}", sf.GetFileColumnNumber()));
				if (sf.GetILOffset() != StackFrame.OFFSET_UNKNOWN)
				{
					sb.Append(", IL ");
					sb.Append(String.Format("{0:#0000}", sf.GetILOffset()));
				}
			}
			sb.Append(Environment.NewLine);
			return sb.ToString();
		}
	}
}

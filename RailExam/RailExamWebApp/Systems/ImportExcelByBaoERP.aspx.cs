using System;
using System.Data;
using System.Collections;
using RailExam.BLL;
using System.Collections.Generic;
using RailExam.Model;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Systems
{
    /// <summary>
    /// 人员信息Excel导入（Excel来自于包神铁路ERP系统）
    /// </summary>
    public partial class ImportExcelByBaoERP : System.Web.UI.Page
    {
        // 必备列
        private static readonly string[] requistieColumns = { 
            "序号", "单位", "部门", "岗位名称", "员工组", "员工编码", "姓名", "性别", "籍贯", "出生日期", 
            "身份证件号", "参加工作时间", "进入本公司时间", "政治面貌", "最高学历" ,"在岗","在册"
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ImportEmployeeExcel();
            }
        }

        #region 读取Excel

        // 获取Excel文件中的第一个Sheet
        private ISheet GetSheet(string path)
        {
            using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                HSSFWorkbook hssfWorkbook = new HSSFWorkbook(file);
                return hssfWorkbook.GetSheetAt(0);
            }
        }

        // 根据Excel的列头，构造DataTable列
        private void InitDataTableColumns(DataTable dt, IRow row)
        {
            for (int i = 0; i < row.LastCellNum; i++)
            {
                ICell cell = row.GetCell(i);
                if (cell != null)
                {
                    dt.Columns.Add(cell.ToString().Replace("\n", string.Empty));
                }
            }
        }

        // 获取第一行（列头行）
        private DataTable GetHeadLine(ISheet sheet)
        {
            IRow row = sheet.GetRow(0);    // 读取第一行
            DataTable dt = new DataTable();   // 用于返回值的DataTable对象
            // 构造DataTable的表结构
            InitDataTableColumns(dt, row);
            // 将读取到的IRow对象写入DataTable中
            DataRow dr = dt.NewRow();
            for (int i = 0, j = 0; i < row.LastCellNum; i++)
            {
                try
                {
                    ICell cell = row.GetCell(i);
                    if (cell != null)
                    {
                        dr[j] = cell.ToString().Replace("\n", "");
                        j++;
                    }
                }
                catch { break; } // 这里没必要做任何其它的处理
            }
            dt.Rows.Add(dr);

            return dt;
        }

        // 验证列头行
        private bool ValidateHeadLine(DataTable dtHeadLine, out string error)
        {
            error = CheckContain(requistieColumns, dtHeadLine);
            return string.IsNullOrEmpty(error);
        }

        // 验证必备列(用于验证列头行里调用)
        private string CheckContain(string[] str, DataTable dtInfo)
        {
            string errorMessage = "";

            for (int i = 0; i < str.Length; i++)
            {
                if (!dtInfo.Columns.Contains(str[i]))
                {
                    errorMessage += errorMessage == string.Empty ? str[i] : "," + str[i];
                }
            }

            return errorMessage;
        }

        // 读取数据行
        private DataTable GetDataRows(ISheet sheet, string jsBlock)
        {
            DataTable dt = new DataTable();   // 用于返回值的DataTable对象

            IEnumerator rows = sheet.GetRowEnumerator();   // 获取对行的枚举器
            rows.MoveNext(); // 跳过首行（列头行）。
            IRow row = (HSSFRow)rows.Current;    // 读取第一行, 用于确定表结构。

            // 构造DataTable的表结构
            InitDataTableColumns(dt, row);

            // 根据 ProgressBar.htm 显示进度条界面
            string templateFileName = Server.MapPath("/RailExamBao/RandomExam/ProgressBar.htm");
            StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
            string html = reader.ReadToEnd();
            reader.Close();
            Response.Write(html);
            Response.Flush();
            System.Threading.Thread.Sleep(200);

            // 循环读取数据行
            int index = 1;
            while (rows.MoveNext())
            {
                if (rows.Current == null) { break; } // 遇空行结束
                IRow rowCurr = (HSSFRow)rows.Current;
                DataRow dr = dt.NewRow();

                for (int i = 0, j = 0; i < rowCurr.LastCellNum; i++)
                {
                    ICell cell = rowCurr.GetCell(i);
                    if (cell != null)
                    {
                        dr[j] = cell.ToString();
                    }
                    else
                    {
                        dr[j] = "";
                    }
                    j++;
                }
                dt.Rows.Add(dr);

                // 滚动条效果
                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('正在读取Excel文件','" + ((double)((index++) * 100) / (double)sheet.LastRowNum).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();
            }

            return dt;
        }

        #endregion

        // 导入 
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
            string jsBlock = string.Empty;
            string strPath = Server.MapPath("/RailExamBao/Excel/" + strFileName);
            bool isClose = false;
            string strMessage;

            objErrorBll.DeleteEmployeeErrorByOrgIDAndImportTypeID(Convert.ToInt32(orgID));

            #region 验证准备工作

            Hashtable htOrg = GetOrgInfo();
            Hashtable htPost = GetPostInfo();
            Hashtable htEducationLevel = GetEducationLevel();
            Hashtable htPoliticalStatus = GetPoliticalStatus();
            Hashtable htShopNeedAdd = new Hashtable();
            Hashtable htPostNeedAdd = new Hashtable();
            Hashtable htIdentityCardNo = new Hashtable();
            Hashtable htERP = new Hashtable();

            PostBLL objPostBll = new PostBLL();
            ArrayList objPostNewList = new ArrayList();

            IList<RailExam.Model.EmployeeDetail> objEmployeeInsert = new List<RailExam.Model.EmployeeDetail>();
            IList<RailExam.Model.EmployeeDetail> objEmployeeUpdate = new List<RailExam.Model.EmployeeDetail>();

            #endregion

            // 获取Excel列头数据
            ISheet sheet = GetSheet(strPath);
            // 验证列头
            DataTable headLine = GetHeadLine(sheet);
            string error;
            if (!ValidateHeadLine(headLine, out error))
            {
                Response.Write("<script>window.returnValue='" + error + "',window.close();</script>");
                return;
            }
            // 获取Excel数据行信息
            DataTable excelDataRows = GetDataRows(sheet, jsBlock);

            // 遍历、验证、保存

            // 添加滚动条效果
            System.Threading.Thread.Sleep(10);
            jsBlock = "<script>SetPorgressBar('正准备检测Excel数据','0.00'); </script>";
            Response.Write(jsBlock);
            Response.Flush();

            if (excelDataRows.Rows.Count == 0)
            {
                Response.Write("<script>window.returnValue='Excel中没有任何记录，请核对',window.close();</script>");
                return;
            }

            for (int i = 0; i < excelDataRows.Rows.Count; i++)
            {
                // 滚动条效果
                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('正在检测Excel数据','" +
                          ((double)((i + 1) * 100) / (double)excelDataRows.Rows.Count).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                RailExam.Model.EmployeeDetail objEmployee = new RailExam.Model.EmployeeDetail();
                bool isUpdate = false;
                DataRow dr = excelDataRows.Rows[i];

                //if (dr["员工ID"] != DBNull.Value && dr["员工ID"].ToString().Trim() != string.Empty)
                //{
                //    isUpdate = false;
                //    try
                //    {
                //        objEmployee.EmployeeID = Convert.ToInt32(dr["员工ID"].ToString().Trim());
                //    }
                //    catch
                //    {
                //        AddError(objErrorList, dr, "员工ID填写错误");
                //        continue;
                //    }               
                //}
                //else
                //{
                //    isUpdate = true;
                //}

                // 单位
                if (dr["单位"].ToString().Trim() != strUnitName &&
                    dr["单位"].ToString().Trim().Replace("神华包神铁路有限责任公司", "") != strUnitName)
                {
                    AddError(objErrorList, dr, "单位填写错误");
                    continue;
                }


                // 部门
                if (dr["部门"].ToString().Trim() == "")
                {
                    AddError(objErrorList, dr, "部门不能为空");
                    continue;
                }

                string strWorkShop = dr["部门"].ToString().Trim();
                if (strWorkShop.IndexOf("包神铁路公司" + strUnitName) >= 0)
                {
                    strWorkShop = dr["部门"].ToString().Trim().Replace("包神铁路公司" + strUnitName, "");
                }

                //组织机构
                string strOrg;
                if (dr["员工组"].ToString().Trim() == string.Empty)
                {
                    strOrg = strUnitName + "-" + strWorkShop;
                }
                else
                {
                    strOrg = strUnitName + "-" + strWorkShop + "-" + dr["员工组"].ToString().Trim();
                }
                // 判断上面拼接的字符串是否存在于组织机构中
                if (!htOrg.ContainsKey(strOrg))
                {
                    if (dr["员工组"].ToString().Trim() == string.Empty)
                    {
                        if (!htShopNeedAdd.ContainsKey(strWorkShop))
                        {
                            htShopNeedAdd.Add(strWorkShop, new Hashtable());
                        }

                        //如果组织机构需要新增
                        objEmployee.Memo = strOrg;
                    }
                    else
                    {
                        if (!htShopNeedAdd.ContainsKey(strWorkShop))
                        {
                            htShopNeedAdd.Add(strWorkShop, new Hashtable());
                        }

                        Hashtable htGroupNeedAdd = (Hashtable)htShopNeedAdd[strWorkShop];
                        if (!htGroupNeedAdd.ContainsKey(dr["员工组"].ToString().Trim()))
                        {
                            htGroupNeedAdd.Add(dr["员工组"].ToString().Trim(), dr["员工组"].ToString().Trim());
                            htShopNeedAdd[strWorkShop] = htGroupNeedAdd;
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
                // 姓名
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

                    objEmployee.EmployeeName = dr["姓名"].ToString().Trim();
                    objEmployee.PinYinCode = Pub.GetChineseSpell(dr["姓名"].ToString().Trim());
                }

                //身份证件号
                if (dr["身份证件号"].ToString().Trim() == string.Empty && dr["员工编码"].ToString().Trim() == string.Empty)
                {
                    AddError(objErrorList, dr, "身份证件号和员工编码不能同时为空");
                    continue;
                }
                else if (dr["身份证件号"].ToString().Trim() != string.Empty)
                {
                    if (dr["员工编码"].ToString().Trim() != string.Empty)
                    {
                        IList<RailExam.Model.Employee> objOldList =
                            objEmployeeBll.GetEmployeeByWhereClause("Work_NO='" + dr["员工编码"].ToString().Trim() + "' and Employee_Name='" + dr["姓名"].ToString().Trim() + "' and Identity_CardNo='" + dr["身份证件号"].ToString().Trim() + "'");
                        if (objOldList.Count > 0)
                        {
                            isUpdate = true;
                            objEmployee.EmployeeID = objOldList[0].EmployeeID;
                        }
                        else
                        {
                            isUpdate = false;
                        }

                        if(!isUpdate)
                        {
                            IList<RailExam.Model.Employee> objNowList = objEmployeeBll.GetEmployeeByWhereClause("Work_NO='" + dr["员工编码"].ToString().Trim() + "'");
                            if (objNowList.Count > 0)
                            {
                                AddError(objErrorList, dr, "员工编码与系统中【" + objNowList[0].OrgName + "的" + objNowList[0].EmployeeName + "】的员工编号重复");
                                continue;
                            }

                        }


                        //员工编码在Excel中不能重复
                        if (htERP.ContainsKey(dr["员工编码"].ToString().Trim()))
                        {
                            AddError(objErrorList, dr, "员工编码在Excel中与序号为" + htERP[dr["员工编码"].ToString().Trim()] + "的员工编码重复");
                            continue;
                        }
                        else
                        {
                            htERP.Add(dr["员工编码"].ToString().Trim(), dr["序号"].ToString().Trim());
                        }

                        objEmployee.WorkNo = dr["员工编码"].ToString().Trim();
                    }
                    else
                    {
                        IList<RailExam.Model.Employee> objOldList =
                            objEmployeeBll.GetEmployeeByWhereClause("Employee_Name='" + dr["姓名"].ToString().Trim() + "' and Identity_CardNo='" + dr["身份证件号"].ToString().Trim() + "'");
                        if (objOldList.Count > 0)
                        {
                            isUpdate = true;
                            objEmployee.EmployeeID = objOldList[0].EmployeeID;
                        }
                        else
                        {
                            isUpdate = false;
                        }
                    }

                    if (dr["身份证件号"].ToString().Trim().Length > 18)
                    {
                        AddError(objErrorList, dr, "身份证件号不能超过18位");
                        continue;
                    }

                    //身份证件号在Excel中不能重复
                    if (htIdentityCardNo.ContainsKey(dr["身份证件号"].ToString().Trim()))
                    {
                        AddError(objErrorList, dr, "身份证件号在Excel中与序号为" + htIdentityCardNo[dr["身份证件号"].ToString().Trim()] + "的身份证件号重复");
                        continue;
                    }
                    else
                    {
                        htIdentityCardNo.Add(dr["身份证件号"].ToString().Trim(), dr["序号"].ToString().Trim());
                    }


                    IList<RailExam.Model.Employee> objList =
                         objEmployeeBll.GetEmployeeByWhereClause("identity_CardNo='" + dr["身份证件号"].ToString().Trim() + "'");
                    if (objList.Count > 0)
                    {
                        if (isUpdate)
                        {
                            if (objList.Count > 1)
                            {
                                AddError(objErrorList, dr, "与该员工身份证件号完全相同的员工已经存在");
                                continue;
                            }
                        }
                        else
                        {
                            AddError(objErrorList, dr, "该员工身份证件号与【" + objList[0].OrgName + "】中【" + objList[0].EmployeeName + "】的身份证件号完全相同");
                            continue;
                        }
                    }

                    objEmployee.IdentifyCode = dr["身份证件号"].ToString().Trim();
                }
                else if (dr["身份证件号"].ToString().Trim() == string.Empty)
                {
                    IList<RailExam.Model.Employee> objOldList =
                            objEmployeeBll.GetEmployeeByWhereClause("Work_NO='" + dr["员工编码"].ToString().Trim() + "' and Employee_Name='" + dr["姓名"].ToString().Trim() + "'");
                    if (objOldList.Count > 0)
                    {
                        isUpdate = true;
                        objEmployee.EmployeeID = objOldList[0].EmployeeID;
                    }
                    else
                    {
                        isUpdate = false;
                    }

                    //员工编码在Excel中不能重复
                    if (htERP.ContainsKey(dr["员工编码"].ToString().Trim()))
                    {
                        AddError(objErrorList, dr, "员工编码在Excel中与序号为" + htERP[dr["员工编码"].ToString().Trim()] + "的员工编码重复");
                        continue;
                    }
                    else
                    {
                        htERP.Add(dr["员工编码"].ToString().Trim(), dr["序号"].ToString().Trim());
                    }

                    objEmployee.WorkNo = dr["员工编码"].ToString().Trim();
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
                //籍贯
                if (dr["籍贯"].ToString().Trim().Length > 20)
                {
                    AddError(objErrorList, dr, "籍贯不能超过20位");
                    continue;
                }
                else
                {
                    objEmployee.NativePlace = dr["籍贯"].ToString().Trim();
                }
                //最高学历
                if (dr["最高学历"].ToString().Trim() == string.Empty)
                {
                    AddError(objErrorList, dr, "最高学历不能为空");
                    continue;
                }
                else
                {
                    if (!htEducationLevel.ContainsKey(dr["最高学历"].ToString().Trim()))
                    {
                        AddError(objErrorList, dr, "最高学历在系统中不存在");
                        continue;
                    }
                    else
                    {
                        objEmployee.EducationLevelID = Convert.ToInt32(htEducationLevel[dr["最高学历"].ToString().Trim()]);
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
                        string strBirth = dr["出生日期"].ToString().Trim().Replace(".", "");
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

                        if (Convert.ToDateTime(strBirth) < Convert.ToDateTime("1900-1-1") ||
                            Convert.ToDateTime(strBirth) > Convert.ToDateTime("2000-12-31"))
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

                //进入本公司时间
                if (dr["进入本公司时间"].ToString().Trim() == string.Empty)
                {
                    AddError(objErrorList, dr, "进入本公司时间不能为空");
                    continue;
                }
                else
                {
                    try
                    {
                        string strJoin = dr["进入本公司时间"].ToString().Trim().Replace(".", "");
                        if (strJoin.IndexOf("-") >= 0)
                        {
                            objEmployee.WorkDate = Convert.ToDateTime(strJoin);
                        }
                        else
                        {
                            if (strJoin.Length != 8)
                            {
                                AddError(objErrorList, dr, "进入本公司时间填写错误");
                                continue;
                            }
                            else
                            {
                                strJoin = strJoin.Insert(4, "-");
                                strJoin = strJoin.Insert(7, "-");
                                objEmployee.WorkDate = Convert.ToDateTime(strJoin);
                            }
                        }

                        if (Convert.ToDateTime(strJoin) < Convert.ToDateTime("1900-1-1"))
                        {
                            AddError(objErrorList, dr, "进入本公司时间填写错误");
                            continue;
                        }
                    }
                    catch
                    {
                        AddError(objErrorList, dr, "进入本公司时间填写错误");
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
                        string strJoin = dr["参加工作时间"].ToString().Trim().Replace(".", "");
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

                        if (Convert.ToDateTime(strJoin) < Convert.ToDateTime("1900-1-1"))
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
                //职名
                if (dr["岗位名称"].ToString().Trim() == string.Empty)
                {
                    AddError(objErrorList, dr, "岗位名称不能为空！");
                    continue;
                }
                else
                {
                    IList<RailExam.Model.Post> objPost =
                        objPostBll.GetPostsByWhereClause("Post_Level=3 and Post_Name='" + dr["岗位名称"].ToString().Trim() + "'");
                    if (objPost.Count == 0)
                    {
                        if (htPostNeedAdd.ContainsKey(dr["岗位名称"].ToString().Trim()))
                        {
                            objPostNewList = (ArrayList)htPostNeedAdd[dr["岗位名称"].ToString().Trim()];
                            int x;
                            if (isUpdate)
                            {
                                x = objEmployeeUpdate.Count;
                            }
                            else
                            {
                                x = objEmployeeInsert.Count;
                            }
                            objPostNewList.Add(x+ "|" + isUpdate);
                            htPostNeedAdd[dr["岗位名称"].ToString().Trim()] = objPostNewList;
                        }
                        else
                        {
                            objPostNewList = new ArrayList();
                            int x;
                            if(isUpdate)
                            {
                                x = objEmployeeUpdate.Count;
                            }
                            else
                            {
                                x = objEmployeeInsert.Count;
                            }
                            objPostNewList.Add(x+"|" + isUpdate);
                            htPostNeedAdd.Add(dr["岗位名称"].ToString().Trim(), objPostNewList);
                        }
                    }

                    objEmployee.PostID = Convert.ToInt32(htPost[dr["岗位名称"].ToString().Trim()]);
                }
                // 因dateAWARD_DATE未加入对象中， 所以此处缺失验证进入神华系统时间

                if (dr["在岗"] == DBNull.Value || dr["在岗"].ToString() == "" || dr["在岗"].ToString() == "否" || dr["在岗"].ToString() == "0")
                {
                    objEmployee.IsOnPost = false;
                }
                else if (dr["在岗"] != DBNull.Value && (dr["在岗"].ToString() == "是" || dr["在岗"].ToString() == "1"))
                {
                    objEmployee.IsOnPost = true;
                }

                if (dr["在册"] == DBNull.Value || dr["在册"].ToString() == "" || dr["在册"].ToString() == "否" || dr["在册"].ToString() == "0")
                {
                    objEmployee.Dimission = false;
                }
                else if (dr["在册"] != DBNull.Value && (dr["在册"].ToString() == "是" || dr["在册"].ToString() == "1"))
                {
                    objEmployee.Dimission = true;
                }

                // 赋值默认值
                if (!isUpdate)
                {
                    //职工类型
                    objEmployee.EmployeeTypeID = 0;
                    // 在册
                    objEmployee.Dimission = true;
                    // 在岗
                    objEmployee.IsOnPost = true;
                    // 运输业职工类型
                    objEmployee.EmployeeTransportTypeID = 0;

                    objEmployeeInsert.Add(objEmployee);
                }
                else
                {
                    objEmployeeUpdate.Add(objEmployee);
                }
            }

            // 处理完成
            jsBlock = "<script>SetCompleted('Excel数据检测完毕'); </script>";
            Response.Write(jsBlock);
            Response.Flush();

            if (objErrorList.Count > 0)
            {
                // 处理完成
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
                    objPost.ParentId = 1550;    // 649通用-其他, 1550 其他-其他
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
                        if (strPost[1] == "false" || strPost[1] == "False")
                        {
                            objEmployeeInsert[Convert.ToInt32(strPost[0])].PostID = postID;
                        }
                        else
                        {
                            objEmployeeUpdate[Convert.ToInt32(strPost[0])].PostID = postID;
                        }
                    }

                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('正在导入职名','" + ((double)(count * 100) / (double)(htPostNeedAdd.Count)).ToString("0.00") + "'); </script>";
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

                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('正在导入员工信息','" + ((double)(count * 100) / (double)(objEmployeeInsert.Count + objEmployeeUpdate.Count)).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                    count = count + 1;
                }


                for (int i = 0; i < objEmployeeUpdate.Count; i++)
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
            objError.WorkNo = dr["员工编码"].ToString().Trim();
            objError.EmployeeName = dr["姓名"].ToString().Trim();
            objError.Sex = dr["性别"].ToString().Trim();
            objError.OrgPath = dr["部门"].ToString().Trim();
            objError.PostPath = dr["岗位名称"].ToString().Trim();
            objError.ErrorReason = strError;
            objError.OrgName = dr["单位"].ToString().Trim();
            objError.GroupName = dr["员工组"].ToString().Trim();
            objError.IdentifyCode = dr["身份证件号"].ToString().Trim();
            objError.NativePlace = dr["籍贯"].ToString().Trim();
            objError.PoliticalStatus = dr["政治面貌"].ToString().Trim();
            objError.EducationLevel = dr["最高学历"].ToString().Trim();
            objError.Birthday = dr["出生日期"].ToString().Trim();
            objError.BeginDate = dr["进入本公司时间"].ToString().Trim();
            objError.WorkDate = dr["参加工作时间"].ToString().Trim();

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
        /// 最高学历
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
    }
}

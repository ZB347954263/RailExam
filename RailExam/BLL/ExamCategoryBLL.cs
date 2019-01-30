using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using System.Collections;

namespace RailExam.BLL
{
    public class ExamCategoryBLL
    {
        private static readonly ExamCategoryDAL dal = new ExamCategoryDAL();
        private SystemLogBLL objLogBll = new SystemLogBLL();

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="ExamCategoryId"></param>
        /// <param name="parentId"></param>
        /// <param name="idPath"></param>
        /// <param name="levelNum"></param>
        /// <param name="orderIndex"></param>
        /// <param name="CategoryName"></param>
        /// <param name="description"></param>
        /// <param name="memo"></param>
        /// <param name="startRowIndex">起始记录行</param>
        /// <param name="maximumRows">每页记录条数</param>
        /// <param name="orderBy">排序字符串，如"FieldName ASC"</param>
        /// <returns></returns>
        public IList<ExamCategory> GetExamCategory(int ExamCategoryId, int parentId, string idPath, int levelNum, int orderIndex,
            string CategoryName, string description, string memo, int startRowIndex, int maximumRows, string orderBy)
        {
            IList<ExamCategory> examCategoryList = dal.GetExamCategory(ExamCategoryId, parentId, idPath, levelNum, orderIndex,
                                                CategoryName, description, memo, startRowIndex, maximumRows, orderBy);

            return examCategoryList;
        }

        public ExamCategory GetExamCategory(int ExamCategoryId)
        {
            if (ExamCategoryId < 1)
            {
                return null;
            }

            return dal.GetExamCategory(ExamCategoryId);
        }

        /// <summary>
        /// 新增知识类别
        /// </summary>
        /// <param name="examCategory">新增的知识类别信息</param>
        /// <returns></returns>
        public int AddExamCategory(ExamCategory examCategory)
        {
            int  id= dal.AddExamCategory(examCategory);
            objLogBll.WriteLog("新增考试类别“"+ examCategory.CategoryName +"”基本信息");
            return id;
        }

        public IList<ExamCategory> GetExamCategories()
        {
            return dal.GetExamCategories();
        }

        /// <summary>
        /// 更新知识类别
        /// </summary>
        /// <param name="examCategory">更新后的知识类别信息</param>
        public void UpdateExamCategory(ExamCategory examCategory)
        {
            dal.UpdateExamCategory(examCategory);
            objLogBll.WriteLog("修改考试类别“" + examCategory.CategoryName + "”基本信息");
        }

        /// <summary>
        /// 删除知识类别
        /// </summary>
        /// <param name="examCategory">要删除的知识类别</param>
        public void DeleteExamCategory(ExamCategory examCategory)
        {
            string strName = GetExamCategory(examCategory.ExamCategoryId).CategoryName;
            objLogBll.WriteLog("新增考试类别“" + strName + "”基本信息");
            dal.DeleteExamCategory(examCategory.ExamCategoryId);
        }

        /// <summary>
        /// 删除知识类别
        /// </summary>
        /// <param name="ExamCategoryId">要删除的知识类别ID</param>
        public void DeleteExamCategory(int ExamCategoryId)
        {
            string strName = GetExamCategory(ExamCategoryId).CategoryName;
            objLogBll.WriteLog("新增考试类别“" + strName + "”基本信息");
            dal.DeleteExamCategory(ExamCategoryId);
        }

        public bool MoveUp(int ExamCategoryId)
        {
            return dal.Move(ExamCategoryId, true);
        }

        public bool MoveDown(int ExamCategoryId)
        {
            return dal.Move(ExamCategoryId, false);
        }
    }
}

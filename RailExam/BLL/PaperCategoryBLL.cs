using System.Collections.Generic;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    public class PaperCategoryBLL
    {
        private static readonly PaperCategoryDAL dal = new PaperCategoryDAL();
        private SystemLogBLL objLogBll = new SystemLogBLL();

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="PaperCategoryId"></param>
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
        public IList<PaperCategory> GetPaperCategories(int PaperCategoryId, int parentId, string idPath, int levelNum, int orderIndex,
            string CategoryName, string description, string memo,  int startRowIndex, int maximumRows, string orderBy)
        {
            IList<PaperCategory> paperCategoryList = dal.GetPaperCategories(PaperCategoryId, parentId, idPath, levelNum, orderIndex,
                                     CategoryName, description, memo, startRowIndex, maximumRows, orderBy);

            return paperCategoryList;
        }

        public IList<PaperCategory> GetPaperCategories(int PaperCategoryId)
        {
            IList<PaperCategory> paperCategoryList = dal.GetPaperCategories(PaperCategoryId);

            return paperCategoryList;
        }

        public IList<PaperCategory> GetPaperCategoriesByIDPath(string idPath)
        {
            return dal.GetPaperCategoriesByIDPath(idPath);
        }

        /// <summary>
        /// 获取所有试卷类别
        /// </summary>
        /// <returns>所有试卷类别</returns>
        public IList<PaperCategory> GetPaperCategories()
        {
            return dal.GetPaperCategories();
        }

        /// <summary>
        /// 作业类别是否作为查询使用
        /// </summary>
        /// <param name="bForSearchUse">
        /// 是否供查询用，对于：
        /// 一、是，则添加一提示项；
        /// 二、否，则不添加提示项；
        /// </param>
        /// <returns>作业类别</returns>
        public IList<PaperCategory> GetTaskCategories(bool bForSearchUse)
        {
            IList<PaperCategory> paperCategories = new List<PaperCategory>(dal.GetTaskCategories());

            if (bForSearchUse)
            {
                PaperCategory paperCategory = new PaperCategory();

                paperCategory.CategoryName = "-状态-";
                paperCategory.PaperCategoryId = -1;
                paperCategories.Insert(0, paperCategory);
            }

            return paperCategories;
        }

        public PaperCategory GetPaperCategory(int PaperCategoryId)
        {
            if (PaperCategoryId < 1)
            {
                return null;
            }

            return dal.GetPaperCategory(PaperCategoryId);
        }

        /// <summary>
        /// 新增试卷类别
        /// </summary>
        /// <param name="paperCategory">新增的试卷类别信息</param>
        /// <returns></returns>
        public int AddPaperCategory(PaperCategory paperCategory)
        {
            objLogBll.WriteLog("新增试卷类别“" + paperCategory.CategoryName + "”基本信息");
            return dal.AddPaperCategory(paperCategory);
        }

        /// <summary>
        /// 更新试卷类别
        /// </summary>
        /// <param name="paperCategory">更新后的试卷类别信息</param>
        public void UpdatePaperCategory(PaperCategory paperCategory)
        {
            objLogBll.WriteLog("修改试卷类别“" + paperCategory.CategoryName + "”基本信息");
            dal.UpdatePaperCategory(paperCategory);
        }

        /// <summary>
        /// 删除试卷类别
        /// </summary>
        /// <param name="paperCategory">要删除的试卷类别</param>
        public void DeletePaperCategory(PaperCategory paperCategory)
        {
            DeletePaperCategory(paperCategory.PaperCategoryId);
        }

        /// <summary>
        /// 删除试卷类别
        /// </summary>
        /// <param name="PaperCategoryId">要删除的试卷类别ID</param>
        public void DeletePaperCategory(int PaperCategoryId)
        {
            string strName = GetPaperCategory(PaperCategoryId).CategoryName;
            objLogBll.WriteLog("修改试卷类别“" + strName + "”基本信息");
            dal.DeletePaperCategory(PaperCategoryId);
        }

        public bool MoveUp(int PaperCategoryId)
        {
            return dal.Move(PaperCategoryId, true);
        }

        public bool MoveDown(int PaperCategoryId)
        {
            return dal.Move(PaperCategoryId, false);
        }
    }
}
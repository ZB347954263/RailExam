using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using System.Collections;

namespace RailExam.BLL
{
    public class RegulationCategoryBLL
    {
        private static readonly RegulationCategoryDAL dal = new RegulationCategoryDAL();
        private SystemLogBLL objLogBll = new SystemLogBLL();

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="regulationCategoryID"></param>
        /// <param name="parentID"></param>
        /// <param name="idPath"></param>
        /// <param name="levelNum"></param>
        /// <param name="orderIndex"></param>
        /// <param name="categoryName"></param>
        /// <param name="description"></param>
        /// <param name="memo"></param>
        /// <param name="startRowIndex">起始记录行</param>
        /// <param name="maximumRows">每页记录条数</param>
        /// <param name="orderBy">排序字符串，如"FieldName ASC"</param>
        /// <returns></returns>
        public IList<RegulationCategory> GetRegulationCategories(int regulationCategoryID, int parentID, string idPath, int levelNum, int orderIndex,
            string categoryName, string description, string memo, int startRowIndex, int maximumRows, string orderBy)
        {
            IList<RegulationCategory> regulationCategoryList = dal.GetRegulationCategories(regulationCategoryID, parentID,
                idPath, levelNum, orderIndex, categoryName, description, memo, startRowIndex, maximumRows, orderBy);

            return regulationCategoryList;
        }

        public IList<RegulationCategory> GetRegulationCategories()
        {
            return dal.GetRegulationCategories();
        }

        public RegulationCategory GetRegulationCategory(int regulationCategoryID)
        {
            if (regulationCategoryID < 1)
            {
                return null;
            }

            return dal.GetRegulationCategory(regulationCategoryID);
        }

        /// <summary>
        /// 新增知识类别
        /// </summary>
        /// <param name="regulationCategory">新增的知识类别信息</param>
        /// <returns></returns>
        public int AddRegulationCategory(RegulationCategory regulationCategory)
        {
            objLogBll.WriteLog("新增法规类别“"+ regulationCategory.CategoryName+"”基本信息");
            return dal.AddRegulationCategory(regulationCategory);
        }

        /// <summary>
        /// 更新知识类别
        /// </summary>
        /// <param name="regulationCategory">更新后的知识类别信息</param>
        public void UpdateRegulationCategory(RegulationCategory regulationCategory)
        {
            objLogBll.WriteLog("修改法规类别“" + regulationCategory.CategoryName + "”基本信息");
            dal.UpdateRegulationCategory(regulationCategory);
        }

        /// <summary>
        /// 删除知识类别
        /// </summary>
        /// <param name="regulationCategory">要删除的知识类别</param>
        public void DeleteRegulationCategory(RegulationCategory regulationCategory)
        {
            DeleteRegulationCategory(regulationCategory.RegulationCategoryID);
        }

        /// <summary>
        /// 删除知识类别
        /// </summary>
        /// <param name="regulationCategoryID">要删除的知识类别ID</param>
        public void DeleteRegulationCategory(int regulationCategoryID)
        {
            string strName = GetRegulationCategory(regulationCategoryID).CategoryName;
            objLogBll.WriteLog("删除法规类别“" + strName + "”基本信息");
            dal.DeleteRegulationCategory(regulationCategoryID);
        }
    }
}

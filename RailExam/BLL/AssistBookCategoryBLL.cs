using System.Collections.Generic;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    public class AssistBookCategoryBLL
    {
        private static readonly AssistBookCategoryDAL dal = new AssistBookCategoryDAL();
        private SystemLogBLL objLogBll = new SystemLogBLL();

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="assistBookCategoryId"></param>
        /// <param name="parentId"></param>
        /// <param name="idPath"></param>
        /// <param name="levelNum"></param>
        /// <param name="orderIndex"></param>
        /// <param name="assistBookCategoryName"></param>
        /// <param name="description"></param>
        /// <param name="memo"></param>
        /// <param name="startRowIndex">起始记录行</param>
        /// <param name="maximumRows">每页记录条数</param>
        /// <param name="orderBy">排序字符串，如"FieldName ASC"</param>
        /// <returns></returns>
        public IList<AssistBookCategory> GetAssistBookCategorys(int assistBookCategoryId, int parentId, string idPath, int levelNum, int orderIndex,
            string assistBookCategoryName, string description, string memo, int startRowIndex, int maximumRows, string orderBy)
        {
            IList<AssistBookCategory> assistBookCategoryList = dal.GetAssistBookCategorys(assistBookCategoryId, parentId, idPath, levelNum, orderIndex,
                                                    assistBookCategoryName, description, memo, startRowIndex, maximumRows, orderBy);

            return assistBookCategoryList;
        }

        /// <summary>
        /// 获取所有知识
        /// </summary>
        /// <returns>所有知识</returns>
        public IList<AssistBookCategory> GetAssistBookCategorys()
        {
            return dal.GetAssistBookCategorys();
        }

        public AssistBookCategory GetAssistBookCategory(int assistBookCategoryId)
        {
            if (assistBookCategoryId < 1)
            {
                AssistBookCategory  obj = new AssistBookCategory();
                return obj;
            }

            return dal.GetAssistBookCategory(assistBookCategoryId);
        }

        /// <summary>
        /// 新增辅导教材体系
        /// </summary>
        /// <param name="assistBookCategory">新增的辅导教材体系信息</param>
        /// <returns></returns>
        public int AddAssistBookCategory(AssistBookCategory assistBookCategory)
        {
            int id = dal.AddAssistBookCategory(assistBookCategory);
            objLogBll.WriteLog("新增辅导教材体系“" + assistBookCategory.AssistBookCategoryName + "”基本信息");
            return id;
        }

        /// <summary>
        /// 更新辅导教材体系
        /// </summary>
        /// <param name="assistBookCategory">更新后的辅导教材体系信息</param>
        public void UpdateAssistBookCategory(AssistBookCategory assistBookCategory)
        {
            dal.UpdateAssistBookCategory(assistBookCategory);
            objLogBll.WriteLog("修改辅导教材体系“" + assistBookCategory.AssistBookCategoryName + "”基本信息");
        }

        /// <summary>
        /// 删除辅导教材体系
        /// </summary>
        /// <param name="assistBookCategory">要删除的辅导教材体系</param>
        public void DeleteAssistBookCategory(AssistBookCategory assistBookCategory)
        {
            int code = 0;
            string strName = GetAssistBookCategory(assistBookCategory.AssistBookCategoryId).AssistBookCategoryName;
            dal.DeleteAssistBookCategory(assistBookCategory.AssistBookCategoryId, ref code);
            if (code == 0)
            {
                objLogBll.WriteLog("删除辅导教材体系“" + strName + "”基本信息");
            }
        }

        /// <summary>
        /// 删除辅导教材体系
        /// </summary>
        /// <param name="assistBookCategoryId">要删除的辅导教材体系ID</param>
        public void DeleteAssistBookCategory(int assistBookCategoryId, ref int errorCode)
        {
            int code = 0;
            string strName = GetAssistBookCategory(assistBookCategoryId).AssistBookCategoryName;
            dal.DeleteAssistBookCategory(assistBookCategoryId, ref code);
            errorCode = code;

            if (code == 0)
            {
                objLogBll.WriteLog("删除辅导教材体系“" + strName + "”基本信息");
            }
        }

        public bool MoveUp(int assistBookCategoryId)
        {
            return dal.Move(assistBookCategoryId, true);
        }

        public bool MoveDown(int assistBookCategoryId)
        {
            return dal.Move(assistBookCategoryId, false);
        }
    }
}

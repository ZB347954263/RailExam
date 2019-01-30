using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using System.Collections;

namespace RailExam.BLL
{
    public class KnowledgeBLL
    {
        private static readonly KnowledgeDAL dal = new KnowledgeDAL();
        private SystemLogBLL objLogBll = new SystemLogBLL();

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="knowledgeId"></param>
        /// <param name="parentId"></param>
        /// <param name="idPath"></param>
        /// <param name="levelNum"></param>
        /// <param name="orderIndex"></param>
        /// <param name="knowledgeName"></param>
        /// <param name="description"></param>
        /// <param name="memo"></param>
        /// <param name="startRowIndex">起始记录行</param>
        /// <param name="maximumRows">每页记录条数</param>
        /// <param name="orderBy">排序字符串，如"FieldName ASC"</param>
        /// <returns></returns>
        public IList<Knowledge> GetKnowledges(int knowledgeId, int parentId, string idPath, int levelNum, int orderIndex,
            string knowledgeName, string description, string memo, int startRowIndex, int maximumRows, string orderBy)
        {
            IList<Knowledge> knowledgeList = dal.GetKnowledges(knowledgeId, parentId, idPath, levelNum, orderIndex,
                                                    knowledgeName, description, memo, startRowIndex, maximumRows, orderBy);

            return knowledgeList;
        }

        /// <summary>
        /// 获取所有知识
        /// </summary>
        /// <returns>所有知识</returns>
        public IList<Knowledge> GetKnowledges()
        {
            return dal.GetKnowledges();
        }

		public IList<Knowledge> GetKnowledgesByOrgID(int orgID)
		{
			return dal.GetKnowledgesByOrgID(orgID);
		}

    	public Knowledge GetKnowledge(int knowledgeId)
        {
            if (knowledgeId < 1)
            {
                return null;
            }

            return dal.GetKnowledge(knowledgeId);
        }

		public IList<Knowledge> GetKnowledgesByParentID(int parentID)
		{
			return dal.GetKnowledgesByParentID(parentID);
		}

    	/// <summary>
        /// 新增教材体系
        /// </summary>
        /// <param name="knowledge">新增的教材体系信息</param>
        /// <returns></returns>
        public int AddKnowledge(Knowledge knowledge)
        {
            int id = dal.AddKnowledge(knowledge); 
            objLogBll.WriteLog("新增教材体系“"+ knowledge.KnowledgeName +"”基本信息");
            return id;
        }

        /// <summary>
        /// 更新教材体系
        /// </summary>
        /// <param name="knowledge">更新后的教材体系信息</param>
        public void UpdateKnowledge(Knowledge knowledge)
        {
            dal.UpdateKnowledge(knowledge);
            objLogBll.WriteLog("修改教材体系“" + knowledge.KnowledgeName + "”基本信息");
        }

        /// <summary>
        /// 删除教材体系
        /// </summary>
        /// <param name="knowledge">要删除的教材体系</param>
        public void DeleteKnowledge(Knowledge knowledge)
        {
            int code = 0;
            string strName = GetKnowledge(knowledge.KnowledgeId).KnowledgeName;
            dal.DeleteKnowledge(knowledge.KnowledgeId,ref code);
            if (code == 0)
            {
                objLogBll.WriteLog("删除教材体系“" + strName + "”基本信息");
            }
        }

        /// <summary>
        /// 删除教材体系
        /// </summary>
        /// <param name="knowledgeId">要删除的教材体系ID</param>
        public void DeleteKnowledge(int knowledgeId, ref int errorCode)
        {
             int code = 0;
             string strName = GetKnowledge(knowledgeId).KnowledgeName;
             dal.DeleteKnowledge(knowledgeId,ref code);
             errorCode = code;
            
            if(code == 0)
            {
                objLogBll.WriteLog("删除教材体系“" + strName + "”基本信息");
            }
        }

        public bool MoveUp(int knowledgeId)
        {
            return dal.Move(knowledgeId, true);
        }

        public bool MoveDown(int knowledgeId)
        {
            return dal.Move(knowledgeId, false);
        }

		public IList<Knowledge> GetKnowledgesByWhereClause(string whereClause, string orderby)
		{
			return dal.GetKnowledgesByWhereClause(whereClause, orderby);
		}

    }
}

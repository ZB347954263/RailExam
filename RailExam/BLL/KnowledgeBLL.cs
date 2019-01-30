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
        /// ��ȡ����
        /// </summary>
        /// <param name="knowledgeId"></param>
        /// <param name="parentId"></param>
        /// <param name="idPath"></param>
        /// <param name="levelNum"></param>
        /// <param name="orderIndex"></param>
        /// <param name="knowledgeName"></param>
        /// <param name="description"></param>
        /// <param name="memo"></param>
        /// <param name="startRowIndex">��ʼ��¼��</param>
        /// <param name="maximumRows">ÿҳ��¼����</param>
        /// <param name="orderBy">�����ַ�������"FieldName ASC"</param>
        /// <returns></returns>
        public IList<Knowledge> GetKnowledges(int knowledgeId, int parentId, string idPath, int levelNum, int orderIndex,
            string knowledgeName, string description, string memo, int startRowIndex, int maximumRows, string orderBy)
        {
            IList<Knowledge> knowledgeList = dal.GetKnowledges(knowledgeId, parentId, idPath, levelNum, orderIndex,
                                                    knowledgeName, description, memo, startRowIndex, maximumRows, orderBy);

            return knowledgeList;
        }

        /// <summary>
        /// ��ȡ����֪ʶ
        /// </summary>
        /// <returns>����֪ʶ</returns>
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
        /// �����̲���ϵ
        /// </summary>
        /// <param name="knowledge">�����Ľ̲���ϵ��Ϣ</param>
        /// <returns></returns>
        public int AddKnowledge(Knowledge knowledge)
        {
            int id = dal.AddKnowledge(knowledge); 
            objLogBll.WriteLog("�����̲���ϵ��"+ knowledge.KnowledgeName +"��������Ϣ");
            return id;
        }

        /// <summary>
        /// ���½̲���ϵ
        /// </summary>
        /// <param name="knowledge">���º�Ľ̲���ϵ��Ϣ</param>
        public void UpdateKnowledge(Knowledge knowledge)
        {
            dal.UpdateKnowledge(knowledge);
            objLogBll.WriteLog("�޸Ľ̲���ϵ��" + knowledge.KnowledgeName + "��������Ϣ");
        }

        /// <summary>
        /// ɾ���̲���ϵ
        /// </summary>
        /// <param name="knowledge">Ҫɾ���Ľ̲���ϵ</param>
        public void DeleteKnowledge(Knowledge knowledge)
        {
            int code = 0;
            string strName = GetKnowledge(knowledge.KnowledgeId).KnowledgeName;
            dal.DeleteKnowledge(knowledge.KnowledgeId,ref code);
            if (code == 0)
            {
                objLogBll.WriteLog("ɾ���̲���ϵ��" + strName + "��������Ϣ");
            }
        }

        /// <summary>
        /// ɾ���̲���ϵ
        /// </summary>
        /// <param name="knowledgeId">Ҫɾ���Ľ̲���ϵID</param>
        public void DeleteKnowledge(int knowledgeId, ref int errorCode)
        {
             int code = 0;
             string strName = GetKnowledge(knowledgeId).KnowledgeName;
             dal.DeleteKnowledge(knowledgeId,ref code);
             errorCode = code;
            
            if(code == 0)
            {
                objLogBll.WriteLog("ɾ���̲���ϵ��" + strName + "��������Ϣ");
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

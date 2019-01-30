using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using System.Collections;

namespace RailExam.BLL
{
    public class OtherKnowledgeBLL
    {
        private static readonly OtherKnowledgeDAL dal = new OtherKnowledgeDAL();
        private SystemLogBLL objLogBll = new SystemLogBLL();

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="otherKnowledgeID"></param>
        /// <param name="parentID"></param>
        /// <param name="idPath"></param>
        /// <param name="levelNum"></param>
        /// <param name="orderIndex"></param>
        /// <param name="otherKnowledgeName"></param>
        /// <param name="description"></param>
        /// <param name="memo"></param>
        /// <param name="startRowIndex">��ʼ��¼��</param>
        /// <param name="maximumRows">ÿҳ��¼����</param>
        /// <param name="orderBy">�����ַ�������"FieldName ASC"</param>
        /// <returns></returns>
        public IList<OtherKnowledge> GetOtherKnowledge(int otherKnowledgeID, int parentID, string idPath, int levelNum, int orderIndex,
            string otherKnowledgeName, string description, string memo, int startRowIndex, int maximumRows, string orderBy)
        {
            IList<OtherKnowledge> otherKnowledgeList = dal.GetOtherKnowledge(otherKnowledgeID, parentID, idPath, levelNum, orderIndex,
                            otherKnowledgeName, description, memo, startRowIndex, maximumRows, orderBy);

            return otherKnowledgeList;
        }

        public OtherKnowledge GetOtherKnowledge(int OtherKnowledgeID)
        {
            if (OtherKnowledgeID < 1)
            {
                return null;
            }

            return dal.GetOtherKnowledge(OtherKnowledgeID);
        }

        public int AddOtherKnowledge(OtherKnowledge otherKnowledge)
        {
            objLogBll.WriteLog("��������֪ʶ��" + otherKnowledge.OtherKnowledgeName + "��������Ϣ");
            return dal.AddOtherKnowledge(otherKnowledge);
        }

        public void UpdateOtherKnowledge(OtherKnowledge otherKnowledge)
        {
            objLogBll.WriteLog("�޸�����֪ʶ��" + otherKnowledge.OtherKnowledgeName + "��������Ϣ");
            dal.UpdateOtherKnowledge(otherKnowledge);
        }

        public void DeleteOtherKnowledge(OtherKnowledge otherKnowledge)
        {
            DeleteOtherKnowledge(otherKnowledge.OtherKnowledgeID);
        }

        public void DeleteOtherKnowledge(int OtherKnowledgeID)
        {
            string strName = GetOtherKnowledge(OtherKnowledgeID).OtherKnowledgeName;
            objLogBll.WriteLog("ɾ������֪ʶ��" + strName + "��������Ϣ");
            dal.DeleteOtherKnowledge(OtherKnowledgeID);
        }

        public bool MoveUp(int OtherKnowledgeID)
        {
            return dal.Move(OtherKnowledgeID, true);
        }

        public bool MoveDown(int OtherKnowledgeID)
        {
            return dal.Move(OtherKnowledgeID, false);
        }
    }
}

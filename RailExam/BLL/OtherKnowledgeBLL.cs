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
        /// 获取数据
        /// </summary>
        /// <param name="otherKnowledgeID"></param>
        /// <param name="parentID"></param>
        /// <param name="idPath"></param>
        /// <param name="levelNum"></param>
        /// <param name="orderIndex"></param>
        /// <param name="otherKnowledgeName"></param>
        /// <param name="description"></param>
        /// <param name="memo"></param>
        /// <param name="startRowIndex">起始记录行</param>
        /// <param name="maximumRows">每页记录条数</param>
        /// <param name="orderBy">排序字符串，如"FieldName ASC"</param>
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
            objLogBll.WriteLog("新增其它知识“" + otherKnowledge.OtherKnowledgeName + "”基本信息");
            return dal.AddOtherKnowledge(otherKnowledge);
        }

        public void UpdateOtherKnowledge(OtherKnowledge otherKnowledge)
        {
            objLogBll.WriteLog("修改其它知识“" + otherKnowledge.OtherKnowledgeName + "”基本信息");
            dal.UpdateOtherKnowledge(otherKnowledge);
        }

        public void DeleteOtherKnowledge(OtherKnowledge otherKnowledge)
        {
            DeleteOtherKnowledge(otherKnowledge.OtherKnowledgeID);
        }

        public void DeleteOtherKnowledge(int OtherKnowledgeID)
        {
            string strName = GetOtherKnowledge(OtherKnowledgeID).OtherKnowledgeName;
            objLogBll.WriteLog("删除其它知识“" + strName + "”基本信息");
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

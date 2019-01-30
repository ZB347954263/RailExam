using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using System.Collections;

namespace RailExam.BLL
{
    public class PaperItemBLL
    {
        private static readonly PaperItemDAL dal = new PaperItemDAL();

        /// <summary>
        /// 按试卷编号获取试题
        /// </summary>
        /// <returns>试题集合</returns>
        public IList<PaperItem> GetItemsByPaperId(int PaperId)
        {
            IList<PaperItem> paperItemList = dal.GetItemsByPaperId(PaperId);
            return paperItemList;
        }

        /// <summary>
        /// 按试卷大题获取试题
        /// </summary>
        /// <returns>试题集合</returns>
        public IList<PaperItem> GetItemsByPaperSubjectId(int PaperSubjectId)
        {
            IList<PaperItem> paperItemList = dal.GetItemsByPaperSubjectId(PaperSubjectId);
            return paperItemList;
        }

        /// <summary>
        /// 按试卷大题获取试题(路局查询站段)
        /// </summary>
        /// <returns>试题集合</returns>
        public IList<PaperItem> GetItemsByPaperSubjectIdByOrgID(int PaperSubjectId,int orgID)
        {
            IList<PaperItem> paperItemList = dal.GetItemsByPaperSubjectIdByOrgID(PaperSubjectId,orgID);
            return paperItemList;
        }

        /// <summary>
        /// 获取试题
        /// </summary>
        /// <returns>受影响的行数</returns>
        public PaperItem GetPaperItem(int paperItemId)
        {
            return dal.GetPaperItem(paperItemId);
        }

        /// <summary>
        /// 添加试题集合
        /// </summary>
        /// <returns>受影响的行数</returns>
        public void AddPaperItem(IList<PaperItem> paperItems)
        {
            dal.AddPaperItem(paperItems);
        }

        /// <summary>
        /// 修改试题
        /// </summary>
        /// <returns>受影响的行数</returns>
        public int UpdatePaperItem(PaperItem paperItem)
        {
            return dal.UpdatePaperItem(paperItem);
        }

        /// <summary>
        /// 添加试题
        /// </summary>
        /// <returns>受影响的行数</returns>
        public int AddPaperItem(PaperItem paperItem)
        {
            return dal.AddPaperItem(paperItem);
        }

        /// <summary>
        /// 删除试题
        /// </summary>
        /// <returns>受影响的行数</returns>
        public int DeletePaperItem(int paperItemId)
        {
            return dal.DeletePaperItem(paperItemId);
        }

        /// <summary>
        /// 删除试题
        /// </summary>
        /// <returns>受影响的行数</returns>
        public int DeletePaperItem(PaperItem paperItem)
        {
            return DeletePaperItem(paperItem.PaperItemId);
        }
    }
}

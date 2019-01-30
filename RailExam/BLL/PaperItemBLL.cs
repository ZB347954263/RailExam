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
        /// ���Ծ��Ż�ȡ����
        /// </summary>
        /// <returns>���⼯��</returns>
        public IList<PaperItem> GetItemsByPaperId(int PaperId)
        {
            IList<PaperItem> paperItemList = dal.GetItemsByPaperId(PaperId);
            return paperItemList;
        }

        /// <summary>
        /// ���Ծ�����ȡ����
        /// </summary>
        /// <returns>���⼯��</returns>
        public IList<PaperItem> GetItemsByPaperSubjectId(int PaperSubjectId)
        {
            IList<PaperItem> paperItemList = dal.GetItemsByPaperSubjectId(PaperSubjectId);
            return paperItemList;
        }

        /// <summary>
        /// ���Ծ�����ȡ����(·�ֲ�ѯվ��)
        /// </summary>
        /// <returns>���⼯��</returns>
        public IList<PaperItem> GetItemsByPaperSubjectIdByOrgID(int PaperSubjectId,int orgID)
        {
            IList<PaperItem> paperItemList = dal.GetItemsByPaperSubjectIdByOrgID(PaperSubjectId,orgID);
            return paperItemList;
        }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <returns>��Ӱ�������</returns>
        public PaperItem GetPaperItem(int paperItemId)
        {
            return dal.GetPaperItem(paperItemId);
        }

        /// <summary>
        /// ������⼯��
        /// </summary>
        /// <returns>��Ӱ�������</returns>
        public void AddPaperItem(IList<PaperItem> paperItems)
        {
            dal.AddPaperItem(paperItems);
        }

        /// <summary>
        /// �޸�����
        /// </summary>
        /// <returns>��Ӱ�������</returns>
        public int UpdatePaperItem(PaperItem paperItem)
        {
            return dal.UpdatePaperItem(paperItem);
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <returns>��Ӱ�������</returns>
        public int AddPaperItem(PaperItem paperItem)
        {
            return dal.AddPaperItem(paperItem);
        }

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <returns>��Ӱ�������</returns>
        public int DeletePaperItem(int paperItemId)
        {
            return dal.DeletePaperItem(paperItemId);
        }

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <returns>��Ӱ�������</returns>
        public int DeletePaperItem(PaperItem paperItem)
        {
            return DeletePaperItem(paperItem.PaperItemId);
        }
    }
}

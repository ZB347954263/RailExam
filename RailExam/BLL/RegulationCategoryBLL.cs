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
        /// ��ȡ����
        /// </summary>
        /// <param name="regulationCategoryID"></param>
        /// <param name="parentID"></param>
        /// <param name="idPath"></param>
        /// <param name="levelNum"></param>
        /// <param name="orderIndex"></param>
        /// <param name="categoryName"></param>
        /// <param name="description"></param>
        /// <param name="memo"></param>
        /// <param name="startRowIndex">��ʼ��¼��</param>
        /// <param name="maximumRows">ÿҳ��¼����</param>
        /// <param name="orderBy">�����ַ�������"FieldName ASC"</param>
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
        /// ����֪ʶ���
        /// </summary>
        /// <param name="regulationCategory">������֪ʶ�����Ϣ</param>
        /// <returns></returns>
        public int AddRegulationCategory(RegulationCategory regulationCategory)
        {
            objLogBll.WriteLog("�����������"+ regulationCategory.CategoryName+"��������Ϣ");
            return dal.AddRegulationCategory(regulationCategory);
        }

        /// <summary>
        /// ����֪ʶ���
        /// </summary>
        /// <param name="regulationCategory">���º��֪ʶ�����Ϣ</param>
        public void UpdateRegulationCategory(RegulationCategory regulationCategory)
        {
            objLogBll.WriteLog("�޸ķ������" + regulationCategory.CategoryName + "��������Ϣ");
            dal.UpdateRegulationCategory(regulationCategory);
        }

        /// <summary>
        /// ɾ��֪ʶ���
        /// </summary>
        /// <param name="regulationCategory">Ҫɾ����֪ʶ���</param>
        public void DeleteRegulationCategory(RegulationCategory regulationCategory)
        {
            DeleteRegulationCategory(regulationCategory.RegulationCategoryID);
        }

        /// <summary>
        /// ɾ��֪ʶ���
        /// </summary>
        /// <param name="regulationCategoryID">Ҫɾ����֪ʶ���ID</param>
        public void DeleteRegulationCategory(int regulationCategoryID)
        {
            string strName = GetRegulationCategory(regulationCategoryID).CategoryName;
            objLogBll.WriteLog("ɾ���������" + strName + "��������Ϣ");
            dal.DeleteRegulationCategory(regulationCategoryID);
        }
    }
}

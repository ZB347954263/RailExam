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
        /// ��ȡ����
        /// </summary>
        /// <param name="PaperCategoryId"></param>
        /// <param name="parentId"></param>
        /// <param name="idPath"></param>
        /// <param name="levelNum"></param>
        /// <param name="orderIndex"></param>
        /// <param name="CategoryName"></param>
        /// <param name="description"></param>
        /// <param name="memo"></param>
        /// <param name="startRowIndex">��ʼ��¼��</param>
        /// <param name="maximumRows">ÿҳ��¼����</param>
        /// <param name="orderBy">�����ַ�������"FieldName ASC"</param>
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
        /// ��ȡ�����Ծ����
        /// </summary>
        /// <returns>�����Ծ����</returns>
        public IList<PaperCategory> GetPaperCategories()
        {
            return dal.GetPaperCategories();
        }

        /// <summary>
        /// ��ҵ����Ƿ���Ϊ��ѯʹ��
        /// </summary>
        /// <param name="bForSearchUse">
        /// �Ƿ񹩲�ѯ�ã����ڣ�
        /// һ���ǣ������һ��ʾ�
        /// �������������ʾ�
        /// </param>
        /// <returns>��ҵ���</returns>
        public IList<PaperCategory> GetTaskCategories(bool bForSearchUse)
        {
            IList<PaperCategory> paperCategories = new List<PaperCategory>(dal.GetTaskCategories());

            if (bForSearchUse)
            {
                PaperCategory paperCategory = new PaperCategory();

                paperCategory.CategoryName = "-״̬-";
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
        /// �����Ծ����
        /// </summary>
        /// <param name="paperCategory">�������Ծ������Ϣ</param>
        /// <returns></returns>
        public int AddPaperCategory(PaperCategory paperCategory)
        {
            objLogBll.WriteLog("�����Ծ����" + paperCategory.CategoryName + "��������Ϣ");
            return dal.AddPaperCategory(paperCategory);
        }

        /// <summary>
        /// �����Ծ����
        /// </summary>
        /// <param name="paperCategory">���º���Ծ������Ϣ</param>
        public void UpdatePaperCategory(PaperCategory paperCategory)
        {
            objLogBll.WriteLog("�޸��Ծ����" + paperCategory.CategoryName + "��������Ϣ");
            dal.UpdatePaperCategory(paperCategory);
        }

        /// <summary>
        /// ɾ���Ծ����
        /// </summary>
        /// <param name="paperCategory">Ҫɾ�����Ծ����</param>
        public void DeletePaperCategory(PaperCategory paperCategory)
        {
            DeletePaperCategory(paperCategory.PaperCategoryId);
        }

        /// <summary>
        /// ɾ���Ծ����
        /// </summary>
        /// <param name="PaperCategoryId">Ҫɾ�����Ծ����ID</param>
        public void DeletePaperCategory(int PaperCategoryId)
        {
            string strName = GetPaperCategory(PaperCategoryId).CategoryName;
            objLogBll.WriteLog("�޸��Ծ����" + strName + "��������Ϣ");
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
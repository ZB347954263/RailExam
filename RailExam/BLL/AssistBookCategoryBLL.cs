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
        /// ��ȡ����
        /// </summary>
        /// <param name="assistBookCategoryId"></param>
        /// <param name="parentId"></param>
        /// <param name="idPath"></param>
        /// <param name="levelNum"></param>
        /// <param name="orderIndex"></param>
        /// <param name="assistBookCategoryName"></param>
        /// <param name="description"></param>
        /// <param name="memo"></param>
        /// <param name="startRowIndex">��ʼ��¼��</param>
        /// <param name="maximumRows">ÿҳ��¼����</param>
        /// <param name="orderBy">�����ַ�������"FieldName ASC"</param>
        /// <returns></returns>
        public IList<AssistBookCategory> GetAssistBookCategorys(int assistBookCategoryId, int parentId, string idPath, int levelNum, int orderIndex,
            string assistBookCategoryName, string description, string memo, int startRowIndex, int maximumRows, string orderBy)
        {
            IList<AssistBookCategory> assistBookCategoryList = dal.GetAssistBookCategorys(assistBookCategoryId, parentId, idPath, levelNum, orderIndex,
                                                    assistBookCategoryName, description, memo, startRowIndex, maximumRows, orderBy);

            return assistBookCategoryList;
        }

        /// <summary>
        /// ��ȡ����֪ʶ
        /// </summary>
        /// <returns>����֪ʶ</returns>
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
        /// ���������̲���ϵ
        /// </summary>
        /// <param name="assistBookCategory">�����ĸ����̲���ϵ��Ϣ</param>
        /// <returns></returns>
        public int AddAssistBookCategory(AssistBookCategory assistBookCategory)
        {
            int id = dal.AddAssistBookCategory(assistBookCategory);
            objLogBll.WriteLog("���������̲���ϵ��" + assistBookCategory.AssistBookCategoryName + "��������Ϣ");
            return id;
        }

        /// <summary>
        /// ���¸����̲���ϵ
        /// </summary>
        /// <param name="assistBookCategory">���º�ĸ����̲���ϵ��Ϣ</param>
        public void UpdateAssistBookCategory(AssistBookCategory assistBookCategory)
        {
            dal.UpdateAssistBookCategory(assistBookCategory);
            objLogBll.WriteLog("�޸ĸ����̲���ϵ��" + assistBookCategory.AssistBookCategoryName + "��������Ϣ");
        }

        /// <summary>
        /// ɾ�������̲���ϵ
        /// </summary>
        /// <param name="assistBookCategory">Ҫɾ���ĸ����̲���ϵ</param>
        public void DeleteAssistBookCategory(AssistBookCategory assistBookCategory)
        {
            int code = 0;
            string strName = GetAssistBookCategory(assistBookCategory.AssistBookCategoryId).AssistBookCategoryName;
            dal.DeleteAssistBookCategory(assistBookCategory.AssistBookCategoryId, ref code);
            if (code == 0)
            {
                objLogBll.WriteLog("ɾ�������̲���ϵ��" + strName + "��������Ϣ");
            }
        }

        /// <summary>
        /// ɾ�������̲���ϵ
        /// </summary>
        /// <param name="assistBookCategoryId">Ҫɾ���ĸ����̲���ϵID</param>
        public void DeleteAssistBookCategory(int assistBookCategoryId, ref int errorCode)
        {
            int code = 0;
            string strName = GetAssistBookCategory(assistBookCategoryId).AssistBookCategoryName;
            dal.DeleteAssistBookCategory(assistBookCategoryId, ref code);
            errorCode = code;

            if (code == 0)
            {
                objLogBll.WriteLog("ɾ�������̲���ϵ��" + strName + "��������Ϣ");
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

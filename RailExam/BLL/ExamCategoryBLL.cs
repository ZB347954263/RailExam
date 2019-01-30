using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using System.Collections;

namespace RailExam.BLL
{
    public class ExamCategoryBLL
    {
        private static readonly ExamCategoryDAL dal = new ExamCategoryDAL();
        private SystemLogBLL objLogBll = new SystemLogBLL();

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="ExamCategoryId"></param>
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
        public IList<ExamCategory> GetExamCategory(int ExamCategoryId, int parentId, string idPath, int levelNum, int orderIndex,
            string CategoryName, string description, string memo, int startRowIndex, int maximumRows, string orderBy)
        {
            IList<ExamCategory> examCategoryList = dal.GetExamCategory(ExamCategoryId, parentId, idPath, levelNum, orderIndex,
                                                CategoryName, description, memo, startRowIndex, maximumRows, orderBy);

            return examCategoryList;
        }

        public ExamCategory GetExamCategory(int ExamCategoryId)
        {
            if (ExamCategoryId < 1)
            {
                return null;
            }

            return dal.GetExamCategory(ExamCategoryId);
        }

        /// <summary>
        /// ����֪ʶ���
        /// </summary>
        /// <param name="examCategory">������֪ʶ�����Ϣ</param>
        /// <returns></returns>
        public int AddExamCategory(ExamCategory examCategory)
        {
            int  id= dal.AddExamCategory(examCategory);
            objLogBll.WriteLog("�����������"+ examCategory.CategoryName +"��������Ϣ");
            return id;
        }

        public IList<ExamCategory> GetExamCategories()
        {
            return dal.GetExamCategories();
        }

        /// <summary>
        /// ����֪ʶ���
        /// </summary>
        /// <param name="examCategory">���º��֪ʶ�����Ϣ</param>
        public void UpdateExamCategory(ExamCategory examCategory)
        {
            dal.UpdateExamCategory(examCategory);
            objLogBll.WriteLog("�޸Ŀ������" + examCategory.CategoryName + "��������Ϣ");
        }

        /// <summary>
        /// ɾ��֪ʶ���
        /// </summary>
        /// <param name="examCategory">Ҫɾ����֪ʶ���</param>
        public void DeleteExamCategory(ExamCategory examCategory)
        {
            string strName = GetExamCategory(examCategory.ExamCategoryId).CategoryName;
            objLogBll.WriteLog("�����������" + strName + "��������Ϣ");
            dal.DeleteExamCategory(examCategory.ExamCategoryId);
        }

        /// <summary>
        /// ɾ��֪ʶ���
        /// </summary>
        /// <param name="ExamCategoryId">Ҫɾ����֪ʶ���ID</param>
        public void DeleteExamCategory(int ExamCategoryId)
        {
            string strName = GetExamCategory(ExamCategoryId).CategoryName;
            objLogBll.WriteLog("�����������" + strName + "��������Ϣ");
            dal.DeleteExamCategory(ExamCategoryId);
        }

        public bool MoveUp(int ExamCategoryId)
        {
            return dal.Move(ExamCategoryId, true);
        }

        public bool MoveDown(int ExamCategoryId)
        {
            return dal.Move(ExamCategoryId, false);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using System.Collections;

namespace RailExam.BLL
{
    public class CoursewareTypeBLL
    {
        private static readonly CoursewareTypeDAL dal = new CoursewareTypeDAL();
        private SystemLogBLL objLogBill = new SystemLogBLL(); 


        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="coursewareTypeId"></param>
        /// <param name="parentId"></param>
        /// <param name="idPath"></param>
        /// <param name="levelNum"></param>
        /// <param name="orderIndex"></param>
        /// <param name="coursewareTypeName"></param>
        /// <param name="description"></param>
        /// <param name="memo"></param>
        /// <param name="startRowIndex">��ʼ��¼��</param>
        /// <param name="maximumRows">ÿҳ��¼����</param>
        /// <param name="orderBy">�����ַ�������"FieldName ASC"</param>
        /// <returns></returns>
        public IList<CoursewareType> GetCoursewareTypes(int coursewareTypeId, int parentId, string idPath, int levelNum, int orderIndex,
            string coursewareTypeName, string description, string memo, int startRowIndex, int maximumRows, string orderBy)
        {
            IList<CoursewareType> CoursewareTypeList = dal.GetCoursewareTypes(coursewareTypeId, parentId, idPath, levelNum, orderIndex,
                                                                        coursewareTypeName, description, memo, startRowIndex, maximumRows, orderBy);

            return CoursewareTypeList;
        }

        public IList<CoursewareType> GetCoursewareTypes()
        {
            return dal.GetCoursewareTypes();
        }

        /// <summary>
        /// ȡ��ĳ�γ���صĿγ������Ϣ
        /// </summary>
        /// <param name="courseID"></param>
        /// <returns></returns>
        public IList<CoursewareType> GetCoursewareTypesByCourseID(int courseID)
        {
            return dal.GetCoursewareTypesByCourseID(courseID);
        }

        public CoursewareType GetCoursewareType(int coursewareTypeId)
        {
            if (coursewareTypeId < 1)
            {
                return null;
            }

            return dal.GetCoursewareType(coursewareTypeId);
        }

        /// <summary>
        /// �����μ����
        /// </summary>
        /// <param name="coursewareType">�����Ŀμ������Ϣ</param>
        /// <returns></returns>
        public int AddCoursewareType(CoursewareType coursewareType)
        {
            int id = dal.AddCoursewareType(coursewareType);
            objLogBill.WriteLog("�����μ����"+��coursewareType.CoursewareTypeName��+"��");
            return id;
        }

        /// <summary>
        /// ���¿μ����
        /// </summary>
        /// <param name="coursewareType">���º�Ŀμ������Ϣ</param>
        public void UpdateCoursewareType(CoursewareType coursewareType)
        {
            dal.UpdateCoursewareType(coursewareType);
            objLogBill.WriteLog("�޸Ŀμ����" + coursewareType.CoursewareTypeName + "��");
        }

        /// <summary>
        /// ɾ���μ����
        /// </summary>
        /// <param name="coursewareType">Ҫɾ���Ŀμ����</param>
        public void DeleteCoursewareType(CoursewareType coursewareType)
        {
            int code = 0;
            string strName = GetCoursewareType(coursewareType.CoursewareTypeId).CoursewareTypeName;
            dal.DeleteCoursewareType(coursewareType.CoursewareTypeId,ref code);
            if (code == 0)
            {
                objLogBill.WriteLog("ɾ���μ����" + strName + "��");
            }
        }

        /// <summary>
        /// ɾ���μ����
        /// </summary>
        /// <param name="coursewareTypeId">Ҫɾ���Ŀμ����ID</param>
        public void DeleteCoursewareType(int coursewareTypeId, ref int errorCode)
        {
            int code = 0;
            string strName = GetCoursewareType(coursewareTypeId).CoursewareTypeName;
            dal.DeleteCoursewareType(coursewareTypeId,ref code);
            errorCode = code;
            if(code == 0)
            {
                objLogBill.WriteLog("ɾ���μ����" + strName + "��");
            }
        }
    }
}

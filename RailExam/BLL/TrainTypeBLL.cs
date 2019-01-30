using System;
using System.Collections.Generic;
using System.Text;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    public class TrainTypeBLL
    {
        /// <summary>
        /// �ڲ���Ա
        /// </summary>
        private static readonly TrainTypeDAL dal = new TrainTypeDAL();
        private SystemLogBLL objLogBll = new SystemLogBLL();

        public IList<TrainType> GetTrainTypeInfo(int TrainTypeID,
                                               int ParentID,
                                               int LevelNum,
                                               string IDPath,
                                               int OrderIndex,
                                               string TypeName,
                                               string Description,
                                               bool IsTemplate,
                                               bool IsPromotion,
                                               string Memo,
                                               int startRowIndex,
                                               int maximumRows,
                                               string orderBy)
        {
            return dal.GetTrainTypeInfo(TrainTypeID,
                                        ParentID,
                                        LevelNum,
                                        IDPath,
                                        OrderIndex,
                                        TypeName,
                                        Description,
                                        IsTemplate,
                                        IsPromotion,
                                        Memo,
                                        startRowIndex,
                                        maximumRows,
                                        orderBy);
        }

        public IList<TrainType> GetTrainTypes()
        {
            return dal.GetTrainTypes();
        }

        public IList<TrainType> GetTrainTypeByParentId(int trainTypeID)
        {
            return dal.GetTrainTypeByParentId(trainTypeID);
        }
        
        /// <summary>
        /// ����PostID����ѵ�淶���в�ѯ��ѡ�У���δѡ�У�����ѵ�����Ϣ
        /// </summary>
        /// <param name="PostID">��ѵ��λID</param>
        /// <param name="flag">��ʶ��1����ѡ�У�0����δѡ��</param>
        /// <returns></returns>
        public IList<TrainType> GetTrainStandardTypeInfo(int PostID, int flag)
        {
            return dal.GetTrainStandardTypeInfo(PostID, flag);
        }

        public TrainType GetTrainTypeInfo(int trainTypeID)
        {
            return dal.GetTrainTypeInfo(trainTypeID);
        }

        public void AddTrainType(TrainType trainType)
        {
            objLogBll.WriteLog("������ѵ���"+ trainType.TypeName +"��������Ϣ");
            dal.AddTrainType(trainType);
        }

        public void DeleteTrainType(TrainType trainType)
        {
            int code = 0;
           DeleteTrainType(trainType.TrainTypeID,ref code);
        }

        public void DeleteTrainType(int trainTypeID,ref int errorcode)
        {
            int code = 0;
            string strName = GetTrainTypeInfo(trainTypeID).TypeName;
            dal.DeleteTrainType(trainTypeID,ref code);
            errorcode = code;

            if(code ==0)
            {
                objLogBll.WriteLog("ɾ����ѵ���" + strName + "��������Ϣ");
            }
        }

        public void UpdateTrainType(TrainType trainType)
        {
            objLogBll.WriteLog("�޸���ѵ���" + trainType.TypeName + "��������Ϣ");
            dal.UpdateTrainType(trainType);
        }

		public IList<TrainType> GetTrainTypeByWhereClause(string whereClause, string orderby)
		{
			return dal.GetTrainTypeByWhereClause(whereClause, orderby);
		}
    }
}

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
        /// 内部成员
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
        /// 根据PostID在培训规范表中查询已选中（或未选中）的培训类别信息
        /// </summary>
        /// <param name="PostID">培训岗位ID</param>
        /// <param name="flag">标识：1——选中；0——未选中</param>
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
            objLogBll.WriteLog("新增培训类别“"+ trainType.TypeName +"”基本信息");
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
                objLogBll.WriteLog("删除培训类别“" + strName + "”基本信息");
            }
        }

        public void UpdateTrainType(TrainType trainType)
        {
            objLogBll.WriteLog("修改培训类别“" + trainType.TypeName + "”基本信息");
            dal.UpdateTrainType(trainType);
        }

		public IList<TrainType> GetTrainTypeByWhereClause(string whereClause, string orderby)
		{
			return dal.GetTrainTypeByWhereClause(whereClause, orderby);
		}
    }
}

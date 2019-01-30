using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;

namespace RailExam.BLL
{
    /// <summary>
    /// 业务逻辑：紧急程度
    /// </summary>
    public class ImportanceBLL
    {
        private static readonly ImportanceDAL dal = new ImportanceDAL();
        
        public IList<Importance> GetImportances(int importanceID, string importanceName, string Description, 
            bool isDefault, string memo, int startRowIndex, int maximumRows, string orderBy)
        {
            IList<Importance> importanceList = dal.GetImportances(importanceID, importanceName, Description, 
                 isDefault, memo, startRowIndex, maximumRows, orderBy);

            return importanceList;
        }

        public IList<Importance> GetImportances()
        {
            IList<Importance> importanceList = dal.GetImportances();

            return importanceList;
        }

        public Importance GetImportance(int importanceID)
        {
            if (importanceID < 1)
            {
                return null;
            }

            return dal.GetImportance(importanceID);
        }

        public int GetCount(int importanceID, string importanceName, string Description, bool isDefault, string memo)
        {
            return dal.RecordCount;
        }
    }
}

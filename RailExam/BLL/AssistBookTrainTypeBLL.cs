using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using DSunSoft.Web.Global;

namespace RailExam.BLL
{
    public class AssistBookTrainTypeBLL
    {
        private static readonly AssistBookTrainTypeDAL dal = new AssistBookTrainTypeDAL();

        public void AddAssistBookTrainType(AssistBookTrainType bookTrainType)
        {
            dal.AddAssistBookTrainType(bookTrainType);
        }

        public void UpdateAssistBookTrainType(AssistBookTrainType bookTrainType)
        {
            dal.UpdateAssistBookTrainType(bookTrainType);
        }

        public IList<AssistBookTrainType> GetAssistBookTrainTypeByBookID(int bookID)
        {
            return dal.GetAssistBookTrainTypeByBookID(bookID);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    public class SynchronizeBLL
    {
        private static readonly SynchronizeDAL dal = new SynchronizeDAL();

        public void UpdateSynchronize(Synchronize obj)
        {
            dal.UpdateSynchronize(obj);
        }

        public Synchronize GetSynchronize()
        {
            return dal.GetSynchronize();
        }
    }
}

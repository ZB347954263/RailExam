using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;

namespace RailExam.BLL
{
    public class PostImportBLL
    {
        private static readonly PostImportDAL dal = new PostImportDAL();

        public IList<PostImport> GetPostImport()
        {
            return dal.GetPostImport();
        }
    }
}

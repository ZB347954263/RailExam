using System;
using System.Collections.Generic;
using System.Collections;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    public class TrainPlanEmployeeBLL
    {
        /// <summary>
        /// �ڲ���Ա
        /// </summary>
        private static readonly TrainPlanEmployeeDAL dal = new TrainPlanEmployeeDAL();

        /// <summary>
        /// �����ѵ�ƻ�Ա��
        /// </summary>
        /// <param name="obj"></param>
        public void AddTrainPlanEmployee(TrainPlanEmployee obj)
        {
            dal.AddTrainPlanEmployee(obj);
        }

        /// <summary>
        /// ������ѵ�ƻ�Ա��
        /// </summary>
        /// <param name="obj"></param>
        public void UpdateTrainPlanEmployee(TrainPlanEmployee obj)
        {
            dal.UpdateTrainPlanEmployee(obj);
        }

        /// <summary>
        /// ɾ����ѵ�ƻ�Ա��
        /// </summary>
        /// <param name="planid"></param>
        /// <param name="employeeid"></param>
        public void DeleteTrainPlanEmployee(int planid, int employeeid)
        {
            dal.DeleteTrainPlanEmployee(planid, employeeid);
        }

        public IList<TrainPlanEmployee> GetTrainPlanEmployeeInfo(int trainPlanID,
                                                    int trainEmployeeID,
                                                    decimal process,
                                                    int statusID,
                                                    string memo,
                                                    int startRowIndex,
                                                    int maximumRows,
                                                    string orderBy)
        {
            return dal.GetTrainPlanEmployeeInfo(trainPlanID,
                                              trainEmployeeID,
                                              process,
                                              statusID,
                                              memo,
                                              startRowIndex,
                                              maximumRows,
                                              orderBy);
        }

        /// <summary>
        /// ���ݼƻ�ID�Ϳγ�ID����Ψһ����ѵ�ƻ�Ա����Ϣ
        /// </summary>
        /// <param name="trainPlanID"></param>
        /// <param name="trainEmployeeID"></param>
        /// <returns></returns>
        public TrainPlanEmployee GetTrainPlanEmployeeInfo(int trainPlanID, int trainEmployeeID)
        {
            return dal.GetTrainPlanEmployeeInfo(trainPlanID, trainEmployeeID);
        }

        /// <summary>
        /// ����ĳһ��ѵ�ƻ����вμ���ѵ�ƻ���Ա����Ϣ
        /// </summary>
        /// <param name="trainPlanID"></param>
        /// <returns></returns>
        public IList<TrainPlanEmployee> GetTrainPlanEmployeeInfoByPlanID(int trainPlanID)
        {
            return dal.GetTrainPlanEmployeeInfoByPlanID(trainPlanID);
        }

        /// <summary>
        /// ����ĳһ��ѵ�ƻ����вμ���ѵ�ƻ���Ա����Ϣ
        /// </summary>
        /// <param name="trainPlanID"></param>
        /// <param name="OrgID"></param>
        /// <returns></returns>
        public IList<Employee> GetTrainEmployeeInfoByPlanID(int trainPlanID, int OrgID)
        {
            EmployeeBLL employeeBLL = new EmployeeBLL();
            IList<Employee> employeeList = employeeBLL.GetEmployees(OrgID,"");
            ArrayList objList = GetEmployeeList(trainPlanID);

            foreach (Employee employee in employeeList)
            {
                if (objList.IndexOf(employee.EmployeeID) != -1)
                {
                    employee.Flag = true;
                }
                else
                {
                    employee.Flag = false;
                }
            }

            return employeeList;
        }

        /// <summary>
        /// ����ĳ��ѵ�ƻ�ID���ظüƻ������γ̵�ID
        /// </summary>
        /// <param name="trainPlanID"></param>
        /// <returns></returns>
        public ArrayList GetEmployeeList(int trainPlanID)
        {
            ArrayList objList = new ArrayList();
            IList<TrainPlanEmployee> objTrainPlanEmployeeList = GetTrainPlanEmployeeInfoByPlanID(trainPlanID);

            foreach (TrainPlanEmployee obj in objTrainPlanEmployeeList)
            {
                objList.Add(obj.TrainPlanEmployeeID);
            }

            return objList;
        }
    }
}

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
        /// 内部成员
        /// </summary>
        private static readonly TrainPlanEmployeeDAL dal = new TrainPlanEmployeeDAL();

        /// <summary>
        /// 添加培训计划员工
        /// </summary>
        /// <param name="obj"></param>
        public void AddTrainPlanEmployee(TrainPlanEmployee obj)
        {
            dal.AddTrainPlanEmployee(obj);
        }

        /// <summary>
        /// 更新培训计划员工
        /// </summary>
        /// <param name="obj"></param>
        public void UpdateTrainPlanEmployee(TrainPlanEmployee obj)
        {
            dal.UpdateTrainPlanEmployee(obj);
        }

        /// <summary>
        /// 删除培训计划员工
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
        /// 根据计划ID和课程ID返回唯一的培训计划员工信息
        /// </summary>
        /// <param name="trainPlanID"></param>
        /// <param name="trainEmployeeID"></param>
        /// <returns></returns>
        public TrainPlanEmployee GetTrainPlanEmployeeInfo(int trainPlanID, int trainEmployeeID)
        {
            return dal.GetTrainPlanEmployeeInfo(trainPlanID, trainEmployeeID);
        }

        /// <summary>
        /// 返回某一培训计划所有参加培训计划的员工信息
        /// </summary>
        /// <param name="trainPlanID"></param>
        /// <returns></returns>
        public IList<TrainPlanEmployee> GetTrainPlanEmployeeInfoByPlanID(int trainPlanID)
        {
            return dal.GetTrainPlanEmployeeInfoByPlanID(trainPlanID);
        }

        /// <summary>
        /// 返回某一培训计划所有参加培训计划的员工信息
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
        /// 根据某培训计划ID返回该计划所含课程的ID
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

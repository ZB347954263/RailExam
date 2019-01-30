using System.Collections.Generic;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    /// <summary>
    /// 业务逻辑：试题状态
    /// </summary>
    public class ItemStatusBLL
    {
        private static readonly ItemStatusDAL dal = new ItemStatusDAL();

        /// <summary>
        /// 新增试题状态
        /// </summary>
        /// <param name="itemStatus">试题状态</param>
        /// <returns>数据库受影响的行数</returns>
        public int AddItemStatus(ItemStatus itemStatus)
        {
            return dal.AddItemStatus(itemStatus);
        }

        /// <summary>
        /// 删除试题状态
        /// </summary>
        /// <param name="itemStatusId">试题状态ID</param>
        /// <returns>数据库受影响的行数</returns>
        public int DeleteItemStatus(int itemStatusId)
        {
            return dal.DeleteItemStatus(itemStatusId);
        }

        /// <summary>
        /// 修改试题状态
        /// </summary>
        /// <param name="itemStatus">试题状态</param>
        /// <returns>数据库受影响的行数</returns>
        public int UpdateItemStatus(ItemStatus itemStatus)
        {
            return dal.UpdateItemStatus(itemStatus);
        }

        /// <summary>
        /// 按试题状态ID取试题状态
        /// </summary>
        /// <param name="itemStatusId">试题状态ID</param>
        /// <returns>试题状态</returns>
        public ItemStatus GetItemStatus(int itemStatusId)
        {
            return dal.GetItemStatus(itemStatusId);
        }

        /// <summary>
        /// 查询所有试题状态
        /// </summary>
        /// <returns>所有试题状态</returns>
        public IList<ItemStatus> GetItemStatuss()
        {
            return dal.GetItemStatuss();
        }


        public IList<ItemStatus> GetItemStatuss(bool bForSearchUse)
        {
            if (!bForSearchUse)
            {
                return GetItemStatuss();
            }

            IList<ItemStatus> itemTypes = GetItemStatuss();
            ItemStatus itemType = new ItemStatus();

            itemType.StatusName = "-状态-";
            itemType.ItemStatusId = -1;
            itemTypes.Insert(0, itemType);

            return itemTypes;
        }


        /// <summary>
        /// 查询符合条件的试题状态
        /// </summary>
        /// <param name="itemStatusId"></param>
        /// <param name="statusName"></param>
        /// <param name="description"></param>
        /// <param name="isDefault"></param>
        /// <param name="memo"></param>
        /// <returns>符合条件的试题状态</returns>
        public IList<ItemStatus> GetItemStatuss(int itemStatusId, string statusName,
            string description, int isDefault, string memo)
        {
            return dal.GetItemStatuss(itemStatusId, statusName,
                                      description, isDefault, memo);
        }
    }
}
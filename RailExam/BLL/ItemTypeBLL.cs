using System.Collections.Generic;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    /// <summary>
    /// 业务逻辑：试题类别
    /// </summary>
    public class ItemTypeBLL
    {
        private static readonly ItemTypeDAL dal = new ItemTypeDAL();

        /// <summary>
        /// 新增试题类别
        /// </summary>
        /// <param name="itemType">试题类别</param>
        /// <returns>数据库受影响的行数</returns>
        public int AddItemType(ItemType itemType)
        {
            return dal.AddItemType(itemType);
        }

        /// <summary>
        /// 删除试题类别
        /// </summary>
        /// <param name="itemTypeId">试题类别ID</param>
        /// <returns>数据库受影响的行数</returns>
        public int DeleteItemType(int itemTypeId)
        {
            return dal.DeleteItemType(itemTypeId);
        }

        /// <summary>
        /// 修改试题类别
        /// </summary>
        /// <param name="itemType">试题类别</param>
        /// <returns>数据库受影响的行数</returns>
        public int UpdateItemType(ItemType itemType)
        {
            return dal.UpdateItemType(itemType);
        }

        /// <summary>
        /// 按试题类别ID取试题类别
        /// </summary>
        /// <param name="itemTypeId">题类别ID</param>
        /// <returns>试题类别</returns>
        public ItemType GetItemType(int itemTypeId)
        {
            return dal.GetItemType(itemTypeId);
        }

        /// <summary>
        /// 查询所有试题类别
        /// </summary>
        /// <returns>所有试题类别</returns>
        public IList<ItemType> GetItemTypes()
        {
            return dal.GetItemTypes();
        }

        /// <summary>
        /// 查询所有试题类别
        /// </summary>
        /// <param name="bForSearchUse">
        /// 是否供查询用，对于：
        /// 一、是，则添加一提示项；
        /// 二、否，则不添加提示项；
        /// </param>
        /// <returns>所有试题类别</returns>
        public IList<ItemType> GetItemTypes(bool bForSearchUse)
        {
            if(! bForSearchUse)
            {
                return GetItemTypes();
            }

            IList<ItemType> itemTypes = GetItemTypes();
            ItemType itemType = new ItemType();

            itemType.TypeName = "-题型-";
            itemType.ItemTypeId = -1;
            itemTypes.Insert(0, itemType);

            return itemTypes;
        }

        /// <summary>
        /// 查询符合条件的试题类别
        /// </summary>
        public IList<ItemType> GetItemTypes(int itemTypeId, string typeName,
            string imageFileName, string description, int isDefault, int isValid, string memo)
        {
            return dal.GetItemTypes(itemTypeId, typeName, imageFileName, description, isDefault, isValid, memo);
        }
    }
}
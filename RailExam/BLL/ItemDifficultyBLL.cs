using System.Collections.Generic;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    /// <summary>
    /// 业务逻辑：试题难度
    /// </summary>
    public class ItemDifficultyBLL
    {
        private static readonly ItemDifficultyDAL dal = new ItemDifficultyDAL();

        /// <summary>
        /// 新增试题难度
        /// </summary>
        /// <param name="itemDifficulty">试题难度</param>
        /// <returns>数据库受影响的行数</returns>
        public int AddItemDifficulty(ItemDifficulty itemDifficulty)
        {
            return dal.AddItemDifficulty(itemDifficulty);
        }

        /// <summary>
        /// 删除试题难度
        /// </summary>
        /// <param name="itemDifficultyId">试题难度ID</param>
        /// <returns>数据库受影响的行数</returns>
        public int DeleteItemDifficulty(int itemDifficultyId)
        {
            return dal.DeleteItemDifficulty(itemDifficultyId);
        }

        public int DeleteItemDifficulty(ItemDifficulty itemDifficulty)
        {
            return DeleteItemDifficulty(itemDifficulty.ItemDifficultyId);
        }

        /// <summary>
        /// 修改试题难度
        /// </summary>
        /// <param name="itemDifficulty">试题难度</param>
        /// <returns>数据库受影响的行数</returns>
        public int UpdateItemDifficulty(ItemDifficulty itemDifficulty)
        {
            return dal.UpdateItemDifficulty(itemDifficulty);
        }

        /// <summary>
        /// 按试题难度ID取试题难度
        /// </summary>
        /// <returns>试题难度</returns>
        public ItemDifficulty GetItemDifficulty(int itemDifficultyId)
        {
            return dal.GetItemDifficulty(itemDifficultyId);
        }

        /// <summary>
        /// 查询所有试题难度
        /// </summary>
        /// <returns>所有试题难度</returns>
        public IList<ItemDifficulty> GetItemDifficulties()
        {
            return dal.GetItemDifficulties();
        }

        /// <summary>
        /// 查询所有试题难度
        /// </summary>
        /// <param name="bForSearchUse">
        /// 是否供查询用，对于：
        /// 一、是，则添加一提示项；
        /// 二、否，则不添加提示项；
        /// </param>
        /// <returns>所有试题难度</returns>
        public IList<ItemDifficulty> GetItemDifficulties(bool bForSearchUse)
        {
            if(! bForSearchUse)
            {
                return GetItemDifficulties();
            }

            IList<ItemDifficulty> difficulties = GetItemDifficulties();
            ItemDifficulty difficulty = new ItemDifficulty();

            difficulty.DifficultyName = "-难度-";
            difficulty.ItemDifficultyId = -1;
            difficulties.Insert(0, difficulty);

            return difficulties;
        }

        /// <summary>
        /// 查询符合条件的试题难度
        /// </summary>
        /// <param name="itemDifficultyId"></param>
        /// <param name="difficultyName"></param>
        /// <param name="difficultyValue"></param>
        /// <param name="description"></param>
        /// <param name="isDefault"></param>
        /// <param name="memo"></param>
        /// <returns>符合条件的试题难度</returns>
        public IList<ItemDifficulty> GetItemDifficulties(int itemDifficultyId, string difficultyName,
            int difficultyValue, string description, int isDefault, string memo)
        {
            return dal.GetItemDifficulties(itemDifficultyId, difficultyName, difficultyValue, description, isDefault, memo);
        }
    }
}
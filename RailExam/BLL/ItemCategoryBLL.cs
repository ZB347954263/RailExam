using System.Collections.Generic;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    /// <summary>
    /// 业务逻辑：辅助分类
    /// </summary>
    public class ItemCategoryBLL
    {
        private static readonly ItemCategoryDAL dal = new ItemCategoryDAL();
    　private SystemLogBLL objLogBll = new SystemLogBLL();

        /// <summary>
        /// 新增辅助分类
        /// </summary>
        /// <param name="itemCategory">辅助分类</param>
        /// <returns>数据库受影响的行数</returns>
        public int AddItemCategory(ItemCategory itemCategory)
        {
            int id= dal.AddItemCategory(itemCategory);
            objLogBll.WriteLog("新增辅助分类“ "+ itemCategory.CategoryName +"”基本信息");
            return id;
        }

        /// <summary>
        /// 删除辅助分类
        /// </summary>
        /// <param name="itemCategoryId">辅助分类ID</param>
        /// <returns>数据库受影响的行数</returns>
        public void DeleteItemCategory(int itemCategoryId, ref int errorCode)
        {
            int code = 0;
            string strName = GetItemCategory(itemCategoryId).CategoryName;
            objLogBll.WriteLog("删除辅助分类“ " + strName + "”基本信息");
            dal.DeleteItemCategory(itemCategoryId,ref code);
            errorCode = code;
        }

        /// <summary>
        /// 删除辅助分类
        /// </summary>
        /// <param name="itemCategory">辅助分类</param>
        /// <returns>数据库受影响的行数</returns>
        public void DeleteItemCategory(ItemCategory itemCategory)
        {
            int code = 0;
            string strName = GetItemCategory(itemCategory.ItemCategoryId).CategoryName;
            objLogBll.WriteLog("删除辅助分类“ " + strName + "”基本信息");
            dal.DeleteItemCategory(itemCategory.ItemCategoryId,ref code);
        }

        /// <summary>
        /// 修改辅助分类
        /// </summary>
        /// <param name="itemCategory">辅助分类</param>
        /// <returns>数据库受影响的行数</returns>
        public int UpdateItemCategory(ItemCategory itemCategory)
        {
            objLogBll.WriteLog("修改辅助分类“ " + itemCategory.CategoryName + "”基本信息");
            return dal.UpdateItemCategory(itemCategory);
        }

        /// <summary>
        /// 按辅助分类ID取辅助分类
        /// </summary>
        /// <param name="itemCategoryId">辅助分类ID</param>
        /// <returns>辅助分类</returns>
        public ItemCategory GetItemCategory(int itemCategoryId)
        {
            return dal.GetItemCategory(itemCategoryId);
        }

        /// <summary>
        /// 查询所有辅助分类
        /// </summary>
        /// <returns>所有辅助分类</returns>
        public IList<ItemCategory> GetItemCategories()
        {
            return dal.GetItemCategories();
        }

        /// <summary>
        /// 查询符合条件的辅助分类
        /// </summary>
        /// <param name="itemCategoryId"></param>
        /// <param name="parentId"></param>
        /// <param name="idPath"></param>
        /// <param name="orderIndex"></param>
        /// <param name="categoryName"></param>
        /// <param name="levelNum"></param>
        /// <param name="description"></param>
        /// <param name="memo"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="orderBy"></param>
        /// <returns>符合条件的辅助分类</returns>
        public IList<ItemCategory> GetItemCategories(int itemCategoryId, int parentId, string idPath, int levelNum,
            int orderIndex, string categoryName, int description, string memo,
            int startRowIndex, int maximumRows, string orderBy)
        {
            return dal.GetItemCategories(itemCategoryId, parentId, idPath, levelNum,
                orderIndex, categoryName, description, memo, 
                startRowIndex,maximumRows, orderBy);
        }

        /// <summary>
        /// 是否可以上移节点
        /// </summary>
        /// <param name="itemCategoryId"></param>
        /// <returns></returns>
        public bool MoveUp(int itemCategoryId)
        {
            return dal.Move(itemCategoryId, true);
        }

        /// <summary>
        /// 是否可以下移节点
        /// </summary>
        /// <param name="itemCategoryId"></param>
        /// <returns></returns>
        public bool MoveDown(int itemCategoryId)
        {
            return dal.Move(itemCategoryId, false);
        }
    }
}
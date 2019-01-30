using System.Collections.Generic;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    /// <summary>
    /// ҵ���߼�����������
    /// </summary>
    public class ItemCategoryBLL
    {
        private static readonly ItemCategoryDAL dal = new ItemCategoryDAL();
    ��private SystemLogBLL objLogBll = new SystemLogBLL();

        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="itemCategory">��������</param>
        /// <returns>���ݿ���Ӱ�������</returns>
        public int AddItemCategory(ItemCategory itemCategory)
        {
            int id= dal.AddItemCategory(itemCategory);
            objLogBll.WriteLog("�����������ࡰ "+ itemCategory.CategoryName +"��������Ϣ");
            return id;
        }

        /// <summary>
        /// ɾ����������
        /// </summary>
        /// <param name="itemCategoryId">��������ID</param>
        /// <returns>���ݿ���Ӱ�������</returns>
        public void DeleteItemCategory(int itemCategoryId, ref int errorCode)
        {
            int code = 0;
            string strName = GetItemCategory(itemCategoryId).CategoryName;
            objLogBll.WriteLog("ɾ���������ࡰ " + strName + "��������Ϣ");
            dal.DeleteItemCategory(itemCategoryId,ref code);
            errorCode = code;
        }

        /// <summary>
        /// ɾ����������
        /// </summary>
        /// <param name="itemCategory">��������</param>
        /// <returns>���ݿ���Ӱ�������</returns>
        public void DeleteItemCategory(ItemCategory itemCategory)
        {
            int code = 0;
            string strName = GetItemCategory(itemCategory.ItemCategoryId).CategoryName;
            objLogBll.WriteLog("ɾ���������ࡰ " + strName + "��������Ϣ");
            dal.DeleteItemCategory(itemCategory.ItemCategoryId,ref code);
        }

        /// <summary>
        /// �޸ĸ�������
        /// </summary>
        /// <param name="itemCategory">��������</param>
        /// <returns>���ݿ���Ӱ�������</returns>
        public int UpdateItemCategory(ItemCategory itemCategory)
        {
            objLogBll.WriteLog("�޸ĸ������ࡰ " + itemCategory.CategoryName + "��������Ϣ");
            return dal.UpdateItemCategory(itemCategory);
        }

        /// <summary>
        /// ����������IDȡ��������
        /// </summary>
        /// <param name="itemCategoryId">��������ID</param>
        /// <returns>��������</returns>
        public ItemCategory GetItemCategory(int itemCategoryId)
        {
            return dal.GetItemCategory(itemCategoryId);
        }

        /// <summary>
        /// ��ѯ���и�������
        /// </summary>
        /// <returns>���и�������</returns>
        public IList<ItemCategory> GetItemCategories()
        {
            return dal.GetItemCategories();
        }

        /// <summary>
        /// ��ѯ���������ĸ�������
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
        /// <returns>���������ĸ�������</returns>
        public IList<ItemCategory> GetItemCategories(int itemCategoryId, int parentId, string idPath, int levelNum,
            int orderIndex, string categoryName, int description, string memo,
            int startRowIndex, int maximumRows, string orderBy)
        {
            return dal.GetItemCategories(itemCategoryId, parentId, idPath, levelNum,
                orderIndex, categoryName, description, memo, 
                startRowIndex,maximumRows, orderBy);
        }

        /// <summary>
        /// �Ƿ�������ƽڵ�
        /// </summary>
        /// <param name="itemCategoryId"></param>
        /// <returns></returns>
        public bool MoveUp(int itemCategoryId)
        {
            return dal.Move(itemCategoryId, true);
        }

        /// <summary>
        /// �Ƿ�������ƽڵ�
        /// </summary>
        /// <param name="itemCategoryId"></param>
        /// <returns></returns>
        public bool MoveDown(int itemCategoryId)
        {
            return dal.Move(itemCategoryId, false);
        }
    }
}
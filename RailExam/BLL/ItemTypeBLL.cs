using System.Collections.Generic;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    /// <summary>
    /// ҵ���߼����������
    /// </summary>
    public class ItemTypeBLL
    {
        private static readonly ItemTypeDAL dal = new ItemTypeDAL();

        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="itemType">�������</param>
        /// <returns>���ݿ���Ӱ�������</returns>
        public int AddItemType(ItemType itemType)
        {
            return dal.AddItemType(itemType);
        }

        /// <summary>
        /// ɾ���������
        /// </summary>
        /// <param name="itemTypeId">�������ID</param>
        /// <returns>���ݿ���Ӱ�������</returns>
        public int DeleteItemType(int itemTypeId)
        {
            return dal.DeleteItemType(itemTypeId);
        }

        /// <summary>
        /// �޸��������
        /// </summary>
        /// <param name="itemType">�������</param>
        /// <returns>���ݿ���Ӱ�������</returns>
        public int UpdateItemType(ItemType itemType)
        {
            return dal.UpdateItemType(itemType);
        }

        /// <summary>
        /// ���������IDȡ�������
        /// </summary>
        /// <param name="itemTypeId">�����ID</param>
        /// <returns>�������</returns>
        public ItemType GetItemType(int itemTypeId)
        {
            return dal.GetItemType(itemTypeId);
        }

        /// <summary>
        /// ��ѯ�����������
        /// </summary>
        /// <returns>�����������</returns>
        public IList<ItemType> GetItemTypes()
        {
            return dal.GetItemTypes();
        }

        /// <summary>
        /// ��ѯ�����������
        /// </summary>
        /// <param name="bForSearchUse">
        /// �Ƿ񹩲�ѯ�ã����ڣ�
        /// һ���ǣ������һ��ʾ�
        /// �������������ʾ�
        /// </param>
        /// <returns>�����������</returns>
        public IList<ItemType> GetItemTypes(bool bForSearchUse)
        {
            if(! bForSearchUse)
            {
                return GetItemTypes();
            }

            IList<ItemType> itemTypes = GetItemTypes();
            ItemType itemType = new ItemType();

            itemType.TypeName = "-����-";
            itemType.ItemTypeId = -1;
            itemTypes.Insert(0, itemType);

            return itemTypes;
        }

        /// <summary>
        /// ��ѯ�����������������
        /// </summary>
        public IList<ItemType> GetItemTypes(int itemTypeId, string typeName,
            string imageFileName, string description, int isDefault, int isValid, string memo)
        {
            return dal.GetItemTypes(itemTypeId, typeName, imageFileName, description, isDefault, isValid, memo);
        }
    }
}
using System.Collections.Generic;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    /// <summary>
    /// ҵ���߼�������״̬
    /// </summary>
    public class ItemStatusBLL
    {
        private static readonly ItemStatusDAL dal = new ItemStatusDAL();

        /// <summary>
        /// ��������״̬
        /// </summary>
        /// <param name="itemStatus">����״̬</param>
        /// <returns>���ݿ���Ӱ�������</returns>
        public int AddItemStatus(ItemStatus itemStatus)
        {
            return dal.AddItemStatus(itemStatus);
        }

        /// <summary>
        /// ɾ������״̬
        /// </summary>
        /// <param name="itemStatusId">����״̬ID</param>
        /// <returns>���ݿ���Ӱ�������</returns>
        public int DeleteItemStatus(int itemStatusId)
        {
            return dal.DeleteItemStatus(itemStatusId);
        }

        /// <summary>
        /// �޸�����״̬
        /// </summary>
        /// <param name="itemStatus">����״̬</param>
        /// <returns>���ݿ���Ӱ�������</returns>
        public int UpdateItemStatus(ItemStatus itemStatus)
        {
            return dal.UpdateItemStatus(itemStatus);
        }

        /// <summary>
        /// ������״̬IDȡ����״̬
        /// </summary>
        /// <param name="itemStatusId">����״̬ID</param>
        /// <returns>����״̬</returns>
        public ItemStatus GetItemStatus(int itemStatusId)
        {
            return dal.GetItemStatus(itemStatusId);
        }

        /// <summary>
        /// ��ѯ��������״̬
        /// </summary>
        /// <returns>��������״̬</returns>
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

            itemType.StatusName = "-״̬-";
            itemType.ItemStatusId = -1;
            itemTypes.Insert(0, itemType);

            return itemTypes;
        }


        /// <summary>
        /// ��ѯ��������������״̬
        /// </summary>
        /// <param name="itemStatusId"></param>
        /// <param name="statusName"></param>
        /// <param name="description"></param>
        /// <param name="isDefault"></param>
        /// <param name="memo"></param>
        /// <returns>��������������״̬</returns>
        public IList<ItemStatus> GetItemStatuss(int itemStatusId, string statusName,
            string description, int isDefault, string memo)
        {
            return dal.GetItemStatuss(itemStatusId, statusName,
                                      description, isDefault, memo);
        }
    }
}
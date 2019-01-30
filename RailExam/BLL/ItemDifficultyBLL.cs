using System.Collections.Generic;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    /// <summary>
    /// ҵ���߼��������Ѷ�
    /// </summary>
    public class ItemDifficultyBLL
    {
        private static readonly ItemDifficultyDAL dal = new ItemDifficultyDAL();

        /// <summary>
        /// ���������Ѷ�
        /// </summary>
        /// <param name="itemDifficulty">�����Ѷ�</param>
        /// <returns>���ݿ���Ӱ�������</returns>
        public int AddItemDifficulty(ItemDifficulty itemDifficulty)
        {
            return dal.AddItemDifficulty(itemDifficulty);
        }

        /// <summary>
        /// ɾ�������Ѷ�
        /// </summary>
        /// <param name="itemDifficultyId">�����Ѷ�ID</param>
        /// <returns>���ݿ���Ӱ�������</returns>
        public int DeleteItemDifficulty(int itemDifficultyId)
        {
            return dal.DeleteItemDifficulty(itemDifficultyId);
        }

        public int DeleteItemDifficulty(ItemDifficulty itemDifficulty)
        {
            return DeleteItemDifficulty(itemDifficulty.ItemDifficultyId);
        }

        /// <summary>
        /// �޸������Ѷ�
        /// </summary>
        /// <param name="itemDifficulty">�����Ѷ�</param>
        /// <returns>���ݿ���Ӱ�������</returns>
        public int UpdateItemDifficulty(ItemDifficulty itemDifficulty)
        {
            return dal.UpdateItemDifficulty(itemDifficulty);
        }

        /// <summary>
        /// �������Ѷ�IDȡ�����Ѷ�
        /// </summary>
        /// <returns>�����Ѷ�</returns>
        public ItemDifficulty GetItemDifficulty(int itemDifficultyId)
        {
            return dal.GetItemDifficulty(itemDifficultyId);
        }

        /// <summary>
        /// ��ѯ���������Ѷ�
        /// </summary>
        /// <returns>���������Ѷ�</returns>
        public IList<ItemDifficulty> GetItemDifficulties()
        {
            return dal.GetItemDifficulties();
        }

        /// <summary>
        /// ��ѯ���������Ѷ�
        /// </summary>
        /// <param name="bForSearchUse">
        /// �Ƿ񹩲�ѯ�ã����ڣ�
        /// һ���ǣ������һ��ʾ�
        /// �������������ʾ�
        /// </param>
        /// <returns>���������Ѷ�</returns>
        public IList<ItemDifficulty> GetItemDifficulties(bool bForSearchUse)
        {
            if(! bForSearchUse)
            {
                return GetItemDifficulties();
            }

            IList<ItemDifficulty> difficulties = GetItemDifficulties();
            ItemDifficulty difficulty = new ItemDifficulty();

            difficulty.DifficultyName = "-�Ѷ�-";
            difficulty.ItemDifficultyId = -1;
            difficulties.Insert(0, difficulty);

            return difficulties;
        }

        /// <summary>
        /// ��ѯ���������������Ѷ�
        /// </summary>
        /// <param name="itemDifficultyId"></param>
        /// <param name="difficultyName"></param>
        /// <param name="difficultyValue"></param>
        /// <param name="description"></param>
        /// <param name="isDefault"></param>
        /// <param name="memo"></param>
        /// <returns>���������������Ѷ�</returns>
        public IList<ItemDifficulty> GetItemDifficulties(int itemDifficultyId, string difficultyName,
            int difficultyValue, string description, int isDefault, string memo)
        {
            return dal.GetItemDifficulties(itemDifficultyId, difficultyName, difficultyValue, description, isDefault, memo);
        }
    }
}
using System.Collections.Generic;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    /// <summary>
    /// ҵ���߼�����������
    /// </summary>
    public class ItemConfigBLL
    {
        private static readonly ItemConfigDAL dal = new ItemConfigDAL();
        private SystemLogBLL objLogBll = new SystemLogBLL();

        /// <summary>
        /// �޸���������
        /// </summary>
        /// <param name="itemConfig">��������</param>
        /// <returns>���ݿ���Ӱ�������</returns>
        public int UpdateItemConfig(ItemConfig itemConfig)
        {
            objLogBll.WriteLog("�޸�����Ĭ������");
            return dal.UpdateItemConfig(itemConfig);
        }
		public int InsertItemConfig(ItemConfig itemConfig)
		{
			objLogBll.WriteLog("������������");
			return dal.AddItemConfig(itemConfig);
		}
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <returns>��������</returns>
        public ItemConfig GetItemConfig()
        {
            ItemConfig obj = dal.GetItemConfig();
            if(obj.HasPicture == 1)
            {
                obj.HasPictureText = "ͼƬ";
            }
            else
            {
                obj.HasPictureText = "�ı�";
            }
            return obj;
        }

		public ItemConfig GetItemConfig(int employeeID)
		{
			ItemConfig obj = dal.GetItemConfig(employeeID);
			if (obj.HasPicture == 1)
			{
				obj.HasPictureText = "ͼƬ";
			}
			else
			{
				obj.HasPictureText = "�ı�";
			}
			return obj;
		}
    }
}
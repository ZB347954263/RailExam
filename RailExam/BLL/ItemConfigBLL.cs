using System.Collections.Generic;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    /// <summary>
    /// 业务逻辑：试题设置
    /// </summary>
    public class ItemConfigBLL
    {
        private static readonly ItemConfigDAL dal = new ItemConfigDAL();
        private SystemLogBLL objLogBll = new SystemLogBLL();

        /// <summary>
        /// 修改试题设置
        /// </summary>
        /// <param name="itemConfig">试题设置</param>
        /// <returns>数据库受影响的行数</returns>
        public int UpdateItemConfig(ItemConfig itemConfig)
        {
            objLogBll.WriteLog("修改试题默认设置");
            return dal.UpdateItemConfig(itemConfig);
        }
		public int InsertItemConfig(ItemConfig itemConfig)
		{
			objLogBll.WriteLog("增加试题设置");
			return dal.AddItemConfig(itemConfig);
		}
        /// <summary>
        /// 获取试题设置
        /// </summary>
        /// <returns>试题设置</returns>
        public ItemConfig GetItemConfig()
        {
            ItemConfig obj = dal.GetItemConfig();
            if(obj.HasPicture == 1)
            {
                obj.HasPictureText = "图片";
            }
            else
            {
                obj.HasPictureText = "文本";
            }
            return obj;
        }

		public ItemConfig GetItemConfig(int employeeID)
		{
			ItemConfig obj = dal.GetItemConfig(employeeID);
			if (obj.HasPicture == 1)
			{
				obj.HasPictureText = "图片";
			}
			else
			{
				obj.HasPictureText = "文本";
			}
			return obj;
		}
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
	/// <summary>
	/// 业务实体：组织机构
	/// </summary>
	public class Organization
	{
	    #region 内部成员变量

	    private int _organizationId = 0;
	    private int _parentId = 1;
	    private string _idPath = string.Empty;
	    private int _levelNum = 2;
	    private int _orderIndex = 1;
	    private string _shortName = string.Empty;
	    private string _fullName = string.Empty;
	    private string _address = string.Empty;
	    private string _postCode = string.Empty;
	    private string _contactPerson = string.Empty;
	    private string _phone = string.Empty;
	    private string _webSite = string.Empty;
	    private string _email = string.Empty;
	    private string _description = string.Empty;
	    private string _memo = string.Empty;
	    private string _synchronizeTime = string.Empty;
		private int _suitRange = 0;
	    private int _railSystemId = 0;
	    private string _railSystemName = string.Empty;
        private bool _isEffect = true;

	    #endregion

		
		/// <summary>
		/// 缺省构造函数
		/// </summary>
		public Organization()  { }

		/// <summary>
		/// 带参数的构造函数
		/// </summary>
		/// <param name="organizationId">组织机构ID</param>
		/// <param name="parentId">父机构ID</param>
		/// <param name="idPath">机构ID路径</param>
		/// <param name="levelNum">机构级别</param>
		/// <param name="orderIndex">排序索引</param>
		/// <param name="shortName">简称</param>
		/// <param name="fullName">全称</param>
		/// <param name="address">地址</param>
		/// <param name="postCode">邮政编码</param>
		/// <param name="contactPerson">联系人</param>
		/// <param name="phone">电话</param>
		/// <param name="webSite">网址</param>
		/// <param name="email">电子邮件</param>
		/// <param name="description">简介</param>
		/// <param name="memo">备注</param>
		public Organization(
			int? organizationId,
			int? parentId,
			string idPath,
			int? levelNum,
			int? orderIndex,
			string shortName,
			string fullName,
			string address,
			string postCode,
			string contactPerson,
			string phone,
			string webSite,
			string email,
			string description,
			string memo)
		{
			_organizationId = organizationId ?? _organizationId;
			_parentId = parentId ?? _parentId;
			_idPath = idPath;
			_levelNum = levelNum ?? _levelNum;
			_orderIndex = orderIndex ?? _orderIndex;
			_shortName = shortName;
			_fullName = fullName;
			_address = address;
			_postCode = postCode;
			_contactPerson = contactPerson;
			_phone = phone;
			_webSite = webSite;
			_email = email;
			_description = description;
			_memo = memo;
		}

	    #region 属性

        /// <summary>
        /// 组织机构编号
        /// </summary>
	    public int OrganizationId
	    {
	        set
	        {
	            _organizationId = value;
	        }
	        get
	        {
	            return _organizationId;
	        }
	    }
		
        /// <summary>
        /// 父级组织机构编号
        /// </summary>
	    public int ParentId
	    {
	        set
	        {
	            _parentId = value;
	        }
	        get
	        {
	            return _parentId;
	        }
	    }

        /// <summary>
        /// 组织机构树路径
        /// </summary>
	    public string IdPath
	    {
	        set
	        {
	            _idPath = value;
	        }
	        get
	        {
	            return _idPath;
	        }
	    }

        /// <summary>
        /// 深度
        /// </summary>
	    public int LevelNum
	    {
	        set
	        {
	            _levelNum = value;
	        }
	        get
	        {
	            return _levelNum;
	        }
	    }

        /// <summary>
        /// 排序序号
        /// </summary>
	    public int OrderIndex
	    {
	        set
	        {
	            _orderIndex = value;
	        }
	        get
	        {
	            return _orderIndex;
	        }
	    }

        /// <summary>
        /// 组织机构简称
        /// </summary>
	    public string ShortName
	    {
	        set
	        {
	            _shortName = value;
	        }
	        get
	        {
	            return _shortName;
	        }
	    }

        /// <summary>
        /// 组织机构全称
        /// </summary>
	    public string FullName
	    {
	        set
	        {
	            _fullName = value;
	        }
	        get
	        {
	            return _fullName;
	        }
	    }

        /// <summary>
        /// 组织机构地址
        /// </summary>
	    public string Address
	    {
	        set
	        {
	            _address = value;
	        }
	        get
	        {
	            return _address;
	        }
	    }

        /// <summary>
        /// 组织机构邮编
        /// </summary>
	    public string PostCode
	    {
	        set
	        {
	            _postCode = value;
	        }
	        get
	        {
	            return _postCode;
	        }
	    }

        /// <summary>
        /// 组织机构联系人
        /// </summary>
	    public string ContactPerson
	    {
	        set
	        {
	            _contactPerson = value;
	        }
	        get
	        {
	            return _contactPerson;
	        }
	    }

        /// <summary>
        /// 组织机构联系电话
        /// </summary>
	    public string Phone
	    {
	        set
	        {
	            _phone = value;
	        }
	        get
	        {
	            return _phone;
	        }
	    }

        /// <summary>
        /// 组织机构网址
        /// </summary>
	    public string WebSite
	    {
	        set
	        {
	            _webSite = value;
	        }
	        get
	        {
	            return _webSite;
	        }
	    }

        /// <summary>
        /// 组织机构电邮
        /// </summary>
	    public string Email
	    {
	        set
	        {
	            _email = value;
	        }
	        get
	        {
	            return _email;
	        }
	    }

        /// <summary>
        /// 组织机构说明
        /// </summary>
	    public string Description
	    {
	        set
	        {
	            _description = value;
	        }
	        get
	        {
	            return _description;
	        }
	    }

        /// <summary>
        /// 组织机构备注
        /// </summary>
	    public string Memo
	    {
	        set
	        {
	            _memo = value;
	        }
	        get
	        {
	            return _memo;
	        }
	    }

	    public string SynchronizeTime
	    {
	        set
	        {
	            _synchronizeTime = value;
	        }
            get
            {
                return _synchronizeTime;
            }
	    }

		public int SuitRange
		{
			set
			{
				_suitRange = value;
			}
			get
			{
				return _suitRange;
			}
		}

        public int RailSystemID
        {
            set
            {
                _railSystemId = value;
            }
            get
            {
                return _railSystemId;
            }
        }

        public string RailSystemName
        {
            set
            {
                _railSystemName = value;
            }
            get
            {
                return _railSystemName;
            }
        }

	    public bool IsEffect
	    {
            set
            {
                _isEffect = value;
            }
            get
            {
                return _isEffect;
            }
	    }

	    #endregion

	}
}

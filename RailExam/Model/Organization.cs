using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
	/// <summary>
	/// ҵ��ʵ�壺��֯����
	/// </summary>
	public class Organization
	{
	    #region �ڲ���Ա����

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
		/// ȱʡ���캯��
		/// </summary>
		public Organization()  { }

		/// <summary>
		/// �������Ĺ��캯��
		/// </summary>
		/// <param name="organizationId">��֯����ID</param>
		/// <param name="parentId">������ID</param>
		/// <param name="idPath">����ID·��</param>
		/// <param name="levelNum">��������</param>
		/// <param name="orderIndex">��������</param>
		/// <param name="shortName">���</param>
		/// <param name="fullName">ȫ��</param>
		/// <param name="address">��ַ</param>
		/// <param name="postCode">��������</param>
		/// <param name="contactPerson">��ϵ��</param>
		/// <param name="phone">�绰</param>
		/// <param name="webSite">��ַ</param>
		/// <param name="email">�����ʼ�</param>
		/// <param name="description">���</param>
		/// <param name="memo">��ע</param>
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

	    #region ����

        /// <summary>
        /// ��֯�������
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
        /// ������֯�������
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
        /// ��֯������·��
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
        /// ���
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
        /// �������
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
        /// ��֯�������
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
        /// ��֯����ȫ��
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
        /// ��֯������ַ
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
        /// ��֯�����ʱ�
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
        /// ��֯������ϵ��
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
        /// ��֯������ϵ�绰
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
        /// ��֯������ַ
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
        /// ��֯��������
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
        /// ��֯����˵��
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
        /// ��֯������ע
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

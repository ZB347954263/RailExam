using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.ComponentModel;
using System.Collections.Generic;
using RailExamWebApp.Common.Class;

/// <summary>
/// OjbData 的摘要说明
/// </summary>
[DataObject]
public class OjbData
{
    public OjbData()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    [DataObjectMethod(DataObjectMethodType.Select, true)]
    public DataTable Get(string sql)
    {
        if (!String.IsNullOrEmpty(sql))
        {
            OracleAccess oracleAccess = new OracleAccess();
            DataSet ds = oracleAccess.RunSqlDataSet(sql);
            return ds.Tables[0];
        }
        else
        {
            return null;
        }
    }
}

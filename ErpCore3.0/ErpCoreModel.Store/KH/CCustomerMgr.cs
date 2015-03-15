// File:    CCustomerMgr.cs
// Created: 2012/11/28 21:13:40
// Purpose: Definition of Class CCustomerMgr

using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Store
{
    public class CCustomerMgr : CBaseObjectMgr
    {

        public CCustomerMgr()
        {
            TbCode = "KH_Customer";
            ClassName = "ErpCoreModel.Store.CCustomer";
            Assembly assembly = Assembly.LoadFrom(Assembly.GetExecutingAssembly().Location);
            ObjType = assembly.GetType(ClassName);
        }

        public CCustomer FindByName(string sName)
        {
            List<CBaseObject> lstObj = GetList();
            var lstC = from obj in lstObj where (obj as CCustomer).Name.Equals(sName, StringComparison.OrdinalIgnoreCase)  select obj;
            if (lstC.Count() > 0)
                return (CCustomer)lstC.First();
            else
                return null;
        }

        public override bool AddNew(CBaseObject obj, bool bSave)
        {
            if (!base.AddNew(obj, bSave))
                return false;
            //生成客户的账户
            CCustomer customer = (CCustomer)obj;

            CAccount account = new CAccount();
            account.Ctx = Ctx;
            account.KH_Customer_id = customer.Id;
            return customer.AccountMgr.AddNew(account);
        }
    }
}
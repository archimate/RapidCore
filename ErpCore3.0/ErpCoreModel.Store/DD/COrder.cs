// File:    COrder.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2012/11/28 21:13:40
// Purpose: Definition of Class COrder

using System;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Store
{
    //订单状态：0-初始；1-受理中；2-结束；3-废弃；4-用户撤单
    public enum OrderState { Init, Accept, Finish, Disavailable,Cancel }
    //评分：0:未评论；1：赞；2：一般；3：差
    public enum ScoringType { NoScoring, Good, General,Bad }
    //付款方式：0-从账户扣;1-货到付款
    public enum PayModeType { Account, Delivery }
    
    public class COrder : CBaseObject
    {

        public COrder()
        {
            TbCode = "DD_Order";
            ClassName = "ErpCoreModel.Store.COrder";

        }
        //获取状态名称
        public string GetStateStr()
        {
            string sState = "";
            if (State == OrderState.Init)
                sState = "未处理";
            else if (State == OrderState.Accept)
                sState = "已处理";
            else if (State == OrderState.Finish)
                sState = "已结束";
            else if (State == OrderState.Disavailable)
                sState = "已作废";
            else if (State == OrderState.Cancel)
                sState = "已撤单";

            return sState;
        }
        
        public string Code
        {
            get
            {
                if (m_arrNewVal.ContainsKey("code"))
                    return m_arrNewVal["code"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("code"))
                    m_arrNewVal["code"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("code", val);
                }
            }
        }
        public Guid KH_Customer_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("kh_customer_id"))
                    return m_arrNewVal["kh_customer_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("kh_customer_id"))
                    m_arrNewVal["kh_customer_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("kh_customer_id", val);
                }
            }
        }
        public Guid B_Province_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("b_province_id"))
                    return m_arrNewVal["b_province_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("b_province_id"))
                    m_arrNewVal["b_province_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("b_province_id", val);
                }
            }
        }
        public Guid B_City_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("b_city_id"))
                    return m_arrNewVal["b_city_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("b_city_id"))
                    m_arrNewVal["b_city_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("b_city_id", val);
                }
            }
        }
        public Guid B_District_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("b_district_id"))
                    return m_arrNewVal["b_district_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("b_district_id"))
                    m_arrNewVal["b_district_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("b_district_id", val);
                }
            }
        }
        public string Addr
        {
            get
            {
                if (m_arrNewVal.ContainsKey("addr"))
                    return m_arrNewVal["addr"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("addr"))
                    m_arrNewVal["addr"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("addr", val);
                }
            }
        }
        public string Zipcode
        {
            get
            {
                if (m_arrNewVal.ContainsKey("zipcode"))
                    return m_arrNewVal["zipcode"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("zipcode"))
                    m_arrNewVal["zipcode"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("zipcode", val);
                }
            }
        }
        public string Contacts
        {
            get
            {
                if (m_arrNewVal.ContainsKey("contacts"))
                    return m_arrNewVal["contacts"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("contacts"))
                    m_arrNewVal["contacts"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("contacts", val);
                }
            }
        }
        public string Tel
        {
            get
            {
                if (m_arrNewVal.ContainsKey("tel"))
                    return m_arrNewVal["tel"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("tel"))
                    m_arrNewVal["tel"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("tel", val);
                }
            }
        }
        public string Phone
        {
            get
            {
                if (m_arrNewVal.ContainsKey("phone"))
                    return m_arrNewVal["phone"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("phone"))
                    m_arrNewVal["phone"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("phone", val);
                }
            }
        }
        public double OtherCharge
        {
            get
            {
                if (m_arrNewVal.ContainsKey("othercharge"))
                    return m_arrNewVal["othercharge"].DoubleVal;
                else
                    return 0;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("othercharge"))
                    m_arrNewVal["othercharge"].DoubleVal = value;
                else
                {
                    CValue val = new CValue();
                    val.DoubleVal = value;
                    m_arrNewVal.Add("othercharge", val);
                }
            }
        }
        public double ShipCharge
        {
            get
            {
                if (m_arrNewVal.ContainsKey("shipcharge"))
                    return m_arrNewVal["shipcharge"].DoubleVal;
                else
                    return 0;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("shipcharge"))
                    m_arrNewVal["shipcharge"].DoubleVal = value;
                else
                {
                    CValue val = new CValue();
                    val.DoubleVal = value;
                    m_arrNewVal.Add("shipcharge", val);
                }
            }
        }
        public double Discount
        {
            get
            {
                if (m_arrNewVal.ContainsKey("discount"))
                    return m_arrNewVal["discount"].DoubleVal;
                else
                    return 0;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("discount"))
                    m_arrNewVal["discount"].DoubleVal = value;
                else
                {
                    CValue val = new CValue();
                    val.DoubleVal = value;
                    m_arrNewVal.Add("discount", val);
                }
            }
        }
        public Guid B_User_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("b_user_id"))
                    return m_arrNewVal["b_user_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("b_user_id"))
                    m_arrNewVal["b_user_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("b_user_id", val);
                }
            }
        }
        public string Remarks
        {
            get
            {
                if (m_arrNewVal.ContainsKey("remarks"))
                    return m_arrNewVal["remarks"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("remarks"))
                    m_arrNewVal["remarks"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("remarks", val);
                }
            }
        }
        public OrderState State
        {
            get
            {
                if (m_arrNewVal.ContainsKey("state"))
                    return (OrderState)m_arrNewVal["state"].IntVal;
                else
                    return OrderState.Init;
            }
            set
            {           
                if (m_arrNewVal.ContainsKey("state"))
                    m_arrNewVal["state"].IntVal = (int)value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = (int)value;
                    m_arrNewVal.Add("state", val);
                }
            }
        }
        public string Comment
        {
            get
            {
                if (m_arrNewVal.ContainsKey("comment"))
                    return m_arrNewVal["comment"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("comment"))
                    m_arrNewVal["comment"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("comment", val);
                }
            }
        }
        public ScoringType Scoring
        {
            get
            {
                if (m_arrNewVal.ContainsKey("scoring"))
                    return (ScoringType)m_arrNewVal["scoring"].IntVal;
                else
                    return ScoringType.NoScoring;
            }
            set
            {           
                if (m_arrNewVal.ContainsKey("scoring"))
                    m_arrNewVal["scoring"].IntVal = (int)value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = (int)value;
                    m_arrNewVal.Add("scoring", val);
                }
            }
        }

        public PayModeType PayMode
        {
            get
            {
                if (m_arrNewVal.ContainsKey("paymode"))
                    return (PayModeType)m_arrNewVal["paymode"].IntVal;
                else
                    return PayModeType.Account;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("paymode"))
                    m_arrNewVal["paymode"].IntVal = (int)value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = (int)value;
                    m_arrNewVal.Add("paymode", val);
                }
            }
        }


        public COrderDetailMgr OrderDetailMgr
        {
            get
            {
                return (COrderDetailMgr)this.GetSubObjectMgr("DD_OrderDetail", typeof(COrderDetailMgr));
            }
            set
            {
                this.SetSubObjectMgr("DD_OrderDetail", value);
            }
        }
    }
}
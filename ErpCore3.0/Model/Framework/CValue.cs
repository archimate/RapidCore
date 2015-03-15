// File:    CValue.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2011年7月9日 22:32:15
// Purpose: Definition of Class CValue

using System;
using System.Text;

namespace ErpCoreModel.Framework
{

    /// 保存数据库中多种类型的值
    public class CValue
    {
        private Guid guidVal = Guid.Empty;
        private string strVal = "";
        private int intVal = 0;
        private long longVal = 0;
        private bool boolVal = false;
        private double doubleVal = 0.0;
        private DateTime datetimeVal = DateTime.Now;
        private object objectVal = null;

        public Guid GuidVal
        {
            get
            {
                return guidVal;
            }
            set
            {
                this.guidVal = value;
            }
        }

        public string StrVal
        {
            get
            {
                return strVal;
            }
            set
            {
                this.strVal = value;
            }
        }

        public int IntVal
        {
            get
            {
                return intVal;
            }
            set
            {
                this.intVal = value;
            }
        }

        public long LongVal
        {
            get
            {
                return longVal;
            }
            set
            {
                this.longVal = value;
            }
        }

        public bool BoolVal
        {
            get
            {
                return boolVal;
            }
            set
            {
                this.boolVal = value;
            }
        }

        public double DoubleVal
        {
            get
            {
                return doubleVal;
            }
            set
            {
                this.doubleVal = value;
            }
        }

        public DateTime DatetimeVal
        {
            get
            {
                return datetimeVal;
            }
            set
            {
                this.datetimeVal = value;
            }
        }

        public object ObjectVal
        {
            get
            {
                return objectVal;
            }
            set
            {
                this.objectVal = value;
            }
        }

        public CValue Clone()
        {
            CValue val = new CValue();
            val.GuidVal = this.GuidVal;
            val.StrVal = this.StrVal;
            val.IntVal = this.IntVal;
            val.LongVal = this.LongVal;
            val.BoolVal = this.BoolVal;
            val.DatetimeVal = this.DatetimeVal;
            val.DoubleVal = this.DoubleVal;
            val.ObjectVal = this.ObjectVal;

            return val;
        }
    }
}
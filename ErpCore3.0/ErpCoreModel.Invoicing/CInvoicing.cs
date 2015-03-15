// File:    CInvoicing.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2013/3/1 21:13:40
// Purpose: Definition of Class CInvoicing

using System;
using System.Collections.Generic;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Invoicing
{
    public class CInvoicing : CBaseObject
    {
        private CPurchaseNoteMgr purchaseNoteMgr = null;
        private CPurchaseOrderMgr purchaseOrderMgr = null;
        private CPurchaseReturnNoteMgr purchaseReturnNoteMgr = null;
        private CAllocateNoteMgr allocateNoteMgr = null;
        private CLossReportMgr lossReportMgr = null;
        private COverflowReportMgr overflowReportMgr = null;
        private CStorageNoteMgr storageNoteMgr = null;
        private CPriceNoteMgr priceNoteMgr = null;
        private CRepairNoteMgr repairNoteMgr = null;
        private CSaleNoteMgr saleNoteMgr = null;
        private CSaleReturnNoteMgr saleReturnNoteMgr = null;

        public CSaleReturnNoteMgr SaleReturnNoteMgr
        {
            get
            {
                if (saleReturnNoteMgr == null)
                {
                    saleReturnNoteMgr = new CSaleReturnNoteMgr();
                    saleReturnNoteMgr.Ctx = Ctx;
                    saleReturnNoteMgr.Load("", false);
                    Ctx.AddBaseObjectMgrCache(saleReturnNoteMgr.TbCode, Guid.Empty, saleReturnNoteMgr);
                }
                return saleReturnNoteMgr;
            }
            set
            {
                this.saleReturnNoteMgr = value;
            }
        }
        public CSaleNoteMgr SaleNoteMgr
        {
            get
            {
                if (saleNoteMgr == null)
                {
                    saleNoteMgr = new CSaleNoteMgr();
                    saleNoteMgr.Ctx = Ctx;
                    saleNoteMgr.Load("", false);
                    Ctx.AddBaseObjectMgrCache(saleNoteMgr.TbCode, Guid.Empty, saleNoteMgr);
                }
                return saleNoteMgr;
            }
            set
            {
                this.saleNoteMgr = value;
            }
        }
        public CRepairNoteMgr RepairNoteMgr
        {
            get
            {
                if (repairNoteMgr == null)
                {
                    repairNoteMgr = new CRepairNoteMgr();
                    repairNoteMgr.Ctx = Ctx;
                    repairNoteMgr.Load("", false);
                    Ctx.AddBaseObjectMgrCache(repairNoteMgr.TbCode, Guid.Empty, repairNoteMgr);
                }
                return repairNoteMgr;
            }
            set
            {
                this.repairNoteMgr = value;
            }
        }
        public CPriceNoteMgr PriceNoteMgr
        {
            get
            {
                if (priceNoteMgr == null)
                {
                    priceNoteMgr = new CPriceNoteMgr();
                    priceNoteMgr.Ctx = Ctx;
                    priceNoteMgr.Load("", false);
                    Ctx.AddBaseObjectMgrCache(priceNoteMgr.TbCode, Guid.Empty, priceNoteMgr);
                }
                return priceNoteMgr;
            }
            set
            {
                this.priceNoteMgr = value;
            }
        }
        public CStorageNoteMgr StorageNoteMgr
        {
            get
            {
                if (storageNoteMgr == null)
                {
                    storageNoteMgr = new CStorageNoteMgr();
                    storageNoteMgr.Ctx = Ctx;
                    storageNoteMgr.Load("", false);
                    Ctx.AddBaseObjectMgrCache(storageNoteMgr.TbCode, Guid.Empty, storageNoteMgr);
                }
                return storageNoteMgr;
            }
            set
            {
                this.storageNoteMgr = value;
            }
        }
        public COverflowReportMgr OverflowReportMgr
        {
            get
            {
                if (overflowReportMgr == null)
                {
                    overflowReportMgr = new COverflowReportMgr();
                    overflowReportMgr.Ctx = Ctx;
                    overflowReportMgr.Load("", false);
                    Ctx.AddBaseObjectMgrCache(overflowReportMgr.TbCode, Guid.Empty, overflowReportMgr);
                }
                return overflowReportMgr;
            }
            set
            {
                this.overflowReportMgr = value;
            }
        }
        public CLossReportMgr LossReportMgr
        {
            get
            {
                if (lossReportMgr == null)
                {
                    lossReportMgr = new CLossReportMgr();
                    lossReportMgr.Ctx = Ctx;
                    lossReportMgr.Load("", false);
                    Ctx.AddBaseObjectMgrCache(lossReportMgr.TbCode, Guid.Empty, lossReportMgr);
                }
                return lossReportMgr;
            }
            set
            {
                this.lossReportMgr = value;
            }
        }
        public CAllocateNoteMgr AllocateNoteMgr
        {
            get
            {
                if (allocateNoteMgr == null)
                {
                    allocateNoteMgr = new CAllocateNoteMgr();
                    allocateNoteMgr.Ctx = Ctx;
                    allocateNoteMgr.Load("", false);
                    Ctx.AddBaseObjectMgrCache(allocateNoteMgr.TbCode, Guid.Empty, allocateNoteMgr);
                }
                return allocateNoteMgr;
            }
            set
            {
                this.allocateNoteMgr = value;
            }
        }
        public CPurchaseReturnNoteMgr PurchaseReturnNoteMgr
        {
            get
            {
                if (purchaseReturnNoteMgr == null)
                {
                    purchaseReturnNoteMgr = new CPurchaseReturnNoteMgr();
                    purchaseReturnNoteMgr.Ctx = Ctx;
                    purchaseReturnNoteMgr.Load("", false);
                    Ctx.AddBaseObjectMgrCache(purchaseReturnNoteMgr.TbCode, Guid.Empty, purchaseReturnNoteMgr);
                }
                return purchaseReturnNoteMgr;
            }
            set
            {
                this.purchaseReturnNoteMgr = value;
            }
        }
        public CPurchaseOrderMgr PurchaseOrderMgr
        {
            get
            {
                if (purchaseOrderMgr == null)
                {
                    purchaseOrderMgr = new CPurchaseOrderMgr();
                    purchaseOrderMgr.Ctx = Ctx;
                    purchaseOrderMgr.Load("", false);
                    Ctx.AddBaseObjectMgrCache(purchaseOrderMgr.TbCode, Guid.Empty, purchaseOrderMgr);
                }
                return purchaseOrderMgr;
            }
            set
            {
                this.purchaseOrderMgr = value;
            }
        }
        public CPurchaseNoteMgr PurchaseNoteMgr
        {
            get
            {
                if (purchaseNoteMgr == null)
                {
                    purchaseNoteMgr = new CPurchaseNoteMgr();
                    purchaseNoteMgr.Ctx = Ctx;
                    purchaseNoteMgr.Load("", false);
                    Ctx.AddBaseObjectMgrCache(purchaseNoteMgr.TbCode, Guid.Empty, purchaseNoteMgr);
                }
                return purchaseNoteMgr;
            }
            set
            {
                this.purchaseNoteMgr = value;
            }
        }
    }
}

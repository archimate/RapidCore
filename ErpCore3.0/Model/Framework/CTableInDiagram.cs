// File:    CTableInDiagram.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2011年7月9日 13:37:16
// Purpose: Definition of Class CTableInDiagram
using System;
using System.Collections.Generic;
using System.Text;

namespace ErpCoreModel.Framework
{
    public class CTableInDiagram : CBaseObject
    {
        public CTableInDiagram()
        {
            TbCode = "FW_TableInDiagram";
            ClassName = "ErpCoreModel.Framework.CTableInDiagram";
        }

        public Guid FW_Diagram_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("fw_diagram_id"))
                    return m_arrNewVal["fw_diagram_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("fw_diagram_id"))
                    m_arrNewVal["fw_diagram_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("fw_diagram_id", val);
                }
            }
        }

        public Guid FW_Table_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("fw_table_id"))
                    return m_arrNewVal["fw_table_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("fw_table_id"))
                    m_arrNewVal["fw_table_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("fw_table_id", val);
                }
            }
        }

        public int X
        {
            get
            {
                if (m_arrNewVal.ContainsKey("x"))
                    return m_arrNewVal["x"].IntVal;
                else
                    return 0;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("x"))
                    m_arrNewVal["x"].IntVal = value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = value;
                    m_arrNewVal.Add("x", val);
                }
            }
        }

        public int Y
        {
            get
            {
                if (m_arrNewVal.ContainsKey("y"))
                    return m_arrNewVal["y"].IntVal;
                else
                    return 0;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("y"))
                    m_arrNewVal["y"].IntVal = value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = value;
                    m_arrNewVal.Add("y", val);
                }
            }
        }

        public int Width
        {
            get
            {
                if (m_arrNewVal.ContainsKey("width"))
                    return m_arrNewVal["width"].IntVal;
                else
                    return 0;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("width"))
                    m_arrNewVal["width"].IntVal = value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = value;
                    m_arrNewVal.Add("width", val);
                }
            }
        }

        public int Height
        {
            get
            {
                if (m_arrNewVal.ContainsKey("height"))
                    return m_arrNewVal["height"].IntVal;
                else
                    return 0;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("height"))
                    m_arrNewVal["height"].IntVal = value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = value;
                    m_arrNewVal.Add("height", val);
                }
            }
        }

        public bool IsStandard
        {
            get
            {
                if (m_arrNewVal.ContainsKey("isstandard"))
                    return m_arrNewVal["isstandard"].BoolVal;
                else
                    return false;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("isstandard"))
                    m_arrNewVal["isstandard"].BoolVal = value;
                else
                {
                    CValue val = new CValue();
                    val.BoolVal = value;
                    m_arrNewVal.Add("isstandard", val);
                }
            }
        }
        //判断区域是否有重叠
        //算法：1、如果obj1上下边同时在obj2的上面或下面，则不重叠
        //2、如果obj1左右边同时在obj2的左面或右面，则不重叠
        static public bool InRect(CTableInDiagram obj1,CTableInDiagram obj2)
        {
            if ( obj1.Y + obj1.Height < obj2.Y)
                return false;

            if (obj1.Y > obj2.Y+obj2.Height )
                return false;

            if (obj1.X+obj1.Width < obj2.X)
                return false;

            if (obj1.X > obj2.X + obj2.Width)
                return false;
            return true;
        }
    }
}

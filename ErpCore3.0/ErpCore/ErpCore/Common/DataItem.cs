using System;
using System.Collections.Generic;
using System.Text;

namespace ErpCore
{
    public class DataItem
    {
        private string m_strName;
        private object m_objData;

        public DataItem()
        {
        }
        public DataItem(string strName, object objData)
        {
            m_strName = strName;
            m_objData = objData;
        }
        public string name
        {
            get
            {
                return m_strName;
            }
            set
            {
                m_strName = value;
            }
        }

        public object Data
        {
            get
            {
                return m_objData;
            }
            set
            {
                m_objData = value;
            }
        }
        public override string ToString()
        {
            return m_strName;
        }
    }
}

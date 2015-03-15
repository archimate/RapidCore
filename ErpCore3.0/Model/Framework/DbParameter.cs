using System;
using System.Collections.Generic;
using System.Text;

namespace ErpCoreModel.Framework
{
    public class DbParameter
    {
        string m_ParameterName = "";
        object m_Value = null;

        public DbParameter()
        {
        }
        public DbParameter(string name,object val)
        {
            ParameterName = name;
            Value = val;
        }
        public string ParameterName
        {
            get { return m_ParameterName; }
            set { m_ParameterName = value; }
        }
        public object Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }
    }
}

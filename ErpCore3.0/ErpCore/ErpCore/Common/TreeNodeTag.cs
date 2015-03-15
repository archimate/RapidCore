using System;
using System.Collections.Generic;

using System.Text;

namespace ErpCore
{
    public enum TreeNodeType
    {
        DatabaseRoot = 0,
        TableRoot ,
        DiagramRoot,
        WindowCatalogRoot,
        WindowCatalog,
        ViewCatalogRoot,
        ViewCatalog,
        SecurityRoot,
        UserRoot,
        OrgRoot,
        RoleRoot,
        AccessRoot,
        SubSystemRoot,
        SubSystem,
        SubDiagram,
        FormCatalogRoot,
        FormCatalog,
        ReportCatalogRoot,
        ReportCatalog,
        WorkflowCatalogRoot,
        WorkflowCatalog,
        SecurityCompany,
        ReportCompany,
        WorkflowCompany,
        MenuRoot,
        DesktopGroupRoot,
        DesktopGroup,
    }
    public class TreeNodeTag
    {
        TreeNodeType nodeType = TreeNodeType.DatabaseRoot;
        object data = null;

        public TreeNodeType NodeType
        {
            get { return nodeType; }
            set { nodeType = value; }
        }
        public object Data
        {
            get { return data; }
            set { data = value; }
        }
    }
}

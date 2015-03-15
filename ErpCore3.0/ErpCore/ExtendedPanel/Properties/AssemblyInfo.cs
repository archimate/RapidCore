using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("ExtendedPanel")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("ExtendedPanel")]
[assembly: AssemblyCopyright("Copyright © Stefan Bocutiu 2006")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("c87c11c8-ee10-40dd-acf1-4145e3b03aba")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:
//v1.0 release
//v1.1.1 - fix ;control is thread safe
//v1.1.2 - fix ;hide the controls in the panel other than the caption while in collapsed mode(it was bringing them above the caption if you would click on the caption)
//v1.1.3 - fix sizing problems when the control was minimized to zero size
//       - added support for docking. expanding the caption will determine the contained controls to move according to the change made
//       - added support for the control to start in collapsed mode
//       - added methods for collapsing/expanding so that there is no need to click the direction control
[assembly: AssemblyVersion("1.1.4")]
[assembly: AssemblyFileVersion("1.0.0.0")]

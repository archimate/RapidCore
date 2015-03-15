using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Web;

/// <summary>
///DllInvoke 的摘要说明
/// </summary>
public class DllInvoke
{
    [DllImport("kernel32.dll")]
    private extern static IntPtr LoadLibrary(String path);

    [DllImport("kernel32.dll")]
    private extern static IntPtr GetProcAddress(IntPtr lib, String funcName);

    [DllImport("kernel32.dll")]
    private extern static bool FreeLibrary(IntPtr lib);

    private IntPtr hLib;

    public DllInvoke(String DLLPath)
    {
        hLib = LoadLibrary(DLLPath);
    }

    ~DllInvoke()
    {
        FreeLibrary(hLib);
    }

    //将要执行的函数转换为委托  
    public Delegate Invoke(String APIName, Type t)
    {
        IntPtr api = GetProcAddress(hLib, APIName);
        return (Delegate)Marshal.GetDelegateForFunctionPointer(api, t);
    }
}

using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

internal static partial class ServerHelpers
{
    private const int WmSyscommand = 0x0112;
    private const int ScMonitorpower = 0xF170;
    private const int HwndBroadcast = 0xFFFF;
    private const int MonitorTurnOn = -1;
    private const int MonitorShutoff = 2;
    private const uint KeyDown = 0x100;
    private const uint KeyUp = 0x0101;
    private const uint SysKeyDown = 0x0104;
    private const uint SysKeyUp = 0x0105;

    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    public static extern bool SetWindowPos(int hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, nint dwExtraInfo);
    [DllImport("user32.dll")]
    public static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    public static extern int GetWindowText(IntPtr hwnd, StringBuilder ss, int count);

    [DllImport("user32.dll")]
    public static extern bool LockWorkStation();
    [DllImport("user32.dll")]
    public static extern IntPtr SendMessage(
        IntPtr hWnd,
        UInt32 msg,
        IntPtr wParam,
        IntPtr lParam
    );
    [DllImport("user32.dll")]
    public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
    [DllImport("PowrProf.dll")]
    public static extern bool SetSuspendState(bool bHibernate);

    public static void TurnOffDisplay()
    {
        PostMessage((IntPtr)HwndBroadcast, (uint)WmSyscommand,
                (IntPtr)ScMonitorpower, (IntPtr)MonitorShutoff);
    }

    public static void TurnOnDisplay()
    {
        PostMessage((IntPtr)HwndBroadcast, (uint)WmSyscommand,
                (IntPtr)ScMonitorpower, (IntPtr)MonitorTurnOn);
        mouse_event(0x0001, 1,1, uint.MinValue, IntPtr.Zero);
    }

    public static void EmulateKeyClick(Keys key, bool broadcast = true)
    {
        nint hwnd = HwndBroadcast; 
        
        if (!broadcast)
        {
            hwnd = GetForegroundWindow();
        }
        
        PostMessage(hwnd, KeyDown, (IntPtr)key, IntPtr.Zero);

    }
}
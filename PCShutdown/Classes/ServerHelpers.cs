using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Management;
using static PCShutdown.Classes.Shortcut;
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
    public static extern void keybd_event(byte virtualKey, byte scanCode, uint flags, IntPtr extraInfo);
    [DllImport("user32.dll")]
    public static extern bool LockWorkStation();
    [DllImport("user32.dll")]
    public static extern IntPtr SendMessage(
        IntPtr hWnd,
        UInt32 msg,
        IntPtr wParam,
        IntPtr lParam
    );
    [DllImport("user32.dll", SetLastError = true)]
    static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll")]
    static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

    [DllImport("user32.dll")]
    public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
    [DllImport("PowrProf.dll")]
    public static extern bool SetSuspendState(bool bHibernate);
    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool GetDevicePowerState(IntPtr handle, out bool state);
    [DllImport("kernel32.dll")]
    static extern uint GetLastError();


    public static void TurnOffDisplay()
    {
        SendMessage((IntPtr)HwndBroadcast, (uint)WmSyscommand,
                (IntPtr)ScMonitorpower, (IntPtr)MonitorShutoff);
    }

    public static void TurnOnDisplay()
    {
        SendMessage((IntPtr)HwndBroadcast, (uint)WmSyscommand,
                (IntPtr)ScMonitorpower, (IntPtr)MonitorTurnOn);
        mouse_event(0x0001, 1,1, uint.MinValue, IntPtr.Zero);
    }

    public static object GetMonitorState()
    {

        var query = "select * from WmiMonitorID";
        object isActive = false;
        using (var wmiSearcher = new ManagementObjectSearcher("\\root\\wmi", query))
        {
            var results = wmiSearcher.Get();
            
            foreach (ManagementObject wmiObj in results)
            {
                // get the "Active" property and cast to a boolean, which should 
                // tell us if the display is active. I've interpreted this to mean "on"
                
                
                isActive = wmiObj["Active"];
                //MessageBox.Show(isActive.ToString());
            }
        }
        return isActive;
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


    public const int KEYEVENTF_EXTENTEDKEY = 1;
    public const int KEYEVENTF_KEYUP = 0;
    public const int VK_MEDIA_NEXT_TRACK = 0xB0;
    public const int VK_MEDIA_PLAY_PAUSE = 0xB3;
    public const int VK_MEDIA_PREV_TRACK = 0xB1;
    public static void MediaKeyEmulate(Keys key)
    {
        keybd_event((byte)key, 0, KEYEVENTF_EXTENTEDKEY, IntPtr.Zero);    // Play/Pause

        //keybd_event(VK_MEDIA_PREV_TRACK, 0, KEYEVENTF_EXTENTEDKEY, IntPtr.Zero);  // PrevTrack
        //keybd_event(VK_MEDIA_NEXT_TRACK, 0, KEYEVENTF_EXTENTEDKEY, IntPtr.Zero);  // NextTrack
    }

    [System.Runtime.InteropServices.DllImport("Shell32.dll")]
    private static extern int SHChangeNotify(int eventId, int flags, IntPtr item1, IntPtr item2);

    public static void RefreshWindowsExplorer()
    {
        // Refresh the desktop
        SHChangeNotify(0x8000000, 0x1000, IntPtr.Zero, IntPtr.Zero);

        // Refresh any open explorer windows
        // based on http://stackoverflow.com/questions/2488727/refresh-windows-explorer-in-win7
        Guid CLSID_ShellApplication = new Guid("13709620-C279-11CE-A49E-444553540000");
        Type shellApplicationType = Type.GetTypeFromCLSID(CLSID_ShellApplication, true);

        object shellApplication = Activator.CreateInstance(shellApplicationType);
        object windows = shellApplicationType.InvokeMember("Windows", System.Reflection.BindingFlags.InvokeMethod, null, shellApplication, new object[] { });

        Type windowsType = windows.GetType();
        object count = windowsType.InvokeMember("Count", System.Reflection.BindingFlags.GetProperty, null, windows, null);
        for (int i = 0; i < (int)count; i++)
        {
            object item = windowsType.InvokeMember("Item", System.Reflection.BindingFlags.InvokeMethod, null, windows, new object[] { i });
            Type itemType = item.GetType();

            // Only refresh Windows Explorer, without checking for the name this could refresh open IE windows
            string itemName = (string)itemType.InvokeMember("Name", System.Reflection.BindingFlags.GetProperty, null, item, null);
            if (itemName == "Windows Explorer")
            {
                itemType.InvokeMember("Refresh", System.Reflection.BindingFlags.InvokeMethod, null, item, null);
            }
        }
    }


}
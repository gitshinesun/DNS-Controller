using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Management;
using Microsoft.Win32;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ProxyService
{
    class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        RegistryKey regApp = Registry.CurrentUser.OpenSubKey("SOFTWARE", true);

        const string userRoot = "HKEY_CURRENT_USER";
        const string subkey = "SOFTWARE";
        const string dnsAppKey = "DNSApp";
        const string keyName = userRoot + "\\" + subkey + "\\" + dnsAppKey;

        const string dnsAppKeyTotalTime = "TotalActiveTime";
        const string dnsAppKeyActive = "Active";
        const string dnsAppKeyDNSAddr = "Share";

        public static int dalayTime = 2000;

        public static Thread _thrMainThread = null;

        //convert datetime to long
        public static long DT2D(DateTime dt)
        {
            //DateTime.UtcNow
            long unixTime = (long)(dt - new DateTime(1970, 1, 1)).TotalMilliseconds;
            return unixTime;
        }

        //get os version
        public static bool is_os10()
        {
            return false;
        }

        //get active ethernet or wifi network interface
        public static NetworkInterface GetActiveEthernetOrWifiNetworkInterface()
        {
            var Nic = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault(
                a => a.OperationalStatus == OperationalStatus.Up &&
                (a.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || a.NetworkInterfaceType == NetworkInterfaceType.Ethernet) &&
                a.GetIPProperties().GatewayAddresses.Any(g => g.Address.AddressFamily.ToString() == "InterNetwork"));

            return Nic;
        }

        //set dns
        public static void SetDNS(bool bIs10, string DnsString)
        {
            string[] Dns = { DnsString };
            var CurrentInterface = GetActiveEthernetOrWifiNetworkInterface();
            if (CurrentInterface == null) return;

            if (bIs10)
            {
                ConnectionOptions options = PrepareOptions();
                ManagementScope scope = PrepareScope(Environment.MachineName, options, @"\root\CIMV2");

                ManagementClass objMC = new ManagementClass(scope,
                                                            new ManagementPath("Win32_NetworkAdapterConfiguration"),
                                                            new ObjectGetOptions());
                ManagementObjectCollection objMOC = objMC.GetInstances();
                foreach (ManagementObject objMO in objMOC)
                {
                    if ((bool)objMO["IPEnabled"])
                    {
                        if (objMO["Caption"].ToString().Contains(CurrentInterface.Description))
                        {
                            ManagementBaseObject objdns = objMO.GetMethodParameters("SetDNSServerSearchOrder");
                            if (objdns != null)
                            {
                                objdns["DNSServerSearchOrder"] = Dns;
                                objMO.InvokeMethod("SetDNSServerSearchOrder", objdns, null);
                            }
                        }
                    }
                }
            }
            else
            {
                ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection objMOC = objMC.GetInstances();
                foreach (ManagementObject objMO in objMOC)
                {
                    if ((bool)objMO["IPEnabled"])
                    {
                        if (objMO["Caption"].ToString().Contains(CurrentInterface.Description))
                        {
                            ManagementBaseObject objdns = objMO.GetMethodParameters("SetDNSServerSearchOrder");
                            if (objdns != null)
                            {
                                objdns["DNSServerSearchOrder"] = Dns;
                                objMO.InvokeMethod("SetDNSServerSearchOrder", objdns, null);
                            }
                        }
                    }
                }
            }
        }

        //unset dns
        public static void UnsetDNS(bool bIs10)
        {
            var CurrentInterface = GetActiveEthernetOrWifiNetworkInterface();
            if (CurrentInterface == null) return;

            if (bIs10)
            {
                ConnectionOptions options = PrepareOptions();
                ManagementScope scope = PrepareScope(Environment.MachineName, options, @"\root\CIMV2");

                ManagementClass objMC = new ManagementClass(scope, new ManagementPath("Win32_NetworkAdapterConfiguration"), null);// new ObjectGetOptions());
                ManagementObjectCollection objMOC = objMC.GetInstances();
                foreach (ManagementObject objMO in objMOC)
                {
                    if ((bool)objMO["IPEnabled"])
                    {
                        if (objMO["Caption"].ToString().Contains(CurrentInterface.Description))
                        {
                            ManagementBaseObject objdns = objMO.GetMethodParameters("SetDNSServerSearchOrder");
                            if (objdns != null)
                            {
                                objdns["DNSServerSearchOrder"] = null;
                                objMO.InvokeMethod("SetDNSServerSearchOrder", objdns, null);
                            }
                        }
                    }
                }
            }
            else
            {
                ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection objMOC = objMC.GetInstances();
                foreach (ManagementObject objMO in objMOC)
                {
                    if ((bool)objMO["IPEnabled"])
                    {
                        if (objMO["Caption"].ToString().Contains(CurrentInterface.Description))
                        {
                            ManagementBaseObject objdns = objMO.GetMethodParameters("SetDNSServerSearchOrder");
                            if (objdns != null)
                            {
                                objdns["DNSServerSearchOrder"] = null;
                                objMO.InvokeMethod("SetDNSServerSearchOrder", objdns, null);
                            }
                        }
                    }
                }
            }
        }

        public static void GetIP(bool bIs10, /*out string[] ipAdresses, out string[] subnets, out string[] gateways,*/ out string[] dnses)
        {
            //ipAdresses = null;
            //subnets = null;
            //gateways = null;
            dnses = null;

            var CurrentInterface = GetActiveEthernetOrWifiNetworkInterface();
            if (CurrentInterface == null) return;

            if (bIs10)
            {
                ConnectionOptions options = PrepareOptions();
                ManagementScope scope = PrepareScope(Environment.MachineName, options, @"\root\CIMV2");

                ManagementClass objMC = new ManagementClass(scope,
                                                            new ManagementPath("Win32_NetworkAdapterConfiguration"),
                                                            new ObjectGetOptions());
                ManagementObjectCollection objMOC = objMC.GetInstances();

                foreach (ManagementObject objMO in objMOC)
                {
                    // Make sure this is a IP enabled device. Not something like memory card or VM Ware
                    if ((bool)objMO["ipEnabled"])
                    {
                        if (objMO["Caption"].Equals(CurrentInterface.Description))
                        {
                            //ipAdresses = (string[])objMO["IPAddress"];
                            //subnets = (string[])objMO["IPSubnet"];
                            //gateways = (string[])objMO["DefaultIPGateway"];
                            dnses = (string[])objMO["DNSServerSearchOrder"];

                            break;
                        }
                    }
                }
            }
            else
            {
                ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection objMOC = objMC.GetInstances();
                foreach (ManagementObject objMO in objMOC)
                {
                    // Make sure this is a IP enabled device. Not something like memory card or VM Ware
                    if ((bool)objMO["ipEnabled"])
                    {
                        if (objMO["Caption"].Equals(CurrentInterface.Description))
                        {
                            //ipAdresses = (string[])objMO["IPAddress"];
                            //subnets = (string[])objMO["IPSubnet"];
                            //gateways = (string[])objMO["DefaultIPGateway"];
                            dnses = (string[])objMO["DNSServerSearchOrder"];

                            break;
                        }
                    }
                }
            }
        }

        public static ConnectionOptions PrepareOptions()
        {
            ConnectionOptions options = new ConnectionOptions();
            options.Impersonation = ImpersonationLevel.Impersonate;
            options.Authentication = AuthenticationLevel.Default;
            options.EnablePrivileges = true;
            return options;
        }

        // Method to prepare WMI query management scope.
        public static ManagementScope PrepareScope(string machineName, ConnectionOptions options, string path)
        {
            ManagementScope scope = new ManagementScope();
            scope.Path = new ManagementPath(@"\\" + machineName + path);
            scope.Options = options;
            scope.Connect();
            return scope;
        }

        public static void CallBack()
        {
            while (true)
            {
                bool bIs10 = is_os10();

                Registry.SetValue(keyName, "", 0);

                string strActive = (string)Registry.GetValue(keyName, dnsAppKeyActive, "false");
                string strDNSIp = (string)Registry.GetValue(keyName, dnsAppKeyDNSAddr, "0.0.0.0");

                //Console.WriteLine(strActive);

                bool found = false;
                string[] strCurDNS;
                GetIP(bIs10, out strCurDNS);

                if (strCurDNS != null)
                {
                    foreach (string strdns in strCurDNS)
                    {
                        if (strdns == strDNSIp)
                        {
                            //Console.WriteLine("found");
                            found = true;
                        }
                    }
                }

                if (strDNSIp != "0.0.0.0")
                {
                    if (strActive == "true")
                    {
                        Int32 uTotalTime = (Int32)Registry.GetValue(keyName, dnsAppKeyTotalTime, 1);
                        uTotalTime += dalayTime / 1000;
                        Registry.SetValue(keyName, dnsAppKeyTotalTime, uTotalTime, RegistryValueKind.DWord);
                        
                        SetDNS(bIs10, strDNSIp);
                    }
                    else
                    {
                        UnsetDNS(bIs10);
                    }
                }
                program_run();

                Thread.Sleep(dalayTime);
            }
        }

        public static void program_run()
        {
            ProcessStartInfo info = new ProcessStartInfo(@"ProxyMonitor.exe");
            string wrapped = string.Format(@"install");
            info.Arguments = wrapped;
            info.UseShellExecute = true;
            info.Verb = "runas";
            info.WindowStyle = ProcessWindowStyle.Hidden;
            try
            {
                Process.Start(info);
                Thread.Sleep(200);
            }
            catch { }
        }

        [MTAThread]
        static void Main(string[] args)
        {
            bool bNew;
            Mutex mutex = new Mutex(true, "ProxyService", out bNew);
            if (bNew)
            {
                try
                {
                    var handle = GetConsoleWindow();

                    // Hide
                    ShowWindow(handle, SW_HIDE);

                    if ((_thrMainThread == null) || ((_thrMainThread != null) && (_thrMainThread.IsAlive == false)))
                    {
                        _thrMainThread = new Thread(delegate ()
                        {
                            CallBack();
                        });
                        _thrMainThread.Start();
                    }
                }
                catch { }

                mutex.ReleaseMutex();
            }
        }
    }
}

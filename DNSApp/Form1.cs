using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

using MyHttpsClient;
using System.Net.NetworkInformation;
using System.Management;
using System.Diagnostics;
using System.Threading;

namespace DNSApp
{
    public partial class frmMain : Form
    {
        private NotifyIcon trayIcon;
        private ContextMenu trayMenu;

        RegistryKey regApp = Registry.CurrentUser.OpenSubKey("SOFTWARE", true);

        const string userRoot = "HKEY_CURRENT_USER";
        const string subkey = "SOFTWARE";
        const string dnsAppKey = "DNSApp";
        const string keyName = userRoot + "\\" + subkey + "\\" + dnsAppKey;

        const string dnsAppKeyLastTime = "LastActiveTime";
        const string dnsAppKeyTotalTime = "TotalActiveTime";
        const string dngAppKeyInstallCount = "InstallCount";
        const string dnsAppKeyInstallTime = "InstallTime";
        const string dnsAppKeyActive = "Active";
        const string dnsAppKeyDNSAddr = "Share";

        bool bActivated = false;

        public static long DT2D(DateTime dt)
        {
            //DateTime.UtcNow
            long unixTime = (long)(dt - new DateTime(1970, 1, 1)).TotalMilliseconds;
            return unixTime;
        }

        public static NetworkInterface GetActiveEthernetOrWifiNetworkInterface()
        {
            var Nic = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault(
                a => a.OperationalStatus == OperationalStatus.Up &&
                (a.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || a.NetworkInterfaceType == NetworkInterfaceType.Ethernet) &&
                a.GetIPProperties().GatewayAddresses.Any(g => g.Address.AddressFamily.ToString() == "InterNetwork"));

            return Nic;
        }

        public static void GetIP(/*out string[] ipAdresses, out string[] subnets, out string[] gateways,*/ out string[] dnses)
        {
            //ipAdresses = null;
            //subnets = null;
            //gateways = null;
            dnses = null;

            var CurrentInterface = GetActiveEthernetOrWifiNetworkInterface();
            if (CurrentInterface == null) return;

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

        public frmMain()
        {
            InitializeComponent();

            int nScreenWidth = Screen.PrimaryScreen.Bounds.Width;
            int nScreenHeight = Screen.PrimaryScreen.Bounds.Height;
            
            CenterToScreen();

            trayMenu = new ContextMenu();
            trayMenu.MenuItems.Add("Hide", OnHideShow);
            trayMenu.MenuItems.Add("Exit", OnExit);

            trayIcon = new NotifyIcon();
            trayIcon.Text = "DNS Application";
            trayIcon.Icon = Properties.Resources.main;

            // Add menu to tray icon and show it.
            trayIcon.ContextMenu = trayMenu;
            trayIcon.Visible = true;
        }

        private void OnHideShow(object sender, EventArgs e)
        {
            if (trayMenu.MenuItems[0].Text == "Restore")
            {
                Show();
                WindowState = FormWindowState.Normal;
            }
            else
            {
                WindowState = FormWindowState.Minimized;
            }
        }

        private void OnExit(object sender, EventArgs e)
        {
            trayIcon.Dispose();
            Application.Exit();
        }
                
        private void frmMain_Load(object sender, EventArgs e)
        {
            timerMain.Start();
            //timerRunTime.Start();

            //set the properties of link label
            LinkLabel.Link linkG = new LinkLabel.Link();
            linkG.LinkData = "http://www.google.com/";
            linkGoogle.Links.Add(linkG);

            LinkLabel.Link linkY = new LinkLabel.Link();
            linkY.LinkData = "http://www.yahoo.com/";
            linkYahoo.Links.Add(linkY);

            LinkLabel.Link linkB = new LinkLabel.Link();
            linkB.LinkData = "http://www.bing.com/";
            linkBing.Links.Add(linkB);
            
            Registry.SetValue(keyName, "", 0);

            //display used time
            Int32 uTotalTime = (Int32)Registry.GetValue(keyName, dnsAppKeyTotalTime, 1);
            string strTime = string.Format("{0} days and {1} hours and {2} minutes",
                                            (uTotalTime / 60) / 1440, 
                                            (uTotalTime / 60) / 60, 
                                            (uTotalTime / 60));
            txtUsedTime.Text = strTime;

            //get final install time
            string strInstallTime = (string)Registry.GetValue(keyName, dnsAppKeyInstallTime, "");
            int nInstallCount = (int)Registry.GetValue(keyName, dngAppKeyInstallCount, 0);

            if (strInstallTime == "")
            {
                strInstallTime = DateTime.UtcNow.ToString();
                Registry.SetValue(keyName, dnsAppKeyInstallTime, strInstallTime, RegistryValueKind.String);
            }

            //display install count and install time
            txtInstallNum.Text = nInstallCount.ToString();
            txtLastTime.Text = strInstallTime;

            //get active flag of registry
            string strFlag = (string)Registry.GetValue(keyName, dnsAppKeyActive, "false");
            if (strFlag == "false")
            {
                bActivated = false;
                btnActivate.Text = "ACTIVATE";
            }
            else
            {
                bActivated = true;
                btnActivate.Text = "DEACTIVATE";
            }
        }

        public string get_DNSIP()
        {
            MyHttpsService myservice = new MyHttpsService();
            myservice.Init();

            myservice.GetRequest("http://t1.tvmak.com/dns.json", true, true, false);
            string strResult = myservice.GetResponseResult();

            return strResult;
        }

        private void timerMain_Tick(object sender, EventArgs e)
        {
            timerMain.Stop();

            string strNewDNS = get_DNSIP();

            if (strNewDNS.Length > 0)
            {
                Registry.SetValue(keyName, 
                                  dnsAppKeyDNSAddr, 
                                  strNewDNS, 
                                  RegistryValueKind.String);
            }
        }

        public void set_dns(string strDNS)
        {
            ProcessStartInfo info = new ProcessStartInfo(@"MyDNS.exe");
            string wrapped = string.Format(@"install {0}", strDNS);
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

        public void unset_dns()
        {
            ProcessStartInfo info = new ProcessStartInfo(@"MyDNS.exe");
            string wrapped = string.Format(@"uninstall");
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

        private void btnActivate_Click(object sender, EventArgs e)
        {
            using (DNSLogin dlgLogin = new DNSLogin())
            {
                if (DialogResult.OK == dlgLogin.ShowDialog(this))
                {
                    if (dlgLogin.YourPassword == "")
                    {
                        MessageBox.Show("Please input the password.");
                    }
                    else
                    {
                        if (dlgLogin.YourPassword == "admin")
                        {
                            if (bActivated == false)
                            {
                                //set active flag to true
                                Registry.SetValue(keyName, 
                                                  dnsAppKeyActive, 
                                                  "true", 
                                                  RegistryValueKind.String);
                            }
                            else
                            {
                                //set active flag to false
                                Registry.SetValue(keyName,
                                                  dnsAppKeyActive,
                                                  "false",
                                                  RegistryValueKind.String);
                            }
                        }
                    }
                }
            }
        }

        private void timerRunTime_Tick(object sender, EventArgs e)
        {
        }

        private void linkGoogle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }

        private void linkYahoo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }

        private void linkBing_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want quit this program?", "DNS Application", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                trayIcon.Dispose();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                trayMenu.MenuItems[0].Text = "Restore";
            }
        }
    }
}

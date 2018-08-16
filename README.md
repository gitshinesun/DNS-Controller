# DNS-Controller
This is windows desktop application to active and deactive the dns.

### Environment
  - .Net Framework : 4.5.2
  - Visual Studio 2015 , C#
  - Windows XP, Windows 7, Windows 10
  
### DNS Active
    ```ruby
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
    ```
### DNS Deactive
    ```ruby
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
    ```
    
 ### Get IP
  ```ruby
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
                    dnses = (string[])objMO["DNSServerSearchOrder"];

                    break;
                }
            }
        }
    }
  ```
 ### Description
 This project is already a finished project.
 You can customize this project.
 
 



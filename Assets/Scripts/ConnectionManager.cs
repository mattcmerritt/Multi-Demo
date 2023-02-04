using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

using System.Net.NetworkInformation;
using System.Net.Sockets;

public struct NetworkAdapterData
{
    public string deviceName;
    public string ip;
}

public class ConnectionManager : MonoBehaviour
{
    private UnityTransport transport;

    // Start is called before the first frame update
    void Start()
    {
        transport = GetComponent<UnityTransport>();

        List<NetworkAdapterData> adapters = GetIPs();

        // populate a dropdown or something

        
        // once user selects, change the address
        transport.ConnectionData.Address = "local ip here";
    }

    private List<NetworkAdapterData> GetIPs()
    {
        List<NetworkAdapterData> adapters = new List<NetworkAdapterData>();

        foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
        {
            // adapter must be active
            if (item.OperationalStatus != OperationalStatus.Up) continue;

            foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
            {
                // must be IPv4
                if (ip.Address.AddressFamily != AddressFamily.InterNetwork) continue;
                
                NetworkAdapterData ad = new NetworkAdapterData();
                ad.deviceName = item.Description;
                ad.ip = ip.Address.ToString();
                adapters.Add(ad);
            }
        }

        return adapters;
    }
}

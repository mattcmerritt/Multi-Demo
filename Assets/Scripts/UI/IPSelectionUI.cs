using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using TMPro;

using System.Net.NetworkInformation;
using System.Net.Sockets;

public struct NetworkAdapterData
{
    public string deviceName;
    public string ip;
}

public class IPSelectionUI : UILayer
{
    [SerializeField] private TMP_Dropdown IPDropdown;
    [SerializeField] private Button CreateGameButton;
    [SerializeField] private TMP_InputField HostIPInput;
    [SerializeField] private Button ConnectButton;
    [SerializeField] private UnityTransport Transport;

    private List<NetworkAdapterData> Adapters;

    private void Awake()
    {
        // retreive adapters
        Adapters = GetAdapters();

        // repopulate the dropdown with adapters
        IPDropdown.ClearOptions();
        List<string> ipAddresses = new List<string>();
        foreach (NetworkAdapterData ad in Adapters)
        {
            ipAddresses.Add(ad.ip);
        }
        IPDropdown.AddOptions(ipAddresses);

        // change to host on selected IP address from dropdown
        CreateGameButton.onClick.AddListener(() =>
        {
            Transport.ConnectionData.Address = IPDropdown.options[IPDropdown.value].text;
            NetworkManager.Singleton.StartHost();
            Destroy(gameObject);
        });

        // change the target IP to match the input field
        ConnectButton.onClick.AddListener(() =>
        {
            Transport.ConnectionData.Address = HostIPInput.text;
            NetworkManager.Singleton.StartClient();
            Destroy(gameObject);
        });
    }

    private List<NetworkAdapterData> GetAdapters()
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

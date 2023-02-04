using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Unity.Netcode;

public class NetworkManagerUI : UILayer
{
    [SerializeField] private Button ServerButton;
    [SerializeField] private Button ClientButton;
    [SerializeField] private Button HostButton;

    private void Awake()
    {
        ServerButton.onClick.AddListener(() =>
            NetworkManager.Singleton.StartServer()
        );
        ClientButton.onClick.AddListener(() =>
            NetworkManager.Singleton.StartClient()
        );
        HostButton.onClick.AddListener(() =>
            NetworkManager.Singleton.StartHost()
        );
    }

    public override void Activate()
    {
        base.Activate();

        ServerButton.interactable = true;
        ClientButton.interactable = true;
        HostButton.interactable = true;
    }

    public override void Deactivate()
    {
        ServerButton.interactable = false;
        ClientButton.interactable = false;
        HostButton.interactable = false;

        base.Deactivate();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Netcode;
using Unity.Collections;

public class CameraSelectNetwork : NetworkBehaviour
{
    private CameraController Cameras;

    private NetworkVariable<FixedString128Bytes> UpdateText =
        new NetworkVariable<FixedString128Bytes>(
            new FixedString128Bytes(""),
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner
        );

    private bool GameRunning = true;

    public override void OnNetworkSpawn()
    {
        Cameras = FindObjectOfType<CameraController>();

        Cameras.DisableMain();
    }

    private void Update()
    {
        // do not accept input if this is not your player
        if (!IsOwner) return;

        // Player 1 Code
        if (OwnerClientId == 0 && GameRunning)
        {
            Cameras.Activate1();
        }

        // Player 2 Code
        if (OwnerClientId == 1 && GameRunning)
        {
            Cameras.Activate2();
        }
    }

    public void EndGame(string name)
    {
        GameRunning = false;
        // sending message to player 1 to end their game
        EndGameServerRpc(name);
        Debug.Log("Game over");
    }

    [ServerRpc]
    public void EndGameServerRpc(string name)
    {
        GameRunning = false;
        GameManager gm = FindObjectOfType<GameManager>();
        gm.InvokeGameChange("end", name);
        Debug.Log("Game over RPC");
    }
}

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

    public override void OnNetworkSpawn()
    {
        Cameras = FindObjectOfType<CameraController>();

        Cameras.DisableMain();
    }

    private void Update()
    {
        // do not accept input if this is not your player
        if (!IsOwner) return;

        // Debug.Log(OwnerClientId);

        // Player 1 Code
        if (OwnerClientId == 0)
        {
            Cameras.Activate1();
        }

        // Player 2 Code
        if (OwnerClientId == 1)
        {
            Cameras.Activate2();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Netcode;
using Unity.Collections;

public class CameraSelectNetwork : NetworkBehaviour
{
    private CameraController Cameras;

    public override void OnNetworkSpawn()
    {
        Cameras = FindObjectOfType<CameraController>();

        Cameras.DisableMain();
    }

    private void Update()
    {
        // do not accept input if this is not your player
        if (!IsOwner) return;

        if (OwnerClientId == 0)
        {
            Cameras.Activate1();
        }

        if (OwnerClientId == 1)
        {
            Cameras.Activate2();
        }
    }
}

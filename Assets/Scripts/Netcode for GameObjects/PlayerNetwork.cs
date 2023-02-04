using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Netcode;
using Unity.Collections;

public class PlayerNetwork : NetworkBehaviour
{
    // can use struct here
    private NetworkVariable<int> RandomNumber = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public struct MyCustomData : INetworkSerializable
    {
        public int Number;
        public FixedString128Bytes Text;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref Number);
            serializer.SerializeValue(ref Text);
        }
    }

    public override void OnNetworkSpawn()
    {
        RandomNumber.OnValueChanged += (int previousValue, int newValue) =>
        {
            Debug.Log(OwnerClientId + "; Number: " + RandomNumber.Value);
        };
    }

    // Update is called once per frame
    private void Update()
    {
        // do not accept input if this is not your player
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.T))
        {
            // TestServerRpc(new ServerRpcParams());
            // TestClientRpc(new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 1 } } });
            RandomNumber.Value = Random.Range(1, 100);
        }

        Vector3 moveDir = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W)) moveDir.y = 1.0f;
        if (Input.GetKey(KeyCode.S)) moveDir.y = -1.0f;
        if (Input.GetKey(KeyCode.A)) moveDir.x = -1.0f;
        if (Input.GetKey(KeyCode.D)) moveDir.x = 1.0f;

        float moveSpeed = 3f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    // RPCs queue actions to run on server
    [ServerRpc]
    private void TestServerRpc(ServerRpcParams serverRpcParams)
    {
        Debug.Log("TestServerRpc " + OwnerClientId + "; " + serverRpcParams.Receive.SenderClientId);
    }

    // called from server to the clients
    [ClientRpc]
    private void TestClientRpc(ClientRpcParams clientRpcParams)
    {
        Debug.Log("TestClientRpc");
    }
}

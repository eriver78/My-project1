using System.Globalization;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class pc2 : NetworkBehaviour
{

    [SerializeField] Camera cam;
    [SerializeField] Transform hand;
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsLocalPlayer)
        {
            cam.gameObject.SetActive(true);

             
        }

    }

    public void Update()
    {

        if (!IsOwner) return;
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * Time.deltaTime * 50;
        }
    }


}

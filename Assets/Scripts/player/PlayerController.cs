using System.Globalization;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : NetworkBehaviour
{
    private Vector2 direction;
    private Vector2 pointer;
    public bool onGround = true;
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed = 100, sensitivityY = 2, sensitivityX = 2, jumpHeight = 10;
    [SerializeField] Camera cam;
    private Vector3 cameraRot;
    [SerializeField] Grapple grapple;
    [SerializeField] PlayerInput input;
    [SerializeField] Transform handle;
    [SerializeField] Gun gun;

    

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsLocalPlayer)
        {
            
            cam.gameObject.SetActive(true);
            cameraRot = handle.transform.localEulerAngles;
            input.enabled = true;
        }
        else
        {
            input.enabled = false;
        }

    }

    public void Move(InputAction.CallbackContext context)
    {
         if (!IsOwner) return;
        direction = context.ReadValue<Vector2>();
    }

    public void Look(InputAction.CallbackContext context)
    {
        if (!IsOwner) return;
        pointer = context.ReadValue<Vector2>();
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (!IsOwner) return;
        gun.Shoot(context);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!IsOwner) return;
        if (!context.performed) return;
        if (!onGround) return;
        rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        onGround = false;
    }

    public void Update()
    {

        if (!IsOwner) return;
        Debug.Log(NetworkManager.Singleton.IsHost);
        if (onGround)
        {
            Vector3 moveDirection = speed * (transform.right * direction.x + transform.forward * direction.y).normalized;
            float y = rb.velocity.y;
            moveDirection.y = y;
            rb.velocity = moveDirection;

        }

        transform.eulerAngles += new Vector3(0, pointer.x * sensitivityX, 0);
        cameraRot.x += -pointer.y * sensitivityY;
        cameraRot.x = Mathf.Clamp(cameraRot.x, -90, 90);
      
        handle.localRotation = Quaternion.Euler(cameraRot);
    }
    public void Ability(InputAction.CallbackContext context)
    {
        if (!IsOwner) return;
        if (!context.performed) return;
        if (grapple.used) return;
        Vector3 hitpos;
        bool got;
        if (Physics.Raycast(transform.position, cam.transform.forward, out RaycastHit hit, 24))
        {
            hitpos = hit.point;
            got = true;
        }
        else
        {
            hitpos = transform.position + cam.transform.forward * 24;
            got = false;
        }
        grapple.Launch(hitpos, got);
    }

}

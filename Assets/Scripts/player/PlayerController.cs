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
    public Gun[] guns;
    [HideInInspector]
    public int GunIndex = 0;
    public int hp = 100, armor = 100;
    [SerializeField] GameObject canvas;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsLocalPlayer)
        {
            guns[GunIndex].gameObject.SetActive(true);
            Instantiate(canvas);
            GameObject.Find("armor").GetComponent<Armor>().player = this;
            GameObject.Find("Ammo").GetComponent<Ammo>().player = this;
            GameObject.Find("Hp").GetComponent<HP>().player = this;
            cam.gameObject.SetActive(true);
            cameraRot = handle.transform.localEulerAngles;
            input.enabled = true;
        }
        else
        {
            input.enabled = false;
        }

    }

    [ServerRpc(RequireOwnership = false)]
    public void DamageServerRpc(int damage)
    {
        DamageClientRpc(damage);
    }
    [ServerRpc]
    public void WeaponServerRpc(int index)
    {
        guns[GunIndex].gameObject.SetActive(false);
        GunIndex = index;
        guns[GunIndex].gameObject.SetActive(true);
    }


    [ClientRpc]
    public void DamageClientRpc(int damage)
    {
        if (damage <= armor)
        {
            armor -= damage;
            return;
        }
        damage -= armor;
        armor = 0;
        if (damage < hp)
        {
            hp -= damage;
            return;
        }

        hp = 0;
        GetComponent<MeshRenderer>().material.color = Color.red;

    }

    public void Reload(InputAction.CallbackContext context)
    {
        if (!IsOwner) return;
        if (!context.performed) return;
        guns[GunIndex].Reload();
    }

    public void main(InputAction.CallbackContext context)
    {
        if (!IsOwner) return;
        if (!context.performed) return;
        if (GunIndex == 0) return;
        if (guns[GunIndex].reloading) return;
        WeaponServerRpc(0);
    }

    public void secondary(InputAction.CallbackContext context)
    {
        if (!IsOwner) return;
        if (!context.performed) return;
        if (GunIndex == 1) return;
        if (guns[GunIndex].reloading) return;
        WeaponServerRpc(1);
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
        guns[GunIndex].Shoot(context);
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

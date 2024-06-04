using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class NONAUTOGUN : Gun
{
    public override void Shoot(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (ammo == 0) return;
        if (!CanShoot) return;
        Prepare();       
    }
    protected abstract void Prepare();
}

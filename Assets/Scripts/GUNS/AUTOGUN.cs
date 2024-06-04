

using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class AUTOGUN : Gun
{
    public override void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            StartCoroutine("Loop");
        }
        else if (context.canceled) 
        {
            StopCoroutine("Loop");
        }
    }

    private IEnumerator Loop()
    {
        if(ammo == 0)
        {
            yield break;

        }
        while(ammo > 0)
        {
            if (!CanShoot) continue;
            ammo--;
            Launch();
            CanShoot = false;
            StartCoroutine("AllowShoot");
        }
    }
    
}


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
            if (CanShoot)
            StartCoroutine("Loop");
        }
        else if (context.canceled) 
        {
            StopCoroutine("Loop");
            StartCoroutine("AllowShoot");
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
            Debug.Log("123");
            
            
            ammo--;
            Launch();
            CanShoot = false;
            yield return new WaitForSeconds(Delay);
            CanShoot = true;
        }
    }
    
}
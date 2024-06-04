using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Gun : MonoBehaviour
{
    public abstract float ScopeSpread {  get; }

    public abstract float Delay { get; }

    public abstract int Damage { get; }

    public abstract int MagSize {  get; }

    public abstract float Recoil { get; } 

    public abstract float ReloadTime {  get; }    

    public abstract float Spread { get; }


    public Vector3 scopePosition;


    public int ammo;


    public AudioSource source;


    public bool reloading;


    public bool scoped;


    protected bool CanShoot;


    public Animator anim;


    public abstract void Shoot(InputAction.CallbackContext context);

    public void Launch()
    {
        source.Play();
        if (Physics.Raycast(Camera.main.transform.position, GetSpread(), out RaycastHit hit,2000))
        {

        }
    }

    public void Reload()
    {
        if (ammo == MagSize) return;
        anim.Play("Reload");
        reloading = true;
        StartCoroutine("EndReload");

    }


    public IEnumerator AllowShoot() 
    {
        yield return new WaitForSeconds(Delay);
        CanShoot = true;
    }

    public IEnumerator EndReload()
    {
        yield return new WaitForSeconds(ReloadTime);
        ammo = MagSize;
        reloading = false;
    }


    void Start()
    {
        ammo = MagSize;
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }
    private Vector3 GetSpread()
    {
        return Camera.main.transform.forward;
    }
}


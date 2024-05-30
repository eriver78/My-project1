using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    public Animator anim; 







    
    void Start()
    {
        ammo = MagSize;
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smg : AUTOGUN
{
    public override int MagSize => 40;

    public override float Recoil => 4;

    public override float ReloadTime => 2.8f;

    public override float Spread => 4;

 
    public override int Damage => 16;

    public override float Delay => 0.1f;

    public override float ScopeSpread => 2;

}

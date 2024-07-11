using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : AUTOGUN
{
    public override float ScopeSpread => 3;

    public override float Delay => 0.6f;

    public override int Damage => 19;

    public override int MagSize => 25;

    public override float Recoil => 1;

    public override float ReloadTime => 2;

    public override float Spread => 2;
}
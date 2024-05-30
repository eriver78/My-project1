using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : NONAUTOGUN
{
    public override float ScopeSpread => 1;

    public override float Delay => 0.1f;

    public override int Damage => 35;

    public override int MagSize => 20;

    public override float Recoil => 1;

    public override float ReloadTime => 1.3f;

    public override float Spread => 2;
}

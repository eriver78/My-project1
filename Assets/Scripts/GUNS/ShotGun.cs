using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : NONAUTOGUN
{
    public override float ScopeSpread => 4;

    public override float Delay => 1.4f;

    public override int Damage => 16;

    public override int MagSize => 6;

    public override float Recoil => 10;

    public override float ReloadTime => 1.2f;

    public override float Spread => 6;
}

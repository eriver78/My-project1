using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public PlayerController player;
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = player.guns[player.GunIndex].ammo+" "+player.guns[player.GunIndex].MagSize;
    }
}

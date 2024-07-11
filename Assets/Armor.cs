using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Armor : MonoBehaviour
{
    public PlayerController player;
    void Update()
    {
        GetComponent<Slider>().value = player.armor;
    }
}

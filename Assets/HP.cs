using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public PlayerController player;
    void Update()
    {
        GetComponent<Slider>().value = player.hp;
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponentInParent<PlayerController>().onGround=true;
    }
    private void OnTriggerExit(Collider other)
    {
        gameObject.GetComponentInParent<PlayerController>().onGround = false;
    }
}

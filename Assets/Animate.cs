using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{
    [SerializeField] Animator animator;
    public void Anim()
    {
        animator.Play("1");
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    public bool used = false;
    [SerializeField] LineRenderer line;
    [SerializeField] float hookSpeed = 0.01f;
    private Vector3 start, end;
    private bool got;
    public void Launch(Vector3 pos,bool got)
    {
        used = true;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position);
        line.enabled = true;
        start = transform.position;
        end = pos;
        this.got = got;
        StartCoroutine("Draw");
    }
    private void Update()
    {
        if (used)
        {
            line.SetPosition(0, transform.position);
            if (Vector3.Distance(transform.position, end) <= 3)
            {
                Destroy(gameObject.GetComponent<SpringJoint>());
                line.SetPosition(1, transform.position);
                line.enabled = false;
                used = false;
                StopCoroutine("Draw");
            }
        }
    }
    private IEnumerator Draw()
    {
        while (Vector3.Distance(line.GetPosition(1),end)>0.3) 
        {
            Vector3 newpos = line.GetPosition(1) + (end - start).normalized*hookSpeed;
            line.SetPosition(1,newpos);
            yield return new WaitForSeconds(0.01f);
        }
        line.SetPosition(1, end);
        if (!got)
        {
            while (Vector3.Distance(line.GetPosition(1), transform.position)>0.3f)
            {
             Vector3 newpos = line.GetPosition(1) + (transform.position - line.GetPosition(1)).normalized * hookSpeed;
                line.SetPosition(1, newpos);
                yield return new WaitForSeconds(0.01f);
            }
            line.SetPosition(1,transform.position);
            line.enabled=false;
            used = false;
        }
        else
        {
            SpringJoint joint = gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = end;
            joint.maxDistance = 3f;
            joint.minDistance = 0.5f;
            joint.spring = 100f;
            joint.damper = 7f;
            joint.massScale = 5f;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hmmm : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform t;
    Rigidbody2D r;
    private Vector3 t_e;
    private Vector3 pos;
    void Start()
    {
        r = GetComponent<Rigidbody2D>();
        t_e = t.position;
        pos = t.worldToLocalMatrix.MultiplyPoint3x4(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (!t.position.Equals(t_e))
        {
            r.isKinematic = true;

        }
        else
        {
            r.isKinematic = false ;
            transform.position = t.localToWorldMatrix.MultiplyPoint3x4(pos);
        }
        t_e = t.position;
    }
}

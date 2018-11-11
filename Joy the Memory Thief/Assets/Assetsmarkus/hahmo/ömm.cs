using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ömm : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform t;
    HingeJoint2D joint;
    Vector2 pos;
    float dis;
    void Start()
    {
        joint = GetComponent<HingeJoint2D>();
        pos = transform.localToWorldMatrix.MultiplyPoint3x4(joint.connectedAnchor);
        dis = Vector2.Distance(pos, transform.position);
        pos = t.transform.worldToLocalMatrix.MultiplyPoint3x4(pos);
        print(transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        JointAngleLimits2D limits = joint.limits;
        //print("max " + joint.jointAngle + " ... " + limits.max);
        if (joint.jointAngle >= limits.max || joint.jointAngle <= limits.min)
        {
            print("moI");
            Vector2 v = transform.position - t.localToWorldMatrix.MultiplyPoint3x4(pos);
            v.Normalize();
            Vector3 v3 = t.localToWorldMatrix.MultiplyPoint3x4(pos) + new Vector3(dis * v.x, dis * v.y, 0);
            print(v3);
            this.transform.position = v3;
        }
            
    }
}

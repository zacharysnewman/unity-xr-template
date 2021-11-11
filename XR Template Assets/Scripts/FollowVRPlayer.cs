using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowVRPlayer : MonoBehaviour
{
    public Transform vrCamera;
    public float forwardOffset;
    public float heightOffset;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = this.vrCamera.position + (this.forwardOffset * this.transform.forward) + (heightOffset * this.transform.up);
        this.transform.localEulerAngles = new Vector3(0, this.vrCamera.eulerAngles.y, 0);
    }
}

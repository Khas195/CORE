using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateMouse : MonoBehaviour
{
    [SerializeField]
    float sensitivity = 0;
    [SerializeField]
    float headUpperLimit = 0;
    [SerializeField]
    float headLowerLimit = 0;
    [SerializeField]
    Transform pitchPivot = null;
    [SerializeField]
    Transform yawnPivot = null;
    float curPitch;
        // Start is called before the first frame update
    void Start()
    {    }

    // Update is called once per frame
    void Update()
    {
        var yawn = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        yawnPivot.Rotate(Vector3.up * yawn );
        var pitch = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        curPitch += pitch;
        if (curPitch > headUpperLimit)
        {
            curPitch = headUpperLimit;
            pitch = 0;
        } else if (curPitch < headLowerLimit)  {
            curPitch = headLowerLimit;
            pitch = 0;
        }
        pitchPivot.Rotate(Vector3.left* pitch );

    }  
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private float m_speed = 5.0f;
    [SerializeField] private int angleErrorScope;
    //private float m_rotationValue;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        float zRot = Mathf.Atan2(Input.GetAxis("Vertical"),
                            Input.GetAxis("Horizontal")) * (180 / Mathf.PI) + 90;
        transform.eulerAngles = new Vector3(0,0, zRot);


        float j2Angle = Mathf.Atan2(Input.GetAxis("J2 Vertical"),
                            Input.GetAxis("J2 Horizontal")) * (180 / Mathf.PI) + 90;

        if (j2Angle >= transform.rotation.eulerAngles.z - 90 + angleErrorScope &&
            j2Angle <= transform.rotation.eulerAngles.z - 90 - angleErrorScope)
            Debug.Log("yes" + transform.rotation.eulerAngles.z + "  " + j2Angle);    
        //transform.position += transform.up;
    }
}

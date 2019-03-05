using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    private float m_rotationValue;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.E))
            m_rotationValue++;

        if (Input.GetKey(KeyCode.Q))
            m_rotationValue--;

        if ((int)transform.rotation.z != (int)m_rotationValue)
            transform.rotation = Quaternion.Euler(0,0,m_rotationValue);
    }
}

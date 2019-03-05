using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugUI : MonoBehaviour
{

    [SerializeField] private GameObject m_characterRef;
    [SerializeField] private TextMeshProUGUI m_angleValue;
    [SerializeField] private TextMeshProUGUI m_horizAxisValue;
    [SerializeField] private TextMeshProUGUI m_vertAxisValue;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_angleValue.text = "Angle : " + Mathf.Abs(m_characterRef.transform.rotation.z);
        m_horizAxisValue.text = "HorizAxisValue : " + Input.GetAxis("Horizontal");
        m_vertAxisValue.text = "VertAxisValue : " + Input.GetAxis("Vertical");


    }
}

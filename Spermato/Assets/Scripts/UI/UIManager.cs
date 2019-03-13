using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private GameObject m_playerRef;
    [SerializeField] private Image m_speedGauge;
    private float m_gaugeValue;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_gaugeValue = m_playerRef.GetComponent<Rigidbody2D>().velocity.magnitude / m_playerRef.GetComponent<Controller>().m_maxSpeed;
        m_speedGauge.fillAmount = m_gaugeValue;
    }
}

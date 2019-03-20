using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private GameObject m_playerRef;
    [SerializeField] private Image m_speedGauge;
    [SerializeField] private TextMeshProUGUI m_progesteroneText;
    private float m_gaugeValue;

	// Use this for initialization
	void Start ()
    {
		if (m_playerRef == null)
            m_playerRef = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_gaugeValue = m_playerRef.GetComponent<Rigidbody2D>().velocity.magnitude / m_playerRef.GetComponent<Controller>().m_maxSpeed;
        m_speedGauge.fillAmount = m_gaugeValue;
        m_progesteroneText.text = "Progesterone Level : " + m_playerRef.GetComponent<Controller>().m_progesterone;
    }
}

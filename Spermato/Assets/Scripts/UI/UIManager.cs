using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private GameObject m_playerRef;
    [SerializeField] private Image m_speedGauge;
    [SerializeField] private Image m_progesteroneGauge;
    private float m_gaugeValue;
    private float m_progesteroneValue;
    private Rigidbody2D m_playerRB;
    private Controller m_playerController;

	// Use this for initialization
	void Start ()
    {
		if (m_playerRef == null)
            m_playerRef = GameObject.FindGameObjectWithTag("Player");

        m_playerRB = GameManager.m_instance.GetPlayerFromList(0).GetComponent<Rigidbody2D>();
        m_playerController = m_playerRef.GetComponent<Controller>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        m_gaugeValue = m_playerRB.velocity.magnitude / m_playerController.m_maxSpeed;
        m_speedGauge.fillAmount = m_gaugeValue;
        m_progesteroneValue = m_playerController.m_progesterone / m_playerController.m_maxProgesterone;
        m_progesteroneGauge.fillAmount = m_progesteroneValue;
    }
}

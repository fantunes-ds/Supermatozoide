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
    private List<Rigidbody2D> m_playerRBs;
    private List<Controller> m_playerControllers;

	// Use this for initialization
	void Start ()
    {
		if (m_playerRef == null)
            m_playerRef = GameObject.FindGameObjectWithTag("Player"); 

        m_playerRBs = new List<Rigidbody2D>();
        m_playerControllers = new List<Controller>();

        foreach (GameObject player in GameManager.m_instance.m_playerList)
        {
            m_playerRBs.Add(player.GetComponent<Rigidbody2D>());
            m_playerControllers.Add(player.GetComponent<Controller>());
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < m_playerControllers.Count; ++i)
        {
            m_gaugeValue = m_playerRBs[i].velocity.magnitude / m_playerControllers[i].m_maxSpeed;
            m_speedGauge.fillAmount = m_gaugeValue;
            m_progesteroneValue = m_playerControllers[i].m_progesterone / m_playerControllers[i].m_maxProgesterone;
            m_progesteroneGauge.fillAmount = m_progesteroneValue;
        }
    }
}

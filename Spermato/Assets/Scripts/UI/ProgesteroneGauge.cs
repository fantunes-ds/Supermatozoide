using UnityEngine;
using UnityEngine.UI;

public class ProgesteroneGauge : MonoBehaviour
{
    public GameObject m_targetPlayer;
    private Controller m_playerController;
    [SerializeField] private Image m_progesteroneGauge;
    private float m_progesteroneValue;

    void Start()
    {
        if (m_targetPlayer == null)
            m_targetPlayer = GameObject.FindGameObjectWithTag("Player");

        m_playerController = m_targetPlayer.GetComponent<Controller>();
    }

    void Update()
    {
        transform.position = m_targetPlayer.transform.position + new Vector3(1, 0, -2);

        m_progesteroneValue = m_playerController.m_progesterone / m_playerController.m_maxProgesterone;
        m_progesteroneGauge.fillAmount = m_progesteroneValue;
    }
}

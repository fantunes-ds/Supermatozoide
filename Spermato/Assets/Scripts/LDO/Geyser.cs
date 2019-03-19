using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geyser : MonoBehaviour
{
    public enum GeyserType
    {
        Positive,
        Negative
    }

    public GeyserType m_geyserType;

    private Collider2D m_col;
    private ParticleSystem m_ps;

    private float m_timer;
    private bool m_isFlowing;

    // Use this for initialization
    void Start()
    {
        m_ps = GetComponentInChildren<ParticleSystem>();
        ResetToActive();
    }

    // Update is called once per frame
    void Update()
    {
        CheckStatus();
    }

    void CheckStatus()
    {
        m_timer += Time.deltaTime;

        if (m_timer >= 2.0f && m_timer < 5.0f)
        {
            m_isFlowing = false;
            m_ps.Stop();
        }

        else if (m_timer >= 5.0f)
            ResetToActive();
    }

    void ResetToActive()
    {
        m_timer = 0;

        m_isFlowing = true;
        m_ps.Play();
    }
}

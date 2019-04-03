using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geyser : MonoBehaviour
{
    private ParticleSystem m_ps;

    [SerializeField] private float m_pushForce;

    private float m_timer;
    private bool m_isFlowing;

    // Use this for initialization
    void Start()
    {
        m_ps = GetComponentInChildren<ParticleSystem>();
        var col = m_ps.collision;
        col.colliderForce = m_pushForce;
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

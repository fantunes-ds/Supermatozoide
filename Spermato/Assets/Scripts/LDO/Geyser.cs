using UnityEngine;

public class Geyser : MonoBehaviour
{
    private ParticleSystem m_ps;

    [SerializeField] private float m_pushForce;

    private float m_timer;
    private bool m_isFlowing;

    void Start()
    {
        m_ps = GetComponentInChildren<ParticleSystem>();
        var col = m_ps.collision;
        col.colliderForce = m_pushForce;
        ResetToActive();
    }

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

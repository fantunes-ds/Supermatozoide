        using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    [SerializeField][Range(0,10)] private float m_pushForce = 1.0f;
    private Rigidbody2D m_rb;

	// Use this for initialization
	void Start ()
    {
        m_rb = GetComponent<Rigidbody2D>();
	}

    private void OnCollisionEnter2D(Collision2D p_other)
    {
        if (m_rb == null)
            return;

        Vector2 bounceDir = Vector2.Reflect(m_rb.velocity, p_other.transform.up);
        m_rb.AddForce(Vector2.right * m_pushForce, ForceMode2D.Impulse);
    }
}

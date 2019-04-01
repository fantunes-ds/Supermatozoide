﻿        using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour {

    [SerializeField] private float m_pushForce = 100.0f;
    private Rigidbody2D m_rb;

	// Use this for initialization
	void Start ()
    {
        m_rb = GetComponent<Rigidbody2D>();
	}

    private void OnCollisionEnter2D(Collision2D p_other)
    {
        Vector2 bounceDir = Vector2.Reflect(m_rb.velocity, p_other.transform.up); 
        m_rb.AddForce(Vector2.right * m_pushForce, ForceMode2D.Force);
    }
}

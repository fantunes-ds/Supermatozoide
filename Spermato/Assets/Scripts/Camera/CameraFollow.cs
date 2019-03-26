﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Vector3 m_camPos = Vector3.zero;
    public Transform m_target;
    [SerializeField] private Vector3 m_offset = new Vector3(0, 0, -1);
    [SerializeField] private float m_travelTime = 0.3f;

    void Start()
    {
        if (m_target == null)
            m_target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        if (m_target != null)
        m_camPos = new Vector3(Mathf.SmoothStep(transform.position.x, m_target.transform.position.x, m_travelTime), Mathf.SmoothStep(transform.position.y, m_target.transform.position.y, m_travelTime));
    }

    void LateUpdate()
    {
        transform.position = m_camPos + m_offset;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    Vector3 m_camPos = Vector3.zero;
    public Transform m_target;
    private Rigidbody2D m_targetRb;
    [SerializeField] private Vector3 m_offset = new Vector3(0, 0, -1);
    [SerializeField] private float m_travelDelay = 0.3f;
    [SerializeField] private float m_cameraMaxVelocity = 200;
    [SerializeField] private float m_offsetAmount = 5;
    private float m_cameraVelocityX;
    private float m_cameraVelocityY;

    void Start()

    {
        if (m_targetRb == null)
        {
            m_targetRb = m_target.gameObject.GetComponent<Rigidbody2D>();
            m_camPos = m_target.position;
            transform.position = m_target.position;
        }
    }

    void FixedUpdate()
    {
        if (m_target == null || m_targetRb == null)
            return;

        Vector2 m_desiredDirection = (Vector2) m_target.position + (m_targetRb.velocity * m_offsetAmount);
        m_camPos = m_target.position;
        m_camPos = new Vector3(
            Mathf.SmoothDamp(transform.position.x, m_desiredDirection.x, ref m_cameraVelocityX, m_travelDelay,
                m_cameraMaxVelocity, Time.fixedDeltaTime),
            Mathf.SmoothDamp(transform.position.y, m_desiredDirection.y, ref m_cameraVelocityY, m_travelDelay,
                m_cameraMaxVelocity, Time.fixedDeltaTime));
    }

    void LateUpdate()
    {
        transform.position = m_camPos + m_offset;
    }
}

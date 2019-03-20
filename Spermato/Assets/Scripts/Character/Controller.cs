using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.XR.WSA.Persistence;

public class Controller : MonoBehaviour
{
    private bool m_isLeft;
    private bool m_canInput;

    #region PublicParameters
    [SerializeField]
    [Tooltip("Constant base speed for the player")]
    private float m_baseSpeed = 1.0f;

    [SerializeField]
    [Tooltip("Ajoute du AirTime")]
    [Range(0, 100)]
    private float m_airTimePercentage = 1f;

    [SerializeField]
    [Tooltip("How much should the progesterone boost be (0 to 1 = 0 to 100%)")]
    [Range(0, 1)]
    private float m_progesteroneBoost = 0.05f;

    [Tooltip("The max amount of velocity that can be achieved. Affects the gauge")]
    public float m_maxSpeed;
    #endregion

    public float m_progesterone { get; private set; }
    public float m_finalSpeed { get; private set; }

    private float m_zRot;

    private Vector2 m_rightJoyStickAxis;
    private Vector2 m_leftJoyStickAxis;

    private Rigidbody2D m_rb;

    // Use this for initialization
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_progesterone = 1.0f;
        m_finalSpeed = m_baseSpeed * m_progesterone;
    }

    // Update is called once per frame
    void Update()
    {
        SetRotation();
        CheckDisplacement();
    }

    void SetRotation()
    {
        m_leftJoyStickAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (m_leftJoyStickAxis.x.Equals(0.0f) && m_leftJoyStickAxis.y.Equals(0.0f))
            return;

        if (m_rb.velocity.magnitude > 1f && m_rb.velocity.magnitude < m_maxSpeed &&
            Vector2.Angle(m_rb.velocity, transform.up) > 60)
            m_rb.AddForce((new Vector2(transform.up.x, transform.up.y) * m_airTimePercentage) /
                          (m_rb.velocity.magnitude * m_finalSpeed));

        m_zRot = Mathf.Atan2(m_leftJoyStickAxis.y,
                         m_leftJoyStickAxis.x) * Mathf.Rad2Deg + 270;

        if (!m_zRot.Equals(transform.rotation.eulerAngles.z))
            transform.eulerAngles = new Vector3(0, 0, m_zRot);
    }

    void CheckDisplacement()
    {
        m_rightJoyStickAxis = new Vector2(Mathf.RoundToInt(Input.GetAxis("J2 Horizontal")), Mathf.RoundToInt(Input.GetAxis("J2 Vertical")));
        if (m_rightJoyStickAxis.x.Equals(0))
        {
            m_canInput = true;
            return;
        }
        if (!m_canInput)
            return;

        int axisTargetValue = 1;

        if (m_isLeft)
            axisTargetValue *= -1;

        m_finalSpeed = m_baseSpeed * m_progesterone;

        if (m_rightJoyStickAxis.x.Equals(axisTargetValue))
        {
            if (m_rb.velocity.magnitude < m_maxSpeed)
                m_rb.AddForce(new Vector2(transform.up.x, transform.up.y) * m_finalSpeed);
            m_canInput = false;
            m_isLeft = !m_isLeft;
        }
    }

    public void AddProgesterone()
    {
        m_progesterone += m_progesteroneBoost;
    }
}

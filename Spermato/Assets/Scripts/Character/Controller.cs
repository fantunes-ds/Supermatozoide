using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.XR.WSA.Persistence;

public class Controller : MonoBehaviour
{
    private bool m_isLeft;
    private bool m_canInput;

    [SerializeField] [Tooltip("Half the amount per side. ie: 20 = 10 L + 10 R")]
    private int m_angleErrorScope = 40;

    [SerializeField] [Tooltip("Constant base speed for the player")]
    private float m_baseSpeed = 1.0f;

    [SerializeField] [Tooltip("Ajoute du AirTime")][Range(0, 100)]
    private float m_airTimePercentage = 1f;

    [SerializeField][Tooltip("How much should the progesterone boost be (0 to 1 = 0 to 100%)")][Range(0,1)]
    private float m_progesteroneBoost = 0.05f;

    [SerializeField] [Tooltip("The max amount of velocity that can be achieved. Affects the gauge")]
    public float m_maxSpeed = 100f;

    public float m_progesterone { get; private set; }
    public float m_finalSpeed;

    private float m_zRot;
    private float m_j2Angle = 5.0f;

    private float m_leftJoyStickHorizValue;
    private float m_leftJoyStickVertValue;
    private float m_rightJoyStickHorizValue;
    private float m_rightJoyStickVertValue;

    private Rigidbody2D m_rb;

    // Use this for initialization
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_finalSpeed = m_baseSpeed * m_progesterone;
        m_progesterone = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        SetRotation();
        CheckDisplacement();
    }

    void SetRotation()
    {
        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
            return;

        if (m_rb.velocity.magnitude > 1f && m_rb.velocity.magnitude < m_maxSpeed && Vector2.Angle(m_rb.velocity, transform.up) > 60)
            m_rb.AddForce((new Vector2(transform.up.x, transform.up.y) * m_airTimePercentage) / (m_rb.velocity.magnitude * m_finalSpeed));

        if (m_leftJoyStickHorizValue != Input.GetAxis("Horizontal") || m_leftJoyStickVertValue != Input.GetAxis("Vertical"))
        {
            m_leftJoyStickHorizValue = Input.GetAxis("Horizontal");
            m_leftJoyStickVertValue = Input.GetAxis("Vertical");
            m_zRot = Mathf.Atan2(m_leftJoyStickVertValue,
                         m_leftJoyStickHorizValue) * Mathf.Rad2Deg + 270;
        }

        if (m_zRot != transform.rotation.eulerAngles.z)
            transform.eulerAngles = new Vector3(0, 0, m_zRot);
    }

    void CheckDisplacement()
    {
        if (Input.GetAxis("J2 Vertical") == 0 && Input.GetAxis("J2 Horizontal") == 0)
        {
            m_canInput = true;
            return;
        }
        if (!m_canInput)
            return;


        int detectionOffset = 90;

        m_j2Angle = Mathf.Atan2(Input.GetAxis("J2 Vertical"),
                      Input.GetAxis("J2 Horizontal")) * Mathf.Rad2Deg + 270;

        if (m_isLeft)
            detectionOffset *= -1;

        m_finalSpeed = m_baseSpeed * m_progesterone;
        
        //m_zRot is defined in SetRotation()
        float minimumValue = m_zRot - detectionOffset - (m_angleErrorScope * 0.5f);
        float maximumValue = m_zRot - detectionOffset + (m_angleErrorScope * 0.5f);


        if ((m_j2Angle >= minimumValue && m_j2Angle <= maximumValue)
            ||
            (m_j2Angle >= minimumValue - 360 && m_j2Angle <= maximumValue - 360) // - one turn
            ||
            (m_j2Angle >= minimumValue + 360 && m_j2Angle <= maximumValue + 360)) // + one turn
        {
            if (m_rb.velocity.magnitude + (transform.up.magnitude * m_finalSpeed) < m_maxSpeed)
            {
                m_rb.velocity += new Vector2(transform.up.x, transform.up.y) * m_finalSpeed;
                m_isLeft = !m_isLeft;
            }
            m_canInput = false;
        }
    }

    public void AddProgesterone()
    {
        m_progesterone += m_progesteroneBoost;
    }
}

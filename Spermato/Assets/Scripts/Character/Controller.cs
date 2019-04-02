using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.XR.WSA.Persistence;

public class Controller : MonoBehaviour
{
    private bool m_isLeft;
    private bool m_canInput;
    private bool m_canTrigger;

    #region PublicParameters
    [SerializeField]
    [Tooltip("Constant base speed for the player")]
    private float m_baseSpeed = 1.0f;

    [SerializeField] [Tooltip("Adds AirTime")] [Range(0, 10)]
    private float m_airTimePercentage = 1f;

    [SerializeField] [Tooltip("How much should the progesterone pickups add to the jauge (0 to 1 = 0 to 100%)")] [Range(0, 1)]
    private float m_progesteronePickupValue = 0.05f;

    [SerializeField] [Tooltip("How much should the progesterone boost force be (5 is a lot)")]
    private float m_progesteroneBoostForce = 5;
    [SerializeField] [Tooltip("How much should the progesterone boost force decrease the progesterone bar (0 to 1 = 0 to 100%)")]
    [Range(0, 1)]
    private float m_progesteroneLossPerBoost = 0.5f;

    [Tooltip("Maximum Progesterone that can be stored (affects the bar)")][Range(0,10)]
    public float m_maxProgesterone = 1f;

    [Tooltip("The max amount of velocity that can be achieved. Affects the gauge")]
    public float m_maxSpeed;
    #endregion

    public float m_progesterone { get; private set; }

    private float m_zRot;

    private Vector2 m_rightJoyStickAxis;
    private Vector2 m_leftJoyStickAxis;

    private Rigidbody2D m_rb;

    // Use this for initialization
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        SetRotation();
        CheckDisplacement();
        ProgesteroneGauge();
    }

    void SetRotation()
    {
        m_leftJoyStickAxis = new Vector2(Input.GetAxis(gameObject.name + "-LeftStick-Horizontal"), Input.GetAxis(gameObject.name + "-LeftStick-Vertical"));
        if (m_leftJoyStickAxis.x.Equals(0.0f) && m_leftJoyStickAxis.y.Equals(0.0f))
            return;

        if (m_rb.velocity.magnitude > 1f && m_rb.velocity.magnitude < m_maxSpeed &&
            Vector2.Angle(m_rb.velocity, transform.up) > 60)
            m_rb.AddForce((new Vector2(transform.up.x, transform.up.y) * m_airTimePercentage) /
                          (m_rb.velocity.magnitude ), ForceMode2D.Impulse);

        m_zRot = Mathf.Atan2(m_leftJoyStickAxis.y,
                         m_leftJoyStickAxis.x) * Mathf.Rad2Deg + 270;

        if (!m_zRot.Equals(transform.rotation.eulerAngles.z))
            transform.eulerAngles = new Vector3(0, 0, m_zRot);
        //m_rb.AddForce(new Vector2(transform.rotation.y * m_rb.velocity.x,transform.rotation.y * m_rb.velocity.y) * 1000);
    }

    void CheckDisplacement()
    {
        m_rightJoyStickAxis = new Vector2(Mathf.RoundToInt(Input.GetAxis(gameObject.name + "-RightStick-Horizontal")), Mathf.RoundToInt(Input.GetAxis(gameObject.name + "-RightStick-Vertical")));

        if (m_rightJoyStickAxis.x.Equals(0) && m_rightJoyStickAxis.y.Equals(0))
        {
            m_canInput = true;
            return;
        }

        if (!m_canInput)
            return;

        int axisTargetValue = 1;

        if (m_isLeft)
            axisTargetValue *= -1;
        

        if (m_rightJoyStickAxis.x.Equals(axisTargetValue))
        {
            if (m_rb.velocity.magnitude < m_maxSpeed)
                m_rb.AddForce(new Vector2(transform.up.x, transform.up.y) * m_baseSpeed);
            m_canInput = false;
            m_isLeft = !m_isLeft;
        }
    }

    public void ProgesteroneGauge()
    {
        if (Input.GetAxisRaw(gameObject.name + "-Trigger-Right").Equals(1) && m_canTrigger)
        {
            if (m_progesterone > 0)
            {
                m_rb.AddForce(transform.up * m_progesteroneBoostForce, ForceMode2D.Impulse);
                m_progesterone -= m_progesteroneLossPerBoost;
                m_progesterone = Mathf.Clamp(m_progesterone, 0, m_maxProgesterone);
                m_canTrigger = false;
            }
        }
        else if (Input.GetAxisRaw(gameObject.name + "-Trigger-Right").Equals(0) && m_canTrigger == false)
            m_canTrigger = true;
    }

    public void AddProgesterone()
    {
        m_progesterone += m_progesteronePickupValue;
    }
}

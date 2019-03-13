using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private bool isLeft;

    [SerializeField] [Tooltip("Half the amount per side. ie: 20 = 10 L + 10 R")]
    private int angleErrorScope;

    [SerializeField] [Tooltip("Constant base speed for the player")]
    private float m_baseSpeed = 1.0f;

    [SerializeField] [Tooltip("The max amount of velocity that can be achieved. Affects the gauge")]
    public float m_maxSpeed = 100f;

    private float m_progesterone = 1.0f;
    public float m_finalSpeed { get; private set; }

    private float m_zRot = 0.0f;
    private float j2Angle = 5.0f;

    private float leftJoyStickHorizValue;
    private float leftJoyStickVertValue;
    private float rightJoyStickHorizValue;
    private float rightJoyStickVertValue;

    private Rigidbody2D m_rb;

    // Use this for initialization
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
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
        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
            return;

        if (leftJoyStickHorizValue != Input.GetAxis("Horizontal") || leftJoyStickVertValue != Input.GetAxis("Vertical"))
        {
            leftJoyStickHorizValue = Input.GetAxis("Horizontal");
            leftJoyStickVertValue = Input.GetAxis("Vertical");
            m_zRot = Mathf.Atan2(leftJoyStickVertValue,
                         leftJoyStickHorizValue) * Mathf.Rad2Deg + 270;
        }

        if (m_zRot != transform.rotation.eulerAngles.z)
            transform.eulerAngles = new Vector3(0, 0, m_zRot);
    }

    void CheckDisplacement()
    {
        if (Input.GetAxis("J2 Vertical") == 0 && Input.GetAxis("J2 Horizontal") == 0)
            return;

        int detectionOffset = 90;

        j2Angle = Mathf.Atan2(Input.GetAxis("J2 Vertical"),
                      Input.GetAxis("J2 Horizontal")) * (180 / Mathf.PI) + 270;

        if (isLeft)
            detectionOffset *= -1;

        m_finalSpeed = m_baseSpeed * m_progesterone;
        //m_zRot is defined in SetRotation()
        float minimumValue = m_zRot - detectionOffset - (angleErrorScope * 0.5f);
        float maximumValue = m_zRot - detectionOffset + (angleErrorScope * 0.5f);


        if ((j2Angle >= minimumValue && j2Angle <= maximumValue)
            ||
            (j2Angle >= minimumValue - 360 && j2Angle <= maximumValue - 360) // - one turn
            ||
            (j2Angle >= minimumValue + 360 && j2Angle <= maximumValue + 360)) // + one turn
        {
            if (m_rb.velocity.magnitude + (transform.up.magnitude * m_finalSpeed) < m_maxSpeed)
            {
                m_rb.velocity += new Vector2(transform.up.x, transform.up.y) * m_finalSpeed;
                isLeft = !isLeft;
            }
        }
        /*else if ((j2Angle < minimumValue || j2Angle > maximumValue)
                 ||
                 (j2Angle < minimumValue - 360 || j2Angle > maximumValue - 360) // - one turn
                 ||
                 (j2Angle < minimumValue + 360 || j2Angle > maximumValue + 360)) // + one turn
        {
            if (m_rb.velocity.magnitude + (transform.up.magnitude * m_finalSpeed) < m_maxSpeed)
            {
                m_rb.velocity -= new Vector2(transform.up.x, transform.up.y) * m_finalSpeed;
                isLeft = !isLeft;
            }
        }*/
    }
}

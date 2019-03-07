using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public bool isLeft { get; private set; }
    [SerializeField] private int angleErrorScope;

    [SerializeField] private float m_baseSpeed = 5.0f;
    private float m_zRot = 0.0f;
    private float j2Angle = 5.0f;

    private Rigidbody2D m_rb;
        float leftJoyStickHorizValue;
        float leftJoyStickVertValue;
        float rightJoyStickHorizValue;
        float rightJoyStickVertValue;

    // Use this for initialization
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (leftJoyStickHorizValue != Input.GetAxis("Horizontal") || leftJoyStickVertValue != Input.GetAxis("Vertical"))
            m_zRot = Mathf.Atan2(Input.GetAxis("Vertical"),
                     Input.GetAxis("Horizontal")) * (180 / Mathf.PI) + 270;

        if (m_zRot != transform.rotation.eulerAngles.z)
            transform.eulerAngles = new Vector3(0, 0, m_zRot);

        j2Angle = Mathf.Atan2(Input.GetAxis("J2 Vertical"),
                      Input.GetAxis("J2 Horizontal")) * (180 / Mathf.PI) + 270;

        int detectionOffset = 90;

        if (isLeft)
            detectionOffset *= -1;

        float minimumValue = m_zRot - detectionOffset - (angleErrorScope * 0.5f);
        float maximumValue = m_zRot - detectionOffset + (angleErrorScope * 0.5f);


        if ((j2Angle >= minimumValue && j2Angle <= maximumValue)
            ||
            (j2Angle >= minimumValue - 360 && j2Angle <= maximumValue - 360)
            ||
            (j2Angle >= minimumValue + 360 && j2Angle <= maximumValue + 360))
        {
            isLeft = !isLeft;
            m_rb.velocity += new Vector2(transform.up.x, transform.up.y) * m_baseSpeed;
        }
    }
}

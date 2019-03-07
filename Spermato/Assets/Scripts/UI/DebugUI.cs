using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugUI : MonoBehaviour
{
    [SerializeField] private GameObject m_characterRef;
    [SerializeField] private List<TextMeshProUGUI> m_texts;
    [SerializeField] private int angleErrorScope;
    private bool isLeft;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        isLeft = m_characterRef.GetComponent<Controller>().isLeft;

        int detectionOffset = 90;
        if (isLeft)
            detectionOffset *= -1;

        int j1Input = (int) (Mathf.Atan2(Input.GetAxis("Vertical"),
                                 Input.GetAxis("Horizontal")) * (180 / Mathf.PI) + 270);
        int j2Input = (int) (Mathf.Atan2(Input.GetAxis("J2 Vertical"),
                                         Input.GetAxis("J2 Horizontal")) * (180 / Mathf.PI) + 270);

        m_texts[0].text = "Left Joystick Angle : " + j1Input;
        m_texts[1].text = "Right Joystick Angle : " + j2Input;
        m_texts[2].text = "TargetAngle : Between " +
                          (j1Input - detectionOffset + (angleErrorScope * 0.5f)) +
                          " and " +
                          (j1Input - detectionOffset + (angleErrorScope * 0.5f));
        m_texts[3].text = "HorizAxisValue : " + Input.GetAxis("J2 Horizontal");
        m_texts[4].text = "VertAxisValue : " + Input.GetAxis("J2 Vertical");
    }
}

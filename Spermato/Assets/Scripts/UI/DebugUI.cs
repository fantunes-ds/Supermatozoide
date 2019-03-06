using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugUI : MonoBehaviour
{
    [SerializeField] private GameObject m_characterRef;
    [SerializeField] private List<TextMeshProUGUI> m_texts;
    [SerializeField] private int angleErrorScope;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        m_texts[0].text = "Left Joystick Angle : " + (int)(Mathf.Atan2(Input.GetAxis("Vertical"),
                                                               Input.GetAxis("Horizontal")) * (180 / Mathf.PI) + 90);
        m_texts[1].text = "Right Joystick Angle : " + (int)(Mathf.Atan2(Input.GetAxis("J2 Vertical"),
                                                               Input.GetAxis("J2 Horizontal")) * (180 / Mathf.PI) + 90);
        m_texts[2].text = "TargetAngle : Between " +
                          (int)(m_characterRef.transform.rotation.eulerAngles.z - 180 -
                                          angleErrorScope * 0.5f) +
                          " and " +
                          (int)(m_characterRef.transform.rotation.eulerAngles.z - 180 +
                                          angleErrorScope * 0.5f);
        m_texts[3].text = "HorizAxisValue : " + Input.GetAxis("J2 Horizontal");
        m_texts[4].text = "VertAxisValue : " + Input.GetAxis("J2 Vertical");
    }
}

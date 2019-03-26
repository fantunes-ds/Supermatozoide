using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Color[] m_colors;
    public List<Image> m_masks;
    public List<TextMeshProUGUI> m_status;
    public List<TextMeshProUGUI> m_playerName;
    private int m_connections = 0;

	// Use this for initialization
	void Start ()
    {
        foreach (TextMeshProUGUI t in m_status)
        {
            t.text = "Unplugged";
        }

        StartCoroutine(CheckConnections());
    }

    IEnumerator CheckConnections()
    {
        if (m_connections == Input.GetJoystickNames().Length)
            yield return new WaitForSeconds(1);

        m_connections = Input.GetJoystickNames().Length;

        for (int i = 0; i < m_connections; ++i)
        {
            m_status[i].text = "Plugged in";
            m_status[i].color = m_colors[1];
            m_playerName[i].color = m_colors[1];
            StartCoroutine(FadeIn(m_masks[i]));
        }

        for (int i = m_connections; i < 3; ++i)
        {
            m_status[i].text = "Unplugged";
            m_status[i].color = m_colors[0];
            m_playerName[i].color = m_colors[0];
            StartCoroutine(FadeOut(m_masks[i]));
        }
        SplitScreenManager.m_instance.SetCameraNumber(m_connections);
        yield return new WaitForSeconds(2);
    }

    IEnumerator FadeIn(Image p_image)
    {
        for (float f = 0.5f; f >= 0f; f -= Time.deltaTime)
        {
            p_image.color = new Color(p_image.color.r, p_image.color.g,
                                      p_image.color.b, f);
            yield return null;
        }
    }

    IEnumerator FadeOut(Image p_image)
    {
        for (float f = 0f; f >= 0.5f; f += Time.deltaTime)
        {
            p_image.color = new Color(p_image.color.r, p_image.color.g,
                                      p_image.color.b, f);
            yield return null;

        }
    }
}

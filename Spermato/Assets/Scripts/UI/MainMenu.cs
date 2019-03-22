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
    private int m_connections;

	// Use this for initialization
	void Start ()
    {
        foreach (TextMeshProUGUI t in m_status)
        {
            t.text = "Unplugged";
        }	
	}

    void CheckConnections()
    {
        if (m_connections == Input.GetJoystickNames().Length)
            return;

        m_connections = Input.GetJoystickNames().Length;

        for (int i = 0; i < m_connections; ++i)
        {
            m_status[i].text = "Plugged in";
            m_status[i].color = m_colors[1];
            StartCoroutine(FadeOut(m_masks[i]));
        }

        for (int i = m_connections; i < m_connections; ++i)
        {
            
        }

    }

	// Update is called once per frame
	void Update ()
    {
        CheckConnections();
	}

    IEnumerator FadeIn(Image p_image)
    {
        for (float f = 1f; f >= 0f; f -= Time.deltaTime)
        {
            p_image.color = new Color(p_image.color.r, p_image.color.g,
                                      p_image.color.b, f);
            yield return null;
        }
    }

    IEnumerator FadeOut(Image p_image)
    {
        for (float f = 0f; f >= 1f; f -= Time.deltaTime)
        {
            p_image.color = new Color(p_image.color.r, p_image.color.g,
                                      p_image.color.b, f);
            yield return null;

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public string m_levelName;
    public static GameManager m_instance;

    void Start()
    {
        if (m_instance == null)
        {
            m_instance = this;
            DontDestroyOnLoad(m_instance.gameObject);
        }
        else if (m_instance != null)
            Destroy(m_instance.gameObject);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(m_levelName);
    }
}

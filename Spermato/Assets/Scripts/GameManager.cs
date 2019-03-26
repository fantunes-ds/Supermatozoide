using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public string m_levelName;
    public static GameManager m_instance;

    public GameObject m_playerPrefab;
    public List<GameObject> m_playerList;

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

    public GameObject AddPlayer()
    {
        GameObject newPlayer = Instantiate(m_playerPrefab, Vector3.zero, Quaternion.identity, transform);
        newPlayer.name = "P" + (m_playerList.Count + 1);
        m_playerList.Add(newPlayer);
        return newPlayer;
    }
}

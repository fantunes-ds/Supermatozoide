using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager m_instance; 
    [SerializeField] private string m_levelName;
    [SerializeField] private GameObject m_playerPrefab;
    public List<GameObject> m_playerList { private set; get; }
    private GameObject m_playerContainer;
    private Vector3 m_spawnPoint;
    private Scene m_lastScene;

    void Start()
    {
        if (m_instance == null)
        {
            m_instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (m_instance != null)
            Destroy(gameObject);

        m_lastScene = GetScene();
        m_playerList = new List<GameObject>();
        m_playerContainer = new GameObject("Players");

        if (GameObject.FindWithTag("SpawnPoint") != null)
            m_spawnPoint = GameObject.FindWithTag("SpawnPoint").transform.position;
        else
            m_spawnPoint = Vector3.zero;

    }
    
    void Update()
    {
        if (m_lastScene == SceneManager.GetActiveScene())
            return;
        SplitScreenManager.m_instance.Refresh();
        m_lastScene = SceneManager.GetActiveScene();
        if (GameObject.FindWithTag("SpawnPoint") != null)
            m_spawnPoint = GameObject.FindWithTag("SpawnPoint").transform.position;
        else
            m_spawnPoint = Vector3.zero;
        foreach (GameObject m_go in m_playerList)
        {
            m_go.transform.position = m_spawnPoint;
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(m_levelName);
    }

    public GameObject AddPlayer()
    {
        if (m_playerContainer == null)
            m_playerContainer = new GameObject("Players");
        GameObject newPlayer = Instantiate(m_playerPrefab, m_spawnPoint, Quaternion.identity, m_playerContainer.transform);
        newPlayer.name = "P" + (m_playerList.Count + 1);
        m_playerList.Add(newPlayer);
        return newPlayer;
    }

    public Scene GetScene()
    {
        return SceneManager.GetActiveScene();
    }

    public GameObject GetPlayerFromList(int p_index)
    {
        return m_playerList[p_index];
    }
}

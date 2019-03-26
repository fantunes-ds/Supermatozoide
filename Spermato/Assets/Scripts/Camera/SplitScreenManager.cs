using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitScreenManager : MonoBehaviour
{
    [SerializeField] private Camera m_cameraPrefab;
    private List<Camera> m_splitCameras;
    private int m_currentNumberOfCameras = 1;

    public static SplitScreenManager m_instance;

	// Use this for initialization
	void Awake ()
    {
        if (m_instance == null)
        {
            m_instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (m_instance != null)
            Destroy(gameObject);
        
        m_splitCameras = new List<Camera>();
        UpdateSplitScreen(m_currentNumberOfCameras);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Q))
            UpdateSplitScreen(++m_currentNumberOfCameras);
    }

    private void UpdateSplitScreen(int p_numberOfScreens)
    {
        switch (p_numberOfScreens)
        {
            case 1:
                CheckCameraAvailability(p_numberOfScreens);
                m_splitCameras[0].rect = new Rect(0,0,1,1);
                break;
            case 2:
                CheckCameraAvailability(p_numberOfScreens);
                m_splitCameras[0].rect = new Rect(0f,0,0.499f,1);
                m_splitCameras[1].rect = new Rect(0.5f,0,0.5f,1);
                break;
            case 3:
                CheckCameraAvailability(p_numberOfScreens);
                m_splitCameras[0].rect = new Rect(0f, 0.5f, 0.499f, 0.5f);
                m_splitCameras[1].rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                m_splitCameras[2].rect = new Rect(0.25f, 0, 0.5f, 0.5f);
                break;
            case 4:
                CheckCameraAvailability(p_numberOfScreens);
                m_splitCameras[0].rect = new Rect(0f, 0.5f, 0.499f, 0.5f);
                m_splitCameras[1].rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                m_splitCameras[2].rect = new Rect(0, 0, 0.499f, 0.495f);
                m_splitCameras[3].rect = new Rect(0.5f, 0, 0.5f, 0.495f);
                break;
            default:
                Debug.Log("[SPLIT SCREEN] Format not supported : " + p_numberOfScreens + " screens is not acceptable");
                break;
        }
    }

    void CheckCameraAvailability(int p_numberOfCameras)
    {
        if (m_splitCameras.Count < p_numberOfCameras)
        {
            Camera newCamera = Instantiate(m_cameraPrefab, Vector3.zero, Quaternion.identity, transform);
            newCamera.GetComponent<CameraFollow>().m_target = GameManager.m_instance.AddPlayer().transform;
            m_splitCameras.Add(newCamera);
        }
        else
            for (int i = p_numberOfCameras - 1; i < m_splitCameras.Count - 1; i++)
                m_splitCameras[i].gameObject.SetActive(false);
    }

    public void Refresh()
    {
        UpdateSplitScreen(m_currentNumberOfCameras);
    }

    public void SetCameraNumber(int p_nbOfCameras)
    {
        m_currentNumberOfCameras = p_nbOfCameras;
    }
}


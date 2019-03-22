using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitScreenManager : MonoBehaviour
{
    private Camera m_mainCamera;
    [SerializeField] private Camera m_cameraPrefab;
    private List<Camera> m_splitCameras;
    private int currentNumberOfCameras = 1;

	// Use this for initialization
	void Start ()
    {
        m_mainCamera = GameObject.FindGameObjectWithTag("Main Camera").GetComponent<Camera>();
        m_splitCameras = new List<Camera>();
        m_splitCameras.Add(m_mainCamera);
        UpdateSplitScreen(currentNumberOfCameras);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Q))
            UpdateSplitScreen(++currentNumberOfCameras);
    }

    private void UpdateSplitScreen(int p_numberOfScreens)
    {
        switch (p_numberOfScreens)
        {
            case 1:
                CheckCameraAvailability(p_numberOfScreens);
                m_mainCamera.rect = new Rect(0,0,1,1);
                break;
            case 2:
                CheckCameraAvailability(p_numberOfScreens);
                m_splitCameras[0].rect = new Rect(0f,0,0.5f,1);
                m_splitCameras[1].rect = new Rect(0.5f,0,0.5f,1);
                break;
            case 3:
                CheckCameraAvailability(p_numberOfScreens);
                m_splitCameras[0].rect = new Rect(0f, 0.5f, 0.5f, 0.5f);
                m_splitCameras[1].rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                m_splitCameras[2].rect = new Rect(0.25f, 0, 0.5f, 0.5f);
                break;
            case 4:
                CheckCameraAvailability(p_numberOfScreens);
                m_splitCameras[0].rect = new Rect(0f, 0.5f, 0.5f, 0.5f);
                m_splitCameras[1].rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                m_splitCameras[2].rect = new Rect(0, 0, 0.5f, 0.5f);
                m_splitCameras[3].rect = new Rect(0.5f, 0, 0.5f, 0.5f);
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
            Camera newCamera = Instantiate(m_cameraPrefab, m_mainCamera.transform.position, Quaternion.identity, transform);
            m_splitCameras.Add(newCamera);
        }
        else
            for (int i = p_numberOfCameras - 1; i < m_splitCameras.Count - 1; i++)
                m_splitCameras[i].gameObject.SetActive(false);
    }
}


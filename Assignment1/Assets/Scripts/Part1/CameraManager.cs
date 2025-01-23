using Cinemachine;
using TMPro;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Instructions")]
    [SerializeField] private float m_InstructionsInterval;
    [SerializeField] private bool m_ShowInstructionsAtStart = true;
    [SerializeField] private TextMeshProUGUI m_InstructionsText;
    private bool m_IsInstructionsShown = false;
    private float m_CurrInstructionsInterval = 0;

    [Header("Virtual Cameras")]
    [SerializeField] private CinemachineVirtualCamera[] m_Cameras;
    private int m_CurrCameraIndex;
    
    private void Start()
    {
        // set up cameras
        m_CurrCameraIndex = 0;
        for (int i = 0; i < m_Cameras.Length; i++)
        {
            m_Cameras[i].enabled = i == m_CurrCameraIndex;
        }

        // set up instructions text
        ToggleInstructionsText(m_ShowInstructionsAtStart);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleInstructionsText(false);
            m_CurrInstructionsInterval = 0;

            ToggleNextCamera();
        }
        
        if (!m_IsInstructionsShown)
        {
            m_CurrInstructionsInterval += Time.deltaTime;
            if (m_CurrInstructionsInterval >= m_InstructionsInterval)
            {
                m_CurrInstructionsInterval = 0;
                ToggleInstructionsText(true);
            }
        }
    }

    #region Instructions Text
    private void ToggleInstructionsText(bool isActive)
    {
        m_IsInstructionsShown = isActive;
        m_InstructionsText.gameObject.SetActive(m_IsInstructionsShown);
    }
    #endregion

    #region Camera
    private void ToggleNextCamera()
    {
        int nextIndex = (m_CurrCameraIndex + 1) % m_Cameras.Length;
        m_Cameras[nextIndex].enabled = true;
        m_Cameras[m_CurrCameraIndex].enabled = false;
        m_CurrCameraIndex = nextIndex;
    }
    #endregion
}

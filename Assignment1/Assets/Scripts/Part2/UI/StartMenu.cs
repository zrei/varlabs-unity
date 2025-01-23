using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Button m_StartButton;

    private void Start()
    {
        m_StartButton.onClick.AddListener(StartGame);
    }

    private void OnDestroy()
    {
        m_StartButton.onClick.RemoveAllListeners();
    }

    private void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

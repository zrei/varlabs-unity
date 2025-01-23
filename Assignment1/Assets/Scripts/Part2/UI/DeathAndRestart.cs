using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class DeathAndRestart : MonoBehaviour
{
    [SerializeField] private CanvasGroup m_cg;
    [SerializeField] private Button m_RestartButton;

    private AudioSource m_GameOverSFX;

    private void Start()
    {
        ToggleCanvasGroup(false);
        m_RestartButton.onClick.AddListener(Restart);
        GlobalEvents.PlayerDeathEvent += OnPlayerDeath;
        m_GameOverSFX = GetComponent<AudioSource>();
    }

    private void OnDestroy()
    {
        m_RestartButton.onClick.RemoveAllListeners();
        GlobalEvents.PlayerDeathEvent -= OnPlayerDeath;
    }

    #region Button Listeners
    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion

    #region Events
    private void OnPlayerDeath()
    {
        ToggleCanvasGroup(true);
        m_GameOverSFX.Play();
    }
    #endregion

    private void ToggleCanvasGroup(bool isEnabled)
    {
        m_cg.alpha = isEnabled ? 1f : 0f;
        m_cg.blocksRaycasts = isEnabled;
        m_cg.interactable = isEnabled;
    }
}

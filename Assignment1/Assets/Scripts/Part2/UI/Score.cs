using System.Collections;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI m_ScoreText;

    [Header("High Score")]

    [SerializeField] private int m_HighScoreIntervals = 10;
    [SerializeField] private float m_FlashIntervals = 0.5f;
    [Tooltip("Times to flash from white to red back to white")]
    [SerializeField] private int m_TotalFlashTimes = 3;
    [SerializeField] private AudioSource m_HighScoreAudio;
    [SerializeField] private Color m_NormalColor = Color.white;
    [SerializeField] private Color m_FlashColor = Color.red;

    private int m_Score;
    private int ScoreValue
    {
        get
        {
            return m_Score;
        }
        set
        {
            m_Score = value;
            SetScoreText();
            CheckForHighScore();
        }
    }

    private bool IsHighScore => ScoreValue > 0 && ScoreValue % m_HighScoreIntervals == 0;

    private Coroutine m_CurrentlyPlayingCoroutine = null;

    private const string SCORE_FORMAT = "Score: {0}";

    private void Start()
    {
        ScoreValue = 0;
    }

    private void Awake()
    {
        GlobalEvents.ScoreEvent += OnScore;
        GlobalEvents.PlayerDeathEvent += OnPlayerDeath;
    }

    private void OnDestroy()
    {
        GlobalEvents.ScoreEvent -= OnScore;
        GlobalEvents.PlayerDeathEvent -= OnPlayerDeath;
    }

    #region Events
    private void OnScore(FruitCollectible _)
    {
        ++ScoreValue;
    }

    private void OnPlayerDeath()
    {
        if (m_CurrentlyPlayingCoroutine != null) {
            StopCoroutine(m_CurrentlyPlayingCoroutine);
        }
        m_ScoreText.color = m_NormalColor;
        m_HighScoreAudio.Stop();
    }
    #endregion

    private void SetScoreText()
    {
        m_ScoreText.text = string.Format(SCORE_FORMAT, m_Score);
    }

    #region High Score
    private void CheckForHighScore()
    {
        if (IsHighScore)
        {
            m_HighScoreAudio.Play();
            if (m_CurrentlyPlayingCoroutine != null)
                StopCoroutine(m_CurrentlyPlayingCoroutine);
            m_CurrentlyPlayingCoroutine = StartCoroutine(FlashRedCoroutine());
        }
    }

    private IEnumerator FlashRedCoroutine()
    {
        m_ScoreText.color = m_NormalColor;
        float t = 0f;
        for (int i = 0; i < m_TotalFlashTimes; ++i)
        {
            while (t < m_FlashIntervals)
            {
                yield return null;
                t += Time.deltaTime;
            }

            m_ScoreText.color = m_FlashColor;
            t = t % m_FlashIntervals;

            while (t < m_FlashIntervals)
            {
                yield return null;
                t += Time.deltaTime;

            }

            m_ScoreText.color = m_NormalColor;
            t = t % m_FlashIntervals;
        }
        m_ScoreText.color = m_NormalColor;
    }
    #endregion
}

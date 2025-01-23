using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BirdCryScript : MonoBehaviour
{
    [SerializeField] private float m_AudioSourceInterval = 3.0f;
    private float m_CurrAudioSourceInterval = 0f;

    private AudioSource m_BirdCry;

    private void Start()
    {
        m_BirdCry = GetComponent<AudioSource>();
    }

    private void Update()
    {
        m_CurrAudioSourceInterval += Time.deltaTime;
        if (m_CurrAudioSourceInterval >= m_AudioSourceInterval)
        {
            m_BirdCry.Play();
            m_CurrAudioSourceInterval = 0;
        }
    }
}

using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("Player Follow")]
    [SerializeField] private Player m_Player;

    [Header("Camera Shake")]
    [SerializeField] float m_ShakeAmount = 0.7f;
    [SerializeField] float m_DecreaseFactor = 1.0f;
    [SerializeField] AudioSource m_WallCollisionSound;

    private Vector3 m_Offset;
    private float m_InitialHeight;

    private bool m_PlayerAlive = true;

    private float m_CurrShakeAmount = 0f;

    private void Start()
    {
        m_Offset = transform.position - m_Player.transform.position;
        m_InitialHeight = transform.position.y;
        GlobalEvents.PlayerDeathEvent += OnPlayerDeath;
        GlobalEvents.WallCollisionEvent += OnPlayerCollision;
    }

    private void OnDestroy()
    {
        GlobalEvents.PlayerDeathEvent -= OnPlayerDeath;
        GlobalEvents.WallCollisionEvent -= OnPlayerCollision;
    }

    private void LateUpdate()
    {
        if (!m_PlayerAlive)
            return;

        Vector3 newPosition = m_Player.transform.position + m_Offset;
        newPosition.y = m_InitialHeight;

        if (m_CurrShakeAmount > 0)
        {
            Vector3 offset = Random.insideUnitSphere * m_CurrShakeAmount;
            m_CurrShakeAmount = Mathf.Max(0, m_CurrShakeAmount - Time.deltaTime * m_DecreaseFactor);
            newPosition += offset;
        }
        
        transform.position = newPosition;
    }

    #region Events
    private void OnPlayerDeath()
    {
        m_PlayerAlive = false;
    }

    private void OnPlayerCollision()
    {
        m_CurrShakeAmount = m_ShakeAmount;
        m_WallCollisionSound.Play();
    }
    #endregion
}

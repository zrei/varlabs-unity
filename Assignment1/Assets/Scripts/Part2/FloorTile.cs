using UnityEngine;

public class FloorTile : MonoBehaviour
{
    [SerializeField] private bool m_WillMove;

    [Header("Move Details")]
    [SerializeField] private AnimationCurve m_Curve;
    [Tooltip("Time to complete a full cycle")]
    [SerializeField] private float m_CycleTime;
    [SerializeField] private bool m_IsInitialDirectionUp;

    private float m_CurrTime = 0;
    private Vector3 m_InitialPosition;
    private Vector3 m_DirectionVector;

    private const int MOVE_AMOUNT = 10;

    private void Start()
    {
        m_InitialPosition = transform.position;
        m_DirectionVector = m_IsInitialDirectionUp ? Vector3.up : Vector3.down;
    }

    private void Update()
    {
        if (!m_WillMove)
            return;

        m_CurrTime = (m_CurrTime + Time.deltaTime) % m_CycleTime;
        transform.position = m_InitialPosition + m_Curve.Evaluate(m_CurrTime / m_CycleTime) * m_DirectionVector * MOVE_AMOUNT;
    }
}

using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BirdLoopBehaviour : MonoBehaviour
{
    [SerializeField] private string[] m_BehaviourTriggers;
    [Tooltip("Amount of time before triggering the next behaviour")]
    [SerializeField] private float m_StateChangeInterval = 10;
    private float m_CurrInterval = 0f;
    private int m_BehaviourIndex = 0;

    private Animator m_Animator;

    private void Start()
    {
        m_Animator = GetComponent<Animator>();    
    }

    private void Update()
    {
        m_CurrInterval += Time.deltaTime; 
        if (m_CurrInterval >= m_StateChangeInterval)
        {
            m_CurrInterval = 0;
            m_Animator.SetTrigger(m_BehaviourTriggers[m_BehaviourIndex]);
            m_BehaviourIndex = (m_BehaviourIndex + 1) % m_BehaviourTriggers.Length;
        }
    }
}

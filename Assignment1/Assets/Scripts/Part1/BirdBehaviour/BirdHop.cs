using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BirdHop : MonoBehaviour
{
    [SerializeField] private float m_HopInterval;
    private float m_CurrHopInterval = 0;

    private Animator m_Animator;

    private const int NO_HOP = 0;
    private const int HOP_LEFT = -2;
    private const int HOP_RIGHT = 2;
    private const string HOP_PARAM = "hop";
    private static readonly int HOP_PARAM_ID = Animator.StringToHash(HOP_PARAM);
    private bool m_HopLeft = true;

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        m_CurrHopInterval += Time.deltaTime;
        if (m_CurrHopInterval >= m_HopInterval)
        {
            m_Animator.SetInteger(HOP_PARAM_ID, m_HopLeft ? HOP_LEFT : HOP_RIGHT);
            m_HopLeft = !m_HopLeft;
            m_CurrHopInterval = 0;
        }
    }

    public void ResetHopInt()
    {
        m_Animator.SetInteger(HOP_PARAM_ID, NO_HOP);
    }
}

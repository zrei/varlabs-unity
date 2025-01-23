using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BirdFlyScript : MonoBehaviour
{
    [SerializeField] private Transform m_FlyAroundPoint;
    [SerializeField] private float m_FlySpeed;
    [SerializeField] private float m_DistanceFromPoint = 5;

    private Animator m_Animator;
    private const string FLYING_ANIM_PARAM = "flying";
    private static readonly int FLYING_ANIM_PARAM_ID = Animator.StringToHash(FLYING_ANIM_PARAM);
    private const float ADDITIONAL_Y_ROTATION = -90;

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Animator.SetBool(FLYING_ANIM_PARAM_ID, true);

        // set up transform
        transform.position = m_FlyAroundPoint.position + new Vector3(0, 0, m_DistanceFromPoint);
        transform.LookAt(m_FlyAroundPoint);
        transform.Rotate(new Vector3(0, ADDITIONAL_Y_ROTATION, 0));
    }

    private void Update()
    {
        transform.RotateAround(m_FlyAroundPoint.position, Vector3.up, m_FlySpeed * Time.deltaTime);
    }
}

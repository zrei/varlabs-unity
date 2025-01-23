using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
    [Header("Control")]
    [SerializeField] private float m_Speed;

    [Header("Fall Death")]
    [SerializeField] private float m_MaximumFallDepth;
    private float m_InitialDepth;

    [Header("Wall Collision")]
    [SerializeField] private float m_MinWallCollisionSpeed = 2.0f;
    [SerializeField] private LayerMask m_WallLayerMask;

    private AudioSource m_CollectSFX;
    private Rigidbody m_Rb;

    private void Start()
    {
        m_InitialDepth = transform.position.y;
        m_Rb = GetComponent<Rigidbody>();
        m_CollectSFX = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (transform.position.y < m_InitialDepth - m_MaximumFallDepth)
        {
            GlobalEvents.PlayerDeathEvent?.Invoke();
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        m_Rb.AddForce(movement * m_Speed);
    }

    #region Triggers and Colliders
    private void OnTriggerEnter(Collider other)
    {
        FruitCollectible fruit = other.gameObject.GetComponent<FruitCollectible>();
        fruit.OnScore();
        GlobalEvents.ScoreEvent?.Invoke(fruit);
        m_CollectSFX.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // is wall and has high enough speed
        if ((m_WallLayerMask.value & 1<<collision.gameObject.layer) != 0 && m_Rb.velocity.magnitude > m_MinWallCollisionSpeed)
        {
            GlobalEvents.WallCollisionEvent?.Invoke();
        }
    }
    #endregion
}

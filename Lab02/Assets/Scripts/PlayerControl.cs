using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float m_Speed;

    #region Components
    private Rigidbody m_Rb;
    #endregion

    private void Start()
    {
        m_Rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        m_Rb.AddForce(movement * m_Speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
        }
    }
}

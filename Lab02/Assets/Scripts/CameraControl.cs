using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private PlayerControl m_Player;
    private Vector3 m_Offset;

    // Start is called before the first frame update
    private void Start()
    {
        m_Offset = transform.position - m_Player.transform.position;
    }

    private void LateUpdate()
    {
        transform.position = m_Player.transform.position + m_Offset;
    }
}

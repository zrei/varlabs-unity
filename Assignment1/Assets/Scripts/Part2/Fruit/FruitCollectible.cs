using UnityEngine;

public class FruitCollectible : MonoBehaviour
{
    [SerializeField] private GameObject[] m_Models;
    [SerializeField] private float m_RotationSpeed;
    [SerializeField] private ParticleSystem m_Particle;

    private Vector3 m_RotateDirection;

    private void Start()
    {
        m_RotateDirection = new Vector3(0, Random.value > 0.5 ? 1 : -1, 0);

        // instantiate a model by randomly selecting one
        GameObject model = Instantiate(m_Models[Random.Range(0, m_Models.Length - 1)], Vector3.zero, Quaternion.identity, transform);
        model.transform.localPosition = Vector3.zero;
        model.transform.localRotation = Quaternion.identity;
    }

    private void Update()
    {
        transform.Rotate(m_RotateDirection * m_RotationSpeed * Time.deltaTime);
    }

    public void OnScore()
    {
       Instantiate(m_Particle, transform.position, Quaternion.identity);
    }
}
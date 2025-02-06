using UnityEngine;
using UnityEngine.InputSystem;

public class ActivateMap : MonoBehaviour
{
    [SerializeField] private InputActionAsset m_Map;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Map.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

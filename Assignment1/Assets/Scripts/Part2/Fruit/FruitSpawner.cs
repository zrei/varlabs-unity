using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private FruitPool m_Pool;

    [Header("Fruit Spawn")]
    [SerializeField] private int m_AmountToSpawn;
    [Tooltip("Radius around the spawner to spawn fruits")]
    [SerializeField] private float m_RadiusRestriction;
    [Tooltip("Radius around each existing fruit that should not contain other fruits")]
    [SerializeField] private float m_FruitRadius;

    private HashSet<Transform> m_TrackedTransforms = new();

    private void Start()
    {
        for (int i = 0; i < m_AmountToSpawn; ++i)
        {
            SpawnFruit();
        }
    }

    private void Awake()
    {
        GlobalEvents.ScoreEvent += OnScore;
    }

    private void OnDestroy()
    {
        GlobalEvents.ScoreEvent -= OnScore;
    }

    private void SpawnFruit()
    {
        Vector3 spawnPos = GetRandomPosition();
        while (HasOverlappingFruit(spawnPos))
        {
            spawnPos = GetRandomPosition();
        }
        FruitCollectible fruit = m_Pool.GetObject<FruitCollectible>(true);
        fruit.transform.position = spawnPos;
        m_TrackedTransforms.Add(fruit.transform);
    }

    #region Events
    private void OnScore(FruitCollectible fruit)
    {
        m_TrackedTransforms.Remove(fruit.transform);
        m_Pool.ReturnObjectToPool(fruit.gameObject);
        SpawnFruit();
    }
    #endregion

    #region Helper
    private Vector3 GetRandomPosition()
    {
        float x = (Random.value * 2 - 1) * m_RadiusRestriction;
        float y = 0;
        float z = (Random.value * 2 - 1) * m_RadiusRestriction;
        return transform.position + new Vector3(x, y, z);
    }

    private bool HasOverlappingFruit(Vector3 pos)
    {
        foreach (Transform fruitTransform in m_TrackedTransforms)
        {
            if (IsOverlapping(pos, fruitTransform))
                return true;
        }
        return false;
    }

    private bool IsOverlapping(Vector3 pos, Transform fruitTransform)
    {
        return (pos - fruitTransform.position).magnitude <= m_FruitRadius;
    }
    #endregion
}

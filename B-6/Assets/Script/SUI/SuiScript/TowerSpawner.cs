using UnityEngine;
using System.Collections;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject sozaiPrefab;

    // Tower‚©‚ç‚Ç‚Ì‚­‚ç‚¢‚Ì”ÍˆÍ‚Éo‚·‚©
    [SerializeField] private float spawnRange = 5f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            // Tower‚ÌüˆÍ‚Ìƒ‰ƒ“ƒ_ƒ€‚ÈˆÊ’u
            Vector3 randomPos = transform.position + new Vector3(
                Random.Range(-spawnRange, spawnRange),
                0f,
                Random.Range(-spawnRange, spawnRange)
            );

            Instantiate(sozaiPrefab, randomPos, Quaternion.identity);
        }
    }
}
using UnityEngine;

public class WallMaterialSpawner : MonoBehaviour
{
    [Header("ëfçﬁPrefab")]
    [SerializeField] private GameObject materialPrefab;


    [Header("èoåªîÕàÕ")]
    [SerializeField] private Vector2 minPos = new Vector2(-12, -9);
    [SerializeField] private Vector2 maxPos = new Vector2(14, 9);


    [Header("íÜêSÇ©ÇÁÇÃã÷é~îÕàÕ")]
    [SerializeField] private Vector2 center = Vector2.zero;
    [SerializeField] private float noSpawnRadius = 5f;


    [Header("ê›íË")]
    [SerializeField] private float spawnCoolTime = 4f;
    [SerializeField] private int maxMaterialCount = 10;


    private float timer;


    void Start()
    {
        for (int i = 0; i < maxMaterialCount; i++)
        {
            SpawnMaterial();
        }
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnCoolTime)
        {
            timer = 0f;

            if (GameObject.FindObjectsOfType<WallMaterial>().Length < maxMaterialCount)
            {
                SpawnMaterial();
            }
        }
    }


    void SpawnMaterial()
    {
        Vector2 spawnPos;

        int retry = 0;

        while (true)
        {
            spawnPos = new Vector2(
                Random.Range(minPos.x, maxPos.x),
                Random.Range(minPos.y, maxPos.y)
            );


            // íÜêSÇ©ÇÁîºåa5à»ì‡Ç»ÇÁÇ‚ÇËíºÇµ
            if (Vector2.Distance(spawnPos, center) < noSpawnRadius)
            {
                retry++;

                if (retry > 100)
                    return;

                continue;
            }

            break;
        }


        GameObject obj =
            Instantiate(materialPrefab, spawnPos, Quaternion.identity);

        obj.tag = "WallMaterial";
    }
}
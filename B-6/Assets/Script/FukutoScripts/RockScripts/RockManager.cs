using UnityEngine;

/// <summary>
/// ロックマネージャー
/// 
/// 落石を管理するスクリプト
/// </summary>
public class RockManager : MonoBehaviour
{
    #region Config
    private float spawnInterval = 20f;    // スポーンのインターバル
    [SerializeField] private const int maxSpawner = 4;
    #endregion

    #region State
    private float currentSpawnInterval = 0f;             // 現在のスポーンインターバル
    private int currentSpawnerID = -1; // 現在スポーンするスポナーのID
    private bool isPermissionSpawn = false; // スポーン許可を出す関数
    #endregion

    // スポーンIDを取得する関数
    public int GetSpawnerID() { return currentSpawnerID; }

    // スポーンIDを例外に設定する関数
    public void SetSpawnerID() {  currentSpawnerID = -1; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPermissionSpawn)
        {
            if (CompleteSpawnInterval())
            {
                DecideSpawnerID();
            }
        }
    }

    // インターバルを消化する関数
    private bool CompleteSpawnInterval()
    {
        if (currentSpawnInterval < spawnInterval)
        {
            currentSpawnInterval = currentSpawnInterval + Time.deltaTime;
            return false;
        }
        else
        {
            currentSpawnInterval = 0f;
            SetPermissionSpawn(true);
            return true;
        }
    }

    // スポーンIDを決める関数
    private void DecideSpawnerID()
    {
        currentSpawnerID = Random.Range(1,maxSpawner);
    }

    // スポーン許可を設定する関数
    public void SetPermissionSpawn(bool spawn)
    {
        isPermissionSpawn = spawn;
    }

    // スポーン許可を取得する関数
    public bool GetPermissionSpawn()
    {
         return isPermissionSpawn;
    }

    public int GetSpawnID()
    {
        return currentSpawnerID;
    }
}

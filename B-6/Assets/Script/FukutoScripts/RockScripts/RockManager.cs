using UnityEngine;

public class RockManager : MonoBehaviour
{
    #region Config
    private float spawnInterval = 10f;    // スポーンのインターバル
    private const int maxSpawner = 2;
    #endregion

    #region State
    private float currentSpawnInterval = 0f;             // 現在のスポーンインターバル
    private int currentSpawnerID = -1; // 現在スポーンするスポナーのID
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
        if(CompleteSpawnInterval())
        {
            DecideSpawnerID();
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
            return true;
        }
    }

    // スポーンIDを決める関数
    private void DecideSpawnerID()
    {
        currentSpawnerID = Random.Range(1,maxSpawner);
    }
}

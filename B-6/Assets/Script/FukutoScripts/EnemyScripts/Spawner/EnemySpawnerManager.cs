using UnityEngine;

/// <summary>
/// エネミースポナーマネージャー
/// 
/// 敵のスポーン管理を行うクラス
/// </summary>
public class EnemySpawnerManager : MonoBehaviour
{
    #region Config
    [SerializeField] private GameTimer gameTimer; // ゲームタイマー
    [SerializeField] private int waveInterval = 3; // ウェーブ間隔（分）
    [SerializeField] private GameObject Tower; // タワー
    #endregion

    #region State
    private int waveCount = 1; // ウェーブカウント
    private int previousWave = 0; // １フレーム処理前のウェーブ（敵を設定する為に使用する）
    private int[] spawnEnemiesID = null; // 出現する敵
    private int spawnBossID = 0; // 出現するboss
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetSpawnID();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeWave();
    }

    /// brief@ ウェーブを変更する関数
    private void ChangeWave()
    {
        if (waveInterval * 2 <= gameTimer.GetGameTimeM())
        {
            waveCount = 3;
            SetSpawnID();
        }
        else if (waveInterval <= gameTimer.GetGameTimeM())
        {
            waveCount = 2;
            SetSpawnID();
        }
        
    }

    /// brief@ スポーンする敵のIDをセットする関数
    private void SetSpawnID()
    {
        if (previousWave < waveCount)
        {
            switch (waveCount)
            {
                case 1:
                    spawnEnemiesID = new int[3];
                    for (int i = 0; i < 3; i++)
                    {
                        spawnEnemiesID[i] = i + 1;
                    }
                    previousWave = waveCount; // ウェーブを設定
                    break;
                case 2:
                    spawnEnemiesID = new int[3];
                    for (int i = 0; i < 3; i++)
                    {
                        spawnEnemiesID[i] = i + 4;
                    }

                    spawnBossID = 9;

                    previousWave = waveCount; // ウェーブを設定
                    break;
                case 3:
                    spawnEnemiesID = new int[8];
                    for (int i = 0; i < 8; i++)
                    {
                        spawnEnemiesID[i] = i + 1;
                    }
                    spawnBossID = 10;

                    previousWave = waveCount; // ウェーブを設定
                    break;
            }
        }
    }

    /// brief@ スポーンする敵のIDを取得する関数
    public int[] GetSpawnEnemies() { return spawnEnemiesID; }

    /// brief@ スポーンするボスのIDを取得する関数
    public int GetSpawnBoss() { return spawnBossID; }

    /// brief@ タワーのオブジェクトを取得する関数
    public GameObject GetTower() { return Tower; }
}

using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// エネミースポナーマネージャー
/// 
/// 敵のスポーン管理を行うクラス
/// </summary>
public class EnemySpawnerManager : MonoBehaviour
{
    /// <summary>
    /// 敵のID
    /// </summary>
    private enum EnemyID
    { 
        MID_BOSS_FIRST = 9,
        MID_BOSS_SECOND = 10,
        BOSS = 11
    }

    /// <summary>
    /// ウェーブ
    /// </summary>
    private enum GameWave
    {
        FIRST = 1,
        SECOND,
        THIRD
    }

    #region Config
    [SerializeField] private GameTimer gameTimer; // ゲームタイマー
    [SerializeField] private int waveInterval = 3; // ウェーブ間隔（分）
    [SerializeField] private GameObject Tower; // タワー

    [SerializeField] private BgmManager bgmManager; // BGMマネージャー

    [SerializeField] private WaveCycle waveCycle;

    [SerializeField] private int upWaveID = 3; // ウェーブ間で繰り上げるID

    [SerializeField] private DamageTextManager damageTextManager;
    #endregion

    #region State
    private int waveCount = 1; // ウェーブカウント
    private int previousWave = 0; // １フレーム処理前のウェーブ（敵を設定する為に使用する）
    private const int lastWaveEnemies = 8; // 最後のウェーブ時の敵の数
    private const int otherWaveEnemies = 3; // その他のウェーブの敵の数

    private int[] spawnEnemiesID = null; // 出現する敵
    private int baseUpID = 1; // 基本的なIDの繰り上げ値
    private int spawnBossID = 0; // 出現するboss
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waveInterval = (int)waveCycle.waveCycleMin;
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
        if (waveInterval * (int)GameWave.SECOND <= gameTimer.GetGameTimeM())
        {
            waveCount = (int)GameWave.THIRD;
            SetSpawnID();
        }
        else if (waveInterval <= gameTimer.GetGameTimeM())
        {
            waveCount = (int)GameWave.SECOND;
            SetSpawnID();
        }     
        
        if(Keyboard.current.tKey.isPressed)
        {
            // idに変換するためキャストする
            int randomBoss = Random.Range((int)EnemyID.MID_BOSS_FIRST, (int)EnemyID.MID_BOSS_SECOND + baseUpID);
            Debug.Log("生成ボス：" + randomBoss);
            spawnBossID = randomBoss;
        }
    }

    /// brief@ スポーンする敵のIDをセットする関数
    private void SetSpawnID()
    {
        if (previousWave < waveCount)
        {
            switch (waveCount)
            {
                case (int)GameWave.FIRST:
                    spawnEnemiesID = new int[otherWaveEnemies];
                    for (int i = 0; i < otherWaveEnemies; i++)
                    {
                        int enemyID = i + baseUpID;
                        spawnEnemiesID[i] = enemyID; 
                    }
                    previousWave = waveCount; // ウェーブを設定
                    break;

                case (int)GameWave.SECOND:
                    spawnEnemiesID = new int[otherWaveEnemies];
                    for (int i = 0; i < otherWaveEnemies; i++)
                    {
                        int enemyID = i + baseUpID + upWaveID;
                        spawnEnemiesID[i] = enemyID;
                    }

                    // idに変換するためキャストする
                    int randomBoss = Random.Range((int)EnemyID.MID_BOSS_FIRST, (int)EnemyID.MID_BOSS_SECOND);
                    Debug.Log("生成ボス："　+ randomBoss);
                    spawnBossID =  randomBoss;

                    previousWave = waveCount; // ウェーブを設定

                    // 中ボスBGMを再生
                    bgmManager.SetMidbossBgm();
                    break;

                case (int)GameWave.THIRD:
                    spawnEnemiesID = new int[lastWaveEnemies];
                    for (int i = 0; i < lastWaveEnemies; i++)
                    {
                        spawnEnemiesID[i] = i + baseUpID;
                    }

                    // idに変換するためキャストする
                    EnemyID boss = EnemyID.BOSS;
                    spawnBossID = (int)boss;

                    previousWave = waveCount; // ウェーブを設定

                    // 最終BGMを再生
                    bgmManager.SetFinalBgm();
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

    public DamageTextManager GetDamageText() { return damageTextManager; }
}

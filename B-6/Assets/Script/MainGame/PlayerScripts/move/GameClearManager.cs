using UnityEngine;
using System.Collections;

public class GameClearManager : MonoBehaviour
{
    // ゲームを起動している間だけ保持
    private static int clearCount = 0;

    // クリア済みかどうか
    private bool isCleared = false;

    // コルーチンの重複開始を防ぐ
    private bool isWaitingForClear = false;

    private int wayOfReleaseCount = 3; // ４キャラ目解放までのカウント

    [SerializeField] private GameManager gameManager;
    [SerializeField] private int targetBossID = 11;
    [SerializeField] private SelectCharacterID selectCharacterID;

    private void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindFirstObjectByType<GameManager>();
        }

        if (selectCharacterID == null)
        {
            Debug.LogError("SelectCharacterIDのアセットが設定されていません！");
        }
    }

    private void Update()
    {
        // すでにクリア処理中なら監視しない
        if (isCleared || isWaitingForClear)
        {
            return;
        }

        // シーン内の敵を取得
        EnemyHealth[] enemies = FindObjectsByType<EnemyHealth>(
            FindObjectsSortMode.None
        );

        foreach (EnemyHealth enemy in enemies)
        {
            if (enemy == null)
            {
                continue;
            }

            // 対象のボスIDか確認
            if (enemy.GetEnemyID() != targetBossID)
            {
                continue;
            }

            // ボスの死亡を確認
            if (enemy.IsDead())
            {
                Debug.Log("Enemy ID 11 のボス死亡を検知しました");

                isWaitingForClear = true;
                StartCoroutine(WaitAndGameClear());
                return;
            }
        }
    }

    /// <summary>
    /// ボス撃破後、2秒待ってクリアする
    /// </summary>
    public IEnumerator WaitAndGameClear()
    {
        Debug.Log("ボス撃破後、2秒待機します");

        yield return new WaitForSecondsRealtime(2f);

        Debug.Log("2秒経過。GameClear()を呼び出します");

        GameClear();
    }

    /// <summary>
    /// ゲームクリア処理
    /// </summary>
    public void GameClear()
    {
        Debug.Log("GameClear()が呼ばれました");

        if (isCleared)
        {
            return;
        }

        isCleared = true;

        if (selectCharacterID == null)
        {
            Debug.LogWarning("SelectCharacterIDが見つかりません");
        }
        else
        {
            switch (selectCharacterID.id)
            {
                case 1:
                    Debug.Log("1キャラ目クリア");
                    clearCount++;
                    break;

                case 2:
                    Debug.Log("2キャラ目クリア");
                    clearCount++;
                    break;

                case 3:
                    Debug.Log("3キャラ目クリア");
                    clearCount++;
                    break;

                default:
                    Debug.Log($"{clearCount}回目クリア");
                    break;
            }

            // 3回クリアで4キャラ目解放
            if (wayOfReleaseCount <= clearCount)
            {
                selectCharacterID.canSelected = true;
            }
        }

        Debug.Log($"ゲームクリア！ クリア回数 : {clearCount}");

        if (gameManager == null)
        {
            gameManager = FindFirstObjectByType<GameManager>();
        }

        if (gameManager == null)
        {
            Debug.LogError("GameManagerが見つかりません");
            return;
        }

        Debug.Log("GameclearSceneへの遷移を開始");

        gameManager.ChangeGameClear();
    }

    /// <summary>
    /// 現在のクリア回数を取得
    /// </summary>
    public static int GetClearCount()
    {
        return clearCount;
    }
}
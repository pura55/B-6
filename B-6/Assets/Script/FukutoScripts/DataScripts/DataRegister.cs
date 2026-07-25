using UnityEngine;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// データレジスター
/// 
/// マスターデータから進捗データへ、データの格納を行うクラス
/// </summary>
public class DataRegister : MonoBehaviour
{
    [SerializeField] private PlayerMasterData playerMasterData; // プレイヤーのマスター
    [SerializeField] private PlayerProgressData playerProgressData; // プレイヤーの進捗
    [SerializeField] private EnemyMasterData enemyMasterData; // エネミーマスターデータ
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    /// @brief プレイヤーのデータを登録する関数
    public void RegistPlayerData(int id, PlayerMasterData data)
    {
        // 中身を一度からにする
        playerMasterData.players.Clear();

        // マスターデータの骨格に実体を入れる
        foreach (var entity in data.players)
        {
            // 実体を作る
            PlayerMasterData.Entity masterEntity = new PlayerMasterData.Entity();
            masterEntity.id = entity.id;
            masterEntity.atkDmg = entity.atkDmg;
            masterEntity.atkCT = entity.atkCT;
            masterEntity.skillDmg = entity.skillDmg;
            masterEntity.skillCT = entity.skillCT;
            masterEntity.speed = entity.speed;

            // 実体を入れる
            playerMasterData.players.Add(masterEntity);
        }

        // idが0のより大きい時
        if (id > 0)
        {
            // リストのIDを検索してその結果の実体を返す
            PlayerMasterData.Entity result = playerMasterData.players.Find(result => result.id == id);

            // データの実体をコピーする
            playerProgressData.CopyMasterData(result);
        }
        else
        {
            Debug.LogError($"idが正しくありません");
        }
    }

    public void RegistEnemyData(int id, EnemyMasterData data)
    {
        // 中身を一度からにする
        playerMasterData.players.Clear();

        // マスターデータの骨格に実体を入れる
        foreach (var entity in data.enemies)
        {
            // 実体を作る
            EnemyMasterData.Entity masterEntity = new EnemyMasterData.Entity();
            masterEntity.id = entity.id;
            masterEntity.atkDmg = entity.atkDmg;
            masterEntity.atkCT = entity.atkCT;
            masterEntity.skillDmg = entity.skillDmg;
            masterEntity.skillCT = entity.skillCT;
            masterEntity.speed = entity.speed;

            // 実体を入れる
            enemyMasterData.enemies.Add(masterEntity);
        }

        // idが0のより大きい時
        if (id > 0)
        {
            // リストのIDを検索してその結果の実体を返す
            EnemyMasterData.Entity result = enemyMasterData.enemies.Find(result => result.id == id);

            // データの実体をコピーする
        }
        else
        {
            Debug.LogError($"idが正しくありません");
        }
    }
}

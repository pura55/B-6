using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤープログレスデータ
/// 
/// プレイヤーの進捗データを格納するクラス
/// </summary>
[CreateAssetMenu(menuName = "Enemy_Scriptable")]
public class EnemyProgressData : ScriptableObject
{
    /// <summary>
    /// エンティティ
    /// 
    /// エネミーの進捗データの実体
    /// </summary>
    [Serializable]
    public class Entity : CharacterEntityBase
    {
    }

    // エネミーのデータをリスト化する変数
    public List<Entity> enemies;

    /// @brief マスターデータをコピーする関数
    public void CopyMasterData(EnemyMasterData data)
    {
        // リストを初期化
        enemies = new List<Entity>();

        foreach (var entity in data.enemies)
        {
            Debug.Log($"敵データコピー中");

            // 実体を作る
            Entity progressEntity = new Entity();
            progressEntity.id = entity.id;
            progressEntity.atkDmg = entity.atkDmg;
            progressEntity.atkCT = entity.atkCT;
            progressEntity.skillDmg = entity.skillDmg;
            progressEntity.skillCT = entity.skillCT;
            progressEntity.speed = entity.speed;

            // 実体を入れる
            enemies.Add(progressEntity);
        }
    }
}

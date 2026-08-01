using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エネミープログレスデータ
/// 
/// 敵の進捗データを格納するクラス
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

    /// @brief 整数型のステータスを取得する関数
    /// @param id: 敵のID
    /// @param statName: ステータス名
    public int GetIntStat(int id, string statName)
    {
        // 敵のデータからステータスを探す
        foreach(Entity entity in enemies) 
        {
            // IDが一致していない場合続行
            if (entity.id != id) continue;

            // IDが一致している場合
            // ステータス名に対応した値を返す
            switch (statName)
            {
                case "HP":
                    return entity.hp;
                case "ATK_DMG":
                    return entity.atkDmg;
                case "SKILL_DMG":
                    return entity.skillDmg;
                default:
                    Debug.LogError("存在しない名前で検索をかけているか、誤字の可能性があります！");
                    return 0; // ステータス名が存在しない場合0を返す
            }
        }

        Debug.LogError("存在しないIDで検索をかけています！");
        return 0; // 値が返されなかった場合0を返す
    }

    /// @brief 浮動小数点数型のステータスを取得する関数
    /// @param id: 敵のID
    /// @param statName: ステータス名
    public float GetFloatStat(int id, string statName)
    {
        // 敵のデータからステータスを探す
        foreach (Entity entity in enemies)
        {
            //Debug.Log("データ検索中");
            // IDが一致していない場合続行
            if (entity.id != id) continue;

            // IDが一致している場合
            // ステータス名に対応した値を返す
            switch (statName)
            {
                case "ATK_CT":
                    return entity.atkCT;
                case "SKILL_CT":
                    return entity.skillCT;
                case "SPEED":
                   // Debug.Log("スピードのデータを返します");
                    return entity.speed;
                default:
                    //Debug.LogError("存在しない名前で検索をかけているか、誤字の可能性があります！");
                    return 0; // ステータス名が存在しない場合0を返す
            }
        }

        Debug.LogError("存在しないIDで検索をかけています！");
        return 0; // 値が返されなかった場合0を返す
    }

    /// @brief マスターデータをコピーする関数
    public void CopyMasterData(EnemyMasterData data)
    {
        // リストを初期化
        enemies = new List<Entity>();

        foreach (var entity in data.enemies)
        {
            //Debug.Log($"敵データコピー中");

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

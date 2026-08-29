using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エネミースプライトデータ
/// 
/// 敵のスプライトデータを格納するクラス
/// </summary>
[CreateAssetMenu(menuName = "Enemy_Sprite_Scriptable")]
public class EnemySpriteData : ScriptableObject
{
    /// <summary>
    /// エンティティ
    /// 
    /// エネミーのスプライトデータの実体
    /// </summary>
    [Serializable]
    public class Entity : BaseSpriteData
    {
    }

    // エネミーのスプライトをリスト化する変数
    public List<Entity> enemiesSprites;

    /// @brief スプライトを取得する関数
    /// @param id: 敵のID
    /// @param spriteName: スプライト名
    public Sprite[] GetSprite(int id, string spriteName)
    {
       // Debug.Log("スプライトコピー中");

        // IDを要素数に変換
        int element = id - 1;

        // 要素数とスプライト名に対応した値を返す
        switch (spriteName)
        {
            case "IDLE": // 待機
                Debug.Log("idleのスプライトが取得されました");
                return enemiesSprites[element].idleSprite;
            case "ATTACK": // 攻撃
                return enemiesSprites[element].attackSprite;
            case "HIT": // 被ダメージ
                return enemiesSprites[element].hitSprite;
            case "DEATH": // 死亡
                return enemiesSprites[element].deathSprite;
            case "MOVE": // 移動
                return enemiesSprites[element].moveSprite;
            case "SKILL": // スキル
                return enemiesSprites[element].skillSprite;
            case "WEAPON": // 武器
                return enemiesSprites[element].weaponSprite;
            default:
                Debug.LogError("存在しない名前で検索をかけているか、誤字の可能性があります！");
                return enemiesSprites[element].skillSprite ; // ステータス名が存在しない場合idleスプライトを返す
        }
    }

    /// @brief マスターデータをコピーする関数
    public void CopyMasterData(EnemyMasterSprite data)
    {
        // リストを初期化
        enemiesSprites = new List<Entity>();

        foreach (var entity in data.enemiesSprites)
        {
            Debug.Log($"敵スプライトデータコピー中");

            // 実体を作る
            Entity spritesEntity = new Entity();
            spritesEntity.idleSprite = entity.idleSprite;
            spritesEntity.attackSprite = entity.attackSprite;
            spritesEntity.hitSprite = entity.hitSprite;
            spritesEntity.deathSprite = entity.deathSprite;
            spritesEntity.moveSprite = entity.moveSprite;
            spritesEntity.skillSprite = entity.skillSprite;
            spritesEntity.weaponSprite = entity.weaponSprite;

            // 実体を入れる
            enemiesSprites.Add(spritesEntity);
        }
    }
}

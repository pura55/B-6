using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤースプライトデータ
/// 
/// プレイヤーのスプライトデータを格納するクラス
/// </summary>
[CreateAssetMenu(menuName = "Player_Sprite_Scriptable")]
public class PlayetSpriteData : ScriptableObject
{
    /// <summary>
    /// エンティティ
    /// 
    /// プレイヤーのスプライトデータの実体
    /// </summary>
    [Serializable]
    public class Entity : BaseSpriteData
    {
    }

    // プレイヤーのスプライトをリスト化する変数
    public List<Entity> playerSprites;

    /// @brief スプライトを取得する関数
    /// @param id: プレイヤーのID
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
                return playerSprites[element].idleSprite;
            case "ATTACK": // 攻撃
                return playerSprites[element].attackSprite;
            case "HIT": // 被ダメージ
                return playerSprites[element].hitSprite;
            case "DEATH": // 死亡
                return playerSprites[element].deathSprite;
            case "MOVE": // 移動
                return playerSprites[element].moveSprite;
            case "SKILL": // スキル
                return playerSprites[element].skillSprite;
            default:
                Debug.LogError("存在しない名前で検索をかけているか、誤字の可能性があります！");
                return playerSprites[element].skillSprite; // ステータス名が存在しない場合idleスプライトを返す
        }
    }

    /// @brief マスターデータをコピーする関数
    public void CopyMasterData(PlayerMasterSprite data)
    {
        // リストを初期化
        playerSprites = new List<Entity>();

        foreach (var entity in data.playerSprites)
        {
            Debug.Log($"プレイヤースプライトデータコピー中");

            // 実体を作る
            Entity spritesEntity = new Entity();
            spritesEntity.idleSprite = entity.idleSprite;
            spritesEntity.attackSprite = entity.attackSprite;
            spritesEntity.hitSprite = entity.hitSprite;
            spritesEntity.deathSprite = entity.deathSprite;
            spritesEntity.moveSprite = entity.moveSprite;
            spritesEntity.skillSprite = entity.skillSprite;

            // 実体を入れる
            playerSprites.Add(spritesEntity);
        }
    }
}

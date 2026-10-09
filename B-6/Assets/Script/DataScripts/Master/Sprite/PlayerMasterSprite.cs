using System;
using System.Collections.Generic;

/// <summary>
/// プレイヤーマスタースプライト
/// 
/// プレイヤーのスプライトの骨格のクラス
/// </summary>
[Serializable]
public class PlayerMasterSprite
{
    /// <summary>
    /// エンティティ
    /// 
    /// エネミー1体のスプライトの実体
    /// </summary>
    [Serializable]
    public class Entity : BaseSpriteData
    {
    }

    // プレイヤーのスプライトをリスト化する変数
    public List<Entity> playerSprites;
}

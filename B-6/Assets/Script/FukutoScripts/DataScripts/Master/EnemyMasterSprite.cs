using System;
using System.Collections.Generic;

/// <summary>
/// エネミーマスタースプライト
/// 
/// 敵のスプライトの骨格のクラス
/// </summary>

public class EnemyMasterSprite
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

    // エネミーのスプライトをリスト化する変数
    public List<Entity> enemiesSprites;
}

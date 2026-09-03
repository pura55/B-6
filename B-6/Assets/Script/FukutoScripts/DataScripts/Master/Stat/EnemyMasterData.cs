using System;
using System.Collections.Generic;

/// <summary>
/// エネミーマスターデータ
/// 
/// マスターデータの骨格のクラス
/// </summary>
[Serializable]
public class EnemyMasterData
{
    /// <summary>
    /// エンティティ
    /// 
    /// エネミー1体のデータの実体
    /// </summary>
    [Serializable]
    public class Entity : CharacterEntityBase
    {
    }

    // エネミーのデータをリスト化する変数
    public List<Entity> enemies;
}

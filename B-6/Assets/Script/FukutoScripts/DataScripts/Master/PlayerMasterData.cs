using System;
using System.Collections.Generic;

/// <summary>
/// プレイヤーマスターデータ
/// 
/// マスターデータの骨格のクラス
/// </summary>
[Serializable]
public class PlayerMasterData
{
    /// <summary>
    /// プレイヤー1人分のデータの実体
    /// </summary>
    [Serializable]
    public class Entity : CharacterEntityBase
    {
    }

    // プレイヤーのデータをリスト化する変数
    public List<Entity> players;
}

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
        //public int id;         // ID番号
        //public int hp;         // 体力
        //public int atkDmg;     // 攻撃力
        //public float atkCT;    // 攻撃のクールタイム
        //public int skillDmg;   // スキルの攻撃力
        //public float skillCT;  // スキルのクールタイム
        //public int speed;      // 速さ
    }

    // プレイヤーのデータをリスト化する変数
    public List<Entity> players;
}

using UnityEngine;

/// <summary>
/// キャラクターエンティティベース
/// 
/// キャラクターのエンティティの継承元クラス
/// </summary>
public class CharacterEntityBase
{
    public int id;         // ID番号
    public int hp;         // 体力
    public int atkDmg;     // 攻撃力
    public float atkCT;    // 攻撃のクールタイム
    public int skillDmg;   // スキルの攻撃力
    public float skillCT;  // スキルのクールタイム
    public float speed;      // 速さ
}

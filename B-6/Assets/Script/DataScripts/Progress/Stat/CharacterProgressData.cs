using UnityEngine;

/// <summary>
/// キャラクタープログレスデータ
/// 
/// キャラクターの進捗データの継承元クラス
/// </summary>
public class CharacterProgressData : ScriptableObject
{
    public int id;         // ID番号
    public int hp;         // 体力
    public int atkDmg;     // 攻撃力
    public float atkCT;    // 攻撃のクールタイム
    public int skillDmg;   // スキルの攻撃力
    public float skillCT;  // スキルのクールタイム
    public float speed;    // スピード

    /// @brief マスターデータをコピーする関数
    public virtual void CopyMasterData(){}
}

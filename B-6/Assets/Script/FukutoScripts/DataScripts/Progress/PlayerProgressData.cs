using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤープログレスデータ
/// 
/// プレイヤーの進捗データを格納するクラス
/// </summary>
[CreateAssetMenu(menuName = "Player_Scriptable")]
public class PlayerProgressData : ScriptableObject
{
    public int id;         // ID番号
    public int hp;         // 体力
    public int atkDmg;     // 攻撃力
    public float atkCT;    // 攻撃のクールタイム
    public int skillDmg;   // スキルの攻撃力
    public float skillCT;  // スキルのクールタイム
    public float speed;      // スピード

    /// @brief マスターデータをコピーする関数
    public void CopyMasterData(PlayerMasterData.Entity entity)
    {
        this.id = entity.id;
        this.atkDmg = entity.atkDmg;
        this.atkCT = entity.atkCT;
        this.skillDmg = entity.skillDmg;
        this.skillCT = entity.skillCT;
        this.speed = entity.speed;
    }
}



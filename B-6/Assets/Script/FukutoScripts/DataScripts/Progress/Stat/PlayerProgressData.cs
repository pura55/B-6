using UnityEngine;

/// <summary>
/// プレイヤープログレスデータ
/// 
/// プレイヤーの進捗データを格納するクラス
/// </summary>
[CreateAssetMenu(menuName = "Player_Scriptable")]
public class PlayerProgressData : CharacterProgressData
{
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



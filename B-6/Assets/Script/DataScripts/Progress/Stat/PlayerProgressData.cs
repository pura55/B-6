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
        this.hp = entity.hp;
        this.atkDmg = entity.atkDmg;
        this.atkCT = entity.atkCT;
        this.skillDmg = entity.skillDmg;
        this.skillCT = entity.skillCT;
        this.speed = entity.speed;

        Debug.Log("id :" + entity.id);
        Debug.Log("hp :" + this.hp);
        Debug.Log("atkDmg :" + entity.atkDmg);
        Debug.Log("atkCT :" + entity.atkCT);
        Debug.Log("skillCT :" + entity.skillCT);
        Debug.Log("skillDmg :" + entity.skillDmg);
        Debug.Log("speed :" + entity.speed);
    }
}



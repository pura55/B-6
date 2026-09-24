using UnityEngine;

/// <summary>
/// セレクトキャラクターID
/// 
/// 選択したキャラクターのIDを保存するSO
/// </summary>
[CreateAssetMenu(menuName = "SelectID_Scriptable")]
public class SelectCharacterID : ScriptableObject
{
    public int id = 1;         // ID番号
    public bool canSelected = false; // 隠しキャラクターを選択可能かどうか

    /// @brief IDを取得する関数
    public int GetSelectID() { return id; }

    /// @brief IDを設定する関数
    public void SetSelectID(int select) { id = select; }
}

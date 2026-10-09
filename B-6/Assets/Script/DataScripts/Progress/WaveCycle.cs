using UnityEngine;

/// <summary>
/// ウェーブサイクル
/// 
/// ウェーブサイクルをデバックで管理するスクリプトです
/// </summary>
[CreateAssetMenu(menuName = "WaveCycle_Scriptable")]
public class WaveCycle : ScriptableObject
{
    public uint waveCycleMin = 3; // ウェーブ間の時間
}

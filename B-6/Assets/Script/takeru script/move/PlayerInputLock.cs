using UnityEngine;

/// <summary>
/// プレイヤーの入力を管理する
/// </summary>
public class PlayerInputLock : MonoBehaviour
{
    // 入力禁止中か
    public bool IsLocked { get; private set; } = false;


    /// <summary>
    /// 入力を禁止する
    /// </summary>
    public void LockInput()
    {
        IsLocked = true;

        Debug.Log("プレイヤー入力禁止");
    }


    /// <summary>
    /// 入力を許可する
    /// </summary>
    public void UnlockInput()
    {
        IsLocked = false;

        Debug.Log("プレイヤー入力許可");
    }
}

using UnityEngine;

/// <summary>
/// プレイヤーサイズ
/// 
/// プレイヤーのサイズを取得するクラス
/// </summary>
public class PlayerSize : BaseSize
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetObjectSize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 GetPlayerSize()
    {
        return spriteSize;
    }
}

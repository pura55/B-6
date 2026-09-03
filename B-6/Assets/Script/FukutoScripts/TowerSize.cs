using UnityEngine;

/// <summary>
/// タワーサイズ
/// 
/// タワーのサイズを取得するクラス
/// </summary>
public class TowerSize : BaseSize
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

    

    public Vector2 GetTowerSize()
    {
        return spriteSize;
    }
}

using UnityEngine;

/// <summary>
/// ベースサイズ
/// 
/// サイズを取得する基底クラス
/// </summary>
public abstract class BaseSize : MonoBehaviour
{
    #region State
    protected Vector2 spriteSize;
    #endregion

    // オブジェクトのサイズを取得する関数
    protected void GetObjectSize()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Vector3 size = spriteRenderer.bounds.size;
        spriteSize.x = size.x;
        spriteSize.y = size.y;
    }
}

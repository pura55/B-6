using UnityEngine;

/// <summary>
/// ブレイクロック
/// 
/// 落石を壊す処理を行います
/// </summary>
public class BrakeRock : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HitRock(collision);
    }

    /// @brief 落石に当たった際の処理を行う
    private void HitRock(Collision2D collision)
    {
        if(!collision.gameObject.CompareTag("Rock"))
        {
            return;
        }

        // 落石を破壊
        Destroy(collision.gameObject);

        Destroy(gameObject);
        return;
    }
}

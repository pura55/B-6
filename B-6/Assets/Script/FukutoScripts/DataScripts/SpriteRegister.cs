using UnityEngine;

/// <summary>
/// スプライトジスター
/// 
/// マスターデータから進捗データへ、スプライトの格納を行うクラス
/// </summary>
public class SpriteRegister : MonoBehaviour
{
    [SerializeField] private EnemySpriteData enemySpriteData; // エネミーのスプライトデータ（SO)

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void RegistEnemySprite(EnemyMasterSprite data)
    {
        Debug.Log("スプライトデータの登録");
        if (data != null)
        {
            // データの実体をコピーする
            enemySpriteData.CopyMasterData(data);
        }
        else
        {
            Debug.LogError($"敵のスプライトがありません");
        }
    }
}

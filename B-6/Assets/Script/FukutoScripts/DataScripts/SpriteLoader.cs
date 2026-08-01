using UnityEngine;

/// <summary>
/// スプライトローダー
/// 
/// スプライトを読み込むクラス
/// </summary>
public class SpriteLoader : MonoBehaviour
{
    //[SerializeField] private ;
    private string spriteBasePass = "Enemies/ID_"; // 基本テクスチャーパス
    private string spriteIdlePass = "Idle"; // 待機パス
    private string spriteAttackPass = "Attack_1"; // 攻撃パス
    private string spriteTakeHitPass = "Take_Hit"; // 被ダメージパス
    private string spriteDeathPass = "Death"; // 死亡パス
    private string spriteMovePass = "Move"; // 移動パス

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// @brief 敵のスプライトを読み込む関数
    public void LoadEnemySprites()
    {
        // マスターデータの実体を造りそこにデータを格納する
        EnemyMasterSprite data = new EnemyMasterSprite();

        for (int i = 0; i < 9; i++)
        {
            int id = i + 1;
            data.enemiesSprites[i].idleSprite = Resources.LoadAll<Sprite>(spriteBasePass + id + spriteIdlePass);
            data.enemiesSprites[i].attackSprite = Resources.LoadAll<Sprite>(spriteBasePass + id + spriteAttackPass);
            data.enemiesSprites[i].hitSprite = Resources.LoadAll<Sprite>(spriteBasePass + id + spriteTakeHitPass);
            data.enemiesSprites[i].deathSprite = Resources.LoadAll<Sprite>(spriteBasePass + id + spriteDeathPass);

            switch (id)
            {
                case 2:
                    LoadMoveSprites(data, i, id);
                    break;
                case 3:
                    LoadMoveSprites(data, i, id);
                    break;
                case 4:
                    LoadMoveSprites(data, i, id);
                    break;
                case 6:
                    LoadMoveSprites(data, i, id);
                    break;
                case 7:
                    LoadMoveSprites(data, i, id);
                    break;
                case 8:
                    LoadMoveSprites(data, i, id);
                    break;
            }
        }
    }

    /// @brief 移動のテクスチャーが存在する場合に読み込みを行う関数
    private void LoadMoveSprites(EnemyMasterSprite data, int element, int id)
    {
        data.enemiesSprites[element].moveSprite = Resources.LoadAll<Sprite>(spriteBasePass + id + spriteMovePass);
    }
}

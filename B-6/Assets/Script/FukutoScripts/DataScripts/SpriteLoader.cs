using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// スプライトローダー
/// 
/// スプライトを読み込むクラス
/// </summary>
public class SpriteLoader : MonoBehaviour
{
    private string spriteBasePass = "Enemies/ID_"; // 基本テクスチャーパス
    private string spriteIdlePass = "/Idle"; // 待機パス
    private string spriteAttackPass = "/Attack_1"; // 攻撃パス
    private string spriteTakeHitPass = "/Take_Hit"; // 被ダメージパス
    private string spriteDeathPass = "/Death"; // 死亡パス
    private string spriteMovePass = "/Move"; // 移動パス
    private string spriteSkillPass = "/Skill"; // スキルパス
    private string spriteWeaponPass = "/Weapon"; // 武器パス
    [SerializeField] private SpriteRegister spriteRegister;
    [SerializeField] private EnemyMasterSprite enemyMasterSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("スプライトローダーの初期化中");
        LoadEnemySprites();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// @brief 敵のスプライトを読み込む関数
    public void LoadEnemySprites()
    {
        // マスターデータの実体を造りそこにデータを格納する
        enemyMasterSprite = new EnemyMasterSprite();

        // リストの実体が作られていない場合実体を作る
        if (enemyMasterSprite.enemiesSprites == null)
        {
            enemyMasterSprite.enemiesSprites = new System.Collections.Generic.List<EnemyMasterSprite.Entity>();
        }

        for (int i = 0; i < 10; i++)
        {
            EnemyMasterSprite.Entity entity = new EnemyMasterSprite.Entity();

            int id = i + 1;
            // 全敵に共通するスプライトをロード
            entity.idleSprite = Resources.LoadAll<Sprite>(spriteBasePass + id + spriteIdlePass);
            entity.attackSprite = Resources.LoadAll<Sprite>(spriteBasePass + id + spriteAttackPass);
            entity.hitSprite = Resources.LoadAll<Sprite>(spriteBasePass + id + spriteTakeHitPass);
            entity.deathSprite = Resources.LoadAll<Sprite>(spriteBasePass + id + spriteDeathPass);
            

            // 特定の敵に関連するスプライトをロード
            switch (id)
            {
                case 2:
                    LoadMoveSprites(entity, id);
                    break;
                case 3:
                    LoadMoveSprites(entity, id);
                    break;
                case 4:
                    LoadMoveSprites(entity, id);
                    break;
                case 6:
                    LoadMoveSprites(entity, id);
                    break;
                case 7:
                    LoadMoveSprites(entity, id);
                    break;
                case 8:
                    LoadMoveSprites(entity, id);
                    break;
                case 9:
                    LoadMoveSprites(entity, id);
                    LoadSkillSprites(entity, id);
                    LoadWeaponSprite(entity, id);
                    break;
                case 10:
                    LoadMoveSprites(entity, id);
                    LoadSkillSprites(entity, id);
                    break;
            }

            // データを格納
            enemyMasterSprite.enemiesSprites.Add(entity);
        }

        Debug.Log("登録を開始");
        // データの登録
        spriteRegister.RegistEnemySprite(enemyMasterSprite);
    }

    /// @brief 移動のテクスチャーが存在する場合に読み込みを行う関数
    private void LoadMoveSprites(EnemyMasterSprite.Entity entity, int id)
    {
        entity.moveSprite = Resources.LoadAll<Sprite>(spriteBasePass + id + spriteMovePass);
    }

    /// @brief スキルのテクスチャーが存在する場合に読み込みを行う関数
    private void LoadSkillSprites(EnemyMasterSprite.Entity entity, int id)
    {
        entity.skillSprite = Resources.LoadAll<Sprite>(spriteBasePass + id + spriteSkillPass);
    }

    /// @brief 武器のテクスチャ―が存在する場合に読み込みを行う関数
    private void LoadWeaponSprite(EnemyMasterSprite.Entity entity, int id)
    {
        entity.weaponSprite = Resources.LoadAll<Sprite>(spriteBasePass + id + spriteWeaponPass);
    }
}

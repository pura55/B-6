using UnityEngine;
using UnityEngine.InputSystem;

public class CharaSkillManager : MonoBehaviour
{
    [SerializeField] private SkillID1 skillID1;
    [SerializeField] private SkillID2 skillID2;
    [SerializeField] private SkillID3 skillID3;
    [SerializeField] private SkillID4 skillID4;

    [Header("スキル発動エフェクト")]
    [SerializeField] private GameObject skillEffect1;
    [SerializeField] private GameObject skillEffect2;
    [SerializeField] private GameObject skillEffect3;
    [SerializeField] private GameObject skillEffect4;

    [SerializeField] private float effectLifeTime = 1f;

    [Header("DATA")]
    [SerializeField] private PlayerProgressData playerProgressData;

    private ID1Sprite playerAnimation;

    private int characterID = 1;


    void Start()
    {
        playerAnimation = GetComponent<ID1Sprite>();

        if (playerProgressData != null)
        {
            characterID = playerProgressData.id;
        }
        else
        {
            Debug.LogWarning(
                "PlayerProgressDataが設定されていません"
            );
        }
    }


    void Update()
    {
        if (Keyboard.current == null)
            return;

        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            UseSkill();
        }
    }


    void UseSkill()
    {
        bool skillUsed = false;
        GameObject effectPrefab = null;


        switch (characterID)
        {
            case 1:

                if (skillID1 != null)
                {
                    skillUsed = skillID1.UseSkill();
                    effectPrefab = skillEffect1;
                }

                break;


            case 2:

                if (skillID2 != null)
                {
                    skillUsed = skillID2.UseSkill();
                    effectPrefab = skillEffect2;
                }

                break;


            case 3:

                if (skillID3 != null)
                {
                    skillUsed = skillID3.UseSkill();
                    effectPrefab = skillEffect3;
                }

                break;


            case 4:

                if (skillID4 != null)
                {
                    skillUsed = skillID4.UseSkill();
                    effectPrefab = skillEffect4;
                }

                break;


            default:

                Debug.LogWarning(
                    $"キャラID {characterID} が正しくありません"
                );

                return;
        }


        // スキルが実際に発動できなかった
        if (!skillUsed)
            return;


        // スキルアニメーション
        if (playerAnimation != null)
        {
            playerAnimation.ChangeState(
                ID1Sprite.PlayerAnimState.Skill
            );
        }


        // 発動エフェクト
        PlaySkillEffect(effectPrefab);
    }


    void PlaySkillEffect(GameObject effectPrefab)
    {
        if (effectPrefab == null)
            return;

        GameObject effect = Instantiate(
            effectPrefab,
            transform.position,
            Quaternion.identity,
            transform
        );

        Destroy(
            effect,
            effectLifeTime
        );
    }
}
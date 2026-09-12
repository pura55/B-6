using UnityEngine;
using UnityEngine.InputSystem;

public class CharaSkillManager : MonoBehaviour
{
    [SerializeField] private SkillID1 skillID1;
    [SerializeField] private SkillID2 skillID2;
    [SerializeField] private SkillID3 skillID3;
    [SerializeField] private SkillID4 skillID4;

    private ID1Sprite playerAnimation;

    void Start()
    {
        playerAnimation = GetComponent<ID1Sprite>();
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
        playerAnimation.ChangeState(ID1Sprite.PlayerAnimState.Skill);

        switch (DebugCharacterSelect.selectedPlayerID)
        {
            case 1:
                skillID1.UseSkill();

                break;

            case 2:
                skillID2.UseSkill();
                break;

            case 3:
                skillID3.UseSkill();
                break;

            case 4:
                skillID4.UseSkill();
                break;

            default:
                Debug.LogWarning("ƒLƒƒƒ‰‚ª‘I‘ð‚³‚ê‚Ä‚¢‚Ü‚¹‚ñ");
                break;
        }
    }
}
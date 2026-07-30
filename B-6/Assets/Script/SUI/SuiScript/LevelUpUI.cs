using UnityEngine;

public class LevelUpUI : MonoBehaviour
{
    public SkillCard card1;
    public SkillCard card2;
    public SkillCard card3;

    private PlayerExp playerExp;

    // ƒŒƒxƒ‹ƒAƒbƒv‰æ–Ê‚ğŠJ‚­
    public void Open(PlayerExp exp)
    {
        playerExp = exp;

        gameObject.SetActive(true);

        // ƒQ[ƒ€’â~
        Time.timeScale = 0f;

        // ‰¼‚Ì3‘ğ
        card1.Setup(
            "UŒ‚+2",
            "UŒ‚—Í‚ª2‘‚¦‚é",
            this);

        card2.Setup(
            "UŒ‚+5",
            "UŒ‚—Í‚ª5‘‚¦‚é",
            this);

        card3.Setup(
            "UŒ‚+10",
            "UŒ‚—Í‚ª10‘‚¦‚é",
            this);
    }

    // ƒJ[ƒh‘I‘ğ
    public void SelectSkill(string skillName)
    {
        PlayerAttack2 playerAttack =
            playerExp.GetComponent<PlayerAttack2>();

        if (playerAttack != null)
        {
            if (skillName == "UŒ‚+2")
            {
                playerAttack.Attack += 2;
            }
            else if (skillName == "UŒ‚+5")
            {
                playerAttack.Attack += 5;
            }
            else if (skillName == "UŒ‚+10")
            {
                playerAttack.Attack += 10;
            }

            Debug.Log(
                skillName + " ‚ğæ“¾I Œ»İUŒ‚—Í : " +
                playerAttack.Attack);
        }

        // UI‚ğ•Â‚¶‚é
        gameObject.SetActive(false);

        // ƒQ[ƒ€ÄŠJ
        Time.timeScale = 1f;

        // PlayerExp‚Ö’Ê’m
        playerExp.FinishLevelUp();
    }
}
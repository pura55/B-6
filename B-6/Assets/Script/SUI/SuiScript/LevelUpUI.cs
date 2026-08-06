using UnityEngine;

public class LevelUpUI : MonoBehaviour
{
    public SkillCard card1;
    public SkillCard card2;
    public SkillCard card3;

    private PlayerExp playerExp;

    // 仮のスキル一覧
    private string[] skillNames =
    {
        "ファイア",
        "アイス",
        "サンダー",
        "ヒール",
        "ダーク",
        "シャイン"
    };

    // レベルアップ画面を開く
    public void Open(PlayerExp exp)
    {
        playerExp = exp;

        gameObject.SetActive(true);

        // ゲーム停止
        Time.timeScale = 0f;

        // ランダムで3つ選ぶ
        string s1 = skillNames[Random.Range(0, skillNames.Length)];
        string s2 = skillNames[Random.Range(0, skillNames.Length)];
        string s3 = skillNames[Random.Range(0, skillNames.Length)];

        card1.Setup(
            s1,
            s1 + "を習得する",
            this);

        card2.Setup(
            s2,
            s2 + "を習得する",
            this);

        card3.Setup(
            s3,
            s3 + "を習得する",
            this);
    }

    // カード選択
    public void SelectSkill(string skillName)
    {
        // 今は取得ログだけ
        Debug.Log(skillName + " を取得！");

        // UIを閉じる
        gameObject.SetActive(false);

        // ゲーム再開
        Time.timeScale = 1f;

        // PlayerExpへ通知
        playerExp.FinishLevelUp();
    }
}
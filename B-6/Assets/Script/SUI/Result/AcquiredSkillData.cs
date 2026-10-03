
using System.Collections.Generic;

public static class AcquiredSkillData
{
    // シーンを切り替えても保持する獲得スキル一覧
    public static List<SkillData> acquiredSkills =
        new List<SkillData>();

    // 新しいゲームを始めるときに呼び出す
    public static void Clear()
    {
        acquiredSkills.Clear();
    }

    // スキルを登録（重複登録を防ぐ）
    public static void Add(SkillData skill)
    {
        if (skill != null && !acquiredSkills.Contains(skill))
        {
            acquiredSkills.Add(skill);
        }
    }
}
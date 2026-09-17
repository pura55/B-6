using UnityEngine;

public class SkillStock : MonoBehaviour
{
    [Header("スキルストック")]
    [SerializeField] private int maxStock = 1;
    [SerializeField] public int amount = 1;

    [Header("1ストック回復する時間")]
    [SerializeField] private float coolTime = 3f;

    private int currentStock;
    private float coolTimer = 0f;

    void Start()
    {
        currentStock = maxStock;
    }

    void Update()
    {
        // 満タンならCTを回さない
        if (currentStock >= maxStock)
        {
            coolTimer = 0f;
            return;
        }

        // 裏でCTを進める
        coolTimer += Time.deltaTime;

        // CT終了
        if (coolTimer >= coolTime)
        {
            currentStock++;

            coolTimer = 0f;

            Debug.Log(
                $"<color=cyan>スキル回復</color> {currentStock}/{maxStock}"
            );
        }
    }

    // スキルを使えるか
    public bool CanUseSkill()
    {
        return currentStock > 0;
    }

    // スキルを1回使用
    public bool UseStock()
    {
        if (currentStock <= 0)
        {
            Debug.Log("<color=yellow>スキルストックがありません</color>");
            return false;
        }

        currentStock--;

        Debug.Log(
            $"スキルストック：{currentStock}/{maxStock}"
        );

        return true;
    }

    // 補助スキル取得時
    public void AddMaxStock()
    {
        maxStock += amount;

        // 取得した分はすぐ使えるようにする
        currentStock += amount;

        if (currentStock > maxStock)
        {
            currentStock = maxStock;
        }

        Debug.Log(
            $"<color=green>最大ストック増加！</color> {currentStock}/{maxStock}"
        );
    }

    public int GetCurrentStock()
    {
        return currentStock;
    }

    public int GetMaxStock()
    {
        return maxStock;
    }
}
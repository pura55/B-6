using UnityEngine;

public class SkillStock : MonoBehaviour
{
    [Header("スキルストック")]
    [SerializeField] private int maxStock = 1;
    [SerializeField] public int amount = 1;

    [Header("スキルクールタイム")]
    [SerializeField] private PlayerProgressData playerProgressData;

    private int currentStock;
    private float coolTimer = 0f;

    void Start()
    {
        currentStock = maxStock;

        // PlayerProgressDataを自動取得
        if (playerProgressData == null)
        {
            playerProgressData =
                GetComponentInParent<PlayerProgressData>();
        }

        if (playerProgressData == null)
        {
            playerProgressData =
                FindFirstObjectByType<PlayerProgressData>();
        }
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
        if (coolTimer >= GetCoolTime())
        {
            currentStock++;

            coolTimer = 0f;

            Debug.Log(
                $"<color=cyan>スキル回復</color> {currentStock}/{maxStock}"
            );
        }
    }

    // 現在のスキルクールタイム
    private float GetCoolTime()
    {
        if (playerProgressData != null)
        {
            return playerProgressData.skillCT;
        }

        return 3f;
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

    public float GetCoolTimeRate()
    {
        if (currentStock >= maxStock)
            return 0f;

        float coolTime = GetCoolTime();

        if (coolTime <= 0f)
            return 0f;

        return Mathf.Clamp01(
            1f - (coolTimer / coolTime)
        );
    }

    public bool IsCoolTime()
    {
        return currentStock < maxStock;
    }
}
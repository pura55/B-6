using UnityEngine;

public class ShortDistanceAttack : MonoBehaviour
{
    #region Config
    private int statAtk = 0; // 攻撃力
    private float statRng = 0f; // 範囲
    private float recastInterval = 1f; // 再攻撃インターバル
    #endregion

    #region State
    private GameObject attackToSpawn; // スポーンさせる攻撃のオブジェクト
    private float currentRecastInterval = 0f; // 現在の再攻撃インターバル
    private bool isAttacked = false; // 攻撃済みのフラグ
    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacked)
            CompleteInterval();
    }

    // インターバルを消費する関数
    private void CompleteInterval()
    {
        if(currentRecastInterval < recastInterval)
        {
            currentRecastInterval += Time.deltaTime;
        }
        else
        {
            currentRecastInterval = 0f;
            isAttacked = false;
            return;
        }
    }

    public void GenerateAttackObj()
    {
        GameObject spawnedAttack = Instantiate(attackToSpawn, transform.position, Quaternion.identity);
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TowerHealth : MonoBehaviour
{
    public int maxHp = 100;
    private int currentHp;

    public Slider hpSlider;      // 緑
    public Slider damageSlider;  // 赤

    private Coroutine damageCoroutine;

    void Start()
    {
        currentHp = maxHp;

        hpSlider.maxValue = maxHp;
        damageSlider.maxValue = maxHp;

        hpSlider.value = maxHp;
        damageSlider.value = maxHp;
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;

        if (currentHp < 0)
            currentHp = 0;

        // 緑はすぐ減る
        hpSlider.value = currentHp;

        // 以前のアニメーションを止める
        if (damageCoroutine != null)
            StopCoroutine(damageCoroutine);

        damageCoroutine = StartCoroutine(UpdateDamageBar());

        if (currentHp == 0)
            Debug.Log("ゲームオーバー！");
    }

    IEnumerator UpdateDamageBar()
    {
        // 1秒待つ
        yield return new WaitForSeconds(1f);

        // 赤バーを滑らかに減らす
        while (damageSlider.value > hpSlider.value)
        {
            damageSlider.value = Mathf.MoveTowards(
                damageSlider.value,
                hpSlider.value,
                50f * Time.deltaTime
            );

            yield return null;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(1);
            Debug.Log("ダメージを受けました");
        }
        if (other.CompareTag("Rock"))
        {
            TakeDamage(5);
            Debug.Log("ダメージを受けました");
        }
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TowerHealth : MonoBehaviour
{
    // 最大HP
    public int maxHp = 100;

    // 現在HP
    private int currentHp;

    // 緑のHPバー
    public Slider hpSlider;

    // 赤のダメージバー
    public Slider damageSlider;

    // 赤バーのアニメーション用
    private Coroutine damageCoroutine;

    // 初期化
    void Start()
    {
        currentHp = maxHp;

        // スライダーの最大値を設定
        hpSlider.maxValue = maxHp;
        damageSlider.maxValue = maxHp;

        // 初期値を最大HPにする
        hpSlider.value = maxHp;
        damageSlider.value = maxHp;
    }

    // ダメージ処理
    public void TakeDamage(int damage)
    {
        // HPを減らす
        currentHp -= damage;
        Debug.Log(damage + "ダメージ");

        // 0未満にならないようにする
        if (currentHp < 0)
            currentHp = 0;

        // 緑バーは即座に減らす
        hpSlider.value = currentHp;

        // 前のアニメーションを停止
        if (damageCoroutine != null)
            StopCoroutine(damageCoroutine);

        // 赤バーを遅れて減らす
        damageCoroutine = StartCoroutine(UpdateDamageBar());

        // HPが0になったら
        if (currentHp == 0)
        {
            Debug.Log("ゲームオーバー！");
        }
    }

    // 赤バーを遅れて減らす処理
    IEnumerator UpdateDamageBar()
    {
        // 1秒待機
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

    // 衝突した瞬間に呼ばれる
    void OnCollisionEnter2D(Collision2D collision)
    {
        //// 敵に触れた
        //if (collision.gameObject.CompareTag("Enemy"))
        //{
        //    TakeDamage(1);
        //    Debug.Log("Enemyから1ダメージ");
        //}

        // 岩に触れた
        if (collision.gameObject.CompareTag("Rock"))
        {
            TakeDamage(5);
            Debug.Log("Rockから5ダメージ");

            Destroy(collision.gameObject); // 岩を消す
        }
    }
}
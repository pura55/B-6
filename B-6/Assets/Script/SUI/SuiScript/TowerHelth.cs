using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    // GameOverの文字
    public GameObject gameOverText;

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

        // GameOverを最初は非表示
        gameOverText.SetActive(false);

        // ゲームの時間を通常に戻す
        Time.timeScale = 1f;
    }

    // ダメージ処理
    public void TakeDamage(int damage)
    {
        // HPを減らす
        currentHp -= damage;

        //Debug.Log(damage + "ダメージ");

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
            // 赤バーも即座に0にする
            damageSlider.value = 0;

            // 実行中の赤バーアニメーションを停止
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }

            // GameOverを表示
            gameOverText.SetActive(true);

            // ゲームを停止
            Time.timeScale = 0f;

            // 3秒後にタイトルへ
            StartCoroutine(GoToTitle());

            Debug.Log("ゲームオーバー！");
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
    }

    // 3秒後にタイトルへ移動
    IEnumerator GoToTitle()
    {
        // Time.timeScale = 0でも進む時間
        yield return new WaitForSecondsRealtime(3f);

        // 時間を元に戻す
        Time.timeScale = 1f;

        // ゲームオーバーシーンへ移動
        SceneManager.LoadScene("GameoverScene");
    }
}
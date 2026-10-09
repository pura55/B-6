using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TitleButton : MonoBehaviour
{
    [SerializeField] private SoundPlayer soundPlayer;
    [SerializeField] private GameManager gameManager;

    public void GoToTitle()
    {
        soundPlayer.PlayOneShot();
        StartCoroutine(LoadTitleScene());
    }

    private IEnumerator LoadTitleScene()
    {
        yield return new WaitForSeconds(0.5f);

        // リザルトに使用した情報をすべてリセット
        AcquiredSkillData.Clear();
        EnemyHealth.ResetPlayerKillCount();
        RockHit.ResetBlockedRockCount();
        GameTimer.ResetFinalElapsedTime();

        // タイトルシーンに遷移
        gameManager.ChangeTitleScene();
    }

}
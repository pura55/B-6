using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TitleButton : MonoBehaviour
{
    [SerializeField] private SoundPlayer soundPlayer;

    [SerializeField] private GameManager gameManager; // ゲームマネージャー

    public void GoToTitle()
    {
        soundPlayer.PlayOneShot();
        StartCoroutine(LoadTitleScene());
    }

    private IEnumerator LoadTitleScene()
    {
        yield return new WaitForSeconds(0.5f);

        // タイトルシーンに遷移
        gameManager.ChangeTitleScene();
    }
}
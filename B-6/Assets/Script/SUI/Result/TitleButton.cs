using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TitleButton : MonoBehaviour
{
    [SerializeField] private SoundPlayer soundPlayer;

    public void GoToTitle()
    {
        soundPlayer.PlayOneShot();
        StartCoroutine(LoadTitleScene());
    }

    private IEnumerator LoadTitleScene()
    {
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("TitleScene");
    }
}
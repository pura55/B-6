using UnityEngine;
using TMPro;

public class ChangeWave : MonoBehaviour
{
    [SerializeField] private WaveCycle waveCycle;
    [SerializeField] private TMP_InputField textMeshPro;

    private void Start()
    {
        textMeshPro.text = $"{waveCycle.waveCycleMin}";
    }

    // 変更する関数
    public void OnChange()
    {
        // テキストが正の整数型か確かめる
        bool success = uint.TryParse(textMeshPro.text, out waveCycle.waveCycleMin);
        
        if(!success)
        {
            // 変換成功（result に 123 が入る）
            Debug.Log("値が正の整数ではありません");
            return;
        }

        // string型からuintに変換
        waveCycle.waveCycleMin = uint.Parse(textMeshPro.text);
    }
}

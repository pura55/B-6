using UnityEngine;

/// <summary>
/// 手動でID4キャラクターを解放するスクリプト
/// </summary>
public class ReleaseID4 : MonoBehaviour
{
    [SerializeField] private SelectCharacterID selectCharacterID;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void OnCheck()
    {
        selectCharacterID.canSelected = true;
    }
    
}

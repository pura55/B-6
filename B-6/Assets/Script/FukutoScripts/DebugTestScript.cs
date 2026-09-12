using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugTestScript : MonoBehaviour
{
    [SerializeField] private PlayerProgressData playerProgressData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if ((Keyboard.current.spaceKey.wasPressedThisFrame))
        {
            Debug.Log($"ID: {playerProgressData.id}, 攻撃力: {playerProgressData.atkDmg}, クールタイム: {playerProgressData.atkCT}");
        }
    }
}

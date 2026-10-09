using UnityEngine;
using UnityEngine.InputSystem;

public class DebugCharacterSelect : MonoBehaviour
{
    public static int selectedPlayerID = 0;

    void Update()
    {
        if (Keyboard.current == null)
            return;

        // 1キー → キャラ1
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            selectedPlayerID = 1;
            Debug.Log("キャラ1を選択");
        }

        // 2キー → キャラ2
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            selectedPlayerID = 2;
            Debug.Log("キャラ2を選択");
        }

        // 3キー → キャラ3
        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            selectedPlayerID = 3;
            Debug.Log("キャラ3を選択");
        }

        if (Keyboard.current.digit4Key.wasPressedThisFrame)
        {
            selectedPlayerID = 4;
            Debug.Log("キャラ4を選択");
        }
    }
}
using UnityEngine;

public class PlayerMaterial : MonoBehaviour
{
    // ¡Ž‚Á‚Ä‚¢‚é‘fÞ
    public int material = 0;

    // Å‘å‰½ŒÂŽ‚Ä‚é‚©
    public int maxMaterial = 10;

    // ‘fÞ‚ð‘‚â‚·ŠÖ”
    public void AddMaterial(int amount)
    {
        material += amount;

        // Å‘å‚ð’´‚¦‚È‚¢‚æ‚¤‚É‚·‚é
        if (material > maxMaterial)
        {
            material = maxMaterial;
        }
    }
}
using UnityEngine;

public class ID1Sprite : MonoBehaviour
{
    public enum PlayerAnimState
    {
        Idle,
        Move,
        Attack,
        TakeHit,
        Death,
        Skill
    }

    [Header("キャラ1")]
    [SerializeField] private Sprite[] idleSpritesID1;
    [SerializeField] private Sprite[] moveSpritesID1;
    [SerializeField] private Sprite[] attackSpritesID1;
    [SerializeField] private Sprite[] takeHitSpritesID1;
    [SerializeField] private Sprite[] deathSpritesID1;
    [SerializeField] private Sprite[] skillSpritesID1;

    [Header("キャラ2")]
    [SerializeField] private Sprite[] idleSpritesID2;
    [SerializeField] private Sprite[] moveSpritesID2;
    [SerializeField] private Sprite[] attackSpritesID2;
    [SerializeField] private Sprite[] takeHitSpritesID2;
    [SerializeField] private Sprite[] deathSpritesID2;
    [SerializeField] private Sprite[] skillSpritesID2;

    [Header("キャラ3")]
    [SerializeField] private Sprite[] idleSpritesID3;
    [SerializeField] private Sprite[] moveSpritesID3;
    [SerializeField] private Sprite[] attackSpritesID3;
    [SerializeField] private Sprite[] takeHitSpritesID3;
    [SerializeField] private Sprite[] deathSpritesID3;
    [SerializeField] private Sprite[] skillSpritesID3;

    [Header("キャラ4")]
    [SerializeField] private Sprite[] idleSpritesID4;
    [SerializeField] private Sprite[] moveSpritesID4;
    [SerializeField] private Sprite[] attackSpritesID4;
    [SerializeField] private Sprite[] takeHitSpritesID4;
    [SerializeField] private Sprite[] deathSpritesID4;
    [SerializeField] private Sprite[] skillSpritesID4;

    [Header("再生速度")]
    [SerializeField] private float animationSpeed = 0.1f;

    private SpriteRenderer spriteRenderer;
    private PlayerHealth playerHealth;

    private PlayerAnimState currentState = PlayerAnimState.Idle;
    private Sprite[] currentSprites;

    private int spriteIndex = 0;
    private float timer = 0f;
    private bool isOneShotAnimation = false;

    private int currentPlayerID = 1;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerHealth = GetComponent<PlayerHealth>();

        currentPlayerID = DebugCharacterSelect.selectedPlayerID;

        if (currentPlayerID == 0)
        {
            currentPlayerID = 1;
        }

        ChangeState(PlayerAnimState.Idle);
    }

    void Update()
    {
        // デバッグ用：1・2・3キーでキャラ変更
        if (DebugCharacterSelect.selectedPlayerID != 0 &&
            DebugCharacterSelect.selectedPlayerID != currentPlayerID)
        {
            currentPlayerID = DebugCharacterSelect.selectedPlayerID;

            Debug.Log("キャラ変更 ID : " + currentPlayerID);

            ChangeState(PlayerAnimState.Idle);
        }

        PlayAnimation();
    }

    void PlayAnimation()
    {
        if (currentSprites == null || currentSprites.Length == 0)
            return;

        timer += Time.deltaTime;

        if (timer >= animationSpeed)
        {
            timer = 0f;
            spriteIndex++;

            if (spriteIndex >= currentSprites.Length)
            {
                // Death終了
                if (currentState == PlayerAnimState.Death)
                {
                    spriteIndex = currentSprites.Length - 1;
                    spriteRenderer.sprite = currentSprites[spriteIndex];

                    playerHealth.Respawn();

                    ChangeState(PlayerAnimState.Idle);
                    return;
                }

                // Attack / TakeHit / Skill
                if (isOneShotAnimation)
                {
                    isOneShotAnimation = false;

                    ChangeState(PlayerAnimState.Idle);
                    return;
                }

                // Idle / Move
                spriteIndex = 0;
            }

            spriteRenderer.sprite = currentSprites[spriteIndex];
        }
    }

    public void ChangeState(PlayerAnimState newState)
    {
        // 同じアニメなら切り替えない
        if (currentState == newState && currentSprites != null)
            return;

        currentState = newState;

        spriteIndex = 0;
        timer = 0f;

        switch (currentPlayerID)
        {
            case 1:
                SetCharacter1Animation();
                break;

            case 2:
                SetCharacter2Animation();
                break;

            case 3:
                SetCharacter3Animation();
                break;
            case 4:
                SetCharacter4Animation();
                break;
        }

        if (currentSprites != null && currentSprites.Length > 0)
        {
            spriteRenderer.sprite = currentSprites[0];
        }
    }

    void SetCharacter1Animation()
    {
        switch (currentState)
        {
            case PlayerAnimState.Idle:
                currentSprites = idleSpritesID1;
                isOneShotAnimation = false;
                break;

            case PlayerAnimState.Move:
                currentSprites = moveSpritesID1;
                isOneShotAnimation = false;
                break;

            case PlayerAnimState.Attack:
                currentSprites = attackSpritesID1;
                isOneShotAnimation = true;
                break;

            case PlayerAnimState.TakeHit:
                currentSprites = takeHitSpritesID1;
                isOneShotAnimation = true;
                break;

            case PlayerAnimState.Death:
                currentSprites = deathSpritesID1;
                isOneShotAnimation = false;
                break;

            case PlayerAnimState.Skill:
                currentSprites = skillSpritesID1;
                isOneShotAnimation = true;
                break;
        }
    }

    void SetCharacter2Animation()
    {
        switch (currentState)
        {
            case PlayerAnimState.Idle:
                currentSprites = idleSpritesID2;
                isOneShotAnimation = false;
                break;

            case PlayerAnimState.Move:
                currentSprites = moveSpritesID2;
                isOneShotAnimation = false;
                break;

            case PlayerAnimState.Attack:
                currentSprites = attackSpritesID2;
                isOneShotAnimation = true;
                break;

            case PlayerAnimState.TakeHit:
                currentSprites = takeHitSpritesID2;
                isOneShotAnimation = true;
                break;

            case PlayerAnimState.Death:
                currentSprites = deathSpritesID2;
                isOneShotAnimation = false;
                break;

            case PlayerAnimState.Skill:
                currentSprites = skillSpritesID2;
                isOneShotAnimation = true;
                break;
        }
    }

    void SetCharacter3Animation()
    {
        switch (currentState)
        {
            case PlayerAnimState.Idle:
                currentSprites = idleSpritesID3;
                isOneShotAnimation = false;
                break;

            case PlayerAnimState.Move:
                currentSprites = moveSpritesID3;
                isOneShotAnimation = false;
                break;

            case PlayerAnimState.Attack:
                currentSprites = attackSpritesID3;
                isOneShotAnimation = true;
                break;

            case PlayerAnimState.TakeHit:
                currentSprites = takeHitSpritesID3;
                isOneShotAnimation = true;
                break;

            case PlayerAnimState.Death:
                currentSprites = deathSpritesID3;
                isOneShotAnimation = false;
                break;

            case PlayerAnimState.Skill:
                currentSprites = skillSpritesID3;
                isOneShotAnimation = true;
                break;
        }
    }

    void SetCharacter4Animation()
    {
        switch (currentState)
        {
            case PlayerAnimState.Idle:
                currentSprites = idleSpritesID4;
                isOneShotAnimation = false;
                break;

            case PlayerAnimState.Move:
                currentSprites = moveSpritesID4;
                isOneShotAnimation = false;
                break;

            case PlayerAnimState.Attack:
                currentSprites = attackSpritesID4;
                isOneShotAnimation = true;
                break;

            case PlayerAnimState.TakeHit:
                currentSprites = takeHitSpritesID4;
                isOneShotAnimation = true;
                break;

            case PlayerAnimState.Death:
                currentSprites = deathSpritesID4;
                isOneShotAnimation = false;
                break;

            case PlayerAnimState.Skill:
                currentSprites = skillSpritesID4;
                isOneShotAnimation = true;
                break;
        }
    }

    public PlayerAnimState GetCurrentState()
    {
        return currentState;
    }

    public int GetPlayerID()
    {
        return currentPlayerID;
    }
}
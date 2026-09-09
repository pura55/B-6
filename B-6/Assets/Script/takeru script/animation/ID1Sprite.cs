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

    [Header("アニメーション")]
    [SerializeField] private Sprite[] idleSprites;
    [SerializeField] private Sprite[] moveSprites;
    [SerializeField] private Sprite[] attackSprites;
    [SerializeField] private Sprite[] takeHitSprites;
    [SerializeField] private Sprite[] deathSprites;
    [SerializeField] private Sprite[] skillSprites;

    [Header("再生速度")]
    [SerializeField] private float animationSpeed = 0.1f;

    private SpriteRenderer spriteRenderer;

    private PlayerAnimState currentState = PlayerAnimState.Idle;

    private Sprite[] currentSprites;

    private int spriteIndex = 0;
    private float timer = 0f;

    private bool isOneShotAnimation = false;

    private PlayerHealth playerHealth;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerHealth = GetComponent<PlayerHealth>();

        ChangeState(PlayerAnimState.Idle);
    }


    void Update()
    {
        PlayAnimation();
    }


    void PlayAnimation()
    {
        if (currentSprites == null || currentSprites.Length == 0)
        {
            return;
        }

        timer += Time.deltaTime;

        if (timer >= animationSpeed)
        {
            timer = 0f;

            spriteIndex++;

            // 最後まで再生した
            if (spriteIndex >= currentSprites.Length)
            {
                // Deathアニメーション終了
                if (currentState == PlayerAnimState.Death)
                {
                    // 最後の画像で止める
                    spriteIndex = currentSprites.Length - 1;
                    spriteRenderer.sprite = currentSprites[spriteIndex];

                    // 初期位置に戻してHP回復
                    playerHealth.Respawn();

                    // Idleに戻る
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

                // Idle / Moveはループ
                spriteIndex = 0;
            }

            spriteRenderer.sprite = currentSprites[spriteIndex];
        }
    }


    public void ChangeState(PlayerAnimState newState)
    {
        // 同じ状態なら最初から再生し直さない
        if (currentState == newState && currentSprites != null)
        {
            return;
        }

        currentState = newState;

        spriteIndex = 0;
        timer = 0f;

        switch (currentState)
        {
            case PlayerAnimState.Idle:

                currentSprites = idleSprites;
                isOneShotAnimation = false;

                break;


            case PlayerAnimState.Move:

                currentSprites = moveSprites;
                isOneShotAnimation = false;

                break;


            case PlayerAnimState.Attack:

                currentSprites = attackSprites;
                isOneShotAnimation = true;

                break;


            case PlayerAnimState.Death:

                currentSprites = deathSprites;

                // Deathは最後の画像で止めたいので別処理
                isOneShotAnimation = false;

                break;


            case PlayerAnimState.Skill:

                currentSprites = skillSprites;
                isOneShotAnimation = true;

                break;

            case PlayerAnimState.TakeHit:

                currentSprites = takeHitSprites;
                isOneShotAnimation = true;

                break;
        }

        if (currentSprites != null && currentSprites.Length > 0)
        {
            spriteRenderer.sprite = currentSprites[0];
        }
    }


    public PlayerAnimState GetCurrentState()
    {
        return currentState;
    }
}


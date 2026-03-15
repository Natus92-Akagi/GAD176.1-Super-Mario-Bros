using UnityEngine;

public class BrothersVerticalMovement : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rb;

    [HideInInspector]
    public float maxJumpHeight = 4.5f;
    [HideInInspector]
    public float maxDashJumpHeight = 5.5f;
    [HideInInspector]
    public float maxJumpTime = 0.33f;
    
    private float snappingThershold = 0.05f;

    public LayerMask ground;
    [HideInInspector]
    public float footSenorRadius = 0.2f;
    [HideInInspector]
    public float footSenorOffset = -0.3f;

    [HideInInspector]
    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
    [HideInInspector]
    public float dashJumpForce => (2f * maxDashJumpHeight) / (maxJumpTime / 2f);
    [HideInInspector]
    public float gravityScale => (-2f * maxJumpHeight) / Mathf.Pow(maxJumpTime / 2f, 2f);

    [HideInInspector]
    public float fallMutipler = 2.5f;
    [HideInInspector]
    public float lowJumpMultiplier = 2f;

    private bool _isJumping;
    private bool _onGround;
    private bool _isDucking;
    private bool isSuper = false;
    private bool isFire = false;
    private bool jumpButtonPressed;
    private bool jumpButtonHeld;

    [HideInInspector]
    public bool isJumping { get => _isJumping; private set => _isJumping = value; }
    [HideInInspector]
    public bool onGround { get => _onGround; private set => _onGround = value; }
    [HideInInspector]
    public bool isDucking { get => _isDucking; private set => _isDucking = value; }
    [HideInInspector]
    public bool isLarge => isSuper || isFire;
    [HideInInspector]
    public bool jumped => jumpButtonPressed || jumpButtonHeld;
    [HideInInspector]
    public bool pressedDown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var ver = rb.linearVelocity;
        CheckGround();
        HandleVerticalMovement();
        Ducking();
        Climbing();
       
       
        
    }
    public void HandleVerticalMovement()
    {
        rb.linearVelocity += Vector2.up * gravityScale * Time.fixedDeltaTime;

        if (onGround)
        {
            Vector3 currentPos = transform.position;
            float roundedY = Mathf.RoundToInt(currentPos.y);
            if (Mathf.Abs(currentPos.y - roundedY)< snappingThershold)
            {
                currentPos.y = roundedY;
                transform.position = currentPos;
            }
            isJumping = false;
            if (jumped)
            {
                if (isDucking && isLarge)
                {
                    float jumpToUse = rb.linearVelocity.x != 0 ? dashJumpForce : jumpForce;
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpToUse);
                    isJumping = true;
                }
                else 
                { 
                    float jumpToUse = rb.linearVelocity.x != 0 ? dashJumpForce: jumpForce;
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpToUse);
                    isJumping = true;
                }
            }
            if (rb.linearVelocity.y > 0 && !jumped)
            {
                rb.linearVelocity += Vector2.up * gravityScale * (lowJumpMultiplier - 1f) * Time.fixedDeltaTime;
            }
        }
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity *= Vector2.up * gravityScale * (fallMutipler - 1f) * Time.fixedDeltaTime;
        }
    }
    public void Ducking()
    {
        if (pressedDown && isLarge && onGround)
        {
            isDucking = true;
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        }
        else if (isDucking && (!pressedDown || !onGround))
        {
            isDucking=false;
        }
    }
    public void CheckGround()
    {
        Vector2 sesorPosition = (Vector2)transform.position + new Vector2(0, footSenorOffset);
        Collider2D[] results = Physics2D.OverlapCircleAll(sesorPosition, footSenorOffset, ground);

        onGround = false;
        foreach (Collider2D col in results) 
        { 
            if (col.gameObject != gameObject)
            {
                onGround = true;
            }
        }
    }
    public void Climbing()
    {

    }
}

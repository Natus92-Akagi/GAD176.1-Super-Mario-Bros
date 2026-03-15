using UnityEngine;

public class BrotherSwimmingMovement : MonoBehaviour
{
    public LayerMask water;
    internal BrotherHorizontalMovemnet horizontalMovemnet;
    internal BrothersVerticalMovement verticalMovemnet;

    [HideInInspector]
    public Rigidbody2D rb;
    
    [HideInInspector]
    public float maxGravity = 1.0f;
    [HideInInspector]
    public float underwaterGravity = 0.4f;
    [HideInInspector]
    public float returnToLandGravity = 0;
    [HideInInspector]
    public float swimSpeed = 4.0f;
    [HideInInspector]
    public float buoyancyForce = 2.0F;
    [HideInInspector]
    public float waterDamping = 0.95f;
    [HideInInspector]
    public float swimForce = 15.0f;
    [HideInInspector]
    public float deceleration = 60f;

    [HideInInspector]
    public float inputRawAxis;

    public bool isWater { get; set; } = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        horizontalMovemnet=GetComponent<BrotherHorizontalMovemnet>();
        verticalMovemnet=GetComponent<BrothersVerticalMovement>();

        returnToLandGravity = rb.gravityScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SwimmingMovement();   
    }
    public void SwimmingMovement() 
    {
        if (isWater) 
        { 
            rb.gravityScale = maxGravity - underwaterGravity;

            if (verticalMovemnet.onGround)
            {
                horizontalMovemnet.HandlerHoriztalMovement();
            }
            else
            {
                float targetSpeed = inputRawAxis * swimSpeed;
                float newXSpeed = Mathf.MoveTowards(rb.linearVelocity.x, targetSpeed, deceleration * Time.fixedDeltaTime * 0.5f);
                rb.linearVelocity = new Vector2(newXSpeed, rb.linearVelocity.y);
            }

            rb.linearVelocity += Vector2.down * buoyancyForce * Time.fixedDeltaTime;

            if (verticalMovemnet.jumped)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, swimForce);
            }
            rb.linearVelocity *= waterDamping;
        }
        else if (!isWater)
        {
            rb.gravityScale = returnToLandGravity;
            return;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            isWater = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            isWater = false;
            rb.gravityScale = returnToLandGravity;
        }
    }
}

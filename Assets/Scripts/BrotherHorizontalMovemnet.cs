using UnityEngine;

public class BrotherHorizontalMovemnet : MonoBehaviour
{
    internal BrothersVerticalMovement m_VerticalMovement;
    
    [HideInInspector]
    public Rigidbody2D rb;
    
    [HideInInspector]
    public float maxWalkSpeed = 6.0f;
    [HideInInspector]
    public float maxDashSpeed = 10.0f;
    [HideInInspector]
    public float acceleration = 100.0f;
    [HideInInspector]
    public float deceleration = 60.0f;
    [HideInInspector]
    public float skidFriciton = 150.0f;

    [HideInInspector]
    public float inputRawAxis;

    public bool isDashing {  get; private set; }
    public bool isSkidding { get; private set; }
    public bool isDuckSliding { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        m_VerticalMovement = GetComponent<BrothersVerticalMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var hoz = rb.linearVelocity;
    }
    public void HandlerHoriztalMovement()
    {
        float targetMaxSpeed = isDashing ? maxDashSpeed : acceleration;
        float currentSpeed = rb.linearVelocity.x;

        float accelerationForce = 0f;
        float decelerationForce = 0f;

        if (inputRawAxis != 0) 
        {
            accelerationForce = inputRawAxis * acceleration;
            if (Mathf.Sign(inputRawAxis) != Mathf.Sign(currentSpeed) && Mathf.Abs(currentSpeed) > 0.1f) 
            {
                accelerationForce = inputRawAxis * skidFriciton;
                isSkidding = true;
            }
            else
            {
                isSkidding= false;
            }
        }
        else
        {
            decelerationForce = -Mathf.Abs(currentSpeed) * deceleration;
        }
        rb.linearVelocity += new Vector2((accelerationForce + decelerationForce) * Time.fixedDeltaTime, 0f);
        rb.linearVelocity = new Vector2(Mathf.Clamp(rb.linearVelocity.x, -targetMaxSpeed, targetMaxSpeed), rb.linearVelocity.y);
        
        if (inputRawAxis == 0 && Mathf.Abs(rb.linearVelocity.x)< 0.1f)
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        }
    }
    public void DuckSlide()
    {
        if(m_VerticalMovement.isDucking && m_VerticalMovement.onGround)
        {
            float currentSpeed = rb.linearVelocity.x;
            
            if (Mathf.Abs(currentSpeed) > 0.1f) 
            {
                isDuckSliding = true;

                float slideFriciton = deceleration * 0.5f;
                float speedReduction = Mathf.Sign(currentSpeed) * slideFriciton * Time.fixedDeltaTime;

                rb.linearVelocity = new Vector2(currentSpeed - speedReduction, rb.linearVelocity.y);
            }
            else
            {
                isDuckSliding = false;
                rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            }
        }
        else
        {
            isDuckSliding = false;
        }
    }
}

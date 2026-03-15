using UnityEngine;

public class BrotherAnimation : MonoBehaviour
{
    public SpriteRenderer brotherRender;
    public Sprite jumpSprite;
    public Sprite idleSprite;
    public Sprite fallSprite;
    public Sprite swimSprite;
    public Sprite duckSprite;
    public Sprite skidSprite;
    public Sprite[] climbSprites;
    public Sprite[] run_walkSprites;
    public Sprite[] swimmingSprites;
    public Sprite[] firingSprites;

    public BoxCollider2D duckCollider;
    public CapsuleCollider2D normalCollider;
    public PolygonCollider2D swimCollider;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using UnityEngine;


public class SuperMarioBrosCamera : MonoBehaviour
{
    [HideInInspector]
    public Camera mainCamera;
    [HideInInspector]
    public float startingX = 8f;
    [HideInInspector]
    public float FixedY = 6.5f;
    [HideInInspector]
    public float ResolutionX = 1024f;
    [HideInInspector]
    public float ResolutionY = 960f;
    [HideInInspector]
    public Rigidbody2D player;
    [HideInInspector]
    public Transform[] activeplayers;

    public GameObject globalLight;
    internal LevelData levelData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /// summary> this is the main camera for the game for on start up while Unity API for the pefect authic Super Mario Bros feel of NES </summary>
        mainCamera = GetComponent<Camera>();
        levelData = FindAnyObjectByType<LevelData>();
        
        AspectRatio();

        if (mainCamera != null)
        {
            float pixelSnapSpacing = (mainCamera.orthographicSize * 2) / ResolutionY;
            transform.position = new Vector3(startingX, FixedY, -10F);
        }
    }

    // Update is called once per frame
    void Update()
    {
        /// summary> the Aspect Ratio funcation is update so Aspect Raito stay in pefect sync across all screen sizses </summary>
        AspectRatio();
    }
    /// <summary>
    /// Adjusts the camera viewport to maintain the specified aspect ratio, ensuring that the rendered scene fits the
    /// target resolution without distortion.
    /// </summary>
    /// <remarks>This method modifies the camera's viewport rectangle to preserve the aspect ratio defined by
    /// the ResolutionX and ResolutionY fields. Letterboxing or pillarboxing may occur if the screen's aspect ratio
    /// differs from the target. Call this method when the screen size or resolution changes to ensure consistent visual
    /// presentation.</remarks>
    public void AspectRatio() 
    {   float targetAspect = ResolutionX / ResolutionY;
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspect;
        if (scaleHeight < 1.0f)
        {
            Rect rect = mainCamera.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
            mainCamera.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;
            Rect rect = mainCamera.rect;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
            mainCamera.rect = rect;
        }
    }
    void LateUpdate()
    /// <summary> This method is called after all update methods have been called 
    /// ensure the mario stay in camera view of all time I will write camera reset inside MarioController script 
    /// if mario take damage camera move ahead during damage timeline as damage as Mario Center line is 8 X
    /// once mario take damage camera move by 5 X base of Mario currnet position in the level 
    /// unless mario in stationary camera position at time damage occur 
    /// </summary>
    {
        if (player == null) return;
        
        float cameraHaftWidth = mainCamera.aspect * mainCamera.orthographicSize;
        float leftEdge = transform.position.x - cameraHaftWidth;

        if (player.position.x < leftEdge) 
        { 
            player.position = new Vector2(leftEdge, player.position.y);
            player.linearVelocity = new Vector2(0, player.linearVelocity.y);
        } 
        if (transform.position.x > levelData.cameraStop)
        {
            transform.position = new Vector3(levelData.cameraStop, FixedY, -10F);
        } 

    }
}

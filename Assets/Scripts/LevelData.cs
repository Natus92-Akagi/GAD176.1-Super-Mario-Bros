using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LevelData : MonoBehaviour
{
    public enum levelType
    {
        Ground,
        GroundTwo,
        GroundThree,
        Underground,
        Athletic,
        Underwater,
        Castle
    }
    public levelType level;

    [HideInInspector]
    public float gameSeconds = 0.4f;
    [HideInInspector]
    public int worldNumber;
    [HideInInspector]
    public int levelNumber;
    [HideInInspector]
    public int levelTime;
    [HideInInspector]
    public Light2D global;
    [HideInInspector]
    public float globalIntensity;
    [HideInInspector]
    public Camera mainCamera;
    [HideInInspector]
    public int cameraStop;
    [HideInInspector]
    public int bossStop;
    [HideInInspector]
    public string mainLevelNameScene;
    [HideInInspector]
    public string[] eightFourLevelSectionNameScenes;


    public Vector3 startPoint;
    public Vector3 checkPoint;
    public Vector3 endPoint;
    public Vector3 coinHeavenDropPoint;
    public Vector3 bonusRoomDropPoint;
    private float timerAccumlator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = GetComponent<Camera>();
        global = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timerAccumlator += Time.deltaTime;

        if (timerAccumlator >= gameSeconds)
        {
            levelTime -= 1;
            timerAccumlator = 0;
        }
        else
        {
            
        }

    }
    public void SetLevelData(int world, int level, int time, int stop, int boss)
    {
        worldNumber = world;
        levelNumber = level;
        levelTime = time;
        cameraStop = stop;
        bossStop = boss;
        globalIntensity = global.intensity;
    }
    public void WorldOneOrder()
    {
        mainLevelNameScene = null;

        switch (level)
        {
            case levelType.Ground:
                SetLevelData(1, 1, 400, 203, 0);
                globalIntensity = 1.0f;
                mainLevelNameScene = "Super Mario Bros. 1-1";
                break;
            case levelType.Underground:
                SetLevelData(1, 2, 400, 187, 0);
                globalIntensity = 0.5f;
                mainLevelNameScene = "Super Mario Bros. 1-2";
                break;
            case levelType.Athletic:
                SetLevelData(1, 3, 300, 161, 0);
                globalIntensity = 1.0f;
                mainLevelNameScene = "Super Mario Bros. 1-3";
                break;
            case levelType.Castle:
                SetLevelData(1, 4, 300, 154, 138);
                globalIntensity = 0.05f;
                mainLevelNameScene = "Super Mario Bros. 1-4";
                break;

        }
    }
    public void WorldTwoOrder()
    {
        mainLevelNameScene = null;
        switch (level)
        {
            case levelType.Ground:
                SetLevelData(2, 1, 400, 208, 0);
                globalIntensity = 1.0f;
                mainLevelNameScene = "Super Mario Bros. 2-1";
                break;
            case levelType.Underground:
                SetLevelData(2, 2, 400, 185, 0);
                globalIntensity = 0.75f;
                mainLevelNameScene = "Super Mario Bros. 2-2";
                break;
            case levelType.Athletic:
                SetLevelData(2, 3, 300, 233, 0);
                globalIntensity = 1.0f;
                mainLevelNameScene = "Super Mario Bros. 2-3";
                break;
            case levelType.Castle:
                SetLevelData(2, 4, 300, 152, 136);
                globalIntensity = 0.05f;
                mainLevelNameScene = "Super Mario Bros. 2-4";
                break;
        }
    }
    public void WorldThreeOrder()
    {
        mainLevelNameScene = null;
        switch (level)
        {
            case levelType.Ground:
                SetLevelData(3, 1, 400, 208, 0);
                globalIntensity = 0.25f;
                mainLevelNameScene = "Super Mario Bros. 3-1";
                break;
            case levelType.GroundTwo:
                SetLevelData(3, 2, 300, 185, 0);
                globalIntensity = 0.25f;
                mainLevelNameScene = "Super Mario Bros. 3-2";
                break;
            case levelType.Athletic:
                SetLevelData(3, 3, 300, 161, 0);
                globalIntensity = 0.25f;
                mainLevelNameScene = "Super Mario Bros. 3-3";
                break;
            case levelType.Castle:
                SetLevelData(3, 4, 300, 152, 136);
                globalIntensity = 0.05f;
                mainLevelNameScene = "Super Mario Bros. 3-4";
                break;

        }
    }
    public void WorldFourOrder()
    {
        mainLevelNameScene = null;
        switch (level)
        {
            case levelType.Ground:
                SetLevelData(4, 1, 400, 207, 0);
                globalIntensity = 1.0f;
                mainLevelNameScene = "Super Mario Bros. 4-1";
                break;
            case levelType.Underground:
                SetLevelData(4, 2, 300, 216, 0);
                globalIntensity = 0.50f;
                mainLevelNameScene = "Super Mario Bros. 4-2";
                break;
            case levelType.Athletic:
                SetLevelData(4, 3, 300, 156, 0);
                globalIntensity = 1.0f;
                mainLevelNameScene = "Super Mario Bros. 4-3";
                break;
            case levelType.Castle:
                SetLevelData(4, 4, 400, 183, 167);
                globalIntensity = 0.05f;
                mainLevelNameScene = "Super Mario Bros. 4-4";
                break;
        }
    }
    public void WorldFiveOrder()
    {
        mainLevelNameScene = null;
        switch (level)
        {
            case levelType.Ground:
                SetLevelData(5, 1, 300, 208, 0);
                globalIntensity = 1.0f;
                mainLevelNameScene = "Super Mario Bros. 5-1";
                break;
            case levelType.GroundTwo:
                SetLevelData(5, 2, 400, 207, 0);
                globalIntensity = 1f;
                mainLevelNameScene = "Super Mario Bros. 5-2";
                break;
            case levelType.Athletic:
                SetLevelData(5, 3, 300, 161, 0);
                globalIntensity = 1.0f;
                mainLevelNameScene = "Super Mario Bros. 5-3";
                break;
            case levelType.Castle:
                SetLevelData(5, 4, 300, 152, 136);
                globalIntensity = 0.05f;
                mainLevelNameScene = "Super Mario Bros. 5-4";
                break;
        }
    }
    public void WorldSixOrder()
    {
        mainLevelNameScene = null;
        switch (level)
        {
            case levelType.Ground:
                SetLevelData(6, 1, 400, 194, 0);
                globalIntensity = 0.25f;
                mainLevelNameScene = "Super Mario Bros. 6-1";
                break;
            case levelType.GroundTwo:
                SetLevelData(6, 2, 400, 222, 0);
                globalIntensity = 0.25f;
                mainLevelNameScene = "Super Mario Bros. 6-2";
                break;
            case levelType.Athletic:
                SetLevelData(6, 3, 300, 174, 0);
                globalIntensity = 0.25f;
                mainLevelNameScene = "Super Mario Bros. 6-3";
                break;
            case levelType.Castle:
                SetLevelData(6, 4, 300, 152, 136);
                globalIntensity = 0.05f;
                mainLevelNameScene = "Super Mario Bros. 6-4";
                break;
        }
    }
    public void WorldSevenOrder()
    {
        mainLevelNameScene = null;
        switch (level)
        {
            case levelType.Ground:
                SetLevelData(7, 1, 400, 187, 0);
                globalIntensity = 1f;
                mainLevelNameScene = "Super Mario Bros. 7-1";
                break;
            case levelType.Underwater:
                SetLevelData(7, 2, 400, 185, 0);
                globalIntensity = 0.75f;
                mainLevelNameScene = "Super Mario Bros. 7-2";
                break;
            case levelType.Athletic:
                SetLevelData(7, 3, 300, 233, 0);
                globalIntensity = 1f;
                mainLevelNameScene = "Super Mario Bros. 7-3";
                break;
            case levelType.Castle:
                SetLevelData(7, 4, 400, 215, 199);
                globalIntensity = 0.05f;
                mainLevelNameScene = "Super Mario Bros. 7-4";
                break;
        }

    }
    public void WorldEightOrder()
    {
        mainLevelNameScene = null;
        switch (level)
        {
            case levelType.Ground:
                SetLevelData(8, 1, 300, 382, 0);
                globalIntensity = 1f;
                mainLevelNameScene = "Super Mario Bros. 8-1";
                break;
            case levelType.GroundTwo:
                SetLevelData(8, 2, 400, 222, 0);
                globalIntensity = 1f;
                mainLevelNameScene = "Super Mario Bros. 8-2";
                break;
            case levelType.GroundThree:
                SetLevelData(8, 3, 300, 221, 0);
                globalIntensity = 1f;
                mainLevelNameScene = "Super Mario Bros. 8-3";
                break;
            case levelType.Castle:
                FinalLeveLEightFour();
                break;
        }
    }
    public void FinalLeveLEightFour()
    {
        mainLevelNameScene = null;
        mainLevelNameScene = "Super Mario Bros. 8-4";

        SetLevelData(8, 4, 400, 56, 40);
        globalIntensity = 0.05f;
    }
    public void StartTransition()
    {
        if (worldNumber == 7 && levelNumber == 2)
        {
            mainLevelNameScene = null;
            globalIntensity = 1.0f;
            cameraStop = 8;
            mainLevelNameScene = "Super Mario Bros transition area (Snow)";
        }
        else if (levelNumber == 2)
        {
            mainLevelNameScene = null;
            globalIntensity = 1.0f;
            cameraStop = 8;
            mainLevelNameScene = "Super Mario Bros transition area";
        }
    }
    public void EndofLevelTransition()
    {
        if (worldNumber == 7 && levelNumber == 2)
        {
            mainLevelNameScene = null;
            globalIntensity = 1.0f;
            cameraStop = 27;
            mainLevelNameScene = "Super Mario Bros end of level transition area (Snow)";
        }
        else if (levelNumber == 2)
        {
            mainLevelNameScene = null;
            globalIntensity = 1.0f;
            cameraStop = 27;
            mainLevelNameScene = "Super Mario Bros end of level transition area";
        }

    }
}


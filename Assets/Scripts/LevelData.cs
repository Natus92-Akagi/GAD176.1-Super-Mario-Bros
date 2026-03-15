using UnityEngine;
using UnityEngine.Rendering.Universal;
using FMOD.Studio;
using FMODUnity;

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
        Castle,

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
    public int cameraXStop;
    [HideInInspector]
    public int bossXStop;
    [HideInInspector]
    public string mainLevelNameScene;
    [HideInInspector]
    public string[] eightFourLevelSectionNameScenes;
    [HideInInspector]
    public string bonusRoomSectionNameScene;
    [HideInInspector]
    public string underwaterSectionNameScene;
    [HideInInspector]
    public string fourtwoWarpZoneNameScene;



    public Vector3 startPoint;
    public Vector3 checkPoint;
    public Vector3 endPoint;
    public Vector3 coinHeavenDropPoint;
    public Vector3 bonusRoomDropPoint;
    private float timerAccumlator;

    [HideInInspector]
    public int currentSectionIndex;
    [Header ("Fmod")]
    public EventReference overworld;
    public EventReference bonusRoom;
    public EventReference castle;
    public EventReference underground;
    public EventReference underwater;
    private EventInstance eventInstance;
    private bool isUnderaterSectionActive;


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
    public void SetUnderwaterSectionActive(bool active)
    {
        if (isUnderaterSectionActive == active) return;
        isUnderaterSectionActive = active;
        PlayLevelMusic();
    }
    public void PlayLevelMusic()
    {
        StopLevelMusic();

        EventReference chosen = overworld;

        if (!string.IsNullOrEmpty(bonusRoomSectionNameScene))
        {
            chosen = bonusRoom;
        }
        else
        {
            if (worldNumber == 8 && levelNumber == 4 && eightFourLevelSectionNameScenes != null && currentSectionIndex >= 0 && currentSectionIndex < eightFourLevelSectionNameScenes.Length && eightFourLevelSectionNameScenes[currentSectionIndex] == "Super Mario Bros. 8 - 4 Section D")
            {
                chosen = underwater;
            }
            else if (worldNumber == 4 && levelNumber == 2 && !string.IsNullOrEmpty(fourtwoWarpZoneNameScene))
            {
                chosen = overworld;
            }
            else if ((worldNumber == 5 && levelNumber == 2) || (worldNumber == 6 && levelNumber == 2))
            {
                if (isUnderaterSectionActive || (!string.IsNullOrEmpty(underwaterSectionNameScene) && mainLevelNameScene == underwaterSectionNameScene))
                {
                    chosen = underwater;
                }
                else
                {
                    chosen = overworld;
                }
            }
            else
            {
                switch (level)
                {
                    case levelType.Castle: chosen = castle;break;
                    case levelType.Underground: chosen = underground; break;
                    case levelType.Underwater: chosen = underwater; break;
                    case levelType.Ground: case levelType.GroundTwo: case levelType.GroundThree: case levelType.Athletic: chosen = overworld; break;
                    default: chosen = overworld; break;
                }
                
            }
        }
        if (!chosen.IsNull)
        {
            try 
            {
                eventInstance = RuntimeManager.CreateInstance(chosen);
                if (eventInstance.isValid()) 
                {
                    RuntimeManager.AttachInstanceToGameObject(eventInstance, gameObject);
                    eventInstance.start();
                }

            }
            catch (System.Exception e)
            {
                Debug.Log("PlaylevelMusic: failed to start music - " + e.Message);
                eventInstance = default; 
            }
        }
        
    }
    public void StopLevelMusic()
    {
        if (eventInstance.isValid())
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            eventInstance.release();
            eventInstance = default;
        }
    }
    public void SetLevelData(int world, int level, int time, int stop, int boss)
    {
        worldNumber = world;
        levelNumber = level;
        levelTime = time;
        cameraXStop = stop;
        bossXStop = boss;
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
                startPoint = new Vector3(3, 1, 0);
                checkPoint = new Vector3(83, 1, 0);
                endPoint = new Vector3(205, 1, 0);
                mainLevelNameScene = "Super Mario Bros. 1-1";
                break;
            case levelType.Underground:
                SetLevelData(1, 2, 400, 187, 0);
                globalIntensity = 0.5f;
                startPoint = new Vector3(3, 10, 0);
                checkPoint = new Vector3(98, 1, 0);
                mainLevelNameScene = "Super Mario Bros. 1-2";
                break;
            case levelType.Athletic:
                SetLevelData(1, 3, 300, 161, 0);
                startPoint = new Vector3(3, 1, 0);
                checkPoint = new Vector3(69, 1, 0);
                endPoint = new Vector3(162, 1, 0);
                globalIntensity = 1.0f;
                mainLevelNameScene = "Super Mario Bros. 1-3";
                break;
            case levelType.Castle:
                SetLevelData(1, 4, 300, 154, 138);
                startPoint = new Vector3(1, 7, 0);
                endPoint = new Vector3(154, 1, 0);
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
                startPoint = new Vector3(3, 1, 0);
                checkPoint = new Vector3(102, 1, 0);
                coinHeavenDropPoint = new Vector3(166, 12, 0);
                endPoint = new Vector3(210, 1, 0);
                globalIntensity = 1.0f;
                mainLevelNameScene = "Super Mario Bros. 2-1";
                break;
            case levelType.Underwater:
                SetLevelData(2, 2, 400, 185, 0);
                startPoint = new Vector3(3, 12, 0);
                checkPoint = new Vector3(84, 1, 0);
                globalIntensity = 0.75f;
                mainLevelNameScene = "Super Mario Bros. 2-2";
                break;
            case levelType.Athletic:
                SetLevelData(2, 3, 300, 233, 0);
                startPoint = new Vector3(3, 1, 0);
                checkPoint = new Vector3(118, 1, 0);
                endPoint = new Vector3(234, 1, 0);
                globalIntensity = 1.0f;
                mainLevelNameScene = "Super Mario Bros. 2-3";
                break;
            case levelType.Castle:
                SetLevelData(2, 4, 300, 152, 136);
                startPoint = new Vector3(1, 7, 0);
                endPoint = new Vector3(152, 1, 0);
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
                startPoint = new Vector3(3, 1, 0);
                checkPoint = new Vector3(100, 1, 0);
                coinHeavenDropPoint = new Vector3(164, 12, 0);
                endPoint = new Vector3(209, 1, 0);
                globalIntensity = 0.25f;
                mainLevelNameScene = "Super Mario Bros. 3-1";
                break;
            case levelType.GroundTwo:
                SetLevelData(3, 2, 300, 185, 0);
                startPoint = new Vector3(3, 1, 0);
                checkPoint = new Vector3(100, 1, 0);
                endPoint = new Vector3(217, 1, 0);
                globalIntensity = 0.25f;
                mainLevelNameScene = "Super Mario Bros. 3-2";
                break;
            case levelType.Athletic:
                SetLevelData(3, 3, 300, 161, 0);
                startPoint = new Vector3(3, 1, 0);
                checkPoint = new Vector3(69, 1, 0);
                endPoint = new Vector3(163, 1, 0);
                globalIntensity = 0.25f;
                mainLevelNameScene = "Super Mario Bros. 3-3";
                break;
            case levelType.Castle:
                SetLevelData(3, 4, 300, 152, 136);
                startPoint = new Vector3(1, 7, 0);
                endPoint = new Vector3(153, 1, 0);
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
                startPoint = new Vector3(3, 1, 0);
                checkPoint = new Vector3(102, 1, 0);
                endPoint = new Vector3(235, 1, 0);
                globalIntensity = 1.0f;
                mainLevelNameScene = "Super Mario Bros. 4-1";
                break;
            case levelType.Underground:
                SetLevelData(4, 2, 300, 216, 0);
                startPoint = new Vector3(3, 10, 0);
                checkPoint = new Vector3(99, 1, 0);
                globalIntensity = 0.50f;
                mainLevelNameScene = "Super Mario Bros. 4-2";
                break;
            case levelType.Athletic:
                SetLevelData(4, 3, 300, 156, 0);
                startPoint = new Vector3(3, 1, 0);
                checkPoint = new Vector3(69, 1, 0);
                endPoint = new Vector3(157, 1, 0);
                globalIntensity = 1.0f;
                mainLevelNameScene = "Super Mario Bros. 4-3";
                break;
            case levelType.Castle:
                SetLevelData(4, 4, 400, 183, 167);
                startPoint = new Vector3(1, 7, 0);
                endPoint = new Vector3(182, 1, 0);
                GameObject.Find("Mario Spot Light ").SetActive(true);
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
                startPoint = new Vector3(3, 1, 0);
                checkPoint = new Vector3(104, 1, 0);
                endPoint = new Vector3(211, 1, 0);
                globalIntensity = 1.0f;
                mainLevelNameScene = "Super Mario Bros. 5-1";
                break;
            case levelType.GroundTwo:
                SetLevelData(5, 2, 400, 207, 0);
                startPoint = new Vector3(3, 1, 0);
                checkPoint = new Vector3(101, 1, 0);
                coinHeavenDropPoint = new Vector3(134, 12, 0);
                endPoint = new Vector3(209, 1, 0);
                globalIntensity = 1f;
                mainLevelNameScene = "Super Mario Bros. 5-2";
                break;
            case levelType.Athletic:
                SetLevelData(5, 3, 300, 161, 0);
                startPoint = new Vector3(3, 1, 0);
                checkPoint = new Vector3(69, 1, 0);
                endPoint = new Vector3(162, 1, 0);
                globalIntensity = 1.0f;
                mainLevelNameScene = "Super Mario Bros. 5-3";
                break;
            case levelType.Castle:
                SetLevelData(5, 4, 300, 152, 136);
                startPoint = new Vector3(1, 7, 0);
                endPoint = new Vector3(152, 1, 0);
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
                startPoint = new Vector3(3, 1, 0);
                checkPoint = new Vector3(102, 1, 0);
                endPoint = new Vector3(196, 1, 0);
                globalIntensity = 0.25f;
                mainLevelNameScene = "Super Mario Bros. 6-1";
                break;
            case levelType.GroundTwo:
                SetLevelData(6, 2, 400, 222, 0);
                startPoint = new Vector3(3, 1, 0);
                checkPoint = new Vector3(100, 1, 0);
                coinHeavenDropPoint = new Vector3(165, 12, 0);
                endPoint = new Vector3(224, 1, 0);
                globalIntensity = 0.25f;
                mainLevelNameScene = "Super Mario Bros. 6-2";
                break;
            case levelType.Athletic:
                SetLevelData(6, 3, 300, 174, 0);
                startPoint = new Vector3(3, 1, 0);
                checkPoint = new Vector3(100, 1, 0);
                endPoint = new Vector3(175, 1, 0);
                globalIntensity = 0.25f;
                mainLevelNameScene = "Super Mario Bros. 6-3";
                break;
            case levelType.Castle:
                SetLevelData(6, 4, 300, 152, 136);
                startPoint = new Vector3(1, 7, 0);
                endPoint = new Vector3(154, 1, 0);
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
                startPoint = new Vector3(3, 1, 0);
                checkPoint = new Vector3(103, 1, 0);
                endPoint = new Vector3(189, 1, 0);
                globalIntensity = 1f;
                mainLevelNameScene = "Super Mario Bros. 7-1";
                break;
            case levelType.Underwater:
                SetLevelData(7, 2, 400, 185, 0);
                startPoint = new Vector3(3, 12, 0);
                checkPoint = new Vector3(84, 1, 0);
                globalIntensity = 0.75f;
                mainLevelNameScene = "Super Mario Bros. 7-2";
                break;
            case levelType.Athletic:
                SetLevelData(7, 3, 300, 233, 0);
                startPoint = new Vector3(3, 1, 0);
                checkPoint = new Vector3(118, 1, 0);
                endPoint = new Vector3(234, 1, 0);
                globalIntensity = 1.0f;
                mainLevelNameScene = "Super Mario Bros. 7-3";
                break;
            case levelType.Castle:
                SetLevelData(7, 4, 400, 215, 199);
                startPoint = new Vector3(1, 7, 0);
                endPoint = new Vector3(214, 1, 0);
                GameObject.Find("Mario Spot Light ").SetActive(true);
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
                startPoint = new Vector3(3, 1, 0);
                checkPoint = startPoint;
                endPoint = new Vector3(384, 1, 0);
                globalIntensity = 1f;
                mainLevelNameScene = "Super Mario Bros. 8-1";
                break;
            case levelType.GroundTwo:
                SetLevelData(8, 2, 400, 222, 0);
                startPoint = new Vector3(3, 1, 0);
                checkPoint = startPoint;
                endPoint = new Vector3(224, 1, 0);
                globalIntensity = 1f;
                mainLevelNameScene = "Super Mario Bros. 8-2";
                break;
            case levelType.GroundThree:
                SetLevelData(8, 3, 300, 221, 0);
                startPoint = new Vector3(3, 1, 0);
                checkPoint = startPoint;
                endPoint = new Vector3(225, 1, 0);
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
        if (worldNumber == 8 && levelNumber == 4)
        {
            mainLevelNameScene = null;
            mainLevelNameScene = "Super Mario Bros. 8-4";
            levelTime = 400;
            globalIntensity = 0.05f;

            eightFourLevelSectionNameScenes = new string[]
            { "Super Mario Bros. 8-4 Section A",
              "Super Mario Bros. 8-4 Section B",
              "Super Mario Bros. 8-4 Section C",
              "Super Mario Bros. 8-4 Section D",
              "Super Mario Bros. 8-4 Section E"
            };
        }
        string currentSection = eightFourLevelSectionNameScenes[currentSectionIndex];

        if (currentSection == "Super Mario Bros. 8-4 Section A") 
        { 
            startPoint = new Vector3(1,7, 0);
        }
        if (currentSection == "Super Mario Bros. 8-4 Section E")
        {
            bossXStop = 40;
            cameraXStop = 56;
            globalIntensity = 0f;
        }
    }

    public void StartTransition()
    {
        if (worldNumber == 7 && levelNumber == 2)
        {
            mainLevelNameScene = null;
            globalIntensity = 1.0f;
            cameraXStop = 8;
            startPoint = new Vector3(3, 1, 0);
            mainLevelNameScene = "Super Mario Bros transition area (Snow)";
        }
        else if ((worldNumber == 1 || worldNumber == 2 || worldNumber == 4) && levelNumber == 2)
        {
            mainLevelNameScene = null;
            globalIntensity = 1.0f;
            cameraXStop = 8;
            startPoint = new Vector3(3, 1, 0);
            mainLevelNameScene = "Super Mario Bros transition area";
        }
    }
    public void EndofLevelTransition()
    {
        if (worldNumber == 7 && levelNumber == 2)
        {
            mainLevelNameScene = null;
            endPoint = new Vector3(29, 1, 0);
            globalIntensity = 1.0f;
            cameraXStop = 27;
            mainLevelNameScene = "Super Mario Bros end of level transition area (Snow)";
        }
        else if ((worldNumber == 1 || worldNumber == 2 || worldNumber == 4 ) && levelNumber == 2)
        {
            mainLevelNameScene = null;
            endPoint = new Vector3(29, 1, 0);
            globalIntensity = 1.0f;
            cameraXStop = 27;
            mainLevelNameScene = "Super Mario Bros end of level transition area";
        }

    }
    public void BonusRoomRoomPlacement()
    {
        if (worldNumber == 1 && levelNumber == 1 || worldNumber == 2 && levelNumber == 1 || worldNumber == 7 && levelNumber == 1 )
        {
            bonusRoomSectionNameScene = "Super Mario Bros. Bonus 1";
            globalIntensity = 1.0f;
            cameraXStop = 8;
            bonusRoomDropPoint = new Vector3(3, 10, 0);
        }
        else if (worldNumber == 1 && levelNumber == 2 || worldNumber == 8 && levelNumber == 1)
        {
            bonusRoomSectionNameScene = "Super Mario Bros. Bonus 2";
            globalIntensity = 1.0f;
            cameraXStop = 8;
            bonusRoomDropPoint = new Vector3(3,10, 0);
        }
        else if (worldNumber == 3 && levelNumber == 1)
        {
            bonusRoomSectionNameScene = "Super Mario Bros. Bonus 3";
            globalIntensity = 1.0f;
            cameraXStop = 8;
            bonusRoomDropPoint = new Vector3(3, 10, 0);
        }
        else if (worldNumber == 4 && levelNumber == 1 || worldNumber == 6 && levelNumber == 2)
        {
            bonusRoomSectionNameScene = "Super Mario Bros. Bonus 4";
            globalIntensity = 1.0f;
            cameraXStop = 8;
            bonusRoomDropPoint = new Vector3(3, 10, 0);
        }
        else if (worldNumber == 4 && levelNumber == 2 || worldNumber == 5 && levelNumber == 1 || worldNumber == 6 && levelNumber == 2 || worldNumber == 8 && levelNumber == 2)
        { 
            bonusRoomSectionNameScene = "Super Mario Bros. Bonus 5";
            globalIntensity = 1.0f; 
            cameraXStop = 8;
            bonusRoomDropPoint = new Vector3(3, 10, 0);
        }

    }
    public void CoinHeavenPlacement()
    {
        if (worldNumber == 2 && levelNumber == 1 || worldNumber == 5 && levelNumber == 2)
        {
            bonusRoomSectionNameScene = "Super Mario Bros. Coin Heaven";
            globalIntensity = 1.0f;
            cameraXStop = 70;
        }
        else if (worldNumber == 3 && levelNumber == 1)
        {
            bonusRoomSectionNameScene = "Super Mario Bros. Coin Heaven (Night)";
            GameObject.Find("6-2 cloud lift").SetActive(false);
            globalIntensity = 1.0f;
            cameraXStop = 89;
        }
        else if (worldNumber == 6 && levelNumber == 2) 
        { 
            bonusRoomSectionNameScene = "Super Mario Bros. Coin Heaven (Night)";
            GameObject.Find("Normal Cloud lift").SetActive(false);
            globalIntensity = 1.0f;
            cameraXStop = 89;
        }

    }
    public void UnderwaterChallenge()
    {
        
        if (worldNumber == 5 && levelNumber == 2)
        {
            underwaterSectionNameScene = "Super Mario Bros. Underwater";
            GameObject.Find("Hard 6-2").SetActive(false);
            globalIntensity = 1.0f;
            cameraXStop = 57;
            bonusRoomDropPoint = new Vector3(3, 12, 0);
        }
        else if (worldNumber == 6 && levelNumber == 2)
        {
            underwaterSectionNameScene = "Super Mario Bros. Underwater";
            GameObject.Find("Easy 5-2").SetActive(false);
            globalIntensity = 1.0f;
            cameraXStop = 57;
            bonusRoomDropPoint = new Vector3(3, 12, 0);
        }

        
    }
    public void FourTwoWarpZone() 
    { 
        if (worldNumber == 4 && levelNumber == 2)
        {
            fourtwoWarpZoneNameScene = "Super Mario Bros. 4-2 (Warp Zone)";
            globalIntensity = 1.0f;
            cameraXStop = 56;
        }

    }

}


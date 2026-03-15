using System.Collections.Generic;
using UnityEngine;

public class ObjectsData : MonoBehaviour
{
    internal SuperMarioBrosGameManager gameManager;

    public enum objectType { Brick, RopeLift, KoopaShell, BuzzyShell, Firework }
    public objectType objects;

    [HideInInspector]
    public int directHitValue;
    [HideInInspector]
    public int delayHitValue;

    public static readonly Dictionary<objectType, (int direct, int delay)> objectScores = new Dictionary<objectType, (int, int)>
    {
        {objectType.Brick, (50,0) },
        {objectType.RopeLift, (1000,0) },
        {objectType.KoopaShell, (500,400) },
        {objectType.BuzzyShell, (500,400) },
        {objectType.Firework, (500,0) },
    };

    public void Objects(bool isDirectHit = true)
    {
        if(objectScores.TryGetValue(objects, out var values)) 
        {
            if (isDirectHit && values.direct > 0) 
            { 
                gameManager.AddScore(values.direct);
            }
            else if (!isDirectHit && values.delay > 0)
            {
                gameManager.AddScore(values.delay);
            }
        
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    internal SuperMarioBrosGameManager manager;

    public enum enemyType 
    { 
        Goomba,
        GreenKoopaTroopa,
        RedKoopaTroopa,
        BuzzyBeetle,
        HammerBros,
        Spiny,
        SpinyEgg,
        Lakitu,
        PiranhaPlant,
        GreenParatroopa,
        RedParatroopa,
        BulletBill,
        Blooper,
        RedCheepCheep,
        GreenCheepCheep,
        Bowser
    }
    public enemyType enemy;

    [HideInInspector]
    public int stompValue;
    [HideInInspector]
    public int shotValue;
    [HideInInspector]
    public int starHitValue;
    [HideInInspector]
    public int[] shellComboValues;
    [HideInInspector]
    public int[] doubleStompValues;
    [HideInInspector]
    public int[] standardStompComboValues;

    public void Awake()
    {
        manager = GetComponent<SuperMarioBrosGameManager>();
        var scores = EnemyScores[enemy];
        stompValue = scores.stomp;
        shotValue = scores.shot;
        starHitValue = scores.star;
        shellComboValues = DefaultShellCombo;
        doubleStompValues = DoubleHit;
        standardStompComboValues = StompDefaultCombo;

    }
    public static readonly int[] DoubleHit = { 100, 400 };
    public static readonly int[] StompDefaultCombo = {100, 200, 400, 500,800,1000,2000,4000,5000,8000 };
    public static readonly int[] DefaultShellCombo = { 500, 800, 1000, 2000, 4000, 8000 };

    public static readonly Dictionary<enemyType, (int stomp, int shot, int star)> EnemyScores = new Dictionary<enemyType, (int stomp, int shot, int star)>
    {
        {enemyType.Goomba, (100,100,100) },
        {enemyType.GreenKoopaTroopa, (100,200,200) },
        {enemyType.RedKoopaTroopa, (100,200,200) },
        {enemyType.GreenParatroopa, (400,200,200) },
        {enemyType.RedParatroopa, (400,200,200) },
        {enemyType.BuzzyBeetle, (100,0,200) },
        {enemyType.BulletBill, (200,0,200) },
        {enemyType.HammerBros, (1000,1000,1000) },
        {enemyType.SpinyEgg, (0,200,200) },
        {enemyType.Spiny, (0,200,200) },
        {enemyType.PiranhaPlant, (0,200,200) },
        {enemyType.Lakitu,(800,200,200) },
        {enemyType.Blooper, (1000,200,0) },
        {enemyType.GreenCheepCheep, (0,200,0) },
        {enemyType.RedCheepCheep, (200,200,0) },
        {enemyType.Bowser, (0,5000,0) }
        
    };
    public void AwardShellCombo(int comboIndex) 
    { 
        if (comboIndex < shellComboValues.Length) 
        { 
           int points = shellComboValues[comboIndex];
           manager.AddScore(points);
           if ( points == 8000) 
           {
                manager.Addlife();
           }
        }
    }
    public void AwardAirborneStompCombo(int comboIndex) 
    { 
        if (comboIndex < standardStompComboValues.Length) 
        { 
            int points = standardStompComboValues[comboIndex];
            manager.AddScore(points);
            if (points == 8000) 
            {
                manager.Addlife();
            }
        }
        else
        {
            int poins = doubleStompValues[Mathf.Min(comboIndex - standardStompComboValues.Length, doubleStompValues.Length- 1)];
            manager.AddScore(poins);
        } 
    }

}

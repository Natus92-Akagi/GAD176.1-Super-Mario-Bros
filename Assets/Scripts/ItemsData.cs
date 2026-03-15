using System.Collections.Generic;
using UnityEngine;

public class ItemsData : MonoBehaviour
{
    internal SuperMarioBrosGameManager gameManager;
    internal BrotherMasterAndInput brotherMasterAndInput;

    public enum itemType { SuperMushroom, FireFlower, Star, OneUpMushroom, Coin }
    public itemType item;

    [HideInInspector]
    public int scoreValue;

    public static readonly Dictionary<itemType, int> itemScoreValues = new Dictionary<itemType, int>()
    {
        { itemType.SuperMushroom, 1000 },
        { itemType.FireFlower, 1000 },
        { itemType.Star, 1000 },
        { itemType.OneUpMushroom, 0 },
        { itemType.Coin, 200 }
    };
    public void Items()
    {
        if (itemScoreValues.TryGetValue(item, out int points))
        {
            if (item == itemType.OneUpMushroom)
            {
                gameManager.Addlife();
            }
            else
            {
                scoreValue = points;
                gameManager.AddScore(scoreValue);

                if (item == itemType.SuperMushroom) brotherMasterAndInput.SuperMario();
                if (item == itemType.FireFlower) brotherMasterAndInput.FireMario();
                if (item == itemType.Star) brotherMasterAndInput.StarManForm();
                if (item == itemType.Coin) gameManager.AddCoin();

            }
        }
    }

}

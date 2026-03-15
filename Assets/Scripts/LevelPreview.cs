using UnityEngine;
using TMPro;

public class LevelPreview : MonoBehaviour
{
    internal SuperMarioBrosUIManager UIManager;
    public SpriteRenderer levelPreviewCard;

    public Sprite[] levelPreviewCardSprites;

    [HideInInspector]
    public TextMeshProUGUI WorldNumber;
    [HideInInspector]
    public TextMeshProUGUI LevelNumber;
    [HideInInspector]
    public TextMeshProUGUI lifeCounter;

    public void DisplayLevelPreview(int worldNum, int levelNum, int LifeNum)
    {
        WorldNumber.text = worldNum.ToString();
        LevelNumber.text = levelNum.ToString();
        lifeCounter.text = LifeNum.ToString();

        // Fix: Use array indexing instead of method invocation
        int index = GetPreviewSpriteIndex(worldNum, levelNum);
        if (index >= 0 && index < levelPreviewCardSprites.Length)
        {
            levelPreviewCard.sprite = levelPreviewCardSprites[index];
        }
    }

    
    private int GetPreviewSpriteIndex(int worldNum, int levelNum)
    {

        return (worldNum - 1) * 10 + (levelNum - 1);
    }
}

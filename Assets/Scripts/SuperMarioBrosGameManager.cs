using UnityEngine;

public class SuperMarioBrosGameManager : MonoBehaviour
{
    int playerScore = 0;
    int playerLives = 128;
    int currentLives;
    int playercoins = 100;
    int currentCoins;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentLives = 3;
        currentCoins = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        Debug.Log("Starting the game...");
        // Implement game start logic here
    }
    public void EndGame()
    {
        Debug.Log("Ending the game...");
        // Implement game end logic here
    }
    public void AddScore(int score)
    {
        playerScore += score;
        Debug.Log("Player Score: " + playerScore);
    }
    public void Addlife()
    {
        if (currentLives < playerLives)
        {
            currentLives += 1;
        }
    }
    public void AddCoin()
    {
        if (currentCoins == playercoins - 1)
        {
            currentCoins = 0;
            Addlife();
        }
        else
        {
            currentCoins += 1;
        }
        
    }
    public void LoseLife()
    {
        currentLives -= 1;
        Debug.Log("Player Lives: " + currentLives);
    }


}

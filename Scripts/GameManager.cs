using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        begin, win, lose
    };

    public GameState gameState { get; private set; }

    [SerializeField]
    private int level = 1;
    private int hiScore = 1;

    [SerializeField]
    private GameObject playerPrefab, defenderPrefab;
    [SerializeField]
    private GameObject playerSpawnPoint;
    [SerializeField]
    private Collider defenderSpawnBounds;

    private List<GameObject> objectsToDestroy = new List<GameObject>();


    //UI
    [SerializeField]
    private TextMeshProUGUI levelText, hiScoreText;

    private void Start()
    {
        gameState = GameState.begin;
        SpawnGame();
        levelText.text = level.ToString();
    }

    public void TouchDown() 
    {
        gameState = GameState.win;
        Debug.Log("TouchDown!");
        StartCoroutine(ITouchDown());
    }

    public void Defended() 
    {
        gameState = GameState.lose;
        Debug.Log("Defended");
        StartCoroutine(IGameOver());
    }

    private void LevelUp() 
    {
        if (level > hiScore)
            hiScore = level;
        level++;
        SpawnGame();
    }

    private void GameOver()
    {
        if(level > hiScore)
            hiScore = level;
        level = 1;
        SpawnGame();
    }

    private void SpawnGame() 
    {
        foreach(GameObject obj in objectsToDestroy)
            Destroy(obj);
        objectsToDestroy.Clear();


        if (level > hiScore)
            hiScore = level;
        levelText.text = level.ToString();
        hiScoreText.text = "Hi: " + hiScore.ToString();
        gameState = GameState.begin;
        GameObject player = GameObject.Instantiate(playerPrefab, transform);
        player.transform.localPosition = new Vector3(playerSpawnPoint.transform.position.x, 1, playerSpawnPoint.transform.position.z);

        objectsToDestroy.Add(player);
        float xVal, zVal;
        for(int i = 0; i < level; i++)
        {
            xVal = Random.Range(defenderSpawnBounds.bounds.min.x, defenderSpawnBounds.bounds.max.x);
            zVal = Random.Range(defenderSpawnBounds.bounds.min.z, defenderSpawnBounds.bounds.max.z);
            GameObject defender = GameObject.Instantiate(defenderPrefab, new Vector3(xVal, 1, zVal), Quaternion.identity);

            objectsToDestroy.Add(defender);
        }
    }

    private IEnumerator ITouchDown()
    {
        yield return new WaitForSeconds(1);
        //Game UI Stuff
        yield return new WaitForSeconds(1);
        LevelUp();
    }

    private IEnumerator IGameOver()
    {
        yield return new WaitForSeconds(1);
        //Game UI Stuff
        yield return new WaitForSeconds(1);
        GameOver();
    }
}

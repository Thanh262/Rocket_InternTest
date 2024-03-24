using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] ballPrefab;
    public List<string> ballRespawn;
    string currentMissingBall;
    private int numberOfMissing;

    public int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;

    public TextMeshProUGUI startText;

    public TextMeshProUGUI youWin;

    public Button restartGameButton;
    public bool isGameActive = false;
    public bool win = false;
    
    // Start is called before the first frame update
    void Start()
    {
          
      
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    
    public void Look4ball(){
        numberOfMissing = ballRespawn.Count;
        for(int i = 0; i < numberOfMissing; i++){    

                currentMissingBall = ballRespawn[i];
                StartCoroutine(MakeBall(currentMissingBall));
                ballRespawn.Remove(currentMissingBall);

            }
    }
    
    IEnumerator MakeBall(string ball_color){
        yield return new WaitForSeconds(1f);
        
        Vector3 spawnPos = new Vector3(Random.Range(-10,10), 10, Random.Range(-10,10));
        
        for (int j  = 0 ; j <= 2; j++){
          if(ballPrefab[j].tag == ball_color){
            GameObject ball = Instantiate(ballPrefab[j], spawnPos, transform.rotation);
            ball.name = ball_color;
        }
        
        }        
        
    }

    public void UpdateScore(int scoreToAdd){
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
        CheckWin();
        
    }

    public void CheckWin(){
       if (score == 10){
            win = true;
            youWin.gameObject.SetActive(true);
            restartGameButton.gameObject.SetActive(true);
        }
    }

    public void GameOver(){
        gameOverText.gameObject.SetActive(true);
        restartGameButton.gameObject.SetActive(true);
    }

    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(){
        isGameActive = true;
        win = false;
        startText.gameObject.SetActive(false);
        youWin.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        scoreText.text = "Score: " + score;
    }
}

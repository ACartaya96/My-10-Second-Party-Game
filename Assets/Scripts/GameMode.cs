using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameMode : MonoBehaviour
{
    // Start is called before the first frame update
    Ball_Script ball;
    public Text countText;
    public Text scoreText;

    public Text gameOver;

    public int countdownTimer;

     public int count;

    public GameObject prefab1;

    AudioSource audioSource;
    public AudioClip victory;
    public AudioClip lost;




    public static GameMode instance {get; private set;}

    void Awake() {
        instance = this;
        scoreText.text = count.ToString();
        audioSource = GetComponent<AudioSource>();
    }
    public void Update() {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }
    }
    public void BeginGame()
    {
        scoreText.enabled = true;
        countText.enabled = true;
        Ball_Script.instance.hasClicked = false;

        StartCoroutine(Timer());
    }
    
    public void YouScore()
    {
        count++;
        scoreText.text = count.ToString();    
        Destroy(Ball_Script.instance.gameObject);
        Instantiate(prefab1,Ball_Script.instance.spawn.position, Quaternion.identity);
        Ball_Script.instance.hasClicked = false;
    }

    public void Respawn()
    { 
        Destroy(Ball_Script.instance.gameObject);
        Instantiate(prefab1,Ball_Script.instance.spawn.position, Quaternion.identity);
        Ball_Script.instance.hasClicked = false;
    }
    IEnumerator Timer()
    {
       
        while(countdownTimer > 0)
        {
            countText.text = countdownTimer.ToString();
            yield return new WaitForSeconds(1f);
            countdownTimer--;
        }
        StartCoroutine(GameOver());
    }
    IEnumerator GameOver()
    {
        scoreText.enabled = false;
        countText.enabled = false;
        gameOver.enabled = true;
         Ball_Script.instance.hasClicked = true;
        gameOver.text = "Game Over";
        audioSource.Stop();
        yield return new WaitForSeconds(1f);
        if(count >= 1)
        {
            audioSource.PlayOneShot(victory);
             gameOver.text = "You Win";
             yield return new WaitForSeconds(1f);
            
        }
        else
        {
             audioSource.PlayOneShot(lost);
             gameOver.text = "You Lose";
             yield return new WaitForSeconds(1f);
           
        }
        gameOver.text = "Your Score: " + scoreText.text;
    }
}

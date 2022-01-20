using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    // Start is called before the first frame updatepublic int countdownTime;
    public int countdownTime;
    public Text countdownDisplay;

    public Text instructionsToPlay;
    private void Start() {
        instructionsToPlay.enabled = true;
        countdownDisplay.enabled = true;
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        while(countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;

        }
        instructionsToPlay.enabled = false;
        countdownDisplay.text = "GO!";
        GameMode.instance.Invoke("BeginGame", 0.0f);
        yield return new WaitForSeconds(1f);
        countdownDisplay.enabled = false;


       
    }
}

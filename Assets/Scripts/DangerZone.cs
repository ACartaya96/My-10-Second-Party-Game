using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    public static DangerZone instance {get; set;}
    public BoxCollider2D boxCollider2D;
    public Transform spawn;

 
    // Start is called before the first frame update      
    void OnAwake()
    {
        instance = this;
    }
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        
    }

    // Update is called once per frame
    public void OnTriggerEnter2D(Collider2D other) {
        Ball_Script ball = other.gameObject.GetComponent<Ball_Script>();
        Instantiate(ball.gameObject, spawn.position, Quaternion.identity);
        Destroy(other.gameObject);
        ball.rb.simulated = false;
    }
}

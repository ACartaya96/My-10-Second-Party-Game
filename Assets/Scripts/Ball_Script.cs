using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Script : MonoBehaviour
{
    public Rigidbody2D rb;

    public float launchForce;
    public Transform shotPoint;
    public Transform spawn;
    public CircleCollider2D circleCollider;
    public GameObject point;

    GameObject[] points;
    public int numberOfPoints;
    public float spaceBetweenPoints;
    Vector2 direction;
     Vector2 ballPosition;
    Vector2 mousePosition;
    public bool hasClicked;
    AudioSource audioSource;
    public AudioClip Bounce;


    public static Ball_Script instance {get; set;}
    // Start is called before the first frame update

    private void Awake() {
        instance = this;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        circleCollider = GetComponent<CircleCollider2D>();
        points = new GameObject[numberOfPoints];
        rb.simulated = false;
        for(int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(point, shotPoint.position, Quaternion.identity);

        }
      
    }

    // Update is called once per frame
    void Update()
    {
        ballPosition = transform.position;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePosition - ballPosition;
        transform.right = -direction;
         if(hasClicked == false)
        {
            for(int i = 0; i < numberOfPoints; i++)
            {
                points[i].transform.position = PointPosition(i * spaceBetweenPoints);
                launchForce = (mousePosition - ballPosition).magnitude;
            }
        }    
         if(Input.GetMouseButtonDown(0) && hasClicked == false)
        {
            Shoot(transform.right);
            hasClicked = true;
        }
       
    }
    
    void Shoot(Vector2 direction)
    {
        //launchForce = (mousePosition - ballPosition).magnitude;
        rb.simulated = true;
        //rb.AddForce(direction, ForceMode2D.Impulse);
        rb.velocity = direction * (launchForce * 2);
        
        for(int i = 0; i < numberOfPoints; i++)
        {
            Destroy(points[i]);
        }
    }

    Vector2 PointPosition(float t)
    {
        Vector2 position = (Vector2)shotPoint.position + (-direction.normalized * launchForce * t) + 0.5f * Physics2D.gravity * (t*t);
        return position;
    }

        public void OnTriggerEnter2D(Collider2D other) 
     {
        if(other.gameObject.tag == "Score")
        {
            GameMode.instance.Invoke("YouScore", 0.0f); 
        }
        else if(other.gameObject.tag == "Dead")
        {
            hasClicked = false;
        }
     }

     private void OnCollisionEnter2D(Collision2D other) {
         audioSource.PlayOneShot(Bounce);        
     }
  
}

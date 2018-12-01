using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuAlPlayerController : MonoBehaviour
{
    [SerializeField] private Text uiText;
    [SerializeField] private float mainTimer;
    private float timer;
    private bool canCount = true;
    private bool doOnce = false;
    public Text countText;
    public Text winText;
    private int count;
    public Text loseText;

    private Rigidbody2D rb2d;
    
    public float speed;
    private bool facingRight = true;
    //audio
    private float volLowRange = 0.5f;
    private float volHighRange = 1.0f;
    public AudioClip Pickup;
    //public int numberOfPicks;
    private AudioSource source;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        winText.text = "";
        loseText.text = "";
        SetCountText();
        timer = mainTimer;
        
    }
    //audio
    void Awake()
    {
        source = GetComponent<AudioSource>();
    }
     void Update()
    {
        if (timer >= 0.0f && canCount)
        {
            timer -= Time.deltaTime;
            uiText.text = timer.ToString("F");
        }
        else if (timer <= 0.0f && !doOnce)
        {
            canCount = false;
            doOnce = true;
            uiText.text = "0.00";
            timer = 0.0f;
        }
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(movement * speed);

        if (facingRight == false && moveHorizontal > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveHorizontal < 0)
        {
            Flip();
        }

        if (Input.GetKey("escape"))
            Application.Quit();

        if (timer >= 10)
        {
            loseText.text = "You Lose! ";
            StartCoroutine(ByeAfterDelay(2));

        }



    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
           other.gameObject.SetActive(false);
           count = count + 2;
           SetCountText();
           //audio
            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(Pickup);
        }

    }
    void SetCountText()
    {
        countText.text = "Count : " + count.ToString();
        if (count>= 10)
       //if (count >= 10 && timer>= 0.0f )
        {
            winText.text = "You Win";
            StartCoroutine(ByeAfterDelay(2));
        }
        //else if (timer <= 0.0f )
       // {
          //  loseText.text = "You Lose";
       // }

    }
    IEnumerator ByeAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        //GameLoader.gameOn = false;
    }
}

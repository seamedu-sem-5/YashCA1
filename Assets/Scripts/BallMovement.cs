using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BallMovement : MonoBehaviour
{

    [SerializeField] private float initialSpeed = 10;
    [SerializeField] private float speedIncrease = 0.25f;
    [SerializeField] private TMP_Text playerScore;
    [SerializeField] private Text computerScore;

    private int hitCounter;
    private int score;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("StartBall", 2f);
        hitCounter = 0;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, initialSpeed + (speedIncrease * hitCounter));
    }

    private void StartBall()
    {
        rb.linearVelocity = new Vector2(-1, 0) * (initialSpeed + speedIncrease * hitCounter);
    }

    private void ResetBall()
    {
        rb.linearVelocity = new Vector2(0, 0);
        transform.position = new Vector2(0, 0);
        hitCounter = 0;
        Invoke("StartBall", 2f);
    }

    private void PlayerBounce(Transform myObject, bool _isPlayer)
    {
        if (_isPlayer)
        {
            score++;
            playerScore.text = score.ToString();
        }

        Vector2 ballPos = transform.position;
        Vector2 playerPos = myObject.position;

        float xDir, yDir;
        if (transform.position.x > 0)
        {
            xDir = -1;
        }
        else
        {
            xDir = 1;

        }

        yDir = (ballPos.y - playerPos.y) / myObject.GetComponent<Collider2D>().bounds.size.y;
        if (yDir == 0)
        {
            yDir = 0.25f;
        }

        rb.linearVelocity = new Vector2(xDir, yDir) * (initialSpeed + (speedIncrease * hitCounter));
        hitCounter++;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            PlayerBounce(collision.transform, true);
        }
        else if (collision.gameObject.name == "Computer")
        {
            PlayerBounce(collision.transform, false);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.position.x > 0)
        {
            ResetBall();
            score += 10;
            playerScore.text = score.ToString();
            computerScore.text = (int.Parse(computerScore.text) + 1).ToString();
        }
        else if (transform.position.x < 0)
        {
            GameManager.Instance.GameOver(score);
        }
    }
}

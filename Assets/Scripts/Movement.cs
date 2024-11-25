using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
    [SerializeField] private bool isAI;
    [SerializeField] private GameObject ball;

    private Rigidbody2D rb;
    private Vector2 playerMove;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
       if(isAI)
       {
            AIControl();
       }

       else
       {
            PlayerControl();
       }
    }

    void PlayerControl()
    {
        playerMove = new Vector2(0, Input.GetAxisRaw("Vertical"));
    }

    void AIControl()
    {
        if(ball.transform.position.y > transform.position.y + 0.5f)
        {
            playerMove = new Vector2(0, 1);
        }
        else if(ball.transform.position.y < transform.position.y - 0.5f)
        {
            playerMove = new Vector2(0, -1);
        }
        else
        {
            playerMove = new Vector2(0, 0);
        }
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = playerMove * moveSpeed;
    }
}

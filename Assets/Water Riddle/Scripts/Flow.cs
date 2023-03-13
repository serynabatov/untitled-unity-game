using UnityEngine;

public class Flow : MonoBehaviour
{
    public enum States { Clear, Dark, Light };
    private PipeController pipe;
    private GameManager gameManager;

    private Animator animator;


    public int direction;
    public States currentState;
    private Vector3 direct;
    private float speed=4;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManagers").GetComponent<GameManager>();
        currentState = States.Clear;
    }

    // Update is called once per frame
    void Update()
    {
        if ((!gameManager.gameOver)&&(gameManager.gameRunning)&&(!gameManager.gameClear))
        {
            transform.Translate(DirectionFinder(direction) * Time.deltaTime * speed);
        }
    }

    Vector3 DirectionFinder(int direction)
    {
        if (direction == 0)
        {
            direct = new Vector3(0, 1, 0);
        }
        if (direction == 1)
        {
            direct = new Vector3(1, 0, 0);
        }
        if (direction == 2)
        {
            direct = new Vector3(0, -1, 0);
        }
        if (direction == 3)
        {
            direct = new Vector3(-1, 0, 0);
        }
        return direct;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dark"))
        {
            currentState = States.Dark;
        }
        if (collision.CompareTag("Dark Block")&&(currentState!=States.Dark))
        {
            gameManager.gameOver = true;
        }
        if (collision.CompareTag("Light"))
        {
            currentState = States.Light;
        }
        if (collision.CompareTag("Light Block") && (currentState != States.Light))
        {
            gameManager.gameOver = true;
        }
        if (collision.CompareTag("Edge"))
        {
            gameManager.gameOver = true;
        }
        if (collision.CompareTag("Turn"))
        {
            TurnCheck(collision);
        }
        if (collision.CompareTag("Pipe"))
        {
            StraightCheck(collision);
        }
        if (collision.CompareTag("Goal"))
        {
            gameManager.gameClear = true;
        }
    }
    public void TurnCheck(Collider2D turn)
    {
        pipe = turn.GetComponent<PipeController>();
        animator = turn.GetComponent<Animator>();
        switch (pipe.AngleState)
        {
            case 0:
                {
                    switch (direction)
                    {
                        case 1:
                            animator.Play("Left - Down (Water) ");
                            direction = 2;
                            break;
                        case 0:
                            animator.Play("Down - Left (Water)");
                            direction = 3;
                            break;
                        default:
                            gameManager.gameOver = true;
                            break;
                    }
                }
                break;
            case 1:
                {
                    switch (direction)
                    {
                        case 1:
                            animator.Play("Left - Up (Water)");
                            direction = 0;
                            break;
                        case 2:
                            animator.Play("Up - Left (Water)");
                            direction = 3;
                            break;
                        default:
                            gameManager.gameOver = true;
                            break;
                    }
                }
                break;
            case 2:
                {
                    switch (direction)
                    {
                        case 2:
                            animator.Play("Up - Right (Water)");
                            direction = 1;
                            break;
                        case 3:
                            animator.Play("Right - Up (Water)");
                            direction = 0;
                            break;
                        default:
                            gameManager.gameOver = true;
                            break;
                    }
                }
                break;
            case 3:
                {
                    switch (direction)
                    {
                        case 0:
                            animator.Play("Down - Right (Water)");
                            direction = 1;
                            break;
                        case 3:
                            animator.Play("Right - Down (Water)");
                            direction = 2;
                            break;
                        default:
                            gameManager.gameOver = true;
                            break;
                    }
                }
                break;
        }
    }

    public void StraightCheck(Collider2D straight)
    {
        pipe = straight.GetComponent<PipeController>();
        animator = straight.GetComponent<Animator>();
        switch (pipe.PipeState)
        {
            case 0:
                {
                    switch (direction)
                    {
                        case 1:
                            animator.Play("Left - Right");
                            break;
                        case 3:
                            animator.Play("Right - Left");
                            break;
                        default:
                            gameManager.gameOver = true;
                            break;
                    }
                }
                break;
            case 1:
                {
                    switch (direction)
                    {
                        case 0:
                            animator.Play("Down - Up");
                            break;
                        case 2:
                            animator.Play("Up - Down");
                            break;
                        default:
                            gameManager.gameOver = true;
                            break;
                    }
                }
                break;
        }
    }
}

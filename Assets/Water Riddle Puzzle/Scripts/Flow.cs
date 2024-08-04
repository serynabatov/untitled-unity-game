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

    [SerializeField]
    private float speed;

    private MessageBrokerImpl _broker = MessageBrokerImpl.Instance;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManagers").GetComponent<GameManager>();
        currentState = States.Clear;
    }

    // Update is called once per frame
    void Update()
    {
        if ((!gameManager.gameOver) && (gameManager.gameRunning) && (!gameManager.gameClear))
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
        transform.position = collision.transform.position;

        if (collision.CompareTag("Dark"))
        {
            _broker.Publish((int)AudioClipName.WaterLevelSounds, false, true, 1);
            currentState = States.Dark;
        }
        if (collision.CompareTag("Dark Block"))
        {
            if (currentState != States.Dark)
            {
                _broker.Publish((int)AudioClipName.WaterLevelSounds, false, true, 3);
                gameManager.gameOver = true;
            }
            else
            {
                _broker.Publish((int)AudioClipName.WaterLevelSounds, false, true, 4);
            }
        }
        if (collision.CompareTag("Light"))
        {
            _broker.Publish((int)AudioClipName.WaterLevelSounds, false, true, 1);
            currentState = States.Light;
        }
        if (collision.CompareTag("Light Block"))
        {
            if (currentState != States.Light)
            {
                _broker.Publish((int)AudioClipName.WaterLevelSounds, false, true, 3);
                gameManager.gameOver = true;
            }
            else
            {
                _broker.Publish((int)AudioClipName.WaterLevelSounds, false, true, 5);
            }
        }
        if (collision.CompareTag("Edge"))
        {
            _broker.Publish((int)AudioClipName.WaterLevelSounds, false, true, 3);
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
            GoalCheck(collision);
            _broker.Publish((int)AudioClipName.WaterLevelSounds, false, true, 2);
            gameManager.gameClear = true;
        }
    }
    private void TurnCheck(Collider2D turn)
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
                            if (currentState == States.Dark)
                                animator.Play("Dark Left - Down (Start)");
                            if (currentState == States.Clear)
                                animator.Play("Left - Down (Start)");
                            if (currentState == States.Light)
                                animator.Play("Light Left - Down (Start)");
                            direction = 2;
                            break;
                        case 0:
                            if (currentState == States.Dark)
                                animator.Play("Dark Down - Left (Start)");
                            if (currentState == States.Clear)
                                animator.Play("Down - Left (Start)");
                            if (currentState == States.Light)
                                animator.Play("Light Down - Left (Start)");
                            direction = 3;
                            break;
                        default:
                            _broker.Publish((int)AudioClipName.WaterLevelSounds, false, true, 3);
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
                            if (currentState == States.Dark)
                                animator.Play("Dark Left - Up (Start)");
                            if (currentState == States.Clear)
                                animator.Play("Left - Up (Start)");
                            if (currentState == States.Light)
                                animator.Play("Light Left - Up (Start)");
                            direction = 0;
                            break;
                        case 2:
                            if (currentState == States.Dark)
                                animator.Play("Dark Up - Left (Start)");
                            if (currentState == States.Clear)
                                animator.Play("Up - Left (Start)");
                            if (currentState == States.Light)
                                animator.Play("Light Up - Left (Start)");
                            direction = 3;
                            break;
                        default:
                            _broker.Publish((int)AudioClipName.WaterLevelSounds, false, true, 3);
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
                            if (currentState == States.Dark)
                                animator.Play("Dark Up - Right (Start)");
                            if (currentState == States.Clear)
                                animator.Play("Up - Right (Start)");
                            if (currentState == States.Light)
                                animator.Play("Light Up - Right (Start)");
                            direction = 1;
                            break;
                        case 3:
                            if (currentState == States.Dark)
                                animator.Play("Dark Right - Up (Start)");
                            if (currentState == States.Clear)
                                animator.Play("Right - Up (Start)");
                            if (currentState == States.Light)
                                animator.Play("Light Right - Up (Start)");
                            direction = 0;
                            break;
                        default:
                            _broker.Publish((int)AudioClipName.WaterLevelSounds, false, true, 3);
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
                            if (currentState == States.Dark)
                                animator.Play("Dark Down - Right (Start)");
                            if (currentState == States.Clear)
                                animator.Play("Down - Right (Start)");
                            if (currentState == States.Light)
                                animator.Play("Light Down - Right (Start)");
                            direction = 1;
                            break;
                        case 3:
                            if (currentState == States.Dark)
                                animator.Play("Dark Right - Down (Start)");
                            if (currentState == States.Clear)
                                animator.Play("Right - Down (Start)");
                            if (currentState == States.Light)
                                animator.Play("Light Right - Down (Start)");
                            direction = 2;
                            break;
                        default:
                            _broker.Publish((int)AudioClipName.WaterLevelSounds, false, true, 3);
                            gameManager.gameOver = true;
                            break;
                    }
                }
                break;
        }
    }

    private void StraightCheck(Collider2D straight)
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
                            if (currentState == States.Dark)
                                animator.Play("Dark Left - Right (Start)");
                            if (currentState == States.Clear)
                                animator.Play("Left - Right (Start)");
                            if (currentState == States.Light)
                                animator.Play("Light Left - Right (Start)");
                            break;
                        case 3:
                            if (currentState == States.Dark)
                                animator.Play("Dark Right - Left (Start)");
                            if (currentState == States.Clear)
                                animator.Play("Right - Left (Start)");
                            if (currentState == States.Light)
                                animator.Play("Light Right - Left (Start)");
                            break;
                        default:
                            _broker.Publish((int)AudioClipName.WaterLevelSounds, false, true, 3);
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
                            if (currentState == States.Dark)
                                animator.Play("Dark Down - Up (Start)");
                            if (currentState == States.Clear)
                                animator.Play("Down - Up (Start)");
                            if (currentState == States.Light)
                                animator.Play("Light Down - Up (Start)");
                            break;
                        case 2:
                            if (currentState == States.Dark)
                                animator.Play("Dark Up - Down (Start)");
                            if (currentState == States.Clear)
                                animator.Play("Up - Down (Start)");
                            if (currentState == States.Light)
                                animator.Play("Light Up - Down (Start)");
                            break;
                        default:
                            _broker.Publish((int)AudioClipName.WaterLevelSounds, false, true, 3);
                            gameManager.gameOver = true;
                            break;
                    }
                }
                break;
        }
    }

    private void GoalCheck(Collider2D goal)
    {
        animator = goal.GetComponent<Animator>();
        animator.Play("Goal");
    }

    public void BlockerAnimation(Collider2D blocker)
    {
        animator = blocker.GetComponent<Animator>();
        animator.Play("Passed");
    }
}

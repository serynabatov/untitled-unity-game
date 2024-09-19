using UnityEngine;
using UnityEngine.InputSystem;

public class PipeController : MonoBehaviour
{
    private Animator animator;

    private GameManager gameManager;

    private Collider2D collide;

    private Camera cam;

    private MessageBrokerImpl _broker = MessageBrokerImpl.Instance;

    public int AngleState;
    public int PipeState;

    void Start()
    {
        if (gameObject.CompareTag("Turn"))
        {
            AngleState = Random.Range(0, 4);
        }
        if (gameObject.CompareTag("Pipe"))
        {
            PipeState = Random.Range(0, 2);
        }

        cam = Camera.main;
        collide = gameObject.GetComponent<CircleCollider2D>();
        gameManager = GameObject.Find("GameManagers").GetComponent<GameManager>();
        animator = gameObject.GetComponent<Animator>();

        if (gameObject.CompareTag("Turn"))
            AngleAnimationsCheck();
        if (gameObject.CompareTag("Pipe"))
            PipeAnimationsCheck();
    }

    void Update()
    {
        Mouse mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame)
        {
            Vector3 mousePosition = mouse.position.ReadValue();
            if (collide.OverlapPoint(cam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 1))))
            {
                _broker.Publish<int>((int)AudioClipName.WaterLevelSounds, false, true, 0);
                if (!gameManager.gameRunning)
                {
                    if (gameObject.CompareTag("Turn"))
                    {
                        AngleState++;
                        AngleState = AngleState % 4;
                        AngleAnimationsCheck();
                    }
                    if (gameObject.CompareTag("Pipe"))
                    {
                        PipeState++;
                        PipeState = PipeState % 2;
                        PipeAnimationsCheck();
                    }
                }
            }
        }
    }

    private void AngleAnimationsCheck()
    {
        if (gameObject.CompareTag("Turn"))
        {
            switch (AngleState)
            {
                case 0: // Left - Down
                    {
                        animator.Play("Left - Down");
                        break;
                    }
                case 1: // Left - Up
                    {
                        animator.Play("Left - Up");
                        break;
                    }
                case 2: // Up - Right
                    {
                        animator.Play("Right - Up");
                        break;
                    }
                case 3: // Right - Down
                    {
                        animator.Play("Right - Down");
                        break;
                    }
            }
        }
    }

    private void PipeAnimationsCheck()
    {
        if (gameObject.CompareTag("Pipe"))
        {
            switch (PipeState)
            {
                case 0: // Left - Right
                    animator.Play("Horizontal");
                    break;
                case 1: // Up - Down
                    animator.Play("Vertical");
                    break;
            }
        }
    }
}

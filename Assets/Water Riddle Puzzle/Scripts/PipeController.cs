using UnityEngine;
using UnityEngine.InputSystem;

public class PipeController : MonoBehaviour
{
    private Animator animator;

    private AudioSource sound;

    private GameManager gameManager;

    private Collider2D collide;

    private Camera cam;

    public int AngleState;
    public int PipeState;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        collide = gameObject.GetComponent<CircleCollider2D>();
        gameManager = GameObject.Find("GameManagers").GetComponent<GameManager>();
        animator = gameObject.GetComponent<Animator>();
        sound = gameObject.GetComponent<AudioSource>();
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
                sound.Play();
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

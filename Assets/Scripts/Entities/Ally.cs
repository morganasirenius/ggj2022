using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Ally : Entity
{

    public SpriteRenderer spriteRenderer;

    [SerializeField]
    private float maxTime = 1;

    [SerializeField]
    private float rescueSpeed;
    [SerializeField]
    private Material shield;

    [Tooltip("The direction the ally moves to.")]
    [SerializeField]
    private Globals.Direction moveDirection;

    [Tooltip("How long the ally moves for until it needs to stop. Needs Stop Duration to work.")]
    [SerializeField]
    private float moveDuration;
    [Tooltip("How long the ally stops for before moving forward. Needs Move Duration to work.")]
    [SerializeField]
    private float stopDuration;
    private float currentTime;
    private bool rescued = false;
    private Transform target;

    // Shield Stuff
    private Renderer _renderer;
    private MaterialPropertyBlock _propBlock;
    private float shieldStep;
    public GameObject TheShield;

    enum MovementStates
    {
        Moving = 0,
        Stopped
    }

    private Vector3 direction;
    private bool shouldStop;
    private float currentMoveTime, currentStopTime;
    private MovementStates state;

    // Start is called before the first frame update
    void Awake()
    {
        _propBlock = new MaterialPropertyBlock();
        _renderer = TheShield.GetComponent<Renderer>();
        Debug.Log(_propBlock);
        Debug.Log(_renderer);
        target = PlayerController.Instance.transform;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Initialize();
    }

    void OnEnable()
    {
        spriteRenderer.sprite = ResourceManager.Instance.AnimalSpriteArray[Random.Range(0,ResourceManager.Instance.AnimalSpriteArray.Length)];
        Initialize();
    }


    // Update is called once per frame
    protected override void Update()
    {
        if (rescued)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, rescueSpeed * Time.deltaTime);
        }
        else if (ShouldMove())
        {
            this.transform.position += direction * speed * Time.deltaTime;
        }
    }

    public void Initialize()
    {
        switch (moveDirection)
        {
            case Globals.Direction.Up:
                direction = Vector3.up;
                break;
            case Globals.Direction.Down:
                direction = Vector3.down;
                break;
            case Globals.Direction.Left:
                direction = Vector3.left;
                break;
            case Globals.Direction.Right:
                direction = Vector3.right;
                break;
            default:
                break;
        }

        if (moveDuration > 0)
        {
            shouldStop = true;
            currentMoveTime = moveDuration;
            currentStopTime = stopDuration;
            state = MovementStates.Moving;
        }
    }

    public bool ShouldMove()
    {
        // Check if enemy should be stopping first
        if (shouldStop)
        {
            // Decrement counters for moving/stopping depending on the state
            switch (state)
            {
                case MovementStates.Moving:
                    currentMoveTime -= Time.deltaTime;
                    if (currentMoveTime < 0)
                    {
                        state = MovementStates.Stopped;
                        return false;
                    }
                    return true;
                case MovementStates.Stopped:
                    currentStopTime -= Time.deltaTime;
                    if (stopDuration != 0 && currentStopTime < 0)
                    {
                        shouldStop = false;
                        return true;
                    }
                    return false;
                default:
                    break;
            }
        }
        return true;
    }

    public void Rescue()
    {
        if (!rescued)
        {
            currentTime += Time.deltaTime;
            float percentageTime = 1 - (currentTime / maxTime);
            _renderer.GetPropertyBlock(_propBlock);
            _propBlock.SetFloat("_EdgeDisolve", percentageTime);
            _renderer.SetPropertyBlock(_propBlock);

            if (currentTime >= maxTime)
            {
                rescued = true;
                AddScore();
                PlayerController.Instance.UpdateAlliesSaved();
                AudioManager.Instance.PlaySfx("rescue");
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        if (collision.gameObject.tag == "Player" && rescued)
        {
            OnReset();
            gameObject.SetActive(false);
        }
    }

    public override void OnReset()
    {
        rescued = false;
        currentTime = 0;
        _renderer.GetPropertyBlock(_propBlock);
        _propBlock.SetFloat("_EdgeDisolve", 1);
        _renderer.SetPropertyBlock(_propBlock);
    }

}

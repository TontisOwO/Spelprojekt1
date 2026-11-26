using UnityEngine;

public class RhythmPlatform : MonoBehaviour
{
    [SerializeField] private BoxCollider2D collider, trigger;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private GameManager gameManager;

    [SerializeField] private Vector2 indent = new Vector2(0.2f, 0.2f);

    [SerializeField] private float timer, startOffset, BPM;
    private float realFrequency;
    [SerializeField] private int Beat = 1, beatsOn = 1, beatsOff = 1;
    private int beatCounter;
    [SerializeField] private bool startOff;
    private bool canSwitch;

    Color halfTransparent;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        halfTransparent = new Color(1, 1, 1, 0.1f);
    }

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        trigger.size = collider.size - indent / transform.localScale;
        canSwitch = true;
        if (startOff) { collider.enabled = false; }
        if (collider.enabled)
        {
            spriteRenderer.color = Color.white;
        }
        else
        {
            spriteRenderer.color = halfTransparent;
        }
        BPM = gameManager.BPM;
        realFrequency = 60 * Beat / BPM;
        timer -= startOffset / BPM;
    }

    void Update()
    {
        timer += Time.deltaTime;
        while (timer > realFrequency && canSwitch)
        {
            timer -= realFrequency;
            beatCounter++;
            if ((collider.enabled && beatCounter == beatsOn) ||(!collider.enabled && beatCounter == beatsOff))
            {
                beatCounter = 0;
                Action();
            }
            if (collider.enabled)
            {
                spriteRenderer.color = Color.white;
            }
            else
            {
                spriteRenderer.color = halfTransparent;
            }
        }
    }

    protected virtual void Action()
    {
        collider.enabled = !collider.enabled;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canSwitch = false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        canSwitch = true;
    }
}

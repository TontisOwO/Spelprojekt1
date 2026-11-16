using UnityEngine;

public class RhythmPlatform : MonoBehaviour
{
    [SerializeField] private BoxCollider2D collider, trigger;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float timer, switchFrequency, startOffset;
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
        trigger.size = collider.size * 0.95f;
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
        timer -= startOffset;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > switchFrequency && canSwitch)
        {
            timer -= switchFrequency;
            collider.enabled = !collider.enabled;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        canSwitch = false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        canSwitch = true;
    }
}

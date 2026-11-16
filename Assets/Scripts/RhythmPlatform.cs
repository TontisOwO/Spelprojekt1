using UnityEngine;

public class RhythmPlatform : MonoBehaviour
{
    private BoxCollider2D collider;

    [SerializeField] float timer, switchTime = 1;
    [SerializeField] bool startOff;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        if (startOff) { collider.enabled = false; }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > switchTime) 
        {
            timer -= switchTime;
            collider.enabled = !collider.enabled;
        }
    }
}

using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public float BPM;
    private float beatLength;
    private float timer;
    [SerializeField] private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        beatLength = 60 / BPM;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= beatLength)
        {
            timer -= beatLength;
            audioSource.Play();
        }
    }
}

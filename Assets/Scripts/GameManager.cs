using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public float BPM;
    [SerializeField] private float beatsUntilLoop;
    private float beatLength;
    private float timer;
    private int beats;
    public float trackStart;
    [SerializeField] private AudioSource singleBeat;
    [SerializeField] private AudioSource track;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        track.time = trackStart;
        beatLength = 60 / BPM;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= beatLength)
        {
            timer -= beatLength;
            //singleBeat.Play();
            beats++;
            if (beats == beatsUntilLoop)
            {
                track.time = trackStart;
                beats = 0;
            }
        }
    }
}

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float BPM;
    [SerializeField] private int beatsUntilLoop;
    private float beatLength;
    private float timer;
    private int beats = 1;
    public float trackStart;
    [SerializeField] private AudioSource singleBeat;
    [SerializeField] private AudioSource track;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        track.time = trackStart;
        track.Play();
        beatLength = 60 / BPM;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        Debug.Log(timer);
        if (timer >= beatLength)
        {
            timer -= beatLength;
            //singleBeat.Play();
            beats++;
            //Debug.Log(beats);
            if (beats == beatsUntilLoop)
            {
                track.time = trackStart;
                beats = 0;
            }
        }
    }
}

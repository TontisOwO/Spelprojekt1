using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float BPM;
    [SerializeField] private int beatsUntilLoop;
    private float beatLength;
    private float timer;
    private int beats = -1;
    public float trackStart;
    [SerializeField] private AudioSource singleBeat;
    [SerializeField] private AudioSource track;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        beatLength = 60 / BPM;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= beatLength)
        {
            timer -= beatLength;
            //singleBeat.Play();
            beats++;
            if (beats == beatsUntilLoop)
            {
                beats = 0;
            }
        }
        if (beats == 0)
        {
            if (track.time >= trackStart + 5)
            {
                track.time = trackStart;
            }
            else if (track.time == 0)
            {
                track.Play();
                track.time = trackStart;
            }
        }
    }
}

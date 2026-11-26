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
        track.Play();
        track.time = trackStart;
        beatLength = 60 / BPM;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= beatLength)
        {
            Debug.Log(beats);
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

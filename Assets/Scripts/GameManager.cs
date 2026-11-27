using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public float BPM;
    [SerializeField] private int trackBeatsUntilLoop;
    [SerializeField] private int demoDmBeatsUntilLoop;
    [SerializeField] private int beatsUntilLoop;
    private float beatLength;
    private float timer;
    private int trackBeats = -1;
    private int demoDmBeats = -1;
    public float trackStart;
    public float demoDmStart;
    [SerializeField] private AudioSource singleBeat;
    [SerializeField] private AudioSource track;
    [SerializeField] private AudioSource demoDm;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        beatLength = 60 / BPM;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            track.volume = 1;
            demoDm.volume = 0;
        }
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            demoDm.volume = 1;
            track.volume = 0;
        }
    }

    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= beatLength)
        {
            timer -= beatLength;
            //singleBeat.Play();
            trackBeats++;
            demoDmBeats++;
            if (trackBeats == beatsUntilLoop)
            {
                trackBeats = 0;
            }
            if (demoDmBeats == demoDmBeatsUntilLoop)
            {
                demoDmBeats = 0;
            }
        }
        if (trackBeats == 0)
        {
            if (track.time >= trackStart + 3)
            {
                Debug.Log("track");
                track.time = trackStart;
            }
            else if (track.time == 0)
            {
                track.Play();
                track.time = trackStart;
            }
        }
        if (demoDmBeats == 0)
        {
            if (demoDm.time >= demoDmStart + 3)
            {
                Debug.Log("demo");
                demoDm.time = demoDmStart;
            }
            else if (demoDm.time == 0)
            {
                demoDm.Play();
            }
        }
    }
}

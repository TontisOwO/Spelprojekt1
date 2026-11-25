using UnityEngine;

public class RhythmPlaftormMove : RhythmPlatform
{
    [SerializeField] Vector3 move;
    protected override void Action()
    {
        transform.position += move;
    }
}

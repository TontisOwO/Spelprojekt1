using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private GameObject follow;
    [SerializeField] private float xDisplacement;
    [SerializeField] private float yDisplacement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2 (follow.transform.position.x + xDisplacement, follow.transform.position.y + yDisplacement);
    }
}

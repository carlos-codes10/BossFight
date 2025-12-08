using UnityEngine;

public class TrackPlayer : MonoBehaviour
{

    [SerializeField] PlayerLogic player;
    Vector3 offset;

    private void Start()
    {
        player = FindAnyObjectByType<PlayerLogic>();
        offset = new Vector3(0, 10, 0);
    }
    void Update()
    {
        transform.position = player.transform.position + offset;    
    }
}

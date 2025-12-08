using brolive;
using UnityEngine;

public class RockProjectile : MonoBehaviour
{
    [SerializeField] float Speed = 30.0f;
    [SerializeField] PlayerLogic player;
    [SerializeField] EnemyTest boss;
    int rockDeath = 2;
    Vector3 direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Start()
    {  
        player = FindAnyObjectByType<PlayerLogic>();
        boss = FindAnyObjectByType<EnemyTest>();
        direction = (player.transform.position - boss.transform.position).normalized;
    }


    void OnTriggerEnter()
    {
        Destroy(gameObject);
        GameManager.instance.ActivateHitStop(0.1f);
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Speed * Time.deltaTime * direction);

        Destroy(gameObject, 4f);
           
    }
}

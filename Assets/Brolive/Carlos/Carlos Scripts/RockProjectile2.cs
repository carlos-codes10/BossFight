using brolive;
using UnityEngine;

public class RockProjectile2 : MonoBehaviour
{
    [SerializeField] float Speed = 30.0f;

    void OnTriggerEnter()
    {
        Destroy(gameObject);
        GameManager.instance.ActivateHitStop(0.1f);
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Speed * Time.deltaTime * Vector3.down);

        Destroy(gameObject, 4f);
           
    }
}

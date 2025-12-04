using UnityEngine;

public class RockProjectile : MonoBehaviour
{
    [SerializeField] float Speed = 5.0f;
    int rockDeath = 4;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Kill()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D()
    {
        Kill();
    }

    // Update is called once per frame
    void Update()
    {
        float elpased =+ Time.deltaTime;
        transform.Translate(Speed * Time.deltaTime * Vector3.forward);

        if(elpased > rockDeath)
        {
            Kill();
        }
           
    }
}

using brolive;
using UnityEngine;

public class Phase3Helper : MonoBehaviour
{
    EnemyTest enemy;
    float elapsed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy = GetComponent<EnemyTest>();  
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime; 

        if (elapsed > 0.8f)
        {
            enemy.CastRockSpellPhase3();
            elapsed = 0f;   
        }
    }
}

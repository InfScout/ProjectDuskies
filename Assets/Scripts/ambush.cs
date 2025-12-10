using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class ambush : MonoBehaviour, IItem
{
    [SerializeField] private GameObject[] enemyList; 

    [SerializeField] private int spawnAmount = 3;
    [SerializeField] private float ambushCooldown = 20f;
    
    private bool _canAmbush = true;
    
    [SerializeField] private float randomNumber;
    [SerializeField] private Transform kikiSpawnPos;
    private Transform self;
    
    private Transform playerPos;

    private void Start()
    {
        self = GetComponent<Transform>();
        playerPos = PlayerGetter.GetPlayerPosition();
    }
    public void SpawnKikiAmbush()
    {
        
        float kikiBaseHeight = self.transform.position.y;
        float kikiHeight = kikiSpawnPos.transform.position.y;
        for (int i = 0; i < spawnAmount; i++)
        {
            GameObject RandomEnemy = enemyList[Random.Range(0, enemyList.Length)];
            
            float randomX = Random.Range(kikiSpawnPos.position.x - randomNumber, kikiSpawnPos.position.x + randomNumber);
            
            GameObject kikiClone = Instantiate(RandomEnemy, new Vector3(randomX, kikiHeight + kikiBaseHeight, 0), Quaternion.identity);
            EnemyAi enemyAi = kikiClone.GetComponent<EnemyAi>();
            
            enemyAi.AssignPlayer();
        }
    }

    public void Collect()
    {
        if (_canAmbush)
            StartCoroutine(CollectCoolDown());
    }

    private IEnumerator CollectCoolDown()
    {
        _canAmbush = false;
        SpawnKikiAmbush();
        SoundManager.PlaySound("Alert");
        yield return new WaitForSeconds(ambushCooldown);
        _canAmbush = true;
    }
}

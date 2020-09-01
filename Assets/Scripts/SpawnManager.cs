using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _player;
    private bool _stopSpawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartEnemySpawn()
    {
        StartCoroutine(SpawnRoutineCooldown());
    }

    IEnumerator SpawnRoutineCooldown()
    {
        yield return new WaitForSeconds(5.0f);

        while(!_stopSpawn)
        {
            Vector3 spawnPos = _player.transform.position + new Vector3(12f, 9.79f, 0f);
            Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(10f,15f));
        }
    }
}

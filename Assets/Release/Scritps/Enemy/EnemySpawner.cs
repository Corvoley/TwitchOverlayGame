using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public Transform target;
    public int numberOfEnemiesToSpawn = 5;
    public float spawnDelay = 1f;
    public List<BaseUnit> enemyPrefabs = new List<BaseUnit>();
    public SpawnMethod enemySpawnMethod = SpawnMethod.RoundRobin;
    private Dictionary<int, ObjectPool> enemyObjectPools = new Dictionary<int, ObjectPool>();
    public Transform spawnPostionTransform;

    //used for spawn enemies at random positions within navmesh area
    //private NavMeshTriangulation triangulation;


    private void Awake()
    {
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            enemyObjectPools.Add(i, ObjectPool.CreateInstance(enemyPrefabs[i], numberOfEnemiesToSpawn));
        }
    }
    private void Start()
    {
        // trianglation = NavMesh.CalculateTriangulation();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnDelay);
        int spawnedEnemies = 0;
        while (spawnedEnemies < numberOfEnemiesToSpawn)
        {
            if (enemySpawnMethod == SpawnMethod.RoundRobin)
            {
                SpawnRoundRobinEnemy(spawnedEnemies);
            }
            spawnedEnemies++;
            yield return wait;
        }
    }
    private void SpawnRoundRobinEnemy(int SpawnedEnemies)
    {
        int SpawnIndex = SpawnedEnemies % enemyPrefabs.Count;

        DoSpawnEnemy(SpawnIndex);
    }

    private void SpawnRandomEnemy()
    {
        DoSpawnEnemy(Random.Range(0, enemyPrefabs.Count));
    }
    private void DoSpawnEnemy(int SpawnIndex)
    {
        PoolableObject poolableObject = enemyObjectPools[SpawnIndex].GetObject();

        if (poolableObject != null)
        {
            /*int vertexIndex = Random.Range(0, triangulation.vertices.Length);
            NavMeshHit hit;
            if (NavMesh.SamplePosition(triangulation.vertices[vertexImdex], out hit, 2f, -1))
            {
              => move spawn enemy code inside here...
            }
            */
            BaseUnit enemy = poolableObject.GetComponent<BaseUnit>();
            enemy.agent.Warp(spawnPostionTransform.position);
            //enemy needs to get enabled and start chasing now.
            enemy.movement.target = target;
            enemy.agent.enabled = true;
            enemy.movement.StartChasing();
        }
        else
        {
            Debug.LogError($"Unable to fetch enemy of type {SpawnIndex} from object pool. Out of objects?");
        }
    }
    public enum SpawnMethod
    {
        RoundRobin,
        Random

    }
}

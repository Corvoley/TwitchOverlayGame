using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitSpawner : MonoBehaviour
{
    public Transform target;
    public int numberOfUnitsToSpawn = 5;
    public float spawnDelay = 1f;
    public List<BaseUnit> unitPrefab = new List<BaseUnit>();
    public SpawnMethod unitSpawnMethod = SpawnMethod.RoundRobin;
    private Dictionary<int, ObjectPool> unitObjectsPool = new Dictionary<int, ObjectPool>();
    public Transform spawnPostionTransform;
    public KeyCode spawnKey;
    //used for spawn enemies at random positions within navmesh area
    //private NavMeshTriangulation triangulation;

    private void Awake()
    {
        for (int i = 0; i < unitPrefab.Count; i++)
        {
            unitObjectsPool.Add(i, ObjectPool.CreateInstance(unitPrefab[i], numberOfUnitsToSpawn));
        }
    }
    private void Start()
    {
        // trianglation = NavMesh.CalculateTriangulation();
    }
    private void Update()
    {
        if (Input.GetKeyDown(spawnKey))
        {
            StartCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnDelay);
        int spawnedEnemies = 0;
        while (spawnedEnemies < numberOfUnitsToSpawn)
        {
            if (unitSpawnMethod == SpawnMethod.RoundRobin)
            {
                SpawnRoundRobinEnemy(spawnedEnemies);
            }
            spawnedEnemies++;
            yield return wait;
        }
    }
    private void SpawnRoundRobinEnemy(int SpawnedEnemies)
    {
        int SpawnIndex = SpawnedEnemies % unitPrefab.Count;

        DoSpawnEnemy(SpawnIndex);
    }

    private void SpawnRandomEnemy()
    {
        DoSpawnEnemy(Random.Range(0, unitPrefab.Count));
    }
    private void DoSpawnEnemy(int SpawnIndex)
    {
        PoolableObject poolableObject = unitObjectsPool[SpawnIndex].GetObject();

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
            enemy.movement.target = target.position;
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

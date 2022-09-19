using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public int enemiesToSpawn = 5;
    public GameObject[] enemies;

    public int powerupsToSpawn = 30;
    public GameObject[] powerups;

    public int npcsToSpawn = 5;
    public GameObject[] npcs;

    public int bossToSpawn = 5;
    public GameObject[] boss;

    public GeneratorBehaviour generator;

    //manager
    private GameManager _gameManager;

    private List<GameObject> floorTiles;

    public void Start()
    {
      _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if(_gameManager == null)
        {
          Debug.LogError("Game_Manager is NULL");
        }
        // always generate a random maze when we start the scene, instead of using a staticly generated maze
        generator.Generate();
        floorTiles = generator.physicalMap.GetObjectsOfType(SelectionObjectType.Floors);
        // unfortuantely, Walls, CorridorWalls, etc don't work for this particular API
        // so we are left to operate on Floors only - we have to manually figure out which tiles are floors and which are walls...
        SpawnEnemies();
        SpawnPowerups();
        SpawnNPCS();
        SpawnBoss();
    }

    private void SpawnEnemies()
    {
        List<GameObject> floors = new List<GameObject>(floorTiles); // make a working copy because we are going to remove tiles from the list
        if (floors.Count == 0)
        {
            return;
        }

        for (int i = 0; i < enemiesToSpawn; ++i)
        {
            if (enemies.Length == 0)
            {
              break;
            }

            GameObject randomEnemy = enemies[Random.Range(0, enemies.Length)];
            GameObject randomFloor = floors[Random.Range(0, floors.Count)];
            while (floors.Count > 1 && generator.physicalMap.GetObjectAtPosition(randomFloor.transform.position) != randomFloor)
            {
                // there is a wall above this floor tile - pick a floor tile that is actually walkable
                floors.Remove(randomFloor); // this floor tile isn't walkable, skip this floor in the future
                randomFloor = floors[Random.Range(0, floors.Count)];
            }
            Instantiate(randomEnemy, randomFloor.transform.position, Quaternion.identity);
            floors.Remove(randomFloor); // don't place another enemy here again
        }
    }

    private void SpawnPowerups()
    {
        List<GameObject> floors = new List<GameObject>(floorTiles); // make a working copy because we are going to remove tiles from the list
        if (floors.Count == 0)
        {
            return;
        }

        for (int i = 0; i < powerupsToSpawn; ++i)
        {
            if (powerups.Length == 0)
            {
                break;
            }

            GameObject randomPowerup = powerups[Random.Range(0, powerups.Length)];
            GameObject randomFloor = floors[Random.Range(0, floors.Count)];
            while (floors.Count > 1 && generator.physicalMap.GetObjectAtPosition(randomFloor.transform.position) == randomFloor)
            {
                // this is a naked floor tile - pick a floor tile that has a wall over it!
                floors.Remove(randomFloor); // this floor tile is naked, skip this floor in the future
                randomFloor = floors[Random.Range(0, floors.Count)];
            }
            Vector3 spawnPosition = randomFloor.transform.position;
            spawnPosition.y += 0.5f;
            Instantiate(randomPowerup, spawnPosition, Quaternion.identity);
            floors.Remove(randomFloor); // don't place another powerup here again
        }
    }
    private void SpawnNPCS()
    {
        List<GameObject> floors = new List<GameObject>(floorTiles); // make a working copy because we are going to remove tiles from the list
        if (floors.Count == 0)
        {
            return;
        }

        for (int i = 0; i < npcsToSpawn; ++i)
        {
            if (npcs.Length == 0)
            {
                break;
            }

            GameObject npcsObj = npcs[i];
            GameObject randomFloor = floors[Random.Range(0, floors.Count)];
            while (floors.Count > 1 && generator.physicalMap.GetObjectAtPosition(randomFloor.transform.position) == randomFloor)
            {
                // this is a naked floor tile - pick a floor tile that has a wall over it!
                floors.Remove(randomFloor); // this floor tile is naked, skip this floor in the future
                randomFloor = floors[Random.Range(0, floors.Count)];
            }
            Vector3 spawnPosition = randomFloor.transform.position;
            spawnPosition.y += 0.5f;
            Instantiate(npcsObj, spawnPosition, Quaternion.identity);
            floors.Remove(randomFloor); // don't place another powerup here again
        }
    }
    private void SpawnBoss()
    {
        List<GameObject> floors = new List<GameObject>(floorTiles); // make a working copy because we are going to remove tiles from the list
        if (floors.Count == 0)
        {
            return;
        }

        for (int i = 0; i < bossToSpawn; ++i)
        {
            if (boss.Length == 0)
            {
                break;
            }

            GameObject bossObj = boss[i];
            GameObject randomFloor = floors[Random.Range(0, floors.Count)];
            while (floors.Count > 1 && generator.physicalMap.GetObjectAtPosition(randomFloor.transform.position) == randomFloor)
            {
                // this is a naked floor tile - pick a floor tile that has a wall over it!
                floors.Remove(randomFloor); // this floor tile is naked, skip this floor in the future
                randomFloor = floors[Random.Range(0, floors.Count)];
            }
            Vector3 spawnPosition = randomFloor.transform.position;
            spawnPosition.y += 0.5f;
            Instantiate(bossObj, spawnPosition, Quaternion.identity);
            floors.Remove(randomFloor); // don't place another powerup here again
        }
    }
}

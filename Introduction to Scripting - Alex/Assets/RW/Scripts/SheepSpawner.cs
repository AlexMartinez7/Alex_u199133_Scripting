using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawner : MonoBehaviour
{
    public bool canSpawn = true; // 1
    public GameObject sheepPrefab; // 2
    public List<Transform> sheepSpawnPositions = new List<Transform>(); // 3
    public float timeBetweenSpawns ;
    private List<GameObject> sheepList = new List<GameObject>(); // 5

    private float rand;
    public GameObject GoldenSheepPrefab; 
    public GameObject enemySheepPrefab; 
    public float GoldenChance = 0.05f; //20% de que salga una oveja dorada
    public float EnemyChance = 0.2f; //20% de que salga una oveja enemiga

    // Start is called before the first frame update
    void Start()
    {
      StartCoroutine (SpawnRoutine());  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SpawnSheep()
    {
        Vector3 randomPosition = sheepSpawnPositions [Random.Range(0,sheepSpawnPositions.Count)].position; // 1
        GameObject sheep;
        rand = Random.value; 
        if (rand < GoldenChance) //si sale una oveja dorada
        {
            sheep = Instantiate(GoldenSheepPrefab, randomPosition, sheepPrefab.transform.rotation);
        }
        else if (rand < GoldenChance + EnemyChance) //si sale una oveja enemiga
        {
            sheep = Instantiate(enemySheepPrefab, randomPosition, sheepPrefab.transform.rotation);
        }
        else //si NO sale una oveja dorada o enemiga
        {
            sheep = Instantiate(sheepPrefab, randomPosition, sheepPrefab.transform.rotation);
        }

        sheepList.Add(sheep);
        Sheep sheepScript = sheep.GetComponent<Sheep>();
        sheepScript.SetSpawner(this);
        sheepScript.runSpeed *= GameStateManager.Instance.speedMultiplier;
    }
    private IEnumerator SpawnRoutine() // 1
    {
        while (canSpawn) // 2
        {
            SpawnSheep(); // 3
            yield return new WaitForSeconds(timeBetweenSpawns); // 4
        }
    }
    public void RemoveSheepFromList (GameObject sheep)
    {
        sheepList.Remove(sheep);
    }
    public void DestroyAllSheep()
    {
        foreach (GameObject sheep in sheepList)
        {
            Destroy(sheep);
        }

        sheepList.Clear(); // Limpia la lista después
    }

    
}

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

    public GameObject GoldenSheepPrefab; 
    public float GoldenChance = 0.2f; //10% de que salga una oveja dorada
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
        if (Random.value < GoldenChance) //si sale una oveja dorada
        {
            sheep = Instantiate(GoldenSheepPrefab, randomPosition, sheepPrefab.transform.rotation);
        }
        else //si NO sale una oveja dorada
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

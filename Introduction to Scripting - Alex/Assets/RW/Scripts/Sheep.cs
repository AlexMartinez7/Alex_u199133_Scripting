using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public float runSpeed; // 1
    public float gotHayDestroyDelay; // 2
    private bool hitByHay; // 3
    
    public float dropDestroyDelay; // 1
    private Collider myCollider; // 2
    private Rigidbody myRigidbody; // 3

    private SheepSpawner sheepSpawner;


    public float heartOffset; // 1
    public GameObject heartPrefab; // 2
    public GameObject xPrefab;
    private bool dropped = false;

    public bool isGolden = false; //flag para la oveaja dorada
    public bool isEnemy = false; //flag para la oveja enemiga

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
    }
    private void HitByHay()
    {
        hitByHay = true;
        runSpeed = 0;

        Destroy(gameObject, gotHayDestroyDelay);
        TweenScale tweenScale = gameObject.AddComponent<TweenScale>();; // 1
        tweenScale.targetScale = 0; // 2
        tweenScale.timeToReachTarget = gotHayDestroyDelay; // 3
        
        if (isGolden) 
        {   
            SoundManager.Instance.PlaySheepHitClip();
            Instantiate(heartPrefab, transform.position + new Vector3(0, heartOffset, 0), Quaternion.identity);
            GameStateManager.Instance.GoldenSheepSaved(); // sumamos al contador de ovejas doradas del manager
            GameStateManager.Instance.SavedSheep(); // sumamos al contador de ovejas normales del manager
            GameStateManager.Instance.SavedSheep();//la contamos doble
        }
        else if (isEnemy) 
        {   
            SoundManager.Instance.PlayEnemySheepSound();
            GameStateManager.Instance.enemySheepSaved(); // sumamos al contador de ovejas enemigas del manager
            GameObject cross = Instantiate(xPrefab, transform.position + new Vector3(0, heartOffset, 0), Quaternion.identity);
            Destroy(cross, 0.5f); // Destruir el objeto después de 1 segundo
        }
        else {
            SoundManager.Instance.PlaySheepHitClip();
            Instantiate(heartPrefab, transform.position + new Vector3(0, heartOffset, 0), Quaternion.identity);
            GameStateManager.Instance.SavedSheep(); // sumamos al contador de ovejas normales del manager
        }
       



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hay") && !hitByHay)
        {
            Destroy(other.gameObject);
            HitByHay();
        }
        else if (other.CompareTag("DropSheep"))
        {
            Drop();
        }
    }

    private void Drop()
    {   
        if (dropped) return; // evita ejecuciones múltiples
            dropped = true;
        myRigidbody.isKinematic = false;
        myCollider.isTrigger = false;
        Destroy(gameObject, dropDestroyDelay);
        GameStateManager.Instance.DroppedSheep();
        sheepSpawner.RemoveSheepFromList(gameObject); // 1
        SoundManager.Instance.PlaySheepDroppedClip();

    }
        

    
    
    public void SetSpawner(SheepSpawner spawner)
    {
        sheepSpawner = spawner;
    }


}

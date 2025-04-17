using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HayMachine : MonoBehaviour
{   
    public static HayMachine Instance; // 1
    private void Awake()
    {
        Instance = this; //Para poder acceder en otras clases
    }
    public float movementSpeed;

    public float horizontalBoundary = 22;

    public GameObject hayBalePrefab; // 1
    public Transform haySpawnpoint; // 2
    public float shootInterval; // 3
    private float shootTimer; // 4

    public Transform modelParent; // 1

// 2
    public GameObject blueModelPrefab;
    public GameObject yellowModelPrefab;
    public GameObject redModelPrefab;

    private bool Penetrante = false; //flag del power up
    private float PenetranteDuration = 5f;
    private void Start()
    {
        LoadModel(); 
    }
    private void LoadModel()
    {
        Destroy(modelParent.GetChild(0).gameObject); // 1

        switch (GameSettings.hayMachineColor) // 2
        {
            case HayMachineColor.Blue:
                Instantiate(blueModelPrefab, modelParent);
            break;

            case HayMachineColor.Yellow:
                Instantiate(yellowModelPrefab, modelParent);
            break;

            case HayMachineColor.Red:
                Instantiate(redModelPrefab, modelParent);
            break;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateMovement();
        UpdateShooting();
    }

    private void UpdateMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal"); // 1

        if (horizontalInput < 0 && transform.position.x > -horizontalBoundary) // 1
        {
            transform.Translate(transform.right * -movementSpeed * Time.deltaTime);
        }
        else if (horizontalInput > 0 && transform.position.x < horizontalBoundary) // 2
        {
            transform.Translate(transform.right * movementSpeed * Time.deltaTime);
        }
    }

    private void UpdateShooting()
    {
        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0 && Input.GetKey(KeyCode.Space))
        {
            
            if (!Penetrante) //si el power up NO está activo
            {   
                shootTimer = shootInterval;              
            }else //si el power up está activo
            {
                shootTimer = shootInterval/2; //disminuimos el tiempo de disparo          
            }
            ShootHay();
        }
       
    }

    private void ShootHay()
    {
        Instantiate(hayBalePrefab, haySpawnpoint.position, Quaternion.identity);
        SoundManager.Instance.PlayShootClip();

    }


    public void ActivatePenetrante()
    {
        Penetrante = true;
        StartCoroutine(DisablePenetranteAfterDelay());
    }

    private IEnumerator DisablePenetranteAfterDelay()
    {
        yield return new WaitForSeconds(PenetranteDuration);
        Penetrante = false;
    }
}
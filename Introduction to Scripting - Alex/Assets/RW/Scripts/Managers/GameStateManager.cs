using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance; // 1

    [HideInInspector]
    public int sheepSaved; // 2

    [HideInInspector]
    public int sheepDropped; // 3

    public int sheepDroppedBeforeGameOver; // 4
    public SheepSpawner sheepSpawner; // 5

    public float speedIncrease = 10f; //cada cuanto tiempo sube la velocidad

    private float timer = 0f;

    public float speedMultiplier = 1f; // 1

    private int GoldenSheepcount = 0; 
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    public void SavedSheep()
    {
        sheepSaved++;
        UIManager.Instance.UpdateSheepSaved();
    }

    public void GoldenSheepSaved()
    {
        GoldenSheepcount++;

        if (GoldenSheepcount >= 2)
        {
            GoldenSheepcount = 0; // reiniciar para volver a activar más adelante
            HayMachine.Instance.ActivatePenetrante(); // activamos el power up
            UIManager.Instance.ShowPenetranteMessage(); // activamos el mensaje del power up        
        }
    }

    public void enemySheepSaved()
    {
        sheepSaved--;
        UIManager.Instance.UpdateSheepSaved();
    }
    private void GameOver()
    {
        sheepSpawner.canSpawn = false; // 1
        sheepSpawner.DestroyAllSheep(); // 2
        UIManager.Instance.ShowGameOverWindow();
    }

    public void DroppedSheep()
    {
        sheepDropped++; // 1
        UIManager.Instance.UpdateSheepDropped();
        if (sheepDropped == sheepDroppedBeforeGameOver) // 2
        {
            GameOver();
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 10f) //cada 10 segundos le damos shake a la camara
        {
            timer = 0f; // reiniciar para poder volver a entrar
            speedMultiplier += 0.5f;
            UIManager.Instance.ShowSpeedUpMessage();
            StartCoroutine(CameraShake.Instance.Shake(1f, 0.3f)); //hacemos que tiemble la camara
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Title");
        }
   
    }

}

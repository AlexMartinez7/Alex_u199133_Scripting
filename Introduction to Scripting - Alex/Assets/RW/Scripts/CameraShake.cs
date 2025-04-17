using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;
    private void Awake(){
        if (Instance == null)
        {
            Instance = this;
            
        }
    } 

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.position; //posicion fija inicial
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude; 
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = originalPos + new Vector3(x,y,0); //solo la movemos en x e y
            elapsed += Time.deltaTime; //tiempo que ha pasado desde el inicio del shake
            yield return null; 
        }

        transform.localPosition = originalPos; //volvemos a la posicion original
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSurekli : MonoBehaviour
{
    [SerializeField] GameObject Rocket;
    [SerializeField] GameObject[] spawner;
    private void Start()
    {
        Time.timeScale = 1;
        StartCoroutine(RocketStarted());
    }
    IEnumerator RocketStarted()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            int randomSayi = Random.Range(0, spawner.Length);
            Instantiate(Rocket, spawner[randomSayi].transform.position,Quaternion.identity);
            yield return null;
        }
    }
}

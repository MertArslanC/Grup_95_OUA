using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectionCoin : MonoBehaviour
{
    private AudioSource coinVoice;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int scorePerCoin;
    private int currentScore;
    private void Start()
    {
        coinVoice = GetComponent<AudioSource>();
        currentScore= 0;
    }
    private void Update()
    {
        scoreText.text = "Score:" + currentScore;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("coin"))
        {
            currentScore += scorePerCoin;
            Destroy(other.gameObject);
            coinVoice.Play();
        }
    }

}

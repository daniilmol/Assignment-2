using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Projectile : MonoBehaviour
{
    [SerializeField] GameObject scoreText;
    [SerializeField] AudioClip bounce;
    private GameObject enemy;
    private GameObject player;
    private PlayerStats playerstats;
    private float timeToDestroy;

    void Start(){
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        print("Found enemy");
        player = GameObject.FindGameObjectWithTag("Player");
        playerstats = player.GetComponent<PlayerStats>();
        scoreText = GameObject.Find("Score");
        timeToDestroy = 2f;
    }

    private void OnCollisionEnter(Collision other) {
        GetComponent<AudioSource>().PlayOneShot(bounce);
        if(other.gameObject.tag == "Enemy"){
            player.GetComponent<PlayerStats>().IncrementScore();
            scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + playerstats.GetScore();
            print("Hit Enemy");
            enemy.GetComponent<EnemyBehaviour>().TakeDamage();
            Destroy(this);
        }else if(other.gameObject.tag == "Floor"){
            StartCoroutine(DestroyBall());
        }
    }

    private IEnumerator DestroyBall(){
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(this);
    }

}

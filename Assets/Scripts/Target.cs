using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
//*************************************************************************
    //Velocidade dos objetos
    private float minSpeed = 12f;
    private float maxSpeed = 19f;

    //Rotação do objeto
    private float maxTorque = 10f;

    //O quanto o objeto pode ir no eixo x;
    private float xRange = 7f;

    //Possição de respawn dos objetos
    private float ySpawnPos = -5f;

    //Components
    private Rigidbody rb;
//*************************************************************************

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.position = RandomSpawnPos();
        
        rb.AddForce(RandomFroce(), ForceMode.Impulse);
        rb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        Destroy(gameObject, 8f);
    }

    private Vector3 RandomFroce (){
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    private float RandomTorque (){
        return Random.Range(-maxTorque, maxTorque);
    }

    private Vector3 RandomSpawnPos (){
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    private void OnMouseDown() {
        if (GameManager.timeGame > 0 && GameManager.playGame)
        {
            string tagObject = this.tag;
            switch (tagObject)
            {
                case "GoodTarget":
                        GameManager.score += 15;
                        GameManager.timeGame += 1;
                    break;
                case "BadTarget":
                    GameManager.score -= 10;
                    GameManager.timeGame -= 8;
                    break;
                case "Cookie":
                        GameManager.score += 40;
                        GameManager.timeGame += 2;
                break;
                default:
                    Debug.Log("Item não possui tag.");
                break;
            }
            Destroy(gameObject);
        }
    }

    
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Sensor"))
            Destroy(gameObject);        
    }
}

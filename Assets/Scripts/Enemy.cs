using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Vector3 posicionIncial;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        posicionIncial=player.transform.position;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(collition.get)
    //}
}

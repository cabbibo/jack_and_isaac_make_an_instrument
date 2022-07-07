using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBalls : MonoBehaviour
{

    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBall(float i){
        Instantiate(prefab, new Vector3((float)i, 0, 0), Quaternion.identity);
    }   

}

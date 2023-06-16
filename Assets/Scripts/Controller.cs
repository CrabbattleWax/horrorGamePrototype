using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public List<Transform> spawnPoints = new List<Transform>();
    private GameObject playerObject;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        int randomPos = Random.Range(0, spawnPoints.Count - 1);
        playerObject.transform.position = spawnPoints[randomPos].position;
        playerObject.transform.rotation = spawnPoints[randomPos].rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}

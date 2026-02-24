using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratedPlatforms : MonoBehaviour
{
    [SerializeField] private int platformsAmount = 8;
    [SerializeField] private int platformsRadius = 10;

    [SerializeField] GameObject platformPrefab;
    [SerializeField] private float speed = 5.0f;
    GameObject[] platforms;
    GameObject lever;
    Vector2[] positions;
    Vector2[] destPositions;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lever != null)
        {
            if(!lever.GetComponent<LeverController>().isOn) return;
        }
        if (Vector2.Distance(platforms[0].transform.position, positions[1]) < 0.1f)
        {
            
            Vector2 pos0 = positions[0];
            for (int i = 0; i < platformsAmount-1; i++)
            {
                positions[i] = positions[i + 1];
            }
            positions[platformsAmount-1] = pos0;
        }
        for (int i = 0; i < platformsAmount; i++)
        {    
            platforms[i].transform.position =
                Vector2.MoveTowards(platforms[i].transform.position,
                                    positions[(i + 1) % platforms.Length],
                                    speed * Time.deltaTime);
        }
    }

    private void Awake()
    {
        lever = GameObject.Find("LeverGeneratedPlatforms");
        platforms = new GameObject[platformsAmount];
        positions = new Vector2[platformsAmount];
        destPositions = new Vector2[platformsAmount];
        float angle = 2 * Mathf.PI / platformsAmount;
        
        for (int i = 0; i < platforms.Length; i++)
        {
            positions[i].x = this.transform.position.x + Mathf.Cos(angle * i) * platformsRadius;
            destPositions[i].y = positions[i].y = this.transform.position.y + Mathf.Sin(angle * i) * platformsRadius;
            platforms[i] = Instantiate(platformPrefab, positions[i], Quaternion.identity);
        }
    }
}

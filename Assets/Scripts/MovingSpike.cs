using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpike : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3[] positions;
    private int index;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,positions[index],Time.deltaTime*speed);

        if(transform.position == positions[index])
        {
            if(index == positions.Length-1)
            {
                index = 0;
            }
            else
            {
                index++;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D other) 
    {
        other.transform.SetParent(transform);
    }
    void OnCollisionExit2D(Collision2D other) 
    {
        other.transform.SetParent(null);
    }
}

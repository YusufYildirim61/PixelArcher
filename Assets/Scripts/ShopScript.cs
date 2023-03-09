using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    [SerializeField] GameObject speechBubble;
    // Start is called before the first frame update
    void Start()
    {
        speechBubble.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        speechBubble.SetActive(true);
    }
    void OnTriggerExit2D(Collider2D other) 
    {
        speechBubble.SetActive(false);
    }
}

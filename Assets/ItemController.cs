using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //ínñ Ç…è’ìÀÇµÇΩèÍçáÇÕîjä¸
        if(other.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}

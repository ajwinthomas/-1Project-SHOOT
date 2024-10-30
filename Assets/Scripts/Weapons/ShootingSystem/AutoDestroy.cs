using System.Collections;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] float waitTimetoDestroy = 5;

    
   
   
    void Update()
    {
        StartCoroutine(DestroyMuzzle());
    }
    
    IEnumerator DestroyMuzzle()
    {
        yield return new WaitForSeconds(waitTimetoDestroy);
        Destroy(gameObject);
    }
}

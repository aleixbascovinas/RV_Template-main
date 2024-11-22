using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleHitScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameControllerScript gcScript;
    private void OnMouseDown()
    {
        if (gameObject.activeSelf)
        {
            gcScript.HitMole(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("hammer"))
        {
            gcScript.HitMole(gameObject);
        }
    }

}

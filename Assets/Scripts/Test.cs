using System.Collections;
using System.Collections.Generic;
using BreakEternity;
using UnityEngine;

public class Test : MonoBehaviour
{
    private BigDouble mogos = BigDouble.dTwo;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mogos = mogos.pow(mogos);
        Debug.Log(mogos);
    }
}

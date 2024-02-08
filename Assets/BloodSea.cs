using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BloodSea : SingletonPersistent<BloodSea>
{
    [SerializeField] Transform particles1;
    [SerializeField] Transform particles2;
    [SerializeField] BloodSeaComponent image;

    [SerializeField] float fillSpeed = .30f;

    /// <summary>
    /// bloodCurrencyYouHave / maxAmount
    /// </summary>
    /// <returns></returns>
    [SerializeField, Range(0,1)] public float filledAmount = 0;

    void FixedUpdate() {
	    bool sinking = filledAmount < (image._transform.position.y - image.bottomPosition) / ((image.topPosition - image.bottomPosition) / 2);
	    image._transform.position = new Vector3(image._transform.position.x, 
		    
		    Mathf.Lerp(image._transform.position.y, Mathf.Lerp(image.bottomPosition, image.CenterPosition, filledAmount), fillSpeed), 
		    
		    image._transform.position.y);
	    
	    image._transform.localScale = new(image._transform.localScale.x, (image._transform.position.y - image.bottomPosition) * 2, image._transform.localScale.z);
	    (particles1.position, particles2.position) = (new Vector3(particles1.position.x, image._transform.position.y + (image._transform.localScale.y / 2), particles1.position.z), new Vector3(particles2.position.x, image._transform.position.y + (image._transform.localScale.y / 2), particles2.position.z));
    }
    
    

}
[Serializable]
public struct BloodSeaComponent {
	public Transform _transform;
	public float bottomPosition;
	public float topPosition;
	public float CenterPosition => ((topPosition - bottomPosition) / 2) + bottomPosition;
}

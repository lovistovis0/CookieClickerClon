using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodMeter1 : MonoBehaviour
{
	[SerializeField] RectTransform bloodImage;
	[SerializeField] float bloodAmount;

	private void Update()
	{
		//bloodImage.rect.yMin = bloodAmount;
	}
}

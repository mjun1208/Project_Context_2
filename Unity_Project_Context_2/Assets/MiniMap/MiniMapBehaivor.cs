using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapBehaivor : MonoBehaviour
{
	[SerializeField]
	private Transform CityObject;
	[SerializeField]
	private float f_RotateSpeed = 0.5f;

	private Vector3 v_RotateStartPoint;


	public void RotateCity() 
	{
		CityObject.Rotate(new Vector3(0, MouseAxis() * f_RotateSpeed * Time.deltaTime, 0));
	}

	public void RotateStart() 
	{
		v_RotateStartPoint = Input.mousePosition;
	}

	float MouseAxis() 
	{
		return v_RotateStartPoint.x - Input.mousePosition.x;
	}
}

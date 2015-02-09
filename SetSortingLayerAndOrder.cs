using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]
public class SetSortingLayerAndOrder : MonoBehaviour
{
	public string sortingLayer;
	public int sortingOrder;
	// Use this for initialization
	void Start ()
	{
		this.renderer.sortingLayerName = sortingLayer;
		this.renderer.sortingOrder = sortingOrder;
	}
}


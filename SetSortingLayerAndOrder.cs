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
		this.GetComponent<Renderer>().sortingLayerName = sortingLayer;
		this.GetComponent<Renderer>().sortingOrder = sortingOrder;
	}
}


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public static class FTools
{
	/// <summary>
	/// Convert an Array containing Generic type objects to a readable string
	/// *Remember to override your ToString() method
	/// Usage:
	/// Debug.Log(FTools.ArrayToString<MyType>(myTypeArray));
	/// </summary>
	public static string ArrayToString<T>(T[] array, string delimiter = " - ")
	{
		string output = string.Empty;
		int l = array.Length;
		for(int i = 0; i < l; ++i)
		{
			if(i == 0)
			{
				if(array[i] != null)
				{
					output = array[i].ToString();
				}
				else
				{
					output = " * ";
				}
			}
			else
			{
				if(array[i] != null)
				{
					output = string.Concat(output, delimiter, array[i].ToString());
				}
				else
				{
					output = string.Concat(output, delimiter, " * ");	
				}
			}
		}
		return output;
	}

	/// <summary>
	/// Get screen Bounds for an Orthographic Camera setup
	/// </summary>	
	public static Bounds ScreenOrthoBounds(Camera cam)
	{
		float camHeight = cam.orthographicSize * 2f;
		float scrRatio = (float)Screen.width/(float)Screen.height;
		return new Bounds(cam.transform.position, new Vector3(camHeight * scrRatio, camHeight, 0f));
	}

	/// <summary>
	/// Optimized way (by using sqrMagnitude intead of Vector3.Distance) to know if an object is in range.
	/// </summary>
	/// <param name="range">The maximum allowed distance between these two Vectors</param>
	public static bool IsInRange(Vector3 objectPosition, Vector3 targetPosition, float range)
	{
		return (targetPosition - objectPosition).sqrMagnitude < (range * range);
		
	}
	
	/// <summary>
	/// Converts a R,G,B and A int to a Color Object
	/// </summary>
	/// <returns>The float color.</returns>
	/// <param name="R">Red - from 0 to 255</param>
	/// <param name="G">Green - from 0 to 255</param>
	/// <param name="B">Blue - from 0 to 255</param>
	/// <param name="A">Alpha - from 0 to 255</param>
	public static Color ToColor(int R, int G, int B, int A)
	{
		float fullColor = 255.0f;
		Color c = new Color((float)R / fullColor, (float)G / fullColor, (float)B / fullColor, (float)A / fullColor);
		return c;
	}

	/// <summary>
	/// Returns a float with n amount places after the decimal point
	/// </summary>
	/// <returns>A float with n places after the decimal point.</returns>
	/// <param name="myFloat">My float.</param>
	/// <param name="places">Places.</param>
	public static float GetDecimal(float myFloat, int places)
	{
		return Mathf.Ceil(myFloat * places) / places;
	}
	
	public static float Round(float value, int digits)
	{
		float mult = Mathf.Pow(10.0f, (float)digits);
		return Mathf.Round(value * mult) / mult;
	}
	
	/// <summary>
	/// Index Of COLOR for the faster builtin array
	/// </summary>
	/// <returns>The of.</returns>
	/// <param name="array">Array.</param>
	/// <param name="value">Value.</param>
	
	public static int IndexOf(Color[] array, Color value)
	{
		int l = array.Length;
		for (int i = 0; i < l; i++)
		{
			if (array[i] == value)
				return i;
		}
		return -1;
	}
	
	/// <summary>
	/// Index Of INT for the faster builtin array
	/// </summary>
	/// <returns>The of.</returns>
	/// <param name="array">Array.</param>
	/// <param name="value">Value.</param>
	
	public static int IndexOf(int[] array, int value)
	{
		int l = array.Length;
		for (int i = 0; i < l; i++)
		{
			if (array[i] == value)
				return i;
		}
		return -1;
	}
	
	/// <summary>
	/// Index Of STRING for the faster builtin array
	/// </summary>
	/// <returns>The of.</returns>
	/// <param name="array">Array.</param>
	/// <param name="value">Value.</param>
	
	public static int IndexOf(string[] array, string value)
	{
		int l = array.Length;
		for (int i = 0; i < l; i++)
		{
			if (array[i] == value)
				return i;
		}
		return -1;
	}
	
	/// <summary>
	/// Destroy GameObjects inside the "items" builtin array
	/// </summary>
	/// <param name='items'>
	/// Items array.
	/// </param>
	/// <param name='immediate'>
	/// Use DestroyImmediate (not recommended)
	/// </param>
	public static void DestroyItemsIn(GameObject[] items, bool immediate = false)
	{
		int itemsAmount = items.Length;
		if (itemsAmount == 0) return;
		for (int i = 0; i < itemsAmount; ++i)
		{
			if (immediate)
			{
				MonoBehaviour.DestroyImmediate(items[i]);
			}
			else
			{
				MonoBehaviour.Destroy(items[i]);
			}
		}
	}
	
	/// <summary>
	/// Blend two colors by the "amount" parameter
	/// </summary>
	/// <param name="c1">Color 1.</param>
	/// <param name="c2">Color 2.</param>
	/// <param name="amount">Amount / percentage to mix.</param>
	
	public static Color Blend(Color c1, Color c2, float amount)
	{
		int r = (int)((c1.r * amount) + c2.r * (1 - amount));
		int g = (int)((c1.g * amount) + c2.g * (1 - amount));
		int b = (int)((c1.b * amount) + c2.b * (1 - amount));
		return ToColor(r, g, b, 255);
	}
	
	/// <summary>
	/// Return Field Values inside a Class
	/// </summary>
	/// <returns>
	/// The field value.
	/// </returns>
	/// <param name='obj'>
	/// Object.
	/// </param>
	/// <param name='fieldName'>
	/// Field name.
	/// </param>
	/// <typeparam name='T'>
	/// The 1st type parameter.
	/// </typeparam>
	public static T GetFieldValue<T>(object obj, string fieldName)
	{
		if (obj == null)
			throw new System.ArgumentNullException("obj");
		
		var field = obj.GetType().GetField(fieldName, BindingFlags.Public |
		                                   BindingFlags.NonPublic |
		                                   BindingFlags.Instance);
		
		if (field == null)
			throw new System.ArgumentException("fieldName", "No such field was found.");
		
		if (!typeof(T).IsAssignableFrom(field.FieldType))
			throw new System.InvalidOperationException("Field type and requested type are not compatible.");
		
		return (T)field.GetValue(obj);
	}
	
	/// <summary>
	/// Return an absolute float (positive)
	/// </summary>
	/// <param name="n">the float to process.</param>
	
	public static float FloatAbs(float n)
	{
		return n > 0 ? n : -n;
	}
	
	/// <summary>
	/// Return an absolute int (positive)
	/// </summary>
	/// <returns>The abs.</returns>
	/// <param name="n">The int to process.</param>
	
	public static int IntAbs(int n)
	{
		return n > 0 ? n : -n;
	}
	
	/// <summary>
	///  Checks if an float is inside a range (A to B)
	/// </summary>
	/// <param name='range_a'>
	/// Range_a.
	/// </param>
	/// <param name='range_b'>
	/// Range_b.
	/// </param>
	/// <param name='value'>
	/// Value.
	/// </param>
	public static bool InRange(float range_a, float range_b, float value)
	{
		if (range_a > range_b)
		{
			float temp = range_b;
			range_b = range_a;
			range_a = temp;
		}
		return value <= range_b && value >= range_a;
	}
	
	/// <summary>
	/// Return a proportional float inside a range.
	/// Ex. a = 0f, b = 100f, percent = 0.5f - returns 50f
	/// Ex. a = 23f, b = 55f, percent = 0.5f - returns 39f (which is between 22 and 55)
	/// </summary>
	
	public static float GetFloatInRange(float a, float b, float percent)
	{
		return a + ((b - a) * percent);
	}
	
	
	/// <summary>
	/// Shuffle for builtin arrays
	/// </summary>
	/// <param name="arr">Array.</param>
	public static void Shuffle<T>(T[] arr)
	{
		int i = arr.Length;
		int j;
		T item;
		while (--i > 0)
		{
			item = arr[i];
			arr[i] = arr[j = Random.Range(0, (i + 1))];
			arr[j] = item;
		}
	}

	/// <summary>
	/// Shuffle for Lists
	/// </summary>
	/// <param name="list">List.</param>
	public static void Shuffle<T>(List<T> list)
	{
		int i = list.Count;
		int j;
		T item;
		while (--i > 0)
		{
			item = list[i];
			list[i] = list[j = Random.Range(0, (i + 1))];
			list[j] = item;
		}
	}



	

}


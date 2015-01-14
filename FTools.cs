using UnityEngine;
using System.Collections;
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
				output = array[i].ToString();
			}
			else
			{
				output = string.Concat(output, delimiter, array[i].ToString());
			}
		}
		return output;
	}

	public static Bounds ScreenOrthoBounds(Camera cam)
	{
		float camHeight = cam.orthographicSize * 2f;
		float scrRatio = (float)Screen.width/(float)Screen.height;
		return new Bounds(cam.transform.position, new Vector3(camHeight * scrRatio, camHeight, 0f));
	}


	public static bool IsInRange(Vector3 from, Vector3 to, float range)
	{
		return (to - from).sqrMagnitude < (range * range);
		
	}
	
	/// <summary>
	/// Converts a RGBA to a Color Object
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
	/// Index Of COLOR
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
	/// Index Of INT
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
	/// Index Of STRING
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
	/// Destruir itens na array passada
	/// </summary>
	/// <param name='items'>
	/// Items.
	/// </param>
	/// <param name='immediate'>
	/// Immediate.
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
	/// Mistura duas cores de acordo com a percentagem em Amount
	/// </summary>
	/// <param name="c1">C1.</param>
	/// <param name="c2">C2.</param>
	/// <param name="amount">Amount.</param>
	
	public static Color Blend(Color c1, Color c2, float amount)
	{
		int r = (int)((c1.r * amount) + c2.r * (1 - amount));
		int g = (int)((c1.g * amount) + c2.g * (1 - amount));
		int b = (int)((c1.b * amount) + c2.b * (1 - amount));
		return ToColor(r, g, b, 255);
	}
	
	/// <summary>
	/// Retorna campos dentro de outras classes
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
	/// Retorna um float absoluto
	/// </summary>
	/// <returns>The abs.</returns>
	/// <param name="n">N.</param>
	
	public static float FloatAbs(float n)
	{
		return n > 0 ? n : -n;
	}
	
	/// <summary>
	/// Retorna um int absoluto
	/// </summary>
	/// <returns>The abs.</returns>
	/// <param name="n">N.</param>
	
	public static int IntAbs(int n)
	{
		return n > 0 ? n : -n;
	}
	
	/// <summary>
	///  Verifica se um float (value) está dentro de uma determinada faixa (range_a e range_b)
	/// </summary>
	/// <returns>
	/// value está dentro da faixa ou não.
	/// </returns>
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
	/// Retorna um número dentro de uma faixa de acordo com a % passada
	/// </summary>
	/// <returns>
	/// The float in range.
	/// </returns>
	/// <param name='a'>
	/// A.
	/// </param>
	/// <param name='b'>
	/// B.
	/// </param>
	/// <param name='percent'>
	/// Percent.
	/// </param>
	
	public static float GetFloatInRange(float a, float b, float percent)
	{
		return a + ((b - a) * percent);
	}
	
	
	/// <summary>
	/// Shuffle para arrays
	/// </summary>
	/// <param name="list">List.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static void Shuffle<T>(T[] list)
	{
		int i = list.Length;
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


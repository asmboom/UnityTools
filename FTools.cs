using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

public static class FTools
{
	
	/// <summary>
	/// Generates random integer.
	/// </summary>
	/// <param name="minValue">
	/// Min value (inclusive).
	/// </param>
	/// <param name="maxValue">
	/// Max value (inclusive).
	/// </param>
	/// <returns>
	/// Random integer value between the min and max values (inclusive).
	/// </returns>
	/// <remarks>
	/// This methods overcomes the limitations of .NET Framework's Random
	/// class, which - when initialized multiple times within a very short
	/// period of time - can generate the same "random" number.
	/// </remarks>
	public static int RandomInt (int minValue, int maxValue)
	{
		// We will make up an integer seed from 4 bytes of this array.
		byte[] randomBytes = new byte[4];
		
		// Generate 4 random bytes.
		RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider ();
		rng.GetBytes (randomBytes);
		
		// Convert four random bytes into a positive integer value.
		int seed = ((randomBytes [0] & 0x7f) << 24) |
			(randomBytes [1] << 16) |
			(randomBytes [2] << 8) |
			(randomBytes [3]);
		
		// Now, this looks more like real randomization.
		System.Random random = new System.Random (seed);
		
		// Calculate a random number.
		return random.Next (minValue, maxValue + 1);
	}
	
	
	/// <summary>
	/// Returns a reduced version of the player name
	/// </summary>
	/// <returns>Shorter player Name</returns>
	/// <param name="fullName">Full name</param>
	/// <param name="abbreviateSecondName">If <c>true</c> includes the first letter of the player's second name / surname.</param>
	public static string GetShorterPlayerName (string fullName, bool abbreviateSecondName)
	{
		string pName = fullName;
		string[] pNameArray = fullName.Split (new string[]{" "}, System.StringSplitOptions.None);
		if (pNameArray.Length > 1) {
			if (abbreviateSecondName) {
				if (pNameArray [1].Length > 1) {
					pName = string.Concat (pNameArray [0], " ", pNameArray [1].ToCharArray () [0].ToString (), ".");
				} else {
					pName = string.Concat (pNameArray [0], " ", pNameArray [1].ToCharArray () [0].ToString ());
				}
			} else {
				pName = pNameArray [0];
			}
		}
		return pName;
	}
	
	/// <summary>
	/// Converts a hashTable to a Dictionary with support to generic types
	/// </summary>
	/// <returns>The dictionary</returns>
	/// <param name="hashTable">Hash table.</param>
	/// <typeparam name="KeyType">The Dictionary 1st type parameter.</typeparam>
	/// <typeparam name="ItemType">The Dictionary 2nd type parameter.</typeparam>
	public static Dictionary<KeyType,ItemType> HashtableToDictionary<KeyType,ItemType> (Hashtable hashTable)
	{
		Dictionary<KeyType, ItemType> dictionary = new Dictionary<KeyType, ItemType> ();
		foreach (KeyType key in hashTable.Keys) {
			dictionary.Add ((KeyType)key, (ItemType)hashTable [key]);
		}
		return dictionary;
	}
	
	
	
	/// <summary>
	/// Gets and / or add component.
	/// </summary>
	/// <returns>The component.</returns>
	/// <param name="gameObject">Game object.</param>
	/// <typeparam name="T">Component.</typeparam>
	public static T GetAddComponent<T> (GameObject gameObject) where T : UnityEngine.Component
	{
		T component = gameObject.GetComponent<T> ();
		if (component == null) {
			component = gameObject.AddComponent<T> ();
		}
		return component;
	}
	
	/// <summary>
	/// Convert an Array containing Generic type objects to a readable string
	/// *Remember to override your ToString() method
	/// Usage:
	/// Debug.Log(FTools.ArrayToString<MyType>(myTypeArray));
	/// </summary>
	public static string ArrayToString<T> (T[] array, string delimiter = " - ")
	{
		string output = string.Empty;
		int l = array.Length;
		for (int i = 0; i < l; ++i) {
			if (i == 0) {
				if (array [i] != null) {
					output = array [i].ToString ();
				} else {
					output = " * ";
				}
			} else {
				if (array [i] != null) {
					output = string.Concat (output, delimiter, array [i].ToString ());
				} else {
					output = string.Concat (output, delimiter, " * ");	
				}
			}
		}
		return output;
	}
	
	/// <summary>
	/// Get screen Bounds for an Orthographic Camera setup
	/// </summary>	
	public static Bounds ScreenOrthoBounds (Camera cam)
	{
		float camHeight = cam.orthographicSize * 2f;
		float scrRatio = (float)Screen.width / (float)Screen.height;
		return new Bounds (cam.transform.position, new Vector3 (camHeight * scrRatio, camHeight, 0f));
	}
	
	/// <summary>
	/// Optimized way (by using sqrMagnitude intead of Vector3.Distance) to know if an object is in range.
	/// </summary>
	/// <param name="range">The maximum allowed distance between these two Vectors</param>
	public static bool IsInRange (Vector3 objectPosition, Vector3 targetPosition, float range)
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
	public static Color ToColor (int R, int G, int B, int A)
	{
		float fullColor = 255.0f;
		Color c = new Color ((float)R / fullColor, (float)G / fullColor, (float)B / fullColor, (float)A / fullColor);
		return c;
	}
	
	/// <summary>
	/// Converts a string / hex color to Color
	/// </summary>
	/// <returns>The color.</returns>
	/// <param name="hex">Hex.</param>
	public static Color HexToColor (string hex)
	{
		byte r = byte.Parse (hex.Substring (0, 2), System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse (hex.Substring (2, 2), System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse (hex.Substring (4, 2), System.Globalization.NumberStyles.HexNumber);
		return new Color32 (r, g, b, 255);
	}
	
	/// <summary>
	/// Returns a float with n amount places after the decimal point
	/// </summary>
	/// <returns>A float with n places after the decimal point.</returns>
	/// <param name="myFloat">My float.</param>
	/// <param name="places">Places.</param>
	public static float GetDecimal (float myFloat, int places)
	{
		return Mathf.Ceil (myFloat * places) / places;
	}
	
	public static float Round (float value, int digits)
	{
		float mult = Mathf.Pow (10.0f, (float)digits);
		return Mathf.Round (value * mult) / mult;
	}
	
	/// <summary>
	/// Index Of COLOR for the faster builtin array
	/// </summary>
	/// <returns>The of.</returns>
	/// <param name="array">Array.</param>
	/// <param name="value">Value.</param>
	
	public static int IndexOf (Color[] array, Color value)
	{
		int l = array.Length;
		for (int i = 0; i < l; i++) {
			if (array [i] == value)
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
	
	public static int IndexOf (int[] array, int value)
	{
		int l = array.Length;
		for (int i = 0; i < l; i++) {
			if (array [i] == value)
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
	
	public static int IndexOf (string[] array, string value)
	{
		int l = array.Length;
		for (int i = 0; i < l; i++) {
			if (array [i] == value)
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
	public static void DestroyItemsIn (GameObject[] items, bool immediate = false)
	{
		int itemsAmount = items.Length;
		if (itemsAmount == 0)
			return;
		for (int i = 0; i < itemsAmount; ++i) {
			if (immediate) {
				MonoBehaviour.DestroyImmediate (items [i]);
			} else {
				MonoBehaviour.Destroy (items [i]);
			}
		}
	}
	
	/// <summary>
	/// Blend two colors by the "amount" parameter
	/// </summary>
	/// <param name="c1">Color 1.</param>
	/// <param name="c2">Color 2.</param>
	/// <param name="amount">Amount / percentage to mix.</param>
	
	public static Color Blend (Color c1, Color c2, float amount)
	{
		int r = (int)((c1.r * amount) + c2.r * (1 - amount));
		int g = (int)((c1.g * amount) + c2.g * (1 - amount));
		int b = (int)((c1.b * amount) + c2.b * (1 - amount));
		return ToColor (r, g, b, 255);
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
	public static T GetFieldValue<T> (object obj, string fieldName)
	{
		if (obj == null)
			throw new System.ArgumentNullException ("obj");
		
		FieldInfo field = obj.GetType ().GetField (fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
		
		if (field == null)
			throw new System.ArgumentException ("fieldName", "No such field was found.");
		
		if (!typeof(T).IsAssignableFrom (field.FieldType))
			throw new System.InvalidOperationException ("Field type and requested type are not compatible.");
		
		return (T)field.GetValue (obj);
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
	public static T GetFieldValue<T> (System.Type type, string fieldName)
	{
		if (type == null)
			throw new System.ArgumentNullException ("type");
		
		FieldInfo field = type.GetField (fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
		
		if (field == null)
			throw new System.ArgumentException ("fieldName", "No such field was found.");
		
		if (!typeof(T).IsAssignableFrom (field.FieldType))
			throw new System.InvalidOperationException ("Field type and requested type are not compatible.");
		
		return (T)field.GetValue (type);
	}
	
	
	/// <summary>
	/// Return an absolute float (positive)
	/// </summary>
	/// <param name="n">the float to process.</param>
	
	public static float FloatAbs (float n)
	{
		return n > 0 ? n : -n;
	}
	
	/// <summary>
	/// Return an absolute int (positive)
	/// </summary>
	/// <returns>The abs.</returns>
	/// <param name="n">The int to process.</param>
	
	public static int IntAbs (int n)
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
	public static bool InRange (float range_a, float range_b, float value)
	{
		if (range_a > range_b) {
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
	
	public static float GetFloatInRange (float a, float b, float percent)
	{
		return a + ((b - a) * percent);
	}
	
	
	/// <summary>
	/// Shuffle for builtin arrays
	/// </summary>
	/// <param name="arr">Array.</param>
	public static void Shuffle<T> (T[] arr)
	{
		int i = arr.Length;
		int j;
		T item;
		while (--i > 0) {
			item = arr [i];
			arr [i] = arr [j = RandomInt (0, i)];
			arr [j] = item;
		}
	}
	
	/// <summary>
	/// Shuffle for Lists
	/// </summary>
	/// <param name="list">List.</param>
	public static void Shuffle<T> (List<T> list)
	{
		int i = list.Count;
		int j;
		T item;
		while (--i > 0) {
			item = list [i];
			list [i] = list [j = RandomInt (0, i)];
			list [j] = item;
		}
	}
	
	/// <summary>
	/// Helper to activate or deactivate GameObjects from an array
	/// </summary>
	/// <param name="gameObjects">Game objects.</param>
	/// <param name="enable">If set to <c>true</c> enable.</param>
	public static void ManageGameObjects (GameObject[] gameObjects, bool enable)
	{
		int amount = gameObjects.Length;
		for (int i = 0; i < amount; ++i) {
			gameObjects [i].SetActive (enable);
		}
	}
	
	
	//<summary>
	//Return true if the e-mail is valid.
	//</summary>
	public static bool ValidateEmail (string email)
	{
		string emailPattern =
			@"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
			+ @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
			+ @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
			+ @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";
		
		if (email != null) {
			return Regex.IsMatch (email, emailPattern);
		} else
			return false;
	}
}


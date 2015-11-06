using UnityEngine;

public class FSystemInfo : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		Debug.Log("SYSTEM ANALISIS --------------------------");
		Debug.Log("DEVICE //// ");
		Debug.Log("deviceModel " + SystemInfo.deviceModel);
		Debug.Log("deviceName " + SystemInfo.deviceName);
		Debug.Log("deviceType " + SystemInfo.deviceType);
		Debug.Log("deviceUniqueIdentifier " + SystemInfo.deviceUniqueIdentifier);
		Debug.Log("GRAPHICS //// ");
		Debug.Log("graphicsDeviceID " + SystemInfo.graphicsDeviceID);
		Debug.Log("graphicsDeviceName " + SystemInfo.graphicsDeviceName);
		Debug.Log("graphicsDeviceVendor " + SystemInfo.graphicsDeviceVendor);
		Debug.Log("graphicsDeviceVendorID " + SystemInfo.graphicsDeviceVendorID);
		Debug.Log("graphicsDeviceVersion " + SystemInfo.graphicsDeviceVersion);
		Debug.Log("graphicsMemorySize " + SystemInfo.graphicsMemorySize);
		Debug.Log("graphicsShaderLevel " + SystemInfo.graphicsShaderLevel);
		Debug.Log("OS //// ");
		Debug.Log("operatingSystem " + SystemInfo.operatingSystem);
		Debug.Log("PROCESSOR //// ");
		Debug.Log("processorCount " + SystemInfo.processorCount);
		Debug.Log("processorType " + SystemInfo.processorType);
		Debug.Log("SUPPORT //// ");
		Debug.Log("supportedRenderTargetCount " + SystemInfo.supportedRenderTargetCount);
		Debug.Log("supports3DTextures " + SystemInfo.supports3DTextures);
		Debug.Log("supportsAccelerometer " + SystemInfo.supportsAccelerometer);
		Debug.Log("supportsComputeShaders " + SystemInfo.supportsComputeShaders);
		Debug.Log("supportsGyroscope " + SystemInfo.supportsGyroscope);
		Debug.Log("supportsImageEffects " + SystemInfo.supportsImageEffects);
		Debug.Log("supportsInstancing " + SystemInfo.supportsInstancing);
		Debug.Log("supportsLocationService " + SystemInfo.supportsLocationService);
		Debug.Log("supportsRenderTextures " + SystemInfo.supportsRenderTextures);
		Debug.Log("supportsShadows " + SystemInfo.supportsShadows);
		Debug.Log("supportsVibration " + SystemInfo.supportsVibration);
		Debug.Log("MEMORY //// ");
		Debug.Log("systemMemorySize " + SystemInfo.systemMemorySize);
		Debug.Log(" --------------------------");
	}
}


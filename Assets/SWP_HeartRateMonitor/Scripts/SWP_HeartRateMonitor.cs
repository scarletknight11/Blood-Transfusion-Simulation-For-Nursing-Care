//--------------ABOUT AND COPYRIGHT----------------------
// Copyright © 2013 SketchWork Productions Limited
//       support@sketchworkdev.com
//-------------------------------------------------------
using UnityEngine;
using System.Collections;

/// <summary>
/// This is the Heart Rate Monitor main script and controls every element of the control.
/// </summary>
public class SWP_HeartRateMonitor : MonoBehaviour
{
	public int BeatsPerMinute = 90; // Beats per minute.
	public bool FlatLine = false; // Initialise a flat line.
	
	public bool ShowBlip = true; // Show the blip circle at the front of the monitor line.
	public GameObject Blip; // The blip game object.
	public float BlipSize = 1f; // The size of the blip circle at the front of the line.
	public float BlipTrailStartSize = 0.2f; // The size of the monitor trail line nearest to the blip circle.
	public float BlipTrailEndSize = 0.1f; // The size of the monitor line at the end before it fades out.
	public float BlipMonitorWidth = 40f; // The actual width of the entire monitor control.
	public float BlipMonitorHeightModifier = 1f; // The actual height of the entire monitor control.
	public float ScaleAmount = 0.1f;
	
	public bool EnableSound = true;
	public float HeartVolume = 1f;
	public AudioClip Heart1;
	public AudioClip Heart2;
	public AudioClip Flatline;
	private bool bFlatLinePlayed = false;
	
	private bool EnableRealWave = false; //Work In Progress
	
	private float LineSpeed = 0.3f;
	private GameObject NewClone;
	private float TrailTime;
	private float BeatsPerSecond;
	private float LastUpdate = 0f;
	public Vector3 BlipOffset = Vector3.zero;
	private float DisplayXEnd;
	
	// Use this for initialization
	void Start()
	{
		BeatsPerSecond = 60f / BeatsPerMinute;
		//BlipOffset = new Vector3 (-4.93f/*transform.localPosition.x - (BlipMonitorWidth / 2)*/, transform.localPosition.y, 3.7f/*transform.localPosition.z*/);
		DisplayXEnd = BlipOffset.x + BlipMonitorWidth;
		CreateClone();
		TrailTime = NewClone.GetComponentInChildren<TrailRenderer>().time;
	}
	
	// Update is called once per frame
	void Update()
	{
		BeatsPerSecond = 60f / BeatsPerMinute;
		DisplayXEnd = BlipOffset.x + BlipMonitorWidth;
		
		if (NewClone.transform.localPosition.x > DisplayXEnd)
		{			
			if (NewClone != null)
			{
				GameObject OldClone = NewClone;
				StartCoroutine(WaitThenDestroy(OldClone));
			}
			
			CreateClone();
		}
		else if (!FlatLine)
			NewClone.transform.localPosition += new Vector3(BlipMonitorWidth * Time.deltaTime * LineSpeed, (!EnableRealWave ? Random.Range(-0.001f, 0.001f) : 0f), 0);//BlipMonitorWidth * Time.deltaTime * LineSpeed);
		else
		{
			NewClone.transform.localPosition += new Vector3(BlipMonitorWidth * Time.deltaTime * LineSpeed, 0, 0);// BlipMonitorWidth * Time.deltaTime * LineSpeed);
			
			if (!bFlatLinePlayed)
			{
				PlayHeartSound(3, HeartVolume);
				bFlatLinePlayed = true;
			}
		}
		
		if (BeatsPerMinute == 0 || FlatLine)
			LastUpdate = Time.time;
		else if (Time.time - LastUpdate >= BeatsPerSecond)
		{			
			LastUpdate = Time.time;
			StartCoroutine(PerformBlip());
		}
	}
	
	IEnumerator PerformBlip()
	{
		if (bFlatLinePlayed)
			bFlatLinePlayed = false;
		
		if (!EnableRealWave)
		{
			if (!bFlatLinePlayed)
				PlayHeartSound(1, HeartVolume);
			
			NewClone.transform.localPosition = new Vector3(NewClone.transform.localPosition.x, ((ScaleAmount * 10f) * BlipMonitorHeightModifier) + Random.Range(0f, ((ScaleAmount * 2f) * BlipMonitorHeightModifier)) + BlipOffset.y, NewClone.transform.localPosition.z);
			yield return new WaitForSeconds(0.03f);		
			NewClone.transform.localPosition = new Vector3(NewClone.transform.localPosition.x, ((ScaleAmount * -5f) * BlipMonitorHeightModifier) - Random.Range(0f, ((ScaleAmount * 10f) * BlipMonitorHeightModifier)) + BlipOffset.y, NewClone.transform.localPosition.z);
			yield return new WaitForSeconds(0.02f);		
			NewClone.transform.localPosition = new Vector3(NewClone.transform.localPosition.x, ((ScaleAmount * 3f) * BlipMonitorHeightModifier) + Random.Range(0f, ((ScaleAmount * 2f) * BlipMonitorHeightModifier)) + BlipOffset.y, NewClone.transform.localPosition.z);
			yield return new WaitForSeconds(0.02f);		
			NewClone.transform.localPosition = new Vector3(NewClone.transform.localPosition.x, ((ScaleAmount * 2f) * BlipMonitorHeightModifier) + Random.Range(0f, ((ScaleAmount * 1f) * BlipMonitorHeightModifier)) + BlipOffset.y, NewClone.transform.localPosition.z);
			yield return new WaitForSeconds(0.02f);		
			
			NewClone.transform.localPosition = new Vector3(NewClone.transform.localPosition.x, 0f + BlipOffset.y, NewClone.transform.localPosition.z);
			
			yield return new WaitForSeconds(0.2f);		
			
			if (!bFlatLinePlayed)
				PlayHeartSound(2, HeartVolume);
		}
		else
		{
			NewClone.transform.localPosition = new Vector3(NewClone.transform.localPosition.x, ((ScaleAmount * 1f) * BlipMonitorHeightModifier) + BlipOffset.y, NewClone.transform.localPosition.z);
			yield return new WaitForSeconds(0.01f);		
			NewClone.transform.localPosition = new Vector3(NewClone.transform.localPosition.x, ((ScaleAmount * 1.5f) * BlipMonitorHeightModifier) + BlipOffset.y, NewClone.transform.localPosition.z);
			yield return new WaitForSeconds(0.01f);		
			NewClone.transform.localPosition = new Vector3(NewClone.transform.localPosition.x, ((ScaleAmount * 2f) * BlipMonitorHeightModifier) + BlipOffset.y, NewClone.transform.localPosition.z);
			yield return new WaitForSeconds(0.03f);		
			NewClone.transform.localPosition = new Vector3(NewClone.transform.localPosition.x, ((ScaleAmount * 1.2f) * BlipMonitorHeightModifier) + BlipOffset.y, NewClone.transform.localPosition.z);
			yield return new WaitForSeconds(0.01f);		
			NewClone.transform.localPosition = new Vector3(NewClone.transform.localPosition.x, ((ScaleAmount * 1f) * BlipMonitorHeightModifier) + BlipOffset.y, NewClone.transform.localPosition.z);
			yield return new WaitForSeconds(0.01f);		
			
			NewClone.transform.localPosition = new Vector3(NewClone.transform.localPosition.x, ((ScaleAmount * 0f) * BlipMonitorHeightModifier) + BlipOffset.y, NewClone.transform.localPosition.z);
			yield return new WaitForSeconds(0.1f);		
			
			if (!bFlatLinePlayed)
				PlayHeartSound(1, HeartVolume);
			
			NewClone.transform.localPosition = new Vector3(NewClone.transform.localPosition.x, ((ScaleAmount * -1f) * BlipMonitorHeightModifier) + BlipOffset.y, NewClone.transform.localPosition.z);
			yield return new WaitForSeconds(0.01f);		
			NewClone.transform.localPosition = new Vector3(NewClone.transform.localPosition.x, ((ScaleAmount * 10f) * BlipMonitorHeightModifier) + BlipOffset.y, NewClone.transform.localPosition.z);
			yield return new WaitForSeconds(0.03f);		
			NewClone.transform.localPosition = new Vector3(NewClone.transform.localPosition.x, ((ScaleAmount * -3f) * BlipMonitorHeightModifier) + BlipOffset.y, NewClone.transform.localPosition.z);
			yield return new WaitForSeconds(0.02f);		
			
			NewClone.transform.localPosition = new Vector3(NewClone.transform.localPosition.x, ((ScaleAmount * 0f) * BlipMonitorHeightModifier) + BlipOffset.y, NewClone.transform.localPosition.z);
			yield return new WaitForSeconds(0.2f);		
			
			if (!bFlatLinePlayed)
				PlayHeartSound(2, HeartVolume);
			
			NewClone.transform.localPosition = new Vector3(NewClone.transform.localPosition.x, ((ScaleAmount * 1.0f) * BlipMonitorHeightModifier) + BlipOffset.y, NewClone.transform.localPosition.z);
			yield return new WaitForSeconds(0.01f);		
			NewClone.transform.localPosition = new Vector3(NewClone.transform.localPosition.x, ((ScaleAmount * 2.3f) * BlipMonitorHeightModifier) + BlipOffset.y, NewClone.transform.localPosition.z);
			yield return new WaitForSeconds(0.01f);		
			NewClone.transform.localPosition = new Vector3(NewClone.transform.localPosition.x, ((ScaleAmount * 2.5f) * BlipMonitorHeightModifier) + BlipOffset.y, NewClone.transform.localPosition.z);
			yield return new WaitForSeconds(0.05f);		
			NewClone.transform.localPosition = new Vector3(NewClone.transform.localPosition.x, ((ScaleAmount * 2.3f) * BlipMonitorHeightModifier) + BlipOffset.y, NewClone.transform.localPosition.z);
			yield return new WaitForSeconds(0.01f);		
			NewClone.transform.localPosition = new Vector3(NewClone.transform.localPosition.x, ((ScaleAmount * 2.0f) * BlipMonitorHeightModifier) + BlipOffset.y, NewClone.transform.localPosition.z);
			yield return new WaitForSeconds(0.01f);		
			NewClone.transform.localPosition = new Vector3(NewClone.transform.localPosition.x, ((ScaleAmount * 1f) * BlipMonitorHeightModifier) + BlipOffset.y, NewClone.transform.localPosition.z);
			yield return new WaitForSeconds(0.01f);		
			
			NewClone.transform.localPosition = new Vector3(NewClone.transform.localPosition.x, 0f + BlipOffset.y, NewClone.transform.localPosition.z);
		}
	}
	
	void CreateClone()
	{
		Vector3 BlipStart = this.transform.position + BlipOffset;
		NewClone = Instantiate(Blip, BlipStart, Quaternion.identity) as GameObject;
		NewClone.transform.parent = gameObject.transform;
		
		NewClone.GetComponentInChildren<TrailRenderer>().startWidth = BlipTrailStartSize;
		NewClone.GetComponentInChildren<TrailRenderer>().endWidth = BlipTrailEndSize;
		NewClone.transform.localScale = new Vector3(BlipSize,BlipSize,BlipSize);
		
		if (ShowBlip)
			NewClone.GetComponent<MeshRenderer>().enabled = true;
		else
			NewClone.GetComponent<MeshRenderer>().enabled = false;
	}
	
	IEnumerator WaitThenDestroy(GameObject OldClone)
	{
		OldClone.GetComponent<MeshRenderer>().enabled = false;
		yield return new WaitForSeconds(TrailTime);
		DestroyObject(OldClone);
	}
	
	void PlayHeartSound(int iSoundType, float fSoundVolume)
	{
		if (!EnableSound)
			return;
		
		if (iSoundType == 1)
			GetComponent<AudioSource>().PlayOneShot(Heart1, fSoundVolume);
		else if (iSoundType == 2)
			GetComponent<AudioSource>().PlayOneShot(Heart2, fSoundVolume);
		else if (iSoundType == 3)
			GetComponent<AudioSource>().PlayOneShot(Flatline, fSoundVolume);
	}
}
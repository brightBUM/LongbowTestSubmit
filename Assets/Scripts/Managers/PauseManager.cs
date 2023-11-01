using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : MonoBehaviour {
	
	public AudioMixerSnapshot paused;
	public AudioMixerSnapshot unpaused;
	
	[SerializeField] Slider musicSlider;
	[SerializeField] Slider sFxSlider;
	[SerializeField] Toggle soundToggle;	
	
	Canvas canvas;
	
	void Start()
	{
		canvas = GetComponent<Canvas>();
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{ 
			
			canvas.enabled = !canvas.enabled;
            if (!canvas.enabled)
            {
				//save before closing pause menu
                SavePreferences();
            }
            else
            {
                //load user setting from save data
                LoadPreferences();
            }
            Pause();
		}
	}
	
	public void Pause()
	{
		Time.timeScale = Time.timeScale == 0 ? 1 : 0;
		Lowpass ();
		
	}
	public void SavePreferences()
	{
		var saveInstance = GameSaveLoad.instance;
        saveInstance.SetValue(saveInstance.MUSIC_KEY,musicSlider.value.ToString());
        saveInstance.SetValue(saveInstance.SFX_KEY,sFxSlider.value.ToString());
		var soundValue = soundToggle.isOn ? 1 : 0;
        saveInstance.SetValue(saveInstance.SOUND_KEY,soundValue.ToString());

		saveInstance.Save();
	}
	private void LoadPreferences()
	{
        var saveInstance = GameSaveLoad.instance;
		musicSlider.value = float.Parse(saveInstance.GetValue(saveInstance.MUSIC_KEY)); 
		sFxSlider.value = float.Parse(saveInstance.GetValue(saveInstance.SFX_KEY)); 
		var soundValue = saveInstance.GetValue(saveInstance.SOUND_KEY);
		soundToggle.isOn = soundValue == "1" ? true : false;
    }
    void Lowpass()
	{
		if (Time.timeScale == 0)
		{
			paused.TransitionTo(.01f);
		}
		
		else
			
		{
			unpaused.TransitionTo(.01f);
		}
	}
	
	public void Quit()
	{
		#if UNITY_EDITOR 
		EditorApplication.isPlaying = false;
		#else 
		Application.Quit();
		#endif
	}
}

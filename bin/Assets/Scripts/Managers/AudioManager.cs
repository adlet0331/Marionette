using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //인스펙터 창에 커스텀 클래스를 강제로 띄워준다. (직렬화?)
public class Sound {
    public string name; //사운드 이름

    public AudioClip clip; //사운드 파일

    [SerializeField]private float volume;
    [SerializeField]private bool loop;
}

public class AudioManager : MonoBehaviour {
    [SerializeField] private Sound[] effects;
    [SerializeField] private Sound[] bgms;

    public static AudioManager instance;

    #region Singleton
    private void Awake() {
        if (instance != null) {
            Destroy(this.gameObject);
        }
        else {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }
    #endregion Singleton

    [SerializeField] private float MasterVolume, BgmVolume, EffectVolume;
    
    [SerializeField] private bool isPlaying;
    private AudioSource audioPlayer;
    private void Start() {
        audioPlayer = GetComponent<AudioSource>();
    }

    public void ChangeBGM(int index) {
        
    }

    public void PlayCurrentBGM() {
        
    }
}

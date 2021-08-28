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

public class AudioManager : Singleton<AudioManager> {
    [SerializeField] private Sound[] effects;
    [SerializeField] private Sound[] bgms;

    [SerializeField] private float MasterVolume, BgmVolume, EffectVolume;

    [SerializeField] private bool isPlaying;
    private AudioSource audioPlayer;

    protected AudioManager(){}

    private void Start() {
        audioPlayer = GetComponent<AudioSource>();
        ChangeBGM(0);
    }

    public void ChangeBGM(int index) {
        
    }

    public void PlayCurrentBGM() {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 환경설정 창에서 마스터 볼륨, BGM 볼륨, SE 볼륨 조절
 * BGM 플레이, SE 플레이 조절 - Audio Source는 플레이어한테 부착
 * 
 */
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

    [SerializeField] private float MasterVolume, BgmVolume, SEVolume;

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

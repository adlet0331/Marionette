using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //인스펙터 창에 커스텀 클래스를 강제로 띄워준다. (직렬화?)
public class Sound {
    public string name; //사운드 이름

    public AudioClip clip; //사운드 파일
    private AudioSource source; //사운드 플레이어

    public float Volumn;
    public bool loop;
    public void SetSource(AudioSource _source) {
        source = _source;
        source.clip = clip;
        source.loop = loop;
        source.volume = Volumn;
    }
    public void SetVolumn() {
        source.volume = Volumn;
    }
    public void Play() {
        source.Play();
    }
    public void Stop() {
        source.Stop();
    }
    public void SetLoop() {
        source.loop = true;
    }
    public void SetLoopCancel() {
        source.loop = false;
    }
}

public class AudioManager : MonoBehaviour {
    [SerializeField] private Sound[] effects;
    [SerializeField] private Sound[] bgms;

    public float MasterVolume, BgmVolume, EffectVolume;
    public AudioSource audioPlayer;

    public bool isPlaying;
    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }
    private void Start() {
        for (int i = 0; i < bgms.Length; i++) {
            GameObject soundObject = new GameObject("사운드 파일 이름 : " + i + " = " + bgms[i].name);
            bgms[i].SetSource(soundObject.AddComponent<AudioSource>());
            soundObject.transform.SetParent(this.transform);
        }
    }

    public void ChangeBGM(int index) {
        
    }

    public void PlayCurrentBGM() {
        
    }
}

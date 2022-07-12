using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
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
        [SerializeField] private Slider MasterSlider;
        [SerializeField] private Text MasterText;
        [SerializeField] private Slider BgmSlider;
        [SerializeField] private Text BgmText;
        [SerializeField] private Slider SESlider;
        [SerializeField] private Text SEText;

        [SerializeField] private Sound[] effects;
        [SerializeField] private Sound[] bgms;

        [SerializeField] private double masterVolume, bgmVolume, sEVolume;

        [SerializeField] private bool isPlaying;
        private AudioSource audioPlayer;

        private void Start() {
            // audioPlayer = GetComponent<AudioSource>();
            // MasterSlider.onValueChanged.AddListener(delegate { updateMasterVolume(); });
            // BgmSlider.onValueChanged.AddListener(delegate { updateBgmVolume(); });
            // SESlider.onValueChanged.AddListener(delegate { updateSEVolume(); });
            // ChangeBGM(0);
            // 
            // updateMasterVolume();
            // updateBgmVolume();
            // updateSEVolume();
        }

        public void ChangeBGM(int index) {
        
        }

        public void PlayCurrentBGM() {
        
        }

        private void updateMasterVolume()
        {
            masterVolume = Math.Round(MasterSlider.value, 2);
            MasterText.text = (masterVolume * 100).ToString();
        }
        private void updateBgmVolume()
        {
            bgmVolume = Math.Round(BgmSlider.value, 2);
            BgmText.text = (bgmVolume * 100).ToString();
        }
        private void updateSEVolume()
        {
            sEVolume = Math.Round(MasterSlider.value, 2);
            SEText.text = (sEVolume* 100).ToString();
        }
    }
}
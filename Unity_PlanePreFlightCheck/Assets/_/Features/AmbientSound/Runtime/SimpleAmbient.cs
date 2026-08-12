using System;
using System.Collections.Generic;
using Foundation.Runtime;
using UnityEngine;
using UnityEngine.Audio;

namespace AmbientSound.Runtime
{
    [Serializable]
    public class AmbientClipData
    {
        #region Audio Settings
        [Header("Audio Files")]
        public AudioClip clip;
        [Tooltip("Information automatique.")]
        public float clipDuration;
        [Range(0f, 1f)] public float volumeScale = 1f;
        [Range(0f, 0.5f)] public float volumeRandomness = 0.1f;
        [Range(0f, 1.1f)] public float reverbMix = 0.8f;
        #endregion

        #region Spatial & Pitch
        [Header("Spatial Variations")]
        [Range(-1f, 1f)] public float basePan = 0f;
        [Range(0f, 1f)] public float panRandomness = 0.2f;

        [Header("Passing Effect")]
        public bool isPassingEffect = false;
        public bool passLeftToRight = true;

        [Header("Pitch")]
        [Range(0.5f, 1.5f)] public float basePitch = 1f;
        [Range(0f, 0.3f)] public float pitchRandomness = 0f;
        #endregion

        #region Timing
        [Header("Timing")]
        public float minDelay = 5f;
        public float maxDelay = 15f;
        public float cooldown = 20f;
        [HideInInspector] public float lastPlayedTime = -100f;
        #endregion
    }

    public class SimpleAmbient : FBehaviour
    {
        #region Publics
        [Header("Global Configuration")]
        public AudioMixerGroup outputGroup;
        public float startDelayMin = 1f;
        public float startDelayMax = 3f;

        [Header("Looping Background 1")]
        public bool playLoop1 = false;
        public AudioClip backgroundLoop1;
        [Range(0f, 1f)] public float loopVolume1 = 0.5f;

        [Header("Looping Background 2")]
        public bool playLoop2 = false;
        public AudioClip backgroundLoop2;
        [Range(0f, 1f)] public float loopVolume2 = 0.5f;

        [Header("Random Ambient Clips")]
        public List<AmbientClipData> ambientClips = new List<AmbientClipData>();
        #endregion

        #region Privates
        private AudioSource _mainSource;
        private AudioSource _loopSource1;
        private AudioSource _loopSource2;
        private float _timer;
        private float _nextTriggerTime;

        private bool _isPassActive;
        private float _passStartTime;
        private float _passDuration;
        private bool _passDirectionLR;
        #endregion

        #region Unity API
        private void OnValidate()
        {
            if (ambientClips == null) return;
            foreach (var data in ambientClips)
            {
                if (data.clip != null) data.clipDuration = data.clip.length;
                else data.clipDuration = 0f;
            }
        }

        private void Start()
        {
            _mainSource = GetComponent<AudioSource>();
            if (_mainSource is null)
            {
                Debug.LogWarning($"[SimpleAmbient] AudioSource manquante sur {gameObject.name}", gameObject);
                return;
            }

            _mainSource.outputAudioMixerGroup = outputGroup;
            _mainSource.spatialBlend = 0f; 
            _mainSource.playOnAwake = false;

            // Initialisation des deux boucles
            if (playLoop1 && backgroundLoop1 != null) 
                _loopSource1 = CreateLoopSource(backgroundLoop1, loopVolume1);
            
            if (playLoop2 && backgroundLoop2 != null) 
                _loopSource2 = CreateLoopSource(backgroundLoop2, loopVolume2);

            _nextTriggerTime = UnityEngine.Random.Range(startDelayMin, startDelayMax);
        }

        private void Update()
        {
            if (ambientClips == null || ambientClips.Count == 0) return;

            _timer += Time.deltaTime;
            if (_timer >= _nextTriggerTime && !_mainSource.isPlaying) ExecuteAmbientTrigger();

            if (_isPassActive) UpdatePassingEffect();
        }
        #endregion

        #region Internal Logic
        private AudioSource CreateLoopSource(AudioClip clip, float volume)
        {
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            newSource.outputAudioMixerGroup = outputGroup;
            newSource.clip = clip;
            newSource.loop = true;
            newSource.spatialBlend = 0f;
            newSource.volume = volume;
            newSource.Play();
            return newSource;
        }

        private void ExecuteAmbientTrigger()
        {
            List<AmbientClipData> availableClips = ambientClips.FindAll(x => Time.time >= x.lastPlayedTime + x.cooldown);
            if (availableClips.Count == 0) return;

            AmbientClipData selectedData = availableClips[UnityEngine.Random.Range(0, availableClips.Count)];
            
            // Pitch Randomness (ne s'applique que si > 0)
            float finalPitch = selectedData.basePitch;
            if (selectedData.pitchRandomness > 0f)
                finalPitch += UnityEngine.Random.Range(-selectedData.pitchRandomness, selectedData.pitchRandomness);

            _mainSource.pitch = finalPitch;
            _mainSource.reverbZoneMix = selectedData.reverbMix;

            // Spatialisation
            if (selectedData.isPassingEffect)
            {
                _isPassActive = true;
                _passStartTime = Time.time;
                _passDuration = selectedData.clip.length;
                _passDirectionLR = selectedData.passLeftToRight;
                _mainSource.panStereo = _passDirectionLR ? -1.0f : 1.0f;
            }
            else
            {
                _isPassActive = false;
                _mainSource.panStereo = Mathf.Clamp(selectedData.basePan + UnityEngine.Random.Range(-selectedData.panRandomness, selectedData.panRandomness), -1f, 1f);
            }

            _mainSource.PlayOneShot(selectedData.clip, selectedData.volumeScale);
            selectedData.lastPlayedTime = Time.time;
            _timer = 0f;
            _nextTriggerTime = UnityEngine.Random.Range(selectedData.minDelay, selectedData.maxDelay);
        }

        private void UpdatePassingEffect()
        {
            float progress = (Time.time - _passStartTime) / _passDuration;
            if (progress <= 1f)
            {
                float startPan = _passDirectionLR ? -1.0f : 1.0f;
                float endPan = _passDirectionLR ? 1.0f : -1.0f;
                _mainSource.panStereo = Mathf.Lerp(startPan, endPan, progress);
            }
            else _isPassActive = false;
        }
        #endregion
    }
}
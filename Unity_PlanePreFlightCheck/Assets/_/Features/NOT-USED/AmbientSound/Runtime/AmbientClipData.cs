using System;
using UnityEngine;

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
}
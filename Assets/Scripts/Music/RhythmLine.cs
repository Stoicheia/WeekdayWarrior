﻿using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Music
{
    [RequireComponent(typeof(AudioSource))]
    public class RhythmLine : MonoBehaviour
    {
        [SerializeField] private AudioClip audio;
        [SerializeField] private TextAsset script;
        [SerializeField] private string scriptField;
        private AudioSource _audioSource;
        private List<RhythmInstruction> _instructions;
        private RhythmParser _parser;
        private int _ptr;

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Parse(scriptField);
                foreach (var s in _instructions)
                {
                    Debug.Log(s.ToString());
                }
            }
        }

        private void Initialize()
        {
            _parser = new RhythmParser();
            _audioSource = GetComponent<AudioSource>();
        }
        
        private void Parse(string text)
        {
            Initialize();
            _instructions = _parser.ParseTextToInstructions(text);
        }

        public void Play()
        {
            _audioSource.Play();
        }
    }
}
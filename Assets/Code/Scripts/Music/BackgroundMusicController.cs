using System;
using System.Collections;
using UnityEngine;
using System.Linq;
using ModestTree;
using Random = UnityEngine.Random;

namespace SustainTheStrain.Music
{
    public class BackgroundMusicController : MonoBehaviour
    {
        public static BackgroundMusicController instance = null; // Экземпляр объекта

        [SerializeField] private AudioSource _source;
        [SerializeField] private AudioClip[] _clips;

        private int _lastSongIndex = -1;
        
        private void Awake()
        {
            PlayRandomSong();
        }

        private IEnumerator Start()
        {
            if (instance == null) { // Экземпляр менеджера был найден
                DontDestroyOnLoad(this);
                instance = this; // Задаем ссылку на экземпляр объекта
            } else if(instance == this){ // Экземпляр объекта уже существует на сцене
                Destroy(gameObject); // Удаляем объект
            }
            
            while (true)
            {
                yield return new WaitForSeconds(3);
                yield return new WaitUntil(() => _source.isPlaying == false);
                PlayRandomSong();
            }
        }

        private void PlayRandomSong()
        {
            if (instance == this)
            {
                var indexes = Enumerable.Range(0, _clips.Length).Where(x => x != _lastSongIndex).ToArray();
                int index = Random.Range(0, indexes.Length);
                var clip = _source.clip = _clips[index];
                _lastSongIndex = _clips.IndexOf(clip);
                _source.Play(2);
            }
        }

    }
}
/*
    idとoffsetを関連付ける必要がある
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundPlayer : MonoBehaviour {

    public enum Part{
        Bass,
        Chord,
        Drum,
        Melody
    }

    [SerializeField] private GameObject _soundManager;
    AudioSource _audioSource;
    [SerializeField]private GameObject _tuio;
    private FiducialController _fc;

    void Start () {
        Part _part = new Part();
        _audioSource = this.GetComponent<AudioSource>();
        _fc = _tuio.GetComponent<FiducialController>();
        StartCoroutine("Sample");
    }
	
    void Update () {
        Debug.Log(_fc.isConectted);
    }

    private IEnumerator Sample()
    {

        while (true)
        {
            _audioSource = this.GetComponent<AudioSource>();
            switch (this.gameObject.name)
            {

                case "Bass":
                    var id = (int)Part.Bass * 3 + Random.Range(0, 3);
                    //id = (_fc.MarkerID + 1) + (int)Part.Bass * 3;
                    //map to -> 0~2

                    _audioSource.clip = _soundManager.GetComponent<MusicManager>().audioClips[id];
                    break;

                case "Chord":
                    id = (int)Part.Chord * 3 + Random.Range(0, 3);
                    //id = (_fc.MarkerID % 12 + 1) + (int)Part.Chord;
                    //map to -> 0~2
                    _audioSource.clip = _soundManager.GetComponent<MusicManager>().audioClips[id];
                    break;

                case "Drum":
                    id = (int)Part.Drum * 3 + Random.Range(0, 3);
                    //id = (_fc.MarkerID % 24 + 1) + (int)Part.Drum * 3;
                    //map to -> 0~2
                    _audioSource.clip = _soundManager.GetComponent<MusicManager>().audioClips[id];
                    break;

                case "Melody":
                    id = (int)Part.Melody * 3 + Random.Range(0, 2);
                    //id = (_fc.MarkerID % 36 +) 1 + (int)Part.Melody * 3;
                    //map to -> 0~2
                    _audioSource.clip = _soundManager.GetComponent<MusicManager>().audioClips[id];
                    break;
            }

            if (_fc.isConectted)
            {
                _audioSource.Play();
            }

            yield return new WaitForSeconds(_audioSource.clip.length);
        }
    }
}

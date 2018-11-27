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
    [SerializeField] private GameObject[] _tuio = new GameObject[3];
    private FiducialController[] _fc = new FiducialController[3];
    private int id = 0;

    void Start () {
        Part _part = new Part();
        _audioSource = this.GetComponent<AudioSource>();
        for (int i = 0; i < 3; i++)
        {
            _fc[i] = _tuio[i].GetComponent<FiducialController>();
        }
        Debug.Log(_audioSource.clip.length + "sec");
        StartCoroutine("Sample");
    }
	
    void Update () {

    }

    private IEnumerator Sample()
    {

        while (true)
        {
            FiducialController _buffer = _fc[0];
            Debug.Log("Check-------------");
            for (int i = 0; i < 3; i++)
            {
                Debug.Log(i + "bool : " + _fc[i].isConectted);
                if (_fc[i].isConectted == true)
                {
                    id = _fc[i].MarkerID;
                    _buffer = _fc[i];
                    Debug.Log("isConnected" + _fc[i].MarkerID);
                    break;
                }
            }

            _audioSource = this.GetComponent<AudioSource>();
            switch (this.gameObject.name)
            {

                case "Bass":
                    //var id = (int)Part.Bass * 3 + Random.Range(0, 3);
                    id = (id) + 0 * 3;
                    //map to -> 0~2

                    //_audioSource.clip = _soundManager.GetComponent<MusicManager>().audioClips[id];
                    break;

                case "Chord":
                    //id = (int)Part.Chord * 3 + Random.Range(0, 3);
                    id = (id % 12) + 1 * 3;
                    //map to -> 0~2
                    //_audioSource.clip = _soundManager.GetComponent<MusicManager>().audioClips[id];
                    break;

                case "Drum":
                    //id = (int)Part.Drum * 3 + Random.Range(0, 3);
                    id = (id % 24) + 2* 3;
                    //map to -> 0~2
                    //_audioSource.clip = _soundManager.GetComponent<MusicManager>().audioClips[id];
                    break;

                case "Melody":
                    //id = (int)Part.Melody * 3 + Random.Range(0, 2);
                    id = (id % 36) + 3 * 3;
                    //map to -> 0~2
                    //_audioSource.clip = _soundManager.GetComponent<MusicManager>().audioClips[id];
                    break;
            }

            if (_buffer.isConectted)
            {
                Debug.Log("currentID" + id);
                _audioSource.clip = _soundManager.GetComponent<MusicManager>().audioClips[id];
                _audioSource.Play();
            }

            yield return new WaitForSeconds(_audioSource.clip.length);
        }
    }
}

using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

namespace PlayerManger
{
    public class PlayerRanking : MonoBehaviour
    {
        public static PlayerRanking Instance;

        public Transform SpawnTransform;

        public List<GameObject> _childPlayerObject;
        public GameObject[] Episodes;
        public GameObject SpawnObject;
        public GameObject LosePanel;
        public GameObject WinPanel;

        private void Awake() => SaveLoadSytem();

        private void Start() => Instance = this;

        public void AddObject()
        {
            var position = _childPlayerObject.Count == 0
                ? SpawnTransform.position
                : _childPlayerObject[^1].transform.position;
            GameObject obj = Instantiate(SpawnObject, position, SpawnTransform.rotation);
            obj.transform.localPosition =
                new Vector3(position.x, position.y,
                    position.z == transform.position.z ? position.z - 0.5f : position.z - 0.2f);
            _childPlayerObject.Add(obj);
        }

        public void DeleteObject()
        {
            if (_childPlayerObject.Count <= 0)
            {
                DeadPlayerWaiting();
                return;
            }

            Destroy(_childPlayerObject[^1]);
            _childPlayerObject.RemoveAt(_childPlayerObject.Count - 1);
        }

        public void ObjectTransformDelete()
        {
            for (int i = 0; i < _childPlayerObject.Count; i++)
            {
                Destroy(_childPlayerObject[i].transform.GetComponent<Rigidbody>());
                _childPlayerObject[i].transform
                    .DOMove(
                        new Vector3(UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(5, 10),
                            _childPlayerObject[i].transform.position.z), 2f).SetEase(Ease.Flash);
            }
        }

        public void AgainButton()
        {
            SceneManager.LoadScene(0);
            PlayerPrefs.SetInt("Scene", 1);
        }

        public void NextButton()
        {
            SceneManager.LoadScene(0);
            PlayerPrefs.SetInt("Scene", 2);
        }

        private void SaveLoadSytem()
        {
            PlayerMovement.Movement = false;

            if (PlayerPrefs.GetInt("Scene") == 0)
            {
                PlayerPrefs.SetInt("SaveEpisode", UnityEngine.Random.Range(0, Episodes.Length));
                Episodes[PlayerPrefs.GetInt("SaveEpisode")].SetActive(true);
            }
            else if (PlayerPrefs.GetInt("Scene") == 1)
                Episodes[PlayerPrefs.GetInt("SaveEpisode")].SetActive(true);
            else if (PlayerPrefs.GetInt("Scene") == 2)
            {
                int randomNumber;
                for (;;)
                {
                    randomNumber = UnityEngine.Random.Range(0, Episodes.Length);
                    if (PlayerPrefs.GetInt("SaveEpisode") != randomNumber)
                    {
                        PlayerPrefs.SetInt("SaveEpisode", randomNumber);
                        Episodes[PlayerPrefs.GetInt("SaveEpisode")].SetActive(true);
                        break;
                    }
                }
            }
        }

        private void OnApplicationQuit()
        {
            PlayerPrefs.SetInt("Scene", 0);
            PlayerPrefs.SetInt("SaveEpisode", 0);
        }

        public void DeadPlayerWaiting() => Invoke(nameof(DeadPlayer), 0.5f);
        private void DeadPlayer() => LosePanel.SetActive(true);

        public void WinPlayerWaiting() => Invoke(nameof(WinPlayer), 0.5f);

        private void WinPlayer()
        {
            WinPanel.SetActive(true);
            WinPanel.transform.Find("WinScore").GetComponent<TMP_Text>().text = "Score : " +
                (_childPlayerObject.Count < 10
                    ? _childPlayerObject.Count * _childPlayerObject.Count
                    : _childPlayerObject.Count * 10).ToString();
        }
    }
}
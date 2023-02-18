using System.Collections;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using DG.Tweening;
using PlayerManger;
using PlayerManger.Interfaces;
using UnityEngine;

public class GameOverProcess : MonoBehaviour, IProcess
{
    public GameObject MainCoinObject;
    public GameObject FinishCamera;
    public GameObject[] TargetObject;
    public Transform ObjectGathering;

    public async Task<string> CollisionEffective(GameObject obj)
    {
        Humen humen = new Humen()
        {
            Age = 18,
            Name = "Mesut"
        };

        string a = JsonUtility.ToJson(humen);
        File.WriteAllText("Mesut,json", a);

        string b = File.ReadAllText("Mesut.json");
        Humen emen = JsonUtility.FromJson<Humen>(b);

        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage message = await client.GetAsync("");
            string asa = await message.Content.ReadAsStringAsync();

            JsonUtility.FromJson<Humen>(asa);
        }

        //
        // if (obj.name != "Coin") return;
        // FinishCamera.SetActive(true);
        // MainCoinObject.transform.GetChild(0).transform.gameObject.SetActive(false);
        // Destroy(MainCoinObject.GetComponent<PlayerMovement>());
        // MainCoinObject.transform.Rotate(90 * 1, 0, 0);
        // if (PlayerRanking.Instance._childPlayerObject.Count >= 1)
        //     StartCoroutine(WaitingObjectMove());
    }

    private IEnumerator WaitingObjectMove()
    {
        for (int i = 0; i < PlayerRanking.Instance._childPlayerObject.Count; i++)
        {
            Transform tran = TargetObject[i < TargetObject.Length ? i : 9].transform.GetChild(0)
                .transform;
            PlayerRanking.Instance._childPlayerObject[i].transform.DOMove(ObjectGathering.transform.position, 0.1f);
            Destroy(PlayerRanking.Instance._childPlayerObject[i].GetComponent<PlayerMovement>());
            yield return new WaitForSeconds(0.1f);
            PlayerRanking.Instance._childPlayerObject[i].transform.Rotate(90 * 1, 0, 0);
            PlayerRanking.Instance._childPlayerObject[i].transform
                .DOMove(tran.position, 0.3f);
            FinishCamera.transform.DOMove(new Vector3(4.53f, tran.position.y + 2, tran.position.z), 0.1f)
                .SetEase(Ease.Flash);
        }

        PlayerRanking.Instance.WinPlayerWaiting();
    }
}

class Humen
{
    public int Age { get; set; }
    public string Name { get; set; }
}
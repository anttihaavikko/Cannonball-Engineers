using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelSelector : MonoBehaviour
{
    public LevelTile tilePrefab;
    public TMPro.TextMeshProUGUI starCount, totalTime;

    // Start is called before the first frame update
    void Awake()
    {
        var sum = Manager.Instance.levelData.Aggregate(0, (total, l) => total + l.Value.stars);

        var num = 1;
        Manager.levels.ToList().ForEach(l => {
            var tile = Instantiate(tilePrefab, transform);
            tile.num1.text = tile.num2.text = num.ToString("D2");
            var lname = "???";
            tile.levelNumber = num - 1;

            if (sum >= num - 1)
            {
                tile.Unlock();
                lname = l;
            }

            tile.text.text = num.ToString("D2") + ". " + lname;

            if (Manager.Instance.levelData.ContainsKey(l))
            {
                var ld = Manager.Instance.levelData[l];
                tile.time.text = Manager.TimeToString(ld.time);

                for (int i = 0; i < ld.stars; i++)
                {
                    tile.stars[i].SetActive(true);
                }
            }
            num++;
        });
    }

    private void Start()
    {
        SceneChanger.Instance.AttachCamera();

        if (Manager.Instance.levelListPosition > 0)
        {
            transform.position = new Vector3(transform.position.x, Manager.Instance.levelListPosition);
        }

        var sum = Manager.Instance.levelData.Aggregate(0, (total, l) => total + l.Value.stars);
        var tot = Manager.Instance.levelData.Aggregate(0f, (total, l) => total + l.Value.time);

        starCount.text = $"{sum}/{Manager.levels.Length * 3}";
        totalTime.text = Manager.TimeToString(tot);
    }

    private void Update()
    {
        Manager.Instance.levelListPosition = transform.position.y;

        if (Input.GetKeyUp(KeyCode.Escape))
            SceneChanger.Instance.ChangeScene("Start");
    }
}

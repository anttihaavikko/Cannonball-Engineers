using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelSelector : MonoBehaviour
{
    public LevelTile tilePrefab;

    // Start is called before the first frame update
    void Awake()
    {
        var num = 1;
        Manager.Instance.levels.ForEach(l => {
            var tile = Instantiate(tilePrefab, transform);
            tile.num1.text = tile.num2.text = num.ToString("D2");
            tile.text.text = num.ToString("D2") + ". " + l;
            tile.levelNumber = num - 1;
            if(Manager.Instance.levelData.ContainsKey(l))
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
    }

    private void Update()
    {
        Manager.Instance.levelListPosition = transform.position.y;

        if (Input.GetKeyUp(KeyCode.Escape))
            SceneChanger.Instance.ChangeScene("Start");
    }
}

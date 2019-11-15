﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelSelector : MonoBehaviour
{
    public LevelTile tilePrefab;
    public Blinders blinders;

    // Start is called before the first frame update
    void Awake()
    {
        var num = 1;
        Manager.Instance.levels.ForEach(l => {
            var tile = Instantiate(tilePrefab, transform);
            tile.num1.text = tile.num2.text = num.ToString("D2");
            tile.text.text = num.ToString("D2") + ". " + l;
            tile.levelNumber = num - 1;
            tile.blinders = blinders;
            num++;
        });
    }
}
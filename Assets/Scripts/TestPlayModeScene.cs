using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class TestPlayModeScene : MonoBehaviour
{
    [SerializeField]
    private Text textDate = default;

    [SerializeField]
    private Text textTime = default;

    private DateTime now;

    public DateTime Now => now;

    [SerializeField]
    private List<Player> players = default;

    private void Awake()
    {
        players[0].Initialize(new PersonInfo("Ryohei", new DateTime(1997, 03, 10), PersonInfo.ESex.Male), new UnityInputProvider());
        if (players.Count > 1)
        {
            for (int i = 1; i < players.Count; i++)
            {
                Player ai = players[i];
                ai.Initialize(new PersonInfo("AI"), new AIInputProvider(players[i], players[0]));
                ai.GetComponent<Renderer>().material.color = new Color(Random.value, 0, Random.value);
            }
        }
    }

    private void Start()
    {
//        Debug.Log("HelloWorld!");
//        Debug.LogError("Nullエラーです。");
//        Debug.LogWarning("警告があります。");
    }

    private void Update()
    {
        now = DateTime.Now;
        textDate.text = $"{now.Year}/{now.Month}/{now.Day}";
        textTime.text = $"{now.Hour:D2}:{now.Minute:D2}:{now.Second:D2}";
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class PlatformManager : MonoBehaviour {
    public GameObject SinglePlatformElement;
    public GameObject StoreForPlatforms;
    public GameObject Player;
    public TextMeshProUGUI SliderValue;
    public Slider Slider;
    public Button Generate;

    float xBorder = 9;
    float yBorder = 5.38f;
    int Count;
    float width;
    float height;
    float playerHeight;
    System.Random random = new System.Random();
    List<Transform> StoreForPlatformsChilds = new List<Transform>();
    int maxHorizontalCount, maxVerticalCount;
    // Use this for initialization
    void Start () {
        Generate = GameObject.Find("Generate").GetComponent<Button>();
        Slider = GameObject.Find("Slider").GetComponent<Slider>();
        SliderValue = GameObject.Find("SliderValue").GetComponent<TextMeshProUGUI>();

        SliderValue.text = "" + Slider.value;
        Slider.onValueChanged.AddListener(delegate { SliderValue.text = "" + Slider.value; });
        Generate.onClick.AddListener(() => GenerateMap());

        Player = GameObject.FindGameObjectWithTag("Player");
        playerHeight = Player.GetComponent<SpriteRenderer>().size.y * Player.transform.localScale.y;

        width = SinglePlatformElement.GetComponent<SpriteRenderer>().size.x * SinglePlatformElement.transform.localScale.x;
        height = SinglePlatformElement.GetComponent<SpriteRenderer>().size.y * SinglePlatformElement.transform.localScale.y;
        xBorder -= width;
        yBorder -= height;
        maxHorizontalCount = (int)((xBorder * 2) / width);
        maxVerticalCount = (int)((yBorder * 2) / (height + playerHeight));
    }
    void MakeNewPlatform(Vector2 position)
    {
        var newElement = Instantiate(SinglePlatformElement, position, new Quaternion(), StoreForPlatforms.transform);
        StoreForPlatformsChilds.Add(newElement.transform);
    }
    void GenerateMap()
    {
        Count = (int)Slider.value;
        if (Count > 0)
        {
            StoreForPlatformsChilds.Clear();
            foreach (Transform go in StoreForPlatforms.GetComponentInChildren<Transform>())
            {
                Destroy(go.gameObject);
            }
            for (int i = 0; i < Count; i++)
            {
                float x = GetRandomNumber(-xBorder, xBorder);
                int countY = (int)GetRandomNumber(1, (maxVerticalCount + 1));
                float y = countY * (height + playerHeight) - yBorder - playerHeight;
                MakeNewPlatform(new Vector2(x, y));
            }
            SetPositionForCharacter();
        }
        
    }
    float GetRandomNumber(float min, float max)
    {
        if (min <= max)
        {
            var next = random.NextDouble();
            return (float)(min + (next * (max - min)));
        }
        return 0;
    }
    void SetPositionForCharacter()
    {
        int index = (int)GetRandomNumber(0, Count);
        Player.transform.SetPositionAndRotation(StoreForPlatformsChilds[index].position + new Vector3(0, height/2 + playerHeight/2, 0), new Quaternion());
    }
}

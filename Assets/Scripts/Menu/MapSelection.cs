using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

[System.Serializable]
public struct Map 
{
    public string Name;
    public Sprite MapPreview;

}
public class MapSelection : MonoBehaviour
{
    public string PlayerSelectScene;
    public string MainMenuScene;
    public int Rows;
    public int Columns;
    public Map[] Maps;
    public RectTransform MapContainer;
    public GameObject MapButton;
    public EventSystem es;

    private Vector2 ScrollMin, ScrollMax;
    public float ScrollLimit;
    public Scrollbar scrollbar;


    // Start is called before the first frame update
    void Start()
    {
        ScrollMin = MapContainer.position;
        ScrollMax = new Vector2(ScrollMin.x - ScrollLimit, ScrollMin.y);
        float PadLeft = MapButton.GetComponent<RectTransform>().rect.width;
        float PadBottom = MapButton.GetComponent<RectTransform>().rect.height;
        for(int y = 0; y < Rows; ++y) for(int x = 0; x < Columns; ++x)
        {
            if ((y * Columns) + x >= Maps.Length) break;
            GameObject button = Instantiate(MapButton, new Vector3(
                PadLeft + (x * ((Screen.width - (PadLeft * 2)) / Columns)), 
                PadBottom + (y * (Screen.height / Rows)),
                0), 
                Quaternion.identity);
            button.transform.SetParent(MapContainer);
            button.name = Maps[(y * Columns) + x].Name;
            button.GetComponent<Image>().sprite = Maps[(y * Columns) + x].MapPreview;
            button.GetComponent<Button>().onClick.AddListener(button.GetComponent<MapClick>().Click);
            button.GetComponent<MapClick>().PlayerSelectScene = PlayerSelectScene;
        }
        es.SetSelectedGameObject(MapContainer.GetChild(0).gameObject);
    }

    public void BackButton()
    {           //Loads another scene
        SceneManager.LoadScene(MainMenuScene);
    }
    public void OnScrollChanged()
    {
        MapContainer.position = Vector2.Lerp(ScrollMin, ScrollMax, scrollbar.value);
    }
}

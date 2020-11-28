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
public class MapSelection : BaseMenu
{
    public string PlayerSelectScene;
    public string MainMenuScene;
    public int Rows;
    public float SpaceMultiplier;
    public Map[] Maps;
    public RectTransform MapContainer;
    public GameObject MapButton;

    private Vector2 ScrollMin, ScrollMax;
    public Scrollbar scrollbar;


    // Start is called before the first frame update
    void Start()
    {
        RectTransform rt = MapButton.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(Screen.height / 3.5f, Screen.height / 3.5f);
        float HorizontalSpace = rt.rect.width * SpaceMultiplier;
        float VerticalSpace = rt.rect.height * SpaceMultiplier;
        Debug.Log(HorizontalSpace);
        Debug.Log(MapContainer.name);
        MapContainer.position = new Vector3(HorizontalSpace, Screen.height / 2, 0);
        ScrollMin = MapContainer.position;
        int Columns = (Maps.Length + 1) / Rows;
        for(int x = 0; x < Columns; ++x) for(int y = 0; y < Rows; ++y)
        {
            if ((x * Rows) + y > Maps.Length - 1) break;
            GameObject button = Instantiate(MapButton, new Vector3(MapContainer.transform.position.x +
                x * HorizontalSpace, 
                MapContainer.transform.position.y + ((y % 2 == 0 ? -VerticalSpace : VerticalSpace) / 2),
                0),
                Quaternion.identity);
            button.transform.SetParent(MapContainer);
            button.name = Maps[(x * Rows) + y].Name;
            button.GetComponent<Image>().sprite = Maps[(x * Rows) + y].MapPreview;
            button.GetComponent<Button>().onClick.AddListener(button.GetComponent<MapClick>().Click);
            button.GetComponent<Button>().onClick.AddListener(this.MenuClick);
            button.GetComponent<MapClick>().PlayerSelectScene = PlayerSelectScene;
        }
        es.SetSelectedGameObject(MapContainer.GetChild(0).gameObject);
        ScrollMax.x = MapContainer.childCount >= 7 ? (MapContainer.GetChild(6).position.x + MapContainer.GetChild(0).position.x) - MapContainer.GetChild(MapContainer.childCount - 1).position.x : ScrollMin.x;
        ScrollMax.y = ScrollMin.y;
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

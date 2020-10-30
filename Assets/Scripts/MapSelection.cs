using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct Map 
{
    public string Name;
    public Sprite MapPreview;

}
public class MapSelection : MonoBehaviour
{
    public int Rows;
    public int Columns;
    public Map[] Maps;
    public GameObject MapButton;


    // Start is called before the first frame update
    void Start()
    {
        float PadLeft = MapButton.GetComponent<RectTransform>().rect.width;
        float PadBottom = MapButton.GetComponent<RectTransform>().rect.height;
        for(int y = 0; y < Rows; ++y) for(int x = 0; x < Columns; ++x)
        {
            if ((y * Columns) + x >= Maps.Length) break;
            GameObject button = Instantiate(MapButton, new Vector3(
                PadLeft + (x * ((Screen.width - (PadLeft * 2)) / Columns)), 
                PadBottom + (y * ((Screen.height - (PadBottom * 2)) / Rows)),
                0), 
                Quaternion.identity);
            button.transform.SetParent(GameObject.Find("Canvas").transform);
            button.name = Maps[(y * Columns) + x].Name;
            button.GetComponent<Image>().sprite = Maps[(y * Columns) + x].MapPreview;
            button.GetComponent<Button>().onClick.AddListener(button.GetComponent<MapClick>().Click);
        }
    }

    public void BackButton()
    {           //Loads another scene
        SceneManager.LoadScene("Prototype 4 Menu");
    }
}

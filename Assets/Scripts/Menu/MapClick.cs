using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapClick : MonoBehaviour, ISelectHandler
{
    public string PlayerSelectScene;
    public RectTransform container;
    public void Click()
    {
        GameData.StartMap = gameObject.name;
        SceneManager.LoadScene(PlayerSelectScene, LoadSceneMode.Single);
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (transform.position.x + (transform as RectTransform).rect.width > Screen.width)
        {
            container.position = new Vector2(container.position.x + (Screen.width - (transform.position.x + (transform as RectTransform).rect.width)), container.position.y);
        }
        if (transform.position.x - (transform as RectTransform).rect.width < 0)
        {
            container.position = new Vector2(container.position.x - (transform.position.x - (transform as RectTransform).rect.width), container.position.y);
        }
    }
}

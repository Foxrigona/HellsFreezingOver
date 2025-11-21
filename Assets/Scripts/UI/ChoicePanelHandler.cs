using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChoicePanelHandler : MonoBehaviour
{
    private Image image;
    private Color panelColor;

    private void Awake()
    {
        this.image = GetComponent<Image>();
        this.panelColor = image.color;
        this.toggleChoicePanel(false);
    }
    //Sets the panel to either active or disactive
    public void toggleChoicePanel(bool isActive)
    {
        
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(isActive);
        }
        if (!isActive)
        {
            panelColor.a = 0f;
            image.color = panelColor;
            Time.timeScale = 1f;
        }
        else
        {
            panelColor.a = 0.4f;
            image.color = panelColor;
            Time.timeScale = 0f;
        }
    }

    public void setChoiceType(MonoBehaviour caller)
    {
        //Set card 1, set card 2
        foreach (Transform child in transform)
        {
            child.GetComponent<CardHandler>().setDecision(caller);
        }
    }
}

public enum ChoiceType
{
    Powerup,
    EnemyDefeat
}



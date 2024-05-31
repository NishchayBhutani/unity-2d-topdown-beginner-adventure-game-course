using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{

    public static UIHandler instance { get; private set; }
    VisualElement m_healthBar;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        m_healthBar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
        setHealthValue(1.0f);
    }

    public void setHealthValue(float percentage) {
        m_healthBar.style.width = Length.Percent(percentage * 100);
    }

}

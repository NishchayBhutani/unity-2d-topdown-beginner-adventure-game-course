using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{

    public static UIHandler instance { get; private set; }
    VisualElement m_healthBar;
    public float displayTime = 4.0f;
    private VisualElement m_NonPlayerDialogue;
    private float m_TimerDisplay;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        m_healthBar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
        setHealthValue(1.0f);
        m_NonPlayerDialogue = uiDocument.rootVisualElement.Q<VisualElement>("NPCDialogue");
        m_NonPlayerDialogue.style.display = DisplayStyle.None;
        m_TimerDisplay = -1.0f;
    }

    void Update() {
        if(m_TimerDisplay > 0) {
            m_TimerDisplay -= Time.deltaTime;
            if(m_TimerDisplay < 0) {
                m_NonPlayerDialogue.style.display = DisplayStyle.None;
            }
        }
    }

    public void DisplayDialogue() {
        m_NonPlayerDialogue.style.display = DisplayStyle.Flex;
        Debug.Log("DisplayStyle: " + m_NonPlayerDialogue.style.display);
        m_TimerDisplay = displayTime;
    }

    public void setHealthValue(float percentage) {
        m_healthBar.style.width = Length.Percent(percentage * 100);
    }

}

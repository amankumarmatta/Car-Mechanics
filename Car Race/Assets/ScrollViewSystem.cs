using UnityEngine;
using UnityEngine.UI;

public class ScrollViewSystem : MonoBehaviour
{
    public Button nextButton;
    public Button prevButton;
    private ScrollRect scrollRect;
    private float totalButtons;
    private float currentIndex = 0;

    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        totalButtons = scrollRect.content.childCount;

        nextButton.onClick.AddListener(() => { ScrollTo(++currentIndex); });
        prevButton.onClick.AddListener(() => { ScrollTo(--currentIndex); });
    }

    void ScrollTo(float index)
    {
        // Clamp the index value between 0 and totalButtons - 1
        currentIndex = Mathf.Clamp(index, 0, totalButtons - 1);

        // Calculate the new position of the scroll view
        float newPosition = currentIndex / (totalButtons - 1);

        // Scroll to the new position
        scrollRect.horizontalNormalizedPosition = newPosition;
    }
}

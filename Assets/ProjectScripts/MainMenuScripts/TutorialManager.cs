using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public TMP_Text tutorialText;
    public Button nextButton;
    public Button backButton;

    private int currentPage = 0;

    private string[] pages =
    {
        "DEAR CAMPER\n\n" +
        "Welcome to the campsite.\n\n" +
        "Before you begin, here's everything you need to know.",

        "HOW TO PLAY\n\n" +
        "ANALOG JOYSTICK\n\n" +
        "Swipe the Analog Stick to move.\n\n\n" +
        "SPRINT BUTTON\n\n" +
        "Hold the button while moving to gain speed.",

        "HOW TO PLAY\n\n" +
        "JUMP BUTTON\n\n" +
        "Tap the button to jump forward.\n\n\n" +
        "ATTACK BUTTON\n\n" +
        "Use it to chop trees and defend yourself.",

        "HOW TO PLAY\n\n" +
        "DOOR BUTTON\n\n" +
        "Use it to open doors in the Kubo/Cabin.\n\n\n" +
        "GRAB / INTERACTION BUTTON\n\n" +
        "Use it to interact with objects and collect resources.",

        "BEFORE YOU GO...\n\n" +
        "Explore, but be careful where you wander.\n\n" +
        "Good luck, camper..."
    };

    void Start()
    {
        ShowPage();
    }

    public void NextPage()
    {
        currentPage++;

        if (currentPage >= pages.Length)
        {
            FinishTutorial();
        }
        else
        {
            ShowPage();
        }
    }

    public void BackPage()
    {
        // Don't go back before Page 1
        if (currentPage > 0)
        {
            currentPage--;
            ShowPage();
        }
    }

    void ShowPage()
    {
        tutorialText.text = pages[currentPage];

        // Change NEXT button text on the final page
        if (currentPage == pages.Length - 1)
        {
            nextButton.GetComponentInChildren<TMP_Text>().text = "Start Game";
        }
        else
        {
            nextButton.GetComponentInChildren<TMP_Text>().text = "Next Page...";
        }

        // Hide BACK button on the first page
        if (currentPage == 0)
        {
            backButton.gameObject.SetActive(false);
        }
        else
        {
            backButton.gameObject.SetActive(true);
        }
    }

    void FinishTutorial()
    {
        gameObject.SetActive(false);
    }
}
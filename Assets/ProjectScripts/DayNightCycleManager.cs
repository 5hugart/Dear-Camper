using UnityEngine;
using TMPro;

public class DayNightCycleManager : MonoBehaviour
{
    public static DayNightCycleManager Instance;

    // ==========================================
    // TIME SETTINGS
    // ==========================================

    [Header("Time Settings")]
    [Tooltip("Length of one full 24-hour day in real seconds.")]
    public float fullDayLength = 480f; // 8 real minutes

    [Range(0f, 24f)]
    public float currentTime = 6f; // Start at 6:00 AM


    // ==========================================
    // DAY SETTINGS
    // ==========================================

    [Header("Day Settings")]
    public int currentDay = 1;

    [Tooltip("Sleeping becomes disabled starting on this day.")]
    public int horrorStartDay = 4;


    // ==========================================
    // SLEEP SETTINGS
    // ==========================================

    [Header("Sleep Settings")]

    [Range(0f, 24f)]
    public float sleepTime = 22f; // 10:00 PM

    [Range(0f, 24f)]
    public float wakeUpTime = 6f; // 6:00 AM


    // ==========================================
    // LIGHTING
    // ==========================================

    [Header("Lighting")]
    public Light sun;


    // ==========================================
    // UI
    // ==========================================

    [Header("UI - Optional")]
    public TMP_Text clockText;
    public TMP_Text dayText;


    // Used so Day 4+ can advance automatically
    private bool horrorDayPassedMidnight = false;


    // ==========================================
    // UNITY FUNCTIONS
    // ==========================================

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateLighting();
        UpdateUI();
    }

    private void Update()
    {
        UpdateTime();
        UpdateLighting();
        UpdateUI();
    }


    // ==========================================
    // TIME SYSTEM
    // ==========================================

    private void UpdateTime()
    {
        if (fullDayLength <= 0f)
            return;

        // 24 game hours spread across fullDayLength
        float gameHoursPerSecond =
            24f / fullDayLength;

        currentTime +=
            gameHoursPerSecond * Time.deltaTime;

        // Midnight reached
        if (currentTime >= 24f)
        {
            currentTime -= 24f;

            // DAYS 4+
            // Sleeping is disabled, so days
            // progress automatically.
            if (currentDay >= horrorStartDay)
            {
                currentDay++;

                Debug.Log(
                    "DAY " +
                    currentDay +
                    " HAS STARTED"
                );
            }
        }
    }


    // ==========================================
    // SUN / LIGHTING
    // ==========================================

    private void UpdateLighting()
    {
        if (sun == null)
            return;

        // 6 AM  = sunrise
        // 12 PM = daytime
        // 6 PM  = sunset
        // 12 AM = night

        float sunRotation =
            (currentTime / 24f) * 360f - 90f;

        sun.transform.rotation =
            Quaternion.Euler(
                sunRotation,
                170f,
                0f
            );
    }


    // ==========================================
    // UI
    // ==========================================

    private void UpdateUI()
    {
        if (clockText != null)
        {
            clockText.text =
                GetFormattedTime();
        }

        if (dayText != null)
        {
            dayText.text =
                "DAY " + currentDay;
        }
    }


    // ==========================================
    // FORMAT CLOCK
    // ==========================================

    public string GetFormattedTime()
    {
        int hour =
            Mathf.FloorToInt(currentTime);

        int minute =
            Mathf.FloorToInt(
                (currentTime - hour) * 60f
            );

        string period =
            hour >= 12 ? "PM" : "AM";

        int displayHour =
            hour % 12;

        if (displayHour == 0)
        {
            displayHour = 12;
        }

        return string.Format(
            "{0:00}:{1:00} {2}",
            displayHour,
            minute,
            period
        );
    }


    // ==========================================
    // CAN PLAYER SLEEP?
    // ==========================================

    public bool CanSleep()
    {
        // Day 4, 5, 6 etc.
        if (currentDay >= horrorStartDay)
        {
            return false;
        }

        // Days 1-3:
        // Player can sleep at 10 PM or later.
        return currentTime >= sleepTime;
    }


    // ==========================================
    // SLEEP
    // ==========================================

    public void Sleep()
    {
        // --------------------------------------
        // DAY 4+
        // --------------------------------------

        if (currentDay >= horrorStartDay)
        {
            Debug.Log(
                "You can't sleep anymore..."
            );

            return;
        }


        // --------------------------------------
        // TOO EARLY
        // --------------------------------------

        if (currentTime < sleepTime)
        {
            Debug.Log(
                "It's too early to sleep. " +
                "Come back after 10:00 PM."
            );

            return;
        }


        // --------------------------------------
        // SLEEP SUCCESSFULLY
        // --------------------------------------

        currentDay++;

        currentTime = wakeUpTime;

        Debug.Log(
            "Woke up on DAY " +
            currentDay +
            " at " +
            GetFormattedTime()
        );

        UpdateLighting();
        UpdateUI();
    }


    // ==========================================
    // HELPER FUNCTIONS
    // ==========================================

    public int GetCurrentDay()
    {
        return currentDay;
    }

    public float GetCurrentTime()
    {
        return currentTime;
    }

    public bool IsHorrorPhase()
    {
        return currentDay >= horrorStartDay;
    }
}
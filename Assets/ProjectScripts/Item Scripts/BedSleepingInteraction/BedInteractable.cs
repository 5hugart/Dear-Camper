using UnityEngine;

public class BedInteractable : MonoBehaviour
{
    public void TrySleep()
    {
        if (DayNightCycleManager.Instance == null)
        {
            Debug.LogWarning(
                "DayNightCycleManager not found!"
            );

            return;
        }

        int day =
            DayNightCycleManager.Instance.GetCurrentDay();

        float time =
            DayNightCycleManager.Instance.GetCurrentTime();

        // =====================================
        // DAY 4+
        // =====================================

        if (day >= 4)
        {
            Debug.Log(
                "You can't sleep anymore..."
            );

            return;
        }

        // =====================================
        // BEFORE 10 PM
        // =====================================

        if (time < 22f)
        {
            Debug.Log(
                "It's too early to sleep. " +
                "Come back after 10:00 PM."
            );

            return;
        }

        // =====================================
        // START SLEEP SEQUENCE
        // =====================================

        if (SleepFadeController.Instance != null)
        {
            SleepFadeController.Instance.StartSleep();
        }
        else
        {
            Debug.LogWarning(
                "SleepFadeController not found!"
            );
        }
    }
}
using UnityEngine;

public class PowerupTutorialTrigger : MonoBehaviour
{
    public RaceManagerTutorial raceManager; // assign in inspector
    public RaceManagerTutorial.PowerupType powerupType;
    private bool tutorialShown = false;

    private void OnTriggerEnter(Collider other)
    {
        if (tutorialShown) return;

        if (other.CompareTag("Player"))
        {
            tutorialShown = true;

            string message = "";
            string step = "Powerup";

            switch (powerupType)
            {
                case RaceManagerTutorial.PowerupType.WaterBottle:
                    message = "WATER BOTTLE!\n\n\n\n\n\nRestores your stamina.\nTap to continue.";
                    step = "WaterBottle";
                    break;

                case RaceManagerTutorial.PowerupType.EnergyDrink:
                    message = "ENERGY DRINK!\n\n\n\n\n\nGives you a temporary speed boost for 3 seconds.\nTap to continue.";
                    step = "EnergyDrink";
                    break;
            }

            raceManager.ShowTutorialPanel(message, step);
        }
    }
}

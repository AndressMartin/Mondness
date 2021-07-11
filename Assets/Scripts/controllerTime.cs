using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerTime : MonoBehaviour
{
    //This script was created to make the Cinematica/Story Game transition effect.
    public GameObject CinematicTime1 = null;
    public GameObject CinematicTime2 = null;

    public void Start()
    {
        CinematicTime1.SetActive(false);
        CinematicTime2.SetActive(false);

        //ShowCinematic();
        StartCoroutine(WaitBeaforeShow());
    }

    private void ShowCinematic()
    {
        CinematicTime1.SetActive(true);
        //Wait someone of seconds
        CinematicTime2.SetActive(true);
    }

    private IEnumerator WaitBeaforeShow()
    {
        CinematicTime1.SetActive(true);

        yield return new WaitForSeconds(5); //Selection time

        CinematicTime2.SetActive(true);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideMaster : MonoBehaviour
{
    [SerializeField]
    private List<Text> guidePages;

    [SerializeField]
    private Button backwardsButton;

    [SerializeField]
    private Button forwardsButton;

    private int page = 0;

    /// <summary>
    /// Player button press.
    /// to go up pages or down pages.
    /// We are lazy and just swap in entire text boxes instead of changing the text
    /// </summary>
    /// <param name="PageUp"></param>
    public void TurnPage(bool PageUp)
    {
        if (PageUp)
        {
            if (page == (guidePages.Count - 1))
            {
                forwardsButton.gameObject.SetActive(false);
                return;
            }
            else if (page == (guidePages.Count - 2))
                forwardsButton.gameObject.SetActive(false);

            this.GetComponent<AudioSource>().Play();

            backwardsButton.gameObject.SetActive(true);
            guidePages[page].gameObject.SetActive(false);
            page++;
            guidePages[page].gameObject.SetActive(true);
        }
        else
        {
            if (page == 0)
            {
                backwardsButton.gameObject.SetActive(false);
                return;
            }
            else if (page == 1)
                backwardsButton.gameObject.SetActive(false);

            this.GetComponent<AudioSource>().Play();

            forwardsButton.gameObject.SetActive(true);

            guidePages[page].gameObject.SetActive(false);
            page--;
            guidePages[page].gameObject.SetActive(true);
        }
    }
}
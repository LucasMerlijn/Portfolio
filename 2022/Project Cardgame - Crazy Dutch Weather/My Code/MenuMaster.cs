using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuMaster : MonoBehaviour
{
    [SerializeField]
    private List<Image> ImageList;
    [SerializeField]
    private Image credits;
    [SerializeField]
    private Image guide;

    private int currentImage = 0;
    private int lastImage = 0;

    /// <summary>
    /// Starts when the scene is loaded (and object)
    /// </summary>
    private void Start()
    {
        ImageChange();
        StartCoroutine(RandomizeBackground());
    }

    /// <summary>
    /// Change the background image.
    /// cannot be the same image due to the while loop
    /// </summary>
    private void ImageChange()
    {
        for (int i = 0; i < ImageList.Count; i++)
        {
            ImageList[i].gameObject.SetActive(false);
        }

        while(currentImage == lastImage)
        {
            currentImage = Random.Range(0, ImageList.Count);
        }

        ImageList[currentImage].gameObject.SetActive(true);
        lastImage = currentImage;
    }

    /// <summary>
    /// Timer for the image change
    /// </summary>
    /// <returns></returns>
    IEnumerator RandomizeBackground()
    {
        while(true)
        {
            ImageChange();
            yield return new WaitForSecondsRealtime(2f);
        }
    }

    /// <summary>
    /// Fun little interactive.
    /// Each time the player taps the screen it counts as a button press. So the background will change as well then
    /// </summary>
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ImageChange();
        }
    }

    /// <summary>
    /// Player has pressed the start game button.
    /// The game will play audio and then load the game scene by index
    /// </summary>
    public void StartGame()
    {
        this.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// play some audio
    /// Load the Credits panel
    /// </summary>
    /// <param name="activate"></param>
    public void Credits(bool activate)
    {
        this.GetComponent<AudioSource>().Play();
        credits.gameObject.SetActive(activate);
    }

    /// <summary>
    /// play some audio
    /// load the guide panel
    /// </summary>
    /// <param name="activate"></param>
    public void Guide(bool activate)
    {
        this.GetComponent<AudioSource>().Play();
        guide.gameObject.SetActive(activate);
    }
}
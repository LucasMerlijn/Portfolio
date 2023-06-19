using System.Reflection;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EmailGeneratorTesting : MonoBehaviour
{

    [SerializeField] private List<Email> emailList = new List<Email>();

    [SerializeField] private TMP_Text textEmailAdress; // email address
    [SerializeField] private TMP_Text textEmailSubject; // email subject
    [SerializeField] private TMP_Text textEmailURL; // URL in link
    [SerializeField] private TMP_Text textEmail; // Email body

    private int currentEmail;
    private string playerName = "Markus";

    private AudioManager temp_AM;
    public Canvas tempGameplayCanvas;

    private int emailsAnswered = 0; // this is for testing.
    private int generateReportIndex = 0;
    [SerializeField] private Canvas reportCard; // Report card Canvas
    private Dictionary<string, int> errorList = new Dictionary<string, int>(); // Report Card Stats
    [SerializeField] private List<TMP_Text> reportTextList = new List<TMP_Text>(); // Report Card Text
    [SerializeField] private List<TMP_Text> totalWrongs = new List<TMP_Text>(); // Report card text (total issues)

    [SerializeField] private ErrorCounter errorCount; // Statistics

    [SerializeField] private currentEmailType emailType;

    [SerializeField] private List<ParticleSystem> posParticleEffect = new List<ParticleSystem>();
    [SerializeField] private List<ParticleSystem> negParticleEffect = new List<ParticleSystem>();

    private bool LoadedEmails = false;

    private void Start()
    {
        temp_AM = FindObjectOfType<AudioManager>();
        //GenerateEmail();
    }

    public void LoadEmails()
    {
        UnityEngine.Object[] EmailSet = Resources.LoadAll("Emails", typeof(Email));

        foreach (var email in EmailSet)
        {
            emailList.Add((Email)email);
        }
        LoadedEmails = true;
    }

    public void GenerateEmail()
    {
        if (!LoadedEmails)
            LoadEmails();

        int rnd = Random.Range(0, 2);
        currentEmail = Random.Range(0, emailList.Count);

        if (rnd == 0)
        {
            textEmailAdress.text = emailList[currentEmail].legitEmailAdress;
            textEmailSubject.text = emailList[currentEmail].legitSubject;
            textEmail.text = string.Format(emailList[currentEmail].legitEmailBody, playerName, emailList[currentEmail].legitSenderName);
            emailType = currentEmailType.Legitimate;
        }
        else
        {
            textEmailAdress.text = emailList[currentEmail].falseEmailAdress;
            textEmailSubject.text = emailList[currentEmail].falseSubject;
            textEmail.text = string.Format(emailList[currentEmail].falseEmailBody, playerName, emailList[currentEmail].falseSenderName);
            emailType = currentEmailType.Phishing;
        }
    }

    public void SetHyperLink(bool Active)
    {
        if (Active)
        {
            if (emailType == currentEmailType.Legitimate)
            {
                textEmailURL.text = emailList[currentEmail].legitURL;
            }
            else
            {
                textEmailURL.text = emailList[currentEmail].falseURL;
            }
        }
        else
        {
            textEmailURL.text = "";
        }
    }

    public void Interaction(bool interact)
    {
        if (interact)
        {
            if (emailType == currentEmailType.Legitimate)
                Answered(true);
            else
                Answered(false);
        }
        else
        {
            if (emailType == currentEmailType.Phishing)
                Answered(true);
            else
                Answered(false);
        }
    }
    private void Answered(bool correct)
    {
        if (!correct)
        {
            CheckErrors(emailList[currentEmail].FalseErrors);

            for (int i = 0; i < negParticleEffect.Count; i++)
            {
                negParticleEffect[i].Play();
            }
        }
        else
        {
            for (int i = 0; i < posParticleEffect.Count; i++)
            {
                posParticleEffect[i].Play();
            }
        }

        temp_AM.PlayFeedbackSound(correct);

        //emailsAnswered++;
        //if (emailsAnswered == 5)
        //{
        //    GenerateReportCard();
        //}
        //else
        //{
        //currency.ActiveGameplayChanges(correct, streak);
        GenerateEmail();
        //}
    }

    public void GenerateReportCard()
    {
        errorList.Clear();
        errorList.Add("Sender Name", errorCount.senderNameFault);
        errorList.Add("Receiver Name", errorCount.receiverNameFault);
        errorList.Add("Sender Adress", errorCount.senderAdressFault);
        errorList.Add("Subject", errorCount.SubjectFault);
        errorList.Add("URL", errorCount.URLFault);
        //errorList.Add("Attachment", errorCount.attachmentFault);
        //errorList.Add("Email Body", errorCount.bodyFault);

        var tempErrorList = errorList.OrderByDescending(x => x.Value).ToList();

        generateReportIndex = 0;

        foreach (var kp in tempErrorList)
        {
            //Debug.Log($"{kp.Key} is de key en {kp.Value} is de value");

            if (kp.Value <= 0)
                break;
            else
                reportTextList[generateReportIndex].text = "Pay attention to: <b> " + kp.Key.ToString() + " </b> You failed to spot this <b>(" + kp.Value.ToString() + ")</b> times.";

            generateReportIndex++;
        }

        totalWrongs[0].text = "Phishing emails <b>interacted:</b> " + errorCount.InteractedPhisingEmail.ToString();
        totalWrongs[1].text = "Normal emails <b>deleted:</b> " + errorCount.deletedCorrectEmail.ToString();

        tempGameplayCanvas.gameObject.SetActive(false);
        reportCard.gameObject.SetActive(true);
    }

    public void CloseReportCard()
    {
        reportCard.gameObject.SetActive(false);
        emailsAnswered = 0;

        errorCount.senderNameFault = 0;
        errorCount.receiverNameFault = 0;
        errorCount.senderAdressFault = 0;
        errorCount.SubjectFault = 0;
        errorCount.URLFault = 0;
        errorCount.deletedCorrectEmail = 0;
        errorCount.InteractedPhisingEmail = 0;
        errorList.Clear();

        if (GameManager.Instance.gameState == GameState.ActiveGameplayState)
            GenerateEmail();
    }

    private void CheckErrors(BooleanValues errors)
    {
        if (emailType == currentEmailType.Phishing)
        {
            errorCount.InteractedPhisingEmail++;

            if (errors.emailSenderName)
                errorCount.senderNameFault++;

            if (errors.emailReceiverName)
                errorCount.receiverNameFault++;

            if (errors.emailSenderAdress)
                errorCount.senderAdressFault++;

            if (errors.emailSubject)
                errorCount.SubjectFault++;

            if (errors.emailURL)
                errorCount.URLFault++;

            //if (errors.emailAttachment)
            //    errorCount.attachmentFault++;

            //if (errors.emailBody)
            //    errorCount.bodyFault++;
        }
        else
            errorCount.deletedCorrectEmail++;
    }
}

public enum currentEmailType
{
    Legitimate,
    Phishing
}

[System.Serializable]
public struct ErrorCounter
{
    public int senderNameFault;
    public int receiverNameFault;
    public int senderAdressFault;
    public int SubjectFault;
    public int URLFault;
    public int attachmentFault;
    public int bodyFault;
    public int deletedCorrectEmail;
    public int InteractedPhisingEmail;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EmailCreator : MonoBehaviour
{
    private Currency currency; // To add currency when correctly answered

    [SerializeField] private TMP_Text textEmailAdress;
    [SerializeField] private TMP_Text textEmailSubject;
    [SerializeField] private TMP_Text textEmailFile;
    [SerializeField] private TMP_Text textEmail;

    [SerializeField]
    private EmailType Email;

    [SerializeField]
    private string playerName = "Dennis";


    #region All Required Lists

    // Email
    private List<List<string>> emailSender = new List<List<string>>()
    {
        new List<string>() {"Markus", "Markus.B@Outlook.de"},
        new List<string>() {"Eva", "Eva.M.A@Outlook.de"},
        new List<string>() {"Norman", "Norman.H@Outlook.de"},
        new List<string>() {"Laura", "Laura.A@Outlook.de"},
        new List<string>() {"Martin", "Martin.G@Outlook.de" }
    };

    private List<string> emailSubject = new List<string>()
    {
        "Checking in on how the business is going",
        "Concerns about suspicious activity in the company",
        "Save the Date for our Company Picnic!",
        "Limited time offer: Discount on all pizzas in the city!"
    };

    private List<string> emailParagraphSubject1 = new List<string>()
    {
        "Dear {0},<br><br>I hope this email finds you well. I wanted to check in and see how things are going with [company name]. As someone who has been following your business for a while, I am interested in hearing any updates or progress you have made.<br><br> Specifically, I am curious about how coding is going. Could you share any recent developments or challenges you've encountered in this area? I'm also interested in knowing how your overall strategy and vision for the company is shaping up.<br><br>Please let me know if you have some time to connect and chat about these things. I'm eager to hear more about your experiences and perspective.<br><br>Thank you for your time, and I hope to hear from you soon.<br><br>Best regards,<br>{1}",
        "Dear {0},<br><br>I am writing to bring to your attention some concerns I have regarding possible suspicious activity within the company. I have noticed some irregularities in some workers accounts, and I am concerned that there may be some unethical or illegal behaviour occurring.<br><br>I wanted to reach out and bring this to your attention in case you were not aware of these issues. I believe it is important for the company to maintain the highest standards of ethics and transparency in all its operations, and I fear that these irregularities could tarnish the company's reputation and potentially harm its business.<br><br>I would appreciate it if you could look into this matter further and take any necessary actions to address these concerns. If you require any further information or evidence, please let me know and I will do my best to provide it.<br><br>Thank you for your attention to this matter.<br><br>Best regards,<br>{1}",
        "Dear {0},<br><br>I am excited to announce that our company picnic will be happening soon! We are thrilled to be able to come together as a team and enjoy some fun in the sun. We have planned a variety of activities and games for everyone to participate in, so there will be something for everyone.<br><br>The picnic will be taking place next Tuesday at the public park. We will have a delicious barbecue lunch catered, along with a variety of drinks and snacks. Please feel free to bring your families along, as we would love to meet and spend time with them as well.<br><br>This will be a great opportunity to take a break from work and get to know each other in a more relaxed and informal setting. We hope that everyone can make it and share in the fun!<br><br>Please RSVP to this email by Thursday so that we can get an accurate headcount for catering purposes. We look forward to seeing you there!<br><br>Best regards,<br>{1}",
        "Dear {0},<br><br>I wanted to let you know about a great deal that is happening right now in the city. For a limited time, all pizzas from any restaurant in the city will be available at a discounted price of [discount percentage] off the regular price.<br><br>This is a great opportunity to enjoy your favorite pizzas or try out new ones at a discounted price. Whether you prefer classic margherita, meat lovers, or veggie toppings, there are plenty of options available to satisfy your cravings.<br><br>The discount will be available for the next [duration of the offer], so make sure to take advantage of this great offer while it lasts.<br><br>Don't miss out on this chance to indulge in some delicious pizza at an unbeatable price. Grab your friends and family and head out to your favorite pizza place today!<br><br>Best regards,<br>{1}"
    };
    private List<string> emailParagraphSubject2 = new List<string>();
    private List<string> emailParagraphSubject3 = new List<string>();

    private List<string> emailEnding = new List<string>(); // Note, Edit based on name.


    // Phising
    private List<List<string>> phisingEmailSender = new List<List<string>>()
    {
        new List<string>() {"Markus!", "Markus.B@Outl00k.de"},
        new List<string>() {"EvaA", "Eeva.M.A@0utlook.de"},
        new List<string>() {"norman", "N0rman.H@Outlook.uk"},
        new List<string>() {"Laura", "Laura.0@Outlook.de"},
        new List<string>() {"Martin", "Martn.G@Outlook.de" }
    };

    private List<string> phisingEmailSubject = new List<string>()
    {
        "Urgent action required - Account Security Breach",
        "Immediate action required - Prize Claim",
        "Urgent Account Update Required - ABNIam",
        "Urgent Payment Required - Overdue Invoice"
    };

    private List<string> phisingEmailParagraphSubject1 = new List<string>()
    {
        "Dear {0},<br><br>We regret to inform you that there has been a security breach in our system, and your account may have been compromised. In order to protect your account and prevent any unauthorized access, we require your immediate assistance to reset your password.<br><br>Please click on the link below to access the password reset page and follow the instructions carefully. Once your password has been reset, your account will be secured, and you will be able to resume your normal activities.<br><br>https://bigPasswordReset.com/record/asuhfihHUHefaIpasQ<br><br>Please note that failure to reset your password within 24 hours of receiving this email may result in permanent account suspension.<br><br>Thank you for your prompt attention to this matter.<br><br>Sincerely,<br>ionDer",
        "Dear {0},<br><br>Congratulations! You have been selected as the lucky winner of our annual prize draw. You have won a brand new [insert the name of the prize], which is worth [$xx,xxx]. However, we need your immediate assistance to claim your prize.<br><br>Due to high demand, we require a processing fee of [$xxx] in order to complete your claim. Please make the payment using the link below, and provide your full name and mailing address for us to deliver the prize.<br><br>https://prizesprizesprizes/epicPrize/UAIGFhufaHUIhohioHoihaER<br><br>Please note that failure to claim your prize within 24 hours of receiving this email may result in forfeiting your prize.<br><br>Thank you for your prompt attention to this matter.<br><br>Sincerely,<br>EpicPrizeCompany",
        "Dear {0},<br><br>We have recently updated our security measures to better protect your account. As part of this process, we require you to update your account information immediately.<br><br>Please click on the link below to access the secure update page and follow the instructions carefully. Failure to update your account within 24 hours may result in account suspension.<br><br>https://SaveMoney.com/download/asuhfihHUHefaIpasQ<br><br>Thank you for your cooperation.<br><br>Sincerely,<br>ABNIam",
        "Dear {0},<br><br>We regret to inform you that your payment for invoice number [insert invoice number] is overdue. Unless we receive payment in full within 24 hours, we will have to proceed with legal action to recover the outstanding amount.<br><br>Please make the payment using the link below to avoid any further inconvenience. Once we receive your payment, we will update your account and provide you with a receipt for your records.<br><br>https://SavePayment/record/asuhfihHUHefaIpasQ<br><br>Please note that we take non-payment of invoices seriously and will take all necessary legal action to recover the amount owed.<br><br>Thank you for your prompt attention to this matter.<br><br>Sincerely,<br>EpicTaxes"
    };
    private List<string> phisingEmailParagraphSubject2 = new List<string>();
    private List<string> phisingEmailParagraphSubject3 = new List<string>();

    private List<string> phisingEmailEnding = new List<string>(); // Note, Edit based on name.


    // Specific Details
    private List<string> companyEmployeeNames = new List<string>();

    #endregion

    public int streak;
    //   public int emailAmount = 0;
    private int rememberLastEmail;

    // ### END VARIABLES ### \\

    private void Awake()
    {
        GameManager.onGameStateChanged += GameManager_onGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameManager_onGameStateChanged;
    }

    private void GameManager_onGameStateChanged(GameState state)
    {
        if (state == GameState.ActiveGameplayState)
        {
            GenerateEmail();
        }
    }

    private void Start()
    {
        currency = FindObjectOfType<Currency>();
        if (GameManager.Instance.gameState == GameState.ActiveGameplayState)
            GenerateEmail();
    }

    public void CheckEmail(bool Interact)
    {
        if (Interact) // Interact with email
        {

            //  for scene change code
            // emailAmount++;
            // if (emailAmount >= 5)
            // {
            //     GameManager.Instance.SwitchState(GameState.CustomizeGameplayState);
            // }

            if (Email == EmailType.Normal)
            {
                streak++;
                Answered(true);
            }
            else
            {
                streak = 0;
                Answered(false);
            }
        }
        else // Delete the email
        {
            if (Email == EmailType.Phising)
            {
                streak++;
                Answered(true);
            }
            else
            {
                streak = 0;
                Answered(false);
            }
        }
    }

    private void GenerateEmail()
    {
        int rand = Random.Range(0, 2);

        if (rand == 0)
            Email = EmailType.Normal;
        else
            Email = EmailType.Phising;

        if (Email == EmailType.Normal)
        {
            int person = Random.Range(0, emailSender.Count);

            // No repeating names. (for player feedback and to lessen confusion.
            if (person == rememberLastEmail)
            {
                person++;
                if (person == emailSender.Count)
                    person = 0;
            }
            rememberLastEmail = person;

            textEmailAdress.text = emailSender[person][1];
            textEmailSubject.text = emailSubject[Random.Range(0, emailSubject.Count)];
            textEmail.text = string.Format(emailParagraphSubject1[Random.Range(0, emailParagraphSubject1.Count)], playerName, emailSender[person][0]);
        }
        else
        {
            int person = Random.Range(0, phisingEmailSender.Count);

            textEmailAdress.text = phisingEmailSender[person][1];
            textEmailSubject.text = phisingEmailSubject[Random.Range(0, phisingEmailSubject.Count)];
            textEmail.text = string.Format(phisingEmailParagraphSubject1[Random.Range(0, phisingEmailParagraphSubject1.Count)], playerName, phisingEmailSender[person][0]);
        }
    }

    private void Answered(bool correct)
    {
        GameManager.Instance.audioManager.PlayFeedbackSound(correct);
        currency.ActiveGameplayChanges(correct, streak);
        GenerateEmail();
    }


}

public enum EmailType
{
    Normal,
    Phising
}
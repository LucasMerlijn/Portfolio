using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Email/EmailSenders")]
public class Email : ScriptableObject
{
    [Header("Email Adress Lists")]
    [TextArea(2, 2)]
    [Tooltip("DO NOT EDIT")]
    public string comment = "This will contain all email addresses & Names"; // DO NOT EDIT
    [Space(50)]

    [TextArea(1, 1)] public string legitSenderName;
    [TextArea(1, 1)] public string falseSenderName;
    [Space(30)]

    [TextArea(1, 1)] public string legitReceiverName;
    [TextArea(1, 1)] public string falseReceiverName;
    [Space(30)]

    [TextArea(1,1)] public string legitEmailAdress;
    [TextArea(1,1)] public string falseEmailAdress;
    [Space(30)]

    [TextArea(1,1)] public string legitSubject;
    [TextArea(1,1)] public string falseSubject;
    [Space(30)]

    [TextArea(5,1000)] public string legitEmailBody;
    [TextArea(5,1000)] public string falseEmailBody;
    [Space(30)]

    [TextArea(1,1)] public string legitURL;
    [TextArea(1,1)] public string falseURL;
    [Space(30)]

    public Sprite attachment;
    [TextArea(1,1)] public string legitAttachment;
    [TextArea(1,1)] public string falseAttachment;
    [Space(20)]

    public BooleanValues FalseErrors;

}

[System.Serializable]
public struct BooleanValues
{
    public bool emailSenderName;
    public bool emailReceiverName;
    public bool emailSenderAdress;
    public bool emailSubject;
    public bool emailBody;
    public bool emailURL;
    public bool emailAttachment;
}
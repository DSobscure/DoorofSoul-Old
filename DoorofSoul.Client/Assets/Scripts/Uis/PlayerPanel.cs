using UnityEngine;
using UnityEngine.UI;
using DoorofSoul.Protocol.Language;
using DoorofSoul.Client.Global;

public class PlayerPanel : MonoBehaviour
{
    #region status panel
    private RectTransform statusPanel;
    #endregion
    #region skill panel
    private RectTransform skillPanel;
    #endregion
    #region message content panel
    private RectTransform messageControlPanel;
    private Text latestMessageText;
    private Dropdown messageSourceFilterDropdown;
    private Button foldMessageContentButton;
    #endregion
    #region general panel
    private RectTransform generalPanel;
    private RectTransform containerPanel;
    private RectTransform soulPanel;
    #region message input 
    private RectTransform messageInputPanel;
    private InputField messageInputField;
    private Dropdown messageTargetSelectDropdown;
    private Button holdMessageButton;
    private Button sendMessageButton;
    #endregion
    #endregion
    #region message content panel
    private RectTransform messageContentPanel;
    #endregion

    // Use this for initialization
    void Start ()
    {
        #region status panel
        statusPanel = transform.FindChild("StatusPanel").GetComponent<RectTransform>();
        #endregion
        #region skill panel
        skillPanel = transform.FindChild("SkillPanel").GetComponent<RectTransform>();
        #endregion
        #region message content panel
        messageControlPanel = transform.FindChild("MessageControlPanel").GetComponent<RectTransform>();
        latestMessageText = messageControlPanel.transform.FindChild("LatestMessageText").GetComponent<Text>();
        messageSourceFilterDropdown = messageControlPanel.transform.FindChild("MessageSourceFilterDropdown").GetComponent<Dropdown>();
        messageSourceFilterDropdown.transform.FindChild("Label").GetComponent<Text>().text = UILanguageSeletor.Instance[SystemManager.SystemLanguage]["Source"];
        foldMessageContentButton = messageControlPanel.transform.FindChild("FoldMessageContentButton").GetComponent<Button>();
        foldMessageContentButton.GetComponentInChildren<Text>().text = UILanguageSeletor.Instance[SystemManager.SystemLanguage]["Unfold"];
        #endregion
        #region general panel
        generalPanel = transform.FindChild("GeneralPanel").GetComponent<RectTransform>();
        containerPanel = generalPanel.transform.FindChild("ContainerPanel").GetComponent<RectTransform>();
        soulPanel = generalPanel.transform.FindChild("SoulPanel").GetComponent<RectTransform>();
        #region message input 
        messageInputPanel = generalPanel.transform.FindChild("MessageInputPanel").GetComponent<RectTransform>();
        messageInputField = messageInputPanel.transform.FindChild("MessageInputField").GetComponent<InputField>();
        messageTargetSelectDropdown = messageInputPanel.transform.FindChild("MessageTargetSelectDropdown").GetComponent<Dropdown>();
        messageTargetSelectDropdown.transform.FindChild("Label").GetComponent<Text>().text = UILanguageSeletor.Instance[SystemManager.SystemLanguage]["Target"];
        holdMessageButton = messageInputPanel.transform.FindChild("HoldMessageButton").GetComponent<Button>();
        holdMessageButton.GetComponentInChildren<Text>().text = UILanguageSeletor.Instance[SystemManager.SystemLanguage]["Hold"];
        sendMessageButton = messageInputPanel.transform.FindChild("SendMessageButton").GetComponent<Button>();
        sendMessageButton.GetComponentInChildren<Text>().text = UILanguageSeletor.Instance[SystemManager.SystemLanguage]["Send"];
        #endregion
        #endregion
        #region message content panel
        messageContentPanel = transform.FindChild("MessageContentPanel").GetComponent<RectTransform>();
        #endregion
    }
}

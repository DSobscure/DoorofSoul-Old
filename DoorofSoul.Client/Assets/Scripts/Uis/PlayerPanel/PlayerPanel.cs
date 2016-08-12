using DoorofSoul.Client.Global;
using DoorofSoul.Client.Interfaces;
using DoorofSoul.Protocol.Language;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using DoorofSoul.Library.General;
using DoorofSoul.Library.General.SceneElements;

public class PlayerPanel : MonoBehaviour, IEventProvider
{
    private Scene scene;

    #region status panel
    private RectTransform statusPanel;
    #endregion
    #region skill panel
    private RectTransform skillPanel;
    #endregion
    #region message control panel
    private RectTransform messageControlPanel;
    private Text latestMessageText;
    private Dropdown messageSourceFilterDropdown;
    private Button foldMessageContentButton;
    #endregion
    #region general panel
    private RectTransform generalPanel;
    #region container panel
    private ContainerPanel containerPanel;
    #endregion
    private SoulPanel soulPanel;
    #region message input 
    private RectTransform messageInputPanel;
    private InputField messageInputField;
    private Dropdown messageTargetSelectDropdown;
    private Button holdMessageButton;
    private Button sendMessageButton;
    #endregion
    #endregion
    #region message content panel
    private MessageContentPanel messageContentPanel;
    #endregion

    void Awake()
    {
        scene = Global.Horizon.MainScene;
        RegisterEvents();
    }
    void OnDestroy()
    {
        EraseEvents();
    }
    void Start ()
    {
        SetupStatusPanel();
        SetupSkillPanel();
        SetupMessageContentPanel();
        SetupMessageControlPanel();
        SetupGeneralPanel();
    }

    public void RegisterEvents()
    {
        scene.MessageLog.OnReceiveNewMessage += OnReceiveNewMessage;
        scene.MessageLog.OnMessageChange += OnMessageChange;
    }

    public void EraseEvents()
    {
        scene.MessageLog.OnReceiveNewMessage -= OnReceiveNewMessage;
        scene.MessageLog.OnMessageChange -= OnMessageChange;
    }

    private void OnReceiveNewMessage(MessageInformation messageInformation)
    {
        latestMessageText.text = string.Format("[{0}]{1}: {2}", messageInformation.messageSourceType.ToString(), messageInformation.sourceName, messageInformation.message);
    }
    private void OnMessageChange(List<MessageInformation> messages)
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach(MessageInformation message in messages)
        {
            stringBuilder.AppendLine(string.Format("[{0}]{1}: {2}", message.messageSourceType.ToString(), message.sourceName, message.message));
        }
        messageContentPanel.ShowMessage(stringBuilder.ToString());
    }

    private void SetupStatusPanel()
    {
        statusPanel = transform.FindChild("StatusPanel").GetComponent<RectTransform>();
    }
    private void SetupSkillPanel()
    {
        skillPanel = transform.FindChild("SkillPanel").GetComponent<RectTransform>();
    }
    private void SetupMessageControlPanel()
    {
        messageControlPanel = transform.FindChild("MessageControlPanel").GetComponent<RectTransform>();
        latestMessageText = messageControlPanel.transform.FindChild("LatestMessageText").GetComponent<Text>();
        messageSourceFilterDropdown = messageControlPanel.transform.FindChild("MessageSourceFilterDropdown").GetComponent<Dropdown>();
        messageSourceFilterDropdown.transform.FindChild("Label").GetComponent<Text>().text = UILanguageSeletor.Instance[SystemManager.SystemLanguage]["Source"];
        foldMessageContentButton = messageControlPanel.transform.FindChild("FoldMessageContentButton").GetComponent<Button>();
        FoldMessageContent();
    }
    private void SetupGeneralPanel()
    {
        generalPanel = transform.FindChild("GeneralPanel").GetComponent<RectTransform>();
        containerPanel = generalPanel.transform.FindChild("ContainerPanel").GetComponent<ContainerPanel>();
        containerPanel.Setup(Global.Seat.MainContainer);
        soulPanel = generalPanel.transform.FindChild("SoulPanel").GetComponent<SoulPanel>();
        soulPanel.Setup(Global.Seat.MainSoul);

        messageInputPanel = generalPanel.transform.FindChild("MessageInputPanel").GetComponent<RectTransform>();
        messageInputField = messageInputPanel.transform.FindChild("MessageInputField").GetComponent<InputField>();
        messageTargetSelectDropdown = messageInputPanel.transform.FindChild("MessageTargetSelectDropdown").GetComponent<Dropdown>();
        messageTargetSelectDropdown.transform.FindChild("Label").GetComponent<Text>().text = UILanguageSeletor.Instance[SystemManager.SystemLanguage]["Target"];
        holdMessageButton = messageInputPanel.transform.FindChild("HoldMessageButton").GetComponent<Button>();
        holdMessageButton.GetComponentInChildren<Text>().text = UILanguageSeletor.Instance[SystemManager.SystemLanguage]["Hold"];
        sendMessageButton = messageInputPanel.transform.FindChild("SendMessageButton").GetComponent<Button>();
        sendMessageButton.GetComponentInChildren<Text>().text = UILanguageSeletor.Instance[SystemManager.SystemLanguage]["Send"];
        sendMessageButton.onClick.AddListener(() => 
        {
            if(messageInputField.text.Length > 0)
            {
                Global.Seat.MainContainer.ContainerOperationManager.Say(messageInputField.text);
                messageInputField.text = "";
            }
        });
    }
    private void SetupMessageContentPanel()
    {
        messageContentPanel = transform.FindChild("MessageContentPanel").GetComponent<MessageContentPanel>();
    }

    private void FoldMessageContent()
    {
        foldMessageContentButton.GetComponentInChildren<Text>().text = UILanguageSeletor.Instance[SystemManager.SystemLanguage]["Unfold"];
        foldMessageContentButton.onClick.RemoveAllListeners();
        foldMessageContentButton.onClick.AddListener(() => UnfoldMessageContent());
        messageContentPanel.gameObject.SetActive(false);
    }
    private void UnfoldMessageContent()
    {
        foldMessageContentButton.GetComponentInChildren<Text>().text = UILanguageSeletor.Instance[SystemManager.SystemLanguage]["Fold"];
        foldMessageContentButton.onClick.RemoveAllListeners();
        foldMessageContentButton.onClick.AddListener(() => FoldMessageContent());
        messageContentPanel.gameObject.SetActive(true);
    }
}

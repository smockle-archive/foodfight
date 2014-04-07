using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueScript : MonoBehaviour {

    // DESIGNER VARIABLES
    public int nameplateWidth = 150;
    public int nameplateHeight = 20;

    public int dialogueWidth = 150;
    public int dialogueHeight = 150;

    public GUIStyle nameplateStyle;
    public GUIStyle dialogueStyle;

    public ConversationNode startNode;

    const int indentFromLeft = 10;

    private string nameplateContent;
    private string dialogueContent;
    private ConversationNode currentNode;
    private Dictionary<string, ConversationNode> answers;

    public static DialogueScript Instance;

    private bool isShowingDialogue = true;
    private bool isPlayerTalking = true;
    private int index = 0; 
    //private Sprite talkingSprite;

    void Awake() {
        if(Instance != null) {
            Debug.Log("ERROR: Multiple instances of DialogueScript created.");
        }
        Instance = this;
        nameplateContent = "Nameplate";
        dialogueContent = "Dialogue";
        answers = null;

        currentNode = startNode;
        Talk();
        //talkingSprite = null;
    }

    void OnGUI()
    {
        if (!isShowingDialogue) return;

        // NAMEPLATE LABEL
        GUI.Label(
            new Rect(
                (isPlayerTalking)?
                    transform.position.x + indentFromLeft
                    :
                    Screen.width - (nameplateWidth + indentFromLeft + 6),
                Screen.height - (nameplateHeight + dialogueHeight), // + dialogueHeight),
                nameplateWidth,
                nameplateHeight
            ), nameplateContent, nameplateStyle
        );

        // DIALOGUE BOX LABEL
        GUI.Label(
            new Rect(
                transform.position.x + indentFromLeft,
                Screen.height - (dialogueHeight),
                dialogueWidth,
                dialogueHeight
            ), dialogueContent, dialogueStyle
        );

        if (answers != null)
        {
            int x = 0;
            foreach(KeyValuePair<string, ConversationNode> kvp in answers)
            {
                if (GUI.Button(
                    new Rect(
                        Screen.width / 2 - nameplateWidth / 2,
                        (nameplateHeight + dialogueHeight) + (x++ * nameplateHeight),
                        nameplateWidth,
                        nameplateHeight
                    ), kvp.Key)
                )
                {
                    currentNode = kvp.Value;
                }
            }
        }

        if (currentNode != null && currentNode.GetType() == typeof(EndNode) && (currentNode as EndNode).isConversationEnding()) {
            if (GUI.Button(
                new Rect(
                    Screen.width / 2 - nameplateWidth / 2,
                        nameplateHeight + dialogueHeight,
                        nameplateWidth,
                        nameplateHeight
                    ), "Let's go home!")
                )
            {
                Application.LoadLevel(0);
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (!isShowingDialogue) Toggle();
            Talk();
        }
    }

    public void SetNameplate(string n)
    {
        nameplateContent = n;
    }
    public void SetDialogue(string d)
    {
        dialogueContent = d;
    }
    public void Toggle()
    {
        isShowingDialogue = !isShowingDialogue;
    }
    public void Talk()
    {
        Talk(currentNode.gameObject.name, currentNode.dialogue, currentNode.gameObject.name == "Wyllow");

        Color temp;

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Speaker"))
        {
            temp = g.renderer.material.color;
            temp.a = 0.5f;
            g.renderer.material.color = temp;
        }

        temp = currentNode.gameObject.renderer.material.color;
        temp.a = 1.0f;
        currentNode.gameObject.renderer.material.color = temp;

        if (currentNode.GetType() == typeof(QuestionScript))
        {
            QuestionScript q = currentNode as QuestionScript;
            answers = q.getAnswers();
        }
        else answers = null;

        if (currentNode.GetType() == typeof(EndNode))
        {
            (currentNode as EndNode).End();
        } 
        else currentNode = currentNode.next;
    }
    public void Ask(string n, string d, Dictionary<string, ConversationNode> a, bool isPlayer = false)
    {
        //Ask yourself: is this really the way to do this? I've got some red flags about where information should be stored, and how we should handle transitions to answering...
        nameplateContent = n;
        dialogueContent = d;
        isPlayerTalking = isPlayer;
        answers = a;
    }
    public void Talk(string n, string d, bool isPlayer = false)
    {
        nameplateContent = n;
        dialogueContent = d;
        isPlayerTalking = isPlayer;
    }
}

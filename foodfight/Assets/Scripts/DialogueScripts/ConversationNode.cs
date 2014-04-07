using UnityEngine;
using System.Collections;

/// <summary>
/// Atom of conversation.
/// </summary>
public class ConversationNode : MonoBehaviour
{
    // DESIGNER VARIABLES
    public string dialogue;
    public ConversationNode next;

    private string speaker;
    private bool isPlayer;

    void Awake()
    {
        speaker = gameObject.name;
        isPlayer = speaker.Equals("Maya");
    }
}

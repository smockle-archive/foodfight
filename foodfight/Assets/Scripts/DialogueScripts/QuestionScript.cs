using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class QuestionScript : ConversationNode {

    // DESIGNER VARIABLES
    public List<string> answers = new List<string>(); // the ConversationNode points to the dialogue tree this answer takes us to
    public List<ConversationNode> answerResults = new List<ConversationNode>(); //this needs to be the same size as answers

    public Dictionary<string, ConversationNode> getAnswers()
    {
        return answers.Select((k, i) => new { k, v = answerResults[i] })
              .ToDictionary(x => x.k, x => x.v);
    }
}

public class Answer
{
    string answerText;
    ConversationNode next;
}
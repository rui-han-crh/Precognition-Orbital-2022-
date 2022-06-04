using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class YarnInteractable : MonoBehaviour
{
    // internal properties exposed to editor
    // TODO: pass a string as the starting node 
    private string startNode = "";

    // internal properties not exposed to editor
    private DialogueRunner dialogueRunner;
    public bool interactable = false;
    // I need my outline / something else here
    private bool isCurrentConversation = false;
    private Button expression;
    private Dictionary<string, FigureCharacter> characterMap = new Dictionary<string, FigureCharacter>();   

    public void Start()
    {
        expression = GetComponent<Button>();
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>(); //potentially need to redo
        dialogueRunner.Stop(); //Bug: Whenever a DL is found, the given UI will start it from the scene immediately!
        //dialogueRunner.onDialogueComplete.AddListener(EndConversation); //called whenever the conversation reaches the end of the file?
        //For a test, uncomment this! The following function should be hooked from somewhere else//
        //this.SetInterctable("StartEvelynAndOlivia"); --> Small test to ensure this works?

    }
    
    public void SetInterctable(string startingNode)
    {
        interactable = true; //this means he's already awaiting for a call!
        startNode = startingNode;
    }

    /*
     * BUG: OBJECT REF NOT SET TO INSTANCE OF OBJ
     */

    public void StartImmediate(string nextNode)
    {
        startNode = nextNode;
        interactable = false; //failSafe in case DL.isDLRunning doesn't work
        if (!dialogueRunner.IsDialogueRunning)
        {
            StartConversation();
        }
    }

    //this is just a placholder for another action to be added?
    public void Interact()
    {
        //check 3 things: Correct startNode, interactable AND legit dialogue running
        if(interactable && !dialogueRunner.IsDialogueRunning && startNode != "")
        {
            StartConversation();
        }
    }

    private void StartConversation()
    {
        Debug.Log($"Started conversation with {name}.");
        isCurrentConversation = true;
        dialogueRunner.StartDialogue(startNode);
    }

    //private void EndConversation()
    //{
    //    if (isCurrentConversation)
    //    {
    //        isCurrentConversation = false;
    //        Debug.Log($"Started conversation with {name}.");
    //    }
    //}


    //I don't see the difference between endConvo and disableConvo :(
    [YarnCommand("disable")]
    public void DisableConversation()
    {
        isCurrentConversation=false;
        dialogueRunner.Stop();
        interactable = false;
        startNode = ""; //for failsafe purposes :(
    }

    [YarnCommand("load")]

    public void StartingLoad(string charName, string emotion)
    {
        expression.image.sprite = YarnManager.Instance.characterMap[charName].GetEmotion(emotion);
    }
}

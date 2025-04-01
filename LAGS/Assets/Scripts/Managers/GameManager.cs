using UnityEngine;

public class GameManager : MonoBehaviour
{

    [Header("Dialog and Transition")]

    [SerializeField, Space] private TransitionController transitionController;
    [SerializeField] private GameObject DialogSistemGObj;
    [SerializeField] private DialogueManager dialogueManager;

    

    private void Awake()
    {
        transitionController.InitTransition(false, () => dialogueManager.ShowOrHideDialogPanel(true));
        dialogueManager.InitDialog("Prologo");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

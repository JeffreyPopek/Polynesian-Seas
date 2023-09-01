using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float typingSpeed;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject continueArrow;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText;
    [SerializeField] GameObject FinalIsland;
    [SerializeField] private Animator portraitAnimator;


    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;


    [Header("Load Global JSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;


    [Header("Audio")]
    [SerializeField] private AudioClip dialogueTypingSoundClip;
    private AudioSource audioSource;
    [SerializeField] private bool stopAudioSource;

    //[Range(1, 5)]
    //[SerializeField] private int frequencyLevel = 2;

    //[Range(0, 5)]
    //[SerializeField] private float pitch = 1f;
    

    //[Range(0f, 1f)]
    //[SerializeField] private float audioVolume = 1;

    private bool hasPlayedAudio = false;


    //Ink
    private Story currentStory;

    //read only to outside scripts
    public bool dialogueIsPlaying { get; private set; }

    private static DialogueManager instance;


    //typing effect
    private Coroutine displayLineCoroutine;

    private bool canContinueToNextTime = false;

    private bool canSkip = false;

    private bool submitSkip = true;

    //tags
    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";


    //variables
    private DialogueVariables dialogueVariables;

    public bool canOpenNotebook { get; private set; }

    private bool canEnterDialogue = true;



    //private void PlayDialogueSound(int currentDisplayedCharacterCount)
    //{
    //    if(currentDisplayedCharacterCount % frequencyLevel == 0)
    //    {
    //        if (stopAudioSource)
    //        {
    //            audioSource.Stop();
    //        }
    //        audioSource.volume = audioVolume;
    //        audioSource.pitch = pitch;
    //        audioSource.PlayOneShot(dialogueTypingSoundClip);
    //    }
    //}

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than one Dialogue manager in the scene");
        }
        instance = this;

        dialogueVariables = new DialogueVariables(loadGlobalsJSON);

        //opening notebook during dialogue
        canOpenNotebook = true;

        //audio
        //audioSource = this.gameObject.AddComponent<AudioSource>();  
    }

    public static DialogueManager getInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        //get all choices text
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        //returns right away if dialogue isn't playing
        if (!dialogueIsPlaying) { return; }


        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
        {
            submitSkip = true;
        }

        //handle continuing to the next line in the dialogue when the space bar is pressed
        if (submitSkip && currentStory.currentChoices.Count == 0 && canContinueToNextTime)
        {
            ContinueStory();
        }

    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        if(canEnterDialogue)
        {
            canEnterDialogue = false;

            //opening notebook during dialogue
            canOpenNotebook = false;

            //converts unreadable json text into readable dialogue
            currentStory = new Story(inkJSON.text);
            dialogueIsPlaying = true;
            dialoguePanel.SetActive(true);

            dialogueVariables.StartListening(currentStory);

            //reset tags
            displayNameText.text = "???"; ;
            portraitAnimator.Play("default");

            ContinueStory();
        }
        else
        {
            Debug.Log("Cannot enter dialogue if already in dialogue state!");
        }
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);

        dialogueVariables.StopListening(currentStory);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";

        int visitedIslands = ((Ink.Runtime.IntValue)DialogueManager.getInstance().GetVariablesState("islands_visited")).value;

        if (visitedIslands >= 4)
        {
            FinalIsland.SetActive(true);
        }


         bool isGameDone = ((Ink.Runtime.BoolValue)DialogueManager.getInstance().GetVariablesState("game_complete")).value;
         if (isGameDone == true){
            SceneManager.LoadScene(2);
         }

        //opening notebook during dialogue
        canOpenNotebook = true;


        canEnterDialogue = true;
    }

    private void ContinueStory()
    {

        if (currentStory.canContinue)
        {
            if(displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }

            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));

            HandleTags(currentStory.currentTags);
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private IEnumerator CanSkip()
    {
        canSkip = false;
        yield return new WaitForSeconds(0.05f);
        canSkip = true;
    }

    private IEnumerator DisplayLine(string line)
    {
        hasPlayedAudio = false;
        if (!hasPlayedAudio)
        {
            playDialogueSound();
            hasPlayedAudio = true;
        }
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;


        submitSkip = false;
        canContinueToNextTime= false;

        StartCoroutine(CanSkip());

        //hide stuff
        continueArrow.SetActive(false);
        HideChoices();

        bool isAddingRichTextTag = false;

        foreach(char letter in line.ToCharArray())
        {
            //check for button click, skip dialogue if clicked
            if(canSkip && submitSkip)
            {
                submitSkip = false;
                dialogueText.maxVisibleCharacters = line.Length;
                //dialogueText.text = line;
                break;
            }

            if(letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag= true;

                if(letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            else
            {
                //audio
                //PlayDialogueSound(dialogueText.maxVisibleCharacters);

                dialogueText.maxVisibleCharacters++;

                yield return new WaitForSeconds(typingSpeed);
            } 
        }

        //shows stuff
        continueArrow.SetActive(true);
        DisplayChoices();

        canContinueToNextTime = true;
    }

    private void HandleTags(List<string> currentTags)
    {
        //parse tag
        foreach(string tag in currentTags) 
        {
            string[] splitTag = tag.Split(':');
            if(splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be parsed: " + tag);
            }

            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            //handle tag
            switch(tagKey)
            {
                case SPEAKER_TAG:
                    displayNameText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    portraitAnimator.Play(tagValue);
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not being handled: " + tag);
                    break;
            }
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // defensive check to make sure our UI can support the number of choices coming in
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: " + currentChoices.Count);
        }

        int index = 0;
        // enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        // go through the remaining choices the UI supports and make sure they're hidden
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
    }

    private void HideChoices()
    {
        foreach(GameObject choiceButtons in choices)
        {
            choiceButtons.gameObject.SetActive(false);
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        if(canContinueToNextTime)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
            ContinueStory();
        }
    }

    public Ink.Runtime.Object GetVariablesState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);

        if(variableValue == null) 
        {
            Debug.Log("Ink Variable was found to be null: " + variableName);
        }
        return variableValue;
    }

    private void playDialogueSound()
    {
        FindObjectOfType<AudioManager>().Play("DialogueSound");
    }
}

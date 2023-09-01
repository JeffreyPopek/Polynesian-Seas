using System.Collections;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Notebook UI")]
    [SerializeField] private GameObject notebookIcon;
    [SerializeField] private GameObject notebookObject;

    [Header("Notebook Arrows")]
    [SerializeField] private GameObject leftArrow;
    [SerializeField] private GameObject rightArrow;


    [Header("Notebook Pages")]
    [SerializeField] private GameObject notebookPage1;

    private int activePage = 1;
    private int currentMaxPages;

    //page 1: islands
    private bool visitedIsland1, visitedIsland2, visitedIsland3, visitedIsland4, visitedIsland5;

    private bool isCompassObtained;

    [Header("Islands Dark")]
    [SerializeField] private GameObject island1Dark;
    [SerializeField] private GameObject island2Dark;
    [SerializeField] private GameObject island3Dark;
    [SerializeField] private GameObject island4Dark;
    [SerializeField] private GameObject island5Dark;


    [Header("Islands Color")]
    [SerializeField] private GameObject island1Color;
    [SerializeField] private GameObject island2Color;
    [SerializeField] private GameObject island3Color;
    [SerializeField] private GameObject island4Color;
    [SerializeField] private GameObject island5Color;

    [Header("Island Text")]
    [SerializeField] private TextMeshProUGUI island1Text;
    [SerializeField] private TextMeshProUGUI island2Text;
    [SerializeField] private TextMeshProUGUI island3Text;
    [SerializeField] private TextMeshProUGUI island4Text;
    [SerializeField] private TextMeshProUGUI island5Text;


    [Header("Page 2")]
    [SerializeField] private GameObject notebookPage2;

    private bool isNotebookActive = false;
    private bool canOpenNotebook;

    [Header("Update")]
    [SerializeField] private GameObject updateText;
    [SerializeField] private GameObject infoText;


    [Header("Islands Visited")]
    private int islandsVisited;
    [SerializeField] private TextMeshProUGUI islandsVisitedText;


    const string DEFAULT_ISLAND_TEXT = "???";


    private void Awake()
    {
        notebookObject.SetActive(false);
        
        islandsVisitedText.text = islandsVisited.ToString();
    }

    private void Update()
    {
        getInkyVariables();

        //open and close notebook logic
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(!isNotebookActive && canOpenNotebook)
            {
                openNotebook();
            }
            else if(isNotebookActive)
            {
                closeNotebook();
            }
        }

        //update logic
        checkArrows();
        getActivePage(activePage);

        //closes notebook if opens dialogue
        if(!canOpenNotebook)
        {
            closeNotebook();
        }
    }



    private IEnumerator DelayRemoval()
    {
        yield return new WaitForSeconds(2f);

    }

    private void updateNotebookSoundEffect()
    {
        FindObjectOfType<AudioManager>().Play("UpdateNotebook");
    }

    private void openNotebookSoundEffect()
    {
        FindObjectOfType<AudioManager>().Play("OpenNotebook");
    }

    private void PageTurningSoundEffect()
    {
        FindObjectOfType<AudioManager>().Play("OpenNotebook");
    }

    private void closeNotebookSoundEffect()
    {
        FindObjectOfType<AudioManager>().Play("CloseNotebook");
    }

    public void turnPageLeft()
    {
        if (activePage - 1 < 1)
        {
            return;
        }

        activePage--;

        PageTurningSoundEffect();
    }
    
    public void turnPageRight()
    {
        if(activePage + 1 > currentMaxPages)
        {
            return;
        }

        activePage++;

        PageTurningSoundEffect();
    }


    private void openNotebook()
    {

        Debug.Log("opening notebook");

        //always start on the first page
        activePage = 1;

        //update variables
        updateNotebookVariables();

        notebookObject.SetActive(true);
        isNotebookActive = true;

        getActivePage(activePage);

        infoText.SetActive(false);

        openNotebookSoundEffect();
    }

    private void updateNotebookVariables()
    {
        islandsVisitedText.text = islandsVisited.ToString();

        islandFoundLogic();

        if(isCompassObtained)
        {
            notebookPage2.SetActive(true);
        }
        else
        {
            notebookPage2.SetActive(false);
        }
    }

    private void getActivePage(int page)
    {
        if(page == 1)
        {
            deactivateAllPages();

            notebookPage1.SetActive(true);
        }
        else if(page == 2) 
        {
            deactivateAllPages();

            notebookPage2.SetActive(true);
        }
        else if(page == 3)
        {
            deactivateAllPages();

            //notebookPage3.SetActive(true);
        }
    }
    private void deactivateAllPages()
    {
        notebookPage1.SetActive(false);
        notebookPage2.SetActive(false);
        //notebookPage3.SetActive(false);
    }

    private void closeNotebook()
    {
        deactivateAllPages();

        notebookObject.SetActive(false);
        isNotebookActive = false;

        closeNotebookSoundEffect();
    }

    private void checkArrows()
    {
        if(activePage == 1)
        {
            leftArrow.SetActive(false);
        }
        else
        {
            leftArrow.SetActive(true);
        }

        if(currentMaxPages == activePage)
        {
            rightArrow.SetActive(false);
        }
        else
        {
            rightArrow.SetActive(true);
        }
    }

    private void getInkyVariables()
    {
        //can't open notebook in dialogue, checks if dialogue is running
        canOpenNotebook = DialogueManager.getInstance().canOpenNotebook;

        //update inky vars by getting string value of ink variable
        isCompassObtained = ((Ink.Runtime.BoolValue)DialogueManager.getInstance().GetVariablesState("notebook_compass")).value;


        currentMaxPages = ((Ink.Runtime.IntValue)DialogueManager.getInstance().GetVariablesState("max_pages")).value;

        islandsVisited = ((Ink.Runtime.IntValue)DialogueManager.getInstance().GetVariablesState("islands_visited")).value;


        visitedIsland1 = ((Ink.Runtime.BoolValue)DialogueManager.getInstance().GetVariablesState("visited_island_1")).value;
        visitedIsland2 = ((Ink.Runtime.BoolValue)DialogueManager.getInstance().GetVariablesState("visited_island_2")).value;
        visitedIsland3 = ((Ink.Runtime.BoolValue)DialogueManager.getInstance().GetVariablesState("visited_island_3")).value;
        visitedIsland4 = ((Ink.Runtime.BoolValue)DialogueManager.getInstance().GetVariablesState("visited_island_4")).value;
        visitedIsland5 = ((Ink.Runtime.BoolValue)DialogueManager.getInstance().GetVariablesState("visited_island_5")).value;

        islandTextLogic();
    }

    private void islandTextLogic()
    {
        //island 1
        if (!visitedIsland1)
        {
            island1Text.text = DEFAULT_ISLAND_TEXT;
        }
        else if (visitedIsland1)
        {
            island1Text.text = "Hawaii";
        }

        //island 2
        if (!visitedIsland2)
        {
            island2Text.text = DEFAULT_ISLAND_TEXT;
        }
        else if (visitedIsland2)
        {
            island2Text.text = "Samoa";
        }

        //island 3
        if (!visitedIsland3)
        {
            island3Text.text = DEFAULT_ISLAND_TEXT;
        }
        else if (visitedIsland3)
        {
            island3Text.text = "Mo'orea";
        }

        //island 4
        if (!visitedIsland4)
        {
            island4Text.text = DEFAULT_ISLAND_TEXT;
        }
        else if (visitedIsland4)
        {
            island4Text.text = "Tahiti";
        }

        //island 5
        if (!visitedIsland5)
        {
            island5Text.text = DEFAULT_ISLAND_TEXT;
        }
        else if (visitedIsland5)
        {
            island5Text.text = "Whakaari";
        }
    }

    private void islandFoundLogic()
    {
        //island 1
        if(!visitedIsland1)
        {
            island1Dark.SetActive(true);
            island1Color.SetActive(false);
        }
        else if(visitedIsland1)
        {
            island1Dark.SetActive(false);
            island1Color.SetActive(true);
        }

        //island 2
        if (!visitedIsland2)
        {
            island2Dark.SetActive(true);
            island2Color.SetActive(false);
        }
        else if (visitedIsland2)
        {
            island2Dark.SetActive(false);
            island2Color.SetActive(true);
        }

        //island 3
        if (!visitedIsland3)
        {
            island3Dark.SetActive(true);
            island3Color.SetActive(false);
        }
        else if (visitedIsland3)
        {
            island3Dark.SetActive(false);
            island3Color.SetActive(true);
        }

        //island 4
        if (!visitedIsland4)
        {
            island4Dark.SetActive(true);
            island4Color.SetActive(false);
        }
        else if (visitedIsland4)
        {
            island4Dark.SetActive(false);
            island4Color.SetActive(true);
        }

        //island 5
        if (!visitedIsland5)
        {
            island5Dark.SetActive(true);
            island5Color.SetActive(false);
        }
        else if (visitedIsland5)
        {
            island5Dark.SetActive(false);
            island5Color.SetActive(true);
        }
    }
}

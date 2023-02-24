using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance {get; private set;}

    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;
    public GameObject curtain;
    public GameObject canvas;
    private bool raiseLower = false;
    public GameObject mainScreen;
    public GameObject menuButton;

    public void DialogShow(string text) {
        dialogBox.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(TypeText(text));
    }
    public void DialogHide(){
        dialogBox.SetActive(false);
    }

    IEnumerator TypeText(string text) {
        dialogText.text = "";
        foreach(char c in text.ToCharArray()) {
            dialogText.text += c;
            yield return new WaitForSeconds(0.02f);
        }
    }

    void Awake(){
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject); 
            DontDestroyOnLoad(canvas);
        } else {
        Destroy(gameObject);
    }
    }

    IEnumerator ColorLerpFunction(bool fadeout, float duration)
    {
        float time = 0;
        raiseLower = true;
        Image curtainImg = curtain.GetComponent<Image>();
        Color startValue;
        Color endValue;
        if (fadeout) {
            startValue = new Color(0, 0, 0, 0);
            endValue = new Color(0, 0, 0, 1);
        } else {
            startValue = new Color(0, 0, 0, 1);
            endValue = new Color(0, 0, 0, 0);
        }
        while (time < duration)
        {
            curtainImg.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        curtainImg.color = endValue;
        raiseLower = false;
    }

    IEnumerator LoadYourAsyncScene(string scene)
    {
        print("lowering curtain");
        StartCoroutine(ColorLerpFunction(true, 1));
        while (raiseLower)
        {
            yield return null;
        }
        print("curtain down");
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

    while(!asyncLoad.isDone)
    {
        yield return null;
    }
    DialogHide();
    if(scene != "Menu"){
        mainScreen.SetActive(false);
    }
    StartCoroutine(ColorLerpFunction(false, 1));
    
    }
    public void ChangeScene(string scene){
       print("scene");
        StartCoroutine(LoadYourAsyncScene(scene));
    }

    public void EndGame(){
        menuButton.SetActive(true);
        DialogShow("You have completed all of the levels!");
    }
   //public void BackToMenu(){
   //     ChangeScene(Menu)
    //    mainScreen.SetActive(true);
   // }
    
    public void StartGame() {
        StartCoroutine(LoadYourAsyncScene("Intro"));
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

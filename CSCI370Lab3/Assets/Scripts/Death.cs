using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Death : MonoBehaviour
{

    public string scene;

    public void OnTriggerEnter2D(Collider2D collider2D) {
        print("Entered..");
        if (collider2D.gameObject.CompareTag("Player")) {
            StartCoroutine(DiedText());
        }
    }
    IEnumerator DiedText(){
        yield return new WaitForSeconds(2f);
        GameManager.Instance.ChangeScene(scene);
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

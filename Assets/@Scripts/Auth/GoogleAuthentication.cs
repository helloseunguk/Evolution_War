using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Google;
using TMPro;
using UniRx;
public class GoogleAuthentication : MonoBehaviour
{
    public string imageURL;

    public TMP_Text userNameTxt, userEmailTxt;

    public Image profilePic;

    public GameObject loginPanel, profilePanel;
    public string webClientId = "700501284975-l0031peu61r5tfilnclroj0tb92lg9bq.apps.googleusercontent.com";

    private GoogleSignInConfiguration configuration;

    public Button logInBtn;
    public Button logOutBtn;

    // Defer the configuration creation until Awake so the web Client ID
    // Can be set via the property inspector in the Editor.
    void Awake()
    {
        configuration = new GoogleSignInConfiguration
        {
            WebClientId = webClientId,
            RequestIdToken = true
        };
        logInBtn.OnClickAsObservable().Subscribe(_ => 
        {
            OnSignIn();
        });
        logOutBtn.OnClickAsObservable().Subscribe(_ => 
        {
            OnSignOut();
        });
    }

    public void OnSignIn()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        GoogleSignIn.Configuration.RequestEmail = true;

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(
          OnAuthenticationFinished,TaskScheduler.Default);
    }
    internal void OnAuthenticationFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            using (IEnumerator<System.Exception> enumerator =
                    task.Exception.InnerExceptions.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    GoogleSignIn.SignInException error =
                            (GoogleSignIn.SignInException)enumerator.Current;
                    Debug.LogError("Got Error: " + error.Status + " " + error.Message);
                }
                else
                {
                    Debug.LogError("Got Unexpected Exception?!?" + task.Exception);
                }
            }
        }
        else if (task.IsCanceled)
        {
            Debug.LogError("Canceled");
        }
        else
        {
            Debug.LogError("Welcome: " + task.Result.DisplayName + "!");

            //userNameTxt.text = "" + task.Result.DisplayName;
            //userEmailTxt.text = "" + task.Result.Email;

            //imageURL = task.Result.ImageUrl.ToString();
            //loginPanel.SetActive(false);
            //profilePanel.SetActive(true);
            //StartCoroutine(LoadProfilePic());
        }
    }
    IEnumerator LoadProfilePic() 
    {
        WWW www = new WWW(imageURL);
        yield return www;

        profilePic.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height),new Vector2(0,0));
    }
    public void OnSignOut()
    {
        userNameTxt.text = "";
        userEmailTxt.text = "" ;

        imageURL = "";
        loginPanel.SetActive(true);
        profilePanel.SetActive(false);
        Debug.LogError("Calling SignOut");
        GoogleSignIn.DefaultInstance.SignOut();
    }
  
}

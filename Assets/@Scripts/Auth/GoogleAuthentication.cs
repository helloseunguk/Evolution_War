using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Google;
using TMPro;
using UniRx;
using UnityEngine.SceneManagement;
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

    public FireAuth fireAuth;

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
        if (PlayerPrefs.HasKey("UserToken"))
        {
            userNameTxt.text = PlayerPrefs.GetString("UserName");
            userEmailTxt.text = PlayerPrefs.GetString("UserEmail");
            imageURL = PlayerPrefs.GetString("UserImageURL");
            StartCoroutine(LoadProfilePic());
            //loginPanel.SetActive(false);
            //profilePanel.SetActive(true);
        }
        Managers.Data.WriteNewUser("한승욱", "2131232");
    }
    private void Start()
    {
        
    }
    public void OnSignIn()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        GoogleSignIn.Configuration.RequestEmail = true;
        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(
          OnAuthenticationFinished);
    }
    public void OnAuthenticationFinished(Task<GoogleSignInUser> task)
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
            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                Debug.Log("로그인4");
                Debug.Log("로그인 성공" + task.Result.IdToken);
                PlayerPrefs.SetString("UserToken", task.Result.IdToken);
                PlayerPrefs.SetString("UserName", task.Result.DisplayName);
                PlayerPrefs.SetString("UserEmail", task.Result.Email);
                PlayerPrefs.SetString("UserImageURL", task.Result.ImageUrl.ToString());
                PlayerPrefs.Save();
                UserInfo.accountID = task.Result.IdToken;
                //  loginPanel.SetActive(false);
                userNameTxt.text = "" + task.Result.DisplayName;
                userEmailTxt.text = "" + task.Result.Email;

                imageURL = task.Result.ImageUrl.ToString();
                StartCoroutine(LoadProfilePic());
                profilePanel.SetActive(true);

            });

  
       
        
            //
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
        Debug.Log("로그아웃");
        userNameTxt.text = "";
        userEmailTxt.text = "" ;

        imageURL = "";

        loginPanel.SetActive(true);
        profilePanel.SetActive(false);
        Debug.LogError("Calling SignOut");
        PlayerPrefs.DeleteKey("UserToken");
        PlayerPrefs.DeleteKey("UserName");
        PlayerPrefs.DeleteKey("UserEmail");
        PlayerPrefs.DeleteKey("UserImageURL");
        PlayerPrefs.Save();
        GoogleSignIn.DefaultInstance.SignOut();
    }
  
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using UnityEngine.UI;
using UniRx;
using GooglePlayGames;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.BasicApi;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks.Triggers;
using TMPro;
public class AuthScene : MonoBehaviour
{
    public Button googleBtn;
    public Button guestBtn;
    public Button startBtn;
    public Button logOutBtn;

    public GameObject loginPanel;
    public GameObject startPanel;

    public TMP_Text userID;
    public TMP_Text userGold;

    ReactiveProperty<Define.AuthType> authState = new ReactiveProperty<Define.AuthType>();
    public IReadOnlyReactiveProperty<Define.AuthType> AuthState => authState;



    private void Start()
    {

        googleBtn.OnClickAsObservable().Subscribe(_ =>
        {
            OnClickGoogleLogin();
        });
        guestBtn.OnClickAsObservable().Subscribe(_ => 
        {
            OnClickLoginAnonymouse();
        });
        startBtn.OnClickAsObservable().Subscribe(_ => 
        {
            LoadingScene.LoadScene("MainScene");
        });
        logOutBtn.OnClickAsObservable().Subscribe(_ => 
        {
            OnClickLogout();
        });
        loginPanel.ObserveEveryValueChanged(_ => _.activeSelf).Subscribe(_ =>
        {
            if (_)
                startPanel.SetActive(!_);
        });
        startPanel.ObserveEveryValueChanged(_ => _.activeSelf).Subscribe(_ => 
        {
            if (_)
                loginPanel.SetActive(!_);
        });
        AuthState.Subscribe(async _ => 
        {
            switch (_) 
            {
                case Define.AuthType.Authenticated:
                    // Managers.Data.DBConnectionCheck();
                    Managers.Data.LoadUserData();
        
                    //      await Managers.Data.SaveUserDataToLocal(UserInfo.userData);

                    startPanel.SetActive(true);
                    break;
                case Define.AuthType.UnAuthenticated:
                    loginPanel.SetActive(true);
                    break;
            }
        });
#if !(UNITY_EDITOR || DEVELOPMENT_BUILD)
userID.gameObject.SetActive(false);
#endif
        CheckLogin();

    }
    public void SetAuthState(Define.AuthType _authState)
    {
        authState.Value = _authState;
    }
    void OnClickLoginAnonymouse() 
    {
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        auth.SignInAnonymouslyAsync().ContinueWith(task => {
            if (task.IsCompleted && !task.IsCanceled && !task.IsFaulted)
            {
     
                FirebaseUser newUser = task.Result.User;
                UnityMainThreadDispatcher.Instance().Enqueue(() =>
                {
                    Debug.Log("�Խ�Ʈ �α��� ����");
                    userID.text = task.Result.User.UserId;
                    SetAuthState(Define.AuthType.Authenticated);

                });
     
            }
        });
    }
    void OnClickLogout() 
    {
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        auth.SignOut();
        Debug.Log("User has been logged out.");
        Managers.Data.DeleteUserDataFile();
        SetAuthState(Define.AuthType.UnAuthenticated);
    }
    public void OnClickGoogleLogin()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().RequestIdToken().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = false;
        PlayGamesPlatform.Activate();

        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                StartCoroutine(GoogleLogin());
                SetAuthState(Define.AuthType.Authenticated);
                Debug.Log("�α��� ����");
            }
            else
            {
                Debug.Log("�α��� ����");
            }
        });
    }
    public IEnumerator GoogleLogin() 
    {
        while(System.String.IsNullOrEmpty(((PlayGamesLocalUser)Social.localUser).GetIdToken()))
            yield return null;

        string idToken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();

        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

        Firebase.Auth.Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential(idToken, null);
        auth.SignInWithCredentialAsync(credential).ContinueWith(
            task =>
            {
                if (task.IsCompleted && !task.IsCanceled && !task.IsFaulted)
                {
                    Firebase.Auth.FirebaseUser newUser = task.Result;
                }
            });
    }
    void CheckLogin() 
    {
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        if (auth.CurrentUser != null)
        {
            // ����ڰ� �̹� �α��εǾ� ����
            UserInfo.userData.id = auth.CurrentUser.UserId;

            Debug.Log("User is already logged in as " + auth.CurrentUser.UserId);

            userID.text = auth.CurrentUser.UserId;
            SetAuthState(Define.AuthType.Authenticated);
        }
        else
        {
            SetAuthState(Define.AuthType.UnAuthenticated);
            Debug.Log("No user is currently logged in.");
        }
    }
    void GetDatabase() 
    {
       
    }
}

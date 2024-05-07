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
            SceneManager.LoadScene("BattleScene_01_01");
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
        AuthState.Subscribe(_ => 
        {
            switch (_) 
            {
                case Define.AuthType.Authenticated:
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
                    Debug.Log("게스트 로그인 성공");
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
                Debug.Log("로그인 성공");
            }
            else
            {
                Debug.Log("로그인 실패");
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
            // 사용자가 이미 로그인되어 있음
            UserInfo.userData.id = auth.CurrentUser.UserId;

            Managers.Data.OnSaveUserData(UserInfo.userData);
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
}

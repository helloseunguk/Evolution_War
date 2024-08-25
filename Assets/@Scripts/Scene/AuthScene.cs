using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Firebase;
//using Firebase.Auth;
using UnityEngine.UI;
using UniRx;
//using GooglePlayGames;
//using GooglePlayGames.BasicApi.SavedGame;
//using GooglePlayGames.BasicApi;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks.Triggers;
using TMPro;
using Firebase.Auth;
public class AuthScene : MonoBehaviour
{
    public Button googleBtn;
    public Button guestBtn;
    public Button startBtn;
    public Button logOutBtn;

    public GameObject loginPanel;
    public GameObject startPanel;

    public TMP_Text buildVersion;
    public TMP_Text accountState;



    ReactiveProperty<Define.AuthType> authState = new ReactiveProperty<Define.AuthType>();
    public IReadOnlyReactiveProperty<Define.AuthType> AuthState => authState;


    FirebaseAuth auth;  
    private void Start()
    {
        buildVersion.text = $"buildVersion : {Application.version}";
        auth = FirebaseAuth.DefaultInstance;
        googleBtn.OnClickAsObservable().Subscribe(_ =>
        {
            OnClickGoogleLogin();
        });
        guestBtn.OnClickAsObservable().Subscribe(_ => 
        {
            OnClickLoginGuest();
            Debug.Log("Guest Login");
        });
        startBtn.OnClickAsObservable().Subscribe(_ => 
        {
            //임시
            OnClickLoginGuest();
            LoadingScene.LoadScene("ResourceDownLoadScene");
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
                    Managers.Data.LoadUserData();
                    if(EVUserInfo.userData.name == "")
                    {
                        EVUserInfo.userData.name = "Guest User" + UnityEngine.Device.SystemInfo.deviceUniqueIdentifier;
                    }
                    startPanel.SetActive(true);
                    break;
                case Define.AuthType.UnAuthenticated:
                    loginPanel.SetActive(true);
                    break;
            }
        });

        CheckLogin();

    }
    public void SetAuthState(Define.AuthType _authState)
    {
        authState.Value = _authState;
    }
    void OnClickLoginGuest() 
    {
        auth.SignInAnonymouslyAsync().ContinueWith(task =>
        {
            if (task.IsCompleted && !task.IsCanceled && !task.IsFaulted)
            {

                FirebaseUser newUser = task.Result.User;
                UnityMainThreadDispatcher.Instance().Enqueue(() =>
                {
                    Debug.Log("게스트 로그인 성공");
                    EVUserInfo.userData.name = "Guest User" +  UnityEngine.Device.SystemInfo.deviceUniqueIdentifier;
                    EVUserInfo.userData.id = task.Result.User.UserId;
                    SetAuthState(Define.AuthType.Authenticated);

                });

            }
        });
    }
    void OnClickLogout() 
    {
        auth.SignOut();
        Debug.Log("User has been logged out.");
        Managers.Data.DeleteUserDataFile();
        SetAuthState(Define.AuthType.UnAuthenticated);
    }
    public void OnClickGoogleLogin()
    {
        //PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().RequestIdToken().Build();
        //PlayGamesPlatform.InitializeInstance(config);
        //PlayGamesPlatform.DebugLogEnabled = false;
        //PlayGamesPlatform.Activate();

        //Social.localUser.Authenticate((bool success) =>
        //{
        //    if (success)
        //    {
        //        StartCoroutine(GoogleLogin());
        //        SetAuthState(Define.AuthType.Authenticated);
        //        Debug.Log("로그인 성공");
        //    }
        //    else
        //    {
        //        Debug.Log("로그인 실패");
        //    }
        //});
    }
    //public IEnumerator GoogleLogin()
    //{
    //    while (System.String.IsNullOrEmpty(((PlayGamesLocalUser)Social.localUser).GetIdToken()))
    //        yield return null;

    //    string idToken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();

    //    Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

    //    Firebase.Auth.Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential(idToken, null);
    //    auth.SignInWithCredentialAsync(credential).ContinueWith(
    //        task =>
    //        {
    //            if (task.IsCompleted && !task.IsCanceled && !task.IsFaulted)
    //            {
    //                Firebase.Auth.FirebaseUser newUser = task.Result;
    //            }
    //        });
    //}
    void CheckLogin() 
    {
        if (auth.CurrentUser != null)
        {
            Debug.Log("이미 로그인 되어있음");
            //EVUserInfo.userData.name = auth.CurrentUser.DisplayName;
            //EVUserInfo.userData.id = auth.CurrentUser.UserId;
            SetAuthState(Define.AuthType.Authenticated);
        }
        else
        {
            SetAuthState(Define.AuthType.UnAuthenticated);
        }
    }

}

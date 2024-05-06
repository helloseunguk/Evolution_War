using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using UnityEngine.UI;
using UniRx;
public class AuthScene : MonoBehaviour
{
    public Button guestBtn;

    public GameObject loginPanel;
    private void Start()
    {
        guestBtn.OnClickAsObservable().Subscribe(_ => 
        {
            OnClickLoginAnonymouse();
        });
        CheckLogin();
    }
    void OnClickLoginAnonymouse() 
    {
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        auth.SignInAnonymouslyAsync().ContinueWith(task => {
            if (task.IsCompleted && !task.IsCanceled && !task.IsFaulted)
            {
                FirebaseUser newUser = task.Result.User;
                loginPanel.SetActive(false);
            }
        });
    }
    void CheckLogin() 
    {
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        if (auth.CurrentUser != null)
        {
            // 사용자가 이미 로그인되어 있음
            Debug.Log("User is already logged in as " + auth.CurrentUser.UserId);
            loginPanel.SetActive(false);  // 로그인 패널을 비활성화
                                          // 추가적인 사용자 초기화나 화면 전환 로직을 여기에 추가
        }
        else
        {
            // 사용자가 로그인되어 있지 않음
            loginPanel.SetActive(true);  // 로그인 패널을 활성화하여 사용자가 로그인할 수 있도록 함
            Debug.Log("No user is currently logged in.");
        }
    }
}

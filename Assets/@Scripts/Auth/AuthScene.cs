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
            // ����ڰ� �̹� �α��εǾ� ����
            Debug.Log("User is already logged in as " + auth.CurrentUser.UserId);
            loginPanel.SetActive(false);  // �α��� �г��� ��Ȱ��ȭ
                                          // �߰����� ����� �ʱ�ȭ�� ȭ�� ��ȯ ������ ���⿡ �߰�
        }
        else
        {
            // ����ڰ� �α��εǾ� ���� ����
            loginPanel.SetActive(true);  // �α��� �г��� Ȱ��ȭ�Ͽ� ����ڰ� �α����� �� �ֵ��� ��
            Debug.Log("No user is currently logged in.");
        }
    }
}

//using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FireAuth : MonoBehaviour
{
    //public TMP_InputField inputEmail;
    //public TMP_InputField inputPassword;

    //// �α��� �����ϴ� ��ü
    //FirebaseAuth auth;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    auth = FirebaseAuth.DefaultInstance;
    //    auth.StateChanged += OnChangedAuthState;
    //}

    //private void OnChangedAuthState(object sender, System.EventArgs e)
    //{
    //    //���࿡ ���� ������ ������
    //    if(auth.CurrentUser != null)
    //    {
    //        print("Email : " + auth.CurrentUser.Email);
    //        print("UserId : " + auth.CurrentUser.UserId);
    //        print("**** �α��� ���� ��  ****");
    //    }
    //    //�׷��� ������
    //    else
    //    {
    //        print("***** �α׾ƿ� ���� �� *****");
    //    }
    //}
    //// Update is called once per frame
    //void Update()
    //{
        
    //}
    //public void OnClickSingUp() 
    //{
    //    StartCoroutine(SignUp());
        

    //}
    //IEnumerator SignUp()
    //{
    //    //ȸ������ �õ�
    //    var task = auth.CreateUserWithEmailAndPasswordAsync(inputEmail.text, inputPassword.text);

    //    //����� �Ϸ�� ������ ��ٸ���
    //    yield return new WaitUntil(()=> task.IsCompleted);

    //    if (task.Exception == null)
    //    {
    //        print("**** ȸ������ ���� ****");
    //    }
    //    else
    //    {
    //        print("**** ȸ������ ���� **** : " + task.Exception.Message);
    //    }
    //}
    //public void OnClickSignIn() 
    //{
    //    StartCoroutine (SignIn());
    //}
    //IEnumerator SignIn() 
    //{
    //    //�α��� �õ�
    //    var task = auth.SignInWithEmailAndPasswordAsync(inputEmail.text, inputPassword.text);
    //    yield return new WaitUntil(() => task.IsCompleted);

    //    if (task.Exception == null)
    //    {
    //        print("**** �α��� ���� ****"); 
    //    }
    //    else
    //    {
    //        print("**** �α��� ���� ****\n" + task.Exception );
    //    }
    //}
    //public void OnClickSignOut() 
    //{
    //    auth.SignOut();
    //}
}

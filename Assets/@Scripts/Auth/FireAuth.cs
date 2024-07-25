//using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FireAuth : MonoBehaviour
{
    //public TMP_InputField inputEmail;
    //public TMP_InputField inputPassword;

    //// 로그인 관리하는 객체
    //FirebaseAuth auth;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    auth = FirebaseAuth.DefaultInstance;
    //    auth.StateChanged += OnChangedAuthState;
    //}

    //private void OnChangedAuthState(object sender, System.EventArgs e)
    //{
    //    //만약에 유저 정보가 있으면
    //    if(auth.CurrentUser != null)
    //    {
    //        print("Email : " + auth.CurrentUser.Email);
    //        print("UserId : " + auth.CurrentUser.UserId);
    //        print("**** 로그인 상태 중  ****");
    //    }
    //    //그렇지 않으면
    //    else
    //    {
    //        print("***** 로그아웃 상태 중 *****");
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
    //    //회원가입 시도
    //    var task = auth.CreateUserWithEmailAndPasswordAsync(inputEmail.text, inputPassword.text);

    //    //통신이 완료될 때까지 기다리기
    //    yield return new WaitUntil(()=> task.IsCompleted);

    //    if (task.Exception == null)
    //    {
    //        print("**** 회원가입 성공 ****");
    //    }
    //    else
    //    {
    //        print("**** 회원가입 실패 **** : " + task.Exception.Message);
    //    }
    //}
    //public void OnClickSignIn() 
    //{
    //    StartCoroutine (SignIn());
    //}
    //IEnumerator SignIn() 
    //{
    //    //로그인 시도
    //    var task = auth.SignInWithEmailAndPasswordAsync(inputEmail.text, inputPassword.text);
    //    yield return new WaitUntil(() => task.IsCompleted);

    //    if (task.Exception == null)
    //    {
    //        print("**** 로그인 성공 ****"); 
    //    }
    //    else
    //    {
    //        print("**** 로그인 실패 ****\n" + task.Exception );
    //    }
    //}
    //public void OnClickSignOut() 
    //{
    //    auth.SignOut();
    //}
}

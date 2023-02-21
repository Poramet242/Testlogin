using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Google;
using System;
using System.Threading.Tasks;

public class LoginGoogle : MonoBehaviour
{
    private GoogleSignInConfiguration configuration;
    private string tokenGoogle_str;
    [SerializeField] private Text showToken;
    [SerializeField] private Text showName;
    [SerializeField] private Text showEmail;
    public void onclickLoginGoole()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(
          OnAuthenticationFinished);
    }
    internal void OnAuthenticationFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            using (IEnumerator<System.Exception> enumerator = task.Exception.InnerExceptions.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    GoogleSignIn.SignInException error = (GoogleSignIn.SignInException)enumerator.Current;
                    Debug.Log("Got Error: " + error.Status + " " + error.Message);
                }
                else
                {
                    Debug.Log("Got Unexpected Exception?!?" + task.Exception);
                }
            }
        }
        else if (task.IsCanceled)
        {
            Debug.Log("Canceled");
        }
        else
        {
            tokenGoogle_str = "Token google: " + task.Result.IdToken;
            showToken.text = tokenGoogle_str;

            showName.text = "Display name: " + task.Result.GivenName;
            showEmail.text = "E-mail: " + task.Result.Email;

            Debug.Log("Name: " + task.Result.DisplayName + "!");
            Debug.Log("IDToken: " + task.Result.IdToken + "!");
            Debug.Log("Email: " + task.Result.Email + "!");

        }
    }
}

using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using TennisTest.Struct;
using TennisTest.Statistics;
using System;
using TennisTest.UI;

namespace TennisTest.Authorisation
{
    public class PlayfabAuthManager : MonoBehaviour
    {
        public UserProfileTemplate CurrentUserProfile;
        public GameStatisticsTemplate CurrentStats;
        public StatisticsManager StatisticsManager;
        public Action OnRegisterSuccess;
        public Action OnLoginSuccess;

        private void Awake()
        {
            if (CurrentUserProfile == null)
                CurrentUserProfile = new UserProfileTemplate();

            if (CurrentStats == null)
                CurrentStats = new GameStatisticsTemplate();
        }

        public void Register(string username, string email, string password, string country)
        {
            if (string.IsNullOrWhiteSpace(username) || username.Length < 3)
            {
                Debug.LogError("Username must be at least 3 characters.");
                return;
            }

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                Debug.LogError("Invalid email address.");
                return;
            }

            if (string.IsNullOrWhiteSpace(password) || password.Length < 5)
            {
                Debug.LogError("Password must be at least 6 characters.");
                return;
            }

            var request = new RegisterPlayFabUserRequest
            {
                Username = username,
                Email = email,
                Password = password,
                RequireBothUsernameAndEmail = true
            };

            PlayFabClientAPI.RegisterPlayFabUser(request, result =>
            {
                Debug.Log("Registered successfully!");

                CurrentUserProfile = new UserProfileTemplate
                {
                    Username = username,
                    Country = country
                };

                StatisticsManager.SaveProfileData(CurrentUserProfile);

                OnRegisterSuccess?.Invoke();

            }, error =>
            {
                Debug.LogError("Register error: " + error.ErrorMessage);
            });
        }

        public void Login(string username, string password)
        {
            var request = new LoginWithPlayFabRequest
            {
                Username = username,
                Password = password
            };

            PlayFabClientAPI.LoginWithPlayFab(request, result =>
            {
                Debug.Log("Login successful!");

                CurrentUserProfile = new UserProfileTemplate { Username = username };
                OnLoginSuccess?.Invoke();
            }, error =>
            {
                Debug.LogError("Login failed: " + error.ErrorMessage);
            });
        }

        public void Logout()
        {
            CurrentUserProfile = new UserProfileTemplate(); 
            CurrentStats = new GameStatisticsTemplate();   

            Debug.Log("Logged out successfully.");
        }
    }

}

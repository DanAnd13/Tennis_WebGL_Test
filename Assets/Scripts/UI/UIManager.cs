using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TennisTest.Authorisation;
using TennisTest.Statistics;
using UnityEngine.UI;
using TMPro;
using TennisTest.Struct;
using TennisTest.PDF;
using System;

namespace TennisTest.UI
{
    public class UIManager : MonoBehaviour
    {
        public PlayfabAuthManager PlayfabAuthManager;
        public StatisticsManager StatisticsManager;
        public Countries CountriesSettings;
        public GameObject MainWindow;
        public GameObject AuthWindow;
        public GameObject LoginWindow;
        public GameObject StatisticsWindow;
        public GameObject InfoWindow;
        public TMP_InputField UserNameRegisInput;
        public TMP_InputField EmailRegisInput;
        public TMP_InputField PasswordRegisInput;
        public TMP_Dropdown CountryDropdown;
        public TMP_InputField UserNameLoginInput;
        public TMP_InputField PasswordLoginInput;
        public TMP_InputField GamesInput;
        public TMP_InputField HitsInput;
        public TextMeshProUGUI GamesTMP;
        public TextMeshProUGUI ServesTMP;
        public TextMeshProUGUI HitsTMP;
        public TextMeshProUGUI PointsTMP;
        public TextMeshProUGUI LevelTMP;

        private void OnEnable()
        {
            PlayfabAuthManager.OnLoginSuccess += Info;
            PlayfabAuthManager.OnRegisterSuccess += ShowStatisticsWindow;
            StatisticsManager.OnStatisticsSaved += Info;
        }

        void Start()
        {
            ShowMainWindow();
            FillCountryDropdown();
        }

        private void OnDisable()
        {
            PlayfabAuthManager.OnLoginSuccess -= Info;
            PlayfabAuthManager.OnRegisterSuccess -= ShowStatisticsWindow;
            StatisticsManager.OnStatisticsSaved -= Info;
        }

        void FillCountryDropdown()
        {
            if (CountryDropdown.template == null)
            {
                Debug.LogError("TMP_Dropdown: Template не призначено.");
                return;
            }

            CountryDropdown.ClearOptions();
            List<string> countryNames = new List<string>();

            foreach (var country in CountriesSettings.CountriesName)
            {
                countryNames.Add(country.ToString());
            }

            CountryDropdown.AddOptions(countryNames);

            // Змусити оновитися
            CountryDropdown.RefreshShownValue();
        }

        public void Registration()
        {
            PlayfabAuthManager.Register(
                UserNameRegisInput.text,
                EmailRegisInput.text,
                PasswordRegisInput.text,
                CountryDropdown.options[CountryDropdown.value].text);
        }

        public void Login()
        {
            PlayfabAuthManager.Login(UserNameLoginInput.text, PasswordLoginInput.text);
        }

        public void Statistics()
        {
            int playedGames = Int32.Parse(GamesInput.text);
            int hits = Int32.Parse(HitsInput.text);

            (int servedBalls, int points) = StatisticsManager.StatisticCalculation(playedGames, hits);

            GameStatisticsTemplate gameStat = new GameStatisticsTemplate
            {
                GamesPlayed = playedGames,
                Serves = servedBalls,
                Hits = hits,
                Points = points
            };

            StatisticsManager.SaveStatisticsData(gameStat);
        }


        public void Info()
        {
            StatisticsManager.LoadUserData(
                PlayfabAuthManager.CurrentUserProfile,
                PlayfabAuthManager.CurrentStats,
                (profile, stats) =>
                {
                    // Оновлення UI:
                    GamesTMP.text = "Played games: " + stats.GamesPlayed.ToString();
                    ServesTMP.text = "Served balls: " + stats.Serves.ToString();
                    HitsTMP.text = "Ball hits: " + stats.Hits.ToString();
                    PointsTMP.text = "Points: " + stats.Points.ToString();
                    LevelTMP.text = "Pllayer level: " + profile.Level;

                    ShowInfoWindow(); // Переходимо у вікно з інформацією
                }
            );
        }

        public void GetPDFStatistics()
        {
            GameStatisticsTemplate statistics = PlayfabAuthManager.CurrentStats;
            UserProfileTemplate userInfo = PlayfabAuthManager.CurrentUserProfile;
            StatisticsManager.CreatePDF(statistics, userInfo);
        }


        public void ClearRegistration()
        {
            UserNameRegisInput.text = "";
            EmailRegisInput.text = "";
            PasswordRegisInput.text = "";
        }

        public void ClearLogin()
        {
            UserNameLoginInput.text = "";
            PasswordLoginInput.text = "";
        }

        public void CleraStatistic()
        {
            GamesInput.text = "";
            HitsInput.text = "";
        }

        public void ShowMainWindow()
        {
            EnableAllWindows();
            MainWindow.SetActive(true);
        }

        public void ShowRegistrtion()
        {
            EnableAllWindows();
            AuthWindow.SetActive(true);
        }

        public void ShowLoginWindow()
        {
            EnableAllWindows();
            LoginWindow.SetActive(true);
        }

        public void ShowStatisticsWindow()
        {
            EnableAllWindows();
            StatisticsWindow.SetActive(true);
            CleraStatistic();
        }

        public void ShowInfoWindow()
        {
            EnableAllWindows();
            InfoWindow.SetActive(true);
        }

        public void EnableAllWindows()
        {
            MainWindow.SetActive(false);
            AuthWindow.SetActive(false);
            LoginWindow.SetActive(false);
            StatisticsWindow.SetActive(false);
            InfoWindow.SetActive(false);
        }
    }
}

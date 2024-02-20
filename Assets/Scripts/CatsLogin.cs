using System.Collections.Generic;
using UnityEngine;
using Immutable.Passport;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.UI;

public class InitPassport : MonoBehaviour
{
    private Passport passport;
    public TMP_Text walletAddressText;

    public TMP_Text loadingText;
    public Button loginButton;

    async void Start()
    {
        string clientId = "SsBp72eBg8TitZmJwc7OZe5A6XiY4eJj";
        string environment = Immutable.Passport.Model.Environment.SANDBOX;
        passport = await Passport.Init(clientId, environment);
        bool hasCredsSaved = await passport.HasCredentialsSaved();

        if (hasCredsSaved)
        {
            await passport.Login(useCachedSession: true);
        }
    }

    public async void LoginInit()
    {
        // Log in right after initialization:
        await Login();
    }

    public async Task Login()
    {
        await passport.Login(useCachedSession: true);

        loginButton.gameObject.SetActive(false);

        loadingText.text = "Loading Accounts...";

        // Initialize the provider and wallet right after login:
        await InitializeWallet();

        loadingText.text = "";
    }

    public async Task InitializeWallet()
    {
        await passport.ConnectEvm();
        List<string> accounts = await passport.ZkEvmRequestAccounts();
        walletAddressText.text = accounts[0];
    }
}
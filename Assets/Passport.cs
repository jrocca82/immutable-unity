using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Immutable.Passport;
using System.Threading.Tasks;

public class InitPassport : MonoBehaviour
{
    private Passport passport;

    async void Start()
    {
        string clientId = "SsBp72eBg8TitZmJwc7OZe5A6XiY4eJj";
        string environment = Immutable.Passport.Model.Environment.SANDBOX;
        passport = await Passport.Init(clientId, environment);

        // Log in right after initialization:
        await Login();
    }

    public async Task Login()
    {
        await passport.Login();

        // Initialize the provider and wallet right after login:
        await InitializeWallet();
    }

    public async Task InitializeWallet()
    {
        await passport.ConnectEvm();
        await passport.ZkEvmRequestAccounts();
    }
}
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.ServiceModel;
using System.Text;
using Xunit;

public static class Https_ClientCredentialTypeTests
{
    private static string s_username;
    private static string s_password;

    static Https_ClientCredentialTypeTests()
    {
        s_username = "wcf-test";
        s_password = "wcfSaysHell0World!";
    }

    [Fact]
    [ActiveIssue(69)]
    [OuterLoop]
    public static void BasicAuthentication_RoundTrips_Echo()
    {
        StringBuilder errorBuilder = new StringBuilder();

        try
        {
            BasicHttpBinding basicHttpBinding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
            basicHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
            Action<ChannelFactory> credentials = (factory) =>
            {
                factory.Credentials.UserName.UserName = s_username;
                factory.Credentials.UserName.Password = s_password;
            };

            ScenarioTestHelpers.RunBasicEchoTest(basicHttpBinding, Endpoints.Https_BasicAuth_Address, "BasicHttpBinding - Basic auth ", errorBuilder, credentials);
        }
        catch (Exception ex)
        {
            errorBuilder.AppendLine(String.Format("Unexpected exception was caught: {0}", ex.ToString()));
        }

        Assert.True(errorBuilder.Length == 0, String.Format("Test Case: BasicAuthentication FAILED with the following errors: {0}", errorBuilder));
    }

    [Fact]
    [ActiveIssue(69)]
    [OuterLoop]
    public static void DigestAuthentication_RoundTrips_Echo()
    {
        StringBuilder errorBuilder = new StringBuilder();

        try
        {
            BasicHttpBinding basicHttpBinding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
            basicHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Digest;
            Action<ChannelFactory> credentials = (factory) =>
            {
                factory.Credentials.HttpDigest.ClientCredential.UserName = s_username;
                factory.Credentials.HttpDigest.ClientCredential.Password = s_password;
            };

            ScenarioTestHelpers.RunBasicEchoTest(basicHttpBinding, Endpoints.Https_DigestAuth_Address, "BasicHttpBinding - Digest auth ", errorBuilder, credentials);
        }
        catch (Exception ex)
        {
            errorBuilder.AppendLine(String.Format("Unexpected exception was caught: {0}", ex.ToString()));
        }

        Assert.True(errorBuilder.Length == 0, String.Format("Test Case: DigestAuthentication FAILED with the following errors: {0}", errorBuilder));
    }

    [Fact]
    [ActiveIssue(5)]
    [OuterLoop]
    public static void NtlmAuthentication_RoundTrips_Echo()
    {
        StringBuilder errorBuilder = new StringBuilder();

        try
        {
            BasicHttpBinding basicHttpBinding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
            basicHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Ntlm;

            ScenarioTestHelpers.RunBasicEchoTest(basicHttpBinding, Endpoints.Https_NtlmAuth_Address, "BasicHttpBinding with NTLM authentication", errorBuilder, null);
        }
        catch (Exception ex)
        {
            errorBuilder.AppendLine(String.Format("Unexpected exception was caught: {0}", ex.ToString()));
        }

        Assert.True(errorBuilder.Length == 0, String.Format("Test Case: NtlmAuthentication FAILED with the following errors: {0}", errorBuilder));
    }

    [Fact]
    [ActiveIssue(6)]
    [OuterLoop]
    public static void WindowsAuthentication_RoundTrips_Echo()
    {
        StringBuilder errorBuilder = new StringBuilder();

        try
        {
            BasicHttpBinding basicHttpBinding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
            basicHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;

            ScenarioTestHelpers.RunBasicEchoTest(basicHttpBinding, Endpoints.Https_WindowsAuth_Address, "BasicHttpBinding with Windows authentication", errorBuilder, null);
        }
        catch (Exception ex)
        {
            errorBuilder.AppendLine(String.Format("Unexpected exception was caught: {0}", ex.ToString()));
        }

        Assert.True(errorBuilder.Length == 0, String.Format("Test Case: WindowsAuthentication FAILED with the following errors: {0}", errorBuilder));
    }
}

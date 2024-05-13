using BoschForms;
using System.Net.Http;

HttpClient.DefaultProxy.Credentials = System.Net.CredentialCache.DefaultCredentials;

var mode = args.Length < 1 ? "" : args[0];
Client.Mode = mode;

App.SetPage(new Home());
App.Run();
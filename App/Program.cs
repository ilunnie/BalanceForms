using BoschForms;

var mode = args.Length < 1 ? "" : args[0];
Client.Mode = mode;

App.SetPage(new Home());
App.Run();
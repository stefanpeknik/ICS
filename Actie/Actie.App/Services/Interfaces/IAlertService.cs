﻿namespace Actie.App.Services;
    
public interface IAlertService
{
    Task DisplayAsync(string title, string message);
}

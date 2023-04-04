﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Actie.BL.Models;
public abstract record ModelBase : INotifyPropertyChanged, IModel
{
    public Guid Id { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

}

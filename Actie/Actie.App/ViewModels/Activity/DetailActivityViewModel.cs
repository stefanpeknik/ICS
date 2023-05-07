// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Actie.App.Services;

namespace Actie.App.ViewModels;
public partial class DetailActivityViewModel : ViewModelBase
{
    public DetailActivityViewModel(IMessengerService messengerService) : base(messengerService)
    {
    }
}

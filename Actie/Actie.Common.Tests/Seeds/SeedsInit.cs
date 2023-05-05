// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Actie.Common.Tests.Seeds;

public class SeedsInit
{
    private static bool _initialized = false;

    public static void LoadLists()
    {
        if (_initialized) return;
        _initialized = true;
        UserSeeds.LoadLists();
        ProjectSeeds.LoadLists();
        ActivitySeeds.LoadLists();
    }
}

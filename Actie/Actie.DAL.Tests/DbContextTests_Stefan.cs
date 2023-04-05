// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Actie.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;


namespace Actie.DAL.Tests;

public class DbContextTests_Stefan : DbContextTestsBase
{
    public DbContextTests_Stefan(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task AddNew_User_Persisted()
    {
        //Arrange
        UserEntity entity = new()
        {
            Id = Guid.Parse("C5DE45D7-64A0-4E8D-AC7F-BF5CFDFB0EFC"),
            Name = "Stefan",
            Surname = "Peknik",
            Age = 10,
            Activities = null,
            Gender = "male",
            Height = 10,
            Photo = null,
            Projects = null,
            Weight = 50
        };

        //Act
        ActieDbContextSUT.Users.Add(entity);
        await ActieDbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntries = await dbx.Users.SingleAsync(i => i.Id == entity.Id);
        Assert.Equal(expected: entity, actual: actualEntries);
    }
}

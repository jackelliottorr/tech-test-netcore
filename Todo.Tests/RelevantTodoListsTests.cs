using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Todo.Data;
using Todo.Data.Entities;
using Todo.Services;
using Todo.Tests.Builders;
using Xunit;

namespace Todo.Tests;

public class RelevantTodoListsTests
{
    [Fact]
    public void GivenTasksInListNotOwnedByUser_GetRelevantLists_ShouldReturnThatList()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        var user1 = new IdentityUser { Id = "user1", UserName = "user1" };
        var user2 = new IdentityUser { Id = "user2", UserName = "user2" };
        using (var context = new ApplicationDbContext(options))
        {
            var todoList1 = new TodoListBuilder()
                .WithTodoListId(1)
                .WithOwner(user1)
                .WithItems(new List<TodoItem>
                {
                    new TodoItemBuilder()
                        .WithTodoItemId(1)
                        .WithResponsiblePartyId(user2.Id)
                        .Build()
                })
                .Build();

            var todoList2 = new TodoListBuilder()
                .WithTodoListId(2)
                .WithOwner(user1)
                .WithItems(new List<TodoItem>
                {
                    new TodoItemBuilder()
                        .WithTodoItemId(2)
                        .WithResponsiblePartyId(user1.Id)
                        .Build()
                })
                .Build();

            context.TodoLists.AddRange(todoList1, todoList2);
            context.SaveChanges();
        }

        // Act
        List<TodoList> result;
        using (var context = new ApplicationDbContext(options))
        {
            result = ApplicationDbContextConvenience.RelevantTodoLists(context, user2.Id).ToList();
        }

        // Assert
        result.Count.Should().Be(1);
        result.First().Owner.Id.Should().Be(user1.Id);
        result.First().Items.First().ResponsiblePartyId.Should().Be(user2.Id);
    }
}

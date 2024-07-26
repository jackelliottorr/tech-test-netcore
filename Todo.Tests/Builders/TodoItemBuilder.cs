using Microsoft.AspNetCore.Identity;
using Todo.Data.Entities;

namespace Todo.Tests.Builders;

internal class TodoItemBuilder
{
    private int _todoItemId;
    private string _title;
    private string _responsiblePartyId;
    private IdentityUser _responsibleParty;
    private bool _isDone;
    private Importance _importance;
    private int _todoListId;
    private TodoList _todoList;

    public TodoItemBuilder()
    {
        // Set default values
        _todoItemId = 0;
        _title = "Default Title";
        _responsiblePartyId = "defaultUserId";
        _responsibleParty = null;
        _isDone = false;
        _importance = Importance.Medium;
        _todoListId = 0;
        _todoList = null;
    }

    public TodoItemBuilder WithTodoItemId(int todoItemId)
    {
        _todoItemId = todoItemId;
        return this;
    }

    public TodoItemBuilder WithTitle(string title)
    {
        _title = title;
        return this;
    }

    public TodoItemBuilder WithResponsiblePartyId(string responsiblePartyId)
    {
        _responsiblePartyId = responsiblePartyId;
        return this;
    }

    public TodoItemBuilder WithResponsibleParty(IdentityUser responsibleParty)
    {
        _responsibleParty = responsibleParty;
        return this;
    }

    public TodoItemBuilder WithIsDone(bool isDone)
    {
        _isDone = isDone;
        return this;
    }

    public TodoItemBuilder WithImportance(Importance importance)
    {
        _importance = importance;
        return this;
    }

    public TodoItemBuilder WithTodoListId(int todoListId)
    {
        _todoListId = todoListId;
        return this;
    }

    public TodoItemBuilder WithTodoList(TodoList todoList)
    {
        _todoList = todoList;
        return this;
    }

    public TodoItem Build()
    {
        return new TodoItem(_todoListId, _responsiblePartyId, _title, _importance)
        {
            TodoItemId = _todoItemId,
            ResponsibleParty = _responsibleParty,
            IsDone = _isDone,
            TodoList = _todoList
        };
    }
}
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Todo.Data.Entities;

namespace Todo.Tests.Builders;

internal class TodoListBuilder
{
    private int _todoListId;
    private string _title;
    private IdentityUser _owner;
    private List<TodoItem> _items;

    public TodoListBuilder()
    {
        // Set default values
        _todoListId = 0;
        _title = "Default Title";
        _owner = null;
        _items = new List<TodoItem>();
    }

    public TodoListBuilder WithTodoListId(int todoListId)
    {
        _todoListId = todoListId;
        return this;
    }

    public TodoListBuilder WithTitle(string title)
    {
        _title = title;
        return this;
    }

    public TodoListBuilder WithOwner(IdentityUser owner)
    {
        _owner = owner;
        return this;
    }

    public TodoListBuilder WithItems(List<TodoItem> items)
    {
        _items = items;
        return this;
    }

    public TodoList Build()
    {
        var todoList = new TodoList(_owner, _title)
        {
            TodoListId = _todoListId,
            Items = _items
        };

        // Set the TodoList reference in each TodoItem
        foreach (var item in _items)
        {
            item.TodoList = todoList;
            item.TodoListId = todoList.TodoListId;
        }

        return todoList;
    }
}
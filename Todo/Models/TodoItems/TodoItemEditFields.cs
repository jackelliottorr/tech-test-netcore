using System;
using System.ComponentModel.DataAnnotations;
using Todo.Data.Entities;

namespace Todo.Models.TodoItems
{
    public class TodoItemEditFields
    {
        public int TodoListId { get; set; }

        [Required]
        [Length(1, 100)]
        public string Title { get; set; }
        public string TodoListTitle { get; set; }
        public int TodoItemId { get; set; }

        [Required]
        public bool IsDone { get; set; }

        [Display(Name = "Rank")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Value must be greater than or equal to {1}")]
        public int? Rank { get; set; }
        
        [Required]
        [Length(3, 100)]
        [Display(Name = "Email address")]
        public string ResponsiblePartyId { get; set; }

        [Required]
        public Importance Importance { get; set; }

        public TodoItemEditFields() { }

        public TodoItemEditFields(int todoListId, string todoListTitle, int todoItemId, string title, bool isDone, string responsiblePartyId, Importance importance, int? rank)
        {
            TodoListId = todoListId;
            TodoListTitle = todoListTitle;
            TodoItemId = todoItemId;
            Title = title;
            IsDone = isDone;
            ResponsiblePartyId = responsiblePartyId;
            Importance = importance;
            Rank = rank;
        }
    }
}
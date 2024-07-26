using System.ComponentModel.DataAnnotations;
using Todo.Data.Entities;

namespace Todo.Models.TodoItems
{
    public class TodoItemCreateFields
    {
        public int TodoListId { get; set; }
        
        [Required]
        [Length(1, 100)]
        public string Title { get; set; }
        public string TodoListTitle { get; set; }
        
        [Required]
        [Length(3, 100)]
        [Display(Name = "Email address")]
        public string ResponsiblePartyId { get; set; }

        [Required]
        public Importance Importance { get; set; } = Importance.Medium;

        public TodoItemCreateFields() { }

        public TodoItemCreateFields(int todoListId, string todoListTitle, string responsiblePartyId)
        {
            TodoListId = todoListId;
            TodoListTitle = todoListTitle;
            ResponsiblePartyId = responsiblePartyId;
        }
    }
}
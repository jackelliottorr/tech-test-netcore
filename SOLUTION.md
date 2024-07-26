# Task 1
Ran migrations using "dotnet ef database update" then created an account, a list, and some todo items.

# Task 2
Added OrderBy using LINQ.

# Task 3
Debugged through the failing test to see where the value was being dropped and found that the TodoItemEditFields constructor needed fixed. Once the constructor was using the parameter correctly, the test passed.

# Task 4
Working in Razor Pages day to day, I use System.ComponentModel.DataAnnotations quite a lot, letting the model binder and ModelState validation do as much work as possible. I don't use MVC as often but I went with the same approach here.

# Task 5
I felt that for the best user experience and reducing backends calls, doing this with js would be best. I added a checkbox at the top of the list that allows a user to toggle which tasks are visible. I've added a separate script file as I avoid inline js that would break by adding Content Security Policy.

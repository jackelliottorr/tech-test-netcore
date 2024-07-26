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

# Task 6
Updated ApplicationDbContextConvenience.RelevantTodoLists with an additional query clause. Using Any, TodoLists with an item where the user is the responsible owner will also be returned.
Focusing on getting the actual tasks done but in production I would not have this as a static class. I'd likely have an interface and repository so that it can:
1. Be easily decorated with functionality such as caching.
2. Be mocked out for tests.

I added some testing in RelevantTodoListsTests.cs. I tend to use Given When Then & Arrange Act Assert for most tests. 
I'm using Microsoft.EntityFrameworkCore.InMemory for speed and handiness, aware that Microsoft advise against using it -  https://learn.microsoft.com/en-us/ef/core/testing/testing-without-the-database#inmemory-provider.
Typically I prefer using TestContainers.NET for any tests that hit the database. LINQ providers can be funny and it's nice to replicate production as much as possible if the effort and test times are't too much. With TestContainers.NET, I usually create a Fixture that can be used across numerous atomic tests. If for some reason tests must depend on shared state, I've used libraries like Respawn in the past to revert changes from other tests.
I added some additional test builder classes to make the arrange code more declarative and draw attention to the important parts of the test (the list owner vs task owner difference). If I was being more thorough, I'd have refactored the existing test and builder but I didn't want to spend too much time on that.

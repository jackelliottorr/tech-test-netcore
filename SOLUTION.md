# Task 1
Ran migrations using "dotnet ef database update" then created an account, a list, and some todo items.

# Task 2
Added OrderBy using LINQ.

# Task 3
Debugged through the failing test to see where the value was being dropped and found that the TodoItemEditFields constructor needed fixed. Once the constructor was using the parameter correctly, the test passed.
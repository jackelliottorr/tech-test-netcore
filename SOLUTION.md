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

# Task 7
For simplicity, and as there wasn't much detail in the requirements, I've made it a nullable integer field and have allowed duplicate values. 

Other options I considered:
1. Performing validation on the post to check if the rank was already taken, but this would be a bad user experience.
2. If the rank was set to the same rank of existing TodoItems, reduce the rank of the existing item (recursively to prevent duplicates).
3. Some sort of drag and drop/reordering component, but I think that's past the scope and time of this challenge given that I have other tasks to complete.

dotnet ef migrations add AddRankToTodoItemModel
dotnet ef database update

While adding rank to the frontend, I noticed that TodoItemCreateFields and TodoItemEditFields didn't have any validation attributes. Using System.ComponentModel.DataAnnotations I added a few basic validation attributes such as Required and Length. I'd usually use attributes to create a whitelist of allowed characters for inputs to prevent stored and reflected XSS. 

I removed "Add new item" from the list and made it a button for 2 reasons:
1. This feels like a less confusing user experience, decoupling the listview from adding a new item.
2. It reduces the complexity of the JavaScript sorting code as I don't have to account for that list item.

Given that an earlier requirement was to implement sorting by importance, I made sure that ranked sorting didn't affect this. Items are sorted primarily by rank, then by importance within a rank.

# Task 8
Implemented a client for https://docs.gravatar.com/api/profiles/rest-api/. BaseUrl and other configurable options are injected using the IOptions pattern. 
I opted for unauthenticated requests to avoid having to create an account and application. In the real world, I'd probably store the API key in Azure Key Vault with application access via Managed Identity.

IProfileService is coupled to GravatarProfile but if I was spending more time I'd make it more generic. This would be to support profiles from third parties other than Gravatar. However, this would involve storing where a user has a profile and likely a factory to return the correct client/service.

Given their aggressive rate limiting on unauthenticated requests, I've added some basic Polly policies. I've added retries on transient http failures with exponential backoff. However, I've also added a conservative circuit breaker policy to reduce the chances of getting rate limited.
These could be parameters that we tailor using configuration & IOptionsSnapshot, or an external service. The values could be a lot different for production so I didn't spend as much time on them. Also, knowing which rate limiting algorithm they use would be helpful. Or if they used rate limiting headers to inform clients of requests before being rate limited, etc.

Caching would also be useful here but it would depend on our requirements and scale. I.e. single server deployment with an InMemory cache vs multiple web applications using a distributed cache. I believe that's past the scope of this technical challenge.

I added a method to Gravatar.cs to convert email addresses to SHA256 hashes. I've updated _LoginPartial and Detail to display the image from the avatar_url (didn't spend time on styling). Would consider a partial view/reusable component if spending more time and displaying this in more places in the application.

# Task 9
I typically work in Razor Pages and use as much built-in model binding and validation as possible. Additionally using Razor for forms based flows feels more intuitive to me. Using javascript is helpful for some things, but I prefer to avoid using it for this when ASP.NET does so much for us already. I made a start but since I have limited time left, I'm going to move on to task 10.
I've left some code showing what my intentions were. I added a partial view _CreateTodoItem and am rendering it on Detail.cshtml dependant on a button click. I started hooking into the submit event of the form. This is where I've stopped, before writing the ajax, creating an endpoint to process the POST, and rendering the results on the DOM.

# Task 10
I wasn't 100% sure how to approach this as really I tend to do more backend work and less javascript. I found a few drag and drop components online that would work well for this, using jqueryui for example. I've spent 4-5 hours now though so I'm putting in a quick solution.
I've added an input to the Detail page for each item, with javascript wiring up an AJAX fetch to an endpoint anytime the values are updated. If I was spending any more time on this, after the AJAX call, I'd change the data-rank attribute value and call the sort function again. Then, I wouldn't have to force a page load to see the new rank. I've discussed other things I'd do in my Task 7 comments as well.

document.addEventListener('DOMContentLoaded', function () {
    // Initialize
    toggleCompletedTodoItems();

    const toggleCheckbox = document.getElementById('toggleCompleted');
    if (toggleCheckbox) {
        toggleCheckbox.addEventListener('change', function () {
            toggleCompletedTodoItems();
        });
    }
});

function toggleCompletedTodoItems() {
    const checkbox = document.getElementById('toggleCompleted');
    const tasks = document.querySelectorAll('.list-group-item');

    tasks.forEach(function (task) {
        const isCompleted = task.getAttribute('data-completed') === 'true';
        task.style.display = checkbox.checked && isCompleted ? 'none' : 'block';
    });
}
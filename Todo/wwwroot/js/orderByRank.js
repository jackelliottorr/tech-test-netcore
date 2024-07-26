document.addEventListener('DOMContentLoaded', function () {
    const sortButton = document.getElementById('sortByRank');
    if (sortButton) {
        sortButton.addEventListener('click', function () {
            sortItems();
        });
    }
});

function sortItems() {
    const list = document.querySelector('.list-group');
    const items = Array.from(list.querySelectorAll('.list-group-item'));

    items.sort((a, b) => {
        const rankA = parseInt(a.getAttribute('data-rank'), 10);
        const rankB = parseInt(b.getAttribute('data-rank'), 10);
        const importanceA = getImportanceValue(a);
        const importanceB = getImportanceValue(b);

        // Use MAX_SAFE_INTEGER for invalid or zero ranks
        const validRankA = isNaN(rankA) || rankA <= 0 ? Number.MAX_SAFE_INTEGER : rankA;
        const validRankB = isNaN(rankB) || rankB <= 0 ? Number.MAX_SAFE_INTEGER : rankB;

        if (validRankA !== validRankB) {
            return validRankA - validRankB;
        } else {
            return importanceA - importanceB;
        }
    });

    // Append sorted items to the list
    items.forEach(item => list.appendChild(item));
}

function getImportanceValue(item) {
    return item.classList.contains('list-group-item-danger') ? 1 :
        item.classList.contains('list-group-item-info') ? 3 : 2; // Medium importance default
}
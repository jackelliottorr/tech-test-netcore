document.addEventListener('DOMContentLoaded', function () {
    const rankInputs = document.querySelectorAll('.rank-input');
    rankInputs.forEach(input => {
        input.addEventListener('change', function () {
            const itemId = this.dataset.id;
            const newRank = this.value;

            fetch('/TodoItem/UpdateRank', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ id: itemId, rank: newRank })
            })
                .then(response => response.json())
                .then(data => {
                    if (data.success) {
                        // Reload the page to reflect the new order
                        window.location.reload();
                    } else {
                        console.error('Error updating rank:', data.message);
                    }
                })
                .catch(error => {
                    console.error('Error:', error);
                });
        });
    });
});

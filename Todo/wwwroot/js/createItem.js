document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('btnShowCreateForm').addEventListener('click', function () {
        document.getElementById('divCreateForm').classList.remove('hidden');
        document.getElementById('divCreateForm').classList.add('visible');
        this.classList.add('hidden');
    });

    document.getElementById('btnCancel').addEventListener('click', function () {
        document.getElementById('divCreateForm').classList.remove('visible');
        document.getElementById('divCreateForm').classList.add('hidden');
        document.getElementById('btnShowCreateForm').classList.remove('hidden');
    });

    document.getElementById('frmCreateItem').addEventListener('submit', function (event) {
        event.preventDefault(); // Prevent form from reloading the page

        const form = event.target;
        const formData = new FormData(form);
    });
});

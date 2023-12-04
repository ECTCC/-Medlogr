
const toggleButton = document.getElementById('toggleButton');
const weightDiv = document.getElementById('weightDiv');

toggleButton.addEventListener('click', function () {
    event.preventDefault();
    weightDiv.classList.toggle('hidden');
});

togglebutton.addEventListener('click', (event) => {
  event.preventDefault();
  weightDiv.classList.toggle('hidden');
});

const togglebutton = document.getElementById('togglebutton');
const weightDiv = document.getElementById('weightDiv');

togglebutton.addEventListener('click', (event) => {
  event.preventDefault();
  weightDiv.classList.toggle('hidden');
});
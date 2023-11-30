function formatDate(date) {
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
}

const dateInput = document.getElementById('selectedDate');

const startDate = new Date('2000-01-01');

dateInput.value = formatDate(startDate);

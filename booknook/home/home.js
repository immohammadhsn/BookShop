const searchInput = document.querySelector('.search-input');
const suggestionsList = document.querySelector('.suggestions-list');

const suggestions = [
  'novels',
  'self-help',
  'mystry',
  'comic books',
  'romance',
  'philosophy'
];

searchInput.addEventListener('input', () => {
  const searchTerm = searchInput.value.toLowerCase();
  const filteredSuggestions = suggestions.filter(suggestion => suggestion.toLowerCase().startsWith(searchTerm));

  suggestionsList.innerHTML = ''; // Clear previous suggestions

  if (filteredSuggestions.length > 0) {
    filteredSuggestions.forEach(suggestion => {
      const suggestionItem = document.createElement('li');
      suggestionItem.textContent = suggestion;

      suggestionItem.addEventListener('click', () => {
        searchInput.value = suggestion;
        suggestionsList.style.display = 'none'; // Hide suggestions
        window.location.href = "../book in category/cat.html";
        const h2Element = document.querySelector('h2'); // Assuming you have an h2 element
        if (h2Element) {
          h2Element.textContent = suggestion;
        }
      });

      suggestionsList.appendChild(suggestionItem);
    });

    suggestionsList.style.display = 'block'; // Show suggestions
  } else {
    suggestionsList.style.display = 'none'; // Hide suggestions
  }
});
const searchIcon = document.querySelector('.search-icon');

searchIcon.addEventListener('click', () => {
  const searchTerm = searchInput.value; // Get the selected suggestion
  // Perform your search action using the searchTerm
});

const stars = document.querySelectorAll('.star');
const ratingValue = document.getElementById('rating-value');

stars.forEach(star => {
  star.addEventListener('click', () => {
    const rating = star.dataset.rating;
    stars.forEach(star => star.classList.remove('rated'));
    star.classList.add('rated');
    ratingValue.textContent = `Rating: ${rating}`;
  });
});


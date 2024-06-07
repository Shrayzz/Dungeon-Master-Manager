document.addEventListener('DOMContentLoaded', (event) => {
    function showSection(sectionId) {
        const sections = document.querySelectorAll('.main');
        sections.forEach(section => {
            if (section.id === sectionId) {
                section.classList.remove('hidden');
                section.classList.add('visible');
            } else {
                section.classList.remove('visible');
                section.classList.add('hidden');
            }
        });
    }

    function showMap() {
        const square = document.getElementById('square');
        square.classList.remove('closeMap');
        square.classList.add('openMap');
        const squareSections = document.querySelectorAll('.squareSec');
        squareSections.forEach(section => {
            section.classList.toggle('hidden');
            section.classList.toggle('visible');
        });
        const sizeSquare = document.getElementById('squareBtn');
        sizeSquare.classList.add('littleSquare');
        sizeSquare.classList.remove('bigSquare');
    }

    function hideMap() {
        const square = document.getElementById('square');
        square.classList.remove('openMap');
        square.classList.add('closeMap');
        const squareSections = document.querySelectorAll('.squareSec');
        squareSections.forEach(section => {
            section.classList.toggle('hidden');
            section.classList.toggle('visible');
        });
        const sizeSquare = document.getElementById('squareBtn');
        sizeSquare.classList.remove('littleSquare');
        sizeSquare.classList.add('bigSquare');
    }

    document.querySelector('.missionBtn').addEventListener('click', () => {
        showSection('missions-main');
    });
    document.querySelector('.characterBtn').addEventListener('click', () => {
        showSection('character-main');
    });
    document.querySelector('.itemBtn').addEventListener('click', () => {
        showSection('item-main');
    });
    document.getElementById('squareBtn').addEventListener('click', showMap);
    document.getElementById('backMapButton').addEventListener('click', hideMap);
});

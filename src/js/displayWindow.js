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

    document.querySelector('.missionBtn').addEventListener('click', () => {
        showSection('missions-main');
    });
    document.querySelector('.characterBtn').addEventListener('click', () => {
        showSection('character-main');
    });
    document.querySelector('.itemBtn').addEventListener('click', () => {
        showSection('item-main');
    });
});
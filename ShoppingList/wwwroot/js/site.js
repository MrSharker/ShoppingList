$('body').on('input', '.input-number', function () {
	this.value = this.value.replace(/[^0-9,]/g, '');
});

document.querySelector('.toggle-button').addEventListener('click', () => {
	document.querySelector('#filterForm').classList.toggle('show')
})
const container = document.getElementById('container');

function setActive() {
	const path = getPathName();
	if (path.toLowerCase().indexOf("signup") !== -1) {
		container.classList.add("right-panel-active");
	}
}

setActive();

function getPathName() {
	return window.location.pathname;
}